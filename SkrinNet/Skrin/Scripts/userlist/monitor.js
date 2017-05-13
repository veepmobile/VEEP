(function () {

    var group_list = [];
    var message_list = [];

    var multiple_options = {
        selectAllText: 'Все',
        allSelected: 'Все',
        countSelected: '# из %',
        selectAllDelimiter: ['', '']
    }

    //Контейнер для генерации селектора групп  пользователя
    function Selector(el_id, select_id) {
        //id селектора
        this.el_id = el_id;
        //значение селектора
        this.select_id = select_id;
    }

    var sector = Number($('input[name=monitor_sector]:checked').val());


    var getSelector = function (sector,use_tr) {
        switch (sector) {
            case 1:
                return use_tr ? "#monitor_news_table_body tr" : "#monitor_news_table_body";
            case 3:
                return use_tr ? "#monitor_message_table_body tr" : "#monitor_message_table_body .upd";
            case 4:
                return use_tr ? "#monitor_egrul_table_body tr" : "#monitor_egrul_table_body .upd";
        }
    }



    //#region monitor_update

   

    //Генерация селектора групп пользователя
    var init_select = function (selector_arr) {
        $.post("/UserLists/GroupList/", null, function (data) {
            group_list = data;
            //Удалим старые значения
            for (var i = 0; i < selector_arr.length; i++) {
                var selector = selector_arr[i];
                $('#' + selector.el_id + ' option[value!=0]').remove();
                $.each(group_list, function (i, item) {
                    $('#' + selector.el_id).append($('<option>', {
                        value: item.id,
                        text: item.name + " (" + item.cnt_disp + ")"
                    }));
                });
                $('#' + selector.el_id).val(selector.select_id);
            }
        });       
    }

    //Загрузка данных о мониториге обновлений
    var load_update_monitor_info = function () {
        showClock();
        $('.error_list').html('');
        $('#monitor_news_table_body').html('');
        $('#btn_Update_Save').addClass("hidden").unbind();
        $.post("/MonitorOperations/GetUpdateMonitorInfo/", null, function (data) {
            hideClock();
            if (data.length < update_limit) {
                $('#btn_Update_Add').removeClass('hidden').unbind().click(function () { add_new_row(); });
            }
            if (data.length == 0) {
                add_new_row();
            } else {
                var selector_arr = [];
                for (var i = 0; i < data.length; i++) {
                    generate_table_row(i+1, data[i].email, data[i].id);
                    selector_arr.push(new Selector("groups_list" + (i + 1), data[i].id));
                }
                init_select(selector_arr);
            }
            //Добавим возможность отбновления ссылок на группы
            $('.group_list').change(function () {
                var row_num = this.id.replace("groups_list", "");//последний символ из id;
                console.log(row_num);
                $('#edit_group_' + row_num).attr('href', '/userlists/group/?id=' + this.value);
            });            
            bind_checkboxes();

        }, "json");
    }

    //Привязка событий к чекбоксам
    var bind_checkboxes = function () {
        var selector = getSelector(sector, false);// sector == 1 ? "#monitor_news_table_body" : "#monitor_message_table_body .upd";
        $(selector + " input:checkbox").unbind();
        $('#selallbox').unbind("change");
        $(selector + " input:checkbox").click(function () { checkOnOff(); })
        $(selector + " input:checkbox").add("#selallbox").change(function () {
            $('#btn_delete').addClass("disabled").unbind();
            var any_checked = $(selector).find('input:checkbox:checked').length > 0;
            if (any_checked) {
                $('#btn_delete').removeClass("disabled").click(function () { delete_rows(); });
            }
        });
    }

    //Удаление строк
    var delete_rows = function () {
        confirm_dialog("<h3>Вы действительно хотите удалить данную подписку?</h3>", function () {
            close_dialog();
            var selector = getSelector(sector, false); //sector == 1 ? "#monitor_news_table_body" : "#monitor_message_table_body .upd";
            $(selector).find('input:checkbox:checked').each(function () {
                var row_num = this.id.replace("upd_cbx_", "");
                $('#upd_' + row_num).remove();
                //monitor_update_changed();
                switch (sector) {
                    case 1:
                        update_subcription_info();
                        break;
                    case 3:
                        update_message_subcription_info();
                        break;
                    case 4:
                        update_egrul_group_subcription_info();
                        break;
                }
            });
        });
    }


    //Добавление пустой строки
    var add_new_row=function(){
        monitor_update_changed();
        var i=get_new_i();
        generate_table_row(i,"",0);
        var selector_arr=[new Selector("groups_list"+i,0)];
        init_select(selector_arr);
        if (get_row_count() == update_limit) {
            $('#btn_Update_Add').addClass('hidden').unbind();
        }
        bind_checkboxes();
    }

    //Общее кол-во записей
    var get_row_count = function () {
        var selector = getSelector(sector, true);// sector == 1 ? "#monitor_news_table_body tr" : "#monitor_message_table_body tr";
        return $(selector).length;
    }

    //Полчение нового значения идентификатора строки
    var get_new_i = function () {
        var ids = [];
        var selector = getSelector(sector, true);// sector == 1 ? "#monitor_news_table_body tr" : "#monitor_message_table_body tr";
        $(selector).each(function () {
            var id = Number(this.id.replace("upd_", ""));
            ids.push(id);
        });
        if (ids.length == 0)
            return 1;
        return Math.max.apply(Math, ids) + 1;
    }

    //Шаблон генерации строки обновлений
    var generate_table_row = function (i, email, group_id) {
        var row = [];
        row.push("<tr id=\"upd_"+i+"\">");
        row.push("<td><input type=\"checkbox\" id=\"upd_cbx_" + i + "\" /></td>");
        row.push("<td><input id=\"email" + i + "\" class=\"search_input\" onkeypress=\"monitor_update_changed();\" type=\"text\" placeholder=\"E-mail для отсылки уведомлений\" value=\"" + email + "\"></td>");
        row.push("<td><select id=\"groups_list" + i + "\" class=\"search_input group_list\" onchange=\"monitor_update_changed();\"><option value=\"0\">Выберите группу</option></select></td>");
        row.push("<td><a id=\"edit_group_"+i+"\" href=\"/userlists/group/?id=" + group_id + "\" class=\"edit_group\">Редактировать<br />группу</a></td>");
        var $row = $(row.join(''));
        $('#monitor_news_table_body').append($row);
    };

    //Обновление данных на сервере
    var update_subcription_info = function () {
        var records = [];
        $('#monitor_news_table_body tr').each(function () {
            var id = Number(this.id.replace("upd_", ""));
            var record = {};
            record.email = $('#email' + id).val();
            record.id = $('#groups_list' + id).val();
            records.push(record);
        });
        var errors = [];
        //проведем валидацию
        for (var i = 0; i < records.length; i++) {
            if (records[i].id == 0) {
                errors.push("Не выбрана группа для мониторинга");
            }
            if (records[i].email == "") {
                errors.push("Не задан email для мониторинга");
                continue;
            }
            var Re = /^((([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))($|\s*;\s*|\s*\,\s*))+/;
            var check_email = records[i].email.replace(Re, "#");
            if (check_email != "#") {
                errors.push("Не правильный формат email для мониторинга");
                continue;
            }
        }
        if (errors.length > 0) {
            var ul = ["<ul>"];
            for (var i = 0; i < errors.length; i++) {
                ul.push("<li>" + errors[i] + "</li>");
            }
            ul.push("</ul>");
            $('.error_list').html(ul.join(""));
        } else {
            var si = JSON.stringify({ 'si': records });

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '/MonitorOperations/AddGroupForUpdate/',
                data: si,
                success: function () {
                    load_update_monitor_info();
                }
            });

        }

    }

    //Включение функции сохрания данных
    window.monitor_update_changed = function (event) {
        var $btn_save= $('#btn_Update_Save');
        if (!$btn_save.hasClass("hidden"))
            return;
        switch (sector) {
            case 1:
                $btn_save.removeClass("hidden").click(function () {
                    if (update_limit > 0) {
                        update_subcription_info();
                    } else {
                        no_rights();
                    }                    
                });
                break;
            case 3:
                $btn_save.removeClass("hidden").click(function () {
                    if (message_limit > 0) {
                        update_message_subcription_info();
                    } else {
                        no_rights();
                    }                    
                });
                break;
            case 4:
                $btn_save.removeClass("hidden").click(function () {
                    if (egrul_group_limit > 0) {
                        update_egrul_group_subcription_info();
                    } else {
                        no_rights();
                    }
                });
                break;
        }
       
    };

    //Отметить все чекбоксы
    window.doSetCheckedAll = function (chb) {
        var selector = getSelector(sector, false);// sector == 1 ? "#monitor_news_table_body" : "#monitor_message_table_body .upd";
        $(selector).find("input:checkbox").each(function (i) {
            this.checked = chb.checked;
        });

    };

    //Проверка - нажаты ли все чекбокы
    var checkOnOff = function () {
        var selector = getSelector(sector, false);// sector == 1 ? "#monitor_news_table_body" : "#monitor_message_table_body .upd";
        var allCheckboxes = $(selector).find('input:checkbox');
        var checkedCheckboxes = $(selector).find('input:checkbox:checked');
        var allChecked = allCheckboxes.length == checkedCheckboxes.length;
        $("#selallbox").get(0).checked = allChecked;
    }

    //#endregion monitor_update



    //#region monitor_message

    //Загрузка данных о мониториге сообщений
    var load_message_monitor_info = function () {
        showClock();
        $('.error_list').html('');
        $('#monitor_message_table_body').html('');
        $('#btn_Update_Save').addClass("hidden").unbind();
        $.post("/MonitorOperations/GetMessageMonitorGroup/", null, function (data) {
            hideClock();
            if (data.length < message_limit) {
                $('#btn_Update_Add').removeClass('hidden').unbind().click(function () { add_new_message_row(); });
            }
            if (data.length == 0) {
                add_new_message_row();
            } else {
                var selector_arr = [];
                var selector_group_arr = [];
                for (var i = 0; i < data.length; i++) {
                    generate_message_table_row(i + 1, data[i].email, data[i].group_id);
                    selector_arr.push(new Selector("groups_list" + (i + 1), data[i].group_id));
                    selector_group_arr.push(new Selector("mt_list" + (i + 1), data[i].message_types))
                }
                init_select(selector_arr);
                init_group_select(selector_group_arr);
            }
            //Добавим возможность отбновления ссылок на группы
            $('.group_list').change(function () {
                var row_num = this.id.replace("groups_list", "");//последний символ из id;
                console.log(row_num);
                $('#edit_group_' + row_num).attr('href', '/userlists/group/?id=' + this.value);
            });
            bind_checkboxes();

        }, "json");
    }

    //Шаблон генерации строки сообщений
    var generate_message_table_row = function (i, email, group_id) {
        var row = [];
        row.push("<tr id=\"upd_" + i + "\">");
        row.push("<td class=\"upd\"><input  type=\"checkbox\" id=\"upd_cbx_" + i + "\" /></td>");
        row.push("<td><input id=\"email" + i + "\" class=\"search_input\" onkeypress=\"monitor_update_changed();\" type=\"text\" placeholder=\"E-mail для отсылки уведомлений\" value=\"" + email + "\"></td>");
        row.push("<td><select id=\"groups_list" + i + "\" class=\"search_input group_list\" onchange=\"monitor_update_changed();\"><option value=\"0\">Выберите группу</option></select></td>");
        row.push("<td><select multiple=\"multiple\" id=\"mt_list" + i + "\" class=\"search_input mt_list\"></select></td>");
        row.push("<td><a id=\"edit_group_" + i + "\" href=\"/userlists/group/?id=" + group_id + "\" class=\"edit_group\">Редактировать<br />группу</a></td>");
        var $row = $(row.join(''));
        $('#monitor_message_table_body').append($row);
    };

    //Добавление пустой строки
    var add_new_message_row = function () {
        monitor_update_changed();
        var i = get_new_i();
        generate_message_table_row(i, "", 0);
        var selector_arr = [new Selector("groups_list" + i, 0)];
        var selector_group_arr = [new Selector("mt_list" + i, "")];
        init_select(selector_arr);
        init_group_select(selector_group_arr);
        if (get_row_count() == message_limit) {
            $('#btn_Update_Add').addClass('hidden').unbind();
        }
        bind_checkboxes();
    }


    //Генерация селектора групп пользователя
    var init_group_select = function (selector_arr) {
        $.post("/MonitorOperations/GetMessageTypes/", null, function (data) {
            message_list = data;
            //Удалим старые значения
            for (var i = 0; i < selector_arr.length; i++) {
                var selector = selector_arr[i];
                //Выделеных элементов может быть много;
                var select_ids = selector.select_id.split(",");
                var opts = [];
                $.each(message_list, function (i, item) {
                    var opt = $.inArray(String(item.id), select_ids) >= 0 ? "<option selected=\"selected\" " : "<option ";
                    opt += " value=\"" + item.id + "\">" + item.name + "</option>";
                    opts.push(opt);
                });
                $('#' + selector.el_id).html(opts.join("")).multipleSelect(multiple_options);
                $('#' + selector.el_id).change(function () {
                    monitor_update_changed();
                })
            }
        });
    }

    //Обновление данных на сервере
    var update_message_subcription_info = function () {
        var records = [];
        var j = 0;
        $('#monitor_message_table_body tr').each(function () {
            j++;
            var id = Number(this.id.replace("upd_", ""));
            var record = {};
            record.i = j;
            record.email = $('#email' + id).val();
            record.group_id = $('#groups_list' + id).val();
            record.mt = $('#mt_list' + id).multipleSelect('getSelects');
            records.push(record);
        });
        var errors = [];
        //проведем валидацию
        for (var i = 0; i < records.length; i++) {
            if (records[i].group_id == 0) {
                errors.push("Не выбрана группа для мониторинга");
            }
            if (records[i].mt.length == 0) {
                errors.push("Не выбраны типы сообщений");
            }
            if (records[i].email == "") {
                errors.push("Не задан email для мониторинга");
                continue;
            }
            var Re = /^((([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))($|\s*;\s*|\s*\,\s*))+/;
            var check_email = records[i].email.replace(Re, "#");
            if (check_email != "#") {
                errors.push("Не правильный формат email для мониторинга");
                continue;
            }
        }
        if (errors.length > 0) {
            var ul = ["<ul>"];
            for (var i = 0; i < errors.length; i++) {
                ul.push("<li>" + errors[i] + "</li>");
            }
            ul.push("</ul>");
            $('.error_list').html(ul.join(""));
        } else {
            var si = JSON.stringify({ 'si': records });

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '/MonitorOperations/AddGroupForMessType/',
                data: si,
                success: function () {
                    load_message_monitor_info();
                }
            });

        }

    }


    //#endregion monitor_message


    //#region monitor_egrul_group

    //Загрузка данных о мониториге ЕГРЮЛ
    var load_egrul_group_monitor_info = function () {
        showClock();
        $('.error_list').html('');
        $('#monitor_egrul_table_body').html('');
        $('#btn_Update_Save').addClass("hidden").unbind();
        $.post("/MonitorOperations/GetEgrulMonitorGroup/", null, function (data) {
            hideClock();
            if (data.length < egrul_group_limit) {
                $('#btn_Update_Add').removeClass('hidden').unbind().click(function () { add_new_egrul_group_row(); });
            }
            if (data.length == 0) {
                add_new_egrul_group_row();
            } else {
                var selector_arr = [];
                var selector_group_arr = [];
                for (var i = 0; i < data.length; i++) {
                    generate_egrul_group_table_row(i + 1, data[i].email, data[i].group_id);
                    selector_arr.push(new Selector("groups_list" + (i + 1), data[i].group_id));
                    selector_group_arr.push(new Selector("et_list" + (i + 1), data[i].egrul_types))
                }
                init_select(selector_arr);
                init_egrul_group_select(selector_group_arr);
            }
            //Добавим возможность отбновления ссылок на группы
            $('.group_list').change(function () {
                var row_num = this.id.replace("groups_list", "");//последний символ из id;
                console.log(row_num);
                $('#edit_group_' + row_num).attr('href', '/userlists/group/?id=' + this.value);
            });
            bind_checkboxes();

        }, "json");
    }

    //Шаблон генерации строки сообщений
    var generate_egrul_group_table_row = function (i, email, group_id) {
        var row = [];
        row.push("<tr id=\"upd_" + i + "\">");
        row.push("<td class=\"upd\"><input  type=\"checkbox\" id=\"upd_cbx_" + i + "\" /></td>");
        row.push("<td><input id=\"email" + i + "\" class=\"search_input\" onkeypress=\"monitor_update_changed();\" type=\"text\" placeholder=\"E-mail для отсылки уведомлений\" value=\"" + email + "\"></td>");
        row.push("<td><select id=\"groups_list" + i + "\" class=\"search_input group_list\" onchange=\"monitor_update_changed();\"><option value=\"0\">Выберите группу</option></select></td>");
        row.push("<td><select multiple=\"multiple\" id=\"et_list" + i + "\" class=\"search_input mt_list\"></select></td>");
        row.push("<td><a id=\"edit_group_" + i + "\" href=\"/userlists/group/?id=" + group_id + "\" class=\"edit_group\">Редактировать<br />группу</a></td>");
        var $row = $(row.join(''));
        $('#monitor_egrul_table_body').append($row);
    };

    //Добавление пустой строки
    var add_new_egrul_group_row = function () {
        monitor_update_changed();
        var i = get_new_i();
        generate_egrul_group_table_row(i, "", 0);
        var selector_arr = [new Selector("groups_list" + i, 0)];
        var selector_group_arr = [new Selector("et_list" + i, "")];
        init_select(selector_arr);
        init_egrul_group_select(selector_group_arr);
        if (get_row_count() == egrul_group_limit) {
            $('#btn_Update_Add').addClass('hidden').unbind();
        }
        bind_checkboxes();
    }

    //Генерация селектора групп пользователя
    var init_egrul_group_select = function (selector_arr) {
        $.post("/MonitorOperations/GetEgrulTypes/", null, function (data) {
            message_list = data;
            //Удалим старые значения
            for (var i = 0; i < selector_arr.length; i++) {
                var selector = selector_arr[i];
                //Выделеных элементов может быть много;
                var select_ids = selector.select_id.split(",");
                var opts = [];
                $.each(message_list, function (i, item) {
                    var opt = $.inArray(String(item.id), select_ids) >= 0 ? "<option selected=\"selected\" " : "<option ";
                    opt += " value=\"" + item.id + "\" isIP=\"" + item.isIP + "\">" + item.name + "</option>";
                    opts.push(opt);
                });
                $('#' + selector.el_id).html(opts.join("")).multipleSelect(multiple_options);
                $('#' + selector.el_id).change(function () {
                    monitor_update_changed();
                })
            }
        });
    }

    //Обновление данных на сервере
    var update_egrul_group_subcription_info = function () {
        var records = [];
        var j = 0;
        $('#monitor_egrul_table_body tr').each(function () {
            j++;
            var id = Number(this.id.replace("upd_", ""));
            var record = {};
            record.id = j;
            record.email = $('#email' + id).val();
            record.group_id = $('#groups_list' + id).val();
            //record.egrul_types = $('#et_list' + id).multipleSelect('getSelects').join();
            record.isIP = "";
            var aEGRUL = new Array()
            var aEGRIP = new Array()
            $('#et_list' + id).find("option:selected").each(
                function () {
                    if ($(this).attr("isip") == 0) {
                        aEGRUL.push($(this).val())
                    }
                    if ($(this).attr("isip") == 1) {
                        aEGRIP.push($(this).val())
                    }
                }
                );
            record.egrul_types = aEGRUL.join();
            record.egrip_types = aEGRIP.join();
            records.push(record);
        });
        var errors = [];
        //проведем валидацию
        for (var i = 0; i < records.length; i++) {
            if (records[i].group_id == 0) {
                errors.push("Не выбрана группа для мониторинга");
            }
            if (records[i].egrul_types == "" && records[i].egrip_types == "") {
                errors.push("Не выбраны типы записей");
            }
            if (records[i].email == "") {
                errors.push("Не задан email для мониторинга");
                continue;
            }
            var Re = /^((([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))($|\s*;\s*|\s*\,\s*))+/;
            var check_email = records[i].email.replace(Re, "#");
            if (check_email != "#") {
                errors.push("Не правильный формат email для мониторинга");
                continue;
            }
        }
        if (errors.length > 0) {
            var ul = ["<ul>"];
            for (var i = 0; i < errors.length; i++) {
                ul.push("<li>" + errors[i] + "</li>");
            }
            ul.push("</ul>");
            $('.error_list').html(ul.join(""));
        } else {
            var si = JSON.stringify({ 'si': records });

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '/MonitorOperations/AddGroupForEgrulType/',
                data: si,
                success: function () {
                    load_egrul_group_monitor_info();
                }
            });

        }

    }


    //#endregion monitor_egrul_group

    window.monitor_init = function (sec) {
        sec = sec || sector;
        $('#monitor_content').html('');
        $.get("/MonitorOperations/LoadSubForm/", { i: sec }, function (data) {
            sector = sec;
            $('#monitor_content').append(data);
            if (sec == 1) {
                load_update_monitor_info();
            }
            if (sec == 2) {
                load_egrul_monitor_info();
            }
            if (sec == 3) {
                load_message_monitor_info();
            }
            if (sec == 4) {
                load_egrul_group_monitor_info();
            }
        });        
    };

   
    
})();

$(document).ready(function () {
    monitor_init();
    $('input[name=monitor_sector]').change(function () {
        var i = Number($('input[name=monitor_sector]:checked').val());
        monitor_init(i);
    });
});