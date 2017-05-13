/// <reference path="../jquery-1.10.2.intellisense.js" />
/// <reference path="../jquery-1.10.2.js" />

var reg_type = 0;
var ind_type = 24;


(function () {
    var country_chooser_open = false;
    var group_menu_open = false;
    var excel_menu_open = false;
    var st_max = 0;
    var titles = { "okopf": "в соответствии с ОКОПФ - Общероссийский классификатор организационно-правовых форм", "okfs": "в соответствии с ОКФС - Общероссийский классификатор форм собственности" };
    var indtitles = { "i1": "Общероссийский классификатор видов экономической деятельности", "i99": "Общероссийский классификатор видов экономической деятельности", "i2": 'Общесоюзный классификатор [Отрасли народного хозяйства]' };
    var regtitles = { "i0": "Общероссийский классификатор объектов административно-территориального деления", "i5": "по регистрации в налоговых органах" };
    var countParams = [30, 50, 100];
    var searching = 0;
    var tree_active;
    var tree_table;
    var src;
    var now = new Date();

    var yellow_color = "#fc8f00";
    var red_color = "#cd2b25";
    var green_color = "#03bc00";
    var grey_color = "#ddd";

    window.search_init = function () {
        $('#country_selector_btn').click(function () {
            _switch_country_chooser();
        });
        $('body').on('click', function (e) {
            _hide_menu_auto(e);
        });

        $("#base_block").bind("keypress", function (e) { try2search(e); });

        $('body').on('click', function (e) {
            hidepopups(e);
            _hide_command_menu(e);
            hideClock();
        });

        getObj("xls_frame").onreadystatechange = hideClock;
        document.onreadystatechange = hideClock;

        $("#xls_frame").ready(function () {
            hideClock();
        })

        $('#extended_switcher').click(function () {
            _toggle_extented_search(false);
        });


        $('#comp').keyup(function (event) {
            show_bones(event);
        })

        if (user_id > 0) {
            var dates = $("#dfrom, #dto").datepicker({
                changeMonth: true,
                showButtonPanel: true,
                changeYear: true,
                yearRange: "1990:" + now.getFullYear(),
                onSelect: function (selectedDate) {
                    var option = this.id == "dfrom" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat ||
                    $.datepicker._defaults.dateFormat,
                    selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                    search_check_changes();
                }
            });
        }

        $('[data-check]').change(function () {
            search_check_changes();
        })

        search_check_changes();
    }


    window.search_check_changes = function () {
        var changes_count = 0;
        $('[data-check]').each(function () {
            var $el = $(this);
            if ($el.attr("id") == "gropVal") {
                if ($el.attr("val") != "0") {
                    changes_count++;
                }
            } else if ($el.attr("type") == "text" || $el.attr("type") == "hidden") {
                if ($el.val() != "") {
                    changes_count++;
                }
            } else {
                if ($el.prop("checked")) {
                    changes_count++;
                }
            }

        });
        //if (getObj("archive").checked) changes_count--;
        //else changes_count++;

        if (changes_count == 0) {
            $('#extended_switcher_inner').text('Расширенный поиск');
        } else {
            $('#extended_switcher_inner').html('Расширенный поиск: <span id="change_count">(Выбрано критериев: ' + changes_count+")</span>");
        }
    }

    var _switch_country_chooser = function () {
        if (country_chooser_open) {
            $('#country_selector_menu').hide();
        } else {
            $('#country_selector_menu').show();
        }

        country_chooser_open = !country_chooser_open;
    };

    var _hide_menu_auto = function (e) {
        if (country_chooser_open) {
            var $menu = $('#country_selector_menu');
            if (!$menu.is(e.target) && $menu.has(e.target).length === 0 && !($('#country_selector_btn').is(e.target) || $('.country_chooser').is(e.target))) {
                $('#country_selector_menu').hide();
                country_chooser_open = false;
            }
        }
    };

    var _hide_command_menu = function (e) {

        if (group_menu_open) {
            $('.sub_command_menu').remove();
            group_menu_open = false;
        }

        if (excel_menu_open) {
            $('.sub_command_menu').remove();
            excel_menu_open = false;
        }
    }

    var mainSearcher = function (pg, group_name) {
        bones_pressed = false;
        var codes = ($("#codes").val() != "ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН") ? $("#codes").val() : '';
        var group_id = $("#gropVal").attr("val");
        var rcount = $("#countVal").attr("val");
        var top1000 = ((group_name.length > 0 && pg == -1000) ? "1" : (pg == -2000) ? "2" : "0");
        var params = {
            "company": ($("#comp").val()).replace(/\"/g, "").replace(/\'/g, ""),
            "strict": 0, //((getObj("rsv").checked) ? 3 : 0), //0,
            "phone": $("#phone").val(),
            "address": $("#addr").val(),
            "ruler": ($("#ruler").val()).replace(/\"/g, "").replace(/\'/g, ""),
            "constitutor": ($("#constitutor").val()).replace(/\"/g, "").replace(/\'/g, ""),
            "regions": $("#reg_val").val(),
            "is_okato": (String($("input[name='reg_type']:checked").val()) == "0") ? 1 : 0,
            "reg_excl": (String($("#reg_excl").val()).length == 0) ? 0 : $("#reg_excl").val(),
            "industry": $("#ind_val").val(),
            "is_okonh": "0",// не используется
            "ind_excl": (String($("#ind_excl").val()).length == 0) ? 0 : $("#ind_excl").val(),
            "ind_main": ($("input[name='ind_type']:checked").val() - 99 == 0) ? "1" : "0",
            "okopf": $("#okopf_val").val(),
            "okopf_excl": (String($("#okopf_excl").val()).length == 0) ? 0 : $("#okopf_excl").val(),
            "okfs": $("#okfs_val").val(),
            "okfs_excl": (String($("#okfs_excl").val()).length == 0) ? 0 : $("#okfs_excl").val(),
            "rfi": -1, //$("#rfi_val").val(), //не используется
            "rfi_excl": 0, //не используется
            "trades": '0' + /*((getObj("l1").checked)?"1":"0")+*/ ((getObj("l2").checked) ? "1" : "0") + ((getObj("l3").checked) ? "1" : "0"),
            "gaap": ((getObj("gaap").checked) ? 1 : 0),
            "filials": ((getObj("filials").checked) ? 1 : 0),
            "archive": ((getObj("archive").checked) ? 1 : 0),
            "bankrupt": ((getObj("bankr").checked) ? 1 : 0),
            "msp": ((getObj("msp").checked) ? 1 : 0),
            "rsbu": ((getObj("rsbu").checked) ? 1 : 0),
            "status": ((getObj("status").checked) ? 1 : 0),
            "kod": "", //$("#codes").val(),
            "kod_type": 0,//$("#code_selector").closest("td").find("input:radio:checked").val(),//не используется
            "dbeg": $("#dfrom").val(),
            "dend": $("#dto").val(),
            "group_id": group_id,//$("#user_groups").val(),
            "page_no": pg,
            "rcount": rcount,//$("#rc").val(),
            "user_id": user_id, //<%=current_user_id%>,
            "top1000": top1000,
            "group_name": ((group_name.length > 0 && pg == -1000) ? group_name : ""),
            "fas": $("#fas_val").val(),
            "fas_excl": (String($("#fas_excl").val()).length == 0) ? 0 : $("#fas_excl").val(),
            "rgstr": ((getObj("rgstr").checked) ? 1 : 0)
        }
        //ключевые слова
        var key_name = ClearText(String($("#comp").val()));
        var key_ruler = ClearText(String($("#ruler").val()));
        var key_constr = ClearText(String($("#constitutor").val()));
        var key_addr = ClearText(String($("#addr").val()));
        var key_phone = ClearText(String($("#phone").val())).replace("(", "").replace(")", "").replace(" ", "");

        // поиск
        if (pg > 0) {
            $.post("/DBSearchRu/CompaniesDoSearch", params, function (data) {
                _toggle_extented_search(true);
                var re = new RegExp("icon_error", "ig")
                //if(String(re_group.exec(data))=="null"){ 
                //getObj("search_result").innerHTML=data;
                //GenerateResult(pg, rcount, data, roles_object, key_name, key_ruler, key_constr, key_addr, key_phone, group_id, user_id);
                GenerateResult(data, roles_object, group_id, user_id);
                hideClock();
                if (String(re.exec(data)) == "null") {
                    if ($("#search_table").css("display") == "block" || $("#search_table").css("display") == "table") {
                        $('#hide_form').show();
                        toggle_form();
                    }
                }

                hideClock();
                $("#btn_search").val("Найти");
                searching = 0;
                location.href = "#top";
            },
            "html");
        }

        // добавление 10000 в группу
        if (pg == -1000) {
            $.post("/DBSearchRu/CompaniesDoSearch", params, function (data) {
                var ret = SaveResultGroup(data, group_name);
                var amsg = String(ret).split("_");
                showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
            },
           "json");
        }

        // экспорт в Excel
        if (pg == -2000) {
            hideClock();
            var form = document.createElement("form");
            form.action = "/DBSearchRu/CompaniesDoSearchExcel";
            form.method = "POST";
            element = document.createElement("input");
            element.type = "hidden";
            element.name = "so_string";
            element.value = JSON.stringify(params);
            form.appendChild(element);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
            /*
            $.post("/DBSearchRu/CompaniesDoSearch", params, function (data) {
                if (data) { DownLoadXLS1000(data); }
                hideClock();
                
            });
            */
            searching = 0;
        }

    };

    var GenerateResult = function (data, ro, group_id, user_id)
    {
        $("#search_count").html('').show();
        $("#search_result").show();
        $("#export_block").html('').show();
        $(".res_item").remove();
        $(".non_result").remove();
        $("#page_counter").remove();
        $("#total").html('');
        $("#note").remove();
        $("#total_found").remove();
        $("#page").remove();
        $("#rcount").remove();
        $("#res_block").append(data).show();

        var total_found = $("#total_found").val();
        var page = $("#page").val();
        var rcount = $("#rcount").val();
        var page_count = total_found / rcount;
        if (total_found > 10000) page_count = 10000 / rcount;
        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total_found && total_found > 0) {
            var total_text;

            if (group_id == "0") {
                total_text = "Всего найдено: " + total_found;
            }
            else {
                total_text = "Предприятий в группе: " + total_found;
            }
            if (total_found > 10000) {
                total_text += " (Выводятся первые 10000 предприятий)";
            }

            //$("#search_count").append("<span class=\"total_count\">" + total_text + "</span>");
            $("#total").html(total_text).show();

            if (ro.canAddToGroup || ro.canExport) {
                var $export_block = $("#export_block");
                var $checkall_block = $('<div>').addClass("checkall_block");
                $checkall_block.append("<span class=\"title\"><em>Выделить все</em><input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/></span>");
                $export_block.append($checkall_block);

                var $export_command_block = $('<div>').addClass("export_command_block");
                if (ro.canAddToGroup) {
                    var $add_group_btn = $("<div id=\"btnaddGroup\"><span class=\"title\"><em>Добавить в Группу</em><span class=\"icon-plus icon\"></span></span></div>");
                    $export_command_block.append($add_group_btn);
                    $add_group_btn.click(function (e) {
                        generate_sub_addGroupmenu(e, total_found);
                    })

                }
                if (ro.canExport) {
                    var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"title\"><em>Экспорт в Excel</em><span class=\"icon-file-excel icon\"></span></span></div>");
                    $export_command_block.append($add_excel_btn);
                    $add_excel_btn.click(function (e) {
                        generate_sub_excelmenu(e, total_found > 10000 ? 10000 : total_found);
                    });

                }
                $export_block.append($export_command_block);
            }

            $("div[id^='asl_']").html('');
            $("div[id^='sl_']").html('');

            $("div[id^='aslb_']").each(function (e) {
                var ogrn = $(this).attr("grn");
                var inn = $(this).attr("inn");
                GetStopLight(ogrn, inn);
            });

            $("div[id^='slb_']").each(function (e) {
                var ogrn = $(this).attr("grn");
                var inn = $(this).attr("inn");
                GetActionStopLight(ogrn, inn);
            });

            if (user_id > 0) {
                $("#search_result").append("<div id='note' class='explain' style='float:left; margin-top:25px;'>*Сведения о прекращении деятельности (реорганизации, ликвидации ) отсутствуют.</div>");
            }
            $("#search_result").append(_get_paging(page, page_count))
            $("#links").html("<a class=\"link\" href=\"javascript:showSeekWin()\">Если Вы не нашли интересующую Вас компанию, попробуйте поискать в ЕГРЮЛ (сформировать выписку)</a>");
            $("#links").removeClass("links_empty").removeClass("links_nofind").addClass("links_box");
        }
        else {
            $("#export_block").html('').hide();
            var $el = $('<div>').addClass("non_result").html("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
            $("#links").html("<a class=\"link\" href=\"javascript:showSeekWin()\">Если Вы не нашли интересующую Вас компанию, попробуйте поискать в ЕГРЮЛ (сформировать выписку)</a>");
            $("#links").removeClass("links_box").removeClass("links_empty").addClass("links_nofind");
        }

    }

    var GenerateResult_old = function (page, rcount, data, ro, key_name, key_ruler, key_constr, key_addr, key_phone, group_id, user_id) {
        $("#search_result").html('').show();

        var total = (data) ? data.hits.hits.length : 0;
        var total_found = (data) ? data.hits.total : 0;
        var page_count = total_found / rcount;
        if (total_found > 10000) page_count = 10000 / rcount;

        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total && total > 0) {

            var total_text;

            if (group_id == "0") {
                total_text = "Всего найдено: " + total_found;
            }
            else {
                total_text = "Предприятий в группе: " + total_found;
            }
            if (total_found > 10000) {
                total_text += " (Выводятся первые 10000 предприятий)";
            }

            $("#search_result").append("<span class=\"total_count\">" + total_text + "</span>");

            var res_arr = data.hits.hits;
            var $res_block = $('<div>').addClass("res_block");

            if (ro.canAddToGroup || ro.canExport) {
                var $export_block = $('<div>').addClass("export_block");

                var $checkall_block = $('<div>').addClass("checkall_block");
                $checkall_block.append("<span class=\"title\"><em>Выделить все</em><input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/></span>");
                $export_block.append($checkall_block);

                var $export_command_block = $('<div>').addClass("export_command_block");
                if (ro.canAddToGroup) {
                    var $add_group_btn = $("<div id=\"btnaddGroup\"><span class=\"title\"><em>Добавить в Группу</em><span class=\"icon-plus icon\"></span></span></div>");
                    $export_command_block.append($add_group_btn);
                    $add_group_btn.click(function (e) {
                        generate_sub_addGroupmenu(e, total_found);
                    })

                }
                if (ro.canExport) {
                    var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"title\"><em>Экспорт в Excel</em><span class=\"icon-file-excel icon\"></span></span></div>");
                    $export_command_block.append($add_excel_btn);
                    $add_excel_btn.click(function (e) {
                        generate_sub_excelmenu(e, total_found > 10000 ? 10000 : total_found);
                    });

                }
                $export_block.append($export_command_block);
                $res_block.append($export_block);
            }

            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var it = res_arr[i]._source;
                var $res_item = $('<div>').addClass("res_item");

                if (user_id > 0) {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + it.issuer_id + "_" + it.type_id + "\"></input>");
                    $res_item.append($check_block);
                }

                var $info_block = $('<div>').addClass("info_block");
                var comp_name = it.short_name;
                if (comp_name == null) comp_name = it.name;
                if (it.ticker.length > 0) {
                    $info_block.append('<a class="comp_title" href="/issuers/' + it.ticker + '" target="_blank">' + comp_name + '</a>');
                } else {
                    $info_block.append('<span class="comp_title">' + comp_name + '</span>');
                }

                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";

                if (key_name != "") {
                    comp_info_text += GetNameHistory(res_arr[i], key_name);
                }

                if (user_id > 0 && it.del_date != null) {
                    comp_info_text += "<p class=\"attention\">Исключено из реестра Росстата " + it.del_date + "</p>";
                }

                if (it.address != null) {
                    comp_info_text += "<p>" + it.address + "</p>";
                }
                if (key_addr != "") {
                    comp_info_text += GetAddressHistory(res_arr[i], key_addr);
                }
                if (key_phone != "") {
                    comp_info_text += GetPhoneHistory(res_arr[i], key_phone);
                }

                var manager = GetRuler(it);
                if (manager != "") {
                    comp_info_text += "<p>Руководитель: " + manager + "</p>";
                }
                if (key_ruler != "") {
                    comp_info_text += GetRulerHistory(res_arr[i], key_ruler);
                }
                if (it.okved_name != null) {
                    comp_info_text += "<p>" + it.okved_name + "</p>";
                }

                if (key_constr != "") {
                    comp_info_text += GetConstitutorsHistory(res_arr[i], key_constr);
                }

                if (user_id > 0 && it.ogrn != null) {
                    comp_info_text += "<p>Сведения о состоянии: ";
                    if (it.status != 0) {
                        comp_info_text += "<span class=\"attention\">" + it.status_name + " от " + it.status_date + "</span>";
                    }
                    else {
                        comp_info_text += "Действующее*";
                    }
                    comp_info_text += "</p>";
                }
                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                var $code_block = $('<div>').addClass("code_block");
                comp_info_text = "";
                if (it.ogrn != null) {
                    comp_info_text += "<p><span class=\"code_title\">ОГРН:</span>" + it.ogrn + "</p>";
                }
                if (it.inn != null) {
                    comp_info_text += "<p><span class=\"code_title\">ИНН:</span>" + it.inn + "</p>";
                }
                if (it.okpo != null) {
                    comp_info_text += "<p><span class=\"code_title\">ОКПО:</span>" + it.okpo + "</p>";
                }
                $code_block.html(comp_info_text);

                var $command_block = $('<div>').addClass("command_block");

                var $sl_info_block = $('<div>').addClass("sl_info_block");
                if (user_id > 0 && it.ogrn != null && it.uniq) {
                    var $stoplight_block = $("<div id=\"slb_" + it.ogrn + "\">").addClass("stoplight_block").attr("hidden", "true");
                    $stoplight_block.append("<div>Уровень надежности</div>");
                    $stoplight_block.append("<div class=\"sl_chart\" id=\"sl_" + it.ogrn + "\"></div>");

                    var $actionstoplight_block = $("<div id=\"aslb_" + it.ogrn + "\">").addClass("actionstoplight_block").attr("hidden", "true");
                    $actionstoplight_block.append("<div>Индекс активности</div>");
                    $actionstoplight_block.append("<div class=\"sl_chart\" id=\"asl_" + it.ogrn + "\"></div>");

                    $sl_info_block.append($actionstoplight_block);
                    $sl_info_block.append($stoplight_block);

                    GetStopLight(it.ogrn, it.inn);
                    GetActionStopLight(it.ogrn, it.inn);
                }
                $command_block.append($sl_info_block);

                $res_item.append($info_block);
                $res_item.append($code_block);
                $res_item.append($command_block);
                $res_block.append($res_item);
            }
            $("#search_result").append($res_block);
            if (user_id > 0) {
                $("#search_result").append("<div class='explain' style='float:left; margin-top:25px;'>*Сведения о прекращении деятельности (реорганизации, ликвидации ) отсутствуют.</div>");
            }
            $("#search_result").append(_get_paging(page, page_count))
            $("#links").html("<a class=\"link\" href=\"javascript:showSeekWin()\">Если Вы не нашли интересующую Вас компанию, попробуйте поискать в ЕГРЮЛ (сформировать выписку)</a>");
            $("#links").removeClass("links_empty").removeClass("links_nofind").addClass("links_box");
        } else {
            var $el = $('<div>').addClass("non_result").html("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
            $("#links").html("<a class=\"link\" href=\"javascript:showSeekWin()\">Если Вы не нашли интересующую Вас компанию, попробуйте поискать в ЕГРЮЛ (сформировать выписку)</a>");
            $("#links").removeClass("links_box").removeClass("links_empty").addClass("links_nofind");
        }

    }

    var GetStopLight = function (ogrn, inn) {
        var params = { "ogrn": ogrn };
        $.get("/DBSearchRu/GetStopLight", params, function (data) {
            ShowStopLight(ogrn, data.rating, data.factors_count);
        }, "json");
    }

    var GetActionStopLight = function (ogrn, inn) {
        var params = { "ogrn": ogrn, "inn": inn };
        $.get("/DBSearchRu/GetActionStopLight", params, function (data) {
            if (data.total_count !==null)
                ShowActionStopLight(ogrn, data.total_count);
        }, "json");
    }

    var ShowStopLight = function (ogrn, rating, factors_count) {
        //Серый рейтинг не показываем
        if (rating == 0)
            return;

        var size = 50;
        var lineWidth = 5;
        var rotate = 0;
        var el = document.getElementById("sl_" + ogrn);
        if (el == null) return;

        var canvas = document.createElement('canvas');
        canvas.className += " sl_canvas"
        var span = document.createElement('span');
        span.className += " sl_span"
        span.textContent = factors_count;

        var color = grey_color;
        if (rating === 1) {
            color = green_color;
        } else if (rating == 2) {
            color = yellow_color;
        } else if (rating == 3) {
            color = red_color;
        }

        span.style.color = color;

        if (typeof (G_vmlCanvasManager) !== 'undefined') {
            G_vmlCanvasManager.initElement(canvas);
        }

        var ctx = canvas.getContext('2d');
        canvas.width = canvas.height = size;

        if (factors_count != "0") {
            el.appendChild(span);
        }
        el.appendChild(canvas);

        ctx.translate(size / 2, size / 2); // change center
        ctx.rotate((-1 / 2 + rotate / 180) * Math.PI); // rotate -90 deg

        //imd = ctx.getImageData(0, 0, 240, 240);
        var radius = (size - lineWidth) / 2;

        var drawCircle = function (color, lineWidth, percent) {
            percent = Math.min(Math.max(0, percent || 1), 1);
            ctx.beginPath();
            ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
            ctx.strokeStyle = color;
            ctx.lineCap = 'round'; // butt, round or square
            ctx.lineWidth = lineWidth
            ctx.stroke();
        };
        drawCircle(color, lineWidth, 100 / 100);

        document.getElementById("slb_" + ogrn).removeAttribute("hidden");
    }

    var ShowActionStopLight = function (ogrn, percent) {
        var size = 50;
        var lineWidth = 8;
        var rotate = 0;
        var el = document.getElementById('asl_' + ogrn); // get canvas
        if (el == null) return;

        var canvas = document.createElement('canvas');
        canvas.className += " asl_canvas"
        var span = document.createElement('span');
        span.className += " asl_span"
        span.textContent = percent >= 0 ? percent : "*";

        var color = grey_color;
        if (percent > 50) {
            color = green_color;
        } else if (percent > 0) {
            color = yellow_color;
        } else if (percent == 0) {
            color = red_color;
        }

        span.style.color = color;

        if (typeof (G_vmlCanvasManager) !== 'undefined') {
            G_vmlCanvasManager.initElement(canvas);
        }

        var ctx = canvas.getContext('2d');
        canvas.width = canvas.height = size;

        el.appendChild(span);
        el.appendChild(canvas);

        ctx.translate(size / 2, size / 2); // change center
        ctx.rotate((-1 / 2 + rotate / 180) * Math.PI); // rotate -90 deg

        //imd = ctx.getImageData(0, 0, 240, 240);
        var radius = (size - lineWidth) / 2;

        var drawCircle = function (color, lineWidth, percent) {
            percent = Math.min(Math.max(0, percent || 1), 1);
            ctx.beginPath();
            ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
            ctx.strokeStyle = color;
            ctx.lineCap = 'round'; // butt, round or square
            ctx.lineWidth = lineWidth
            ctx.stroke();
        };

        drawCircle(grey_color, lineWidth, 100 / 100);
        if (percent >= 0) {
            drawCircle(color, lineWidth, percent / 100);
        }

        document.getElementById("aslb_" + ogrn).removeAttribute("hidden");
    }

    var generate_sub_addGroupmenu = function (e, total) {
        if (group_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Добавить выбранные компании в Группу").click(function () { doSave2List(); }).appendTo($ul);
        $("<li>").text(total > group_limit ? "Добавить первые " + group_limit + " компаний в Группу" : "Добавить найденные компании (" + total + ") в Группу").click(function (event) { doCreateGroup1000(event); }).appendTo($ul);
        $("#btnaddGroup").append($ul);
        group_menu_open = true;
        e.stopPropagation();
    }

    var generate_sub_excelmenu = function (e, total) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным компаниям").click(function () { doSave2XLS(); }).appendTo($ul);
        $("<li>").text("Экспорт по " + (total == 10000 ? "первым " : "") + total + " компаниям").click(function () { doExport10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
    }

    var SaveResultGroup = function (data, group_name) {
        if (data) {
            for (var j = 0, j_max = (extGroupList.length - 1) ; j <= j_max; j++) {
                if (extGroupList[j][1] == group_name) {
                    return "1_Группа с таким именем уже есть";
                }
            }
            var issuers = "";
            for (var i = 0; i <= (data.length - 1) ; i++) {
                var it = data[i];
                issuers += it.issuer_id + "_" + it.type_id + ",";
            }
            doSaveToUserList(issuers, group_name, true);
            hideClock();
            return "0_В группе сохранено " + data.length + " компаний"
        }
        else {
            return "1_Внутренняя ошибка";
        }
    };

    var GetNameHistory = function (it, key_name) {
        if (it.inner_hits) if (it.inner_hits.key_list) {
            var hits_count = it.inner_hits.key_list.hits.hits.length;
            if (hits_count > 0) {
                var a = it.inner_hits.key_list.hits.hits[0]._source;
                if (!a.is_old) {
                    return "";
                }
                else {
                    if (a.key_type == 4 || a.key_type == 5 || a.key_type == 51 || a.key_type == 52) {
                        return "<p><span class='history'>" + a.key_value + "</span></p>";
                    }
                }
            }
        }
        return "";
    }

    var ClearName = function (txt) {
        txt = txt.replace(/\&/g, '').replace(/\amp;/g, '');
        return txt;
    }

    var FindKeys = function (text, keys) {
        var n = 0;
        if (text == null) return 0;
        text = ClearText(text);
        var names = text.replace(/\&/g, '').replace(/\amp;/g, '').replace(/\?/g, '');
        names = names.split(';');
        for (var i = 0; i < keys.length; i++) {
            for (var j = 0; j < names.length; j++) {
                if (names[j] == keys[i]) {
                    n++;
                    break;
                }
            }
        }
        return (n == keys.length) ? 1 : 0;
    }

    var GetRuler = function (item) {
        var manager = "";

        for (var i = 0; i < item.manager_list.length; i++) {
            if (!item.manager_list[i].is_old) {
                manager += (manager != "" ? ", " : "") + item.manager_list[i].fio;
                if (item.manager_list[i].position != null) {
                    manager += " - " + item.manager_list[i].position;
                }
                if (item.manager_list[i].inn != null) {
                    manager += " (ИНН: " + item.manager_list[i].inn + ")";
                }
            }
        }

        return manager;
    }

    var GetRulerHistory = function (it, key_ruler) {
        var hits_count = it.inner_hits.manager_list.hits.hits.length;
        if (hits_count > 0) {
            var a = it.inner_hits.manager_list.hits.hits[0]._source;
            var manager = a.fio;
            if (a.position != null) manager += " - " + a.position;
            if (a.inn != null) manager += " (ИНН: " + a.inn + ")";
            if (!a.is_old) {
                return "";
            }
            else {
                return "<p>Руководитель: <span class='history'>" + manager + "</span></p>";
            }
        }
        return "";
    }

    var GetConstitutorsHistory = function (it, key_constr) {
        var hits_count = it.inner_hits.constitutor_list.hits.hits.length;
        if (hits_count > 0) {
            var a = it.inner_hits.constitutor_list.hits.hits[0]._source;
            var constitutor = a.name;
            if (a.inn != null) constitutor += " (ИНН: " + a.inn + ")";
            if (!a.is_old) {
                return "<p>Учредитель: " + constitutor + "</p>";
            }
            else {
                return "<p>Учредитель: <span class='history'>" + constitutor + "</span></p>";
            }
        }
        return "";
    }

    var GetAddressHistory = function (it, key_addr) {
        var hits_count = it.inner_hits.address_list.hits.hits.length;
        if (hits_count > 0) {
            var a = it.inner_hits.address_list.hits.hits[0]._source;
            if (!a.is_old) {
                return "";
            }
            else {
                var addr = "<p><span class='history'>" + a.address + "</span></p>";
                return addr;
            }
        }
        return "";
    }

    var GetPhoneHistory = function (it, key_phone) {
        var hits_count = it.inner_hits.phone_list.hits.hits.length;
        if (hits_count > 0) {
            var a = it.inner_hits.phone_list.hits.hits[0]._source;
            if (!a.is_old) {
                var phone = "<p>Телефон: " + a.phone + "</p>";
                return phone;
            }
            else {
                var phone = "<p>Телефон: <span class='history'>" + a.phone + "</span></p>";
                return phone;
            }
        }
        return "";
    }

    var DownLoadXLS1000 = function (filename) {
        var form = document.createElement("form");
        form.action = "/DBSearchRu/GetFile";
        form.method = "POST";
        if (!document.addEventListener) {
            showClock();
            form.target = "xls_frame"
        } else {
            form.target = "blank"
        }
        form.style.display = "none"
        form.appendChild(make_input("src", filename));
        form.appendChild(make_input("page", "dbsearchru/companies"));
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    };


    var doExport10000 = function () {
        showClock();
        mainSearcher(-2000, '');
    }

    var make_input = function (name, value) {
        var element = null;
        element = document.createElement("input");
        element.type = "text";
        element.name = name;
        element.value = value;
        return element;
    };


    var checkCanSearch = function () {
        var ss = $("#comp").val() + $("#phone").val() + $("#addr").val() + $("#ruler").val() +
            $("#reg_val").val() + $("#ind_val").val() + $("#okopf_val").val() + $("#okfs_val").val() +
            $("#status_val").val() +
            ((getObj("l2").checked) ? "1" : "") + ((getObj("l3").checked) ? "1" : "") +
            ((getObj("gaap").checked) ? "1" : "") + ((getObj("bankr").checked) ? "1" : "") + $("#codes").val() +
            $("#dfrom").val() + $("#dto").val() + ((String($("#gropVal").attr("val")) == "0") ? "" : "1");
        return (ss.length == 0) ? false : true;
    };

    var doStop = function () {
        hideClock();
        searching = 0;
        $.post("/iss/modules/operations.asp", { "action": "6" });
        $("#btn_search").val("Найти");
        window.stop();
    };



    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#comp').css("border-radius", "4px");
        }
    };

    var draw_selection = function (i) {
        if (getObj("tdhover" + i)) {
            var d = getObj("tdhover" + i)
            $("#dpw").find('td').each(function () {
                this.className = "bones_pointer";
            });
            d.className = "bones_pointer over_color";
            $("#comp").val($("#tdhover" + i).text());
        }

    };

    var try2search = function (e) {
        var kk
        if (window.event) {
            e = window.event;
        }
        kk = e.keyCode;
        if (e.stopPropagation) {
            e.stopPropagation();
        } else {
            e.cancelBubble = true;
        }
        /*if(bones_pressed){
            $("#sel_div_val").attr("sv",3);
            getObj("sel_div_val").innerHTML=searchNamesParams[3];
    
        }*/
        if (kk == 13) {
            doSearch();
        }

    };


    var remove_div = function (event, div_selector, exclude_selector) {

        var container = $(div_selector); //сам контейнер, который нужно убрать
        var exclude = $(exclude_selector);//окно поиска нажатием на которое появляется контейнер

        if (!container.is(event.target) // if the target of the click isn't the container...
        && container.has(event.target).length === 0  // ... nor a descendant of the container
        && !exclude.is(event.target)
        && exclude.has(event.target).length === 0) {
            container.remove();
            exclude.removeClass("customSelect_focus");

        }
    }

    var hidepopups = function (event) {
        close_bones();

        if (getObj(tree_active + "_window")) {
            getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window"));
            $("#" + tree_active).css({ 'background-color': '#FFFFFF' });
        }
        close_dialog();
        if (event) {
            remove_div(event, "#groupSelector", "#groupSelect");
            remove_div(event, "#countSelector", "#countSelect");
        }

    };

    var _get_paging = function (page, page_count) {
        if (page_count <= 1) {
            return "";
        }

        var html = "";
        var StartPage = 1;
        if (page_count >= 8) {
            StartPage = ((page - 3 > 0) ? ((page_count - page < 3) ? page_count + (page_count - page) - 8 : page - 3) : 1);
        }

        if (page > 1 && page_count > 7) {
            html += '<td onclick="doSearch(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="doSearch(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="doSearch(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    window._toggle_extented_search = function (only_need_close) {

        var $switch = $('#switcher_ico');

        if (only_need_close || $switch.hasClass("icon-angle-up")) {
            $switch.removeClass("icon-angle-up").addClass("icon-angle-down");
            $("#extended_block").hide();
        } else {
            $switch.removeClass("icon-angle-down").addClass("icon-angle-up");
            $("#extended_block").show();
        }

    };

    var show_bones = function (e) {
        var d;
        $("#base_block").unbind();
        e.stopPropagation();

        if ((e.keyCode == 40 || e.keyCode == 38) && getObj("dp_window")) {
            skip(e.keyCode);
        } else {

            if (!getObj("dp_window") && !(e.keyCode == 13 || e.keyCode == 27)) {
                d = document.createElement("div");
                d.id = "dp_window";
                st_selected = -1;
                d.className = "dp_window";
                d.style.height = "120px";

                $("#comp").css("border-radius", "4px 4px 0 0");

                var boundes = $("#comp").position();
                d.style.top = boundes.top + 94 + $("#comp").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#comp").width() + 23) + "px";
                d.style.display = "none";
                document.body.appendChild(d);
            } else {
                d = getObj("dp_window");
            }
            if ((e.keyCode == 13 || e.keyCode == 27) && getObj("dp_window")) {
                document.body.removeChild(getObj("dp_window"));
                if (e.keyCode == 13) {
                    bones_pressed = true;
                    if (st_selected >= 0) {
                        $('input:checkbox[name=rsv]').attr('checked', true);

                    }
                    try2search(e);
                    st_selected = -1;
                }
            }
            $("html").click(close_bones);
            if (getObj("dp_window")) {
                $.post("/DBSearchRU/GetBones", { "input": $("#comp").val() },
                    function (data) {
                        var htm = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" id=\"dpw\" onkeydown=\"skip(event)\">"
                        st_max = 0
                        if (data.length == 0 && getObj("dp_window")) {
                            document.body.removeChild(d);
                            st_selected = -1;
                        } else {
                            for (var i = 0; i < data.length; i++) {
                                htm += "<tr class=\"bones_pointer\"><td id=\"tdhover" + i + "\"  onclick=\"dispissuer('" + data[i].name + "')\" >" + data[i].name + "</td><td  style=\"text-align:right;width:150px;\">" + data[i].cnt + " совпадений</td></tr>";
                                st_max++;
                            }
                            htm += "</table>"
                            d.innerHTML = htm;
                            d.style.display = "block";
                        }
                    }, "json"
                );
            }
        }
        $("base_block").bind("keypress", function (e) { try2search(e) });
    };

    var show_dictionary = function () {
        var $placer = $("body"); // контенер для добавления
        var id = "dic_container"; //идентификатор диалогового окна
        var body = '<div id="' + id + '" class="modal fade" tabindex="-1" style="display: none;" aria-hidden="true">'
                    + '<div class="modal-dialog"><div class="modal-content">'
                    + '<div class="modal-header"><button class="close" aria-hidden="true" data-dismiss="modal" type="button">×</button>'
                    + '</div><div class="modal-body"><div id="td_' + tree_active + '"></div></div>'
                    + '<div class="modal-footer">'
                    + '</div>'
                    + '</div></div></div>';
        //Удалим старое содержимое
        $('#' + id).remove();

        $placer.append(body);

        $('#' + id).modal();
    };

    var restrictGropName = function (groupName) {
        if (groupName.length > 65)
            groupName = groupName.substring(0, 65) + "...";
        return groupName;
    }

    window.ClearText = function (txt) {
        txt = txt.toLowerCase().replace(/-/g, ';').replace(/\s+/g, ';').replace(/\"/g, "").replace(/\'/g, "").replace(/>/g, ";").replace(/</g, ";").replace(/\+/g, ";");
        return txt;
    }

    window.doSearch = function (pg) {
        close_bones();
        if (searching == 1) {
            doStop();
        } else {
            if (checkCanSearch()) {
                searching = 1;
                pg = (isNaN(String(pg))) ? 1 : pg;
                showClock();
                $("#btn_search").val("Остановить");
                mainSearcher(pg, '');
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        }
    };

    /*
    var doSave2XLS = function () {
        var issuers = "";

        if ($(".res_item").find('input:checkbox:checked').length > 0) {
            $(".res_item").find("input:checkbox:checked").each(function (i) {
                if (String(this.value).length > 0) {
                    if (String(this.value).indexOf("_") >= 0) {
                        issuers += "'" + String(this.value).substring(0, String(this.value).indexOf("_")) + "',";
                    } else {
                        issuers += "'" + String(this.value) + "',";
                    }
                }
            });
            issuers = issuers.substring(0, issuers.length - 1);
            var url = '/DBSearchRu/CompaniesGetExcel?issuers=' + issuers;
            window.open(url, '_blank');
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
        }

    };
    */

    var doSave2XLS = function () {
        var arr = Array();
        if ($(".res_item").find('input:checkbox:checked').length > 0) {
            $(".res_item").find("input:checkbox:checked").each(function (i) {
                if (String(this.value).length > 0) {
                    var issuers = (this.value).split("_");
                    arr.push({ "issuer_id": issuers[0], "type_id": issuers[1] });
                }
            });
            var form = document.createElement("form");
            form.action = "/DBSearchRu/CompaniesGetExcel";
            form.method = "POST";
            element = document.createElement("input");
            element.type = "hidden";
            element.name = "issuers";
            element.value = JSON.stringify(arr);
            form.appendChild(element);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
        }

    };


    window.doSetCheckedAll = function (chb) {
        $(".res_item").find("input:checkbox").each(function (i) {
            this.checked = chb.checked;
        });

    };

    window.checkOnOff = function (o) {
        var allCheckboxes = $(".res_item").find('input:checkbox');
        var checkedCheckboxes = $(".res_item").find('input:checkbox:checked');
        var allChecked = allCheckboxes.length == checkedCheckboxes.length;
        $("#selallbox").get(0).checked = allChecked;
    }

    window.doClear = function () {
        $("#countVal").text(countParams[0]);
        $("#countVal").attr("val", countParams[0]);
        $("#gropVal").text(restrictGropName(extGroupList[0].txt));
        $("#gropVal").attr("val", extGroupList[0].code);


        $("input").each(function (i) {

            if (this.type == "text") {
                this.value = "";
                this.title = "";
            }
            if (this.type == "hidden") {
                this.value = ""
            }
            if (this.type == "checkbox") {
                if (this.id == "ind_main" || this.id == "archive") {
                    this.checked = true;
                } else {
                    this.checked = false;
                }
            }
            if (this.type == "radio") {
                if (this.id == "ind_default") {
                    this.checked = true;
                    ch_ind_type(this);
                }
                if (this.id == "reg_default") {
                    this.checked = true;
                    ch_reg_type(this);
                }
                if (this.id == "c2") {
                    this.checked = true;
                }

            }


        });
        search_check_changes();
    };

    window.ch_ind_type = function (i_type) {
        ind_type = i_type.value;
    };

    window.ch_reg_type = function (r_type) {
        $("#reg").val("");
        $("#reg_val").val("");
        $("#reg_excl").val("");
        $("#reg").attr({ "title": "" });
        reg_type = r_type.value;
        $("#reg").attr({ "title": eval("regtitles.i" + r_type.value) });
    };

    window.skip = function (kod) {
        if (kod == 40 && st_selected < st_max - 1) {
            st_selected++;
            draw_selection(st_selected);
        }
        if (kod == 38) {
            if (st_selected > 0) {
                st_selected--;
                draw_selection(st_selected);
            } else {
                $("#dpw").find('td').each(function () {
                    this.className = "bones_pointer";
                });
                st_selected = -1
                getObj("comp").focus();
            }
        }
    };

    window.dispissuer = function (text) {
        $("#comp").val(text).css("border-radius", "4px");
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
        }
    };

    window.show_tree_selector = function (e, sr, is_tree, mult) {

        src = (sr == 99) ? 24 : sr;
        tree_table = src;
        hidepopups()
        if (window.event) {
            e = window.event;
            caller = e.srcElement;
        }
        if (e.stopPropagation) {
            e.stopPropagation();
            caller = e.target;
        } else {

            e.cancelBubble = true;
        }
        tree_active = caller.id;
        $("#" + tree_active).css({ 'background-color': '#F0F0F0' });
        var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
        if (!getObj(tree_active + "_window")) {
            d = document.createElement("iframe");
            d.className = "tree_frame";
            d.id = tree_active + "_window";

            d.frameBorder = "0"
            if (is_tree == 1) {
                d.src = "/Tree/TreeSelector?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val();
            } else {
                tree_table += 6;
                d.src = "/iss/selector/selector.asp?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val() + "&mult=" + mult;
            }
            var content = "<div id=\"td_" + tree_active + "\"></div>";
            show_dictionary();
            getObj("td_" + tree_active).appendChild(d);

            showContentClock('#dic_container .modal-dialog');

            $("#dic_container").click(function (event) {
                event.stopPropagation();

            })
        }
    };

    window.Write_TS = function (retval, is_excl) {
        if (retval.length > 0) {
            $.post("/Tree/GetResultString", { "src": tree_table, "id": retval },
            function (data) {
                $("#" + tree_active).val(((is_excl == -1) ? "Искл.: " : "") + data);
                $("#" + tree_active).attr({ "title": ((is_excl == -1) ? "Исключая: " : "") + data.replace(",", ",\n") });
                $("#" + tree_active + "_excl").val(is_excl);
                $("#" + tree_active + "_val").val(retval);
                search_check_changes();
            })
        } else {
            $("#" + tree_active).val("");
            $("#" + tree_active).attr({ "title": eval("titles." + tree_active) });
            $("#" + tree_active + "_excl").val(0);
            $("#" + tree_active + "_val").val("");
            search_check_changes();
        }
        hidepopups();

    };

    window.selectNewGroup = function (i) {
        $("#gropVal").text(restrictGropName(extGroupList[i].txt));
        $("#gropVal").attr("val", extGroupList[i].code);
        document.body.removeChild(getObj("groupSelector"));
        $("#groupSelect").removeClass("customSelect_focus");
        search_check_changes();
    }

    window.show_groupSelector = function () {
        var d;


        if (!getObj("groupSelector")) {
            d = document.createElement("div");
            d.id = "groupSelector";
            d.className = "dp_window";


            var boundes = $("#groupSelect").parent().position();
            d.style.top = boundes.top + 94 + $("#groupSelect").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#groupSelect").width() + 12) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("groupSelector"));
            $("#groupSelect").removeClass("customSelect_focus");
        };
        if (getObj("groupSelector")) {
            $("#groupSelect").addClass("customSelect_focus");
            var selected_i = 0;
            var htm = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" id=\"grouptbl\" >"
            for (var i = 0; i < extGroupList.length; i++) {
                if (extGroupList[i].code - $("#gropVal").attr("val") == 0) {
                    selected_i = i;
                }
                htm += "<tr class='bones_pointer'  onClick='selectNewGroup("
                 + i + ");'><td id='tdgroup" + i + "' class='bones_pointer' >" + extGroupList[i].txt + "</td></td><td class='bones_pointer' style='text-align:right;width:150px;color:#999;font-size:14px;white-space:nowrap;'>"
                  + (extGroupList[i].val == "" ? "все&nbsp;ЮЛ&nbsp;и&nbsp;ИП" : extGroupList[i].val) + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            if (extGroupList.length > 10)
                d.style.height = "180px";
            d.style.display = "block";
            d.scrollTop = 9 * selected_i
            $("#grouptbl").one("mouseenter", function () {
                $('#tdgroup' + $('#gropVal').attr('val')).attr('className', 'bones_pointer');
            })

        }
    }

    window.show_countSelector = function () {
        var d;


        if (!getObj("countSelector")) {
            d = document.createElement("div");
            d.id = "countSelector";
            d.className = "dp_window";


            var boundes = $("#countSelect").parent().position();
            d.style.top = boundes.top + 94 + $("#countSelect").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#countSelect").width() + 12) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("countSelector"));
            $("#countSelect").removeClass("customSelect_focus");
        }
        if (getObj("countSelector")) {
            $("#countSelect").addClass("customSelect_focus");
            var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">"
            for (var i = 0; i < countParams.length; i++) {
                htm += "<tr class='selec_option'  onClick='selectNewCount("
                 + i + ");'><td id='tdgroup" + i + "' id='tdgroup" + i + "' class='bones_pointer'>" + countParams[i] + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            d.style.display = "block";
            $("#cnttbl").one("mouseenter", function () {
                $('#tdgroup' + $('#countVal').attr('val')).attr('className', 'bones_pointer');
            })

        }
    }

    window.selectNewCount = function (i) {
        $("#countVal").text(countParams[i]);
        $("#countVal").attr("val", countParams[i]);
        document.body.removeChild(getObj("countSelector"));
        $("#countSelect").removeClass("customSelect_focus");
    }

    window.USERGROPS = [];

    window.doSave2List = function (issuer_id) {
        if ($(".res_item").find('input:checkbox:checked').length > 0 || issuer_id) {
            $.post("/Modules/GetGroups", null,
                        function (data) {
                            var err = 0
                            var htm = "<div id=\"group_dialog\"><h4>Выберите группу</h4><div class=\"scroll_select\"><table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"dpw\" >"
                            st_max = 0
                            try {
                                data = eval(data)
                                USERGROPS = data;
                            } catch (e) {
                                err = 1
                            }
                            if (err == 0) {
                                if (getObj("modal_dialog")) {
                                    close_dialog();
                                }
                                for (var i = 0; i < data.length; i++) {
                                    htm += "<tr><td  id=\"tdhover" + i + "\" class=\"bones_pointer\" onclick=\"savegroup(" + data[i].lid + ",'" + issuer_id + "')\" ><span id=\"s" + data[i].lid + "\">" + data[i].name + "</span></td></tr>";
                                }
                            }
                            htm += "</table></div>"
                            htm += "<h4>или создайте новую</h4><div><label class=\"label_form\" style=\"width:110px;margin-top:5px;\">Новая группа:</label><input type=\"text\" class=\"search_input\" style=\"width:200px;\" id=\"ngroup\"/><button  class=\"btns darkblue\" style=\"margin-left:5px;\"onclick=\"validateGroupNames('" + issuer_id + "');\" >Добавить в новую</button></div><div id=\"error_groups\" class=\"error_login\"></div>"
                            htm += "<div style='text-align:center;margin:20px 0;'></div></div>";
                            

                            show_dialog({ "content": htm, "is_print": false });
                            $("#group_dialog").click(function (event) {
                                event.stopPropagation();

                            });

                        }, "html");
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для включения в группу</p>', 3000);
        }
    };

    window.validateGroupNames = function (issuer_id) {
        $('#error_groups').html('');
        var new_name = $("#ngroup").val();
        for (var i = 0; i < USERGROPS.length; i++) {
            if (USERGROPS[i].name == new_name.trim()) {
                $('#error_groups').html('Данное имя группы уже существует');
                return;
            }
        }
        savegroup(0, issuer_id);
    }

    window.savegroup = function (group_id, issuer_id) {
        g_id = group_id;
        if (group_id != 0) {
            $("#ngroup").val("");
        }
        doSaveGroups(issuer_id);
        close_dialog();
    }

    /*
        window.doSave2List = function (issuer_id) {
            if ($(".res_item").find('input:checkbox:checked').length > 0 || issuer_id) {
                $.post("/Modules/GetGroups", null,
                            function (data) {
                                var err = 0
                                var htm = "<div id=\"group_dialog\"><div class=\"form-group\"><label class=\"label_form\" style=\"width:150px;\">Новая группа:</label><input type=\"text\" class=\"search_input\" style=\"width:350px;\" id=\"ngroup\"/></div><div class=\"scroll_select\"><table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"dpw\" >"
                                st_max = 0
                                try {
                                    data = eval(data)
                                } catch (e) {
                                    err = 1
                                }
                                if (err == 0) {
                                    if (getObj("modal_dialog")) {
                                        close_dialog();
                                    }
                                    for (var i = 0; i < data.length; i++) {
                                        htm += "<tr><td  id=\"tdhover" + i + "\" class=\"bones_pointer\" ><a href=\"#\"   onclick=\"dispgroup(this,'" + data[i].name + "'," + data[i].lid + ")\" id=\"s" + data[i].lid + "\">" + data[i].name + "</a></td></tr>";
                                    }
                                }
                                htm += "</table></div>"
                                htm += "<div style='text-align:center;margin:20px 0;'><button  class=\"btns darkblue\" onclick=\"doSaveGroups('" + issuer_id + "');\" >Сохранить</button></div></div>";
    
                                show_dialog({ "content": htm, "is_print": false });
                                $("#group_dialog").click(function (event) {
                                    event.stopPropagation();
    
                                });
    
                            }, "html");
            } else {
                showwin('critical', '<p align=center>Не отмечено ни одно предприятие для включения в группу</p>', 3000);
            }
        };
    */
    var doCreateGroup1000 = function (event) {
        event.stopPropagation();
        _hide_command_menu(event);
        var html = "<div id=\"group100dialog\"><div class=\"form-group\"><label class=\"label_form\" style=\"width:250px;\">Введите уникальное имя группы:</label><input type=\"text\" class=\"search_input\" style=\"width:250px;\" id=\"ngroup\"/></div>";
        html += "<div style='text-align:center;margin:20px 0;'><button  class=\"btns darkblue\" onclick=\"doSearch1000();\" >Сохранить</button></div></div>";
        show_dialog({ "content": html, "is_print": false });
        $("#group100dialog").click(function (event) {
            event.stopPropagation();

        });
    };

    window.doSearch1000 = function () {
        var group_name = $('#ngroup').val();
        if (group_name == "") {
            showwin('critical', '<p align=center>Отсутствует имя группы</p>', 2000);
            return;
        }
        $.get("/Modules/GetGroups", function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].name == group_name.trim()) {
                    showwin('critical', '<p align=center>Данное имя группы уже существует</p>', 2000);
                    return;
                }
            }
            showClock();
            mainSearcher(-1000, group_name);
        }, "json")
    };


    window.doAddEGRUL = function (issuer_id) {
        showClock()
        $.post("/MonitorOperations/AddToEgrul", { "id": issuer_id },
            function (data) {
                hideClock();
                if (data >= 0) {
                    showwin("info", "Добавлено компаний в список монитринга ЕГРЮЛ:" + data, 2000);
                    if (data > 0) {
                        getObj("tdiss_" + issuer_id).innerHTML = "<span class=\"title\"><em>Удалить из списка мониторинга ЕГРЮЛ</em><span class=\"monitor_icon_block\" onclick=\"delIssuer('" + issuer_id + "',1,true);\"><span class=\"icon-minus-squared-alt\"></span><span class=\"icon-binoculars\"></span></span></span>";

                    }
                }
                if (data < 0) {
                    showwin("critical", "Превышено допустимое количество <br/>компаний в списке мониторинга ЕГРЮЛ", 2000);
                }
            }
        )

    }

    window.delIssuer = function (id, t, issearch) {
        win_confirm('Удаление организации из списка мониторинга.', 'Вы действительно хотите удалить эту запись?', function (result) {
            if (result) {
                var service_url = t == 0 ? "/MonitorOperations/RemoveFromMess" : "/MonitorOperations/RemoveFromEgrul";
                $.post(service_url, { "o": 9 + t, "id": id }, function (data) {
                    if (!issearch) {
                        if (t == 0) {
                            load_mess_subscribe_data();
                        }
                        if (t == 1) {
                            load_egrul_subscribe_data();
                        }
                    }
                    if (getObj("tdiss_" + id)) {
                        getObj("tdiss_" + id).innerHTML = "<span class=\"title\"><em>Добавить в список мониторинга ЕГРЮЛ</em><span class=\"monitor_icon_block\" onclick=\"doAddEGRUL('" + id + "');\"><span class=\"icon-plus-squared-alt\"></span><span class=\"icon-binoculars\"></span></span></span>";
                    }

                })
            }
        });
    }

})();


$().ready(function () {
    search_init();
});
