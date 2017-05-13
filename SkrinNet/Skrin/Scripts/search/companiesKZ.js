/// <reference path="../jquery-1.10.2.intellisense.js" />
/// <reference path="../jquery-1.10.2.js" />


var ind_type = 32;

(function () {
    var country_chooser_open = false;
    var tree_active;
    var group_menu_open = false;
    var excel_menu_open = false;
    var indtitles = { "i1": "Общий классификатор видов экономической деятельности", "i99": "Общий классификатор видов экономической деятельности" };
    var countParams = [30, 50, 100];
    var searching = 0;
    var tree_active;
    var tree_table;
    var src;
    var now = new Date();



    window.doSearch = function (pg) {

        if (!roles_object.canSearch) {
            no_rights();
            return;
        }

        pg = (isNaN(String(pg))) ? 1 : pg;     
        close_bones();
        if (searching == 1) {
            doStop();
        } else {
            if (checkCanSearch()) {
                searching = 1;             
                showClock();
                $("#btn_search").val("Остановить");
                mainSearcher(pg, '');
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        }
    };

    window.search_init = function () {
        $('#country_selector_btn').click(function () {
            _switch_country_chooser();
        });
        $('body').on('click', function (e) {
            _hide_menu_auto(e);
        });

        $('#extended_switcher').click(function () {
            _toggle_extented_search(false);
        });

        $('body').on('click', function (e) {
            hidepopups(e);
            _hide_command_menu(e);
        });


        $('[data-check]').change(function () {
            search_check_changes();
        });

        $('#comp').keyup(function (event) {
            show_bones(event);
        })
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

        if (changes_count == 0) {
            $('#extended_switcher_inner').text('Расширенный поиск');
        } else {
            $('#extended_switcher_inner').text('Расширенный поиск: ' + changes_count);
        }
    }

    window.show_tree_selector = function (e, sr, is_tree, mult) {

        src = (sr == 99) ? 31 : sr;
        tree_table = src;
        hidepopups();
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
            $("#" + tree_active).attr({ "title": eval("indtitles." + tree_active) });
            $("#" + tree_active + "_excl").val(0);
            $("#" + tree_active + "_val").val("");
            search_check_changes();
        }
        hidepopups();

    };

    window.selectNewCount = function (i) {
        $("#countVal").text(countParams[i]);
        $("#countVal").attr("val", countParams[i]);
        document.body.removeChild(getObj("countSelector"));
        $("#countSelect").removeClass("customSelect_focus");
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

    window.ch_ind_type = function (i_type) {
        $("#ind").val("");
        $("#ind_val").val("");
        $("#ind_excl").val("");
        $("#ind").attr({ "title": "" });
        $("#ind").attr({ "title": eval("indtitles.i" + i_type.value) });
        ind_type = i_type.value;
    };

    window.doClear = function () {
        $("#countVal").text(countParams[0]);
        $("#countVal").attr("val", countParams[0]);

        $("input").each(function (i) {

            if (this.type == "text") {
                this.value = "";
                this.title = "";
            }
            if (this.type == "hidden") {
                this.value = ""
            }
            if (this.type == "checkbox") {
                if (this.id == "ind_main") {
                    this.checked = true;
                } else {
                    this.checked = false;
                }
                if (this.id == "rsv") {
                    this.checked = false;
                }
            }
            if (this.type == "radio") {
                if (this.id == "ind_default") {
                    this.checked = true;
                    ch_ind_type(this);
                }          
                if (this.id == "c2") {
                    this.checked = true;
                }

            }


        });
        search_check_changes();
    };
    /*
    window.search_check_changes = function () {
        var changes_count = 0;
        $('[data-check]').each(function () {
            var $el = $(this);
            if ($el.attr("type") == "text" || $el.attr("type") == "hidden") {
                if ($el.val() != "") {
                    changes_count++;
                }
            } else {
                if ($el.prop("checked")) {
                    changes_count++;
                }
            }


        });

        if (changes_count == 0) {
            $('#extended_switcher').html('Расширенный поиск');
        } else {
            $('#extended_switcher').html('<b>Расширенный поиск: ' + changes_count + '</b>');
        }
    }
    */
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

    var mainSearcher = function (pg, group_name) {
        var rcount = $("#countVal").attr("val");
        var top1000 = ((group_name.length > 0 && pg == -1000) ? "1" : (pg == -2000) ? "2" : "0");

        var params = {
            "company": $("#comp").val(),//имя компании           
            "ruler": $("#ruler").val(),//руководитель
            "status": $("#stat_val").val(),//статус
            "stat_excl": (String($("#stat_excl").val()).length == 0) ? 0 : $("#stat_excl").val(),//исключить выбранные статусы
            "regions": $("#reg_val").val(),//регион
            "reg_excl": (String($("#reg_excl").val()).length == 0) ? 0 : $("#reg_excl").val(),//исключить выбранные коатуу
            "industry": $("#ind_val").val(),//окэд 
            "ind_excl": (String($("#ind_excl").val()).length == 0) ? 0 : $("#ind_excl").val(),//исключить выбранные кведы
            "ind_main": ($('input[name="ind_type"]:radio:checked').attr("prop") == "IsMain") ? "1" : "0",//только основной квед
            "econ": $("#econ_val").val(),//сектор экономики
            "econ_excl": (String($("#econ_excl").val()).length == 0) ? 0 : $("#econ_excl").val(),//исключить выбранный сектор экономики
            "own": $("#own_val").val(),//вид собственности
            "own_excl": (String($("#own_excl").val()).length == 0) ? 0 : $("#own_excl").val(), //исключить выбранный вид собственности
            "siz": $("#siz_val").val(),//размерность предприятия
            "siz_excl": (String($("#siz_excl").val()).length == 0) ? 0 : $("#siz_excl").val(), //исключить выбранную размерность предприятия
            "pcount": $("#pcount_val").val(),//численность предприятия
            "pcount_excl": (String($("#pcount_excl").val()).length == 0) ? 0 : $("#pcount_excl").val(), //исключить выбранную численность предприятия
            "page_no": pg,
            "user_id": user_id,
            "rcount": $("#countVal").attr("val"),
            "top1000": top1000
        };

        var key_name = ClearText(String($("#comp").val()));
        var key_ruler = ClearText(String($("#ruler").val()));
        _toggle_extented_search(true);

        if (pg == -2000) {
            hideClock();
            searching = 0;
            var urlparams = 'company='+params.company+'&ruler='+params.ruler+'&status='+params.status+'&stat_excl='+params.stat_excl+
                '&regions=' + params.regions + '&reg_excl=' + params.reg_excl + '&industry=' + params.industry + '&ind_excl=' + params.ind_excl +
                '&ind_main=' + params.ind_main + '&econ=' + params.econ + '&econ_excl=' + params.econ_excl + '&own=' + params.own + '&own_excl=' + params.own_excl +
            '&siz=' + params.siz + '&siz_excl=' + params.siz_excl + '&pcount=' + params.pcount + '&pcount_excl=' + params.pcount_excl + '&page_no=' + params.page_no +
            '&user_id=' + params.user_id + '&rcount=' + params.rcount + '&top1000=' + params.top1000;
            window.open('/DBSearchKZ/CompaniesGetExcelTop1000?' + urlparams, '_blank');       
        }
        else {
            //Новый поиск
            $.post("/DBSearchKZ/CompaniesDoSearch", params, function (data) {
                var re = new RegExp("icon_error", "ig")
                if (pg > 0) { 
                    //getObj("search_result").innerHTML=data;
                    GenerateResult(pg, rcount, data, roles_object, key_name, key_ruler, user_id);                  
                    hideClock();
                    if (String(re.exec(data)) == "null") {
                        if ($("#search_table").css("display") == "block" || $("#search_table").css("display") == "table") {
                            $('#hide_form').show();
                            toggle_form();
                        }
                    }
                }
                if (pg == -1000) {
                    // Добавление 10000 в группу
                    var ret = SaveResultGroup(data, group_name);
                    var amsg = String(ret).split("_");
                    showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
                }
                hideClock();
                $("#btn_search").val("Найти");
                searching = 0;
                location.href = "#top";
            },
    "json");
        }

    };

    var GenerateResult = function (page, rcount, data, ro, key_name, key_ruler, user_id)
    {
        $("#search_result").html('').show();
        var total = data.length > 0 ? data[0].cnt : 0;
        var page_count = total / rcount;
        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }

        if (total && total > 0) {

            var total_text;
            total_text = "Всего найдено: " + total;       
         
            $("#search_result").append("<span class=\"total_count\">" + total_text + "</span>");
            var res_arr = data;
            var $res_block = $('<div>').addClass("res_block");
            var $export_block = $('<div>').addClass("export_block");
            var $checkall_block = $('<div>').addClass("checkall_block");
            $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");
            $export_block.append($checkall_block);           
            var $export_command_block = $('<div>').addClass("export_command_block");
            if (ro.canExport) {
                var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"icon-file-excel icon\"></span></div>");
                $export_command_block.append($add_excel_btn);
                $add_excel_btn.click(function (e) {
                    generate_sub_excelmenu(e);
                });
            }
            $export_block.append($export_command_block);
            $res_block.append($export_block); 

            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var $res_item = $('<div>').addClass("res_item");               

                //if (user_id > 0 && (res_arr[i].access == 1)) {
                if (res_arr[i].access == -1) {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + res_arr[i].code + "\"></input>");
                    $res_item.append($check_block);
                }

                var $info_block = $('<div>').addClass("info_block");
                var comp_name = res_arr[i].name;
                if (res_arr[i].code.length > 0) {
                    $info_block.append('<a class="comp_title" href="/profilekz/' + res_arr[i].code + '" target="_blank">' + comp_name + '</a>');
                } else {
                    $info_block.append('<span class="comp_title">' + comp_name + '</a>');
                }
                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";
                if (res_arr[i].FullAddress != '') {
                    comp_info_text = '<p>' + res_arr[i].FullAddress + '</p>';
                }
                if (res_arr[i].Manager != '') {
                    comp_info_text = '<p>Руководитель: ' + res_arr[i].Manager + '</p>';
                }
                if (res_arr[i].MainDeal != '') {
                    comp_info_text += '<p>' + res_arr[i].MainDeal + '</p>';
                }
                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                var $code_block = $('<div>').addClass("code_block");
                comp_info_text = "";
                if (res_arr[i].region != '') {
                    comp_info_text += '<p>Регион: '+res_arr[i].region+'</p>';
                }
                if (res_arr[i].code != '') {
                    comp_info_text += '<p>ОКРО: ' + res_arr[i].code + '</p>';
                }               
                $code_block.html(comp_info_text);
                      
                $res_item.append($info_block);
                $res_item.append($code_block);
                $res_block.append($res_item);
            }

            $("#search_result").append($res_block);
            $("#search_result").append(_get_paging(page, page_count));     
        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
        }

    }

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
            var url = '/DBSearchKZ/CompaniesGetExcel?issuers=' + issuers;
            window.open(url, '_blank');
            //$.post("/DBSearchKZ/CompaniesGetExcel", { "issuers": issuers }, function (data) {
            //    if (data.filename) {
            //        DownLoadXLS1000(data.filename);
            //    } else if (data.error) {
            //        showwin('critical', '<p align=center>' + data.error + '</p>', 3000);
            //    }
            //}, "json");

        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
        }

    };


    var make_input = function (name, value) {
        var element = null;
        element = document.createElement("input");
        element.type = "text";
        element.name = name;
        element.value = value;
        return element;
    };


    //var DownLoadXLS1000 = function (filename) {
    //    var form = document.createElement("form");
    //    form.action = "/DBSearchKZ/GetFile";
    //    form.method = "POST";
    //    if (!document.addEventListener) {
    //        showClock();
    //        form.target = "xls_frame"
    //    } else {
    //        form.target = "blank"
    //    }
    //    form.style.display = "none"
    //    form.appendChild(make_input("src", filename));
    //    form.appendChild(make_input("page", "DBSearchKZ/companies"));
    //    document.body.appendChild(form);
    //    form.submit();
    //    document.body.removeChild(form);
    //};

    var generate_sub_excelmenu = function (e) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным компаниям").click(function () { doSave2XLS(); }).appendTo($ul);
        $("<li>").text("Экспорт по первым 10 000 компаний").click(function () { doExport10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
    }


    var doExport10000 = function () {
        showClock();
        mainSearcher(-2000, '');
    }

    var ClearText = function (txt) {
        txt = txt.toLowerCase().replace(/-/g, ';').replace(/\s+/g, ';').replace(/\"/g, "").replace(/\'/g, "");
        return txt;
    }

    var checkCanSearch = function () {
        var ss = $('#comp').val() + $("#ruler").val() + $('#econ_val').val() + $('#stat_val').val() + $('#own_val').val() +
            $('#pcount_val').val() + $('#reg_val').val() + $('#siz_val').val() + $('#ind_val').val();                      
        //return (ss.length == 0) ? false : true;
        return true;
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
                $.post("/Modules/GetBonesKZ", { "input": $("#comp").val() },
                    function (data) {
                        var htm = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" id=\"dpw\" onkeydown=\"skip(event)\">"
                        st_max = 0
                        if (!data[0].name && getObj("dp_window")) {
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


    var _toggle_extented_search = function (only_need_close) {

        var $switch = $('#switcher_ico');

        if (only_need_close || $switch.hasClass("icon-angle-up")) {
            $switch.removeClass("icon-angle-up").addClass("icon-angle-down");
            $("#extended_block").hide();
        } else {
            $switch.removeClass("icon-angle-down").addClass("icon-angle-up");
            $("#extended_block").show();
        }

    };

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

    var doStop = function () {
        hideClock();
        searching = 0;
        $("#btn_search").val("Найти");
        window.stop();
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

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#comp').css("border-radius", "4px");
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



})();


$().ready(function () {
    search_init();
});

