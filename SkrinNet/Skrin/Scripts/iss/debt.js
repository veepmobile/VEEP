
(function () {
    var debt = {};
    var iss = ISS;
    var step = 20;
    var keywords = [];

    $('#xls_btn').unbind('click').addClass('disabled');
    set_xls_function(function () {
        var form = document.createElement("form");
        form.action = "/debt2/DebtExport";
        form.method = "POST";
        element = document.createElement("input");
        element.type = "hidden";
        element.name = "so_string";
        element.value = JSON.stringify(debt);
        form.appendChild(element);
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    });

    //инициализация календаря
    var init_bg_calendar = function () {
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

    var format_date = function (ts) {
        var timestampInMilliSeconds = ts * 1000;
        var date = new Date(timestampInMilliSeconds);
        var day = (date.getDate() < 10 ? '0' : '') + date.getDate();
        var month = (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1);
        var year = date.getFullYear();
        var formattedDate = day + '.' + month + '.' + year;
        return formattedDate;
    }

    var get_type_values = function () {
        if (!$("input[name='tselect']:checked").val()) {
            return ""
        }
        var ret = [];
        $("input[name='tselect']").each(function () {
            var $el = $(this);
            if ($el.prop('checked')) {
                ret.push($el.val());
            }
        });
        return ret.join(",");
    }
  

    var get_regions = function () {
    //    showClock();
        var url = "/debt2/LoadRegions";
        $.ajax({
            url: url,
            type: "POST",
            data: { iss: iss },
            success: function (data) {
                if (data) {
                //    hideClock();
                //    var res = $.parseJSON(data);
                    res = data;
                    if (res) {
                        var sHtml = '<option value="0">Все</option>';
                        for (var i = 0; i < res.length; i++) {
                            sHtml += '<option value="' + res[i].region_id + '">' + res[i].region_id + ' ' + res[i].name + '</option>';
                        }
                        $('select#regSelect').append(sHtml);
                    }
                }
            }
        });
    }

    var get_predmets = function () {
     //   showClock();
        var url = "/debt2/LoadPredmet";
        $.ajax({
            url: url,
            type: "POST",
            data: { iss: iss },
            success: function (data) {
                if (data) {
           //         hideClock();
                    //var res = $.parseJSON(data);
                    var res = data;
                    if (res) {
                        var sHtml = '<option value="-1">Все</option>';
                        for (var i = 0; i < res.length; i++) {
                            sHtml += '<option value="' + res[i].predmet + '">' + res[i].predmet + '</option>';
                        }
                        $('select#perfobject').append(sHtml);
                    }
                }
            }
        });
    }

    var get_addresses = function () {
        //   showClock();
        var url = "/debt2/LoadAddress";
        $.ajax({
            url: url,
            type: "POST",
            data: { iss: iss },
            success: function (data) {
                if (data) {
                    //         hideClock();
                    var res = $.parseJSON(data);
                    if (res) {
                        var sHtml = '<option value="-1">Все</option>';
                        for (var i = 0; i < res.length; i++) {
                            sHtml += '<option value="' + res[i].address + '">' + res[i].address + '</option>';
                        }
                        $('select#addrSelect').append(sHtml);
                    }
                }
            }
        });
    }

    var get_debtors = function () {
      //  showClock();
        var url = "/debt2/LoadDebtor";
        $.ajax({
            url: url,
            type: "POST",
            data: { iss: iss },
            success: function (data) {
                if (data) {
               //     hideClock();
                    var res = $.parseJSON(data);
                    if (res) {
                        var sHtml = '';
                        var $source_container = $('#debtors');
                        var count = 0;
                        for (var i = 0; i < res.length; i++) {
                            var style = (i > 2) ? " debtors_toggle" : "";
                            count = (i > 2) ? count + 1 : count;
                            var $radio_div = $('<div class="checkbox'+ style +'">');
                            var $src_label = $('<label>');
                            var $src_input = $('<input type="checkbox" name="tselect">');
                            $src_input.val(res[i].debtor);
                            $src_label.append($src_input);
                            $src_label.append(res[i].debtor);
                            $radio_div.append($src_label);
                            $source_container.append($radio_div);
                        }
                        if (count > 0) {
                            $source_container.append("<a class=\"table_link icon-angle-down\" id=\"debtors_togler\" href=\"#\" onclick=\"change_toggler(event,1);\" data-count=\"Показать все (" +
                                count + " позиций).\">Показать всех (" + count + " позиций).</a>");
                            $(".debtors_toggle").css("display", "none");
                        }
                    }
                }
            }
        });
    }

    var get_keywords = function () {
        var url = "/debt2/LoadKeywords";
        $.ajax({
            url: url,
            type: "POST",
            data: { iss: iss },
            success: function (data) {
                if (data) {
                    keywords = data.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '').toUpperCase().split(' ');
                }
            }
        });
    }

    var get_paging = function (page, total) {
        var page_count = Math.ceil(total / step);
        if (page_count <= 1) {
            return "";
        }

        var html = "";
        var StartPage = 1;
        if (page_count >= 8) {
            StartPage = ((page - 3 > 0) ? ((page_count - page < 3) ? page_count + (page_count - page) - 8 : page - 3) : 1);
        }

        if (page > 1 && page_count > 7) {
            html += '<td onclick="debt_save_search_params(); debt_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="debt_save_search_params(); debt_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="debt_save_search_params(); debt_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };


    function include(arr, obj) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == obj) return true;
        }
    }

    /************************************/


    var debt_generate_result = function (result, page, total) {
        var res_arr = [];
        var counter = 0;        
        counter = total;//40
        res_arr = result;//20
        debt_restore_search_params();
        var th_str = "<table class=\"data_table\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"float:none;\">" +
            "<td class=\"table_caption\" style=\"width:65px;\"><input type=\"checkbox\" name=\"ch_bankrot_selector\" id=\"ch_bankrot_selector\"></td>" +
            "<td class=\"table_caption\">&nbsp;</td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата возбуждения</td>" +
            "<td class=\"table_caption\">Номер и регион исполнительного производства</td>" +
            "<td class=\"table_caption\">Предмет исполнения	</td>" +
            "<td class=\"table_caption\">Остаток задолженности (руб.)</td>" +
            "<td class=\"table_caption\">Должник</td>" +
            "<td class=\"table_caption\">Адрес должника</td></tr>";
      
        var i_min =  0 ;
        var i_max = step - 1;
        i_max = (i_max < res_arr.length) ? i_max : res_arr.length - 1;
        for (var i = i_min ; i <= i_max; i++) {
            var ostatok = getostatok(getval(res_arr[i].version), getval(res_arr[i].cause), getval(res_arr[i].date3), getval(res_arr[i].summ));
            var label = getlabel(res_arr[i].searchtype);
            th_str += "<tr><td class=\"table_item_left\" align=\"left\" style=\"width:20px;\"><input type=\"checkbox\" name=\"ch_bankrot\" val=\"" + getval(res_arr[i].version.toString()) + " " +
                getval(res_arr[i].id) + "\" value=\"" + getval(res_arr[i].version.toString()) + " " + getval(res_arr[i].id) + "\"></input></td>" +
                "<td class=\"table_item_left\" align=\"center\">" + label + "</td>" +
                "<td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:80px;white-space:nowrap;\">" + getval(res_arr[i].date1_ts) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\"><a href=\"#\" onclick=\"ShowDebtSel([{id:" + getval(res_arr[i].id) + ", ver:" + getval(res_arr[i].version.toString()) + "}])\">" +
                getval(res_arr[i].nproizv) + "</a> " + getval(res_arr[i].name) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\">" + getval(res_arr[i].predmet) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\">" + ostatok + "</td>" +
                //"<td class=\"table_item_left\" align=\"center\">" + WordSelector(getval(res_arr[i].debtor)) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\">" + getval(res_arr[i].debtor) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\">" + getval(res_arr[i].adress) + "</td>" +
               "</tr>";
        }
        th_str += "</table>";
        $('#t_content').html(th_str);

        $('#pager').html(get_paging(page, counter));
      
        if (counter == 0) {
            $('#t_content').text('');
            $('#pager').text('');
            $('#link_details').text('');
        }
        else {
            GetLinks();
            $("#ch_bankrot_selector").on('click', function () {
                var is_checked = $(this).prop('checked')
                $("input[name='ch_bankrot']").prop('checked', is_checked);
            });
        }
    }

    //Выделение keywords в тексте
    var WordSelector = function (val) {
        if (keywords.length > 0) {
            for (var i = 0; i < keywords.length; i++) {
                if (keywords[i].length > 1) {
                    val = val.replace(new RegExp('(' + keywords[i] + ')', 'gi'), "<span style=\"background-color: #FFFF55;\">$1</span>");
                }
            }
        }
        return val;
    }
   
    var GetLinks = function () {
        $('#link_details').text('');
        //var links = '<br/><img src=\"/images/icon_only_selected.gif\" width=\"16\" height=\"16\" border=\"0\"><a style="cursor:pointer" onclick=\"showSelectedDebt()\">Посмотреть выбранные дела</a><br/>';
      //  var links = "<br/><br/><table width=\"100%\" cellspacing=\"4\" cellpadding=\"0\"><tr><td><div class=\"data_comment\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и отображаются исключительно при обнаружении в поле \"Должник\" текста (наименования юридического лица или его части), по которому осуществляется поиск</div></td></tr></table><br/><br/><br/><br/><br/><br/>";
        var links = '<span class="data_comment">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и отображаются при обнаружении в поле "Должник" и/или "Адрес должника" текста, по которому осуществляется поиск</span>';
        $('#link_details').html(links);
    }

    // вывод деталей дел


    window.debt_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();
        //debt.type = get_type_values();
        debt.dateFrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        debt.dateTo = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        debt.numProizv = $('#nproizv').val().replace(/\s+/g, '%20');
        var region = $('select#regSelect').val();
        debt.region = (region) ? region.toString() : "0"
        //debt.shortname = $('#shortName').val().toLowerCase();
        //var searchname = $('#searchname').val().toLowerCase();
        //var searchname2 = $('#searchname2').val().toLowerCase();
        //debt.fullname = (searchname2) ? searchname2.replace(/\s+/g, '%20') : "";
        //debt.searchname = searchname.replace(/"/g, '').replace(/\!/g, '').replace(/-/g, ' ').replace(/'/g, '').toLowerCase();
        //debt.searchname2 = searchname2;
        //debt.debtorName = (searchname) ? searchname.replace(/\s+/g, '%20').toLowerCase() : "";
        var predmet = $('#perfobject').val();
        debt.predmet = (predmet || predmet == "") ? predmet.replace(/\s+/g, '%20') : "-1";
        //var address = $('select#addrSelect').val();
        //debt.address = (address || address == "") ? address : "-1";
        debt.iss = iss;
        debt.step = step;
        //var debtorstr = "";
        //$("input[name='tselect']").each(function () {
        //    var $el = $(this);
        //    if ($el.prop('checked')) {
        //        debtorstr += '\"' + $(this).val().split('"').join('\\\"') + '\",';
        //    }
        //});
        //debt.debtorstr = debtorstr;
        var version = "";
        $("input[name='verselect']:checked").each(function () {
            version += $(this).val();
        });
        debt.version = (version.length == 1 ? version : "-1");
/*
        var queryvar = 0;
        $("input[name='querselect']:checked").each(function () {
            queryvar += parseInt($(this).val());
        });
        debt.queryoption = (queryvar == 7 ? 0 : queryvar);
*/
        debt.searchtypes = [];
        $("input[name='querselect']:checked").each(function () {
            debt.searchtypes.push(parseInt($(this).val()));
        });

    };

    var debt_restore_search_params = function () {
        $('#dfrom').val(debt.dateFrom);
        $('#dto').val(debt.dateTo);
    };

    window.debt_init = function () {
        SEARCHNAME = $('#searchname').val();
        SEARCHNAME2 = $('#searchname2').val();

        get_regions();
        get_predmets();

        //get_keywords();
//        get_addresses();
//        get_debtors();

        init_bg_calendar();
        debt_save_search_params();
        get_summary();
        debt_search(1);

        $('#btn_find').click(function () {
            debt_save_search_params();
            get_summary();
            debt_search(1);
        });

        set_print_function(function () {
            var print_all = true;
            $("input[name='ch_bankrot']").each(function () {
                if ($(this).prop('checked')) {
                    print_all = false;
                    return false;
                }
            });
            showSelectedDebt(print_all);
        });

        $("#tab_content").css("min-width", "1450px");
    };

    window.debt_search = function (page) {
        showClock();
        if (page * debt.step > 10000)
        {
            $('#search_count').html('Слишком много результатов. Попробуйте уточнить запрос.');
            $('#t_content').html('');
            $('#pager').text('');
            $('#link_details').text('');
            hideClock();
            return false;
        }
        debt.page = page;
        var url = "/debt2/DebtorSearchAsync";
/*
        var params = "dateFrom=" + debt.dateFrom + "&dateTo=" + debt.dateTo + "&debtorName=" + encodeURIComponent(debt.debtorName) + "&fullName=" + encodeURIComponent(debt.fullname) + "&numProizv=" + debt.numProizv + "&predmet=" +
           debt.predmet + "&region=" + debt.region + "&iss=" + debt.iss + "&page=" + debt.page + "&step=" + debt.step + "&address=" + encodeURIComponent(debt.address) + "&debtorstr=" + encodeURIComponent(debt.debtorstr) + "&version=" + debt.version + "&queryoption=" + debt.queryoption;
*/
        $.ajax({
            url: url,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(debt),
            success: function (data) {
                if (data) {
                    hideClock();
                    res = data;
                    if (res) {
                        var total = res.total;
                        $('#search_count').html(total == 0 ? "<table class=\"notfound\" width=\"100%\"><tr><td class=\"notfound\">Нет данных соответствующих заданному условию</td></tr></table>" : "<div class=\"minicaption\">Всего найдено исполнительных производств с наименованием должника, имеющим сходство с искомым: " + total + ".</div>");
                        var res_arr = res.results;
                        if (res_arr.length > 0) {
                            $('#t_content').text('');
                            $('#link_details').text('');
                            debt_generate_result(res_arr, page, total);
                        } else {
                            $('#t_content').html('');
                            $('#pager').text('');
                            $('#link_details').text('');
                        }
                    } else {
                        $('#search_count').text('Ошибка сервиса, попробуйте зайти позже.')
                    }
                }
            }
        });        
    }

    window.get_summary = function () {
        var url = "/debt2/LoadSummaryTable";
/*
        var params = "dateFrom=" + debt.dateFrom + "&dateTo=" + debt.dateTo + "&debtorName=" + encodeURIComponent(debt.debtorName) + "&fullName=" + encodeURIComponent(debt.fullname) + "&numProizv=" + debt.numProizv + "&predmet=" +
           debt.predmet + "&region=" + debt.region + "&iss=" + debt.iss + "&page=" + debt.page + "&step=" + debt.step + "&address=" + encodeURIComponent(debt.address) + "&debtorstr=" + encodeURIComponent(debt.debtorstr) + "&version=" + debt.version + "&queryoption=" + debt.queryoption;
*/
        $.ajax({
            url: url,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(debt),
            success: function (data) {
                if (data) {
                    var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
                    "<tr><td class=\"table_caption\" align=\"center\" style=\"width:50px;\" rowspan=\"2\">Год</td>" +
                    "<td class=\"table_caption\" align=\"center\" colspan=\"2\">Незавершенные исполнительные производства</td><td class=\"table_caption\" align=\"center\" colspan=\"2\">Завершенные исполнительные производства</td></tr>" +
                    "<tr><td class=\"table_caption\" align=\"center\">Количество</td><td class=\"table_caption\" align=\"center\">Сумма, руб.</td>" +
                    "<td class=\"table_caption\" align=\"center\">Количество</td><td class=\"table_caption\" align=\"center\">Сумма, руб.</td></tr>";
                    //var res_arr = $.parseJSON(data);
                    console.log(data);
                    res_arr = data;
                    if (res_arr) {
                        var tr_str = "";
                        if (res_arr.length > 0) {
                            var sum_str = "<tr style=\"background: #ffffff;\"><td class=\"table_item\" align=\"center\"><b>Итого:</b></td>";
                            var now_count = 0;
                            var now_sum = 0;
                            var old_count = 0;
                            var old_sum = 0;
                            var count = 0;
                            res_arr = delempty(res_arr);
                            for (var i = 0, i_max = res_arr.length; i < i_max; i++) {

                                var style = (i > 2) ? "class=\"summary_toggle\"" : "";
                                count = (i > 2) ? count + 1 : count;
                                tr_str += "<tr " + style + "><td class=\"table_item_center\">" + res_arr[i].year.toString() +
                                    "</td><td class=\"table_item_right\">" + getnum(res_arr[i].nowcnt) + "</td><td class=\"table_item_right\">" +
                                    number_format(getfloat(res_arr[i].nowsum), { thousands_sep: "", dec_point: "," }) + "</td><td class=\"table_item_right\">" +
                                   getnum(res_arr[i].oldcnt) + "</td><td class=\"table_item_right\">" +
                                    number_format(getfloat(res_arr[i].oldsum), { thousands_sep: "", dec_point: "," }) + "</td></tr>";
                                now_count = now_count + getnum(res_arr[i].nowcnt);
                                now_sum = now_sum + getfloat(res_arr[i].nowsum);
                                old_count = old_count + getnum(res_arr[i].oldcnt);
                                old_sum = old_sum + getfloat(res_arr[i].oldsum);
                            }
                            sum_str += "<td class=\"table_item_right\"><b>" + now_count + "</b></td><td class=\"table_item_right\"><b>" +
                                number_format(now_sum, { thousands_sep: "", dec_point: "," }) + "</b></td><td class=\"table_item_right\"><b>" +
                                old_count + "</b></td><td class=\"table_item_right\"><b>" + number_format(old_sum, { thousands_sep: "", dec_point: "," }) +
                                "</b></td></tr>";
                            if (count > 0) {
                                tr_str += "<tr><td colspan=\"5\" class=\"table_item_right\"><a class=\"table_link icon-angle-down\" id=\"summary_togler\" href=\"#\" onclick=\"change_toggler(event,0);\" data-count=\"Показать все (" +
                                    count + " позиций).\">Показать все (" + count + " позиций).</a></td></tr>";
                            }
                            $('#summary').html(th_str + sum_str + tr_str);
                            $(".summary_toggle").css("display", "none");
                        }
                        else {
                            $('#summary').html('');
                        }
                    }
                }
            }
        });
    }

    window.change_toggler = function (event, place) {
        if (event) {
            event.preventDefault();
            event.stopPropagation();
        }
        var nameofplace = (place == 0 ? 'summary' : 'debtors');
        var togler = "#" + nameofplace + "_togler";
        var toggle = '.' + nameofplace + '_toggle';
        var $el = $(togler);
        if ($el) {
            if ($el.hasClass("table_link")) {
                $el.removeClass("table_link").addClass("table_unlink").removeClass("icon-angle-down").addClass("icon-angle-up").text("Показать первые 3 позиции");
                $(toggle).css("display", "table-row");
            } else {
                $el.removeClass("table_unlink").addClass("table_link").removeClass("icon-angle-up").addClass("icon-angle-down").text($el.attr("data-count"));
                $(toggle).css("display", "none");
            }
        }
    };

})();

