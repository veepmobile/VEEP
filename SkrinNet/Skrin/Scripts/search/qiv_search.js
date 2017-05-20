/// <reference path="../jquery-1.10.2.intellisense.js" />
/// <reference path="../jquery-1.10.2.js" />

var reg_type = 0;
var ind_type = 24;


(function () {
    var pattern_menu_open = false;
    var group_menu_open = false;
    var excel_menu_open = false;
//    var content_top = $("#header").height() + $("#qiv_search_block").position().top - 1;
    var content_top = $("#header").height() + $("#qiv_search_block").position().top - 87;


    var st_max = 0;    
    var titles = { "okopf": "в соответствии с ОКОПФ - Общероссийский классификатор организационно-правовых форм", "okfs": "в соответствии с ОКФС - Общероссийский классификатор форм собственности" };
    var indtitles = { "i1": "Общероссийский классификатор видов экономической деятельности", "i99": "Общероссийский классификатор видов экономической деятельности", "i2": 'Общесоюзный классификатор [Отрасли народного хозяйства]' };
    var regtitles = { "i0": "Общероссийский классификатор объектов административно-территориального деления", "i5": "по регистрации в налоговых органах" };
    var countParams = [30, 50, 100];
    var searching = 0;
    var tree_active;
    var tree_table;
    var src;
    var now = new Date();
    var sort_col = 0;
    var sort_direct = "1"; // 1 - desc, 0 - asc, -1 - сортировка не выбрана
    var sort_icon = "icon-down";

    var qivparam_pattern = '                \
                <div class="form-group param_row" id="qivparam_{name}{id}"> \
                    <div class="sub_form"> \
                        <span style="padding: 0 0px 0 0;">Год</span> \
                        <div class="customSelect short_custom" id="yearSelect{name}{id}" onclick="show_yearSelector(\'{name}{id}\');"> \
                            <span id="yearVal{name}{id}" class="customVal" data-check val=""></span> \
                            <span class="icon-angle-down"></span> \
                        </div> \
                        <span style="padding: 0 0px 0 4px;">Показатель</span> \
                        <div class="customSelect search_input" id="paramSelect{name}{id}" onclick="show_param_selector(event, \'{name}{id}\');"> \
                            <span id="paramVal{name}{id}" class="customVal" data-check val="" type=""></span> \
                        </div> \
                        <span style="padding: 0 0px 0 4px;">Значение от </span> \
                        <input id="fromVal{name}{id}" type="text" class="search_input shortest" style="padding-left:2px;padding-right:2px;" placeholder="в рублях" data-check /> \
                        <span style="padding: 0 0px 0 4px;">до </span> \
                        <input id="toVal{name}{id}" type="text" class="search_input shortest" style="padding-left:2px;padding-right:2px;" placeholder="в рублях" data-check /> \
                        <button class="btns red disabled" id="btn_{name}{id}">Удалить</button> \
                    </div> \
                </div> \
    ';

    function getDefaultYear(id) {
        var i = 0;       
        if (id.length > 0) {
            var name = id.substring(0, id.length - 1);
            var url = "";
            switch (name) {
                case "gks":
                    url = "/QIVParam/GetDefaultYear";
                    break;
                case "naufor":
                    url = "/QIVParam/GetNauforYear";
                    break;
                case "msfo":
                    url = "/QIVParam/GetMsfoYear";
                    break;
            }
            $.post(url, {},
                   function (data) {
                       $("#yearVal" + id).text(data);
                       if (data.length > 4) {
                           var q = data.substring(0, 1);
                           var y = data.substring(data.length - 4, data.length);
                           data = y + q;
                       }
                       $("#yearVal" + id).attr("val", data);
                       getDefaultParam(id);
                   }
           );
        }
    }

    function getDefaultParam(id) {
        $("#paramVal" + id).html("");
        $("#paramVal" + id).attr("val", "0");
        $("#paramVal" + id).attr("type", "0");
        var y = $("#yearVal" + id).attr("val");
//        if (y !== "") {
        if (id.length > 0) {
            var name = id.substring(0, id.length - 1);
            var url = "";
            switch (name) {
                case "gks":
                    url = "/QIVParam/GetDefaultParam";
                    break;
                case "naufor":
                    url = "/QIVParam/GetNauforParam";
                    break;
                case "msfo":
                    url = "/QIVParam/GetMsfoParam";
                    break;
            }
            $.post(url, { "period": y },
                   function (data) {
                       if (name == "msfo") {
                           selectNewParam(data.id, data.name, id, data.type_id);
                       }
                       else {
                           selectNewParam(data.id, "<b>" + data.line_code + "</b> " + data.name, id, data.type_id);
                       }
                       
/*
                       $("#paramVal" + id).html("<b>" + data.line_code + "</b> " + data.name);
                       $("#paramVal" + id).attr("val", data.id);
*/
                   },
           "json");
        }
    }

   
    function getDefaultTabHeader() {
        sort_col = 0;
        sort_direct = "1"; // 1 - desc, 0 - asc, -1 - сортировка не выбрана
        sort_icon = "icon-down";
        //$("span[id^='icon'").removeClass().addClass("icon-none");
    }
    
    toggleTab = function (tab_ndx) {
        cur_qiv_tab = tab_ndx;
        for (var i = 0; i < sub_tabs.length; i++) {
            if (i === tab_ndx) {
                $('#qiv_tab_' + sub_tabs[tab_ndx].name).addClass("active");
                $('#qivparam-group_' + sub_tabs[tab_ndx].name).removeClass("hidden");
            } else {
                $('#qiv_tab_' + sub_tabs[i].name).removeClass("active");
                $('#qivparam-group_' + sub_tabs[i].name).addClass("hidden");
            }
        }
    }

    var maxParam = 5;

    checkParam = function ()
    {
        var tab_name = sub_tabs[cur_qiv_tab].name;
        if ($('[id^="qivparam_' + tab_name + '"').length > (maxParam - 1)) {
            $('#qiv_param_add').addClass("disabled");
        }
        else {
            $('#qiv_param_add').removeClass("disabled");
        }
        if ($('[id^="qivparam_' + tab_name + '"').length > 1) {
            $('[id^="btn_' + stab.name + '"').removeClass("disabled");
        }
        else {
            $('[id^="btn_' + stab.name + '"').addClass("disabled");
        }

    }

    addClickToggleHandler = function (source_ndx, source_array) {
        var sndx = source_ndx;
        var sarray = source_array;
        var handler = function () {
            toggleTab(sndx);
            if ($('[id^="qivparam_' + sarray[sndx].name + '"').length == 0) {
                addParam();
            }
            checkParam();
        }
        $('#qiv_tab_' + sarray[sndx].name).click(handler);
    }

    var cur_qiv_tab = 0;
    var sub_tabs = [{ name: 'gks', id: 0, maxid: 0 }, { name: 'naufor', id: 1, maxid: 0 }, { name: 'msfo', id: 2, maxid: 0 }];
    for (var i = 0; i < sub_tabs.length; i++) {
        addClickToggleHandler(i, sub_tabs);
    }

    
    addParam = function (param) {
        stab = sub_tabs[cur_qiv_tab];

        if ($('[id^="qivparam_' + stab.name + '"').length < maxParam)
        {
            stab.maxid++;
            $('#qivparam-group_' + stab.name).append(qivparam_pattern.split("{id}").join(stab.maxid).split("{name}").join(stab.name));
            addDelHandler(stab.maxid, stab.name);
            if (!param) {
                getDefaultYear(stab.name + String(stab.maxid));
            }
        }
        if (param) {
            var period = param.period;
            if (param.period.length > 4) {/*naufor*/
                period = param.period.substr(4, 1) + "кв. " + param.period.substr(0, 4);
            }
            $("#yearVal" + stab.name + stab.maxid).attr("val", param.period).text(period);
            $("#paramVal" + stab.name + stab.maxid).attr("val", param.param_id).data("type", param.param_type);
            $("#fromVal" + stab.name + stab.maxid).val(param.from);
            $("#toVal" + stab.name + stab.maxid).val(param.to);
            write_param_name($("#paramVal" + stab.name + stab.maxid), stab.name);
        }
        checkParam();
    }

    var write_param_name = function ($el,tab_name) {
        var id = Number($el.attr("val"));
        var type_id = $el.data("type");
        $.post("/QIVParam/GetCodeNames", { id: id, type_id: type_id, tab_name: tab_name }, function (data) {
            $el.html(restrictGropName("<b>" + data.code + "</b> " + data.name, 52));
        }, "json")
    }

    addDelHandler = function (id, name) {
        var _id = id;
        var _name = name;
        var handler = function () {
            stab = sub_tabs[cur_qiv_tab];
            if ($('[id^="qivparam_' + _name + '"').length > 1)
                $("#qivparam_" + _name + _id).remove(); 
            if ($('[id^="qivparam_' + stab.name + '"').length < maxParam)
                $('#qiv_param_add').removeClass("disabled"); 
            if ($('[id^="qivparam_' + stab.name + '"').length === 1)
                $('[id^="btn_' + stab.name + '"').addClass("disabled");
        }
        $("#btn_" + _name + _id).click(handler);
    }

    addParam();

    window.search_init = function () {

        $('body').on('click', function (e) {
            hidepopups(e);
            _hide_command_menu(e);
        });
        //.on("keypress", function (e) { try2search(e); });

        getObj("xls_frame").onreadystatechange = hideClock;
        document.onreadystatechange = hideClock;

        $("#xls_frame").ready(function () {
            hideClock();
        })

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
                }
            });
        }
    }

    var _hide_command_menu = function (e) {
        if (pattern_menu_open) {
            $('.sub_command_menu').remove();
            pattern_menu_open = false;
        }

        if (group_menu_open) {
            $('.sub_command_menu').remove();
            group_menu_open = false;
        }

        if (excel_menu_open) {
            $('.sub_command_menu').remove();
            excel_menu_open = false;
        }
    }

    function getParamJSON(tab_name)
    {
        var res = [];

        var pdiv = $('[id^="qivparam_' + tab_name + '"');
        for (var i = 0; i < pdiv.length; i++) {
            var pid = $(pdiv[i]).attr("id");
            pid = pid.split("qivparam_")[1];
            res.push({ 
                "period": $("#yearVal"+pid).attr("val"), 
                "param_id": $("#paramVal" + pid).attr("val"),
                "param_type": $("#paramVal" + pid).attr("type"),
                "from": $("#fromVal" + pid).val(),
                "to": $("#toVal" + pid).val()
                });
        }
        return res;
    }

    function getParamName(tab_name) {
        var res = [];

        var pdiv = $('[id^="qivparam_' + tab_name + '"');
        for (var i = 0; i < pdiv.length; i++) {
            var pid = $(pdiv[i]).attr("id");
            pid = pid.split("qivparam_")[1];
            var period = $("#yearVal" + pid).attr("val");
            if (period.length > 4) {
                period = period.substring(4,period.length) + "кв. " + period.substring(0,4);
            }
            res.push(period + "г."+ "<br />" + $("#paramVal" + pid).text());
        }
        return res;
    }

    var mainSearcher = function (pg, group_name, issuers) {
        var group_id = $("#gropVal").attr("val");
        var rcount = $("#countVal").attr("val");
        //var top1000 = ((group_name.length > 0 && pg == -1000) ? "1" : (pg == -2000) ? "2" : "0");
        var result_type = ((group_name.length > 0 && pg == -1000) ? 1 : (pg == -2000) ? 2 : 0);
        var params = get_so_params(pg, group_name, issuers);

        if (result_type == 2) {
            //XSL
            hideClock();
            searching = 0;
            var form = document.createElement("form");
            form.action = "/QIVSearch/DoSearchToExcel";
            form.method = "POST";
            element = document.createElement("input");
            element.type = "hidden";
            element.name = "string_params";
            element.value = JSON.stringify(params);
            form.appendChild(element);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
        else {
            if (result_type == 1) {
                //Group
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'html',
                    type: 'POST',
                    url: '/QIVSearch/DoSearchToGroup',
                    data: JSON.stringify(params),
                    success: function (data) {
                        if (data) {
                            var ret = SaveResultGroup(data, group_name);
                            var amsg = String(ret).split("_");
                            showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
                        }
                        hideClock();
                        searching = 0;
                    }
                });

                   /*     $.post("/QIVSearch/DoSearchToGroup", params, function (data) {
                    if (data) {
                        var ret = SaveResultGroup(data,group_name);
                        var amsg = String(ret).split("_");
                        showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
                    }
                    hideClock();
                    searching = 0;
                        }, "html");*/
            }
            else {
                console.log(params);
                //Web
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    type: 'POST',
                    url: '/QIVSearch/DoSearch',
                    data: JSON.stringify(params),
                    success: function (data) {
                        //                    unlock_button('#btn_search');
                        /*if (pg == -1000) {
                            // Добавление в группу
                            var ret = SaveResultGroup(data, group_name);
                            var amsg = String(ret).split("_");
                            showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
                        }*/
                        hideClock();
                        GenerateResult(pg, rcount, getParamName(sub_tabs[cur_qiv_tab].name), data, roles_object);

                    },
                    error: function () {
                        unlock_button('#btn_search');
                    }
                });
            }
        }
    };

    function sdf_FTS(_number,_decimal,_separator)
        // сокращение переводится как Float To String
        // _number - число любое, целое или дробное не важно
        // _decimal - число знаков после запятой
        // _separator - разделитель разрядов
    {
        // определяем, количество знаков после точки, по умолчанию выставляется 2 знака
        var decimal=(typeof(_decimal)!='undefined')?_decimal:2;

        // определяем, какой будет сепаратор [он же разделитель] между разрядами
        var separator=(typeof(_separator)!='undefined')?_separator:'';

        // преобразовываем входящий параметр к дробному числу, на всяк случай, если вдруг
        // входящий параметр будет не корректным
        var r=parseFloat(_number)

        // так как в javascript нет функции для фиксации дробной части после точки
        // то выполняем своеобразный fix
        var exp10=Math.pow(10,decimal);// приводим к правильному множителю
        r=Math.round(r*exp10)/exp10;// округляем до необходимого числа знаков после запятой

        // преобразуем к строгому, фиксированному формату, так как в случае вывода целого числа
        // нули отбрасываются не корректно, то есть целое число должно
        // отображаться 1.00, а не 1
        rr=Number(r).toFixed(decimal).toString().split('.');

        // разделяем разряды в больших числах, если это необходимо
        // то есть, 1000 превращаем 1 000
        b=rr[0].replace(/(\d{1,3}(?=(\d{3})+(?:\.\d|\b)))/g,"\$1"+separator);
        r=b+((_decimal > 0)? ('.'+rr[1]):"");

        return r;// возвращаем результат
    }


    window.generate_sub_addGroupmenu = function (e,total) {
        if (group_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Добавить выбранные компании в Группу").click(function () { doSave2List(); }).appendTo($ul);
        $("<li>").text(total > group_limit ? "Добавить первые " + group_limit + " компаний в Группу" : "Добавить найденные компании (" + total + ") в Группу").click(function (event) { doCreateGroup1000(event); }).appendTo($ul);
        $("#btnaddGroup").append($ul);
        group_menu_open = true;
        e.stopPropagation();
    }

    window.generate_sub_excelmenu = function (e,total) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным компаниям").click(function () { doSave2XLS(); }).appendTo($ul);
        $("<li>").text("Экспорт по " + (total>10000? "первым 10000" : total)+" компаниям").click(function () { doExport10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
    }


    var GenerateResult = function (page, rcount, value_names, data, ro) {
        $("#search_result").html('');

        var total = data.total;
        var page_count = total / rcount;

        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total && total > 0) {
            var res_arr = data["results"];
            var res_html = [];
            res_html.push("<span class=\"total_count\">" + "Всего найдено: " + total + "</span>");

            if (ro.canAddToGroup || ro.canExport) {
                res_html.push("<div class=\"export_block\"><div class=\"checkall_block\"><span class=\"title\"><em>Выделить все</em><input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" title=\"Выделить все\"></span></div><div class=\"export_command_block\"><span class=\"title\"><em>Добавить в Группу</em><div id=\"btnaddGroup\" onclick=\"generate_sub_addGroupmenu(event," + total + "); \" title=\"Добавить компании в Группу\"><span class=\"icon-plus icon\"></span></div></span><span class=\"title\"><em>Экспорт в Excel</em><div id=\"btnaddExcel\" onclick=\"generate_sub_excelmenu(event," + total + ");\" title=\"Экспорт компаний\"><span class=\"icon-file-excel icon\"></span></div></span></div></div>");
            }

            res_html.push("<table class=\"data_table\">");

            res_html.push("<tr>");
            res_html.push("<th></th><th>Наименование компании</th>");

            for (var i = 0, li = value_names.length; i < li; i++) {
                var sort = "";
                if (sort_col == i) {
                    sort = sort_icon;
                }
                else {
                    sort =  "icon-none";
                    //$('[id="icon_' + i + '"]').attr("class", "icon-up");
                }

                res_html.push("<th id=\"col" + i + "\" col=\"" + i + "\" direct=\"" + sort_direct + "\" onclick=\"sortOnOff(" + i + "," + page + ");\" style=\"cursor:pointer;text-align:center; vertical-align:top;\"><div class=\"th_box\"><div style=\"width:90%;\">" + value_names[i] + "</div><div style=\"width:10px; margin-top:20px;\"\"><span id=\"icon_" + i + "\" class=\"" + sort + "\">&nbsp;&nbsp;</span></div></th>");
            }
            res_html.push("</tr>");
            for (var i = 0, li = res_arr.length; i < li; i++) {
                res_html.push("<tr>");
                res_html.push("<td class=\"result_item\">");
                if (ro.canAddToGroup || ro.canExport) {
                    res_html.push("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" v=\"" + res_arr[i].gks_id + "\" type=\"checkbox\" value=\"" + res_arr[i].issuer_id + "_" + res_arr[i].type_id + "\"></input>");
                }
                res_html.push("</td>");
                if (res_arr[i].ticker != null && res_arr[i].ticker != ""){
                    res_html.push("<td><a class=\"comp_title\" href=\"/issuers/" + res_arr[i].ticker + "\" target=\"_blank\">" + res_arr[i].name + "</a></td>");
                }
                else {
                    res_html.push("<td>" + res_arr[i].name + "</td>");
                }
                for (var j = 0, lj = res_arr[i].param_values.length; j < lj; j++) {
                    if (res_arr[i].param_values[j] == null)
                    {
                        res_html.push("<td class=\"cell_number\">-</td>");
                    }
                    else
                    {
                        res_html.push("<td class=\"cell_number\" style=\"text-align:right;white-space:nowrap;\">" + sdf_FTS(res_arr[i].param_values[j], 0, " ") + "</td>");
                    }
                }
                res_html.push("</tr>");
            }
            $("#search_result").append(res_html.join(""));
            $("#search_result").append(_get_paging(page, page_count))

        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);            
        }
    }

    var SaveResultGroup = function (data, group_name) {
        if (data) {
            var cnt = 0;
            for (var j = 0, j_max = (extGroupList.length - 1) ; j <= j_max; j++) {
                var name = extGroupList[j].txt;
                if (extGroupList[j].txt == group_name) {
                    cnt = parseInt(extGroupList[j].val);
                    return "1_Группа с таким именем уже есть";
                }
            }
            close_dialog();
            hideClock();
            return "0_" + data;
        }
        else {
            return "1_Внутренняя ошибка";
        }
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
        var ss = $("#reg_val").val() + $("#ind_val").val() + $("#okopf_val").val() + $("#okfs_val").val() +
            $("#status_val").val() +
            ((getObj("l2")&&getObj("l2").checked) ? "1" : "") + ((getObj("l3")&&getObj("l3").checked) ? "1" : "") +
            ((getObj("gaap")&&getObj("gaap").checked) ? "1" : "") + ((getObj("bankr")&&getObj("bankr").checked) ? "1" : "") + $("#codes").val() +
            $("#dfrom").val() + $("#dto").val() + ((String($("#gropVal").attr("val")) == "0") ? "" : "1");
        return (ss.length == 0) ? false : true;
    };

    var draw_selection = function (i) {
        if (getObj("tdhover" + i)){
            var d=getObj("tdhover" + i)
            $("#dpw").find('td').each(function(){
                this.className = "bones_pointer";
            }); 
            d.className="bones_pointer over_color";
            $("#comp").val($("#tdhover" + i).text());
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
    }

    var hidepopups = function (event) {
        if (getObj(tree_active + "_window")) {
            getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window"));
            $("#" + tree_active).css({ 'background-color': '#FFFFFF' });
        }
        close_dialog();
        if (event) {
            remove_div(event, "#groupSelector", "#groupSelect");
            remove_div(event, "#countSelector", "#countSelect");
            remove_div(event, "#tmplSelector", "#tmplSelector");
            for (i = 1; i < 6; i++) {
                remove_div(event, "#yearSelectorgks"+i, "#yearSelectgks"+i);
                remove_div(event, "#yearSelectornaufor"+i, "#yearSelectnaufor"+i);
                remove_div(event, "#yearSelectormsfo"+i, "#yearSelectmsfo"+i);
            }
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

    var show_dictionary = function () {
        var $placer = $("body"); // контенер для добавления
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

    var restrictGropName = function(groupName) {
        if (groupName.length > 52)
            groupName = groupName.substring(0, 52) + "...";
        return groupName;
    }

    var check_monitor = function () {
        $.post('/Operation/EgrulMonitoringList', null, function (data) {
            if (data) {
                check_monitor_callback(data);
            }
            else {
                check_monitor_callback("");
            }
        });
    }

    var check_monitor_callback = function(iss_list) {
        var issuers = iss_list.split(',');
        $('[id*="tdiss_"]').each(function (i) {
            var issuer_id = $(this).attr('issuer');
            if ($.inArray(issuer_id, issuers) > -1) {
                //Удалить из списка мониторинга ЕГРЮЛ
                $(this).html("<span class=\"title\"><em>Удалить из списка мониторинга ЕГРЮЛ</em><span class=\"monitor_icon_block\" onclick=\"delIssuer('" + issuer_id + "',1,true);\"><span class=\"icon-minus-squared-alt\"></span><span class=\"icon-binoculars\"></span></span></span>");
            }
            else {
                //Добавить в список мониторинга ЕГРЮЛ
                $(this).html("<span class=\"title\"><em>Добавить в список мониторинга ЕГРЮЛ</em><span class=\"monitor_icon_block\" onclick=\"doAddEGRUL('" + issuer_id + "');\"><span class=\"icon-plus-squared-alt\"></span><span class=\"icon-binoculars\"></span></span></span>");
            }
        });
    }

    window.doSearch = function (pg) {
        if (roles_object.canSearch) {
            if (checkCanSearch()) {
                searching = 1;
                pg = (isNaN(String(pg))) ? 1 : pg;
                showClock();
                getDefaultTabHeader();
                mainSearcher(pg, '');
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        } else {
            no_rights();
        }
    };

    var doSave2XLS = function () {
        var issuers = "";
        if ($("input:checkbox:checked[name=selsissuer]").length > 0) {
            $("input:checkbox:checked[name=selsissuer]").each(function (i) {
                issuers += String(this.value).split("_")[0] + ",";
            })
            issuers = issuers.substring(0, issuers.length - 1);
            mainSearcher(-2000, '', issuers);
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
        }

    }

    var doExport10000 = function () {
        showClock();
        mainSearcher(-2000, '','');
    }

    window.doSetCheckedAll = function (chb) {
        $(".result_item").find("input:checkbox").each(function (i) {
            this.checked = chb.checked;
        });

    };

    window.checkOnOff = function(o) {
        var allCheckboxes = $(".result_item").find('input:checkbox');
        var checkedCheckboxes = $(".result_item").find('input:checkbox:checked');
        var allChecked = allCheckboxes.length == checkedCheckboxes.length;
        $("#selallbox").get(0).checked = allChecked;        
    }

    window.sortOnOff = function (col, pg) {
        var direct = $("#col" + col).attr("direct"); //1 - desc(icon-down), 0 - asc(icon-up)
        if (sort_col != col) {
            direct = "-1";
        }
        sort_col = col;
        $("span[id^='icon'").removeClass().addClass("icon-none");
        $("th[id^='col']").each(function () {
            $(this).attr("direct", "-1");
            if ($(this).attr("col") == sort_col) {
                switch (direct) {
                    case "-1":
                        $("#icon_" + sort_col).removeClass("icon-none").addClass("icon-down");
                        sort_direct = "1";
                        sort_icon = "icon-down";
                        break;
                    case "0":
                        $("#icon_" + sort_col).removeClass("icon-none").addClass("icon-down");
                        sort_direct = "1";
                        sort_icon = "icon-down";
                        break;
                    case "1":
                        $("#icon_" + sort_col).removeClass("icon-none").addClass("icon-up");
                        sort_direct = "0";
                        sort_icon = "icon-up";
                };
            }
        });
        showClock();
        mainSearcher(pg, '');
    }

    /*
    window.sortOnOff = function (col, pg) {
        sort_col = col;
        var direct = $("#col" + sort_col).attr("direct"); //1 - desc(icon-down), 0 - asc(icon-up)
        $("span[id^='icon'").removeClass();
        $("th[id^='col']").each(function () {
            $(this).attr("direct", "-1");
            if ($(this).attr("col") == sort_col) {
                switch (direct) {
                    case "-1":
                        $("#icon_" + sort_col).removeClass("icon-none").addClass("icon-down");
                        sort_direct = "1";
                        sort_icon = "icon-down";
                        break;
                    case "0":
                        $("#icon_" + sort_col).removeClass("icon-none").addClass("icon-down");
                        sort_direct = "1";
                        sort_icon = "icon-down";
                        break;
                    case "1":
                        $("#icon_" + sort_col).removeClass("icon-none").addClass("icon-up");
                        sort_direct = "0";
                        sort_icon = "icon-up";
                };
            }
        });
        showClock();
        mainSearcher(pg, '');
    }
    */
    window.doClear = function () {
        $("#countVal").text(countParams[0]);
        $("#countVal").attr("val", countParams[0]);
        $("#gropVal").text(restrictGropName(extGroupList[0].txt));
        $("#gropVal").attr("val", extGroupList[0].code);
        $("#tmplVal").attr("val", 0);
        $("#tmplVal").text("Нет");

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

        maxid = 0;
        $('.param_row').remove();
//        addParam();
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
                getObj("comp").focus();
            }
        }
    };

    window.dispissuer = function (text) {
        $("#comp").val(text).css("border-radius", "4px");
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
        }
    };

    window.show_tree_selector = function (e, sr, is_tree, mult) {

        src = (sr == 99) ? 24 : sr;
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
            } 
            var content = "<div id=\"td_" + tree_active + "\"></div>";
            show_dictionary();
            getObj("td_" + tree_active).appendChild(d);

            showContentClock('#dic_container .modal-dialog');
            
            $("#dic_container").click(function (event) {
//                event.stopPropagation();
            })
        }
    };

    window.show_param_selector = function (event, id) {
        if (window.event) {
            event = window.event;
        }
        if (event.stopPropagation) {
            event.stopPropagation();
        } else {
            event.cancelBubble = true;
        }
        var year = $("#yearVal" + id).attr("val");
        if (year && year!=="")
        {
            tree_active = "paramSelect"+id;
            $("#" + tree_active).css({ 'background-color': '#F0F0F0' });
            var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
            if (!getObj(tree_active + "_window")) {
                d = document.createElement("iframe");
                d.className = "tree_frame";
                d.id = tree_active + "_window";
                d.frameBorder = "0";
                //id = id.substring(0, id.length - 1);
                d.src = "/QIVParam/QIVParamSelector?year=" + year + "&id=" + id;
                var content = "<div id=\"td_" + tree_active + "\"></div>";
                show_dictionary();
                getObj("td_" + tree_active).appendChild(d);

//                showContentClock('#dic_container .modal-dialog');

                $("#dic_container").click(function (event) {
//                    event.stopPropagation();

                })
            }
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
            })
        } else {
            $("#" + tree_active).val("");
            $("#" + tree_active).attr({ "title": eval("titles." + tree_active) });
            $("#" + tree_active + "_excl").val(0);
            $("#" + tree_active + "_val").val("");
        }
        hidepopups();
        
    };

    window.selectNewGroup = function (i) {
        $("#gropVal").text(restrictGropName(extGroupList[i].txt));
        $("#gropVal").attr("val", extGroupList[i].code);
        document.body.removeChild(getObj("groupSelector"));
        $("#groupSelect").removeClass("customSelect_focus");
    }

    window.selectNewYear = function (i, name, period) {
        var id = name.substring(0, name.length - 1);
        switch (id) {
            case "gks":
                yearList = yearList;
                break;
            case "naufor":
                yearList = nauforYearList;
                break;
            case "msfo":
                yearList = msfoYearList;
                break;
        }
        $("#yearVal"+name).text(yearList[i]);
        $("#yearVal" + name).attr("val", period);
        document.body.removeChild(getObj("yearSelector" + name));
        $("#yearSelect" + name).removeClass("customSelect_focus");
        getDefaultParam(name);
    }

    window.selectNewParam = function (param_id, param_name, elem_id, type) {
        var max_length = 52;
        if (param_name.length > max_length)
        {
            $("#paramVal" + elem_id).html(param_name.substring(0, max_length)+'...');
        }
        else
        {
            $("#paramVal" + elem_id).html(param_name);
        }
        $("#paramVal" + elem_id).attr("val", param_id);
        $("#paramVal" + elem_id).attr("type", type);
//        $("#paramSelect" + elem_id).removeClass("customSelect_focus");
        hidepopups();
    }

    window.show_groupSelector = function () {
        var d;


        if (!getObj("groupSelector")) {
            d = document.createElement("div");
            d.id = "groupSelector";
            d.className = "dp_window";


            var boundes = $("#groupSelect").parent().position();
            d.style.top = boundes.top + content_top + $("#groupSelect").height() + "px";
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
                 + i + ");'><td id='tdgroup" + i + "' class='bones_pointer' >" + extGroupList[i].txt + "</td></td><td class='bones_pointer' style='text-align:right;width:90px;color:#999;font-size:14px;white-space:nowrap;'>"
                  + (extGroupList[i].val == "" ? "все&nbsp;ЮЛ и ИП" : extGroupList[i].val) +  "</td></tr>";
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

    window.show_yearSelector = function (name) {
        var d;
        if (!getObj("yearSelector" + name)) {
            d = document.createElement("div");
            d.id = "yearSelector" + name;
            d.className = "dp_window";

            var elem = $("#yearSelect" + name);
            var boundes = elem.parent().position();
            d.style.top = boundes.top + content_top + $("#yearSelect" + name).height() + "px";
            d.style.left = boundes.left + elem.position().left+ "px";
            d.style.width = (elem.outerWidth()-2) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("yearSelector" + name));
            $("#yearSelect" + name).removeClass("customSelect_focus");
        }
        if (getObj("yearSelector" + name)) {
            var years;
            if (name.length > 0) {
                var id = name.substring(0, name.length - 1);
                switch (id) {
                    case "gks":
                        years = yearList;
                        break;
                    case "naufor":
                        years = nauforYearList;
                        break;
                    case "msfo":
                        years = msfoYearList;
                        break;
                }
            }
            $("#yearSelect" + name).addClass("customSelect_focus");
            var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"yeartbl\">"
            for (var i = 0; i < years.length; i++) {
                var period = years[i];
                if (years[i].length > 4) {
                    var q = years[i].substring(0, 1);
                    var y = years[i].substring(years[i].length - 4, years[i].length);
                    period = y + q;
                }
                htm += "<tr class='selec_option'  onClick='selectNewYear("
                 + i + ",\"" + name + "\"," + period + ");'><td id='tdgroup" + i + "' class='bones_pointer'>" + years[i] + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            d.style.display = "block";
            $("#yeartbl").one("mouseenter", function () {
                $('#tdgroup' + $('#yearVal' + name).attr('val')).attr('className', 'bones_pointer');
            })

        }
    }

    window.show_tmplSelector = function () {
        $.get("/QIVSearch/GetTemplates", function (data) {
            var d;

            if (!getObj("tmplSelector")) {
                d = document.createElement("div");
                d.id = "tmplSelector";
                d.className = "dp_window";


                var boundes = $("#tmplSelect").parent().position();
                d.style.top = boundes.top + content_top + $("#tmplSelect").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#tmplSelect").width() + 12) + "px";
                document.body.appendChild(d);
            }
            else {
                document.body.removeChild(getObj("tmplSelector"));
                $("#tmplSelect").removeClass("tmplSelect_focus");
            }
            if (getObj("tmplSelector")) {
                $("#tmplSelect").addClass("customSelect_focus");
                var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"tmpltbl\">"
                for (var i = 0; i < data.length; i++) {
                    htm += "<tr class='selec_option'  onClick='selectNewTemplate("
                     + data[i].id + ",\"" + data[i].name + "\");'><td  class='bones_pointer'>" + data[i].name + "</td></tr>";
                }
                htm += "</table>";
                d.innerHTML = htm;
                d.style.display = "block";

            }
        });
    }

    window.get_so_params = function (pg,group_name,issuers) {
        var group_id = $("#gropVal").attr("val");
        var rcount = $("#countVal").attr("val");
        var tab_name = sub_tabs[cur_qiv_tab].name;
        //var top1000 = ((group_name.length > 0 && pg == -1000) ? "1" : (pg == -2000) ? "2" : "0");
        var result_type = ((group_name.length > 0 && pg == -1000) ? 1 : (pg == -2000) ? 2 : 0);
        var tp = {
            "qiv_type": sub_tabs[cur_qiv_tab].id,
            "regions": $("#reg_val").val(),
            "is_okato": $("input:radio[name=reg_type]").filter(":checked").val(),
            "reg_excl": (String($("#reg_excl").val()).length == 0) ? 0 : $("#reg_excl").val(),
            "industry": $("#ind_val").val(),
            "ind_excl": (String($("#ind_excl").val()).length == 0) ? 0 : $("#ind_excl").val(),
            "ind_main": ($("input[name='ind_type']:checked").val() - 99 == 0) ? "1" : "0",
            "okopf": $("#okopf_val").val(),
            "okopf_excl": (String($("#okopf_excl").val()).length == 0) ? 0 : $("#okopf_excl").val(),
            "okfs": $("#okfs_val").val(),
            "okfs_excl": (String($("#okfs_excl").val()).length == 0) ? 0 : $("#okfs_excl").val(),
            "dbeg": $("#dfrom").val(),
            "dend": $("#dto").val(),
            "group_id": group_id,
            "page_no": pg,
            "rcount": rcount,
            "result_type": result_type,
            "group_name": ((group_name.length > 0 && pg == -1000) ? group_name : ""),
            "andor": $("input:radio[name=cond_selector_" + tab_name + "]").filter(":checked").val(),
            "issuers": issuers,
            "param_lines": getParamJSON(sub_tabs[cur_qiv_tab].name),
            "rgstr": ((getObj("rgstr").checked) ? 1 : 0),
            "sort_col": sort_col,
            "sort_direct": sort_direct

        };
        return tp;
    }

    var scanGroups = function (code) {
        var ret = 0;
        for (var i = 0; i < extGroupList.length; i++) {
            if (extGroupList[i].code == code) {
                ret = i
            }
        }
        return ret;
    }

    var Write_Loaded = function (retval, is_excl, objid, tree_id) {
        if (retval.length > 0) {
            $.post("/Tree/GetResultString", { "src": tree_id, "id": retval },
            function (data) {
                $("#" + objid).val(((is_excl == -1) ? "Искл.: " : "") + data);
                $("#" + objid + "_excl").val(is_excl);
                $("#" + objid + "_val").val(retval);
            })
        } else {
            $("#" + objid).val("");
            $("#" + objid + "_excl").val(0);
            $("#" + objid + "_val").val("");

        }
        hidepopups();
    }

    window.restore_so_params = function (so, template) {
        $("#countVal").text(so.rcount);
        $("#countVal").attr("val", so.rcount);
        $("#gropVal").attr("val", so.group_id);
        $("#gropVal").text(extGroupList[scanGroups(so.group_id)].txt);

        $("#tmplVal").text(template.name);
        $("#tmplVal").attr("val", template.id);

        $("#okopf_val").val(so.okopf);
        $("#okopf_excl").val(so.okopf_excl);
        Write_Loaded(so.okopf, so.okopf_excl, "okopf", 4);

        $("#reg_val").val(so.regions);
        $("#reg_excl").val(so.reg_excl);
        Write_Loaded(so.regions, so.reg_excl, "reg", 0);

        $("#ind_val").val(so.industry);
        $("#ind_excl").val(so.ind_excl);
        Write_Loaded(so.industry, so.ind_excl, "ind", 24);

        $("#okfs_val").val(so.okfs);
        $("#okfs_excl").val(so.okfs_excl);
        Write_Loaded(so.okfs, so.okfs_excl, "okfs", 3);


        $("input:radio[name=ind_type][value='" + (so.ind_main == 1 ? "99" : "24") + "']").prop("checked", true);
        $("input:radio[name=reg_type][value='" + (so.is_okato == "5" ? "5" : "0") + "']").prop("checked", true);

        $("input:checkbox[id=rgstr]").prop("checked", so.rgstr == 1);

        toggleTab(so.qiv_type);

        var tab_name = sub_tabs[cur_qiv_tab].name;
        $("input:radio[name=cond_selector_" + tab_name + "][value='" + so.andor + "']").prop("checked", true);
        maxid = 0;
        $('.param_row').remove();
        for (var i = 0; i < so.param_lines.length; i++) {
            addParam(so.param_lines[i]);
        }
        checkParam();
    }

    window.selectNewTemplate = function (id, name) {
        $("#tmplVal").text(restrictGropName(name));
        document.body.removeChild(getObj("tmplSelector"));
        $.get("/QIVSearch/GetTemplate", { id: id }, function (data) {
            doClear();
            restore_so_params(data, { id: id, name: name });
        });
    }

    var save_template_dialog = function (event) {
        event.stopPropagation();
        var html = "<div id=\"templatedialog\"><div class=\"form-group\"><label class=\"label_form\" style=\"width:250px;\">Введите имя шаблона:</label><input type=\"text\" class=\"search_input\" style=\"width:250px;\" id=\"ntemplate\"/></div>";
        html += "<div style='text-align:center;margin:20px 0;'><div class=\"error_list\"></div><button  class=\"btns darkblue\" onclick=\"save_template();\" >Сохранить</button></div></div>";
        show_dialog({ content: html, $placer: $("#main") });
        $("#templatedialog").click(function (event) {
            event.stopPropagation();

        });
    }

    window.save_template = function () {
        var $error_list = $('.error_list');
        $error_list.html('');
        var name = $('#ntemplate').val();
        if (name.length == 0) {
            $error_list.html("<ul><li>Введите имя шаблона</li></ul>");
        } else {
            var tp = get_so_params(1,'');
            $.post("/QIVSearch/SaveTemplate", { name: name, template: JSON.stringify(tp) }, function (data) {
                close_dialog();
                showwin("info", "<p align=\"center\">Сохранено</p>", 2000);
            });
        }
    }

    window.show_countSelector = function () {
        var d;


        if (!getObj("countSelector")) {
            d = document.createElement("div");
            d.id = "countSelector";
            d.className = "dp_window";


            var boundes = $("#countSelect").parent().position();
            d.style.top = boundes.top + content_top + $("#countSelect").height() + "px";
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
    }

    window.selectNewCount = function (i){
        $("#countVal").text(countParams[i]);
        $("#countVal").attr("val", countParams[i]);
        document.body.removeChild(getObj("countSelector"));
        $("#countSelect").removeClass("customSelect_focus");
    }

    window.USERGROPS = [];

    window.doSave2List = function (issuer_id) {
        var issuers = "";
        if ($(".result_item").find('input:checkbox:checked').length > 0 || issuer_id) {

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
                                if (!data[0].name && getObj("modal_dialog")) {
                                    close_dialog();
                                } else {
                                    for (var i = 0; i < data.length; i++) {
                                        htm += "<tr><td  id=\"tdhover" + i + "\" class=\"bones_pointer\" ><a href=\"#\"   onclick=\"dispgroup(this,'" + data[i].name + "'," + data[i].lid + ")\" id=\"s" + data[i].lid + "\">" + data[i].name + "</a></td></tr>";
                                    }
                                }
                            }
                            htm += "</table></div>";
                            htm += "<h4>или создайте новую</h4><div><label class=\"label_form\" style=\"width:150px;\">Новая группа:</label><input type=\"text\" class=\"search_input\" style=\"width:350px;\" id=\"ngroup\"/></div><div id=\"error_groups\" class=\"error_login\"></div>";
                            htm += "<div style='text-align:center;margin:20px 0;'><button  class=\"btns darkblue\" onclick=\"doSaveGroups(" + issuer_id + ");\" >Сохранить</button></div></div>";
                           

                            show_dialog({ "content": htm, "is_print": false });
                            $("#group_dialog").click(function (event) {
                                event.stopPropagation();

                            });

                        }, "html");
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для включения в группу</p>', 3000);
        }
    };

    window.doSaveGroups = function (issuer_id) {
        if ($("#ngroup").val().length > 0) {
            g_id = 0;
            var new_name = $("#ngroup").val();
            for (var i = 0; i < USERGROPS.length; i++) {
                if (USERGROPS[i].name == new_name.trim()) {
                    $('#error_groups').html('Данное имя группы уже существует');
                    return;
                }
            }
        }
        var issuers = (String(issuer_id) != "undefined") ? issuer_id : "";
        var cnt = 0;
        $(".result_item").find("input:checkbox:checked").each(function (i) {
            issuers += this.value + ",";
            cnt++;
        })
        
        $.post("/Modules/SaveGroup/", { "id": g_id, "iss": issuers, "newname": $("#ngroup").val(), "is1000": false },
            function (data) {
                if (data.length > 0) {
                    close_dialog();
                    showwin("critical", data, 0);
                } else {
                    close_dialog();
                    updateGroups(user_id);
                    showwin("info", "<p align=\"center\">Сохранено</p>", 2000);
                }
            }
        )
    }
    
    window.updateGroups = function () {
        $.post("/Modules/UpdateGroups", null,
               function (data) {
                   extGroupList = data;
               },
       "json");
    }


    window.dispgroup = function (elem, name, id) {
        $("#dpw").find("a").each(function (i) {
            $(this).removeClass("selected");
        })

        $("#" + elem.id).addClass("selected");
        g_id = id;
        $("#ngroup").val("");
    }


    window.doCreateGroup1000 = function (event) {
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
            mainSearcher(-1000, group_name);
        }, "json")

       
    };

    
    window.doAddEGRUL = function (issuer_id) {
        showClock()
        $.post("/MonitorOperations/AddToEgrul", { "id": issuer_id },
            function (data) {
                hideClock();
                if (data >= 0) {
                    showwin("info", "Добавлено компаний в список монитринга ЕГРЮЛ:" + data, 2000);
                    if (data > 0) {
                        getObj("tdiss_" + issuer_id).innerHTML = "<span class=\"title\"><em>Удалить из списка мониторинга ЕГРЮЛ</em><span class=\"monitor_icon_block\" onclick=\"delIssuer('" + issuer_id + "',1,true);\"><span class=\"icon-minus-squared-alt\"></span><span class=\"icon-binoculars\"></span></span></span>";

                    }
                }
                if (data < 0) {
                    showwin("critical", "Превышено допустимое количество <br/>компаний в списке мониторинга ЕГРЮЛ", 2000);
                }
            }
        )

    }

    window.delIssuer = function (id, t, issearch) {
        win_confirm('Удаление организации из списка мониторинга.', 'Вы действительно хотите удалить эту запись?', function (result) {
            if (result) {
                var service_url = t == 0 ? "/MonitorOperations/RemoveFromMess" : "/MonitorOperations/RemoveFromEgrul";
                $.post(service_url, { "o": 9 + t, "id": id }, function (data) {
                    if (!issearch) {
                        if (t == 0) {
                            load_mess_subscribe_data();
                        }
                        if (t == 1) {
                            load_egrul_subscribe_data();
                        }
                    }
                    if (getObj("tdiss_" + id)) {
                        getObj("tdiss_" + id).innerHTML = "<span class=\"title\"><em>Добавить в список мониторинга ЕГРЮЛ</em><span class=\"monitor_icon_block\" onclick=\"doAddEGRUL('" + id + "');\"><span class=\"icon-plus-squared-alt\"></span><span class=\"icon-binoculars\"></span></span></span>";
                    }

                })
            }
        });
    }

    $().ready(function () {
        search_init();


        $('#btn_search').click(function (event) {
            doSearch();
        });

        $('#btn_clear').click(function (event) {
            doClear();
        })

        $('#btn_save_template').click(function (event) {
            if (roles_object.canSearch) {
                save_template_dialog(event);
            } else {
                no_rights();
            }
        })


    });

})();



