(function () {

    var SO={}

    var init_calendar = function () {
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
                var option = this.id == "dfrom" ? "MIN_TRADEDATE" : "MAX_TRADEDATE",
                instance = $(this).data("datepicker");
                date = $.datepicker.parseDate(
                instance.settings.dateFormat ||
                $.datepicker._defaults.dateFormat,
                selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    }

    var get_input_values = function (name) {
        if (!$("input[name='"+name+"']:checked").val()) {
            return ""
        }
        var ret = [];
        $("input[name='" + name + "']").each(function () {
            var $el = $(this);
            if ($el.prop('checked')) {
                ret.push($el.val());
            }
        });
        return ret.join(",");
    }

    var GenerateGet=function(obj)
    {
        var res = [];
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                res.push(key + "=" + obj[key]);
            }
        }
        return res.join("&");
    }

    var search = function () {
        showClock();
        SO.ticker = ISS;
        SO.sDate = $('#dfrom').val();
        SO.tDate = $('#dto').val();
        SO.currency = $("input[name='currency']:checked").val();
        SO.exchange_list = get_input_values("exchange");
        SO.issues_list = get_input_values("issues");
        var so_get = GenerateGet(SO);
        $('#full_view').html("<a href=\"/trade/fullview/?" + so_get + "\" target=\"_blank\"><span class=\"icon-link-ext\" />Расширенный вид</a>");

        $.post("/Trade/Search", SO, function (data) {
            hideClock();
            generate_result(data);
        }, "json");
    }

    var searchtoexcel = function () {
        SO.ticker = ISS;
        SO.sDate = $('#dfrom').val();
        SO.tDate = $('#dto').val();
        SO.currency = $("input[name='currency']:checked").val();
        SO.exchange_list = get_input_values("exchange");
        SO.issues_list = get_input_values("issues");

        var form = document.createElement("form");
        form.action = "/Trade/DoSearchToExcel";
        form.method = "POST";
        element = document.createElement("input");
        element.type = "hidden";
        element.name = "string_params";
        element.value = JSON.stringify(SO);
        form.appendChild(element);
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    }


    var generate_result=function(result)
    {
        $('#search_count').html('');
        $('#t_content').html('');
        if (result.Items.length == 0) {
            $('#search_count').html("<span class=\"notfound\">Нет данных за указанный период</span>");
        } else {
            var tbl_arr = [];
            var headers = result.Headers;
            tbl_arr.push("<table class=\"data_table\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr>");
            tbl_arr.push(set_header_cell("Дата"));
            tbl_arr.push(set_header_cell("Биржевой код"));
            tbl_arr.push(set_header_cell("Орг. торг."));
            tbl_arr.push(set_header_cell(headers[1]));
            tbl_arr.push(set_header_cell(headers[2]));
            tbl_arr.push(set_header_cell(headers[7]));
            tbl_arr.push(set_header_cell(headers[15]));
            tbl_arr.push("</tr>");
            for (var i = 0; i < result.Items.length; i++) {
                tbl_arr.push("<tr>");
                var item = result.Items[i];
                tbl_arr.push(set_cell(item.RegDate));
                tbl_arr.push(set_cell(get_moex_link(item.Code)));
                tbl_arr.push(set_cell(item.TradePlaceName));
                tbl_arr.push(set_cell(item.Values[1]));
                tbl_arr.push(set_cell(item.Values[2]));
                tbl_arr.push(set_cell(item.Values[7]));
                tbl_arr.push(set_cell(item.Values[15]));
                tbl_arr.push("</tr>");
            }
            tbl_arr.push("</table>");
            tbl_arr.push("<div class=\"data_comment\">* - для облигаций значение показателя - % от номинала.</div>")
            $('#t_content').html(tbl_arr.join(''));
        }
    }

    var get_moex_link = function (code) {
        if (!code || code.length == 0) {
            return "";
        }
        return "<a href=\"http://moex.com/ru/issue.aspx?code=" + code + "\" target=\"_blank\">" + code + "</a>";
    }

    var set_header_cell = function (val) {
        return "<th>" + val + "</th>";
    }

    var set_cell = function (val) {
        return "<td>" + val + "</td>";
    }

    window.trade_init = function () {
        init_calendar();
        search();
        $('#btn_find').click(function () { search(); });
        $("#tab_content").css("min-width", "1100px");
        set_xls_function(function () { searchtoexcel(); });
    }
})();

$(document).ready(function () {
    trade_init();
});