var getval = function (val) {
    if (!val) {
        return "";
    }
    return val;
}

var getnum = function (val) {
    if (!val) {
        return 0;
    }
    return parseInt(val);
}

var getfloat = function (val) {
    if (!val) {
        return 0;
    }
    return parseFloat(val);
}

function number_format(_number, _cfg) {
    function obj_merge(obj_first, obj_second) {
        var obj_return = {};
        for (key in obj_first) {
            if (typeof obj_second[key] !== 'undefined') obj_return[key] = obj_second[key];
            else obj_return[key] = obj_first[key];
        }
        return obj_return;
    }

    function thousands_sep(_num, _sep) {
        if (_num.length <= 3) return _num;
        var _count = _num.length;
        var _num_parser = '';
        var _count_digits = 0;
        for (var _p = (_count - 1) ; _p >= 0; _p--) {
            var _num_digit = _num.substr(_p, 1);
            if (_count_digits % 3 == 0 && _count_digits != 0 && !isNaN(parseFloat(_num_digit))) _num_parser = _sep + _num_parser;
            _num_parser = _num_digit + _num_parser;
            _count_digits++;
        }
        return _num_parser;
    }

    if (typeof _number !== 'number') {
        //_number = parseFloat(_number);
        if (isNaN(parseFloat(_number))) {
            return _number; // false;
        } else {
            _number = parseFloat(_number);
        }
    }

    var _cfg_default = {
        before: '',
        after: '',
        decimals: 2,
        dec_point: '.',
        thousands_sep: ','
    };

    if (_cfg && typeof _cfg === 'object') {
        _cfg = obj_merge(_cfg_default, _cfg);
    }
    else _cfg = _cfg_default;
    _number = _number.toFixed(_cfg.decimals);
    if (_number.indexOf('.') != -1) {
        var _number_arr = _number.split('.');
        var _number = thousands_sep(_number_arr[0], _cfg.thousands_sep) + _cfg.dec_point + _number_arr[1];
    }
    else var _number = thousands_sep(_number, _cfg.thousands_sep);
    return _cfg.before + _number + _cfg.after;

}

