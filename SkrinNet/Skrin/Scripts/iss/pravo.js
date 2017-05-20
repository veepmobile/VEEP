
(function () {
    var prSO = {};
    var iss = ISS;
    var courts = null;
    var dtypes = null;
    var dcateg = null;

    $('#xls_btn').unbind('click').addClass('disabled');
    set_xls_function(function () {
        pr_save_search_params();
        prSO.rcount = 10000;
        var form = document.createElement("form");
        form.action = "/Pravo/PravoExport_elasticAsync";
        form.method = "POST";
        element = document.createElement("input");
        element.type = "hidden";
        element.name = "so_string";
        element.value = JSON.stringify(prSO);
        form.appendChild(element);
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    });

    //инициализация календаря
    var init_pr_calendar = function () {
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

    //инициализация селекторов
    var init_pr_selectors = function () {
        load_courts_selector();
        load_dtypes_selector();
    }

    var load_courts_selector = function () {
        if (!courts) {
            showClock();
            pr_save_search_params();
            var url = "/Pravo/GetCourts_elasticAsync";
            var params = "&kw=" + prSO.kw + "&iss=" + prSO.iss + "&isCompany=" + prSO.iscompany + "&rmode=" + prSO.rmode;
            $.ajax({
                url: url,
                type: "POST",
                data: params,
                success: function (data) {
                    hideClock();
                    var res = $.parseJSON(data);
                    courts = (res) ? res.results : null;
                    show_courts_selector();
                }
            });
        }
        else {
            show_courts_selector();
        }
    }

    var show_courts_selector = function () {
        var $source_container = $('#courts_select');
        for (var i = 0; i < courts.length; i++) {
            if (i > 9) {
                $radio_div = $('<div class="checkbox ac_toggle">');
            }
            else {
                $radio_div = $('<div class="checkbox">');
            }
            $src_label = $('<label>');
            $src_input = $('<input type="checkbox" name="cselect">');
            $src_input.val(courts[i]["cname"]);
            $src_label.append($src_input);
            $src_label.append(courts[i]["cname"]);
            $radio_div.append($src_label);
            $source_container.append($radio_div);
        }
        if (courts.length > 10)
        {
            var $link_toggle = $('<div align=\"left\"><a class=\"table_link icon-angle-down\" id=\"ac_togler\" href=\"#\" onclick=\"ac_toggler(event);\" data-count=\"Показать все (' + courts.length + ' позиций).\">Показать все (' + courts.length + ' позиций).</a></div>');
            $source_container.append($link_toggle);
            $(".ac_toggle").css("display", "none");
        }
    }

    var load_dtypes_selector = function () {
        if (!dtypes) {
            showClock();
            pr_save_search_params();
            var url = "/Pravo/GetDtypes_elasticAsync";
            var params = "&kw=" + prSO.kw + "&iss=" + prSO.iss + "&isCompany=" + prSO.iscompany + "&rmode=" + prSO.rmode;
            $.ajax({
                url: url,
                type: "POST",
                data: params,
                success: function (data) {
                    hideClock();
                    var res = $.parseJSON(data);
                    dtypes = (res) ? res.results : null;
                    show_dtypes_selector();
                }
            });
        }
        else {
            show_dtypes_selector();
        }
    }

    var show_dtypes_selector = function () {
        var $source_container = $('#dtypes_select');
        for (var i = 0; i < dtypes.length; i++) {
            var $radio_div = $('<div class="checkbox">');
            var $src_label = $('<label>');
            var $src_input = $('<input type="checkbox" name="dselect">');
            $src_input.val(dtypes[i]["ctype_id"]);
            $src_label.append($src_input);
            $src_label.append(dtypes[i]["ctype_name"]);
            $radio_div.append($src_label);
            $source_container.append($radio_div);
        }
    }

    var pr_clear = function () {
        $('#dfrom').val('');
        $('#dto').val('');
        $('#kw').val('');
        $('#job_no').val('');
        set_input_value('courts_select', '-1');
        set_input_value('dtype_select', '-1');
        set_input_value('dcategory_select', '-1');
    }


    function load_summary() {
        var url = "/Pravo/SummaryTable_elasticAsync";
        //var params = "ticker=" + prSO.iss;
        var params = "dfrom=" + prSO.dfrom + "&dto=" + prSO.dto + "&kw=" + prSO.kw + "&job_no=" + prSO.job_no + "&ac_name=" + prSO.ac_name + "&dtype=" + prSO.dtype + "&dcateg_id=" + prSO.dcateg_id + "&iss=" + prSO.iss + "&isCompany=" + prSO.iscompany + "&rmode=" + prSO.rmode;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                    var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
                    "<tr><td class=\"table_caption\" align=\"center\" style=\"width:50px;\" rowspan=\"2\">Год</td>" +
                    "<td class=\"table_caption\" align=\"center\" colspan=\"2\">В качестве истца</td><td class=\"table_caption\" align=\"center\" colspan=\"2\">В качестве ответчика</td></tr>" +
                    "<tr><td class=\"table_caption\" align=\"center\">Количество дел</td><td class=\"table_caption\" align=\"center\">Сумма, руб.</td>" +
                    "<td class=\"table_caption\" align=\"center\">Количество дел</td><td class=\"table_caption\" align=\"center\">Сумма, руб.</td></tr>";
                    var res_arr = data["YearsData"];
                    var tr_str = "";
                    if (res_arr.length > 0) {
                        var sum_str = "<tr style=\"background: #ffffff;\"><td class=\"table_item\" align=\"center\"><b>Итого:</b></td>";
                        var i_count = 0;
                        var i_sum = 0;
                        var o_count = 0;
                        var o_sum = 0;
                        var count = 0;
                        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
                            //var style = (i > 2) ? "hidded" : "stripy";
                            var style = (i > 2) ? "class=\"summary_toggle\"" : "";
                            count = (i > 2) ? count + 1 : count;
                            tr_str += "<tr " + style + "><td class=\"table_item_center\">" + res_arr[i].year + "</td><td class=\"table_item_right\">" + res_arr[i].icnt + "</td><td class=\"table_item_right\">" + RemZero(format_num(fixed(res_arr[i].isumma, 2))) + "</td><td class=\"table_item_right\">" + res_arr[i].ocnt + "</td><td class=\"table_item_right\">" + RemZero(format_num(fixed(res_arr[i].osumma, 2))) + "</td></tr>";
                            i_count = i_count + res_arr[i].icnt;
                            i_sum = i_sum + res_arr[i].isumma;
                            o_count = o_count + res_arr[i].ocnt;
                            o_sum = o_sum + res_arr[i].osumma;
                        }
                        sum_str += "<td class=\"table_item_right\"><b>" + i_count + "</b></td><td class=\"table_item_right\"><b>" + RemZero(format_num(fixed(i_sum, 2))) + "</b></td><td class=\"table_item_right\"><b>" + o_count + "</b></td><td class=\"table_item_right\"><b>" + RemZero(format_num(fixed(o_sum, 2))) + "</b></td></tr>";

                        //tr_str += "<tr><td colspan=\"5\" class=\"table_item_right\"><span style=\"cursor:pointer;color:#003399;\" val=\"0\" id=\"sw_summary\" onclick=\"SwitchSummary(" + count + ");\">Показать (" + count + " строк)</span><img src=\"/images/tra_e.png\" alt=\"\" style=\"padding-left:3px;padding-right:20px;\" id=\"arr\"></td></tr>";
                        tr_str += "<tr><td colspan=\"5\" class=\"table_item_right\"><a class=\"table_link icon-angle-down\" id=\"summary_togler\" href=\"#\" onclick=\"change_toggler(event);\" data-count=\"Показать все (" + count + " позиций).\">Показать все (" + count + " позиций).</a></td></tr>";

                        $('#summary').html(th_str + sum_str + tr_str);
                        $(".summary_toggle").css("display", "none");
                    }
                    else
                    {
                        $('#summary').html('');
                    }
            }
        });
    }

    function RemZero(str) {
        if (str == '0') {
            return '0.00';
        }
        return str;
    }

    function format_num(str) {
        return String(str).replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 ');
    }

    function fixed(N, n) {
        return Math.round(N * Math.pow(10, n)) / Math.pow(10, n);
    }

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }

    //Разбивает список организаций на отдельные
    function parse_sideslist(side_list) {
        var org_arr = side_list.split(',');
        var iss_list = [];
        for (var i = 0, i_max = org_arr.length; i < i_max; i++) {
            var iss_obj = org_arr[i].split('|');
            iss_list[i] = { "name": iss_obj[0], "ticker": iss_obj[1], "inn": iss_obj[2], "type": iss_obj[3] };
        }
        return iss_list;
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
            var re = new RegExp("[\"\']", "ig");
            var ruler = issuer_side.name;
            var name = String(issuer_side.name).toLowerCase();
            if (name.indexOf("арбитражный управляющий ") == 0)
            {
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

    var pr_generate_result = function (res_arr, page, total) {
        pr_restore_search_params();

        var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:22px;\"></td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата поступления дела в суд</td>" +
            "<td class=\"table_caption\" style=\"width:100px;\">Номер дела</td>" +
            "<td class=\"table_caption\">Сумма иска</td>" +
            "<td class=\"table_caption\">Арбитражный суд</td>" +
            "<td class=\"table_caption\">Форма участия</td>" +
            "<td class=\"table_caption\">Истец</td>" +
            "<td class=\"table_caption\">Ответчик</td></tr>";

        var counter = 0;
        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
            counter++;
            var it = res_arr[i]._source;
            var ist_list = [];
            var otv_list = [];
            var rmode = 3;
            for (var j = 0, j_max = (it.side.length - 1) ; j <= j_max; j++) {
                if (it.side[j].side_type_id == 0) ist_list[ist_list.length] = it.side[j];
                if (it.side[j].side_type_id == 1) otv_list[otv_list.length] = it.side[j];
                if (it.side[j].inn == INN && it.side[j].ogrn == OGRN) rmode = 1;
                if (it.side[j].inn == INN && it.side[j].ogrn == null && rmode > 2) rmode = 2;
                if (it.side[j].ogrn == OGRN && it.side[j].inn == null && rmode > 2) rmode = 2;
            }

            th_str += "<tr><td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:22px;white-space:nowrap;\">";
            if (rmode == 1) {
                th_str += "<span class=\"mode_green\">V</span>";
            }
            if (rmode == 2)
            {
                th_str += "<span class=\"mode_yellow\">!</span>";
            }
            if (rmode == 3)
            {
                th_str += "<span class=\"mode_red\">?</span>";
            }
            th_str += "</td>";

            th_str += "<td>" + getval(it.reg_date);

            th_str += "</td>" +
                "<td class=\"table_item_left\" align=\"center\"><nobr>" + get_categ(it.disput_type_cat_id) + "&nbsp;&nbsp;<a style=\"cursor:pointer;\" onclick=\"ShowCase('" + it.case_id + "','" + getval(it.reg_no) + "','" + iss + "','" + getval(it.case_type) + "');\">" + getval(it.reg_no) + "</a></nobr/></td>" +
                "<td class=\"table_item_right\" align=\"right\"><nobr>" + RemZero(format_num(it.case_sum)) + "</nobr></td>" +
                "<td class=\"table_item_left\" align=\"center\">" + getval(it.court_name) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\">" + res_arr[i].inner_hits.side.hits.hits[0]._source.side_type_name + "</td>";
            th_str += "<td class=\"table_item_left\" valign=\"top\">";
            var cell_content = "";
            if (ist_list.length == 1) {
                cell_content = show_side_issuer(ist_list[0]);
            }
            if (ist_list.length > 1) {
                cell_content = "<table class=\"no_border\" width=\"100%\"><tr><td style=\"white-space:nowrap;\"><span  style=\"font-weight:bold; \">Всего: " + ist_list.length
                            + "&nbsp;</span></td><td align=\"left\" style=\"width:100%;white-space:nowrap;\"><span style=\"cursor:pointer;color:#003399;\" val=\"0\" onclick=\"switch_membs(" + (i + 1) + ")\" id=\"s" + (i + 1) + "\">Подробнее</span><img src=\"/images/tra_e.png\" alt=\"\" style=\"padding-left:3px\" id=\"img" + (i + 1) + "\"></td></tr></table>"
                            + "<div style=\"display:none;\" id=\"o" + (i + 1) + "\"><table class=\"no_border\">";
                for (var j = 0, j_max = ist_list.length; j < j_max; j++) {
                    cell_content += "<tr><td>" + show_side_issuer(ist_list[j]) + "</td></tr>";
                }
                cell_content += "</table></div>";
            }
            th_str += cell_content + "</td><td class=\"table_item_left\" valign=\"top\">";
            cell_content = "";
            if (otv_list.length == 1) {
                cell_content = show_side_issuer(otv_list[0]);
            }
            if (otv_list.length > 1) {
                cell_content = "<table class=\"no_border\" width=\"100%\"><tr><td style=\"white-space:nowrap;\"><span style=\"font-weight:bold; \">Всего: " + otv_list.length
                            + "&nbsp;</span></td><td style=\"width:100%;white-space:nowrap;\"><span style=\"cursor:pointer;color:#003399;\" val=\"0\" onclick=\"switch_membs2(" + (i + 1) + ")\" id=\"ss" + (i + 1) + "\">Подробнее</span><img src=\"/images/tra_e.png\" alt=\"\" style=\"padding-left:3px\" id=\"img" + (i + 1) + "\"></td></tr></table>"
                            + "<div style=\"display:none;\" id=\"oo" + (i + 1) + "\"><table class=\"no_border\">";
                for (var j = 0, j_max = otv_list.length; j < j_max; j++) {
                    cell_content += "<tr><td>" + show_side_issuer(otv_list[j]) + "</td></tr>";
                }
                cell_content += "</table></div>";
            }
            th_str += cell_content + "</td></tr>";
        }
        th_str += "</table>";
        $('#t_content').html(th_str);

        $('#pager').html(get_paging(page, total));

        //pr_get_date();
        if (counter == 0) {
            $('#t_content').text('');
            $('#pager').text('');
            $('#link_details').text('');
        }
    }

    /*
    var pr_get_date = function () {
        var url = "/Pravo/GetPravoDatesAsync";
        var params = "dfrom=" + prSO.dfrom + "&dto=" + prSO.dto + "&kw=" + prSO.kw + "&ac_name=" + prSO.ac_name + "&dtype=" + prSO.dtype + "&iss=" + prSO.iss + "&page=" + prSO.page + "&isCompany=" + prSO.iscompany + "&mode=" + prSO.mode;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                if (data) {
                    var res = $.parseJSON(data);
                    var d1 = (prSO.dfrom != "") ? prSO.dfrom : format_date(res.results[0].mindate);
                    var d2 = (prSO.dto != "") ? prSO.dto : format_date(res.results[0].maxdate);
                    $('#dfrom').val(d1);
                    $('#dto').val(d2);
                }
            }
        });
    }*/

    var format_date = function (ts) {
        var timestampInMilliSeconds = ts * 1000;
        var date = new Date(timestampInMilliSeconds);
        var day = (date.getDate() < 10 ? '0' : '') + date.getDate();
        var month = (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1);
        var year = date.getFullYear();
        var formattedDate = day + '.' + month + '.' + year;
        return formattedDate;
    }

    var stopEvent = function (e) {

        if (window.event) {
            e = window.event;
        }
        kk = e.keyCode;
        if (e.stopPropagation) {
            e.stopPropagation();
        } else {
            e.cancelBubble = true;
        }
    }

    var get_paging = function (page, total) {
        var page_count = Math.ceil(total / 20);
        if (page_count > 500) {
            page_count = 500;
        }

        if (page_count <= 1) {
            return "";
        }

        var html = "";
        var StartPage = 1;
        if (page_count >= 8) {
            StartPage = ((page - 3 > 0) ? ((page_count - page < 3) ? page_count + (page_count - page) - 8 : page - 3) : 1);
        }

        if (page > 1 && page_count > 7) {
            html += '<td onclick="pr_save_search_params(); pr_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="pr_save_search_params(); pr_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="pr_save_search_params(); pr_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var get_categ = function (cat) {
        var result = "";
        switch (cat) {
            case 0:
                result = "<span class=\"cat_yellow\">П</span>";   //Прочие
                break;
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

    var pr_restore_search_params = function () {
        $('#dfrom').val(prSO.dfrom);
        $('#dto').val(prSO.dto);
        $('#kw').val(prSO.kw);
        $('#job_no').val(prSO.job_no);
        //set_input_value('courts_select', prSO.ac_name);
        //set_input_value('dtype_select', prSO.dtype);
        //set_input_value('dcategory_select', prSO.dcateg_id);
        //set_input_value('rmode', prSO.rmode);
        $('#is_company').val(prSO.iscompany);
    };

    window.pr_search = function (page) {
        $('#search_count').html("");
        showClock();
        load_summary();
        prSO.page = page;
        var url = "/Pravo/PravoSearch_elasticAsync";
        var params = "dfrom=" + prSO.dfrom + "&dto=" + prSO.dto + "&kw=" + prSO.kw + "&job_no=" + prSO.job_no + "&ac_name=" + prSO.ac_name + "&dtype=" + prSO.dtype + "&dcateg_id=" + prSO.dcateg_id + "&iss=" + prSO.iss + "&page=" + prSO.page + "&rcount=20" + "&isCompany=" + prSO.iscompany + "&rmode=" + prSO.rmode;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    var res = $.parseJSON(data);

                    //Общее количество найденных строк
                    var total = (res) ? res.hits.total : 0;
                    if (res) {
                        $('#search_count').html(total == 0 ? "<table class=\"notfound\" width=\"100%\"><tr><td class=\"notfound\">Нет данных соответствующих заданному условию</td></tr></table>" : "<div class=\"minicaption\">Всего найдено " + total + " сообщений.</div>");
                        var res_arr = res.hits.hits;
                        if (res_arr.length > 0) {
                            $('#t_content').text('');
                            $('#link_details').text('');
                            pr_generate_result(res_arr, page, total);
                        } else {
                            $('#t_content').html('');
                            $('#pager').text('');
                            $('#link_details').text('');
                            if (window.iss_pravo_firstload) {
                                window.iss_pravo_firstload = false;
                            }
                        }
                    } else {
                        $('#search_count').text('Ошибка сервиса, попробуйте зайти позже.')
                    }
                }
            }
        });

    }

    window.pravo_init = function () {
        pr_save_search_params();
        init_pr_calendar();
        init_pr_selectors();

        change_toggler();
        $("#summary_togler").click(change_toggler);


        ac_toggler();
        $("#ac_togler").click(ac_toggler);
        
        $("#tab_content").css("min-width", "1350px");

        window.iss_pravo_firstload = true;

    }

    window.pr_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();
        var kw = $('#kw').val();
        var job_no = TrimStr($("#job_no").val()).replace(new RegExp("[';]*", "ig"), "");
        var dtype_id = $('#dtype_id').val();
        prSO.iss = iss;
        prSO.iscompany = $('#is_company').val();
        prSO.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        prSO.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        prSO.kw = kw;
        prSO.job_no = job_no;
        prSO.ac_name = get_ac_values();
        prSO.dtype = get_dtype_values();;
        prSO.dcateg_id = get_dcateg_values();
        prSO.rmode = get_rmode_values();
    };

    var get_ac_values = function () {
        if (!$("input[name='cselect']:checked").val()) {
            return "-1"
        }
        var ret = [];
        $("input[name='cselect']").each(function () {
            var $el = $(this);
            if ($el.prop('checked')) {
                ret.push($el.val());
            }
        });
        return ret.join(",");
    }

    var get_dtype_values = function () {
        if (!$("input[name='dselect']:checked").val()) {
            return "-1"
        }
        var ret = [];
        $("input[name='dselect']").each(function () {
            var $el = $(this);
            if ($el.prop('checked')) {
                ret.push($el.val());
            }
        });
        return ret.join(",");
    }

    var get_dcateg_values = function () {
        if (!$("input[name='catselect']:checked").val()) {
            return "-1"
        }
        var ret = [];
        $("input[name='catselect']").each(function () {
            var $el = $(this);
            if ($el.prop('checked')) {
                ret.push($el.val());
            }
        });
        return ret.join(",");
    }

    var get_rmode_values = function () {
        if (!$("input[name='rmode']:checked").val()) {
            return "-1"
        }
        var ret = [];
        $("input[name='rmode']").each(function () {
            var $el = $(this);
            if ($el.prop('checked')) {
                ret.push($el.val());
            }
        });
        return ret.join(",");
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

    /*
    window.SwitchSummary = function (count) {
        if ($("#sw_summary").attr("val") == "0") {
            //надо развернуть
            $(".hidded").css("display", "table-row");
            $("#sw_summary").attr("val", "1");
            $("#sw_summary").html("Свернуть");
            $("#arr").attr("src", "/images/tra_w.png");

        }
        else {
            //надо свернуть
            $(".hidded").css("display", "none");
            $("#sw_summary").attr("val", "0");
            $("#sw_summary").html("Показать (" + count + " строк)");
            $("#arr").attr("src", "/images/tra_e.png");
        }
    };*/

    window.ShowCase = function (ids, reg_no, iss, src) {
        showClock();
        iss = (!iss) ? $("#iss").val() : iss;

        var url = "/Pravo/GetMessage";
        var params = "ids=" + ids + "&ticker=" + iss + "&src=" + src;
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

    window.change_toggler = function (event) {
        if (event) {
            event.preventDefault();
            event.stopPropagation();
        }
        var $el = $("#summary_togler");
        if ($el) {
            if ($el.hasClass("table_link")) {
                $el.removeClass("table_link").addClass("table_unlink").removeClass("icon-angle-down").addClass("icon-angle-up").text("Показать первые 3 позиции");
                $('.summary_toggle').css("display", "table-row");
            } else {
                $el.removeClass("table_unlink").addClass("table_link").removeClass("icon-angle-up").addClass("icon-angle-down").text($el.attr("data-count"));
                $('.summary_toggle').css("display", "none");
            }
        }
    };

    window.ac_toggler = function (event) {
        if (event) {
            event.preventDefault();
            event.stopPropagation();
        }
        var $el = $("#ac_togler");
        if ($el) {
            if ($el.hasClass("table_link")) {
                $el.removeClass("table_link").addClass("table_unlink").removeClass("icon-angle-down").addClass("icon-angle-up").text("Показать первые 10 позиций");
                $('.ac_toggle').css("display", "table-row");
            } else {
                $el.removeClass("table_unlink").addClass("table_link").removeClass("icon-angle-up").addClass("icon-angle-down").text($el.attr("data-count"));
                $('.ac_toggle').css("display", "none");
            }
        }
    };
})();


$(document).ready(function () {
    pravo_init();
    pr_save_search_params();
    pr_search(1);

    $('#btn_find').on("click", function () {
        e.preventDefault();
        pr_save_search_params();
        pr_search(1);
    });

});
