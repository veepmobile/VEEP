(function () {

    

    var group_list = [];

    var change_group = function (el) {
        selected.id = Number(el.value);
        check_group_changes();
    }

    var group_new_exists = function (new_name) {
        var new_name_exists = false;
        for (var i = 0; i < group_list.length; i++) {
            if (new_name.trim() == group_list[i].name.trim()) {
                return true;
            }
        }
        return false;
    }
    var check_group_changes = function () {
        if (selected.id != $("#grops_list option:selected").val()) {
            $("#grops_list").val(selected.id);
        }
        if (selected.id == 0) {
            $('#new_group_comand_block').css('display', 'inline-block');
            $('#edit_group_comand_block').css('display', 'none');
            $("#search_result").html('').hide();
        } else {
            $('#new_group_comand_block').css('display', 'none');
            $('#edit_group_comand_block').css('display', 'inline-block');
            search(1);
        }
    }


    window.search = function (page) {
        if (selected.id == 0)
            return;
        page= page || 1;
        showClock();
        $.post('/UserLists/Search/', { page: page, group_id: selected.id }, function (data) {
            hideClock();
            generate_result(page, data);
        }, "json");
    }

    var generate_result = function(page,data){
        $("#search_result").html('').show();

        if (data && data.length > 0) {

            var total = data[0].search_count;
            var page_count = total / 20;
            if (page_count % 1 > 0) {
                page_count = (page_count - (page_count % 1)) + 1;
            }
            
            $("#search_result").append("<span class=\"total_count\">Предприятий и ИП в группе: " + total + "</span>");

            var $res_block = $('<div>').addClass("res_block");

            var $export_block = $('<div>').addClass("export_block");

            var $checkall_block = $('<div>').addClass("checkall_block");
            $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");

            var $check_command_block = $('<div>').addClass("check_command_block");
            $check_command_block.append('<span class="title"><em>Удалить</em><span class=\"icon-trash-empty icon disabled\" id=\"btn_delete"\></span></span>');


            $export_block.append($checkall_block);
            $export_block.append($check_command_block);

            $res_block.append($export_block);

            for (var i = 0; i < data.length; i++) {
                var $res_item = $('<div>').addClass("res_item");

                var $check_block = $('<div>').addClass("check_block");
                $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + data[i].issuer_id + "_" + data[i].type_id + "\"></input>");
                $res_item.append($check_block);

                var $info_block = $('<div>').addClass("info_block");
                if (data[i].ticker.length > 0) {
                    $info_block.append('<a class="comp_title" href="/' + ((data[i].type_ka == 0) ? 'issuers' : 'profileip') + '/' + data[i].ticker + '" target="_blank">' + data[i].name + '</a>');
                } else {
                    $info_block.append('<span class="comp_title">' + data[i].name + '</a>');
                }

                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";

                if (data[i].del != '0') {
                    comp_info_text += "<p class=\"attention\">Исключено из реестра Росстата " + data[i].del + "!</p>";
                }
                if (data[i].ip_stop != 0) {
                    comp_info_text += "<p class=\"attention\">Сведения о прекращении деятельности</p>";
                }

                if (data[i].legal_address != "") {
                    comp_info_text += "<p>" + data[i].legal_address + "</p>";
                }
                if (data[i].ruler != "") {
                    comp_info_text += "<p>Руководитель: " + data[i].ruler + "</p>";
                }

                if (data[i].okved != "") {
                    comp_info_text += "<p>" + data[i].okved + "</p>";
                }

                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                var $code_block = $('<div>').addClass("code_block");
                comp_info_text = "";
                if (data[i].ogrn != "") {

                    comp_info_text += "<p><span class=\"code_title_ip\">ОГРН" +((data[i].ogrn.length==15)?"ИП":"") +":</span>" + data[i].ogrn + "</p>";
                }
                if (data[i].inn != "") {
                    comp_info_text += "<p><span class=\"code_title_ip\">ИНН:</span>" + data[i].inn + "</p>";
                }
                if (data[i].okpo != "") {
                    comp_info_text += "<p><span class=\"code_title_ip\">ОКПО:</span>" + data[i].okpo + "</p>";
                }
                $code_block.html(comp_info_text);

                $res_item.append($info_block);
                $res_item.append($code_block);
                $res_block.append($res_item);

            }


            $("#search_result").append($res_block);
            $("#search_result").append(_get_paging(page, page_count));

            post_search_binding();

        } else {
            var $el = $('<div>').addClass("non_result").text("Предприятий и ИП в группе: 0");
            $("#search_result").append($el);
        }


    }


    var _get_paging = function (page, page_count) {

        if (page_count <= 1) {
            return "";
        }

        var html = "";
        var StartPage = 1;
        if (page_count >= 8) {
            StartPage = ((page - 3 > 0) ? ((page_count - page < 3) ? page_count + (page_count - page) - 8 : page - 3) : 1);
        }

        if (page > 1 && page_count > 7) {
            html += '<td onclick="search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="search(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };


    var init_select = function (id) {
        $.post("/UserLists/GroupList/", null, function (data) {
            group_list = data;
            //Удалим старые значения
            $('#'+id+' option[value!=0]').remove();
            $.each(group_list, function (i, item) {
                $('#' + id).append($('<option>', {
                    value: item.id,
                    text: item.name + " (" + item.cnt_disp + ")"
                }));
            });
            check_group_changes();
            
        });        
    }

   

    var post_search_binding = function () {
        $(".res_item").find('input:checkbox').add("#selallbox").change(function () {
            $('#btn_delete').addClass("disabled").unbind();
            var any_checked = $(".res_item").find('input:checkbox:checked').length > 0;
            if (any_checked) {
                $('#btn_delete').removeClass("disabled").click(function () { delete_issuers(); });
            }
        });
    }

    //Загрузка формы импорта кодов
    var load_import_form = function (is_new) {
        showClock();
        $.post("/UserLists/ImportForm/", { is_new: is_new }, function (result) {
            hideClock();
            show_dialog({ content: result, $placer: $("#main") });
            if (selected.id != 0) {
                var group_name = $("#grops_list option:selected").text();
                $('#import_group_header').text('Импорт кодов в группу: '+group_name)
            }
        }, "html");
    }

    var load_rename_form = function () {
        showClock();
        $.post("/UserLists/RenameForm/", null, function (result) {
            hideClock();
            show_dialog({ content: result, $placer: $("#main") });
            var group_name = $("#grops_list option:selected").text();
            $('#import_group_header').text('Переименование группы: ' + group_name);
        });
    }


    window.import_group = function (is_new) {
        var $error_list = $('.error_list');
        $error_list.html('');
        var group_params = {};
        var errors = [];
        group_params.id = selected.id;
        if(selected.id==0)
        {
            group_params.name = $('#group_name').val();
            if (group_params.name.length == 0) {
                errors.push("Введите имя группы");
            }
            if (group_new_exists(group_params.name)) {
                errors.push("Данное имя группы уже существует");
            }

        }
        group_params.code_type = $('input[name=code_type]:checked').val();
        if (group_params.code_type == undefined) {
            errors.push("Выберите тип кода");
        }
        group_params.branch_exclude = $('#branch_exclude').prop('checked');
        if ($('#code_list').val().length == 0) {
            errors.push("Список кодов пустой");
        }
        var code_list = $('#code_list').val().replace(/[ ]*/ig, '').replace(/\n/ig, ',').replace(/\r/ig, "");
        if (code_list.substr(code_list.length - 1, 1) == ',') {
            code_list = code_list.substr(0, code_list.length - 1);
        }
        var arr_codes = code_list.split(',');
        if (arr_codes.length > group_limit) {
            errors.push("Количество кодов не может превышать " + group_limit);
        }
        var msg = "";
        var re = "";
        var re_ip=""
        if (group_params.code_type == 1) {
            re = /[0-9]{10}/i;
            re_ip = /[0-9]{12}/i;
            msg = "ИНН должен состоять из 10 или 12 цифр! Неправильный ИНН: "
        }
        if (group_params.code_type == 2) {
            re = /[0-9]{13}/i;
            re_ip = /[0-9]{15}/i;
            msg = "ОГРН(ОГРНИП) должен состоять из 13(15) цифр!  Неправильный ОГРН: "
        }
        if (re != "") {
            for (var i = 0; i < arr_codes.length; i++) {
                if (arr_codes[i].replace(re, '#') != '#' && arr_codes[i].replace(re_ip, '#') != '#') {
                    errors.push(msg + arr_codes[i]);
                    break;
                }
            }
        }
        group_params.codes = code_list;

        if (errors.length > 0) {
            var ul = ["<ul>"];
            for (var i = 0; i < errors.length; i++) {
                ul.push("<li>" + errors[i] + "</li>");
            }
            ul.push("</ul>");
            $error_list.html(ul.join(""));
        } else {
            $.post("/UserLists/Import/", group_params, function (data) {
                if (data.res_count > group_limit) {
                    $error_list.html("<ul><li>Общее кол-во записей не может превышать " + group_limit + "</li></ul>");
                } else {
                    selected.id = data.id;
                    init_select("grops_list");
                    close_dialog();
                }
            }, "json");
        }
        

    }

    window.rename_group = function () {
        var $error_list = $('.error_list');
        $error_list.html('');
        var group_params = {};
        var errors = [];
        group_params.id = selected.id;
        group_params.name = $('#group_name').val();
        if (group_params.name.length == 0) {
            errors.push("Введите новое имя группы");
        }
        if (group_new_exists(group_params.name)) {
            errors.push("Данное имя группы уже существует");
        }
        if (errors.length > 0) {
            var ul = ["<ul>"];
            for (var i = 0; i < errors.length; i++) {
                ul.push("<li>" + errors[i] + "</li>");
            }
            ul.push("</ul>");
            $error_list.html(ul.join(""));
        } else {
            $.post("/UserLists/RenameList/", group_params, function (data) {
                close_dialog();
                init_select("grops_list");
            }, "json");
        }
    }

    window.export_codes = function (code_type) {
        var issuers_arr = [];
        showClock();
        var any_checked = $(".res_item").find('input:checkbox:checked').length > 0;
        if (any_checked) {
            $(".res_item").find("input:checkbox:checked").each(function () {
                issuers_arr.push(this.value);
            });
        }
        $.post("/UserLists/Export/", { code_type: code_type, id: selected.id, issuers_info: issuers_arr.join(",") }, function (result) {
            hideClock();
            show_dialog({ content: result, $placer: $("#main") });
            var group_name = $("#grops_list option:selected").text();
            var code_name = "";
            switch (code_type) {
                case 1:
                    code_name = "ИНН";
                    break;
                case 2:
                    code_name = "ОГРН / ОГРНИП";
                    break;
                case 3:
                    code_name = "СКРИН";
                    break;
            }
            $('#import_group_header').text('Список кодов '+code_name+' группы: ' + group_name);
        })
    }

    var delete_list = function () {
        var group_name = $("#grops_list option:selected").text();
        confirm_dialog("<h3>Вы действительно хотите удалить группу: " + group_name + "?</h3>", function () {
            $.post("/UserLists/DeleteList/", { id: selected.id }, function (data) {
                selected.id = 0;
                init_select("grops_list");
                close_dialog();
            });
        }, { $placer: $("#main") });
    }

    var delete_issuers = function () {
        
        var issuers_arr = [];
        $(".res_item").find("input:checkbox:checked").each(function () {
            issuers_arr.push(this.value);
        });
        if (issuers_arr.length == 0) {
            return;
        }

        confirm_dialog("<h3>Количество удаляемых ЮЛ: " + issuers_arr.length + "</h3>", function () {
            $.post("/UserLists/DeleteIssuers/", { list_id: selected.id, issuers_info: issuers_arr.join(",") }, function (data) {
                init_select("grops_list");
                close_dialog();
            });
        }, { $placer: $("#main") });
    }

    window.doSetCheckedAll = function (chb) {
        $(".res_item").find("input:checkbox").each(function (i) {
            this.checked = chb.checked;
        });

    };

    window.checkOnOff = function (o) {
        var allCheckboxes = $(".res_item").find('input:checkbox');
        var checkedCheckboxes = $(".res_item").find('input:checkbox:checked');
        var allChecked = allCheckboxes.length == checkedCheckboxes.length;
        $("#selallbox").get(0).checked = allChecked;
    }
    

    window.groups_init = function () {
        init_select('grops_list');
        $('#grops_list').change(function () {
            change_group(this);
        });
        $('#create_new_group_btn').click(function () {
            if (group_limit > 0) {
                load_import_form(true);
            } else {
                no_rights();
            }
        });
        $('#import_group_btn').click(function () {
            if (group_limit > 0) {
                load_import_form(false);
            } else {
                no_rights();
            }            
        });
        $('#delete_group_btn').click(function () {
            if (group_limit > 0) {
                delete_list();
            } else {
                no_rights();
            }     
        });
        $('#rename_group_btn').click(function () {
            if (group_limit > 0) {
                load_rename_form();
            } else {
                no_rights();
            }            
        });
    }



})();



$(document).ready(function () {
    groups_init();
});