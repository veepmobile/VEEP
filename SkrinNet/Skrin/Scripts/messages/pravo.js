(function () {
    var SO = {};
    SO.ac_type = '';
    SO.disput_type = '';
    SO.side_type = '';
    var tree_active;
    var tree_table;
    var src;
    var titles = { "ac_type": "Арбитражный суд", "disput_type": "Категория спора", "side_type": "Форма участия" };
    var ShowCaseFlag = false;



    var init_ev_calendar = function () {

        if (user_id != 0) {
            //if ($('#dfrom').val() == "") { $('#dfrom').val(now.format("dd.mm.yyyy")); }
            //if ($('#dto').val() == "") { $('#dto').val(now.format("dd.mm.yyyy")); }
            var dates = $('input:text[name="insDBeg"], input:text[name="insDEnd"]').datepicker({
                changeMonth: true,
                showButtonPanel: true,
                changeYear: true,
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 6);
                    }, 0);
                },
                onSelect: function (selectedDate) {
                    var option = this.name == "insDBeg" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat ||
                    $.datepicker._defaults.dateFormat,
                    selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
            var dates2 = $('input:text[name="lastDBeg"], input:text[name="lastDEnd"]').datepicker({
                changeMonth: true,
                showButtonPanel: true,
                changeYear: true,
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 6);
                    }, 0);
                },
                onSelect: function (selectedDate) {
                    var option = this.name == "lastDBeg" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker2");
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
        init_ev_calendar();
        ev_save_search_params();
        if (isMsg == "1") {
            if(roles_object.canSearch) ev_search(1);
        }
        else ev_search(0);

        $('#btn_find').click(function () {

            if (!roles_object.canSearch) {
                no_rights();
                return;
            }

            ev_save_search_params();
            ev_search(1);
        });

        $('#btn_clear').click(function () {
            $("#insDate_DBeg").val('');
            $("#insDate_DEnd").val('');
            $("#lastDate_DBeg").val('');
            $("#lastDate_DEnd").val('');
            $("#job_no").val('');
            $("#ac_type").val('');
            $("#ac_type_val").val('');
            $("#ac_type_excl").val('0');
            $("#disput_type").val('');
            $("#disput_type_val").val('');
            $("#disput_type_excl").val('0');
            $("#search_text").val('');
            $("#side_type").val('');
            $("#side_type_val").val('');
            $("#side_type_excl").val('0');

        });

        $('html').on("click", function (e) {
            $("input").css({ 'background-color': '#FFFFFF' });
        });

        if (isMsg == "1") {
            if (user_id == 0)
                need_login();
            else
                if (roles_object.canMsg) ShowCase(Msg_id, Msg_reg_no, "", Msg_src);
                else no_rights();
        }
    }

    window.ev_save_search_params = function () {

        SO.ins_DBeg = _get_dt2('insDate_DBeg', '');
        SO.ins_DEnd = _get_dt2('insDate_DEnd', '');
        SO.last_DBeg = _get_dt2('lastDate_DBeg', '');
        SO.last_DEnd = _get_dt2('lastDate_DEnd', '');
        var job_no = $('#job_no').val();
        SO.job_no = (job_no !== null && job_no !== 'undefined') ? job_no : '';
        var ac_type = $('#ac_type_val').val();
        SO.ac_type = (ac_type !== null && (ac_type !== '999' || $('#ac_type_excl').val() !== '0')) ? ac_type : '';
        var ac_type_excl = $('#ac_type_excl').val();
        SO.ac_type_excl = (ac_type_excl !== null && ac_type !== '999') ? ac_type_excl : 0;
        var disput_type = $('#disput_type_val').val();
        SO.disput_type = (disput_type !== null && (disput_type !== '999' || $('#disput_type_excl').val() !== '0')) ? disput_type : '';
        var disput_type_excl = $('#disput_type_excl').val();
        SO.disput_type_excl = (disput_type_excl !== null && disput_type !== '999') ? disput_type_excl : 0;
        var side_type = $('#side_type_val').val();
        SO.side_type = (side_type !== null && (side_type !== '999' || $('#side_type_excl').val() !== '0')) ? side_type : '';
        var side_type_excl = $('#side_type_excl').val();
        SO.side_type_excl = (side_type_excl !== null && side_type !== '999') ? side_type_excl : 0;
        var search_txt = $('#search_text').val();
        SO.search_txt = (search_txt !== null && search_txt !== 'undefined') ? search_txt : '';
        var grp_val = $('#gropVal').attr('val');
        SO.grp = grp_val !== null ? grp_val * 1 : 0;
    }

    window.ev_search = function (page) {
        showClock();
        SO.page = page;
        SO.rcount = 20;
        var url = "/PravoSearch/PravoSearch_elastic";
        //var params = "ins_DBeg='" + SO.insDate_DBeg + "'&ins_DEnd='" + SO.insDate_DEnd + "last_DBeg='" + SO.lastDate_DBeg + "'&last_DEnd='" + SO.lastDate_DEnd + "&job_no=" + SO.job_no + "'&ac_type=" + SO.ac_type + "&ac_type_excl=" + SO.ac_type_excl + "&disput_type=" + SO.disput_type + "&disput_type_excl=" + SO.disput_type_excl + "&side_type=" + SO.side_type + "&side_type_excl=" + SO.side_type_excl + "&search_txt=" + SO.search_txt + "&page=" + SO.page + "&rcount=" + SO.rcount;
        $.ajax({
            url: url,
            type: "POST",
            data: SO,
            success: function (data) {
                if (!ShowCaseFlag) hideClock();
                if (data) {
                    GenerateResult(data, SO.page, SO.rcount, SO.search_text, user_id, roles_object);
                }
            }
        });
    }


    var GenerateResult = function (data, page, rcount, kw, user_id, ro) {
        $("#search_count").html('');
        $("#search_result").html('');
        var res = $.parseJSON(data);
        var total = (res) ? res.hits.hits.length : 0;
        var total_found = (res) ? res.hits.total : 0;
        if (total && total > 0) {
            var page_count = total_found / rcount;
            if (total_found > 10000) page_count = 10000 / rcount;
            if (page_count % 1 > 0) {
                page_count = (page_count - (page_count % 1)) + 1;
            }
            if (total == 0) {
                $("#search_count").html("<span class=\"total_count\">Нет данных соответствующих заданному условию</span>");
            }
            else {
                if ($("#insDate_DBeg").val().length > 0 || page > 0) {
                    $("#search_count").html("<span class=\"total_count\">Всего найдено " + normalW(total_found) + ".</span>");
                }
                else {
                    showCount();
                }
            }

            var res_arr = res.hits.hits;
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

            $res_block.append("<div id=\"legend\" style=\"margin-top:5px; margin-bottom:5px;\"><span class=\"cat_blue\">А</span>&nbsp;&nbsp;Административные&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class=\"cat_green\">Г</span>&nbsp;&nbsp;Гражданские&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class=\"cat_red\">Б</span>&nbsp;&nbsp;Банкротные</div>");
            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var it = res_arr[i]._source;
                var $res_item = $('<div>').addClass("res_item");

                if (ro.canShowAll) {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input type=\"checkbox\" id=\"evt_" + getval(it.case_id) + "\" name=\"evt\" value=\"" + getval(it.case_id) + "_" + getval(it.case_type) + "\"></input>");
                    $res_item.append($check_block);
                }
                var $info_block = $('<div>').addClass("info_block_sm");
                var content = [];
                content.push("<span class=\"news_data news_data_sm\">" + it.reg_date + "</span>");
                if (user_id == 0) {
                    content.push("<nobr>Номер дела: <a class=\"event_header\" href=\"#\" onclick=\"need_login();return false;\">" + getval(it.reg_no) + "</a>&nbsp;&nbsp;" + get_categ(it.disput_type_cat_id));
                }
                else {
                    if (!roles_object.canSearch) {
                        content.push("<nobr>Номер дела: <a class=\"event_header\" href=\"#\" onclick=\"no_rights();return false;\">" + getval(it.reg_no) + "</a>&nbsp;&nbsp;" + get_categ(it.disput_type_cat_id));
                    }
                    else {
                        content.push("<nobr>Номер дела: <a style=\"cursor:pointer;\" onclick=\"ShowCase('" + it.case_id + "','" + getval(it.reg_no) + "','','" + getval(it.case_type) + "');\">" + getval(it.reg_no) + "</a>&nbsp;&nbsp;" + get_categ(it.disput_type_cat_id) + "</nobr/>");
                    }
                }
                content.push("<br/><div class=\"comp_info\">Арбитражный суд: " + getval(it.court_name) + "</div>");
                content.push("<span class=\"event_comment\">Дата обновления: " + it.upd_date + "</span>");
                $info_block.html(content.join(""));
                $res_item.append($info_block);

                var ist_list = [];
                var otv_list = [];
                for (var j = 0, j_max = (it.side.length - 1) ; j <= j_max; j++) {
                    if (it.side[j].side_type_id == 0) ist_list[ist_list.length] = it.side[j];
                    if (it.side[j].side_type_id == 1) otv_list[otv_list.length] = it.side[j];
                }

                var $ist_block = $('<div>').addClass("code_block_sm");
                var ist_info_text = "";
                if (ist_list.length > 0) {
                    ist_info_text += "Истец:<br/>";
                    if (ist_list.length == 1) {
                        ist_info_text += show_side_issuer(ist_list[0]);
                    }
                    if (ist_list.length > 1) {
                        ist_info_text += "<table class=\"no_border\" width=\"100%\"><tr><td style=\"white-space:nowrap;\"><span  style=\"font-weight:bold; \">Всего: " + ist_list.length
                                    + "&nbsp;</span></td><td align=\"left\" style=\"width:100%;white-space:nowrap;\"><span style=\"cursor:pointer;color:#003399;\" val=\"0\" onclick=\"switch_membs(" + (i + 1) + ")\" id=\"s" + (i + 1) + "\">Подробнее</span><img src=\"/images/tra_e.png\" alt=\"\" style=\"padding-left:3px\" id=\"img" + (i + 1) + "\"></td></tr></table>"
                                    + "<div style=\"display:none;\" id=\"o" + (i + 1) + "\"><table class=\"no_border\">";
                        for (var j = 0, j_max = ist_list.length; j < j_max; j++) {
                            ist_info_text += "<tr><td>" + show_side_issuer(ist_list[j]) + "</td></tr>";
                        }
                        ist_info_text += "</table>";
                    }
                }
                $ist_block.html(ist_info_text);
                $res_item.append($ist_block);

                var $otv_block = $('<div>').addClass("code_block_sm");
                var otv_info_text = "";
                if (otv_list.length > 0) {
                    otv_info_text += "Ответчик:<br/>";
                    if (otv_list.length == 1) {
                        otv_info_text += show_side_issuer(otv_list[0]);
                    }
                    if (otv_list.length > 1) {
                        otv_info_text += "<table class=\"no_border\" width=\"100%\"><tr><td style=\"white-space:nowrap;\"><span  style=\"font-weight:bold; \">Всего: " + otv_list.length
                                    + "&nbsp;</span></td><td align=\"left\" style=\"width:100%;white-space:nowrap;\"><span style=\"cursor:pointer;color:#003399;\" val=\"0\" onclick=\"switch_membs2(" + (i + 1) + ")\" id=\"ss" + (i + 1) + "\">Подробнее</span><img src=\"/images/tra_e.png\" alt=\"\" style=\"padding-left:3px\" id=\"img" + (i + 1) + "\"></td></tr></table>"
                                    + "<div style=\"display:none;\" id=\"oo" + (i + 1) + "\"><table class=\"no_border\">";
                        for (var j = 0, j_max = otv_list.length; j < j_max; j++) {
                            otv_info_text += "<tr><td>" + show_side_issuer(otv_list[j]) + "</td></tr>";
                        }
                        otv_info_text += "</table>";
                    }
                }
                $otv_block.html(otv_info_text);
                $res_item.append($otv_block);

                $res_block.append($res_item);
            }
            $("#search_result").append($res_block);
            if (page > 0) {
                $("#search_result").append(_get_paging(page, page_count));
            }
        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных, соответствующих заданному условию");
            $("#search_result").append($el);
        }
    }

    var get_categ = function (cat) {
        var result = "";
        switch (cat) {
            case 1:
                result = "<span class=\"cat_red\">Б</span>";   //Банкротные
                break;
            case 2:
                result = "<span class=\"cat_blue\">А</span>";   //Административные
                break;
            case 3:
                result = "<span class=\"cat_green\">Г</span>";   //Гражданские
                break;
        }
        return result;
    }

    function check_side(ist_list, otv_list, keywords1, keywords2, third_list, over_list) {
        var result = "";
        ist_list = getval(ist_list);
        otv_list = getval(otv_list);
        var flag1 = true;
        var flag2 = true;
        var i, i_max;
        for (i = 0, i_max = keywords1.length; i < i_max; i++) {
            if (ist_list.toLowerCase().indexOf(keywords1[i].toLowerCase()) < 0) {
                flag1 = false;
            }
        }
        for (i = 0, i_max = keywords2.length; i < i_max; i++) {
            if (ist_list.toLowerCase().indexOf(keywords2[i].toLowerCase()) < 0) {
                flag2 = false;
            }
        }
        if (flag1 || flag2) {
            result += "Истец<br/>";
        }
        flag1 = true;
        flag2 = true;
        for (i = 0, i_max = keywords1.length; i < i_max; i++) {
            if (otv_list.toLowerCase().indexOf(keywords1[i].toLowerCase()) < 0) {
                flag1 = false;
            }
        }
        for (i = 0, i_max = keywords2.length; i < i_max; i++) {
            if (otv_list.toLowerCase().indexOf(keywords2[i].toLowerCase()) < 0) {
                flag2 = false;
            }
        }
        if (flag1 || flag2) {
            result += "Ответчик<br/>";
        }
        flag1 = true;
        flag2 = true;
        for (i = 0, i_max = keywords1.length; i < i_max; i++) {
            if (third_list.toLowerCase().indexOf(keywords1[i].toLowerCase()) < 0) {
                flag1 = false;
            }
        }
        for (i = 0, i_max = keywords2.length; i < i_max; i++) {
            if (third_list.toLowerCase().indexOf(keywords2[i].toLowerCase()) < 0) {
                flag2 = false;
            }
        }
        if (flag1 || flag2) {
            result += "<nobr>Третье лицо</nobr><br/>";
        }
        flag1 = true;
        flag2 = true;
        for (i = 0, i_max = keywords1.length; i < i_max; i++) {
            if (over_list.toLowerCase().indexOf(keywords1[i].toLowerCase()) < 0) {
                flag1 = false;
            }
        }
        for (i = 0, i_max = keywords2.length; i < i_max; i++) {
            if (over_list.toLowerCase().indexOf(keywords2[i].toLowerCase()) < 0) {
                flag2 = false;
            }
        }
        if (flag1 || flag2) {
            result += "<nobr>Иные лица</nobr><br/>";
        }
        return result == "" ? result : result.substr(0, result.length - 5);
    }

    //Отображает организацию
    function show_side_issuer(issuer_side) {
        var cell_content = "";
        if (issuer_side.ticker != null) {
            if (issuer_side.profile_type == 1) {
                cell_content = '<a href=\"/issuers/' + issuer_side.ticker + '\" target=\"_blank\">' + issuer_side.name + '</a>';
            } else {
                cell_content = '<a href=\"/profile_ip/' + issuer_side.ticker + '\" target=\"_blank\">' + issuer_side.name + '</a>';
            }

        } else {
            var ruler = issuer_side.name;
            var name = String(issuer_side.name).toLowerCase();
            if (name.indexOf("арбитражный управляющий ") == 0) {
                issuer_side.profile_type = 2;
                ruler = issuer_side.name.substr(24);
            }
            if (name.indexOf("ип ") == 0) {
                issuer_side.profile_type = 2;
                ruler = issuer_side.name.substr(3);
            }
            if (issuer_side.profile_type == 1) {
                var re = new RegExp("[\"\']", "ig");
                cell_content = '<a href=\"/dbsearch/dbsearchru?name=' + String(issuer_side.name).replace(re, "").replace(/^\s+|\s+$/g, '').replace("+", '-D-').replace(/\-/g, '%20') + '\" target=\"_blank\">' + issuer_side.name + '</a>';
            }
            else {
                cell_content = '<a href=\"/dbsearch/dbsearchru/ichp?ruler=' + String(ruler).replace(re, "").replace(/^\s+|\s+$/g, '').replace("+", '-D-').replace(/\-/g, '%20') + '\" target=\"_blank\">' + issuer_side.name + '</a>';
            }
        }
        return cell_content;
    }

    window.switch_membs = function (id) {
        if ($("#s" + id).attr("val") == "0") {
            //надо развернуть
            $("#s" + id).html("Свернуть");
            $("#o" + id).css("display", "block");
            $("#img" + id).attr("src", "/images/tra_w.png");
            $("#s" + id).attr("val", "1");

        } else {
            //надо свернуть
            $("#s" + id).html("Подробнее");
            $("#s" + id).attr("val", "0");
            $("#o" + id).css("display", "none");
            $("#img" + id).attr("src", "/images/tra_e.png");
        }
    };

    window.switch_membs2 = function (id) {
        if ($("#ss" + id).attr("val") == "0") {
            //надо развернуть
            $("#ss" + id).html("Свернуть");
            $("#oo" + id).css("display", "block");
            $("#img" + id).attr("src", "/images/tra_w.png");
            $("#ss" + id).attr("val", "1");

        } else {
            //надо свернуть
            $("#ss" + id).html("Подробнее");
            $("#ss" + id).attr("val", "0");
            $("#oo" + id).css("display", "none");
            $("#img" + id).attr("src", "/images/tra_e.png");
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
            html += '<td onclick="ev_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="ev_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="ev_search(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<br/><div id="page_counter"><table>' + html + '</table></div>';
    };


    window.showCount = function () {
        $.post("/PravoSearch/Counter_elastic", function (data) {
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

    window.ShowCase = function (ids, reg_no, iss, src) {
        showClock();
        ShowCaseFlag = true;
        iss = (!iss) ? $("#iss").val() : iss;

        var url = "/Pravo/GetMessage";
        var params = "ids=" + ids + "&ticker=" + iss + "&src=" + src;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                ShowCaseFlag = false;
                if (data) {
                    show_dialog({ "content": data, "extra_style": "width:990px;", is_print: true });
                }
            }
        });
    };


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
        var url = "/PravoSearch/GetMessages";
        var params = "ids=" + ids;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    show_dialog({ "content": data, "extra_style": "width:990px;", is_print: true });
                }
            }
        });
    };



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

    Date.prototype.format = function (mask, utc) {
        return dateFormat(this, mask, utc);
    };

})();

$().ready(function () {
    search_init();
});
