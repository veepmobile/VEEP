
$(document).ready(function () {
    app_init();
});

(function () {

    this.app_init = function () {
        
        //посчитаем размер меню
        var menu_height = $('.left-menu').height();
        //посчитаем размер экрана
        var screen_height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
        //берем, что больше
        var height = menu_height > screen_height ? menu_height : screen_height;
        var bottom_lenght = $('#bottom').height();
        $('.main_content').css("min-height", (height - 50 - bottom_lenght) + "px");

        $("#mn1").click(function () { load_menu_content(1); });
        $("#mn2").click(function () { load_menu_content(2); });
        $("#mn3").click(function () { load_menu_content(3); });

        set_default_print();

        showClock();
        var $content = $("#tab_content");
        $.get("/profilefl/ProfileMain", { "fio": fio, "inn": inn }, function (data) {
            $content.html(data);
            hideClock();
        }, "html")
    }

    var load_menu_content = function (menu) {
        clear_content();
        showClock();
        var $content = $("#tab_content");
        $("#mn1").removeClass("selected");
        $("#mn2").removeClass("selected");
        $("#mn3").removeClass("selected");
        $("#pdf_title").addClass("hidden");
        $("#print_title").addClass("hidden");

        if (menu == 1)
        {
            $("#mn1").addClass("selected")
            $("#pdf_title").removeClass("hidden");
            $.get("/profilefl/ProfileMain", { "fio": fio, "inn": inn }, function (data) {
                $content.html(data);
                hideClock();
            }, "html")
        }
        if (menu == 2) {
            $("#mn2").addClass("selected")
            $("#print_title").removeClass("hidden");
            $.get("/profilefl/Bankruptcy", { "fio": fio, "inn": inn }, function (data) {
                $content.html(data);
                hideClock();
            }, "html")
        }
        if (menu == 3) {
            $("#mn3").addClass("selected")
            $("#print_title").removeClass("hidden"); $.get("/profilefl/Passports", { "fio": fio, "inn": inn }, function (data) {
                $content.html(data);
                hideClock();
            }, "html")
        }
    }

    //Очистка предыдущего контента
    var clear_content = function () {
        $("#tab_content").empty();
    }

    window.set_input_value = function (name, val) {
        var inputs = document.getElementsByName(name);
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].value == val) {
                inputs[i].checked = true;
            } else {
                inputs[i].checked = false;
            }
        }
    }
    //Печать по умолчанию
    window.set_default_print = function () {
        $('#print_btn').unbind('click');
        $('#print_btn').bind('click', function () {
            printSelector('#tab_content');
        }).removeClass('disabled');
    }

    window.printSelector = function (selector) {
        /*var name = $('title').text();*/
        var name = $('.tabular > h1:first').text();
        var subHeader = $('.tabular > h2:first').text();
        $(selector).printElement({
            pageTitle: name + ".html", leaveOpen: false,
            printMode: 'iframe',
            headers: ['<h1>' + name + '</h1>', '<h2>' + subHeader + '</h2>'],
            overrideElementCSS: ['/Content/print.css']
            /*overrideElementCSS: ['/Content/iss/tabs.css', '/Content/iss/stoplight.css', '/Content/iss/fontello/css/fontello.css',
                '/Content/forms.css', '/Content/modals.css', '/Content/modal_dialog.css', '/Content/iss/main.css']*/
        });
    }

})();