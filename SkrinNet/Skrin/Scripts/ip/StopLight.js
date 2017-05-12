/// <reference path="jquery144.js" />
var StopLight = (function () {
    var ogrn;
    var content;

    function _get_yerstarday_date() {
        var date = new Date();
        date.setDate(date.getDate() - 1);
        return ('0' + date.getDate()).slice(-2) + '.'
             + ('0' + (date.getMonth() + 1)).slice(-2) + '.'
             + date.getFullYear();;
    }

    return {
        init: function (id) {
            var $elem = $("#" + id);
            if (!$elem.length)
                return;
            ogrn = $elem.attr("ogrn");
            $.post("/StopLight/IndexIP/", { "ogrn": ogrn }, function (data) {
                content = data
                var $sl_data = $(data).filter("#stoplight_data");
                if (!$sl_data.length)
                   return;
                var color_rate = $sl_data.val();
                var factors_count = $sl_data.attr("factors_count");
                var color_name = "";
                switch (color_rate) {
                    case "Green":
                        color_name = "Контрагент надежный";
                        break;
                    case "Yellow":
                        color_name = "Внимание, условный стоп-сигнал.<br/>Количество найденных факторов: " + factors_count;
                        break;
                    case "Red":
                        color_name = "Внимание, безусловный стоп-сигнал.<br/>Количество найденных факторов: " + factors_count;
                        break;
                }
                $elem.html("<div id=\"rating\" class=\"tr_light " + color_rate.toLowerCase() + "\"><span class=\"tr_info\">" + color_name + "<br/><!--span class=\"explain\">(дата проверки " + _get_yerstarday_date() + ")</span><br/--><a href=\"javascript:StopLight.showprotocol()\">Посмотреть протокол проверки</a></span>");
                $("#sl_date").html("Дата проверки " + _get_yerstarday_date());
            }, "html");
        },
        showprotocol: function () {
            show_dialog({ "content": content, "extra_style": "width:990px;", is_print: true });
        },
        switch_rating_info: function (obj) {
            if ($(obj).find("span[class*='icon-angle-up']").length > 0) {
                $(obj).find("span[class*='icon-angle-up']").html("Подробнее");
                $(obj).find("span[class*='icon-angle-up']").attr("class", "icon-angle-down");

            } else {
                $(obj).find("span[class*='icon-angle-down']").html("Свернуть");
                $(obj).find("span[class*='icon-angle-down']").attr("class", "icon-angle-up");
            }
        },
        show_rating_info: function (id) {
            if ($('#tbl' + id + '_info').is(":visible")) {
                $('#tbl' + id + '_info').hide();
                $('#tbl' + id).hover(function () {
                    $(this).css({ 'background-color': "#f4f1f0" });
                }, function () {
                    $(this).css({ 'background-color': "" });
                });
            } else {
                $('#tbl' + id).css("background-color", "#EEE")
                $('#tbl' + id + '_info').show();
                $('#tbl' + id).hover(function () {
                    $(this).css({ 'background-color': "#f4f1f0" });
                }, function () {
                    $(this).css({ 'background-color': "#eee" });
                });
            }
        }
    }

})();


INIT_FUNCT["StopLight"] = function () {
    StopLight.init("StopLightBox");
};

