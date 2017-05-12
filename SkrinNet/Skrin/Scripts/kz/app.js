/// <reference path="../jquery-1.10.2.intellisense.js" />
//KZ

$(document).ready(function () {
    app_init();
});


(function () {
    var selected_id = 1;


    this.app_init = function () {
        $.post("/Menu/GetKZMenu/", { "ticker": ISS }, function (data) {
            generate_left_menu(data);          
            //load_init_functions();
            set_profile_print();
        }, "json");
    };


    var generate_left_menu = function (menu_data) {
        var $l_menu = $('<ul class="firts_level">');
        for (var i = 0; i < menu_data.length; i++) {

            var m = menu_data[i];
            if (m.Tabs.length == 0 && m.SubMenu.length == 0) {
                continue;
            }

            var $li = $("<li>");


            var c_class = "t_level" + (m.Id == selected_id ? " selected" : "") + (m.Tabs.length == 0 ? "" : " linkable");
            var $div = $('<div class="' + c_class + '">' + m.Name + "</div>");
            $div.attr("id", "mn" + m.Id);
            //Если у коревого меню есть привязанные табы
            if (m.Tabs.length != 0) {
                if (user_have_rights_menu(m)) {
                    $div.click((function (cur_menu) {
                        return function (event) {
                            load_menu_content(event, cur_menu);
                        }
                    })(m));
                } else {
                    $div.addClass("no_rights");
                    $div.click(function () {
                        no_rights();
                    });
                }
            }


            $li.append($div);

            if (m.SubMenu.length != 0) {
                var $u_sub = $('<ul class="second_level">');
                for (var j = 0; j < m.SubMenu.length; j++) {
                    var m_sub = m.SubMenu[j];
                    if (m_sub.Tabs.length == 0) {
                        continue;
                    }
                    var $li_sub = $("<li>" + m_sub.Name + "</li>");
                    $li_sub.attr("id", "mn" + m_sub.Id);
                    if (!user_have_rights_menu(m_sub)) {
                        $li_sub.addClass("no_rights");
                        $li_sub.click(function () {
                            no_rights();
                        });
                    } else {
                        $li_sub.click((function (cur_menu) {
                            return function (event) {
                                load_menu_content(event, cur_menu);
                            }
                        })(m_sub));
                    }

                    $u_sub.append($li_sub);
                }
                $li.append($u_sub);
            }



            $l_menu.append($li);
        }
        $('.left-menu').append($l_menu);

        //посчитаем размер меню
        var menu_height = $('.left-menu').height();
        //посчитаем размер экрана
        var screen_height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
        //берем, что больше
        var height = menu_height > screen_height ? menu_height : screen_height;
        var bottom_lenght = $('#bottom').height();


        $('.main_content').css("min-height", (height - 50 - bottom_lenght) + "px");
    };


    var load_menu_content = function (event, menu) {
        //первый таб должен быть общедоступным.

        clear_content();

        if (menu.Tabs.length > 0) {
            $(".t_level").add(".second_level li").removeClass("selected");
            var $cur_el = $(event.target);
            $cur_el.addClass("selected");
            if (!$cur_el.hasClass(".t_level")) {
                $cur_el.parents("li").find(".t_level").addClass("selected");
            }
            if (menu.Id != 1) {
                var $content = $("<h1>" + NAME + "</h1><h2>" + menu.Name + "</h2>");
                var $ul = $("<ul class=\"menu_tabs\">");
                for (var i = 0; i < menu.Tabs.length; i++) {
                    var tab = menu.Tabs[i];
                    var $li = $("<li>");
                    if (i == 0) {
                        $li.addClass("active");
                    }
                    var $span = $("<span>");
                    if (user_have_rights_tab(tab)) {
                        $span.click((function (cur_tab) {
                            return function (event) {
                                activate_tab(event, cur_tab);
                            }
                        })(tab));
                    } else {
                        $span.click(function () {
                            no_rights();
                        });
                    }

                    $span.text(tab.Name);
                    $li.append($span);
                    $ul.append($li);
                }
                var $tab = $("<div class=\"tabular\">");
                $tab.append($content).append($ul).append("<div id=\"tab_content\">");
                $(".main_content").append($tab);
                $(".menu_tabs li.active span").click();
            } else {
                //Для первой страницы другие правила           );
                load_KZprofile();
            }
        }
    }

    //Очистка предыдущего контента
    var clear_content = function () {
        $(".main_content").empty();
        INIT_FUNCT = {};
    }

    window.set_print_function = function (fn) {
        if (!fn)
            return;
        $('#print_btn').unbind('click');
        $('#print_btn').bind('click', fn).removeClass('disabled');
    }
    //Функция печати для профиля
    var set_profile_print = function () {
        set_print_function(function () {
            var name = $('title').text();
            var p_script = null;
            if ($(".chart_block").length > 0) {
                var canvas = document.getElementById("myChart");
                var link1 = canvas.toDataURL();
                canvas = document.getElementById("myChart1");
                var link2 = canvas.toDataURL();
                var p_script_internal = [];
                p_script_internal.push("$('#myChart').remove();");
                p_script_internal.push("$('#myChart1').remove();");
                p_script_internal.push("$('#myChearImgPlacer').html('<img src=\"" + link1 + "\"/>');");
                p_script_internal.push("$('#myChearImgPlacer1').html('<img src=\"" + link2 + "\"/>');");
                p_script = "$(document).ready(function () {" + p_script_internal.join("") + "});"

            }
            $('.left_content_html').printElement({
                pageTitle: name + ".html", leaveOpen: false,
                printMode: 'popup',
                script: p_script,
                overrideElementCSS: ['/Content/print.css', '/Content/iss/fontello/css/fontello.css']
            });
        });
    }

    //Печать по умолчанию
    var set_default_print = function () {
        $('#print_btn').unbind('click');
        $('#print_btn').bind('click', function () {
            printSelector('#tab_content');
        }).removeClass('disabled');
    }

    window.printSelector = function (selector) {
        var name = $('title').text();
        var subHeader = $('.tabular > h2:first').text();
        $(selector).printElement({
            pageTitle: name + ".html", leaveOpen: false,
            printMode: 'popup',
            headers: ['<h1>' + name + '</h1>', '<h2>' + subHeader + '</h2>'],
            overrideElementCSS: ['/Content/print.css']
            /*overrideElementCSS: ['/Content/iss/tabs.css', '/Content/iss/stoplight.css', '/Content/iss/fontello/css/fontello.css',
                '/Content/forms.css', '/Content/modals.css', '/Content/modal_dialog.css', '/Content/iss/main.css']*/
        });
    }


    //Загрузка содержимого табов
    var activate_tab = function (event, tab) {
        showClock();
        set_default_print();
        //Удалим события повешанные на другие табы       
        $('#xls_btn').unbind('click').addClass('disabled');
        $('#pdf_btn').unbind('click').addClass('disabled');
        $('#wrd_btn').unbind('click').addClass('disabled');
        //Удалим специфичную для определенных страниц ширину табов
        $("#tab_content").removeAttr('style')
        $(".menu_tabs li").removeClass("active");
        $(event.target).parent().addClass("active");
        $.get("/TabKZ/", { "id": tab.Id, "ticker": ISS, "PG": 1 }, function (data) {
            hideClock();
            if ($("#tabId").length == 0) $("#tab_content").after('<input type="hidden" id="tabId" value="' + tab.Id + '"/>');
            $("#tab_content").html(data);
        }, "html").fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            $("#tab_content").html(textStatus);
        });
    }


    //Загрузка профиля(для него отдельный скрипт)
    var load_KZprofile = function () {
        clear_content();
        //Удалим события повешанные на другие табы
        $('#xls_btn').unbind('click').addClass('disabled');
        $('#pdf_btn').unbind('click').addClass('disabled');
        $('#wrd_btn').unbind('click').addClass('disabled');
        showClock();
        var result = {};
        $.get("/profileKZ/modules/ProfileMain", { "ticker": ISS }, function (data) {
            result.left_content = data;
            generate_profile_content(result);
        }, "html")
    }

    var generate_profile_content = function (result) {
        var $content = $(".main_content");
        $content.html("<div class=\"left_content\">" + result.left_content + "</div>");
        if (result.right_content) {
            $content.append("<div class=\"right_content\">" + result.right_content + "</div>");
        }
        hideClock();
        load_init_functions();
    }

    //Очистка предыдущего контента
    var clear_content = function () {
        $(".main_content").empty();
        INIT_FUNCT = {};
    }

    //Запуск дополнительных функций после загрузки меню
    var load_init_functions = function () {
        for (var fn in INIT_FUNCT) {
            INIT_FUNCT[fn]();
        }
    }


    //Имеет ли пользователь право просматривать данный tab 
    var user_have_rights_tab = function (tab) {
        if (OPEN_COMPANY)
            return true;
        if (!tab.Accesses)
            return false;
        return intersect_array(ACCESS, tab.Accesses).length > 0;
    }
    //Имеет ли пользователь право просматривать какой-нибудь tab из данного меню
    var user_have_rights_menu = function (menu) {
        for (var i = 0; i < menu.Tabs.length; i++) {
            if (user_have_rights_tab(menu.Tabs[i]))
                return true;
        }
        return false;
    }




})();