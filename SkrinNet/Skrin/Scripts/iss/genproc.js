(function () {

    var SO = {};

    window.genproc_init = function () {
        SO.ticker = ISS;
        search(false);

    }

    var search = function (extra_params) {
        if (extra_params) {
            SO.year = $('#cyear').val();
            SO.month = $('#cmonth').val();
            SO.organ = escape($('#organ').val());
        }
        $('#search_count').remove();
        $('#search_result').remove();
        showClock();
        $.post("/GenProc/Index/", SO, function (result) {
            hideClock();
            $('#gen_proc_content').html(result);
            $('#btn_find').click(function () {
                search(true);
            });
            var caption = $("#search_count .minicaption").text();
            var pat = /По Вашему запросу найдено слишком большое количество плановых проверок/;
            if (pat.test(caption)) {
                $('#extra_search').css("display", "inline-block");
            }
        }, 'html');
    };

    window.ShowCase = function (id, org) {
        var filters = {
            cid: id,
            name: $('#SearchName').val().replace(/\s+/g, '%20').replace(/"/g, '').replace(/\!/g, '').replace(/-/g, ' ').replace(/'/g, ''),
            year: $('#cyear').val(),
            org: org.replace(/\s+/g, '%20').replace(/"/g, '').replace(/\!/g, '').replace(/-/g, ' ').replace(/'/g, '')
        };
        showClock();
        $.post("/GenProc/Details/", filters, function (data) {
            hideClock();
            if (data.length < 20) {
                data = "Сервис временно недоступен. Попробуйте позже";
            }
            show_dialog({ "content": data, "extra_style": "width:990px;", is_print: true });
        }, "html");
    }

})();

$(document).ready(function () {
    genproc_init();
});