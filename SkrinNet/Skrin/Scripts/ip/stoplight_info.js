/// <reference path="jquery144.js" />
var StopLight = (function () {
    var ogrn;

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
            if (!$elem)
                return;
            ogrn = $elem.attr("ogrn");
            $.post("/StopLight/InfoIP/", { "ogrn": ogrn }, function (data) {

                var color_rate = data.rating;
                var factors_count = data.count;
                var color_name = "";
                switch (color_rate) {
                    case "NoRating":
                        return;
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
                $elem.html("<div id=\"rating\" class=\"tr_light " + color_rate.toLowerCase() + "\"><span class=\"tr_info\">" + color_name + "<br/><span class=\"explain\">(дата проверки " + _get_yerstarday_date() + ")</span><br/><a href=\"javascript:no_rights()\">Посмотреть протокол проверки</a></span>");
            }, "json");
        }



    }

})();


INIT_FUNCT["StopLight"] = function () {
    StopLight.init("StopLightBox");
};
/*
$(document).ready(function () {
    StopLight.init("StopLightBox");
});
*/