function ShowDebtSel(indexes) {
    showClock();
    var url = '/debt2/DebtorDetailsJson/';
    $.ajax({
        url: url,
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
        type: 'POST',
        data: "so_string=" + JSON.stringify(indexes),
        success: function (data) {
            if (data) {
                hideClock();
                var dhtml = "";
                dhtml += "<div class='minicaption'>БАНК ИСПОЛНИТЕЛЬНЫХ ПРОИЗВОДСТВ</div>Источник данных: ФССП России.<br/><br/>";
                dhtml += "<div class=minicaption>" + NAME + "</div><hr/>";

                for (var i = 0, i_max = (data.length - 1) ; i <= i_max; i++) {
                    dhtml += "<table width='100%' class='profile_table'><tr><td class='table_caption' align='left' width='70%'>Номер исполнительного производства: " +
                        getval(data[i].NumProizv) + "</td><td>" + getval(data[i].UpdateDate) + "</td></tr></table>";
                    dhtml += "<table width='100%' class='profile_table'><tr><td class='minicaption' colspan=2>Общие сведения</td>";
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Регион</td><td style='width:50%'>" + getval(data[i].Region) + "</td></tr>";
                    dhtml += "<tr><th style='width:50%'>Отдел судебных приставов</td><td style='width:50%'>" + getval(data[i].PristavName) + "</td></tr>";
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Адрес отдела судебных приставов</td><td style='width:50%'>" + getval(data[i].PristavAddress) + "</td></tr>";
                    dhtml += "<tr><th style='width:50%'>Номер исполнительного производства</td><td style='width:50%'>" + getval(data[i].NumProizv) + "</td></tr>";
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Дата возбуждения</td><td style='width:50%'>" + getval(data[i].DateProizv) + "</td></tr>";
                    dhtml += "<tr><th style='width:50%'>Номер сводного исполнительного производства</td><td style='width:50%'>" + getval(data[i].NumSvodPr) + "</td></tr>";
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Тип исполнительного документа</td><td style='width:50%'>" + getval(data[i].DocumentType) + "</td></tr>";
                    dhtml += "<tr><th style='width:50%'>Дата исполнительного документа</td><td style='width:50%'>" + getval(data[i].DocumentDate) + "</td></tr>";
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Номер исполнительного документа</td><td style='width:50%'>" + getval(data[i].DocumentNum) + "</td></tr>";
                    dhtml += "<tr><th style='width:50%'>Требования исполнительного документа</td><td style='width:50%'>" + getval(data[i].DocumentReq) + "</td></tr>";
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Предмет исполнения</td><td style='width:50%'>" + getval(data[i].Predmet) + "</td></tr>";
                    if (data[i].Status == 0) {
                        dhtml += "<tr><th style='width:50%'>Остаток задолженности (руб.)</td><td style='width:50%'>" + (data[i].Sum ? number_format(getval(data[i].Sum), { thousands_sep: "", dec_point: "," }) : "0,00") + "</td></tr>";
                    }
                    if (data[i].Status == 1){
                        dhtml += "<tr><th style='width:50%'>Дата окончания (прекращения) ИП</td><td style='width:50%'>" + getval(data[i].CloseDate) + "</td></tr>";
                        dhtml += "<tr><th style='width:50%'>Причина окончания (прекращения) ИП (статья, часть, пункт основания)</td><td style='width:50%'>" + getval(data[i].CloseCause) + "</td></tr>";
                        dhtml += "<tr><th style='width:50%'>Наименование статьи причины окончания ИП</td><td style='width:50%'>" + getostatok(1,getval(data[i].CloseCause)) + "</td></tr>";
                    }
                    dhtml += "<tr bgcolor='#F0F0F0'><th style='width:50%'>Должник</td><td style='width:50%'>" + getval(data[i].DebtorName) + "</td></tr>";
                    dhtml += "<tr><th style='width:50%'>Адрес</td><td style='width:50%'>" + getval(data[i].DebtorAddress) + "</td></tr></table><br/><hr/><br/>";
                }
                dhtml += "<div class='data_comment limitation'>ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.</div>";

                show_dialog({ "content": dhtml, is_print: true });
            }
        }
    });
}

