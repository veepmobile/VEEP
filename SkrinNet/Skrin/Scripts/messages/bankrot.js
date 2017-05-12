(function () {
    var SO = {};
    SO.type_id = '';
    var tree_active;
    var tree_table;
    var src;
    var titles = { "types": "Тип сообщения" };

    var init_bc_calendar = function () {

        if (user_id != 0) {
            //if ($('#dfrom').val() == "") { $('#dfrom').val(now.format("dd.mm.yyyy")); }
            //if ($('#dto').val() == "") { $('#dto').val(now.format("dd.mm.yyyy")); }
            var dates = $("#dfrom, #dto").datepicker({
                changeMonth: true,
                showButtonPanel: true,
                changeYear: true,
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 6);
                    }, 0);
                },
                onSelect: function (selectedDate) {
                    var option = this.id == "dfrom" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat ||
                    $.datepicker._defaults.dateFormat,
                    selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        }
    }

    window.search_init = function () {
        init_bc_calendar();
        bc_save_search_params();
        bc_search(0);

        $('#btn_find').click(function () {
            if (!roles_object.canSearch) {
                no_rights();
                return;
            }

            bc_save_search_params();
            bc_search(1);
        });

        $('#btn_clear').click(function () {
            $("#search_name").val('');
            $("#dfrom").val('');
            $("#dto").val('');
            $("#sourceVal").text('Все');
            $("#sourceVal").attr('val','0');
            $("#types").val('');
            $("#types_val").val('');
            $("#types_excl").val('0');
            $("#types_block").hide();
        });

        $('html').on('click', function () {
            $('input').css({ 'background-color': '#FFFFFF' });
        });

    }

    window.show_ShowSelector = function () {
        var d;
        if (!getObj("sourceSelector")) {
            d = document.createElement("div");
            d.id = "sourceSelector";
            d.className = "dp_window";

            var boundes = $("#sourceSelect").parent().position();
            d.style.top = boundes.top + 94 + $("#sourceSelect").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#sourceSelect").width() + 12) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("sourceSelector"));
            $("#sourceSelect").removeClass("customSelect_focus");
        }
        if (getObj("sourceSelector")) {
            $("#sourceSelect").addClass("customSelect_focus");
            var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">";
            htm += "<tr class='selec_option'  onClick='selectNewSource(0,\"Все\");'><td id='tdgroup0' id='tdgroup0' class='bones_pointer' style='padding-left:15px;'>Все</td></tr>";
            htm += "<tr class='selec_option'  onClick='selectNewSource(1,\"ЕФРСБ\");'><td id='tdgroup1' id='tdgroup1' class='bones_pointer' style='padding-left:15px;'>ЕФРСБ</td></tr>";
            htm += "<tr class='selec_option'  onClick='selectNewSource(34,\"Коммерсантъ\");'><td id='tdgroup34' id='tdgroup34' class='bones_pointer' style='padding-left:15px;'>Коммерсантъ</td></tr>";
            htm += "<tr class='selec_option'  onClick='selectNewSource(38,\"Российская газета\");'><td id='tdgroup38' id='tdgroup38' class='bones_pointer' style='padding-left:15px;'>Российская газета</td></tr>";
            htm += "</table>";
            d.innerHTML = htm;
            d.style.display = "block";
            $("#cnttbl").one("mouseenter", function () {
                $('#tdgroup' + $('#sourceVal').attr('val')).attr('className', 'bones_pointer');
            });
        }
    }


    window.selectNewSource = function (id, name) {
        $("#sourceVal").text(name);
        $("#sourceVal").attr("val", id);
        document.body.removeChild(getObj("sourceSelector"));
        $("#sourceSelect").removeClass("customSelect_focus");
        if(id==1) {
            //$("#types").removeClass("search_input_disabled").addClass("search_input");
            //$("#types").attr("onclick", "show_tree_selector(event,19,1);");
            $("#types_block").show();
        }
        else {
            $("#types_block").hide();
            $("#types").val("");
            //$("#types").attr({ "title": "Тип сообщения" }).attr({ "style": "" }).removeClass("search_input").addClass("search_input_disabled");
            $("#types_excl").val(0);
            $("#types_val").val("");
            //$("#types").attr("onclick", "");
        }
    }

    window.bc_save_search_params = function () {
        SO.DBeg = _get_dt2('dfrom', '');
        SO.DEnd = _get_dt2('dto', '');

        var search_name = $('#search_name').val();
        SO.search_name = (search_name !== null || search_name !== 'undefined') ? search_name : '';
        var src = $('#sourceVal').attr('val');
        SO.src = src !== null ? src : '';
        var types = $('#types_val').val();
        SO.types = types !== null ? types : '';
        var types_excl = $('#types_excl').val();
        SO.types_excl = types_excl !== null ? types_excl : 0;

        var grp_val = $('#gropVal').attr('val');
        SO.grp = grp_val !== null ? grp_val * 1 : 0;
    }

    window.bc_search = function (page) {
        showClock();
        SO.page = page;
        SO.rcount = 20;
        var url = "/Bankrot/BankrotSearch";
        //var params = "DBeg='" + SO.DBeg + "'&dto='" + SO.DEnd + "'&types=" + SO.types + "&types_excl=" + SO.types_excl + "&search_name=" + SO.search_name + "&src=" + SO.src + "&page=" + SO.page + "&rcount=" + SO.rcount;
        $.ajax({
            url: url,
            type: "POST",
            data: SO,
            success: function (data) {
                hideClock();
                if (data) {
                    GenerateResult(data, SO.page, SO.rcount, user_id, roles_object);
                }
            }
        });
    }

    var GenerateResult = function (data, page, rcount, user_id, ro) {
        $("#search_count").html('');
        $("#event_search_result").html('');
        var res = $.parseJSON(data);
        var total = (res) ? res.total : 0;
        var total_found = (res) ? res.total_found : 0;
        if (total && total > 0) {
            var page_count = total / rcount;
            if (page_count % 1 > 0) {
                page_count = (page_count - (page_count % 1)) + 1;
            }
            var total_text;
            if (total == 0) {
                $("#search_count").html("<span class=\"total_count\">Нет данных соответствующих заданному условию</span>");
            }
            else {
                if ($("#dfrom").val().length > 0 || page > 0) {
                    $("#search_count").html("<span class=\"total_count\">Всего найдено " + normalW(total_found) + ".</span>");
                }
                else {
                    showCount();
                }
            }

            var res_arr = res.results;
            var $res_block = $('<div>').addClass("res_block");

            if (ro.canShowAll) {
                var $export_block = $('<div>').addClass("export_block");
                var $checkall_block = $('<div>').addClass("checkall_block");
                $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");
                $export_block.append($checkall_block);

                var $export_command_block = $('<div>').addClass("export_command_block");
                var $add_excel_btn = $("<div id=\"btnPrint\"><span class=\"icon-print icon\" onclick=\"set_print_function();\"></span></div>");
                $export_command_block.append($add_excel_btn);
                $export_block.append($export_command_block);
                $res_block.append($export_block);
            }
            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var $res_item = $('<div>').addClass("res_item");

                if (ro.canShowAll) {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input type=\"checkbox\" id=\"evt_" + getval(res_arr[i].num) + "\" name=\"evt\" value=\"" + getval(res_arr[i].num) + "_" + getval(res_arr[i].source) + "\"></input>");
                    $res_item.append($check_block);
                }

                var $info_block = $('<div>').addClass("info_block");

                var content = [];
                content.push("<span class=\"news_data news_data_sm\">" + res_arr[i].reg_date + "</span><br/>");

                //content.push('<span class=\"issuer\"><b>' + res_arr[i].name + '</b></span>');
                var re = new RegExp("[\"\']", "ig");
                if (res_arr[i].ticker != "") {
                    if (res_arr[i].iscompany==0) {
                        content.push("<a href=\"/profileip/" + res_arr[i].ticker + "\" target=\"_blank\"><b>" + res_arr[i].name + "</b></a>");
                    }
                    else {
                        content.push("<a href=\"/issuers/" + res_arr[i].ticker + "\" target=\"_blank\"><b>" + res_arr[i].name + "</b></a>");
                    }
                }
                else {
                    if (res_arr[i].iscompany==0) {
                        content.push("<a href=\"/dbsearch/dbsearchru/ichp?ruler=" + String(res_arr[i].name).replace(re, "").replace(/^\s+|\s+$/g, '').replace("+", '-D-').replace(/\-/g, '%20') + "\" target=\"_blank\"><b>" + res_arr[i].name + "</b></a>");
                    }
                    else {
                        content.push("<a href=\"/dbsearch/dbsearchru?name=" + String(res_arr[i].name).replace(re, "").replace(/^\s+|\s+$/g, '').replace("+", '-D-').replace(/\-/g, '%20') + "\" target=\"_blank\"><b>" + res_arr[i].name + "</b></a>")
                    }
                }

                if (res_arr[i].source == "1") {
                    headline = getval(res_arr[i].type_name);
                }
                else {
                    var len = getval(res_arr[i].contents).length;
                    if (len > 150) {
                        headline = getval(res_arr[i].headers) + " ... ";
                    }
                    else {
                        headline = getval(res_arr[i].contents) + " ... ";
                    }
                }

                if (user_id == 0) {
                    content.push(": <a class=\"event_header\" href=\"#\" onclick=\"need_login();return false;\">" + headline + "</a>");
                }
                else {
                    if (!roles_object.canSearch)
                    {
                        content.push(": <a class=\"event_header\" href=\"#\" onclick=\"no_rights();return false;\">" + headline + "</a>");
                    }
                    else
                    {
                        content.push(": <a class=\"event_header\" href=\"#\" onclick=\"show_event_text('" + getval(res_arr[i].num) + "','" + getval(res_arr[i].tab) + "');\">" + headline + "</a>");
                    }
                }
                content.push("<br/><span class=\"event_comment\">Источник " + res_arr[i].source_name + "</span>")
                $info_block.html(content.join(""));

                /*
                var mess_text = "<span class=\"event_title_16\">" + res_arr[i].reg_date + "&nbsp;&nbsp;&nbsp;&nbsp;<b>" + res_arr[i].source_name + "</b></span><br/>";
                if (res_arr[i].source == "1") {
                    headline = getval(res_arr[i].type_name);
                }
                else {
                    var len = getval(res_arr[i].contents).length;
                    if (len > 150) {
                        headline = getval(res_arr[i].headers) + "...";
                    }
                    else {
                        headline = getval(res_arr[i].contents) + "...";
                    }
                }

                if (user_id == 0) {
                    mess_text += "<a class=\"comp_title\" href=\"#\" onclick=\"need_login();return false;\">" + headline + "</a></span>";
                }
                else {
                    mess_text += "<a class=\"comp_title\" href=\"#\" onclick=\"javascript:show_event_text('" + getval(res_arr[i].num) + "','" + getval(res_arr[i].tab) + "');\">" + headline + "</a>";
                }
                $info_block.append(mess_text);
                var $code_block = $('<div>').addClass("code_block");
                $code_block.append('<span class="comp_title_16">' + res_arr[i].name + '</span>');
                */

                /*if (res_arr[i].ticker.length > 0) {
                    $code_block.append('<a class="comp_title_16" href="/issuers/' + res_arr[i].ticker + '" target="_blank">' + res_arr[i].name + '</a>');
                } else {
                    $code_block.append('<span class="comp_title_16">' + res_arr[i].name + '</span>');
                }*/
                $res_item.append($info_block);
                //$res_item.append($code_block);
                $res_block.append($res_item);
            }
            $("#event_search_result").append($res_block);
            if (page > 0) {
                $("#event_search_result").append(_get_paging(page, page_count));
            }
        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных, соответствующих заданному условию");
            $("#event_search_result").append($el);
        }
    }

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
            html += '<td onclick="bc_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="bc_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="bc_search(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<br/><div id="page_counter"><table>' + html + '</table></div>';
    };

    window.showCount = function () {
        $.post("/Bankrot/Counter", function (data) {
            if (data) {
                $("#search_count").append("<span class=\"total_count\">Всего найдено " + normalW(data) + ".</span>");
            }
        }, "html");
    }

    window.normalW = function (cnt) {
        var d = (cnt) * 1;
        var a = ["ий", "ие", "ия", "ия", "ия"];
        if (d > 99) d = d % 100;
        if (d > 19) d = d % 10;
        return cnt + " сообщен" + ((d < a.length) ? a[d] : "ий");
    }

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }

    window._get_dt2 = function (dt_nm, dflt_dt) {
        var dt;
        var ret_dt = '';
        var spliter = dt_nm.indexOf('-') > 0 ? '-' : '.';
        if (dt_nm != '' && $('#' + dt_nm).get().length > 0) {
            dt = $('#' + dt_nm).val();
            if (dt != '') {
                dt = dt.split(spliter);
                ret_dt = dt[0] + "." + dt[1] + "." + dt[2];
            }
        }
        if (ret_dt == '') {
            dt = (typeof (dflt_dt) == 'undefined' ? new Date() : dflt_dt);
            if (dt != '') {
                var dty = dt.getFullYear();
                var dtm = (dt.getMonth() + 1);
                if (dtm * 1 < 10) dtm = '0' + dtm;
                var dtd = dt.getDate();
                dt = dtd + "." + dtm + "." + dty;
            }
            ret_dt = dt;
        }
        return ret_dt;
    }

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

    window.show_tree_selector = function (e, sr, is_tree, mult) {

        src = (sr == 99) ? 1 : sr;
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

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#comp').css("border-radius", "4px");
        }
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

    };

    window.show_event_text = function (id, src) {
        showClock();

        var url = "/Bankrot/GetBankrotMsg";
        var params = "id=" + id + "&src=" + src;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    if (src == "1" || src == "-1") {
                        show_dialog({ "content": data, "extra_style": "width:990px;", is_print: true });
                    }
                    else {
                        show_dialog({ "content": printMessage(data), "extra_style": "width:990px;", is_print: true });
                    }
                }
            }
        });
    };

    window.printMessage = function (data) {
        var res = "<div  style=\"color:#3a61ad;font-weight:bold;\">СООБЩЕНИЕ О БАНКРОТСТВЕ</div><br/><table width=\"100%\"><tbody><tr><td align=\"left\">Источник данных: " + data.source + "</td><td align=\"right\">Дата публикации сообщения: " + data.reg_date + "</td></tr></tbody></table><span class=\"bluecaption\"></span><br>" + data.contents + "<hr><div class=\"data_comment limitation\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.</div><hr><br/>";

        return res;
    }

    window.set_print_function = function () {
        var print_all = true;
        $("input[name='evt']").each(function () {
            if ($(this).prop('checked')) {
                print_all = false;
            }
        });
        if (print_all) {
            $("input[name='evt']").prop('checked', 'checked');
            $("input[name='selallbox']").prop('checked', 'checked');
        }
        showSelectedMsg();
    };

    window.showSelectedMsg = function () {
        var ids = '';
        $('input[name="evt"]:checkbox:checked').each(function () {
            ids += this.value + ",";
        });
        if (!ids || ids == '') {
            return;
        }
        var result = "";
        showClock();
        var url = "/Bankrot/GetBankrotSelected";
        var params = "ids=" + ids;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    show_dialog({ "content": data, "extra_style": "width:990px;", is_print: true });;
                }
            }
        });
    };

    Date.prototype.format = function (mask, utc) {
        return dateFormat(this, mask, utc);
    };

})();

$().ready(function () {
    search_init();
    /*Если есть ID и A*/
    var loc = window.location.search;
    var aLoc = loc.split("&");
   
    if (aLoc.length > 1) {
        var id = aLoc[0].split("=")[1];
        var ag = aLoc[1].split("=")[1];
        show_event_text(id, ag);

    }
});
