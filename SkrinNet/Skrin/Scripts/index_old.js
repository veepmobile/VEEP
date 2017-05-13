(function () {
    $().ready(function () {
        load_news();
        //location.href = '/company/apages/check/';
    });



    function load_news() {
        load_statistic();
        load_site_updates();
        load_last_reports();
        load_last_events();
        load_analitics();
        load_ul_updates();
        //load_uc();
    }

    function load_statistic() {
        $.post("/Home/GetNews/", {type:1}, function (data) {
            $('#ul_count').text(fomat_number(data.ul_count));
            $('#fl_count').text(fomat_number(data.fl_count));
        }, "json");
    }

    function load_site_updates() {
        var $conteiner = $('#site_updates_conteiner');
        show_preloader($conteiner);
        $.post("/Home/GetNews/", { type: 2 }, function (data) {
            hide_preloader($conteiner);
            var ml = {};
            ml.items = [];
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                var row = {};
                row.datetime = source.datetime;
                row.news_text = '<a  href="/company/base_skrin/new/?nid=' + source.message_link + '" target="_blank">' + source.message_title + '</a>';
                ml.items.push(row);
            }
            generate_news_table($conteiner, ml);
        }, "json");
    }

    function load_last_reports() {
        var $conteiner = $('#last_reports_container');
        show_preloader($conteiner);
        $.post("/Home/GetNews/", { type: 3 }, function (data) {
            hide_preloader($conteiner);
            var ml = {};
            ml.items = [];
            ml.info_date = data.info_date;
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                var row = {};
                var ticker = source.company_link.replace("/issuers/");
                var link = source.message_link.split('|');
                row.datetime = source.datetime;

                row.news_text = "<a class=\"organitation\" onclick=\"openIssuerMenu('" + source.company_link + "','" + ticker + "');return false;\" href=\"#\">" + source.company_title + ":</a> "
                                + "<a  href=\"#\" onclick=\"show_disclosure_news(" + link[0] + "," + link[1] + ",'"+ticker+"');return false;\">" + source.message_title + "</a>";
                //row.news_text = "<a class=\"organitation\" onclick=\"openIssuerMenu('" + source.company_link + "','" + ticker + "');return false;\" href=\"#\">" + source.company_title + ":</a> "
                //                + "<a  href=\"#\" onclick=\"show_dialog($('body'),'345','test','hello',false);return false;\">" + source.message_title + "</a>";
                ml.items.push(row);
            }
            generate_news_table($conteiner, ml);
        }, "json");
    }

    function load_last_events() {
        var $conteiner = $('#last_events_container');
        show_preloader($conteiner);
        $.post("/Home/GetNews/", { type: 4 }, function (data) {
            hide_preloader($conteiner);
            var ml = {};
            ml.items = [];
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                var row = {};
                var comp_link = "/issuers/" + source.company_link;
                row.datetime = source.datetime;

                row.news_text = "<a class=\"organitation\" onclick=\"openIssuerMenu('" + comp_link + "','" + source.company_link + "');return false;\" href=\"#\">" + source.company_title + ":</a> "
                                + source.message_title;
                ml.items.push(row);
            }
            generate_news_table($conteiner, ml);
        }, "json");
    }

    function load_analitics() {
        var $conteiner = $('#analitics_container');
        show_preloader($conteiner);
        $.post("/Home/GetNews/", { type: 5 }, function (data) {
            hide_preloader($conteiner);
            var ml = {};
            ml.items = [];
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                var row = {};
                row.datetime = source.datetime;
                row.news_text = "<span class=\"organitation\" >" + source.company_title + ":</span> "
                                + "<a  href=\"" + source.message_link + "\">" + source.message_title + "</a>";
                ml.items.push(row);
            }
            generate_news_table($conteiner, ml);
        }, "json");
    }

    function load_ul_updates() {
        var $conteiner = $('#ul_updates_container');
        show_preloader($conteiner);
        $.post("/Home/GetNews/", { type: 7 }, function (data) {
            hide_preloader($conteiner);
            var ml = {};
            ml.items = [];
            ml.info_date = data.info_date;
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                var row = {};
                var comp_link = "/issuers/" + source.company_link;
                row.datetime = source.datetime;

                row.news_text = "<a class=\"organitation\" onclick=\"openIssuerMenu('" + comp_link + "','" + source.company_link + "');return false;\" href=\"#\">" + source.company_title + "</a>";
                ml.items.push(row);
            }
            generate_news_table($conteiner, ml);
        }, "json");
    }

    function load_uc() {
        var $container = $('#announce_ul');
        $container.after('<div id="announce_ul_preloader" class="preloader" style="height:100px;width:100px;"></div>')
        $.post("/Home/GetNews/", { type: 6 }, function (data) {
            $('#announce_ul_preloader').remove();
            var max_height = 250; //максимально допустимый размер блока
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                if ($container.height() < max_height) {
                    $container.append('<li id="announce_ul_li_' + i + '"><span class="an_time">' + source.datetime + '</span>' + source.message_title + '</li>');
                } else {
                    break;
                }
            }
            if ($container.height() > max_height) {
                //если выходит за габариты то удалим последнюю
                $('#announce_ul_li_' + i).remove();
            }
        }, "json");
    }

    function generate_news_table($container, message_list) {
        if (message_list.info_date) {
            $container.append('<tr><td class="datetime" colspan="2" style="font-size: 13px;">' + message_list.info_date + '</td><tr>');
        }
        for (var i = 0, i_max = message_list.items.length; i < i_max; i++) {
            var item = message_list.items[i];
            $container.append('<tr><td class="datetime">' + item.datetime + '</td><td class="news_text">' + item.news_text + '</td></tr>')
        }
    }

    function show_preloader($container) {
        $container.append('<tr class="preloader"><td style="height:40px;width:100px;">&nbsp;</td></tr>');
    }

    function hide_preloader($container) {
        $container.find('.preloader').remove();
    }

})();

function show_disclosure_news(id, agency_id,ticker) {
    showClock();
    $.post('/Message/Event/', { "id": id, "agency_id": agency_id,"ticker":ticker }, function (data) {
        hideClock();
        var content = $(data).html();
        show_dialog({ "content": content, "is_print": true });
    });
}