
(function () {
    var SO = {};
    var iss = ISS;
    var src = new Array();
    var mes_types = null;

    var init_vs_calendar = function () {
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

    var vs_get_date = function () {
        var url = "/Vestnik/GetMessageDatesAsync";
        var params = "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&type_exl=" + SO.type_exl + "&iss=" + SO.iss + "&page=" + SO.page + "&isCompany=" + SO.iscompany + "&mode=" + SO.mode;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                if (data) {
                    var res = $.parseJSON(data);
                    var d1 = (SO.dfrom != "") ? SO.dfrom : format_date(res.results[0].mindate);
                    var d2 = (SO.dto != "") ? SO.dto : format_date(res.results[0].maxdate);
                    $('#dfrom').val(d1);
                    $('#dto').val(d2);
                }
            }
        });
    };

    var format_date = function (ts) {
        var timestampInMilliSeconds = ts * 1000;
        var date = new Date(timestampInMilliSeconds);
        var day = (date.getDate() < 10 ? '0' : '') + date.getDate();
        var month = (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1);
        var year = date.getFullYear();
        var formattedDate = day + '.' + month + '.' + year;
        return formattedDate;
    };


    var init_vs_selectors = function () {
        load_type_selectors();
    }

    var load_type_selectors = function () {
            showClock();
            vs_save_search_params();
            var url = "/Vestnik/GetTypesAsync";
            var params = "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&type_exl=" + SO.type_exl + "&iss=" + SO.iss + "&page=" + SO.page + "&isCompany=" + SO.iscompany + "&mode=" + SO.mode;
            $.ajax({
                url: url,
                type: "POST",
                data: params,
                success: function (data) {
                    hideClock();
                    var res = $.parseJSON(data);
                    mes_types = (res) ? res.results : null;
                    show_type_selectors();
                }
            });
    }

    var show_type_selectors = function () {
        var $div = $('<div class="filter_block">');
        $div.append('<h4>Тип сообщения</h4>');
        var $source_container = $('<div class="form-group">');
        for (var i = 0; i < mes_types.length; i++) {
            var $radio_div = $('<div class="checkbox">');
            var $src_label = $('<label>');
            var $src_input = $('<input type="checkbox" name="tselect">');
            $src_input.val(mes_types[i].type_id);
            $src_label.append($src_input);
            $src_label.append(mes_types[i].type_name);
            $radio_div.append($src_label);
            $source_container.append($radio_div);
        }
        $div.append($source_container);
        $('#type_selector_container').append($div);
    }

    SO.toString = function () {
        return "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&type_exl=" + SO.type_exl + "&iss=" + SO.iss + "&page=" + SO.page + "&isCompany=" + SO.iscompany + "&mode=" + SO.mode;
    }

    window.vs_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();

        SO.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        SO.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        SO.kw = $('#kw').val();
        SO.type = get_type_values();
        SO.iss = iss;
        SO.iscompany = $('#is_company').val();
        SO.mode = $('input[name="vkl"]:checked').val();
    };

    window.vs_restore_search_params = function () {
        $('#dfrom').val(SO.dfrom);
        $('#dto').val(SO.dto);
        $('#kw').val(SO.kw);
        set_input_value('vkl', SO.mode);
        set_type_values(SO.type);
        $('#is_company').val(SO.iscompany);
    };

    var vs_clear = function () {
        $('#dfrom').val('');
        $('#dto').val('');
        $('#kw').val('');
        $('#type_selector_container').empty();
        $("#tsdiv").remove();
    }

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
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

    var set_type_values = function (values) {
        var ar;
        if (!values) {
            ar = [];
        } else {
            ar = values.split(",");
        }
        $("input[name='tselect']").each(function () {
            var $el = $(this);
            $el.prop('checked', false);
            for (var i = 0; i < ar.length; i++) {
                if ($el.val() == ar[i]) {
                    $el.prop('checked', true);
                    break;
                }
            }
        });
    }

    window.vs_search = function (page) {
        showClock();
        SO.page = page;
        var url = "/Vestnik/VestnikSearchAsync";
        var params = "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&iss=" + SO.iss + "&isCompany=" + SO.iscompany + "&page=" + SO.page + "&mode=" + SO.mode;
        hideClock();
        
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    var res = $.parseJSON(data);

                    //Общее количество найденных строк
                    var total = (res) ? res.total : 0;
                    if (total) {
                        $('#search_count').html(total == 0 ? "<table class=\"notfound\" width=\"100%\"><tr><td class=\"notfound\">Нет данных соответствующих заданному условию</td></tr></table>" : "<div class=\"minicaption\">Всего найдено " + total + " сообщений.</div>");
                        var res_arr = res.results;
                        if (res_arr.length > 0) {
                            $('#t_content').text('');
                            $('#link_details').text('');
                            vs_generate_result(res_arr, page, total);
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

    var vs_generate_result = function (res_arr, page, total) {
        vs_restore_search_params();
        var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
            "<td class=\"table_caption\" style=\"width:65px;\"><input type=\"checkbox\" name=\"ch_event_selector\" id=\"ch_event_selector\"></td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата</td>" +
            "<td class=\"table_caption\">Вид документа (информации)</td></tr>";

        var counter = 0;
        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
            counter++;
            var headers = '';
            /*if (res_arr[i].type_name == null || res_arr[i].type_name == '') {
                headers = getval(res_arr[i].headers); 
            }
            else {
                headers = getval(res_arr[i].type_name);
            }*/
            headers = getval(res_arr[i].type_name);
            th_str += "<tr><td class=\"table_item_left\" align=\"left\" style=\"width:20px;\"><input type=\"checkbox\" name=\"ch_event\" val=\"" + getval(res_arr[i].type_id) + "\" value=\"" + getval(res_arr[i].event_id) + "\"></input></td>" +
                "<td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:80px;white-space:nowrap;\">" + getval(res_arr[i].file_date) + "</td>" +
                "<td class=\"table_item_left\" align=\"left\" style=\"width:100%\" id=\"tn" + (i + 1) + "\"><a href=\"#\" style=\"cursor:pointer;\" onclick=\"ShowVEvent('" + getval(res_arr[i].event_id) + "','" + SO.kw + "');\">" + headers + "</a></td></tr>";
        }
        th_str += "</table>";
        $('#t_content').html(th_str);


        $('#pager').html(get_paging(page, total));

        vs_get_date();
        if (counter == 0) {
            $('#t_content').text('');
            $('#pager').text('');
            $('#link_details').text('');
        }
        else {
            GetLinks();
            $("#ch_event_selector").on('click', function () {
                var is_checked = $(this).prop('checked')
                $("input[name='ch_event']").prop('checked', is_checked);
            });
        }
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
            html += '<td onclick="vs_save_search_params(); vs_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="vs_save_search_params(); vs_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="vs_save_search_params(); vs_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var GetLinks = function () {
        $('#link_details').text('');
        var links = "<br/><div class=\"data_comment\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и отображаются исключительно при наличии в тексте сообщения данных (ОГРН и ИНН) юридического лица. В случае отсутствия ОГРН и/или ИНН , информация в данном разделе не отображается.</div>";
        $('#link_details').html(links);
    }

    var loadVEventSelected = function () {
        var ids = '';
        $('input[name="ch_event"]:checkbox:checked').each(function () {
            ids += this.value + ",";
        });
        if (!ids || ids == '') {
            return;
        }
        showClock()
        var url = "/Vestnik/GetMessagesSelected";
        var params = "ids=" + ids + "&ticker=" + iss;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    var result = "";
                    for (var i = 0; i < data.length; i++) {
                        result = result + printMessage(data[i]) + "<br/><hr/><br/>";
                    }
                    show_dialog({ "content": result, "extra_style": "width:990px;", is_print: true });
                }
            }
        });
    };

    var showVEventSelected = function (print_all) {
        if (print_all) {
            $("input[name='ch_event_selector']").prop('checked', 'checked');
            $("input[name='ch_event']").prop('checked', 'checked');
        }
        loadVEventSelected();
    };

    window.vestnik_init = function () {
        vs_save_search_params();
        init_vs_calendar();
        init_vs_selectors();

        $('input[name="vkl"]').on("change", function () {
            vs_clear();
            vs_save_search_params();
            vs_search(1);
            init_vs_selectors();
            vs_get_date();
        });

        vs_save_search_params();
        vs_search(1);
        
        set_print_function(function () {
            var print_all = true;
            $("input[name='ch_event']").each(function () {
                if ($(this).prop('checked')) {
                    print_all = false;
                    //return false;
                }
            });
            showVEventSelected(print_all);
        });
    }

    window.ShowVEvent = function (id, kw) {
        showClock();
        var url = "/Vestnik/GetMessage";
        var params = "id=" + id + "&ticker=" + iss;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    show_dialog({ "content": printMessage(data), "extra_style": "width:990px;", is_print: true });
                }
            }
        });
    };

    var printMessage = function (data) {
        var res = "";
        res += "<p class=\"minicaption\">СООБЩЕНИЕ ВЕСТНИКА ГОСУДАРСТВЕННОЙ РЕГИСТРАЦИИ</p><p>Источник данных: Вестник государственной регистрации.</p><br /><div class=\"minicaption\" style=\"float:left;\">" + data.name + "</div><br /><hr /><table style=\"width:100%;\"><tr><td align=\"right\">Дата публикации сообщения: " + data.dt + "<br />" + data.nomera + "</td></tr></table><br />";
        res += "<span style=\"font-weight:bold\">" + data.type_name + "</span><br />" + data.region + "<br /><br />";
        if (data.type_id == "17") {
            res += "<b>Считать недействительным(и) сообщенияе(я), опубликованное(ые) в журнале «Вестник государственной регистрации»</b><br /><br />";
        }
        res += data.contents;
        if (data.corr_id.length == 32) {
            res += "<br /><table style=\"width:100%;\"><tr><td  class=\"minicaption\"><br />Текст корректирующего сообщения</td></tr></table><table style=\"width:100%;\"><tr><td align=\"right\">Дата публикации сообщения: " + data.corr_dt + "<br />" + data.corr_nomera + "</td></tr></table><span style=\"font-weight:bold\">" + data.corr_type_name + "</span><br />" + data.corr_region + "<br /><br /><b>Считать недействительным(и) сообщенияе(я), опубликованное(ые) в журнале «Вестник государственной регистрации»</b><br /><br />" + data.corr_content + "<br />";
        }

        if (data.orig_id.length == 32) {
            res += "<table style=\"width:100%;\"><tr><td  class=\"minicaption\"><br />Текст корректируемого сообщения</td></tr></table><table style=\"width:100%;\"><tr><td align=\"right\">Дата публикации сообщения: " + data.orig_dt + "<br />" + data.orig_nomera + "</td></tr></table><span style=\"font-weight:bold\">" + data.orig_type_name + "</span><br />" + data.orig_region + "<br /><br />" + data.orig_content + "<br />";
        }

        res += "<br /><div class=\"data_comment limitation\"><hr />ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.</div>";
        return res;
    }

})();

$(document).ready(function () {
    vestnik_init();
    vs_save_search_params();
    vs_search(1);

    $('#btn_find').on("click", function (e) {
        e.preventDefault();
        vs_save_search_params();
        vs_search(1);
    });

});

