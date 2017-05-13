/// <reference path="../jquery-1.10.2.intellisense.js" />


(function () {

    var bcSO = {};
    var iss = ISS;
    var src = new Array();
    var mes_types = null;

    //инициализация календаря
    var init_bc_calendar = function () {
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

    var init_bc_selectors = function () {
        src[0] = new Array(0, "Все");
        src[1] = new Array(1, "ЕФРСБ");
        src[2] = new Array(34, "Коммерсантъ");
        src[3] = new Array(38, "Российская газета");

        var $source_container = $('#td_sselect');

        for (var i = 0; i < src.length; i++) {
            var $radio_div = $('<div class="radio">');
            var $src_label = $('<label>');
            var $src_input = $('<input type="radio" name="sselect">');
            if (i == 0) {
                $src_input.prop("checked", true);
            }
            $src_input.val(src[i][0]);
            $src_label.append($src_input);
            $src_label.append(src[i][1]);
            $radio_div.append($src_label);
            $source_container.append($radio_div);
        }

        $("input[name='sselect']").click(function () {
            //console.log(this.value);
            var source = this.value;
            if (source == 1) {
                load_type_selectors();
            } else {
                $('#type_selector_container').empty();
            }
        });
    }


    var load_type_selectors = function () {
        if (!mes_types) {
            showClock();
            bc_save_search_params();
            var url = "/Bancruptcy/GetMessageTypesAsync";
            var params = "dfrom=" + bcSO.dfrom + "&dto=" + bcSO.dto + "&kw=" + bcSO.kw + "&src=" + bcSO.src + "&type=" + bcSO.type + "&iss=" + bcSO.iss + "&page=" + bcSO.page + "&isCompany=" + bcSO.iscompany + "&mode=" + bcSO.mode;
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
        else {
            show_type_selectors();
        }
    }

    var show_type_selectors = function () {
        var $div = $('<div class="filter_block">');
        $div.append('<h4>Тип сообщения</h4>');
        var $source_container = $('<div class="form-group">');
        for (var i = 0; i < mes_types.length; i++) {
            var $radio_div = $('<div class="checkbox">');
            var $src_label = $('<label>');
            var $src_input = $('<input type="checkbox" name="tselect">');
            $src_input.val(mes_types[i].mes_type);
            $src_label.append($src_input);
            $src_label.append(mes_types[i].headers);
            $radio_div.append($src_label);
            $source_container.append($radio_div);
        }
        $div.append($source_container);
        $('#type_selector_container').append($div);
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

    

    var bc_restore_search_params = function () {
        $('#dfrom').val(bcSO.dfrom);
        $('#dto').val(bcSO.dto);
        $('#kw').val(bcSO.kw);
        set_input_value('sselect', bcSO.src);
        set_input_value('vkl', bcSO.mode);
        set_type_values(bcSO.type);
        $('#is_company').val(bcSO.iscompany);
    };

    var bc_clear = function () {
        $('#dfrom').val('');
        $('#dto').val('');
        $('#kw').val('');
        set_input_value('sselect', '0');
        $('#type_selector_container').empty();
        $("#tsdiv").remove();
    }

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }

    var bc_generate_result = function (res_arr, page, total) {
        bc_restore_search_params();
        var th_str = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">" +
            "<td class=\"table_caption\" style=\"width:65px;\"><input type=\"checkbox\" name=\"ch_bankrot_selector\" id=\"ch_bankrot_selector\"></td>" +
            "<td class=\"table_caption\" align=\"center\" style=\"width:80px;\">Дата</td>" +
            "<td class=\"table_caption\" style=\"width:100px;\">Данные</td>" +
            "<td class=\"table_caption\">Вид документа (информации)</td></tr>";

        var counter = 0;
        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
            counter++;
            var headers = '';
            if (res_arr[i].type_name == null || res_arr[i].type_name == '') {
                headers = getval(res_arr[i].headers);
            }
            else {
                headers = getval(res_arr[i].type_name);
            }
            th_str += "<tr><td class=\"table_item_left\" align=\"left\" style=\"width:20px;\"><input type=\"checkbox\" name=\"ch_bankrot\" val=\"" + getval(res_arr[i].tab) + "\" value=\"" + getval(res_arr[i].orig_id) + "\"></input></td>" +
                "<td class=\"table_item_left\" align=\"left\" nowrap=\"yes\" style=\"width:80px;white-space:nowrap;\">" + getval(res_arr[i].reg_date) + "</td>" +
                "<td class=\"table_item_left\" align=\"center\">" + getval(res_arr[i].source_name) + "</td>" +
                "<td class=\"table_item_left\" align=\"left\" style=\"width:100%\" id=\"tn" + (i + 1) + "\"><a href=\"#\" style=\"cursor:pointer;\" onclick=\"ShowBankrotUni('" + getval(res_arr[i].orig_id) + "'," + getval(res_arr[i].tab) + ");\">" + headers + "...</a></td></tr>";
        }
        th_str += "</table>";
        $('#t_content').html(th_str);

        //bcGenPages(total, page, step);

        $('#pager').html(get_paging(page, total));

        bc_get_date();
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

    var bc_get_date = function () {
        var url = "/Bancruptcy/GetMessageDatesAsync";
        var params = "dfrom=" + bcSO.dfrom + "&dto=" + bcSO.dto + "&kw=" + bcSO.kw + "&src=" + bcSO.src + "&type=" + bcSO.type + "&iss=" + bcSO.iss + "&page=" + bcSO.page + "&isCompany=" + bcSO.iscompany + "&mode=" + bcSO.mode;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                if (data) {
                    var res = $.parseJSON(data);
                    var d1 = (bcSO.dfrom != "") ? bcSO.dfrom : format_date(res.results[0].mindate);
                    var d2 = (bcSO.dto != "") ? bcSO.dto : format_date(res.results[0].maxdate);
                    $('#dfrom').val(d1);
                    $('#dto').val(d2);
                }
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
            html += '<td onclick="bc_save_search_params(); bc_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="bc_save_search_params(); bc_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="bc_save_search_params(); bc_search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };


    var GetLinks = function () {
        $('#link_details').text('');
        var links = "<br/><div class=\"data_comment\">ВНИМАНИЕ: Представленные сведения являются результатом обработки данных первоисточника и отображаются исключительно при наличии в тексте сообщения данных (ОГРН и ИНН) участника процесса. В случае отсутствия ОГРН и/или ИНН участника процесса, информация по делу в данном разделе отсутствует. Дополнительную информацию можно получить в разделе <a href=\"/t/dbsearch/dbsearchru/news/?n=3\" target=\"_blank\">СООБЩЕНИЯ/Сообщения о банкротстве</a>.</div>";
        $('#link_details').html(links);
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


    var loadBankrotSelected = function (ids) {
        if (!ids || ids == '') {
            return;
        }
        showClock()
        var url = "/Bancruptcy/GetMessagesSelected";
        var params = "ids=" + ids + "&ticker=" + iss;
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

    var showBankrotSelected = function (all) {
        
        if (all) {
            var ids_ar = [];
            $.post('/Bancruptcy/GetAllIds', bcSO.toString(), function (data) {
                var results = data.results;
                for (var i = 0; i < results.length; i++) {
                    ids_ar.push(results[i].orig_id+":"+results[i].tab);
                }
                loadBankrotSelected(ids_ar.join(","));
            },"json");
        } else {
            var ids = '';
            $("input:checkbox[name='ch_bankrot']" + (all ? "" : ":checked")).each(function () {
                ids += this.value + ":" + $(this).attr("val") + ",";
            });
            loadBankrotSelected(ids);
        }
        
    }

    window.bancruptcy_init = function () {
        init_bc_calendar();
        init_bc_selectors();

        $('#btn_find').click(function () {
            bc_save_search_params();
            bc_search(1);
        });

        $('input[name="vkl"]').on("change", function () {
            bc_clear();
            bc_save_search_params();
            bc_search(1);
            bc_get_date();
        });

        bc_save_search_params();
        bc_search(1);
        bc_get_date();

        set_print_function(function () {
            var print_all = true;
            $("input[name='ch_bankrot']").each(function () {
                if ($(this).prop('checked')) {
                    print_all = false;
                    return false;
                }
            });
            showBankrotSelected(print_all);
        });
    }

    

    bcSO.toString = function () {
        return "dfrom=" + bcSO.dfrom + "&dto=" + bcSO.dto + "&kw=" + bcSO.kw + "&src=" + bcSO.src + "&type=" + bcSO.type + "&iss=" + bcSO.iss + "&page=" + bcSO.page + "&isCompany=" + bcSO.iscompany + "&mode=" + bcSO.mode;
    }

    window.bc_search = function(page) {
        showClock();
        bcSO.page = page;
        var url = "/Bancruptcy/BancruptcySearchAsync";
        var params = "dfrom=" + bcSO.dfrom + "&dto=" + bcSO.dto + "&kw=" + bcSO.kw + "&src=" + bcSO.src + "&type=" + bcSO.type + "&iss=" + bcSO.iss + "&page=" + bcSO.page + "&isCompany=" + bcSO.iscompany + "&mode=" + bcSO.mode;
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
                            bc_generate_result(res_arr, page, total);
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

    window.ShowBankrotUni = function(ids, src) {
        showClock();
        var kw = "";
        iss = (!iss) ? $("#iss").val() : iss;
        if (getObj("kw")) {
            kw = $("#kw").val();
        }

        var url = "/Bancruptcy/GetMessage";
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

    }

    window.bc_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        var dto = $('#dto').val();

        bcSO.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        bcSO.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        bcSO.kw = $('#kw').val();
        bcSO.src = $("input[name='sselect']:checked").val();
        bcSO.type = get_type_values();
        bcSO.iss = iss;
        bcSO.iscompany = $('#is_company').val();
        bcSO.mode = $('input[name="vkl"]:checked').val();
    };

})();

$(document).ready(function () {
    bancruptcy_init();
});