/// <reference path="../jquery-1.10.2.intellisense.js" />

if (!String.prototype.trim) {
    String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
}
String.prototype.removelastcomma = function () { return this.replace(/\,$/g, ''); };

String.prototype.right = function (count) { return this.slice(-(count)); };

(function () {
    var YRANGE = "2011:'+new Date().getFullYear()+'";
    var tree_active;
    var tree_table;
    var src;
    var SO = {};

    window.gz_init = function () {
        init_calendar();
        load_exec_status();
        init_radio();

        $('#contragent').bind('blur', function (event) { check_count(this); });
        $('#product').bind('blur', function (event) { check_count(this); });
        search(0);

        $("#tab_content").css("min-width", "1300px");
    };

    var init_calendar = function () {

        var dates = $("#dFrom, #dTo").datepicker({
            changeMonth: true,
            showButtonPanel: true,
            changeYear: true,
            yearRange: YRANGE,
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

    var load_exec_status = function () {
        $.get("/Zakupki/GetStatuses", function (data) {

            var $container = $("#status_block");
            for (var i = 0; i < data.length; i++) {
                var $label = $('<label class="parent_label">');
                var $cbx = $('<input type="checkbox" class="parent-status-checkbox">');
                $cbx.attr("id", "st_par_" + i);
                $label.append($cbx);
                $label.append(data[i].name);

                var $check_box = $('<div class="checkbox">');
                $check_box.append($label);
                $container.append($check_box);

                var statuses = data[i].statuses;
                var $sub_container = $('<div class="sub-form-group">')

                for (var j = 0; j < statuses.length; j++) {
                    $label = $('<label>');
                    $cbx = $('<input type="checkbox" name="cbx_status">');
                    $cbx.attr("value", statuses[j].id);
                    $cbx.attr("parent-id", "st_par_" + i);
                    $label.append($cbx);
                    $label.append(statuses[j].name);

                    $check_box = $('<div class="checkbox">');
                    $check_box.append($label);
                    $sub_container.append($check_box);
                }

                $container.append($sub_container);
            }

            $(".parent-status-checkbox").click(function () {
                var $cbx = $(this);
                var parent_id = $cbx.attr("id");
                if ($cbx.prop("checked")) {
                    $('[parent-id=' + parent_id + ']').prop("checked", true);
                } else {
                    $('[parent-id=' + parent_id + ']').prop("checked", false);
                }
            })

        }, "json");
    }

    var init_radio = function () {
        $('#ch_contr_name').click(function () { radio_switcher(this, true) });
        $('#ch_contr_code').click(function () { radio_switcher(this, true) });
        $('#ch_pr_name').click(function () { radio_switcher(this, true) });
        $('#ch_pr_code').click(function () { radio_switcher(this, true) });
    }

    var check_count = function (elem) {
        var this_id = elem.id;
        var input_text = $(elem).val();
        var is_sel = ($("input:radio[name=ch_" + this_id + "]:checked").val() == 0) ? 1 : 0;
        if (is_sel) {
            $('#' + this_id + '_val').val(input_text);
        }
    };

    var clear_tree = function (input_id, istree) {
        $('#' + input_id).val('');
        if (istree) {
            $('#' + input_id + '_val').val('');
            $('#' + input_id + '_excl').val(0);
            switch (input_id) {
                case "product":
                    radio_switcher(getObj('ch_pr_name'), false);
                    check_count(getObj(input_id));
                    //$('#ch_pr_name').prop('checked', true);
                    break;
                case "contragent":
                    radio_switcher(getObj('ch_contr_name'), false);
                    check_count(getObj(input_id));
                    //$('#ch_sup_name').prop('checked', true);
                    break;
            }
        }
    }

    var radio_switcher = function (el, need_clear) {
        //console.log(el);
        switch (el.id) {
            case "ch_pr_name":
                el.checked = true;
                $('#product').unbind('click').prop('readonly', false);
                if (need_clear) {
                    clear_tree("product", 1);
                }
                break;
            case "ch_pr_code":
                el.checked = true;
                $('#product').click(function (event) { show_tree_selector(event, 17, 1, 0); });
                $('#product').val('').prop('readonly', true);
                break;
            case "ch_contr_name":
                el.checked = true;
                $('#contragent').unbind('click').prop('readonly', false);
                if (need_clear) {
                    clear_tree("contragent", 1);
                }
                break;
            case "ch_contr_code":
                el.checked = true;
                $('#contragent').click(function (event) { show_tree_selector(event, 1, 1, 0); });
                $('#contragent').val('').prop('readonly', true);
                break;
        }
    }

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#comp').css("border-radius", "4px");
        }
    };

    var hidepopups = function () {
        close_bones();

        if (getObj(tree_active + "_window")) {
            getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window"));
            $("#" + tree_active).css({ 'background-color': '#FFFFFF' });
        }
        close_dialog();
    }

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

    var save_search_params = function () {



        SO.dfrom = $('#dFrom').val();
        SO.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(SO.dfrom) ? SO.dfrom : "";
        SO.dto = $('#dTo').val();
        SO.dto = /\d{2}\.\d{2}\.\d{4}/.test(SO.dto) ? SO.dto : "";

        SO.Is44 = $('#ch44').prop('checked');
        SO.Is94 = $('#ch94').prop('checked');
        SO.Is223 = $('#ch223').prop('checked');



        SO.IsSup = $('#rS').prop('checked');
        SO.IsCus = $('#rC').prop('checked');
        SO.IsPar = $('#rP').prop('checked');

        var stat_arr = [];
        $("input:checkbox[name='cbx_status']:checked").each(function () {
            stat_arr.push(this.value);
        })

        SO.status = stat_arr.join(",");

        SO.sfrom = $('#sFrom').val();
        SO.sfrom = /\d+/.test(SO.sfrom) ? SO.sfrom : "";
        SO.sto = $('#sTo').val();
        SO.sto = /\d+/.test(SO.sto) ? SO.sto : "";
        SO.is_product = ($("input:radio[name=ch_product]:checked").val() == 0) ? true : false;
        SO.product = $('#product_val').val();
        SO.product_excl = (String($("#product_excl").val()).length == 0) ? 0 : $("#product_excl").val();
        SO.is_contrname = ($("input:radio[name=ch_contragent]:checked").val() == 0) ? true : false;
        SO.contragent = $('#contragent_val').val();
        SO.contragent_excl = (String($("#contragent_excl").val()).length == 0) ? 0 : $("#contragent_excl").val();

    };

    var restore_search_params = function () {
        $('#dFrom').val(SO.dfrom);
        $('#dTo').val(SO.dto);
        $('#ch44').prop('checked', SO.Is44);
        $('#ch94').prop('checked', SO.Is94);
        $('#ch223').prop('checked', SO.Is223);
        $('#rS').prop('checked', SO.IsSup);
        $('#rC').prop('checked', SO.IsCus);
        $('#rP').prop('checked', SO.IsPar);
        $('#reg_num', SO.reg_num).val();



        $('#zak_status').val(SO.status);

        var stat_arr = SO.status.split(",");

        $("input:checkbox[name='cbx_status']").each(function () {
            $(this).prop("checked", false);
            for (var i = 0; i < stat_arr.length; i++) {
                if (stat_arr[i] == this.value) {
                    $(this).prop("checked", true);
                    break;
                }
            }
        });

        $('#sFrom').val(SO.sfrom);
        $('#sTo').val(SO.sto);
        if (SO.is_product) {
            $('#product').val(SO.product);
            check_count(getObj('product'));
        } else {
            $('#product_val').val(SO.product);
        }
        $("#product_excl").val(SO.product_excl);

        if (SO.is_contrname) {
            $('#contragent').val(SO.contragent);
            check_count(getObj('contragent'));
        } else {
            $('#contragent_val').val(SO.contragent);
        }
        $("#contragent_excl").val(SO.contragent_excl);

    }

    var generate_result = function (res_arr, page, total) {   
 
        if (page > 0) {
            restore_search_params();
            //        $('#more_finds').remove();
        }
        $('#t_content').remove();
        var th_str = '<table width="100%" cellspacing="0" cellpadding="2" border="0" id="t_content">' +
                        '<tr>' +
                            '<td class="table_caption">Дата публикации</td>' +
                            '<td class="table_caption">Номер сведений<br/> о закупке / контракте</td>' +
                            '<td class="table_caption">Способ определения<br/>поставщика</td>' +
                            '<td class="table_caption">Цена (в&nbsp;рублях)</td>' +
                            '<td class="table_caption">Статус</td>' +
                            '<td class="table_caption">Заказчик</td>' +
                            '<td class="table_caption">Участники/Поставщики</td>' +
                        '</tr>' +
                        '<tr>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                            '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer type="block" width="1px" height="1px"></spacer></div></td>' +
                        '</tr>' +
                     '</table>';
        $('#search_count').after(th_str);
        //var t_body = $('<tbody></tbody>');
        var _row_bg = ['#ffffff', '#f0f0f0'];
        var _row_sw = 1;
   
        for (var i = 0, i_max = res_arr.length; i < i_max; i++) {
            $('#t_content').append("<tr class=\"rows_divider\"><td colspan=\"7\"></tr>");

            _row_sw = 1 - _row_sw;

            var has_notification = res_arr[i].notification_id != "0";
            var contrs = res_arr[i].contr_data_json;
            var has_contract = contrs.length > 0;

            var sourse_fz;
            switch (res_arr[i].source) {
                case "1":
                    //                source = 94;
                    sourse_fz = 'ФЗ&nbsp;94';
                    break;
                case "2":
                    //                source = 223;
                    sourse_fz = 'ФЗ&nbsp;223';
                    break;
                case "3":
                    //                source = 44;
                    sourse_fz = 'ФЗ&nbsp;44';
                    break;
            }

            var el_line = '';
            var bg = '';
            if (has_notification) {
                bg = 'border-top:1px solid #e0e0e0';
                el_line = $('<tr style="background-color:#fafafa; vertical-align:top;"></tr>');
                $('<td style="width:78px;text-align:center;' + bg + '">' + getval(res_arr[i].not_publish_date) + '</td>').appendTo(el_line);
                $('<td style="width:148px;text-align:left;' + bg + '"> Закупка - ' + sourse_fz + '<br/>' + get_not_href(res_arr[i]) + '</td>').appendTo(el_line);
                $('<td style="width:138px;text-align:center;' + bg + '">' + getval(res_arr[i].not_type) + '</td>').appendTo(el_line);
                $('<td style="width:108px;text-align:center;' + bg + '">' + formatNumber(getval(res_arr[i].st_not_sum)) + '</td>').appendTo(el_line);
                $('<td style="width:78px;"text-align:center;' + bg + '">' + getval(res_arr[i].not_status_name) + '</td>').appendTo(el_line);
                $('<td style="width:158px;text-align:left;' + bg + '">' + show_issuer(getval(res_arr[i].not_cust_json)) + '</td>').appendTo(el_line);
                $('<td style="width:158px;text-align:left;' + bg + '">' + show_issuer_list(getval(res_arr[i].part_json)) + '</td>').appendTo(el_line);
                $('#t_content').append(el_line);
                if (!has_contract) {
                    el_line = $('<tr style="background-color:#fafafa; vertical-align:top;"></tr>');
                    $('<td colspan="7" style="width:100%;text-align:left;"><b>Предмет заказа:</b><br/>' + WordSelectorText(getval(res_arr[i].not_product.replace(/\|/g, '<br/>')), KEYWORDS) + '</td>').appendTo(el_line);
                    $('#t_content').append(el_line);
                } else {
                    el_line = $('<tr style="background-color:#fafafa; vertical-align:top;"></tr>');
                    $('<td colspan="7" style="width:100%;text-align:center;"><b>Изменение начальной цены:</b> <span style="font-weight:bold">' + formatNumber(getval(res_arr[i].dif_sum)) + ' (' + formatNumber(getval(res_arr[i].dif_per)) + '%)' + '</span></td').appendTo(el_line);
                    $('#t_content').append(el_line);
                }
            }
            bg = '';
            for (var j = 0; j < contrs.length; j++) {
                if (!has_notification && j == 0) {
                    bg = 'border-top:1px solid #e0e0e0';
                }
                el_line = $('<tr style="background-color:' + _row_bg[_row_sw] + '; vertical-align:top;"></tr>');
                $('<td style="width:78px;text-align:center;' + bg + '">' + getval(contrs[j].pub_date) + '</td>').appendTo(el_line);
                $('<td style="width:148px;text-align:left;' + bg + '"> Контракт - ' + sourse_fz + "<br/>" + get_contr_href(contrs[j]) + '</td>').appendTo(el_line);
                $('<td style="width:138px;text-align:center;' + bg + '">' + getval(contrs[j].placing) + '</td>').appendTo(el_line);
                $('<td style="width:108px;text-align:center;' + bg + '">' + formatNumber(getval(contrs[j].sum)) + '</td>').appendTo(el_line);
                $('<td style="width:78px;text-align:center;' + bg + '">' + getval(contrs[j].contr_stage) + '</td>').appendTo(el_line);
                $('<td style="width:158px;text-align:left;' + bg + '">' + show_issuer(getval(contrs[j].customer)) + '</td>').appendTo(el_line);
                $('<td style="width:158px;text-align:left;' + bg + '">' + show_issuer_list(getval(contrs[j].supliers)) + '</td>').appendTo(el_line);
                $('#t_content').append(el_line);
                if (contrs[j].product_list != "") {
                    el_line = $('<tr style="background-color:' + _row_bg[_row_sw] + '; vertical-align:top;"></tr>');
                    $('<td colspan="7" style="width:100%;text-align:left;"><b>Предмет контракта / договора:</b><br/>' + WordSelectorText(getval(contrs[j].product_list.replace(/\|/g, "<br/>")), KEYWORDS) + '</td>').appendTo(el_line);
                    $('#t_content').append(el_line);
                }
            }
        }
        //WordSelector('#t_content', KEYWORDS);
        $('#pager').html(get_paging(page, total));
    };

    var get_paging = function (page, total) {      
        var page_count = Math.ceil(total / 100);

        if (page_count <= 1) {
            return "";
        }

        var html = "";
        var StartPage = 1;
        if (page_count >= 8) {
            StartPage = ((page - 3 > 0) ? ((page_count - page < 3) ? page_count + (page_count - page) - 8 : page - 3) : 1);
        }

        if (page > 1 && page_count > 7) {
            html += '<td onclick="search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1 && page_count > 7) {
            html += '<td onclick="search(' + page_count + ');">&raquo;</td>';
        }
        if (i == page_count + 1 && page_count > 7) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var get_contr_href = function (contr_data) {
        return '<a href="#" onclick="ShowContract(\'' + contr_data.id + '\', \'' + contr_data.source + '\');return false;">' + WordSelectorText(getval(contr_data.reg_num), KEYWORDS) + '</a>';
    };

    var get_not_href = function (not_data) {
        var lot_num = Number(not_data.lot_num);
        var lot_num_text = lot_num == 0 ? "" : "Лот № " + not_data.lot_num;
        lot_num = lot_num == 0 ? 1 : lot_num;
        return '<a href="#" onclick="ShowPurchase(\'' + not_data.pur_num + '\', \'' + lot_num + '\');return false;">' + WordSelectorText(getval(not_data.pur_num), KEYWORDS) + "<br/>" + lot_num_text + '</a>';
    };

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    };

    //Отображает организацию
    var show_issuer = function (issuer_side) {
        var cell_content = "";
        //временная затычка
        if (issuer_side.ticker == undefined && issuer_side.tiker != undefined)
            issuer_side.ticker = issuer_side.tiker;

        if (issuer_side.ticker != "") {
            if (issuer_side.type == "P") {
                cell_content = '<a style="float:left;" href="/ip/' + issuer_side.ticker + '" target="_blank">' + WordSelectorText(issuer_side.name, KEYWORDS) + '</a>';
            } else {
                cell_content = '<a style="float:left;" href="/issuers/' + issuer_side.ticker + '" target="_blank">' + WordSelectorText(issuer_side.name, KEYWORDS) + '</a>';
            }
        } else {
            cell_content = issuer_side.name;
        }
        return cell_content;
    };

    var show_issuer_list = function (i_list) {
        var ret = "";
        for (var i = 0; i < i_list.length; i++) {
            ret += show_issuer(i_list[i]);
            if (i < i_list.length - 1)
                ret += "<div class='issuer_devider'/>";
        }
        return ret;
    };


    // Выдлеляет ключевые слова в заданном тексте заданного идентификатора
    var WordSelector = function (id, keywords) {
        //Нужно выделить
        if (keywords.length > 0) {
            var html_text = $(id).html();
            if (html_text.length > 0) {
                for (var i = 0; i < keywords.length; i++) {
                    if (keywords[i].length > 2) {
                        html_text = html_text.replace(new RegExp('(' + keywords[i] + ')', 'gi'), "<span class='search_text'>$1</span>");
                    }
                    var test = html_text;
                }
                $(id).html(html_text);
            }
        }
    };
    // Выдлеляет ключевые слова в заданном тексте 
    var WordSelectorText = function (text, keywords) {
        //Нужно выделить
        if (keywords.length > 0) {
            if (text.length > 0) {
                for (var i = 0; i < keywords.length; i++) {
                    if (keywords[i].length > 2) {
                        text = text.replace(new RegExp('(^|[^A-Za-z0-9_А-Яа-я])(' + keywords[i] + ')($|[^A-Za-z0-9_А-Яа-я])', 'gi'), "$1<span class='search_text'>$2</span>$3");
                    }
                }
            }
        }
        return text;
    };

    var formatNumber = function (number) {
        if (isNaN(number)) {
            return number;
        }
        var number = Number(number.replace(',', '.')).toFixed(2) + '';
        var x = number.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + '&nbsp;' + '$2');
        }
        return x1 + x2;
    };


    window.ShowContract = function (id, SOURCE) {

        showClock();
        var sHtm = '<div id="contract_content">' +
                '<div class="minicaption">Единый Реестр Государственных и Муниципальных Контрактов</div>' +
                '<span>Источник данных: Федеральное казначейство</span>' +
               // '<p class="bluecaption">' + NM + '</p>' +
                '<hr/>' +
                '<div style="margin-top: 15px; margin-bottom: 0px;">' +
                    '<p id="contract_header" class="minicaption"></p>' +
                '</div>' +
                '<div id="contract_blocks"></div>' +
            '</div>';
        $('#contract_container').html(sHtm);
        if (SOURCE == 1)
            load_data44(id, function () {
                show_dialog({ "content": $("#contract_container").html(), "extra_style": "width:990px;", is_print: true });
                hideClock();
                $('#contract_container').empty();
            });
        if (SOURCE == 2)
            load_data223(id, function () {
                show_dialog({ "content": $("#contract_container").html(), "extra_style": "width:990px;", is_print: true });
                hideClock();
                $('#contract_container').empty();
            });
    }

    window.ShowPurchase = function (pur_num, lot_num) {
        var sHtm = '<div id="contract_content">' +
                '<div class="minicaption">Единый Реестр Государственных и Муниципальных Контрактов</div>' +
                '<span>Источник данных: Федеральное казначейство</span>' +
             //   '<p class="bluecaption">' + NM + '</p>' +
                '<hr/>' +
                '<div style="margin-top: 15px; margin-bottom: 0px;">' +
                    '<h1>Закупка № ' + pur_num + '</h1>' +
                    '<h2 class="minicaption">Лот № ' + lot_num + '</h2>' +
                '</div>' +
                '<div id="purchase_blocks"></div>' +
            '</div>';
        $('#contract_container').html(sHtm);
        load_data(pur_num, lot_num, function () {
            show_dialog({ "content": $("#contract_container").html(), "extra_style": "width:990px;", is_print: true });
            hideClock();
            $('#contract_container').empty();
        });
    }


    window.search = function (page) {
        save_search_params();
        SO.page = page - 1;
        SO.inn = INN;
        showClock();
        $.post("/Zakupki/Search/", SO, function (data) {
            hideClock();
            var keyword;
            var i_k = 0;
            KEYWORDS = [];
            do {
                keyword = data["keyword[" + i_k + "]"];
                if (keyword) {
                    KEYWORDS[i_k] = keyword;
                }
                i_k++;
            } while (keyword);
            var total = data["total_found"];
            if (total) {
                $('#search_count').html(total == 0 ? "<table class=\"notfound\" width=\"100%\"><tr><td class=\"notfound\">Нет данных соответствующих заданному условию</td></tr></table>" : "<div class=\"minicaption\">Всего найдено " + total + " сообщений.</div>");
                var res_arr = data["results"];
                if (res_arr.length > 0) {
                    generate_result(res_arr, page, total);
                } else {
                    $('#t_content').remove();
                    $('#thead2').remove();
                    $('.tline').remove();
                    $('#export_menu').remove();
                }
            } else {
                $('#search_count').show().text('Ошибка сервиса, попробуйте зайти позже.');
            }
        }, "json");
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
                $("#" + tree_active + "_excl").val(is_excl);
                $("#" + tree_active + "_val").val(retval);
            })
        } else {
            $("#" + tree_active).val("");
            $("#" + tree_active + "_excl").val(0);
            $("#" + tree_active + "_val").val("");
        }
        hidepopups();

    };


    //#region contracts

    //#region 223
    function load_data223(CONTRACT_ID, finish_fn) {
        showClock();
        var par = "contract_id=" + CONTRACT_ID;
        $.post("/Zakupki/GetCont223Data/?" + par, function (data) {
            hideClock();
            load_223_gi(data.gi);
            load_223_ci(data.ci, 1);
            load_223_ci(data.pi, 2);
            load_223_gd(data.gd);
            load_223_si(data.si);
            load_223_products(data.products);
            finish_fn();
        }, "json");
    }

    function load_223_gi(gi) {
        if (gi.reg_num != null)
            $('#contract_header').text("Контракт №" + gi.reg_num);

        var items = [
             { name: "Номер извещения", val: gi.reg_num },
             { name: "Статус договора", val: gi.status },
             { name: "Способ определения поставщика (подрядчика, исполнителя)", val: gi.place },
             { name: "Наименование закупки", val: gi.purchase_name },
             { name: "Редакция", val: gi.edition },
             { name: "Дата публикации (по местному времени)", val: gi.publish_date }
        ];
        $('#contract_blocks').append(generate_table(items, "Общая информация"));
    }

    function load_223_ci(ci, type) {
        if (ci != null) {
            var header = type == 1 ? "Информация о заказчике" : "Контактное лицо";
            var items = [
                { name: "Наименование", val: (ci.ticker != null ? "<a href='/issuers/" + ci.ticker + "\">" + ci.fullname + "</a>" : ci.fullname) },
                { name: "Сокращенное наименование", val: ci.shortname },
                { name: "ИНН", val: ci.inn },
                { name: "КПП", val: ci.kpp },
                { name: "ОГРН", val: ci.ogrn },
                { name: "Адрес места нахождения", val: ci.legal_address },
                { name: "Почтовый адрес", val: ci.post_address },
                { name: "Электронная почта", val: ci.email },
                { name: "Телефон", val: ci.phone },
                { name: "Факс", val: ci.fax }
            ];
            $('#contract_blocks').append(generate_table(items, header));
        }
    }

    function load_223_gd(ci) {
        var items = [
            { name: "Дата заключения договора", val: ci.crDate },
            { name: "Цена договора", val: ci.summa },
            { name: "Код валюты договора", val: ci.currency },
            { name: "Срок исполнения договора", val: ci.exe_date },
            { name: "Место поставки товара, выполнения работ, оказания услуг", val: ci.deliveryPlace }
        ];
        $('#contract_blocks').append(generate_table(items, "Общие данные"));
    }

    function load_223_si(si) {
        if (si != null) {
            var name = si.ticker == null ? si.name : "<a href='/" + (si.type == "L" ? "issuers" : "ip") + "/" + si.ticker + "' target='_blank'>" + si.name + "</a>";
            var items = [
                { name: "Наименование", val: name },
                { name: "ОГРН", val: si.ogrn },
                { name: "ИНН", val: si.inn },
                { name: "КПП", val: si.kpp }
            ];
            $('#contract_blocks').append(generate_table(items, "Информация о поставщике"));
        }
    }

    function load_223_products(products) {
        if (products.length > 0) {
            var ths = ["№", "Наименование товара, работ, услуг Классификация по ОКДП", "Классификация по ОКВЭД", "Единицы измерения", "Количество", "Дополнительные сведения"];
            var tds = [];
            for (var i = 0; i < products.length; i++) {
                tds[i] = [products[i].num, products[i].okdp, products[i].okved, products[i].ei, products[i].qty, products[i].additionalInfo];
            }
            generate_horizontal_table(ths, tds, "Предмет договора");
        }
    }

    //#endregion 223

    //#region 44

    function load_data44(CONTRACT_ID, finish_fn) {
        showClock();
        var par = "contract_id=" + CONTRACT_ID;
        $.post("/Zakupki/GetCont44Data/?" + par, function (data) {
            hideClock();
            load_44_gi(data.gi);
            load_44_ci(data.ci);
            load_44_gd(data.gd);
            load_bud(data.bud, 1);
            load_bud(data.ebud, 2);
            load_products(data.products);
            load_suppliers(data.supliers);
            finish_fn();
        }, "json");
    }

    function load_44_gi(gi) {
        if (gi.reg_num != null)
            $('#contract_header').text("Контракт №" + gi.reg_num);

        var items = [
            { name: "Реестровый номер контракта", val: gi.reg_num },
            { name: "Статус контракта", val: gi.status },
    //        { name: "Номер извещения об осуществлении закупки", val: (gi.notification_exists ? "<a href='purchase.asp?pur_num=" + gi.not_num + "&lot_num=" + gi.lot_num + "' target='_blank'>" + gi.not_num + "</a>" : gi.not_num) },
            { name: "Номер извещения об осуществлении закупки", val: (gi.notification_exists ? "<a href='#' onclick='ShowPurchase(\"" + gi.not_num + "\", \"" + gi.lot_num + "\")'>" + gi.not_num + "</a>" : gi.not_num) },
            { name: "Способ определения поставщика (подрядчика, исполнителя)", val: gi.place },
            { name: "Дата подведения результатов определения поставщика (подрядчика, исполнителя)", val: gi.prot_date },
            { name: "Дата публикации (по местному времени)", val: gi.publish_date },
            { name: "Реквизиты документа, подтверждающего основание заключения контракта", val: gi.doc }
        ];
        $('#contract_blocks').append(generate_table(items, "Общая информация"));
    }

    function load_44_ci(ci) {
        var items = [
            { name: "Полное наименование заказчика", val: (ci.ticker != null ? "<a href='/issuers/" + ci.ticker + "/' target='_blank'>" + ci.fullname + "</a>" : ci.fullname) },
            { name: "Сокращенное наименование заказчика", val: ci.shortname },
            { name: "Дата постановки на учет в налоговом органе", val: ci.reg_date },
            { name: "Идентификационный код заказчика", val: ci.cust_code },
            { name: "ИНН", val: ci.inn },
            { name: "КПП", val: ci.kpp },
            { name: "Источник финансирования", val: ci.finace_source },
            { name: "Наименование бюджета", val: ci.budget },
            { name: "Уровень бюджета", val: ci.bud_level },
            { name: "Вид внебюджетных средств", val: ci.exta_budget },
            { name: "Код территории мунициального образования", val: ci.budget_oktmo }
        ];
        $('#contract_blocks').append(generate_table(items, "Информация о заказчике"));
    }

    function load_44_gd(gd) {
        var items = [
            { name: "Дата заключения контракта", val: gd.sign_date },
            { name: "Номер контракта", val: gd.contract_num },
            { name: "Цена контракта", val: gd.price },
            { name: "Валюта контракта", val: gd.currency },
            { name: "Дата начала исполнения контракта", val: gd.exec_start_date },
            { name: "Дата окончания исполнения контракта", val: gd.exec_end_date },
            { name: "Размер обеспечения исполнения контракта", val: (gd.enforcement == null ? null : gd.enforcement + " в российских рублях") }
        ];
        $('#contract_blocks').append(generate_table(items, "Общие данные"));
    }


    function load_bud(bud, type) {
        if (bud.length > 0) {
            var $tabl = $('<table class="contract_table" style=" width: 50%;" cellspacing="0" cellpadding="0" border="0">"');
            $tabl.append("<tr><th>Период</th><th>Сумма</th></tr>")
            for (var i = 0; i < bud.length; i++) {
                $tabl.append('<tr><td>' + bud[i].period + '</td><td>' + bud[i].sum + '</td></tr>')
            }
            var header = type == 1 ? "За счет бюджетных средств" : "За счет внебюджетных средств";
            var block = $('<div class="contract_block">');
            block.append("<h2>" + header + "</h2>");
            block.append($tabl);
            $('#contract_blocks').append(block);
        }
    }

    function load_products(products) {
        if (products.length > 0) {
            var ths = ["Наименование объекта закупки", "Код продукции по ОКПД", "Единицы измерения", "Цена за единицу", "Количество", "Сумма"];
            var tds = [];
            for (var i = 0; i < products.length; i++) {
                tds[i] = [products[i].name, products[i].code, products[i].okei, products[i].price, products[i].quantity, products[i].sum];
            }
            generate_horizontal_table(ths, tds, "Предмет контракта");
        }
    }

    function load_suppliers(supl) {
        if (supl.length > 0) {
            var ths = ["Наименование", "Страна, код", "Адрес", "Телефон, электронная почта", "ИНН", "КПП", "Статус"];
            var tds = [];
            for (var i = 0; i < supl.length; i++) {
                var name = supl[i].ticker == null ? supl[i].name : "<a href='/" + (supl[i].s_type == "U" ? "issuers" : "ip") + "/" + supl[i].ticker + "' target='_blank'>" + supl[i].name + "</a>";
                var email = supl[i].phone == null ? "" : (supl[i].email == null ? supl[i].phone : supl[i].phone + ", " + supl[i].email);
                tds[i] = [name, supl[i].country_name + " (" + supl[i].country_code + ")", supl[i].address, email, supl[i].inn, supl[i].kpp, supl[i].status];
            }
            generate_horizontal_table(ths, tds, "Информация о поставщиках");
        }
    }

    ///#endregion


    function generate_row(header_text, val) {
        if (val == null || val == "")
            return "";
        return '<tr><th>' + header_text + '</th><td>' + val + '</td></tr>';
    }

    function generate_table(items, header) {
        var $tabl = $('<table class="contract_table vert" cellspacing="0" cellpadding="0" border="0">"');
        for (var i = 0; i < items.length; i++) {
            $tabl.append(generate_row(items[i].name, items[i].val));
        }
        var $blk = $('<div class="contract_block">');
        $blk.append('<h2>' + header + '</h2>');
        $blk.append($tabl);
        return $blk;
    }

    function generate_horizontal_table(ths, trs, header) {
        if (trs.length > 0) {
            var $tabl = $('<table class="contract_table hor" cellspacing="0" cellpadding="0" border="0">"');
            var th = "<tr>";
            for (var i = 0; i < ths.length; i++) {
                th += "<th>" + ths[i] + "</th>";
            }
            th += "</tr>";
            $tabl.append(th);
            for (var i = 0; i < trs.length; i++) {
                var tds = trs[i];
                var td = "<tr>";
                for (var j = 0; j < tds.length; j++) {
                    var val = tds[j] == null ? "" : tds[j];
                    td += "<td>" + val + "</td>";
                }
                td += "</tr>";
                $tabl.append(td);
            }
            var block = $('<div class="contract_block">');
            block.append("<h2>" + header + "</h2>");
            block.append($tabl);
            $('#contract_blocks').append(block);
        }
    }


    //#endregion contracts

    //#region purchase

    var change_toggler = function (event) {
        if (event) {
            event.preventDefault();
            event.stopPropagation();
        }
        var $el = $("#const_togler");
        if ($el) {
            if ($el.hasClass("table_link")) {
                $el.removeClass("table_link").addClass("table_unlink").text("Показать первые 3 контракта");
                $('.const_toggle').show();
            } else {
                $el.removeClass("table_unlink").addClass("table_link").text($el.attr("data-count"));
                $('.const_toggle').hide();
            }
        }
    };

    function load_data(PUR_NUM, LOT_NUM, finish_fn) {
        showClock();
        var par = "pur_num=" + PUR_NUM + "&lot_num=" + LOT_NUM;
        $.post("/Zakupki/GetShortNotData/?" + par, function (data) {
            hideClock();
            load_other_lots(data, PUR_NUM);
            load_contract_data(data);
            load_main_data(data);
            load_price_data(data);
            load_pur_data(data);
            finish_fn();
        }, "json");

    }

    function load_other_lots(data, PUR_NUM) {
        var lots = data.other_lots;
        if (lots.length > 0) {
            var $tabl = $('<table id="other_lots" style="display:none;" class="contract_table hor" cellspacing="0" cellpadding="0" border="0">"');
            $tabl.append("<tr><th>№</th><th>Наименование товара, работы, услуги</th><th>Стоимость</th></tr>");
            for (var i = 0; i < lots.length; i++) {
                //            $tabl.append("<tr><td><a href='purchase.asp?pur_num=" + PUR_NUM + "&lot_num=" + get_val(lots[i].lot_number) + "' target='_blank'>Лот&nbsp;№" + get_val(lots[i].lot_number) + "</a></td><td>" + (lots[i].lot_info) + "</td><td>" + get_val(lots[i].max_price) + "</td></tr>");
                $tabl.append('<tr><td><a href="#" onclick="ShowPurchase(\'' + PUR_NUM + '\', \'' + get_val(lots[i].lot_number) + '\');">Лот&nbsp;№' + get_val(lots[i].lot_number) + '</a></td><td>' + (lots[i].lot_info) + '</td><td>' + get_val(lots[i].max_price) + '</td></tr>');
            }
            var block = $('<div class="contract_block">');
            block.append("<div class='header_list_block'><span id='lot_sw' class='more_ico_right' onclick='showLots();'>Информация о других лотах данной закупки</span></div>");
            block.append($tabl);
            $('#purchase_blocks').append(block);
        }
    }

    function showLots() {
        if ($('#lot_sw').hasClass("more_ico_right")) {
            $('#other_lots').show();
            $('#lot_sw').removeClass("more_ico_right").addClass("less_ico_right");
        } else {
            $('#other_lots').hide();
            $('#lot_sw').removeClass("less_ico_right").addClass("more_ico_right");
        }
    }

    function load_contract_data(data) {
        var con = data.contracts;
        if (con.length > 0) {
            var $tabl = $('<table class="contract_table hor" cellspacing="0" cellpadding="0" border="0">"');
            $tabl.append("<tr><th>Дата публикации</th><th>Номер сведений о  контракте</th><th>Цена (в рублях)</th><th>Статус</th><th>Заказчик</th><th>Поставщики</th></tr>");
            for (var i = 0; i < con.length; i++) {
                var cls = i > 2 ? " class='const_toggle'" : "";
                //            $tabl.append("<tr" + cls + "><td>" + get_val(con[i].pub_date) + "</td><td>Контракт ФЗ-" + (con[i].contract_source == 3 ? "44" : "94") + "<br/><a href='contract.asp?contract_id=" + con[i].id + "&source=1' target='_blank'>" + get_val(con[i].reg_num) + "</a></td><td>" + get_val(con[i].price) + "</td><td>" + get_val(con[i].stage) + "</td><td>" + show_company(con[i].cust_name, con[i].cust_ticker) + "</td><td>" + show_supliers(con[i].supliers) + "</td></tr>");
                $tabl.append('<tr' + cls + '><td>' + get_val(con[i].pub_date) + '</td><td>Контракт ФЗ-' + (con[i].contract_source == 3 ? '44' : '94') + '<br/><a href="#" onclick="ShowContract(\'' + con[i].id + '\', 1);return false;">' + get_val(con[i].reg_num) + '</a></td><td>' + get_val(con[i].price) + '</td><td>' + get_val(con[i].stage) + '</td><td>' + show_company(con[i].cust_name, con[i].cust_ticker) + '</td><td>' + show_supliers(con[i].supliers) + '</td></tr>');
            }
            if (con.length > 3) {
                var text = "Показать все: " + con.length;
                $tabl.append('<tfoot><tr><td colspan="7" style="text-align:right; padding-right:15px;"><a id="const_togler" class="table_unlink" data-count="' + text + '" href="#">' + text + '</a></td></tr></tfoot>');
            }
            var block = $('<div class="contract_block">');
            block.append("<h2>Заключенные контракты</h2>");
            block.append($tabl);
            $('#purchase_blocks').append(block);
            if (con.length > 3) {
                change_toggler();
                $("#const_togler").click(change_toggler);
            }
        }
    }

    function show_company(name, ticker, type) {
        if (ticker == null || ticker == "")
            return name;
        type = type || 'U';
        if (type == "U")
            return "<a href='/issuers/" + ticker + "' target='_blank'>" + name + "</a>";
        return "<a href='/ip/" + ticker + "' target='_blank'>" + name + "</a>";
    }

    function show_supliers(supl) {
        var text = "";
        var s_a = supl.split(',');
        for (var i = 0; i < s_a.length; i++) {
            var sup = s_a[i].split('|');
            text += show_company(sup[0], sup[2], sup[1]);
            if (i < s_a.length - 1)
                text += "<br/>"
        }
        return text;
    }

    function load_main_data(data) {
        var $tabl = $('<table class="contract_table vert" cellspacing="0" cellpadding="0" border="0">"');
        if (!(data.not_href == null || data.not_href == "")) {
            $tabl.append('<tr><td colspan="2"><a href="' + data.not_href + '" target=\"_blank\">Гиперссылка на опубликованное извещение<a></td></tr>');
        }
        $tabl.append(generate_row("Способ определения поставщика (подрядчика, исполнителя)", data.not_type));
        $tabl.append(generate_row("Наименование  электронной площадки в информационно-телекомуникационной сети \"Интернет\"", data.etp_name));
        if (!(data.etp_url == null || data.etp_url == "")) {
            $tabl.append('<tr><th>Адрес электронной площадки в информационно-телекомуникационной сети "Интернет"</th><td><a href="' + data.etp_url + '" target=\"_blank\">' + data.etp_url + '<a></td></tr>');
        }
        if (!(data.customer == null || data.customer.name == null || data.customer.name == "")) {
            if (data.customer.ticker == null || data.customer.ticker == "") {
                $tabl.append('<tr><th>Закупку осуществляет</th><td>' + data.customer.name + '</td></tr>');
            } else {
                $tabl.append('<tr><th>Закупку осуществляет</th><td><a href="/issuers/' + data.customer.ticker + '/" target=\"_blank\">' + data.customer.name + '<a></td></tr>');
            }
        }
        $tabl.append(generate_row("Наименование объекта закупки", data.purchaseObjectInfo));
        $tabl.append(generate_row("Этап закупки", data.status_name));

        var block = $('<div class="contract_block">');
        block.append("<h2>Общая информация о закупке</h2>");
        block.append($tabl);
        $('#purchase_blocks').append(block);
    }



    function load_price_data(data) {
        var $tabl = $('<table class="contract_table vert" cellspacing="0" cellpadding="0" border="0">"');
        $tabl.append(generate_row("Начальная (максимальная) цена контракта", data.maxPrice));
        $tabl.append(generate_row("Валюта", data.currency));
        $tabl.append(generate_row("Источник финансирования", data.financeSource));

        var block = $('<div class="contract_block">');
        block.append("<h2>Начальная (максимальная) цена контракта</h2>");
        block.append($tabl);
        $('#purchase_blocks').append(block);
    }

    function load_pur_data(data) {
        var pur = data.purchases;
        if (pur.length > 0) {
            var $tabl = $('<table class="contract_table hor" cellspacing="0" cellpadding="0" border="0">"');
            $tabl.append("<tr><th>Наименование товара, работы, услуги</th><th>Код по ОКПД</th><th>Единица измерения</th><th>Количество</th><th>Цена за ед.изм.</th><th>Стоимость</th></tr>");
            for (var i = 0; i < pur.length; i++) {
                $tabl.append("<tr><td>" + get_val(pur[i].name) + "</td><td>" + get_val(pur[i].okdp) + "</td><td>" + get_val(pur[i].okei) + "</td><td>" + get_val(pur[i].quantity) + "</td><td>" + get_val(pur[i].price) + "</td><td>" + get_val(pur[i].sum) + "</td></tr>");
            }
            if (data.maxPrice != null) {
                $tabl.append("<tr><td colspan=\"6\" style=\"font-weight:bold;padding-right: 20px;text-align: right;\">Итого: " + data.maxPrice + "</td></tr>");
            }
            var block = $('<div class="contract_block">');
            block.append("<h2>Информация об объекте закупки</h2>");
            block.append($tabl);
            $('#purchase_blocks').append(block);
        }
    }



    function generate_row(header_text, val) {
        if (val == null || val == "")
            return "";
        return '<tr><th>' + header_text + '</th><td>' + val + '</td></tr>';
    }

    function get_val(json_val) {
        return json_val == null ? "" : json_val;
    }

    //#endregion purchase


})();





$(document).ready(function () {
    gz_init();
});