var showSelectedDebt = function (all) {
    var sel = [];
    if (all) {
        $('input[name="ch_bankrot"]:checkbox').each(function (i) {
            var valuesplit = this.value.split(' ');
            if (valuesplit.length > 1) {
                sel.push({ id:valuesplit[1], ver:valuesplit[0] });
            }
        });
    }
    else {
        $('input[name="ch_bankrot"]:checkbox:checked').each(function (i) {
            var valuesplit = this.value.split(' ');
            if (valuesplit.length > 1) {
                sel.push({ id: valuesplit[1], ver: valuesplit[0] });
            }
        });
    }
    if (sel.length>0) {
        ShowDebtSel(sel);
    }
  
}

var getostatok = function (version, cause, date3, summ) {
    var ret = ""
    if (version == 0) {
        ret = "<span class=\"cat_yellow\">Н</span>&nbsp;Не завершено (" + (summ ? number_format(summ, { thousands_sep: "", dec_point: "," }) : "0,00") + " руб.)";
    }
    else if (version == 1) {
        if (date3)
        {
            ret += "<span class=\"cat_blue\">З</span>&nbsp;";
        }
        ret += "Завершено ";
        if (date3) {
            var month = ["", "января", "февраля", "марта", "апреля", "мая", "июня",
                        "июля", "августа", "сентября", "октября", "ноября", "декабря"][parseInt(date3.substring(3, 5))];
            ret += date3.substring(0, 2) + " " + month + " " + date3.substring(6, 10);
        }
        ret += " в связи с ";
        switch (cause) {
            case '. 47 . 1 .. 6':
                ret += "ликвидацией должника";
                break;
            case '. 47 . 1 .. 7':
                ret += "признанием должника банкротом";
                break;
            case '. 46 . 1 .. 3':
                ret += "невозможностью установить местонахождение должника";
                break;
            case '. 46 . 1 .. 4':
                ret += "отсутствием у должника имущества";
                break;
        }
    }
    return ret;
}

