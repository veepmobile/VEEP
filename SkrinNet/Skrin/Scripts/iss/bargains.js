
(function () {
    var bg = {};
    var iss = ISS;
    var issuer_id = $('#issuer_id').val();
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


    var bg_get_date = function () {
        var url = "/Bargains/GetMessageDatesAsync";
        $.ajax({
            url: url,
            type: "POST",
            data: { issuer_id: issuer_id },
            success: function (data) {
                if (data) {
                    $('#dfrom').val(data.dStart);
                    $('#dto').val(data.dEnd);
                }
            }
        });
    }

    var getMessageTypes = function () {
      //  showClock();
        var url = "/Bargains/GetMessageTypesAsync";
        $.ajax({
            url: url,
            type: "POST",
            data: { issuer_id: issuer_id },
            success: function (data) {
                //hideClock();
                if (data) {
                    var $source_container = $('#bargTypes');
                    var $radio_div = $('<div class="checkbox">');
                    for (var i = 0; i < data.length; i++) {
                        if (i > 9) {
                            $radio_div = $('<div class="checkbox types_toggle">');
                        }
                        else
                        {
                            $radio_div = $('<div class="checkbox">');
                        }
                        var $src_label = $('<label>');
                        var $src_input = $('<input type="checkbox" name="tselect">');
                        $src_input.val(data[i].id);
                        $src_label.append($src_input);
                        $src_label.append(data[i].name);
                        $radio_div.append($src_label);
                        $source_container.append($radio_div);
                    }
                    if (data.length > 10) {
                        var $link_toggle = $('<div align=\"left\"><a class=\"table_link icon-angle-down\" id=\"types_togler\" href=\"#\" onclick=\"types_toggler(event);\" data-count=\"Показать все (' + data.length + ' позиций).\">Показать все (' + data.length + ' позиций).</a></div>');
                        $source_container.append($link_toggle);
                        $(".types_toggle").css("display", "none");
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

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }

    var bg_generate_result = function (res_arr, page, total) {
       // bg_restore_search_params();
        var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
            "<td class=\"table_caption\" style=\"width:65px;\"><input type=\"checkbox\" name=\"ch_bargains_selector\" id=\"ch_bargains_selector\"></td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата</td>" +       
            "<td class=\"table_caption\">Вид документа (информации)</td></tr>";

        var counter = 0;
        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
            counter++;
            var IsBarg = res_arr[i].file_name.length > 0 ? 'IsBarg="1"' : 'IsBarg="0"';
            var fn = res_arr[i].file_name.length > 0 ? ('fn="' + res_arr[i].file_name + '"') : ('fn=""');
            var event = res_arr[i].file_name.length > 0 ? ('showbarg(\'' + iss + '\',\'' + res_arr[i].id + '\',\'' + issuer_id + '\',\'' + res_arr[i].file_name + '\')') : ('shownews(\'' + res_arr[i].id + '\',\'\',\''+iss+'\',0,1,0)');
            th_str += "<tr><td class=\"table_item_left\" align=\"left\" style=\"width:20px;\"><input type=\"checkbox\" " + IsBarg + " " + fn + " name=\"ch_bargains\"  value=\"" + getval(res_arr[i].id) + "\"></input></td>" +
                "<td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:80px;white-space:nowrap;\">" + getval(res_arr[i].reg_date) + "</td>" +                
                "<td class=\"table_item_left\" align=\"left\" style=\"width:100%\" id=\"tn" + (i + 1) + "\"><a href=\"#\" style=\"cursor:pointer;\" onclick=\""+ event +"\">" + res_arr[i].name + "</a></td></tr>";
        }
        th_str += "</table>";
        $('#t_content').html(th_str);

        //bcGenPages(total, page, step);

        $('#pager').html(get_paging(page, total));

       // bc_get_date();
        if (counter == 0) {
            $('#t_content').text('');
            $('#pager').text('');
            $('#link_details').text('');
        }
        else {
            GetLinks();
            $("#ch_bargains_selector").on('click', function () {
                var is_checked = $(this).prop('checked')
                $("input[name='ch_bargains']").prop('checked', is_checked);
            });
        }
    }

    var GetLinks = function () {
       // $('#link_details').text('');
       // var links = '<br/><img src=\"/images/icon_only_selected.gif\" width=\"16\" height=\"16\" border=\"0\"><a style="cursor:pointer" onclick=\"showSelectedBarg(\'' + iss + '\',\'' + issuer_id + '\')\">Посмотреть выбранные документы</a><br/>';      
      //  $('#link_details').html(links);
    }
    

    window.bargains_init = function () {
        bg_get_date();
        getMessageTypes();
        init_bg_calendar();
        bg_save_search_params();
        bg_search(1);

        $('#btn_find').click(function () {
            bg_save_search_params();      
            bg_search(1);
        });

        set_print_function(function () {
            var print_all = true;
            $("input[name='ch_bargains']").each(function () {
                if ($(this).prop('checked')) {
                    print_all = false;
                    //return false;
                }
            });
            loadSelectedBarg(print_all);
        });
    };


    window.bg_save_search_params = function () { 
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();
        bg.type = get_type_values();
        bg.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        bg.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";      
        bg.iss = iss;
    };

    var bg_restore_search_params = function () {
        $('#dfrom').val(bg.dfrom);
        $('#dto').val(bg.dto);  
    };


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
            html += '<td onclick="bg_save_search_params(); bg_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="bg_save_search_params(); bg_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="bg_save_search_params(); bg_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    window.bg_search = function (page) {      
        showClock();
        bg.page = page;       
        var url = "/Bargains/BargainsSearchAsync";
        var params = "dfrom=" + bg.dfrom + "&dto=" + bg.dto + "&types=" + bg.type + "&iss=" + iss + "&page=" + bg.page;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    var total = data.Items[0].total;
                    $('#search_count').html(data.length == 0 ? "<table class=\"notfound\" width=\"100%\"><tr><td class=\"notfound\">Нет данных соответствующих заданному условию</td></tr></table>" : "<div class=\"minicaption\">Всего найдено " + total + " сообщений.</div>");
                    var res_arr = data.Items;
                    if (res_arr.length > 0) {
                        $('#t_content').text('');
                        $('#link_details').text('');
                        bg_generate_result(res_arr, page, total);
                    } else {
                        $('#t_content').html('');
                        $('#pager').text('');
                        $('#link_details').text('');
                    }
                }
            }
        });
    }

    window.types_toggler = function (event) {
        if (event) {
            event.preventDefault();
            event.stopPropagation();
        }
        var $el = $("#types_togler");
        if ($el) {
            if ($el.hasClass("table_link")) {
                $el.removeClass("table_link").addClass("table_unlink").removeClass("icon-angle-down").addClass("icon-angle-up").text("Показать первые 10 позиций");
                $('.types_toggle').css("display", "table-row");
            } else {
                $el.removeClass("table_unlink").addClass("table_link").removeClass("icon-angle-up").addClass("icon-angle-down").text($el.attr("data-count"));
                $('.types_toggle').css("display", "none");
            }
        }
    };

    window.showSelectedBarg = function (iss, iss_code) {
        var ids = '';
        var IsBarg = '';
        var fnames = '';
        $('input[name="ch_bargains"]:checkbox:checked').each(function (i) {
            ids += this.value + ",";
            IsBarg += $(this).attr('isbarg') + ',';
            fnames += $(this).attr('fn') + ',';
        });
        showClock();

        var params = 'ids=' + ids + '&isbargs=' + IsBarg + '&ticker=' + iss + '&iss_code=' + iss_code + '&fns=' + fnames;
        $.ajax({
            url: "/Bargains/GetSelectedMessages",
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    show_dialog({ "content": data, "is_print": true });
                }
            }
        });
    }

    var loadSelectedBarg = function (print_all) {
        if (print_all) {
            $("input[name='ch_bargains']").prop('checked', 'checked');
        }
        showSelectedBarg(iss, issuer_id);
    }


})();




$(document).ready(function () {
    bargains_init();
});