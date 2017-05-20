(function () {
    var SO = {};
    var iss = ISS;
    var mes_types = null;

    var init_fr_calendar = function () {
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

    var init_fr_selectors = function () {
        load_type_selectors();
    }

    var load_type_selectors = function () {
        showClock();
        fr_save_search_params();
        var url = "/Fedresurs/GetMessageTypesAsync";
        var params = "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&iss=" + SO.iss + "&page=" + SO.page + "&isCompany=" + SO.iscompany;
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

    window.fr_search = function (page) {
        showClock();
        SO.page = page;
        var url = "/Fedresurs/FedresursSearchAsync";
        var params = "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&iss=" + SO.iss + "&page=" + SO.page + "&isCompany=" + SO.iscompany;
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
                            fr_generate_result(res_arr, page, total);
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

    var fr_generate_result = function (res_arr, page, total) {
        fr_restore_search_params();
        var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
            "<td class=\"table_caption\" style=\"width:65px;\"><input type=\"checkbox\" name=\"ch_message_selector\" id=\"ch_message_selector\"></td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата</td>" +
            "<td class=\"table_caption\">Наименование общества</td>" +
            "<td class=\"table_caption\">Вид документа (информации)</td></tr>";

        var counter = 0;
        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
            var company_name = "";
            if (res_arr[i].companies != "") {
                var arr = JSON.parse(res_arr[i].companies);
                for (var j = 0; j < arr.companies.length; j++) {
                    var del = ", ";
                    if (j == (arr.companies.length - 1))
                        del = "";
                    company_name = company_name + arr.companies[j].cname + del;
                }
            }
            counter++;
            th_str += "<tr><td class=\"table_item_left\" align=\"left\" style=\"width:20px;\"><input type=\"checkbox\" name=\"ch_message\" val=\"" + getval(res_arr[i].type_id) + "\" value=\"" + getval(res_arr[i].id) + "\"></input></td>" +
                "<td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:80px;white-space:nowrap;\">" + getval(res_arr[i].show_pub_date) + "</td>" +
                "<td class=\"table_item_left\" align=\"left\" style=\"width:50%\">" + getval(company_name) + "</a></td>" +
                "<td class=\"table_item_left\" align=\"left\" style=\"width:50%\"><a id=\"msg_" + (i + 1) + " name=" + getval(res_arr[i].id) + " href=\"#\" style=\"cursor:pointer;\" onclick=\"ShowFEvent('" + getval(res_arr[i].id) + "');\">" + getval(res_arr[i].type_name) + "</a></td></tr>";
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
            $("#ch_message_selector").on('click', function () {
                var is_checked = $(this).prop('checked')
                $("input[name='ch_message']").prop('checked', is_checked);
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
            html += '<td onclick="fr_save_search_params(); fr_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="fr_save_search_params(); fr_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="fr_save_search_params(); fr_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var GetLinks = function () {
        $('#link_details').text('');
        var links = "<br/><div class=\"data_comment\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и отображаются исключительно при наличии в тексте сообщения данных (ОГРН и ИНН) юридического лица. В случае отсутствия ОГРН и/или ИНН , информация в данном разделе не отображается.</div>";
        $('#link_details').html(links);
    }

    SO.toString = function () {
        return "dfrom=" + SO.dfrom + "&dto=" + SO.dto + "&kw=" + SO.kw + "&type=" + SO.type + "&iss=" + SO.iss + "&page=" + SO.page + "&isCompany=" + SO.iscompany;
    }

    window.fr_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();

        SO.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        SO.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        SO.kw = $('#kw').val();
        SO.type = get_type_values();
        SO.iss = iss;
        SO.iscompany = $('#is_company').val();
    };

    window.fr_restore_search_params = function () {
        $('#dfrom').val(SO.dfrom);
        $('#dto').val(SO.dto);
        $('#kw').val(SO.kw);
        set_type_values(SO.type);
        $('#is_company').val(SO.iscompany);
    };

    window.fr_init = function () {
        fr_save_search_params();
        init_fr_calendar();
        init_fr_selectors();

        fr_save_search_params();
        fr_search(1);

        set_print_function(function () {
            var print_all = true;
            $("input[name='ch_message']").each(function () {
                if ($(this).prop('checked')) {
                    print_all = false;
                }
            });
            showFEventSelected(print_all);
        });
    }

    window.ShowFEvent = function (id) {
        showClock();
        var url = "/Fedresurs/GetMessage";
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
        res += "<p class=\"minicaption\">СВЕДЕНИЯ О ФАКТАХ ДЕЯТЕЛЬНОСТИ ЮРИДИЧЕСКОГО ЛИЦА</p><p>Источник данных: Единый федеральный реестр сведений о фактах деятельности юридических лиц.</p><br /><div class=\"minicaption\" style=\"float:left;\">" + data.CompanyName + "</div><br style=\"clear: left;\" /><hr /><table style=\"width:100%;\"><tr><td align=\"right\">Дата публикации сообщения: " + data.ShowPubDate + "</td></tr></table><br />";
        /*
        res += "<span style=\"font-weight:bold\">" + data.TypeName + "</span><br />";
        res += "<table style=\"width:100%\"><tr><td>№ сообщения</td><td>" + data.MessNum + "</td></tr><tr><td>Юридическое лицо </td><td>" + data.CompanyName + "</td></tr><tr><td>Юридический адрес (по данным ЕГРЮЛ) </td><td>" + data.Address + "</td></tr><tr><td>ИНН</td><td>" + data.INN + "</td></tr><tr><td>ОГРН/ОГРНИП</td><td>" + data.OGRN + "</td></tr>";
        if (data.MessValues != null)
        {
            for (var i = 0, i_max = data.MessValues.length; i < i_max; i++) {
                res += "<tr><td>" + data.MessValues[i].name + "</td><td>" + data.MessValues[i].value + "</td></tr>"
            }
        }
        res += "</table><br/>" + data.Contents + "<hr/>";
        */
        res += data.HtmlTable + "<hr/>";
        res += "<div class=\"data_comment limitation\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника. В связи с особенностями функционирования и обновления, указанного источника информации АО «СКРИН» не может гарантировать полную актуальность и достоверность данных.</div>";
        return res; 
    }

    var loadFEventSelected = function () {
        var ids = '';
        $('input[name="ch_message"]:checkbox:checked').each(function () {
            ids += this.value + ",";
        });
        if (!ids || ids == '') {
            return;
        }
        showClock()
        var url = "/Fedresurs/GetMessagesSelected";
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

    var showFEventSelected = function (print_all) {
        if (print_all) {
            $("input[name='ch_message_selector']").prop('checked', 'checked');
            $("input[name='ch_message']").prop('checked', 'checked');
        }
        loadFEventSelected();
    };

})();

$(document).ready(function () {
    fr_init();
    fr_save_search_params();
    fr_search(1);

    $('#btn_find').on("click", function (e) {
        e.preventDefault();
        fr_save_search_params();
        fr_search(1);
    });

});