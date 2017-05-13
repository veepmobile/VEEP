


$(document).ready(function () {
    app_init();
});



(function () {
    var selected_id = 1;
    var docreg_content = "";
    var bad_addr_content = "";
    var disfind_content = "";
    var docreg_content = "";


    this.app_init = function () {
        $.post("/Menu/GetIPMenu/", { "ticker": ISS }, function (data) {
            generate_left_menu(data);
            load_init_functions();
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


    //Имеет ли пользователь право просматривать данный tab 
    var user_have_rights_tab = function (tab) {  
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


    //Установка значения селектора
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
                load_ipprofile();
            }
        }
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
        $.get("/TabIp/", { "id": tab.Id, "ticker": ISS, "PG": 1 }, function (data) {
            hideClock();
            if ($("#tabId").length == 0) $("#tab_content").after('<input type="hidden" id="tabId" value="' + tab.Id + '"/>');
            $("#tab_content").html(data);
        }, "html").fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            $("#tab_content").html(textStatus);
        });
    }


    //Загрузка профиля(для него отдельный скрипт)
    var load_ipprofile = function () {
        clear_content();
        //Удалим события повешанные на другие табы
        $('#xls_btn').unbind('click').addClass('disabled');
        $('#pdf_btn').unbind('click').addClass('disabled');
        $('#wrd_btn').unbind('click').addClass('disabled');
        showClock();
        var result = {};
        $.get("/profileip/modules/ProfileMain", { "ticker": ISS }, function (data) {
            result.left_content = data;
            $.get("/profileip/modules/ProfileRightMenu", { "ticker": ISS }).done(function (data) {
                result.right_content = data;
                generate_profile_content(result);
                set_profile_print();
            }, "html");
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

    window.ShowUnfairInn = function (inn) {          
        $.get("/Message/ShowUnfair/", { "inn": inn }, function (data) {
           show_dialog({ "content": data, "extra_style": "width:800px;", is_print: true });
        });
    }



    window.docreg_check = function (e) {
        e.stopPropagation();
        var docInterval;

        var show_docreg = function () {
            $.get("/Check/DocReg/", { "ticker": ISS }, function (data) {
                var q_stat = data.QStatus;
                docreg_content = "";
                switch (q_stat) {
                    case 0:
                        clearInterval(docInterval);
                        unblock_checkbutton('docreg_check');
                        complite_job("docr", "<div  onclick='docreg_check();'><span class='icon-search link ico'></span>Сведения о представлении ИП заявлений для гос.регистрации в ФНС<span></span></div>");
                        var result = JSON.parse(data.Result);
                        if (!result || !result.length) {
                            docreg_content = '<br/><span class="minicaption">СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ЮЛ ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                            docreg_content += '<h4>Результат поиска по ОГРН ' + data.Ogrn + ':</h4><hr/>';
                            docreg_content += "<div class=\"check_not_found\">Сведения о представлении в регистрирующий (налоговый) орган документов за последний год отсутствуют.</div>";
                        } else {
                            docreg_content = '<br/><span class="minicaption">СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ЮЛ ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                            docreg_content += '<h4>Результат поиска по ОГРН ' + data.Ogrn + ':</h4><hr/>';
                            docreg_content += '<table width="100%" class="data_table" cellspacing="0" cellpadding="0">';
                            docreg_content += '<tr><th>Сведения о юридическом лице</th><th>Сведения о поданном заявлении</th><th>Сведения о представлении документов</th><th>Наименование налогового органа</th><th>Сведения о решении</th></tr>';
                            docreg_content += get_result_content(result);
                            docreg_content += '</table>';
                            docreg_content += "<div style='clear:both;height:50px'></div>";
                        }
                        show_dialog({ "content": docreg_content, "extra_style": "width:900px;", is_print: true });
                        break;
                    case 6:
                        clearInterval(docInterval);
                        unblock_checkbutton('docreg_check');
                        complite_job("docr", format_message_error("СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ИП ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС:<br/> Сервис временно не доступен, попробуйте зайти позже."));
                        break;
                    case 8:
                        clearInterval(docInterval);
                        unblock_checkbutton('docreg_check');
                        complite_job("docr", format_message_error("СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ИП ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС:<br/> Ваш уровень доступа не позволяет использование данной функцией."));
                        break;
                }
            });
        };


        var addResultItemHtml = function (html, caption, value) {
            if (value && value.length) {
                if (caption && caption.length) html.push(' <b>' + caption + ': </b> ');
                html.push(value + '<br/>');
            }
        }

        var get_result_content = function (result) {
            var html = [];
            for (var i = 0; i < result.length; i++) {
                var row = result[i];
                html.push('<tr>');
                html.push('<td>');
                addResultItemHtml(html, false, row['NM']);
                addResultItemHtml(html, 'ОГРН', row['OG']);
                html.push('</td>');
                html.push('<td>');
                addResultItemHtml(html, 'Форма заявления', row['FR']);
                addResultItemHtml(html, 'Вид изменений', row['IZM']);
                html.push('</td>');
                html.push('<td>');
                addResultItemHtml(html, 'Дата представления', row['DT']);
                addResultItemHtml(html, 'Способ представления', row['PREDST']);
                addResultItemHtml(html, 'Входящий номер', row['NUM']);
                html.push('</td>');
                html.push('<td>');
                addResultItemHtml(html, false, row['REG']);
                html.push('</td>');
                html.push('<td>');
                addResultItemHtml(html, 'Вид решения', row['VR']);
                addResultItemHtml(html, 'Дата готовности документов', row['DTGOTOV']);
                html.push('</td>');
                html.push('</tr>');
            }
            return html.join('');
        }


        if (docreg_content == "") {
            block_checkbutton('docreg_check');
            push_job("docr", "Сведения о представлении ИП заявлений для гос.регистрации в ФНС");
            docInterval = setInterval(function () { show_docreg(); }, 5000);
        } else {
            show_dialog({ "content": docreg_content, "extra_style": "width:900px;", is_print: true });
        }

    }

    var block_checkbutton = function (id) {
//        $('#' + id).find('.ico').removeClass('icon-search').addClass('icon-spin3').addClass('animate-spin');
        $('#' + id).addClass('disabled');
    }

    var unblock_checkbutton = function (id) {
//        $('#' + id).find('.ico').addClass('icon-search').removeClass('icon-spin3').removeClass('animate-spin');
        $('#' + id).removeClass('disabled');
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

    window.gotomenu = function (id) {
        $('#mn' + id).click();
    }

})();