/// <reference path="../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    app_init();  
});
var ltab = null;

(function () {
    var selected_id = 1;
    var mass_reg_content = "";
    var bad_addr_content = "";
    var disfind_content = "";
    var docreg_content = "";
    var nalog_debt_content = "";
    var CLOUD_KEY = "cloud_block_open";
    var AC_text = "";
    //Массив открытых блоков облака
    var state_arr = [0, 0, 0, 0, 0];



    this.app_init = function () {
        $.get("/Menu/GetMenu/?ticker="+ISS,function (data) {
            generate_left_menu(data);
            load_init_functions();
            set_profile_print();            
        }, "json");
        cloud_block_init();
    };



    var generate_left_menu = function (menu_data) {
        var $l_menu = $('<ul class="firts_level">');
        for (var i = 0; i < menu_data.length; i++) {

            var m = menu_data[i];
            if (m.Tabs.length == 0 && m.SubMenu.length == 0) {
                continue;
            }
                
            var $li = $("<li>");
            
            
            var c_class = "t_level" +  (m.Tabs.length == 0 ? "" : " linkable");
            var $div = $('<div class="' + c_class + '">' + m.Name + "</div>");
            $div.attr("id", "mn" + m.Id);
            //Если у коревого меню есть привязанные табы
            if (m.Tabs.length != 0) {
                if (user_have_rights_menu(m)) {
                    $div.click((function (cur_menu) {
                        return function (event) {
                            load_menu_content(event,cur_menu);
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
                    if (m_sub.Id == selected_id) {
                        $li_sub.addClass("selected");
                    }
                    if (!user_have_rights_menu(m_sub) && m_sub.Id!=selected_id) {
                        $li_sub.addClass("no_rights");
                        $li_sub.click(function () {
                            no_rights();
                        });
                    } else {
                        $li_sub.click((function (cur_menu) {
                            return function (event) {
                                load_menu_content(event,cur_menu);
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
        if (OPEN_COMPANY)
            return true;
        if(!tab.Accesses)
            return false;
        return intersect_array(ACCESS, tab.Accesses).length > 0;
    }
    //Имеет ли пользователь право просматривать какой-нибудь tab из данного меню
    var user_have_rights_menu = function (menu) {
        for (var i = 0; i <menu.Tabs.length; i++) {
            if (user_have_rights_tab(menu.Tabs[i]))
                return true;
        }
        return false;
    }


    var load_menu_content = function (event, menu) {
        //Проверим есть ли выделеный таб и если он есть, присутствует ли он в данном меню
        var should_cancel;
        if (ltab != null) {
            should_cancel = true;
            for (var i = 0; i < menu.Tabs.length; i++) {
                if (menu.Tabs[i].Id == ltab) {
                    should_cancel = false;
                    break;
                }
            }
            if (should_cancel) {
                ltab = null;
                return;
            }
        }



        //первый таб должен быть общедоступным.

        clear_content();

        

        if (menu.Tabs.length > 0) {
            $(".t_level").add(".second_level li").removeClass("selected");
            var $cur_el = $(event.target);
            $cur_el.addClass("selected");
            if (!$cur_el.hasClass(".t_level")) {
                $cur_el.parents("li").find(".t_level").addClass("selected");
            }
            var t_act = 0;
            if (menu.Id != 1) {
                var $content = $("<h1><a href=\"\">" + NAME + "</a></h1><h2>" + menu.Name + "</h2>");
                var $ul = $("<ul class=\"menu_tabs\">");
                var act_tab = 0;
                for (var i = 0; i < menu.Tabs.length; i++) {
                    
                    var tab = menu.Tabs[i];
                    var $li = $("<li>");
                    if (ltab) {
                        if (ltab == tab.Id) {
                            act_tab = i;
                        }

                    } 
                    if (i == act_tab) {
                            $li.addClass("active");
                      
                    }
                    var $span = $("<span>");
                    if (user_have_rights_tab(tab)) {
                        $span.click((function (cur_tab) {
                            return function (event) {
                                activate_tab(event, cur_tab.Id);
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
                //Для первой страницы другие правила
                load_profile();
            }
            
        }

    }



    //Очистка предыдущего контента
    var clear_content = function () {
        $(".main_content").empty();
        INIT_FUNCT = {};
    }


    //Печать по умолчанию
    var set_default_print = function () {
        $('#print_btn').unbind('click');
        $('#print_btn').bind('click', function () {
            printSelector('#tab_content');
        }).removeClass('disabled');
    }

    //Назначение специального обработчика функции печати
    window.set_print_function = function (fn) {
        if (!fn)
            return;
        $('#print_btn').unbind('click');
        $('#print_btn').bind('click',fn).removeClass('disabled');
    }

    //Функция печати для профиля
    var set_profile_print = function () {
        set_print_function(function () {
            var name = $('title').text();
            var p_script = null;
            if ($(".chart_block").length>0) {
                var canvas = document.getElementById("myChart");
                var link1 = canvas.toDataURL();
                canvas = document.getElementById("myChart1");
                var link2 = canvas.toDataURL();
                var p_script_internal = [];
                p_script_internal.push("$('#myChart').remove();");
                p_script_internal.push("$('#myChart1').remove();");
                p_script_internal.push("$('#myChearImgPlacer').html('<img src=\"" + link1 + "\"/>');");
                p_script_internal.push("$('#myChearImgPlacer1').html('<img src=\"" + link2 + "\"/>');");
                p_script = "$(document).ready(function () {"+p_script_internal.join("")+"});"
                
            }
            $('.left_content_html').printElement({
                pageTitle: name + ".html", leaveOpen: false,
                printMode: 'iframe',
                script:p_script,
                overrideElementCSS: ['/Content/print.css', '/Content/iss/fontello/css/fontello.css']
            });
        });
    }

    

    window.set_xls_function = function (fn) {
        if (!fn)
            return;
        $('#xls_btn').bind('click', fn).removeClass('disabled');
    }

    window.set_pdf_function = function (fn) {
        if (!fn)
            return;
        $('#pdf_btn').bind('click', fn).removeClass('disabled');
    }

    window.set_wrd_function = function (fn) {
        if (!fn)
            return;
        $('#wrd_btn').bind('click', fn).removeClass('disabled');
    }

    //Загрузка содержимого табов
    var activate_tab = function (event, tab_id) {
        if (ltab) {
            tab_id = ltab;
            ltab = null;
        }
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
        $.ajax({
            url:"/tab/",
            cache: false,
            data:{
                "id": tab_id, "ticker": ISS, "PG": 1
            }
        })
        .done(function (data) {
            hideClock();
            if ($("#tabId").length == 0) $("#tab_content").after('<input type="hidden" id="tabId" value="' + tab_id + '"/>');
            $("#tab_content").html(data);                           
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            hideClock();
            //gotomenu(1);
            $("#tab_content").html(errorThrown);
        });
    }      

    //Загрузка профиля(для него отдельный скрипт)
    var load_profile = function () {
        clear_content();        
        //Удалим события повешанные на другие табы
        $('#xls_btn').unbind('click').addClass('disabled');
        $('#pdf_btn').unbind('click').addClass('disabled');
        $('#wrd_btn').unbind('click').addClass('disabled');
        showClock();
        var result = {};
        $.get("/issuers/modules/ProfileMain", { "ticker": ISS }, function (data) {
            result.left_content = data;
            $.get("/issuers/modules/ProfileRightMenu", { "ticker": ISS }).done(function (data) {                
                result.right_content = data;
                generate_profile_content(result);
                set_profile_print();
                cloud_block_init();
            }, "html")
        },"html")
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

    //Запуск дополнительных функций после загрузки меню
    var load_init_functions = function () {
        for (var fn in INIT_FUNCT) {
            INIT_FUNCT[fn]();
        }
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


    var format_message_error = function (message) {
        return "<span class=\'error\'>" + message + "</error>";
    }
    

    var cloud_block_switch = function (id) {
        var i = Number(id.replace("lb_", ""));
        var is_open = $("#" + id).find(".cloud_switcher_ar").hasClass("icon-angle-up");
        if (is_open) {
            $("#" + id).find(".cloud_switcher_ar").addClass("icon-angle-down").removeClass("icon-angle-up");
            $("#" + id).find(".extra_table").hide();
            state_arr[i] = 0;
        } else {
            $("#" + id).find(".cloud_switcher_ar").addClass("icon-angle-up").removeClass("icon-angle-down");
            $("#" + id).find(".extra_table").show();
            state_arr[i] = 1;
        }
        var curDate = new Date();
        curDate.setFullYear(curDate.getFullYear() + 1);
        CookieUtil.set(CLOUD_KEY, state_arr.join(','), curDate);

    }


    var cloud_block_render = function (arr) {
        if (arr == null || arr.length == 0) {
            return;
        }
        for (var i = 0; i < arr.length; i++) {
            var $lb = $('#lb_' + i);
            if (arr[i] == 1) {
                $lb.find(".cloud_switcher_ar").addClass("icon-angle-up").removeClass("icon-angle-down");
                $lb.find(".extra_table").show();
            } else {
                $lb.find(".cloud_switcher_ar").addClass("icon-angle-down").removeClass("icon-angle-up");
                $lb.find(".extra_table").hide();
            }
        }
    }

    var cloud_block_init = function () {
        if ($("#cloud_block").length == 0) {
            return;
        }
              
        //проверим значение в cookie
        var save_state = CookieUtil.get(CLOUD_KEY);
        if (save_state != null && save_state.split(',').length == 5) {
            state_arr = save_state.split(',');
        }

        cloud_block_render(state_arr);

        $('.cloud_switcher_header').click(function () {
            var id = $(this).closest('.link_block').attr('id');
            cloud_block_switch(id);
        })
    }


    window.bad_addr_check = function (e) {
        e.stopPropagation();
        var badrInterval;

        var show_bad_addr = function () {
            $.get("/Check/BadAddress/", { "ticker": ISS }, function (data) {
                var q_stat = data.QStatus;
                bad_addr_content = "";
                switch (q_stat) {
                    case 0:
                        clearInterval(badrInterval);
                        unblock_checkbutton('bad_addr_check');
                        complite_job("badr", "<div  onclick='bad_addr_check(event);'><span class='icon-search link ico'></span>Отсутствие ЮЛ по адресу, указанному в ЕГРЮЛ <span></span></div>");
                        var searchResult = data.SearchResult;
                        var ret;
                        var ogrn = "";
                        var inn = "";
                        var badr_row = "";
                        if (searchResult == "1") {
                            if (data.BadAddress != null) {
                                ret = JSON.parse(data.BadAddress);
                                ogrn = ret["OGRN"];
                                inn = ret["INN"];
                                var result = JSON.parse(ret["Result"]);
                                var row_found = result["rowsFound"];
                                if (row_found == 0) {
                                    bad_addr_content = '<br/><span class="minicaption">ОТСУТСТВИЕ ЮЛ ПО АДРЕСУ, УКАЗАННОМУ В ЕГРЮЛ</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                                    bad_addr_content += '<h4>Результат поиска по ОГРН ' + ogrn + ', ИНН ' + inn + ':</h4><hr/>';
                                    bad_addr_content += "<div class=\"check_not_found\">Информация в сведениях о юридических лицах, связь с которыми по указанному ими адресу (месту нахождения), внесенному в Единый государственный реестр юридических лиц, отсутствует, не найдена.</div>";
                                }
                                else {
                                    var rows = [];
                                    rows = result["rows"];
                                    for (i = 0 ; i < rows.length ; i++) {
                                        badr_row += '<tr><td>' + rows[i].OGRN + '</td><td>' + rows[i].INN + '</td><td>' + rows[i].NAMEUL + '</td><td>' + rows[i].ADRUL + '</td></tr>';
                                    }
                                    bad_addr_content = '<br/><span class="minicaption">ОТСУТСТВИЕ ЮЛ ПО АДРЕСУ, УКАЗАННОМУ В ЕГРЮЛ</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                                    bad_addr_content += '<h4>Результат поиска по ОГРН ' + ogrn + ', ИНН ' + inn + ':</h4><hr/>';
                                    bad_addr_content += '<table width="100%" class="data_table" cellspacing="0" cellpadding="0">';
                                    bad_addr_content += '<tr><th>ОГРН</th><th>ИНН</th><th>Полное наименование<br/>юридического лица</th><th>Адрес (местонахождение)<br/>юридического лица</th></tr>';
                                    bad_addr_content += badr_row;
                                    bad_addr_content += '</table>';
                                    bad_addr_content += "<div style='clear:both;height:50px'></div>";
                                }
                            }
                        }
                        show_dialog({ "content": bad_addr_content, "extra_style": "width:900px;", is_print: true });
                        break;
                    case 6:
                        clearInterval(badrInterval);
                        unblock_checkbutton('bad_addr_check');
                        complite_job("badr", format_message_error("ОТСУТСТВИЕ ЮЛ ПО АДРЕСУ, УКАЗАННОМУ В ЕГРЮЛ :<br/> Сервис временно не доступен, попробуйте зайти позже."));
                        break;
                    case 8:
                        clearInterval(badrInterval);
                        unblock_checkbutton('bad_addr_check');
                        complite_job("badr", format_message_error("ОТСУТСТВИЕ ЮЛ ПО АДРЕСУ, УКАЗАННОМУ В ЕГРЮЛ :<br/> Ваш уровень доступа не позволяет использование данной функцией."));
                        break;
                }
            });
        };

        
        if (bad_addr_content == "") {
            block_checkbutton('bad_addr_check');
            push_job("badr", "Отсутствие ЮЛ по адресу, указанному в ЕГРЮЛ ");
            badrInterval = setInterval(function () { show_bad_addr(); }, 5000);
            
        } else {
            show_dialog({ "content": bad_addr_content, "extra_style": "width:900px;", is_print: true });
        }

    }

    window.account_Check = function (e) {
        e.stopPropagation();
        var AccInterval;
        var show_Account = function () {
            $.get("/Check/Accounts/", { "ticker": ISS }, function (data) {
                var q_stat = data.QStatus;
                switch (q_stat) {
                    case 0:
                        complite_job("accounts", "<div onclick='account_Check(event);'><span class='icon-search link ico'></span>Решения о приостановлении операций по счетам<span></span></div>");
                        clearInterval(AccInterval);
                        unblock_checkbutton('account_check');

                        var IsFinded = data.IsFinded;
                        var ret;
                        var inn = "";

                        if (IsFinded == "1") {
                            var searchResult = data.SearchResult.replace(/\"/g, "\\\"");
                            ret = JSON.parse(data.SearchResult);
                            AC_text = "<br/><span class=\"minicaption\">Проверка решений о приостановлении операций по счетам</span>";
                            AC_text += "<table style=\"margin: 20px 0;border:none;\" width=\"100%\" cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tbody><tr></td><td width=\"50%\">Источник данных: ФНС России.</td><td width=\"50%\" align=\"right\">Дата и время запроса:  " + ret.check_date + "</td></tr></tbody></table>";
                            AC_text += "<h4>Сведения о запросе</h4>"
                            AC_text += "ИНН налогоплательщика: " + ret.inn + "<br/>";
                            if (ret.name) {
                                AC_text += "Наименование налогоплательщика: " + ret.name + "<br/>";
                            }
                            AC_text += "<h4>Результат запроса:</h4>";
                            AC_text += ret.result;
                            if (ret.scheta) {
                                AC_text += "<h4>Сведения о приостановлении операций по счетам налогоплательщиков:</h4>";
                                AC_text += "<table   class=\"data_table\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><th>Номер решения о приостановлении</th><th>Дата решения о приостановлении</th><th>Код налогового органа</th><th>БИК банка, в который направлено решение</th><th>Дата и время размещения информации в сервисе (Мск)</th></tr>";
                                var scheta = ret.scheta.sort(CompareForSort)
                                for (var i = 0; i < scheta.length; i++) {
                                    AC_text += "<tr><td>" + scheta[i].nom_post + "</td>";
                                    AC_text += "<td>" + scheta[i].dt_resh + "</td>";
                                    AC_text += "<td>" + scheta[i].no_kod + "</td>";
                                    AC_text += "<td>" + scheta[i].bik + "</td>";
                                    AC_text += "<td>" + scheta[i].dt_razm + "</td></tr>";
                                }
                                AC_text += "</table>"
                            }
                        }
                        show_dialog({ "content": AC_text, "extra_style": "width:900px;", is_print: true });
                        break;
                    case 6:
                        clearInterval(AccInterval);
                        unblock_checkbutton('account_check');
                        complite_job("accounts", format_message_error("Проверка решений о приостановлении операций по счетам <br/> Сервис временно не доступен, попробуйте зайти позже."));
                        break;
                    case 8:
                        clearInterval(AccInterval);
                        unblock_checkbutton('account_check');
                        complite_job("accounts", format_message_error("Проверка решений о приостановлении операций по счетам <br/> Ваш уровень доступа не позволяет использование данной функцией."));
                        break;
                }
            });
        };
        if (AC_text == "") {
            block_checkbutton('account_check');
            push_job("accounts", "Проверка решений о приостановлении операций по счетам ");
            AccInterval = setInterval(function () { show_Account(); }, 5000);
        } else {
            show_dialog({ "content": AC_text, "extra_style": "width:900px;", is_print: true });
        }

    }
    function CompareForSort(first, second) {
        var df = 0, ds = 0;
        if (first.dt_resh.split(".").length == 3) {
            var df = first.dt_resh.split(".")[2] + "" + first.dt_resh.split(".")[1] + "" + first.dt_resh.split(".")[0];
        }
        if (second.dt_resh.split(".").length == 3) {
            var ds = second.dt_resh.split(".")[2] + "" + second.dt_resh.split(".")[1] + "" + second.dt_resh.split(".")[0];
        }
        if (df == ds)
            return 0;
        if (df > ds)
            return -1;
        else
            return 1;
    }
    window.nalog_debt_check = function (e) {
        e.stopPropagation();
        var nalogdebtInterval;

        var show_nalog_debt = function () {
            $.get("/Check/NalogDebt/", { "ticker": ISS }, function (data) {
                var q_stat = data.QStatus;
                nalog_debt_content = "";

                switch (q_stat) {
                    case 0:
                        clearInterval(nalogdebtInterval);
                        unblock_checkbutton('nalog_debt_check');
                        complite_job("nalog_debt", "<div  onclick='nalog_debt_check(event);'><span class='icon-search link ico'></span>Задолженность по налогам<span></span></div>");

                        var searchResult = data.SearchResult;

                        var ret;
                        var inn = "";

                        if (searchResult == "1") {
                            if (data.NalogDebt != null) {
                                var nalog = JSON.parse(data.NalogDebt);
                                var result = JSON.parse(nalog.Result);
                                var text = result.text;
                                nalog_debt_content = "<br/><table width=\"100%\" cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tbody><tr></td><td width=\"50%\">Источник данных: ФНС России.</td><td width=\"50%\" align=\"right\">Дата выгрузки результатов поиска:  " + result.date + "</td></tr></tbody></table><br/>";
                                nalog_debt_content += "<div class=\"warningbox\" style=\"display: block;padding-bottom:30px;\">" + text + "</div><br/><br/>";
                            }
                        }
                        show_dialog({ "content": nalog_debt_content, "extra_style": "width:900px;", is_print: true });
                        break;
                    case 6:
                        clearInterval(nalogdebtInterval);
                        unblock_checkbutton('nalog_debt_check');
                        complite_job("nalog_debt", format_message_error("Задолженность по уплате налогов: <br/> Сервис временно не доступен, попробуйте зайти позже."));
                        break;
                    case 8:
                        clearInterval(nalogdebtInterval);
                        unblock_checkbutton('nalog_debt_check');
                        complite_job("nalog_debt", format_message_error("Задолженность по уплате налогов :<br/> Ваш уровень доступа не позволяет использование данной функцией."));
                        break;
                }
            });
        };


        if (nalog_debt_content == "") {
            block_checkbutton('nalog_debt_check');
            push_job("nalog_debt", "Задолженность по уплате налогов");
            nalogdebtInterval = setInterval(function () { show_nalog_debt(); }, 5000);
        } else {
            show_dialog({ "content": nalog_debt_content, "extra_style": "width:900px;", is_print: true });
        }

    }

    window.mass_reg_check = function (e) {
        e.stopPropagation();

        var massInterval;

        var show_mass_reg=function () {
            $.get("/MassReg/OgrnSearch/", { "ticker": ISS }, function (data) {
                var q_stat = data.QStatus;
                mass_reg_content = "";
                switch (q_stat) {
                    case 0:
                        clearInterval(massInterval);
                        unblock_checkbutton('mass_reg_check');
                        complite_job("massreg", "<div  onclick='mass_reg_check(event);'><span class='icon-search link ico'></span>Адреса массовой регистрации<span></span></div>");
                        var searchResult = data.SearchResult;
                        var origAddress = data.OriginalAddress;
                        //Обрежем исходный адрес до дома
                        var pos = 0;
                        for (var i = 0; i < 7; i++) {
                            pos = origAddress.indexOf(",", pos + 1);
                        }
                        origAddress = origAddress.substring(0, pos);                        
                        if (searchResult == "1") {
                            var street = data.Street;
                            var address_row = "";
                            var i_count = 0;
                            $.each(data.Items, function (entryIndex, entry) {
                                var address = "";
                                var house = entry['House'];
                                if (house != null) {
                                    house = "дом " + house;
                                    address += house;
                                }
                                var corp = entry['Korpus'];
                                if (corp != null) {
                                    corp = " корпус " + corp;
                                    address += corp;
                                }
                                var flat = entry['Flat'];
                                if (flat != null) {
                                    flat = " кв. " + flat;
                                    address += flat;
                                }
                                var rcount = entry['RegCount'];
                                i_count++;
                                address_row += '<tr><td style="padding-left:5px">' + street + ' ' + address + '</td><td align="center">' + rcount + '</td></tr>';
                            });



                            mass_reg_content = '<br/><span class="minicaption">АДРЕСА МАССОВОЙ РЕГИСТРАЦИИ</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                            mass_reg_content += '<h4>Адрес по которому искали: ' + origAddress + '</h4><hr>';
                            mass_reg_content += '<table class="data_table" width="100%" cellspacing="0" cellpadding="0"><tr>';
                            mass_reg_content += '<tr><th>Адрес</th><th>Количество ЮЛ</th></tr>';
                            mass_reg_content += address_row;
                            mass_reg_content += '</table>';
                        }
                        if (searchResult == "0") {
                            mass_reg_content = '<br/><span class="minicaption">АДРЕСА МАССОВОЙ РЕГИСТРАЦИИ</span><table width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                            mass_reg_content += '<h4>Адрес по которому искали: ' + origAddress + '</h4><hr>';
                            mass_reg_content += "<div class=\"check_not_found\">По адресу не  найдены  записи массовых регистраций</div>";
                        }
                        show_dialog({ "content": mass_reg_content, "extra_style": "width:900px;", is_print: true });
                        break;
                    case 2:
                    case 4:
                    case 5:
                    case 7:
                        clearInterval(massInterval);
                        unblock_checkbutton('mass_reg_check');
                        complite_job("massreg", format_message_error("АДРЕСА МАССОВОЙ РЕГИСТРАЦИИ:<br/> Адрес юридического лица не позволяет произвести<br/> автоматический поиск в реестре адресов массовой регистрации."));
                        break;
                    case 3:
                    case 6:
                        clearInterval(massInterval);
                        unblock_checkbutton('mass_reg_check');
                        complite_job("massreg", format_message_error("АДРЕСА МАССОВОЙ РЕГИСТРАЦИИ:<br/> Сервис временно не доступен, попробуйте зайти позже."));
                        break;
                }
            });
        };


        if (mass_reg_content == "") {
            block_checkbutton('mass_reg_check');
            push_job("massreg", "Адреса массовой регистрации");
            massInterval = setInterval(function () { show_mass_reg();}, 5000);
        }else{
            show_dialog({ "content": mass_reg_content, "extra_style": "width:900px;", is_print: true });
        }
         

    };

    window.disfind_check = function (e) {
        e.stopPropagation();

        var disInterval;

        var show_disfind = function () {
            $.get("/Check/DisFind/", { "ticker": ISS }, function (data) {
                var is_finded = data.IsFinded;
                var error_type = data.ErrorType;
                disfind_content = "";
                if (is_finded == 0) {
                    clearInterval(disInterval);
                    unblock_checkbutton('disfind_check');
                    complite_job("disfind", "<div  onclick='disfind_check(event);'><span class='icon-search link ico'></span>Наличие дисквалифицированных лиц в исполнительном органе.<span></span></div>");
                    disfind_content += '<br/><span class="minicaption">НАЛИЧИЕ ДИСКВАЛИФИЦИРОВАННЫХ ЛИЦ В ИСПОЛНИТЕЛЬНОМ ОРГАНЕ</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                    disfind_content += '<h4>Результат поиска по ОГРН ' + data.SearchOgrn + ':</h4>';
                    disfind_content += "<div class=\"check_not_found\">Информация о наличии в составе исполнительных органов организации<br/> дисквалифицированных лиц не найдена.</div>";
                    show_dialog({ "content": disfind_content, "extra_style": "width:900px;", is_print: true });
                }
                if (is_finded == 1 || (is_finded == -1 && error_type == 1)) {
                    clearInterval(disInterval);
                    unblock_checkbutton('disfind_check');
                    complite_job("disfind", "<div  onclick='disfind_check(event);'><span class='icon-search link ico' id='top_egrul'></span>Наличие дисквалифицированных лиц в исполнительном органе.<span></span></div>");
                    disfind_content += '<br/><span class="minicaption">НАЛИЧИЕ ДИСКВАЛИФИЦИРОВАННЫХ ЛИЦ В ИСПОЛНИТЕЛЬНОМ ОРГАНЕ</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                    disfind_content += '<h4>Результат поиска по ОГРН ' + data.SearchOgrn + ':</h4>';
                    disfind_content += "<div class=\"check_not_found\">Найдена запись о наличии в составе исполнительных органов организации<br/> дисквалифицированных лиц.</div>";
                    disfind_content += '<table class="data_table" style="width:90%" cellspacing="0" cellpadding="0" border="0">';
                    disfind_content += '<tr><th width="25%">Наименование</th><td>' + data.Name + '</td></tr>';
                    disfind_content += '<tr><th width="25%">ОГРН</th><td>' + data.Ogrn + '</td></tr>';
                    if (is_finded == 1) {
                        disfind_content += '<tr><th width="25%">ИНН</th><td>' + data.Inn + '</td></tr>';
                    } else {
                        disfind_content += '<tr><th width="25%">ИНН</th><td style="color:red;">' + data.Inn + '*</td></tr>';
                    }
                    disfind_content += '<tr><th width="25%">КПП</th><td>' + data.Kpp + '</td></tr>';
                    disfind_content += '<tr><th width="25%">Адрес</th><td>' + data.Address + '</td></tr></table>';
                    if (is_finded == -1) {
                        disfind_content += "<div style='clear:both;color:red;padding-top: 20px;'>* - Внимание! Код ИНН организации в результатах поиска не соответствует коду ИНН в профиле организации.</div>";
                    }
                    disfind_content += "<div style='clear:both;height:50px'></div>";
                    show_dialog({ "content": disfind_content, "extra_style": "width:900px;", is_print: true });
                }
                if (is_finded == -1 && error_type != 1) {
                    clearInterval(disInterval);
                    unblock_checkbutton('disfind_check');
                    complite_job("disfind", format_message_error("НАЛИЧИЕ ДИСКВАЛИФИЦИРОВАННЫХ ЛИЦ:<br/> Сервис временно не доступен, попробуйте зайти позже."));
                }      
            });
        };


        if (disfind_content == "") {
            block_checkbutton('disfind_check');
            push_job("disfind", "Наличие дисквалифицированных лиц в исполнительном органе");
            disInterval = setInterval(function () { show_disfind(); }, 5000);
        } else {
            show_dialog({ "content": disfind_content, "extra_style": "width:900px;", is_print: true });
        }


    };

    window.ShowUnfairInn = function (inn) {
        $.get("/Message/ShowUnfair/", { "inn": inn }, function (data) {
            show_dialog({ "content": data, "extra_style": "width:800px;", is_print: true });
        });
    }

    window.ShowFas = function (ticker) {
        $.get("/Message/ShowFas/", { "ticker": ticker }, function (data) {
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
                        complite_job("docr", "<div  onclick='docreg_check(event);'><span class='icon-search link ico'></span>Сведения о представлении ЮЛ заявлений для гос.регистрации в ФНС<span></span></div>");
                        var result = JSON.parse(data.Result);
                        if (result.rows.length==0) {
                            docreg_content = '<br/><span class="minicaption">СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ЮЛ ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                            docreg_content += '<h4>Результат поиска по ОГРН ' + data.Ogrn + ':</h4><hr/>';
                            docreg_content += "<div class=\"check_not_found\">Сведения о представлении в регистрирующий (налоговый) орган документов за последний год отсутствуют.</div>";
                        } else {
                            docreg_content = '<br/><span class="minicaption">СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ЮЛ ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС</span><table style="margin: 20px 0;" width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                            docreg_content += '<h4>Результат поиска по ОГРН ' + data.Ogrn + ':</h4><hr/>';
                            docreg_content += '<table width="100%" class="data_table" cellspacing="0" cellpadding="0">';
                            docreg_content += '<tr><th>Сведения о юридическом лице</th><th>Сведения о поданном заявлении</th><th>Сведения о представлении документов</th><th>Наименование налогового органа</th><th>Сведения о решении</th></tr>';
                            docreg_content += get_result_content(result.rows);
                            docreg_content += '</table>';
                            docreg_content += "<div style='clear:both;height:50px'></div>";
                        }                        
                        show_dialog({ "content": docreg_content, "extra_style": "width:900px;", is_print: true });
                        break;
                    case 6:
                        clearInterval(docInterval);
                        unblock_checkbutton('docreg_check');
                        complite_job("docr", format_message_error("СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ЮЛ ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС:<br/> Сервис временно не доступен, попробуйте зайти позже."));
                        break;
                    case 8:
                        clearInterval(docInterval);
                        unblock_checkbutton('docreg_check');
                        complite_job("docr", format_message_error("СВЕДЕНИЯ О ПРЕДСТАВЛЕНИИ ЮЛ ЗАЯВЛЕНИЙ ДЛЯ ГОС.РЕГИСТРАЦИИ В ФНС:<br/> Ваш уровень доступа не позволяет использование данной функцией."));
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
            push_job("docr", "Сведения о представлении ЮЛ заявлений для гос.регистрации в ФНС");
            docInterval = setInterval(function () { show_docreg(); }, 5000);
        } else {
            show_dialog({ "content": docreg_content, "extra_style": "width:900px;", is_print: true });
        }

    }

    window.get_paid_egrul = function (e,is_new) {
        var elem = e.currentTarget;
        e.stopPropagation();
        var egrulInterval;

        var stop_loading_icon = function (is_new) {            
            $('#payegrulcontainer_result').removeClass('disabled');
            if (is_new) {
                $('#payegrulcontainer_request').remove();
            } else {
                $('#payegrulcontainer_request span').removeClass('animate-spin');
            }
        };

        var start_loading_icon = function (is_new) {
            $(elem).unbind();
            $('#payegrulcontainer_result').addClass('disabled');
            if (is_new) {
                $('#payegrulcontainer').append('<span id="payegrulcontainer_request"  style="font-size:18px;"><span class="icon-spin3 word ico"></span></span>');
            }
            $('#payegrulcontainer_request span').addClass('animate-spin');
        };

        var generateEgrulError = function (error_message,is_new) {
            if (egrulInterval) {
                clearInterval(egrulInterval);
            }            
            stop_loading_icon(is_new);
            show_dialog({ "content": error_message });
            $(elem).click(function (event) {
                get_paid_egrul(event);
            });
        };

        var generateEgrulContent = function (ogrn) {
            var dtObj = new DateObj();
            var cur_date = dtObj.dd + '.' + dtObj.mm + '.' + dtObj.yyyy
            var inner_text = "<a id=\"payegrulcontainer_result\" href=\"#\" onclick=\"getRTF('/Report/Egrul/" + ogrn + "');\"><span class=\"icon-file-word word ico\"></span><span>Выписка МИ ФНС России по ЦОД от " + cur_date + "</span></a>";
            $('#payegrulcontainer').html(inner_text);
        }

        var generatePaidEgrul = function (is_new) {
            $.get("/Report/AskEgrulByTicker/", { "ticker": ISS }, function (data) {
                var egrul_status = data.status;
                switch (egrul_status) {
                    case 5:
                        generateEgrulError("Вами превышен дневной лимит на заказ Выписок из ЕГРЮЛ.",is_new);
                        break;
                    case 4:
                        generateEgrulError("Выписка из ЕГРЮЛ по данной компании временно не доступна, попробуйте зайти позже.",is_new);
                        break;
                    case 3:
                        generateEgrulError("Сервис заказа Выписок из ЕГРЮЛ временно не доступен, попробуйте зайти позже.",is_new);
                        break;
                    case 2:
                        generateEgrulError("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.",is_new);
                        break;
                    case 0:
                        if (egrulInterval) {
                            clearInterval(egrulInterval);
                        }
                        stop_loading_icon(is_new);
                        generateEgrulContent(data.ogrn);
                        break;
                }
            });
        };

        start_loading_icon(is_new);
        egrulInterval = setInterval(function () { generatePaidEgrul(is_new); }, 5000);
        
    }

    var block_checkbutton = function (id) {
//        $('#' + id).find('.ico').removeClass('icon-search').addClass('icon-spin3').addClass('animate-spin');
        $('#' + id).addClass('disabled');
    }

    var unblock_checkbutton = function (id) {
        $('#' + id).find('.ico').addClass('icon-search').removeClass('icon-spin3').removeClass('animate-spin');
        $('#' + id).removeClass('disabled');
    }

    window.printSelector = function (selector) {
        var name = $('title').text();
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

    window.gotomenu = function (id, tid) {
        ltab = tid;
        $('#mn' + id).click();
    }

           

})();