var delempty = function (arr) {
    for (var i = 0; i < arr.length; i++) {
        if (getnum(arr[i].nowcnt) == 0 && getnum(arr[i].oldcnt) == 0) {         
            arr.splice(i, 1);
            i--;
        }
    }
    return arr;
};

var getlabel = function(searchtype)
{
    var label = '';
    var greenlabel = '<span class="mode_green">V</span>';
    var redlabel = '<span class="mode_red">А</span>';
    var yellowlabel = '<span class="cat_yellow">Н</span>';
    switch (searchtype)
    {
        case 1:
            label = greenlabel;
            break;
        case 2:
            label = yellowlabel;
            break;
        case 3:
            label = redlabel;
            break;
    }
    return label;


/*
var getlabel = function(query, list, version, id)
{
    var label = '';
    var greenlabel = '<span class="mode_green">V</span>';
    var redlabel = '<span class="mode_red">А</span>';
    var yellowlabel = '<span class="cat_yellow">Н</span>';
    switch (query)
    {
        case 1:
            label = greenlabel;
            break;
        case 2:
            label = yellowlabel;
            break;
        case 4:
            label = redlabel;
            break;
        case 3:
            if (isexact(list[0], version, id))
                label = greenlabel;
            else
                label = yellowlabel;
            break;
        case 5:
            if (isexact(list[0], version, id))
                label = greenlabel;
            else
                label = redlabel;
            break;
        case 6:
            if (isexact(list[0], version, id))
                label = yellowlabel;
            else
                label = redlabel;
            break;
        case 0:
            if (isexact(list[0], version, id))
                label = greenlabel;
            else if (isexact(list[1], version, id))
                label = yellowlabel;
            else
                label = redlabel;
            break;
    }
    return label;
*/
}
var isexact = function (list, version, id) {
    var findid = binary_search_iterative(list, id);
    if (findid != null)
    {
        if (list[findid].version == version)
            return true;
    }
    return false;
}
function binary_search_iterative(a, value) {
    var lo = 0, hi = a.length - 1, mid;
    while (lo <= hi) {
        mid = Math.floor((lo + hi) / 2);
        if (a[mid].id > value)
            hi = mid - 1;
        else if (a[mid].id < value)
            lo = mid + 1;
        else
            return mid;
    }
    return null;
}

$(document).ready(function () {
    debt_init();
});