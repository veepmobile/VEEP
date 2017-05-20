(function () {

    $().ready(function () {
        load_news();
    });

    function load_news() {
        load_statistic();
        load_site_updates();
        load_last_reports();
        load_last_events();
        load_analitics();
    }

    function load_statistic() {
        $.post("/Home/GetNews/", { type: 1 }, function (data) {
            $('#ul_count').text(fomat_number(data.ul_count));
            $('#fl_count').text(fomat_number(data.fl_count));
            $('#case_count').text(fomat_number(data.pravo_count));
            $('#zak_count').text(fomat_number(data.zak_count));
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
                row.news_text = '<a  href="/news/?id=' + source.message_link + '" target="_blank">' + source.message_title + '</a>';
                ml.items.push(row);
            }
            $conteiner.html(generate_content(ml));
        }, "json");
    }

    function load_last_reports() {
        var $conteiner = $('#last_reports_container');
        show_preloader($conteiner);
        $.post("/Home/GetNews/", { type: 3 }, function (data) {
            hide_preloader($conteiner);
            var ml = {};
            ml.items = [];
            for (var i = 0, i_max = data.items.length; i < i_max; i++) {
                var source = data.items[i];
                var row = {};
                var ticker = source.company_link.replace("/issuers/");
                var link = source.message_link.split('|');
                row.datetime = source.datetime;

                row.news_text = "<a class=\"organitation\" href=\"" + source.company_link + "\" target=\"_blank\">" + source.company_title + ":</a> "
                                + "<a  href=\"#\" onclick=\"show_disclosure_news(" + link[0] + "," + link[1] + ",'" + ticker + "');return false;\">" + source.message_title + "</a>";
                ml.items.push(row);
            }
            $conteiner.before('<span class="news_data news_data_large">' + data.info_date + '</span>');
            $conteiner.html(generate_content(ml));
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

                row.news_text = "<a class=\"organitation\"  href=\"" + comp_link + "\" target=\"_blank\">" + source.company_title + ":</a> "
                                + source.message_title;
                ml.items.push(row);
            }
            $conteiner.html(generate_content(ml));
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
            $conteiner.html(generate_content(ml));
        }, "json");
    }


    function generate_content(message_list) {
        var gen_result = [];
        for (var i = 0, i_max = message_list.items.length; i < i_max; i++) {
            var item = message_list.items[i];
            gen_result.push('<li><span class=\"news_data news_data_sm\">' + item.datetime + '</span>' + item.news_text + '</li>');
        }
        return gen_result.join('');
    }

    function show_preloader($container) {
        $container.append('<tr class="preloader"><td style="height:40px;width:100px;">&nbsp;</td></tr>');
    }

    function hide_preloader($container) {
        $container.find('.preloader').remove();
    }

})();

function show_disclosure_news(id, agency_id, ticker) {
    showClock();
    $.post('/Message/Event/', { "id": id, "agency_id": agency_id, "ticker": ticker }, function (data) {
        hideClock();
        var content = $(data).html();
        show_dialog({ "content": content, "is_print": true });
    });
}