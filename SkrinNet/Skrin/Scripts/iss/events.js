(function () {
    var ev = {};
    ev.type = '';
    var iss = ISS;
    var issuer_id = $('#issuer_id').val();

    var init_ev_calendar = function () {
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
    var ev_get_date = function () {
        var url = "/Events/GetMessageDatesAsync";
        $.ajax({
            url: url,
            type: "POST",
            data: { issuer_id: issuer_id },
            success: function (data) {
                if (data) {
                    $('#dfrom').val(data.Item1);
                    $('#dto').val(data.Item2);
                }
            }
        });
    }




    var getMessageTypes = function () {     
        var url = "/Events/GetMessageTypesAsync";
        $.ajax({
            url: url,
            type: "POST",
            data: { issuer_id: issuer_id },
            success: function (data) {
                if (data) {                         
                    //var sHtml = '<option value="0">Все</option>';
                    //for (var i = 0; i < data.length; i++) {
                    //    sHtml += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
                    //}
                    //$('select#eventTypes').append(sHtml);
                    var $source_container = $('#eventTypes');
                    for (var i = 0; i < data.length; i++) {
                        var $radio_div = $('<div class="checkbox">');
                        var $src_label = $('<label>');
                        var $src_input = $('<input type="checkbox" name="tselect">');
                        $src_input.val(data[i].id);
                        $src_label.append($src_input);
                        $src_label.append(data[i].name);
                        $radio_div.append($src_label);
                        $source_container.append($radio_div);
                    }
                }
            }
        });
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

    window.events_init = function () {
        ev_get_date();
        getMessageTypes();
        init_ev_calendar();
        ev_save_search_params();
        ev_search(1);

        $('#btn_find').click(function () {
            ev_save_search_params();
            ev_search(1);
        });

        /*

        set_print_function(function () {
            var print_all = true;
            $("input[name='ch_bankrot']").each(function () {
                if ($(this).prop('checked')) {
                    print_all = false;
                    return false;
                }
            });
            showSelectedEvents('',print_all);
        });*/
    };


    window.ev_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();
       // var type = $('#eventTypes').val();      
        //ev.type = type !== null ? type : '';
        var type = get_type_values();
        ev.type = type;   
        ev.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        ev.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        ev.iss = iss;
    }

    //var ev_restore_search_params = function () {
    //    $('#dfrom').val(ev.dfrom);
    //    $('#dto').val(ev.dto);
    //};

    var GetLinks = function () {
        $('#link_details').text('');
       // var links = '<br/><img src=\"/images/icon_only_selected.gif\" width=\"16\" height=\"16\" border=\"0\"><a style="cursor:pointer" onclick=\"showSelectedEvents(\'\',true)\">Посмотреть выбранные события</a><br/>';
       // var links = "<br/><br/><table width=\"100%\" cellspacing=\"4\" cellpadding=\"0\"><tr><td><div class=\"data_comment\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе «Существенные факты»</div></td></tr></table><br/><br/><br/><br/><br/><br/>";
        var links = '<span class="data_comment">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и могут не отражать абсолютную актуальность и достоверность информации. Дополнительную информацию можно получить в разделе «Существенные факты»</span>';
        $('#link_details').html(links);
    }

    var get_paging = function (page, total) {
        var page_count = Math.ceil(total / 20);

        if (page_count <= 1) {
            return "";
        }

        var html = "";
        var StartPage = 1;
        if (page_count >= 8) {
            StartPage = ((page - 3 > 0) ? ((page_count - page < 3) ? page_count + (page_count - page) - 8 : page - 3) : 1);
        }

        if (page > 1 && page_count > 7) {
            html += '<td onclick="ev_save_search_params(); ev_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="ev_save_search_params(); ev_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="ev_save_search_params(); ev_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    window.ev_search = function (page) {
        showClock();
        ev.page = page;        
        var url = "/Events/SearchAsync";
        var params = "dfrom=" + ev.dfrom + "&dto=" + ev.dto + "&type_id=" + ev.type + "&issuer_id=" + issuer_id + "&page=" + ev.page;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    var total = data.length;
                    $('#search_count').html(data.length == 0 ? "<table class=\"notfound\" width=\"100%\"><tr><td class=\"notfound\">Нет данных соответствующих заданному условию</td></tr></table>" : "<div class=\"minicaption\">Всего найдено " + total + " сообщений.</div>");
                    var res_arr = data;
                    if (res_arr.length > 0) {
                        $('#t_content').text('');
                        $('#link_details').text('');
                        ev_generate_result(res_arr, page, total);
                    } else {
                        $('#t_content').html('');
                        $('#pager').text('');
                        $('#link_details').text('');
                    }
                }
            }
        });
    }
    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }


    var ev_generate_result = function (res_arr, page, total) {      
        var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
           // "<td class=\"table_caption\" style=\"width:65px;\"><input type=\"checkbox\" name=\"ch_bankrot_selector\"  id=\"ch_bankrot_selector\"></td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата</td>" +
            "<td class=\"table_caption\">Вид документа (информации)</td></tr>";
        var i_max = res_arr.length > 20 ? 20 : res_arr.length;
        var i_start = (page - 1) * 20;
        var i_end = (page * 20) > res_arr.length ? res_arr.length : page * 20; 
        var counter = 0;
        for (var i = i_start; i < i_end; i++) {
            counter++;     
            var event = "<a href=\"#\" style=\"cursor:pointer;\" onclick=\"Showevent('',\'" + iss + "\', \'" + res_arr[i].id + "\',0)\">" + res_arr[i].name + " </a>";
            var comment = res_arr[i].ec_date.length == 10 ? ('<span class="data_comment" style="margin:0">' + (res_arr[i].ec_news.length >= 32 ? ('<a class="data_comment" style="margin:0" href="#" onclick="Showevent(\'\',\'' + iss + '\', \'' + res_arr[i].ECID + '\',0)">' + res_arr[i].ec_date + ' ' + res_arr[i].ec_headline + '</a>') : (res_arr[i].ec_date + ' ' + res_arr[i].ec_headline)) + '</span>') : '';
            comment += res_arr[i].ce_date.length == 10 ? ('<span class="data_comment" style="margin:0">' + (res_arr[i].ce_news.length >= 32 ? ('<a class="data_comment" style="margin:0" href="#" onclick="Showevent(\'\',\'' + iss + '\', \'' + res_arr[i].CEID + '\',0)">' + res_arr[i].ce_date + ' ' + res_arr[i].ce_headline + '</a>') : (res_arr[i].ce_date + ' ' + res_arr[i].ce_headline)) + '</span>') : '';
            //th_str += "<tr><td class=\"table_item_left\" align=\"left\" style=\"width:20px;\"><input type=\"checkbox\" name=\"ch_bankrot\" needprint=\"" + (res_arr[i].enr_news.length > 31 ? "1" : "0") + "\" value=\"" + getval(res_arr[i].id) + "\"></input></td>" +
            th_str += "<tr>" +
                "<td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:80px;white-space:nowrap;\">" + getval(res_arr[i].rd) + "</td>" +
                "<td class=\"table_item_left\" align=\"left\" style=\"width:100%\" id=\"tn" + (i + 1) + "\"> " + (res_arr[i].enr_news.length > 31 ? event : res_arr[i].name) + comment + " </td></tr>";
        }
        th_str += "</table>";
        $('#t_content').html(th_str);       

        $('#pager').html(get_paging(page, total));
   
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

})();



$(document).ready(function () {
    events_init();
});