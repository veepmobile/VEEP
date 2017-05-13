(function () {
    var searching = 0;

    window.search_init = function () {
        $.post("/Multilinks/GroupList/", null, function (data) {
            group_list = data;
            $('#group_list option[value!=0]').remove();
            $.each(group_list, function (i, item) {
                $('#group_list').append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            });
        });
    }

    window.doSearch = function (iss) {
        if (searching == 1) {
            doStop();
        } else {
            if (checkCanSearch(iss)) {
                searching = 1;
                showClock();
                $("#btn_search").text("Остановить");
                mainSearcher(iss);
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        }
    };

    var mainSearcher = function (iss) {
        if (iss == "")
        {
            var params = {
                "codes": $("#codes").val().replace(new RegExp("\n", "g"), " ,"),
                "listid": $("#group_list").val()
            }
        }
        else
        {
            var params = {
                "codes": iss + "," + $("#codes").val().replace(new RegExp("\n", "g"), " ,"),
                "listid": "0"
            }
        }

        $.post("/Multilinks/Search", params, function (data) {
            var re = new RegExp("icon_error", "ig")
            GenerateResult(data);
            hideClock();
            if (String(re.exec(data)) == "null") {
                if ($("#search_table").css("display") == "block" || $("#search_table").css("display") == "table") {
                    $('#hide_form').show();
                    toggle_form();
                }
            }

            hideClock();
            $("#btn_search").text("Найти");
            searching = 0;
            location.href = "#top";
        }, "json");
    };

    var GenerateResult = function (data) {
        $("#search_result").html('').show();

        if (data.error != null) {
            var $el = $('<div>').addClass("non_result").text("Ошибка выполнения запроса");
            $("#search_result").append($el);
            return;
        }

        var chain = data.chain;
        var total_text = "";
        if (data.search_countul < data.countul || data.search_countfl < data.countfl) {
            total_text += "<div>поиск выполнен по первым";
            if (data.countul > 0) {
                total_text += " " + data.search_countul + " юр.лицам";
            }
            if (data.countfl > 0) {
                if (data.countul > 0) total_text += ", ";
                total_text += " " + data.search_countfl + " физ.лицам ";
            }
            total_text += "(всего в списке";
            if (data.countul > 0) {
                total_text += " " + data.countul + " юр.лиц ";
            }
            if (data.countfl > 0) {
                if (data.countul > 0) total_text += ", ";
                total_text += " " + data.countfl + " физ.лиц ";
            }
            total_text += ")</div>";
        }
        $("#search_result").append("<span class=\"total_count\">" + total_text + "</span>");

        if (chain.length == 0) {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
            return;
        }

        var $res_block = $('<div>').addClass("res_block");

        for (var i = 0, i_max = (chain.length - 1) ; i <= i_max; i++) {
            var $res_item = $('<div>').addClass("res_item");
            $res_item.css({ "width": "auto" });
            var arr_prof = chain[i].prof;
            var arr_link = chain[i].link;
            var row = 0;
            var col = 5;
            var ret = "";
            for (var j = 0, j_max = (arr_prof.length - 1) ; j <= j_max; j++) {
                var prof = arr_prof[j]
                if (col > 4) {
                    if (row > 0) ret += "</tr>";
                    ret += "<tr>";
                    row++;
                    col = 1;
                }
                ret += "<td width=\"200px\">";
                ret += "<div style=\"display:table\">";
                if (row > 1 && col == 1) {
                    if (arr_link[j - 1].direction == 1)
                        ret += "<div class=\"profile_link_right\"></div>";
                    else
                        ret += "<div class=\"profile_link_left\"></div>";
                }
                var big = false;
                if ((j > 0) && (j < arr_prof.length - 1)) if ((arr_link[j - 1].direction == 2) && (arr_link[j].direction == 1)) big = true;
                ret += "<div class=\"profile" + (prof.is_src ? "_src" : "") + (big ? "_big" : "") + "\" style=\"" + (row > 0 && col == 0 ? "display: table-cell" : "") + "\">";
                if (prof.ProfType == 1) {
                    if (prof.ul.ticker != "")
                        ret += "<a class=\"ref\" href=\"/issuers/" + prof.ul.ticker + "\" target=\"_blank\">" + prof.ul.name + "</a><br><text>ОГРН " + prof.ul.ogrn + "</text>";
                    else
                        ret += "<text>" + prof.ul.name + "</text>";
                }
                else {
                    ret += "<a  class=\"ref\" href=\"/profilefl?fio=" + prof.fl.fio + "&inn=" + prof.fl.inn + "\" target=\"_blank\">" + prof.fl.fio + "</a><br><text>ИНН " + prof.fl.inn + "</text>";
                }
                ret += "</div></div>";
                ret += "</td>";
                if (j < j_max) {
                    ret += "<td width=\"80px\">";
                    link = arr_link[j];
                    ret += "<div class=\"" + (link.direction == 1 ? (link.tmplink ? "link_right_tmp" : "link_right") : (link.tmplink ? "link_left_tmp" : "link_left")) + "\">";
                    if (link.LinkType == 1 || link.LinkType == 3) {
                        ret += "<text>Учредитель</text><br>";
                        if (link.share_percent != "") ret += "<text>" + link.share_percent + " %</text>";
                        else if (link.share != "0" && link.share != "") ret += "<text>" + link.share + " руб.</text>";
                    }
                    if (link.LinkType == 2) ret += "<text>Управляющая организация</text>";
                    if (link.LinkType == 4) ret += "<text>" + link.position + "</text>";
                    if (link.LinkType == 5) ret += "<text>Преемник</text>";
                    ret += "</div>";
                    ret += "</td>";
                    col++;
                    ret += "</td>";
                }
            }
            if (arr_prof.length > 0) ret += "</tr>";
            ret += "</table>";
            var $res_tbl = $('<table>').addClass("graph").html(ret);
            $res_item.append($res_tbl);
            $res_block.append($res_item);
        }
        $("#search_result").append($res_block);
    }

    var doStop = function () {
        hideClock();
        searching = 0;
        $("#btn_search").text("Найти");
        window.stop();
    };

    window.doClear = function () {
        $("#codes").val("");
    }

    function checkCanSearch(iss) {
        var ss = $("#codes").val();
        if (iss == "") if ($("#group_list").val() != "0") ss += $("#group_list").val();
        return (ss.length == 0) ? false : true;
    }

})();


$(document).ready(function () {
    search_init();
});