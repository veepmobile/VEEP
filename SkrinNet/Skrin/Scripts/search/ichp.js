
var reg_type = 0;
var ind_type = 1;

(function () {
    var excel_menu_open = false;
    var group_menu_open = false;
    var indtitles = { "i1": "Общероссийский классификатор видов экономической деятельности", "i99": "Общероссийский классификатор видов экономической деятельности", "i2": 'Общесоюзный классификатор [Отрасли народного хозяйства]' };
    var regtitles = { "i0": "Общероссийский классификатор объектов административно-территориального деления", "i5": "по регистрации в налоговых органах" };
    var countParams = [30, 50, 100];
    var searching = 0;
    var tree_active;
    var tree_table;
    var src;
    var now = new Date();

    window.search_init = function () {

        $("#base_block").bind("keypress", function (e) { try2search(e); });

        $('body').on('click', function (e) {
            hidepopups(e);
            _hide_command_menu(e);
        });

        getObj("xls_frame").onreadystatechange = hideClock;
        document.onreadystatechange = hideClock;

        $("#xls_frame").ready(function () {
            hideClock();
        })

        $('#extended_switcher').click(function () {
            _toggle_extented_search(false);
        });


       // $('#ruler').keyup(function (event) {
       //     show_bones(event);
       // })

        if (user_id > 0) {
            var dates = $("#dfrom, #dto").datepicker({
                defaultDate: "-1y",
                changeMonth: true,
                showButtonPanel: true,
                changeYear: true,
                yearRange: "1990:" + now.getFullYear(),
                onSelect: function (selectedDate) {
                    var option = this.id == "dfrom" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat ||
                    $.datepicker._defaults.dateFormat,
                    selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                    search_check_changes();
                }
            });
        }

        $('[data-check]').change(function () {
            search_check_changes();
        })
    };

    window.search_check_changes = function () {
        var changes_count = 0;
        $('[data-check]').each(function () {
            var $el = $(this);
            if ($el.attr("id") == "gropVal") {
                if ($el.attr("val") != "0") {
                    changes_count++;
                }
            } else if ($el.attr("type") == "text" || $el.attr("type") == "hidden") {
                if ($el.val() != "") {
                    changes_count++;
                }
            } else {
                if ($el.prop("checked")) {
                    changes_count++;
                }
            }

           
        });

        if (changes_count == 0) {
            $('#extended_switcher_inner').text('Расширенный поиск');
        } else {
            $('#extended_switcher_inner').text('Расширенный поиск: ' + changes_count);
        }
    };

    var _hide_command_menu = function (e) {
        if (group_menu_open) {
            $('.sub_command_menu').remove();
            group_menu_open = false;
        }
        if (excel_menu_open) {
            $('.sub_command_menu').remove();
            excel_menu_open = false;
        }
    };

    var mainSearcher = function (pg, group_name) {
        bones_pressed = false;
        var rcount = $("#countVal").attr("val");
        var top1000 = (pg == -1000) ? "1" : "0";
        var group_id = $("#gropVal").attr("val");
        var params = {
            "ruler": $("#ruler").val(),
            "regions": $("#reg_val").val(),
            "is_okato": ($("input:radio[name=reg_type]:checked").val() == 0) ? 1 : 0,
            //"is_okato": (String($("#td_regtype").closest("td").find("input:radio:checked").val()) == "0") ? 1 : 0,
            "reg_excl": (String($("#reg_excl").val()).length == 0) ? 0 : $("#reg_excl").val(),
            "industry": $("#ind_val").val(),
            "ind_excl": (String($("#ind_excl").val()).length == 0) ? 0 : $("#ind_excl").val(),
            "page_no": pg,
            "rcount": rcount,//$("#rc").val(),
            "user_id": user_id, //<%=current_user_id%>,
            "group_id": group_id,
            "group_name": ((group_name.length > 0 && pg == -1000) ? group_name : ""),
            "top1000": top1000,
        }
        //ключевые слова
        var key_ruler = ClearText(String($("#ruler").val()));

        _toggle_extented_search(true);

        if (pg == -1000) {
            hideClock();
            var form = document.createElement("form");
            //form.action = "/SearchIchp/Search";
            form.action = "/SearchIchp/DoSearchExcel";
            form.method = "POST";
            element = document.createElement("input");
            element.type = "hidden";
            element.name = "so_string";
            element.value = JSON.stringify(params);
            form.appendChild(element);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

            /*
            $.post("/SearchIchp/Search", params, function (data) {
                if (data) { DownLoadXLS1000(data); }
                hideClock();
                searching = 0;
            });*/
            searching = 0;
        }
        else {

            //Новый поиск
            $.post("/SearchIchp/Search", params, function (data) {
                var re = new RegExp("icon_error", "ig")
                if (pg > 0) { 
                    //getObj("search_result").innerHTML=data;
                    
                    GenerateResult(pg, rcount, data, roles_object, key_ruler, user_id, group_id);
                    hideClock();
                    if (String(re.exec(data)) == "null") {
                        if ($("#search_table").css("display") == "block" || $("#search_table").css("display") == "table") {
                            $('#hide_form').show();
                            toggle_form();
                        }
                    }
                }
                if (pg == -2000) {
                    // Добавление 10000 в группу
                    var ret = SaveResultGroup(data, group_name);
                    var amsg = String(ret).split("_");
                    showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
                }
                hideClock();
                $("#btn_search").val("Найти");
                searching = 0;
                location.href = "#top";
            },
    "json");
        }
    };

    var GenerateResult = function (page, rcount, data, ro, key_ruler, user_id, group_id) {
        $("#search_result").html('').show();

        var total = data.total;
        var total_found = data.total_found;
        var page_count = total / rcount;

        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total && total > 0) {


            var total_text;
            //total_text = "Всего найдено: " + total;

            if (group_id == "0") {
                total_text = "Всего найдено: " + total_found;
            }
            else {
                total_text = "Индивидуальных предпринимателей в группе: " + total_found;
            }
            if (total_found > 10000) {
                total_text += " (Выводятся первые 10000 индивидуальных предпринимателей)";
            }

            $("#search_result").append("<span class=\"total_count\">"+ total_text + "</span>");

            var res_arr = data["results"];
            var $res_block = $('<div>').addClass("res_block");
            if (ro.canAddToGroup || ro.canExport) {
                var $export_command_block = $('<div>').addClass("export_command_block");
                var $export_block = $('<div>').addClass("export_block");
                var $checkall_block = $('<div>').addClass("checkall_block");
                $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");
                $export_block.append($checkall_block);

                if (ro.canAddToGroup == 1) {
                    var $add_group_btn = $("<div id=\"btnaddGroup\"><span class=\"title\"><em>Добавить в Группу</em><span class=\"icon-plus icon\"></span></span></div>");
                    $export_command_block.append($add_group_btn);
                    $add_group_btn.click(function (e) {
                        generate_sub_addGroupmenu(e, total_found);
                    })

                }
                if (ro.canExport) {
                    var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"icon-file-excel icon\"></span></div>");
                    $export_command_block.append($add_excel_btn);
                    $add_excel_btn.click(function (e) {
                        generate_sub_excelmenu(e, total);
                    });
                }
                $export_block.append($export_command_block);
                $res_block.append($export_block);
            }

            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var $res_item = $('<div>').addClass("res_item");

                if (ro.canExport || (ro.canAddToGroup == 1))
                {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + res_arr[i].ogrnip + "_10\"></input>");
                    $res_item.append($check_block);
                }                

                var $info_block = $('<div>').addClass("info_block_ip");
                if (res_arr[i].ogrnip.length > 0) {
                    $info_block.append('<a class="comp_title" href="/profileip/' + res_arr[i].ogrnip + '" target="_blank">' + res_arr[i].fio + '</a>');
                } else {
                    $info_block.append('<span class="comp_title">' + res_arr[i].ogrnip + '</span>');
                }

                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";

                if (res_arr[i].typeip == 1) { comp_info_text += "<p>Индивидуальный предприниматель</p>"; }
                if (res_arr[i].typeip == 2) { comp_info_text += "<p>Глава крестьянского фермерского хозяйства</p>"; }
                
                comp_info_text += "<p>Регион: " + res_arr[i].region + "</p>";

                if (user_id > 0 && res_arr[i].stoping != '0') {
                    var dt = new Date();
                    var sdt = (dt.getDate()-1) + "." + (dt.getMonth()+1) + "." + (dt.getFullYear());
                    comp_info_text += "<p class=\"attention\">Сведения о прекращении деятельности</p>";
                }

                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                var $code_block = $('<div>').addClass("code_block");
                comp_info_text = "";
                if (res_arr[i].ogrnip != "") {
                    comp_info_text += "<p><span class=\"code_title_ip\">ОГРНИП:</span>" + res_arr[i].ogrnip + "</p>";
                }
                if (res_arr[i].inn != "") {
                    comp_info_text += "<p><span class=\"code_title_ip\">ИНН:</span>" + res_arr[i].inn + "</p>";
                }                
                if (res_arr[i].okpo != "") {
                    comp_info_text += "<p><span class=\"code_title_ip\">ОКПО:</span>" + res_arr[i].okpo + "</p>";
                }
                $code_block.html(comp_info_text);

                $res_item.append($info_block);               
                $res_item.append($code_block);
                $res_block.append($res_item);

            }
            $("#search_result").append($res_block);
            $("#search_result").append(_get_paging(page, page_count))

        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);            
        }
    };
    var SaveResultGroup = function (data, group_name) {
        if (data) {
            for (var j = 0, j_max = (extGroupList.length - 1) ; j <= j_max; j++) {
                if (extGroupList[j][1] == group_name) {
                    return "1_Группа с таким именем уже есть";
                }
            }
            var res_arr = data["results"];
            var issuers = "";
            for (var i = 0; i <= (res_arr.length - 1) ; i++) {
                issuers += res_arr[i].ogrnip + "_10,";
            }
            doSaveToUserList(issuers, group_name, true);
            hideClock();
            return "0_В группе сохранено " + res_arr.length + " ИП"
        }
        else {
            return "1_Внутренняя ошибка";
        }

    };
    var generate_sub_addGroupmenu = function (e, total) {
        if (group_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Добавить выбранные ИП в Группу").click(function () { doSave2List(); }).appendTo($ul);
        $("<li>").text(total > group_limit ? "Добавить первые " + group_limit + " ИП в Группу" : "Добавить найденные ИП (" + total + ") в Группу").click(function (event) { doCreateGroup1000(event); }).appendTo($ul);
        $("#btnaddGroup").append($ul);
        group_menu_open = true;
        e.stopPropagation();
    }
    window.USERGROPS = [];

    window.doSave2List = function (issuer_id) {
        if ($(".res_item").find('input:checkbox:checked').length > 0 || issuer_id) {
            $.post("/Modules/GetGroups", null,
                        function (data) {
                            var err = 0
                            var htm = "<div id=\"group_dialog\"><h4>Выберите группу</h4><div class=\"scroll_select\"><table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"dpw\" >"
                            st_max = 0
                            try {
                                data = eval(data)
                                USERGROPS = data;
                            } catch (e) {
                                err = 1
                            }
                            if (err == 0) {
                                if (getObj("modal_dialog")) {
                                    close_dialog();
                                }
                                for (var i = 0; i < data.length; i++) {
                                    htm += "<tr><td  id=\"tdhover" + i + "\" class=\"bones_pointer\" onclick=\"savegroup(" + data[i].lid + ",'" + issuer_id + "')\" ><span id=\"s" + data[i].lid + "\">" + data[i].name + "</span></td></tr>";
                                }
                            }
                            htm += "</table></div>"
                            htm += "<h4>или создайте новую</h4><div><label class=\"label_form\" style=\"width:110px;margin-top:5px;\">Новая группа:</label><input type=\"text\" class=\"search_input\" style=\"width:200px;\" id=\"ngroup\"/><button  class=\"btns darkblue\" style=\"margin-left:5px;\"onclick=\"validateGroupNames(0,'" + issuer_id + "');\" >Добавить в новую</button></div><div id=\"error_groups\" class=\"error_login\"></div>";
                            htm += "<div style='text-align:center;margin:20px 0;'></div></div>";

                            show_dialog({ "content": htm, "is_print": false });
                            $("#group_dialog").click(function (event) {
                                event.stopPropagation();

                            });

                        }, "html");
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для включения в группу</p>', 3000);
        }
    };
    window.validateGroupNames = function (issuer_id) {
        $('#error_groups').html('');
        var new_name = $("#ngroup").val();
        for (var i = 0; i < USERGROPS.length; i++) {
            if (USERGROPS[i].name == new_name.trim()) {
                $('#error_groups').html('Данное имя группы уже существует');
                return;
            }
        }
        savegroup(0, issuer_id);
    }
    window.savegroup = function (group_id, issuer_id) {
        g_id = group_id;
        if (group_id != 0) $("#ngroup").val("");

        doSaveGroups(issuer_id);
        close_dialog();
    }
    var doCreateGroup1000 = function (event) {
        event.stopPropagation();
        _hide_command_menu(event);
        var html = "<div id=\"group100dialog\"><div class=\"form-group\"><label class=\"label_form\" style=\"width:250px;\">Введите уникальное имя группы:</label><input type=\"text\" class=\"search_input\" style=\"width:250px;\" id=\"ngroup\"/></div>";
        html += "<div style='text-align:center;margin:20px 0;'><button  class=\"btns darkblue\" onclick=\"doSearch1000();\" >Сохранить</button></div></div>";
        show_dialog({ "content": html, "is_print": false });
        $("#group100dialog").click(function (event) {
            event.stopPropagation();

        });
    };
    window.doSearch1000 = function () {
        var group_name = $('#ngroup').val();
        if (group_name == "") {
            showwin('critical', '<p align=center>Отсутствует имя группы</p>', 2000);
            return;
        }
        $.get("/Modules/GetGroups", function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].name == group_name.trim()) {
                    showwin('critical', '<p align=center>Данное имя группы уже существует</p>', 2000);
                    return;
                }
            }
            showClock();
            mainSearcher(-2000, group_name);
        }, "json")
    };
    var generate_sub_excelmenu = function (e,total) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным ИП").click(function () { doSave2XLS(); }).appendTo($ul);
        $("<li>").text("Экспорт по " + (total > 10000 ? "первым 10000" : total) + " ИП").click(function () { doExport10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
    };

    var DownLoadXLS1000 = function (filename) {
        var form = document.createElement("form");
        form.action = "/SearchIchp/GetFile";
        form.method = "POST";
        if (!document.addEventListener) {
            showClock();
            form.target = "xls_frame"
        } else {
            form.target = "blank"
        }
        form.style.display = "none"
        form.appendChild(make_input("src", filename));
        form.appendChild(make_input("page", "dbsearchru/ichp"));
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    };

    var doExport10000 = function() {
        showClock();
        mainSearcher(-1000, '');
    };

    var make_input = function (name, value) {
        var element = null;
        element = document.createElement("input");
        element.type = "text";
        element.name = name;
        element.value = value;
        return element;
    };


    var checkCanSearch = function () {
        var ss = $("#ruler").val() + $("#reg_val").val() + $("#ind_val").val() + ((String($("#gropVal").attr("val")) == "0") ? "" : "1");
        return (ss.length == 0) ? false : true;
    };

    var doStop = function () {
        hideClock();
        searching = 0;
        $.post("/iss/modules/operations.asp", { "action": "6" }); //??
        $("#btn_search").val("Найти");
        window.stop();
    };

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#ruler').css("border-radius", "4px");
        }
    };

    var draw_selection = function (i) {
        if (getObj("tdhover" + i)){
            var d=getObj("tdhover" + i)
            $("#dpw").find('td').each(function(){
                this.className = "bones_pointer";
            }); 
            d.className="bones_pointer over_color";
            $("#ruler").val($("#tdhover" + i).text());
        }
    };

    var try2search = function (e) {
        var kk
        if (window.event){
            e = window.event;
        }
        kk=e.keyCode;
        if (e.stopPropagation) {
            e.stopPropagation();
        }else{
            e.cancelBubble=true;
        }    
        if(kk==13){
            doSearch();
        }
    };

    var remove_div = function (event,div_selector,exclude_selector) {

        var container = $(div_selector); //сам контейнер, который нужно убрать
        var exclude = $(exclude_selector);//окно поиска нажатием на которое появляется контейнер

        if (!container.is(event.target) // if the target of the click isn't the container...
        && container.has(event.target).length === 0  // ... nor a descendant of the container
        && !exclude.is(event.target)
        && exclude.has(event.target).length === 0)
        {
            container.remove();
            exclude.removeClass("customSelect_focus");
        }
    };

    var hidepopups = function (event) {
        close_bones();

        if (getObj(tree_active + "_window")) {
            getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window"));
            $("#" + tree_active).css({ 'background-color': '#FFFFFF' });
        }
        close_dialog();
        if (event) {
            remove_div(event, "#groupSelector", "#groupSelect");
            remove_div(event, "#countSelector", "#countSelect");
        }
    };

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
            html += '<td onclick="doSearch(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="doSearch(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count+1) {
            html += '<td onclick="doSearch(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count+1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var _toggle_extented_search = function (only_need_close) {
        
        var $switch = $('#switcher_ico');

        if (only_need_close || $switch.hasClass("icon-angle-up")) {
            $switch.removeClass("icon-angle-up").addClass("icon-angle-down");
            $("#extended_block").hide();
        } else {
            $switch.removeClass("icon-angle-down").addClass("icon-angle-up");
            $("#extended_block").show();
        }
        
    };
    var restrictGropName = function (groupName) {
        if (groupName.length > 65)
            groupName = groupName.substring(0, 65) + "...";
        return groupName;
    }
    window.selectNewGroup = function (i) {
        $("#gropVal").text(restrictGropName(extGroupList[i].txt));
        $("#gropVal").attr("val", extGroupList[i].code);
        document.body.removeChild(getObj("groupSelector"));
        $("#groupSelect").removeClass("customSelect_focus");
        search_check_changes();
    }

    window.show_groupSelector = function () {
        var d;


        if (!getObj("groupSelector")) {
            d = document.createElement("div");
            d.id = "groupSelector";
            d.className = "dp_window";


            var boundes = $("#groupSelect").parent().position();
            d.style.top = boundes.top + 94 + $("#groupSelect").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#groupSelect").width() + 12) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("groupSelector"));
            $("#groupSelect").removeClass("customSelect_focus");
        };
        if (getObj("groupSelector")) {
            $("#groupSelect").addClass("customSelect_focus");
            var selected_i = 0;
            var htm = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" id=\"grouptbl\" >"
            for (var i = 0; i < extGroupList.length; i++) {
                if (extGroupList[i].code - $("#gropVal").attr("val") == 0) {
                    selected_i = i;
                }
                htm += "<tr class='bones_pointer'  onClick='selectNewGroup("
                 + i + ");'><td id='tdgroup" + i + "' class='bones_pointer' >" + extGroupList[i].txt + "</td></td><td class='bones_pointer' style='text-align:right;width:120px;color:#999;white-space:nowrap;'>"
                  + (extGroupList[i].val == "" ? "все&nbsp;ЮЛ&nbsp;и&nbsp;ИП" : extGroupList[i].val) + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            if (extGroupList.length > 10)
                d.style.height = "180px";
            d.style.display = "block";
            d.scrollTop = 9 * selected_i
            $("#grouptbl").one("mouseenter", function () {
                $('#tdgroup' + $('#gropVal').attr('val')).attr('className', 'bones_pointer');
            })

        }
    }
    var show_bones = function (e) {
        var d;
        $("#base_block").unbind();
        e.stopPropagation();

        if ((e.keyCode == 40 || e.keyCode == 38) && getObj("dp_window")) {
            skip(e.keyCode);
        } else {

            if (!getObj("dp_window") && !(e.keyCode == 13 || e.keyCode == 27)) {
                d = document.createElement("div");
                d.id = "dp_window";
                st_selected = -1;
                d.className = "dp_window";
                d.style.height = "120px";

                $("#ruler").css("border-radius","4px 4px 0 0");

                var boundes = $("#ruler").position();
                d.style.top = boundes.top + 94 + $("#ruler").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#ruler").width() + 23) + "px";
                d.style.display = "none";
                document.body.appendChild(d);
            } else {
                d = getObj("dp_window");
            }
            if ((e.keyCode == 13 || e.keyCode == 27) && getObj("dp_window")) {
                document.body.removeChild(getObj("dp_window"));
                if (e.keyCode == 13) {
                    bones_pressed = true;
                    if (st_selected >= 0) {
                        $('input:checkbox[name=rsv]').attr('checked', true);

                    }
                    try2search(e);
                    st_selected = -1;
                }
            }
            $("html").click(close_bones);
            if (getObj("dp_window")) {
                $.post("/Modules/GetBones", { "input": $("#ruler").val() },
                    function (data) {
                        var htm = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" id=\"dpw\" onkeydown=\"skip(event)\">"
                        st_max = 0
                        if (!data[0].name && getObj("dp_window")) {
                            document.body.removeChild(d);
                            st_selected = -1;
                        } else {
                            for (var i = 0; i < data.length; i++) {
                                htm += "<tr class=\"bones_pointer\"><td id=\"tdhover" + i + "\"  onclick=\"dispissuer('" + data[i].name + "')\" >" + data[i].name + "</td><td  style=\"text-align:right;width:150px;\">" + data[i].cnt + " совпадений</td></tr>";
                                st_max++;
                            }
                            htm += "</table>"
                            d.innerHTML = htm;
                            d.style.display = "block";
                        }
                    }, "json"
                );
            }
        }
        $("base_block").bind("keypress", function (e) { try2search(e) });
    };

    var show_dictionary = function () {
        var $placer = $("body"); // контейнер для добавления
        var id = "dic_container"; //идентификатор диалогового окна
        var body = '<div id="' + id + '" class="modal fade" tabindex="-1" style="display: none;" aria-hidden="true">'
                    + '<div class="modal-dialog"><div class="modal-content">'
                    + '<div class="modal-header"><button class="close" aria-hidden="true" data-dismiss="modal" type="button">×</button>'
                    + '</div><div class="modal-body"><div id="td_' + tree_active + '"></div></div>'
                    + '<div class="modal-footer">'
                    + '</div>'
                    + '</div></div></div>';
        //Удалим старое содержимое
        $('#' + id).remove();

        $placer.append(body);

        $('#' + id).modal();        
    };

    var ClearText = function (txt) {
        txt = txt.toLowerCase().replace(/-/g, ';').replace(/\s+/g, ';').replace(/\"/g, "").replace(/\'/g, "");
        return txt;
    };

    window.doSearch = function (pg) {
        close_bones();
        if (searching == 1) {
            doStop();
        } else {
            if (checkCanSearch()) {
                searching = 1;
                pg = (isNaN(String(pg))) ? 1 : pg;
                showClock();
                $("#btn_search").val("Остановить");
                mainSearcher(pg, '');
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        }
    };

    var doSave2XLS = function () {
        var issuers = "";
        showClock();
        if ($(".res_item").find('input:checkbox:checked').length > 0) {
            $(".res_item").find("input:checkbox:checked").each(function (i) {
                if (String(this.value).length > 0) {
                        issuers += String(this.value) + "|";
                }
            });
            issuers = issuers.substring(0, issuers.length - 1);
            var url = '/SearchIchp/GetExcel?issuers=' + issuers;
            window.open(url, '_blank');
            hideClock();            /*
            $.post("/SearchIchp/GetExcel", { "issuers": issuers }, function (data) {
                if (data) { DownLoadXLS1000(data); }
                hideClock();
            });
            */
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одного ИП для экспорта</p>', 3000);
        }
    };

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
    };

        window.doClear = function () {
            $("#countVal").text(countParams[0]);
            $("#countVal").attr("val", countParams[0]);
            $("#gropVal").text(restrictGropName(extGroupList[0].txt));
            $("#gropVal").attr("val", extGroupList[0].code);
            $("input").each(function (i) {

                if (this.type == "text") {
                    this.value = "";
                    this.title = "";
                }
                if (this.type == "hidden") {
                    this.value = ""
                }
                if (this.type == "checkbox") {
                    if (this.id == "ind_main") {
                        this.checked = true;
                    } else {
                        this.checked = false;
                    }
                    if (this.id == "rsv") {
                        this.checked = false;
                    }
                }
                if (this.type == "radio") {
                    if (this.id == "ind_default") {
                        this.checked = true;
                        ch_ind_type(this);
                    }
                    if (this.id == "reg_default") {
                        this.checked = true;
                        ch_reg_type(this);
                    }
                    if (this.id == "c2") {
                        this.checked = true;
                    }
                }
            });
            search_check_changes();
        };


        window.ch_ind_type = function(i_type) {
            if ((ind_type - 2 == 0 && (i_type.value - 1 == 0 || i_type.value - 99 == 0)) || (i_type.value - 2 == 0 && (ind_type - 1 == 0 || ind_type - 99 == 0))) {
                $("#ind").val("");
                $("#ind_val").val("");
                $("#ind_excl").val("");
                $("#ind").attr({ "title": "" });
                $("#ind").attr({ "title": eval("indtitles.i" + i_type.value) });
            }
            ind_type = i_type.value;
        };

        window.ch_reg_type = function (r_type) {
            $("#reg").val("");
            $("#reg_val").val("");
            $("#reg_excl").val("");
            $("#reg").attr({ "title": "" });
            reg_type = r_type.value;
            $("#reg").attr({ "title": eval("regtitles.i" + r_type.value) });
        };

        window.skip = function (kod) {
            if (kod == 40 && st_selected < st_max - 1) {
                st_selected++;
                draw_selection(st_selected);
            }
            if (kod == 38) {
                if (st_selected > 0) {
                    st_selected--;
                    draw_selection(st_selected);
                } else {
                    $("#dpw").find('td').each(function () {
                        this.className = "bones_pointer";
                    });
                    st_selected = -1
                    getObj("ruler").focus();
                }
            }
        };

        window.dispissuer = function (text) {
            $("#ruler").val(text).css("border-radius", "4px");
            if (getObj("dp_window")) {
                document.body.removeChild(getObj("dp_window"));
            }
        };

        window.show_tree_selector = function (e, sr, is_tree, mult) {

            src = (sr == 99) ? 1 : sr;
            tree_table = src;
            hidepopups()
            if (window.event) {
                e = window.event;
                caller = e.srcElement;
            }
            if (e.stopPropagation) {
                e.stopPropagation();
                caller = e.target;
            } else {

                e.cancelBubble = true;
            }
            tree_active = caller.id;
            $("#" + tree_active).css({ 'background-color': '#F0F0F0' });
            var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
            if (!getObj(tree_active + "_window")) {
                d = document.createElement("iframe");
                d.className = "tree_frame";
                d.id = tree_active + "_window";

                d.frameBorder = "0"
                if (is_tree == 1) {
                    d.src = "/Tree/TreeSelector?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val();
                } else {
                    tree_table += 6;
                    d.src = "/iss/selector/selector.asp?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val() + "&mult=" + mult;
                }
                var content = "<div id=\"td_" + tree_active + "\"></div>";
                show_dictionary();
                getObj("td_" + tree_active).appendChild(d);

                showContentClock('#dic_container .modal-dialog');
            
                $("#dic_container").click(function (event) {
                    event.stopPropagation();

                })
            }
        };

        window.Write_TS = function (retval, is_excl) {
            if (retval.length > 0) {
                $.post("/Tree/GetResultString", { "src": tree_table, "id": retval },
                function (data) {
                    $("#" + tree_active).val(((is_excl == -1) ? "Искл.: " : "") + data);
                    $("#" + tree_active).attr({ "title": ((is_excl == -1) ? "Исключая: " : "") + data.replace(",", ",\n") });
                    $("#" + tree_active + "_excl").val(is_excl);
                    $("#" + tree_active + "_val").val(retval);
                    search_check_changes();
                })
            } else {
                $("#" + tree_active).val("");
                $("#" + tree_active).attr({ "title": eval("titles." + tree_active) });
                $("#" + tree_active + "_excl").val(0);
                $("#" + tree_active + "_val").val("");
                search_check_changes();
            }
            hidepopups();
        
        };

        window.show_countSelector = function () {
            var d;

            if (!getObj("countSelector")) {
                d = document.createElement("div");
                d.id = "countSelector";
                d.className = "dp_window";

                var boundes = $("#countSelect").parent().position();
                d.style.top = boundes.top + 94 + $("#countSelect").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#countSelect").width() + 12) + "px";
                document.body.appendChild(d);
            }
            else {
                document.body.removeChild(getObj("countSelector"));
                $("#countSelect").removeClass("customSelect_focus");
            }
            if (getObj("countSelector")) {
                $("#countSelect").addClass("customSelect_focus");
                var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">"
                for (var i = 0; i < countParams.length; i++) {
                    htm += "<tr class='selec_option'  onClick='selectNewCount("
                     + i + ");'><td id='tdgroup" + i + "' id='tdgroup" + i + "' class='bones_pointer'>" + countParams[i] + "</td></tr>";
                }
                htm += "</table>";
                d.innerHTML = htm;
                d.style.display = "block";
                $("#cnttbl").one("mouseenter", function () {
                    $('#tdgroup' + $('#countVal').attr('val')).attr('className', 'bones_pointer');
                })
            }
        };

        window.selectNewCount = function (i){
            $("#countVal").text(countParams[i]);
            $("#countVal").attr("val", countParams[i]);
            document.body.removeChild(getObj("countSelector"));
            $("#countSelect").removeClass("customSelect_focus");
        };

    })();


    $().ready(function () {
        search_init();
    });
