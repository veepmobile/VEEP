(function () {
    var searching = 0;
    //Функция основного поиска
    var excel_menu_open = false;
    var group_menu_open = false;
    $('body').on('click', function (e) {
        _hide_command_menu(e);
        hideClock();
    });


    window.doSearchUA = function (pg) {
        if (!roles_object.canSearch) {
            no_rights();
            return;
        }

        if (searching == 1) {
            doStop();
        } else {
            if (checkCanSearchUA()) {
                searching = 1;
                pg = (isNaN(String(pg))) ? 1 : pg;
                showClock();
                $("#btn_search").val("Остановить");
                mainSearcherUA(pg, '');
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        }
    };
    var _hide_menu_auto = function (e) {
        if (country_chooser_open) {
            var $menu = $('#country_selector_menu');
            if (!$menu.is(e.target) && $menu.has(e.target).length === 0 && !($('#country_selector_btn').is(e.target) || $('.country_chooser').is(e.target))) {
                $('#country_selector_menu').hide();
                country_chooser_open = false;
            }
        }
    };

    


    var _hide_command_menu = function (e) {

      
        if (excel_menu_open) {
            $('.sub_command_menu').remove();
            excel_menu_open = false;
        }
    }

    var generate_sub_excelmenu = function (e) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным компаниям").click(function () { doSave2XLS(); }).appendTo($ul);
        $("<li>").text("Экспорт по первым 10 000 компаниям").click(function () { doExport10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
    }
    var doExport10000 = function () {
        showClock();
        mainSearcherUA(-2000, '');
    }
    var doSave2XLS = function () {
        var issuers = "";

        if ($(".res_item").find('input:checkbox:checked').length > 0) {
            $(".res_item").find("input:checkbox:checked").each(function (i) {
                if (String(this.value).length > 0) {
                    if (String(this.value).indexOf("_") >= 0) {
                        issuers += "'" + String(this.value).substring(0, String(this.value).indexOf("_")) + "',";
                    } else {
                        issuers += "'" + String(this.value) + "',";
                    }
                }
            });
            issuers = issuers.substring(0, issuers.length - 1);
            var url = '/DBSearchUA/CompaniesGetExcel?issuers=' + issuers;
            window.open(url, '_blank');
           

        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
        }

    };


    window.mainSearcherUA = function (pg) {
        var _okfs = ($("#okfs_list").val() ? $("#okfs_list").val() : "");
        var rcount = $("#countVal").attr("val");
        var top1000 = ((pg == -2000) ? "2" : "0");
        var prm = {
            "company" : $("#comp_ua").val(),     //наименование
            "strict"  : 0, //строгий поиск=1
            "phone" :   $("#phone").val(),    //телефон
            "address" : $("#addr").val(),     //адрес
            "ruler" :   $("#ruler").val(),       //руководитель
            "constitutor" : "", //учредитель
            "regions" : $("#reg_val").val(),    //код региона
            "is_okato" : reg_type - 42, //коатуу или просто область 1 - коатуу( окато ), 0 - регистрация в налоговой(area)
            "reg_excl"  : (String($("#reg_excl").val()).length==0)? 0:$("#reg_excl").val(), //-1 - кроме региона
            "industry"  : $("#ind_val").val(),//квэд - отрасль
            "is_okonh" : 0, //по коду оконх
            "ind_excl"  : (String($("#ind_excl").val()).length==0) ? 0:$("#ind_excl").val(),//исключить выбранные кведы -1 - кроме отрасли
            "ind_main": ind_type - 44, //только основной квед -1 - оквэд основной
            "okopf"     : $("#okopf_val").val(),//копф=опф
            "okopf_excl": (String($("#okopf_excl").val()).length == 0) ? 0 : $("#okopf_excl").val(),//исключить выбранные копф  -1 - кроме опф
            "okfs": _okfs+"",    //окфс - форма собственности
            "okfs_excl" : (String($("#okfs_excl").val()).length==0)? 0:$("#okfs_excl").val(),  //-1 - кроме окфс
            "trades" :  "",      //листинг: РТС + ММВБ + РТС Board
            "gaap" : 0,           //1 - Отчетность IAS-GAAP
            "bankrupt" : 0,     //1 - в реестре недобросовестных поставщиков
            "kod" : "",     //код (ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН)
            "dbeg"      : $("#dfrom").val(),//дата регистрации с
            "dend"      : $("#dto").val(),//дата регистрации по
            "group_id" : 0,       //id группы, 0 - все предприятия
            "page_no" : pg, //номер страницы
            "rcount": rcount,   //количество записей на странице
            "user_id" : user_id, //user_id
            "top1000": top1000,
            "group_name": "",
            "fas" : "",  //лидеры рынка - код региона
            "fas_excl": 0, //1 - кроме региона
            "rgstr" : 0,    //1 - в реестре росстата
            "is_emitent" : false
        }

       
        //ключевые слова
        var key_name = ClearText(String($("#comp_ua").val()));
        var key_ruler = ClearText(String($("#ruler").val()));

        _toggle_extented_search(true);

        if (pg == -2000) {
            hideClock();
            var form = document.createElement("form");
            form.action = "/DBSearchUa/CompaniesDoSearchExcel/";
            form.method = "POST";
            element = document.createElement("input");
            element.type = "hidden";
            element.name = "so_string";
            element.value = JSON.stringify(prm);
            form.appendChild(element);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
           
        }
        else {
            //Новый поиск
            $.post("/DBSearchUa/CompaniesDoSearch", prm, function (data) {
                var re = new RegExp("icon_error", "ig")
                if (pg > 0) { //if(String(re_group.exec(data))=="null"){ 
                    //$("#search_result").html(data);
                    GenerateResult(pg, rcount, data, roles_object, key_name, key_ruler, user_id);
                    //check_monitor();
                    hideClock();
                    if (String(re.exec(data)) == "null") {
                        if ($("#search_table").css("display") == "block" || $("#search_table").css("display") == "table") {
                            $('#hide_form').show();
                            toggle_form();
                        }
                    }
                }
                /*
                if (pg == -1000) {
                    // Добавление 10000 в группу
                    var ret = SaveResultGroup(data, group_name);
                    var amsg = String(ret).split("_");
                    showwin((String(amsg[0]) == "0") ? 'info' : 'critical', amsg[1], 0);
                }*/
                hideClock();
                $("#btn_search").val("Найти");
                searching = 0;
                location.href = "#top";
            },
    "json");
        }
    };


    var GenerateResult = function (page, rcount, data, ro, key_name, key_ruler, user_id) {
        $("#search_result").html('').show();

        var total = data.total;
        var total_found = data.total_found;
        var page_count = total / rcount;

        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total && total > 0) {


            var total_text;
            total_text = "Всего найдено: " + total_found;
            if (total_found > 10000) {
                total_text += " (Выводятся первые 10000 предприятий)";
            }

            $("#search_result").append("<span class=\"total_count\">" + total_text + "</span>");

            var res_arr = data["results"];
            var $res_block = $('<div>').addClass("res_block");


            if (ro.canExport) {
                var $export_block = $('<div>').addClass("export_block");

                var $checkall_block = $('<div>').addClass("checkall_block");
                $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");
                $export_block.append($checkall_block);

                var $export_command_block = $('<div>').addClass("export_command_block");

                var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"title\"><em>Экспорт в Excel</em><span class=\"icon-file-excel icon\"></span></span></div>");
                $export_command_block.append($add_excel_btn);
                $add_excel_btn.click(function (e) {
                    generate_sub_excelmenu(e);
                });

                $export_block.append($export_command_block);
                $res_block.append($export_block);
            }

            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var $res_item = $('<div>').addClass("res_item"); 

                //var emitent = res_arr[i].isemitent;

                if (ro.canExport) {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + res_arr[i].edrpou + "\"></input>");
                    $res_item.append($check_block);
                }

                var $info_block = $('<div>').addClass("info_block");
                var comp_name = (res_arr[i].shortname.length > 2) ? res_arr[i].shortname : res_arr[i].name;
                $info_block.append('<a class="comp_title" href="/ua/' + res_arr[i].edrpou + '" target="_blank">' + comp_name + '</a>');

                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";
                //address, region, area, koatuu_name
                if (res_arr[i].address!="") {
                    comp_info_text += "<p>" + res_arr[i].address + "</p>";
                } else if (res_arr[i].region!="" ) {
                    comp_info_text += "<p>" + res_arr[i].region + "</p>";
                } else if (res_arr[i].area !="" ) {
                    comp_info_text += "<p>" + res_arr[i].area + "</p>";
                } else if (res_arr[i].koatuu_name != "") {
                    comp_info_text += "<p>" + res_arr[i].koatuu_name + "</p>";
                }

                if (res_arr[i].rulername != "") {
                    comp_info_text += "<p>" + (res_arr[i].rulertitle.length > 0  ? res_arr[i].rulertitle+": " : "Руководитель: ") + res_arr[i].rulername + "</p>";
                }

                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                var $code_block = $('<div>').addClass("code_block");
                comp_info_text = "";
                comp_info_text += "<p><span style=\"width:80px; display: inline-block;\">ЕДРПОУ:</span>" + res_arr[i].edrpou + "</p>";
                $code_block.html(comp_info_text);

//
//                var $command_block = $('<div>').addClass("command_block");
//                if (ro.canMonitor && res_arr[i].uniq == 1 && res_arr[i].ogrn.length == 13) {
//                    $command_block.append("<div class=\"monitor_block\" id=\"tdiss_" + res_arr[i].issuer_id + "\" issuer=\"" + res_arr[i].issuer_id + "\"><span class=\"title\" style=\"width:15px;\"></span></div>");
//                }

                $res_item.append($info_block);
                $res_item.append($code_block);
//                $res_item.append($command_block);
                $res_block.append($res_item);

            }
            $("#search_result").append($res_block);
            $("#search_result").append(_get_paging(page, page_count))

        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
        }

    }

    var checkCanSearchUA = function () {
        var ss = $("#comp_ua").val() + $("#phone").val() + $("#addr").val() + $("#ruler").val() +
            $("#reg_val").val() + $("#ind_val").val() + $("#okopf_val").val() + ($("#okfs_list").val() ? $("#okfs_list").val() : "") + //$("#okfs_val").val() +
            $("#dfrom").val() + $("#dto").val();
        return (ss == null || ss.length == 0) ? false : true;
    };

    window.ch_uareg_type = function (r_type) {
        $("#reg").val("");
        $("#reg_val").val("");
        $("#reg_excl").val("");
        $("#reg").attr({ "title": "" });
        reg_type = r_type.value;
        $("#reg").attr({ "title": eval("regtitles.i" + r_type.value) });
    };

    window.ch_uaind_type = function (i_type) {
        ind_type = i_type.value;
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
        if (i < page_count + 1) {
            html += '<td onclick="doSearch(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#comp').css("border-radius", "4px");
        }
    };

    window.dispissuer = function (text) {
        $("#comp_ua").val(text).css("border-radius", "4px");
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
        }
    };

    window.show_bones_ua = function (e) {
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

                $("#comp_ua").css("border-radius", "4px 4px 0 0");

                var boundes = $("#comp_ua").position();
                d.style.top = boundes.top + 94 + $("#comp_ua").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#comp_ua").width() + 23) + "px";
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
                $.post("/Modules/GetBonesUA", { "input": $("#comp_ua").val() },
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
})();

$().ready(function () {
    //    search_init();
    $('#comp_ua').keyup(function (event) {
        show_bones_ua(event);
    })
});

/*
document.all ? document.attachEvent('onclick', checkClick) : document.addEventListener('click', checkClick, false);

function ch_reg_type(r_type) {
    $("#reg").val("");
    $("#reg_val").val("");
    $("#reg_excl").val("");
    $("#reg").attr({ "title": "" });
    reg_type = r_type.value;
    $("#reg").attr({ "title": eval("regtitles.i" + r_type.value) });


}

function ch_ind_type(i_type) {
    ind_type = i_type.value;
}


//Функция основного поиска
function doSearch(pg){
    if(searching==1){
        doStop();
    }else{
        if(checkCanSearch()){
            searching=1;
            pg=(isNaN(String(pg)))?1:pg;
            showClock();
            $("#btn_search").val("Остановить");
            mainSearcher(pg,'');
        }else{
            showwin('warning','Надо выбрать критерий поиска!',2000);
        }
    }
}

function checkCanSearch(){
    var ss=$("#comp").val()+$("#ruler").val()+$("#reg_val").val()+$("#ind_val").val()+
            $("#okopf_val").val()+$("#okfs_val").val()+$("#codes").val()+
            $("#dfrom").val()+$("#dto").val();
    return (ss.length==0)? false:true;
}

function mainSearcher(pg){
    var params={"company"   : $("#comp").val(),//имя компании
        "strict"    : $("#sel_div_val").attr("sv"),//тип поиска имени
        "ruler"     : $("#ruler").val(),//руководитель
        "regions"   : $("#reg_val").val(),//регион
        "is_okato"  : (String($("#td_regtype").closest("td").find("input:radio:checked").val())=="0")?1:0,//коатуу или просто область
        "reg_excl"  : (String($("#reg_excl").val()).length==0)? 0:$("#reg_excl").val(),//исключить выбранные коатуу
        "industry"  : $("#ind_val").val(),//квэд 
        "ind_excl"  : (String($("#ind_excl").val()).length==0)? 0:$("#ind_excl").val(),//исключить выбранные кведы
        "ind_main"  : ($("#td_indtype").closest("td").find("input:radio:checked").val()-99==0)? "1" : "0",//только основной квед
        "okopf"     : $("#okopf_val").val(),//копф
        "okopf_excl": (String($("#okfs_excl").val()).length==0)? 0:$("#okfs_excl").val(),//исключить выбранные копф
        "okfs"      : $("#okfs_val").val(),//формы собственности
        "okfs_excl" : (String($("#okfs_excl").val()).length==0)? 0:$("#okfs_excl").val(), //исключить выбранные формы собственности
        "kod"       : $("#codes").val(),//код едрпоу
        "dbeg"      : $("#dfrom").val(),//дата регистрации с
        "dend"      : $("#dto").val(),//дата регистрации по
        "page_no"   : pg,
        "rcount"    :  $("#countVal").attr("val"),
        "user_id"   : <%=current_user_id%>,
        "top1000"   : ((pg==-2000)? "2":"0")
    }
    if (pg==-2000){
        DownLoadXLS(params);
    }else{           
        $.post("/dbsearch/dbsearchua/modules/dosearch.asp",params,function(data){
            var re=new RegExp("icon_error","ig")
            var re_group=new RegExp("#my_msg","ig")
            if(String(re_group.exec(data))=="null"){ 
                getObj("search_result").innerHTML=data;
                hideClock();
                if(String(re.exec(data))=="null"){
                    if($("#search_table").css("display")=="block" || $("#search_table").css("display")=="table"){ 
                        $('#hide_form').show();
                        toggle_form();
                    }
                }
            }else{
                var amsg=String(data).split("_");
                showwin((String(amsg[0])=="0")?'info':'critical',amsg[1],0);
            }    
            $("#btn_search").val("Найти");
            searching=0;
        },         
             "html");     
    }
}

var form_show=true;	
function toggle_form(){
    form_show=!form_show;
    if(form_show){
        $("#search_table").show();
        $("#btn_hide").attr("value","Скрыть форму");
        $("#zaglushka").hide()
    }else{
        $("#search_table").hide();
        $("#btn_hide").attr("value","Новый поиск");
        $("#zaglushka").hide()
    }
    
}

function doStop(){
    hideClock();
    searching=0;
    $.post("/iss/modules/operations.asp",{"action" : "6"});
    $("#btn_search").val("Найти");
}

function doClear(){
    $("#countVal").text(countParams[0]);
    $("#countVal").attr("val",countParams[0]);
    $("#sel_div_val").text(searchNamesParams[1]);
    $("#sel_div_val").attr("sv",1);

    
    $("input").each(function(i){
    
        if(this.type=="text"){
            this.value="";
            this.title="";
        

            
        }
        if(this.type=="hidden"){
            this.value=""
        }
        if(this.type=="checkbox"){
            if(this.id=="ind_main"){
                this.checked=true;
            }else{
                this.checked=false;
            }
        }
        if(this.type=="radio"){
            if(this.id=="ind_default"){
                this.checked=true;
                ch_ind_type(this);
            }
            if(this.id=="reg_default"){
                this.checked=true;
                ch_reg_type(this);
            }
            if(this.id=="c1"){
                this.checked=true;
            }
            
        }
       

    });
    $("select").each(function(i){
        this.options.selectedIndex=eval("default_selected." + this.id );
    });
}

function MoveTo(page,pcount){
    var sel_page = 0;
    sel_page=myPrompt('amover','Укажите номер страницы на которую вы хотите перейти. Число должно быть в диапазоне от 1 до ' + pcount, page,"doSearch",pcount);
}

function try2search(e){
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

}
//Конец функции основного поиска
    
//Подсказка по названию компании
    
function show_bones(e) {
    var d;
    if (window.event) {
        e = window.event;
    }


    if ((e.keyCode == 40 || e.keyCode == 38) && getObj("dp_window")) {

        skip(e.keyCode);

    } else {
        st_selected = -1;
        if (!getObj("dp_window") && !(e.keyCode == 13 || e.keyCode == 27)) {
            d = document.createElement("div");
            d.id = "dp_window";

            d.className = "dp_window";
            d.style.height = "120px";

            var boundes = $("#comp").position();
            d.style.top = boundes.top + 4 + $("#comp").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#comp").width() + 1) + "px";
            d.style.display = "none";
            document.body.appendChild(d);
        } else {
            d = getObj("dp_window");
        }
        if ((e.keyCode == 13 || e.keyCode == 27) && getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
        }
        if (getObj("dp_window")) {
            $.post("/dbsearch/dbsearchua/modules/getBones.asp", { "sub": $("#comp").val() },
            function (data) {
                var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"dpw\" onkeydown=\"skip(event)\">"
                st_max = 0
                if (!data[0].name && getObj("dp_window")) {
                    document.body.removeChild(d);
                    st_selected = -1;
                } else {
                    for (var i = 0; i < data.length; i++) {
                        htm += "<tr><td id=\"tdhover" + i + "\" class=\"bones_pointer\" onclick=\"dispissuer('" + data[i].name + "')\" onmouseover=\"colorit(this);\" onmouseout=\"blankit(this);\">" + data[i].name + "</td><td class=\"bones_pointer\" style=\"text-align:right;width:150px;\">" + data[i].cnt + " совпадений</td></tr>";
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
}

function draw_selection(i) {
    if (getObj("tdhover" + i)) {
        var d = getObj("tdhover" + i)
        $("#dpw").find('td').each(function () {
            this.className = "bones_pointer";
        });
        d.className = "bones_pointer over_color";
        $("#comp").val($("#tdhover" + i).html());
    }

}
function skip(kod) {
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
}

function dispissuer(text){
    $("#comp").val(text);
    if(getObj("dp_window")){
        document.body.removeChild(getObj("dp_window"));   
    }
}

//конец подсказки по названию

   

// Выбор типа поиска

function show_divSelector() {
    var d;

    if (!getObj("divSelector")) {
        d = document.createElement("div");
        d.id = "divSelector";
        d.className = "divSelector";
        d.style.height = searchNamesParams.length * 18 + "px";

        var boundes = $("#select_div").position();
        d.style.top = boundes.top + 4 + $("#select_div").height() + "px";
        d.style.left = boundes.left + "px";
        d.style.width = ($("#select_div").width() + 6) + "px";
        document.body.appendChild(d);
    }
    else
    { document.body.removeChild(getObj("divSelector")); };
    if (getObj("divSelector")) {
        var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"seltbl\">"
        for (var i = 0; i < searchNamesParams.length; i++) {
            htm += "<tr><td id=\"tddiv" + i + "\" class='" + ((i - $("#sel_div_val").attr("sv") == 0) ? "bones_pointer over_color" : "bones_pointer") + "' onClick='selectNewSP(" + i + ");' onmouseover=\"colorit(this)\" onmouseout=\"blankit(this);\">"
            + searchNamesParams[i] + "</td></tr>";
        }
        htm += "</table>";
        d.innerHTML = htm;
        d.style.display = "block";
        $("#seltbl").one("mouseenter", function () {
            $('#tddiv' + $('#sel_div_val').attr('sv')).attr('className', 'bones_pointer');
        })
    }
}

function colorit(obj) {
    obj.className = "bones_pointer over_color";
}
function blankit(obj) {
    obj.className = "bones_pointer out_color";

}

function selectNewSP(i) {
    $("#sel_div_val").text(searchNamesParams[i]);
    $("#sel_div_val").attr("sv", i);
    document.body.removeChild(getObj("divSelector"));
}

function isChild(s, d) {
    while (s) {
        if (s == d)
            return true;
        s = s.parentNode;
    }
    return false;
}

function checkClick(e) {
    e ? evt = e : evt = event;
    CSE = evt.target ? evt.target : evt.srcElement;
    if (getObj("divSelector") && !(CSE.id == "sel_div_val" || CSE.id == "sel_div_btn" || CSE.id == "selTd"))
        if (!isChild(CSE, getObj("divSelector")))
            document.body.removeChild(getObj("divSelector"));
    if (getObj("groupSelector") && !isChild(CSE, getObj("td3")))
        if (!isChild(CSE, getObj("groupSelector")))
            document.body.removeChild(getObj("groupSelector"));
    if (getObj("countSelector") && !isChild(CSE, getObj("td4")))
        if (!isChild(CSE, getObj("countSelector")))
            document.body.removeChild(getObj("countSelector"));
}
// конец выбора типа поиска

var tree_active;
var tree_table;
var src;
function show_tree_selector(e, sr, is_tree, mult) {

    src = (sr == 99) ? 1 : sr;
    if (src == 3) {
        is_tree = 0;
        src = 0;
    }
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
        bounds = $("#" + tree_active).position();
        d.style.top = bounds.top + 4 + $("#" + tree_active).height() + "px";
        if (scw >= bounds.left + 642) {
            d.style.left = bounds.left + 5 + "px";
        } else {
            d.style.left = bounds.left + $("#" + tree_active).width() - 638 + "px";
        }
        d.frameBorder = "0"
        if (is_tree == 1) {
            d.src = "/dbsearch/dbsearchua/modules/tree/tree_selector.asp?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val();

        } else {
            tree_table += 3;
            d.src = "/dbsearch/dbsearchua/modules/selector.asp?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val() + "&mult=" + mult;
        }
        getObj("td_" + tree_active).appendChild(d);
        $("#" + tree_active + "_window").click(function (event) {
            event.stopPropagation();

        })
    }
}

function Write_TS(retval, is_excl) {
    if (retval.length > 0) {
        $.post("/dbsearch/dbsearchua/modules/tree/getResultString.asp", { "src": tree_table, "id": retval },
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
}
//Конец выбора значений из справочников



$(document).ready(function(){
    $("#comp").attr({"autocomplete":"off"});
    
    document.onreadystatechange=hideClock;
    getObj("xls_frame").onreadystatechange=hideClock;

    $('body').on('click',function(){hidepopups();});


    $("#xls_frame").ready(function(){
        hideClock();
    })

   
    <%if(String(user_id)>"0"){%>    
     //Выбор диапазона даты
        var dates = $("#dfrom, #dto").datepicker({
            defaultDate: "-1y",
            changeMonth: true,
            showButtonPanel: true,
            changeYear: true,
            yearRange: "1990:" + now.getFullYear(),
            onSelect: function( selectedDate ) {
                var option = this.id == "dfrom" ? "minDate" : "maxDate",
                instance = $( this ).data( "datepicker" );
                date = $.datepicker.parseDate(
                instance.settings.dateFormat ||
                $.datepicker._defaults.dateFormat,
                selectedDate, instance.settings );
                dates.not( this ).datepicker( "option", option, date );
            }
        });
        // Конец выбора диапазона даты
        <%}%>
        });
var now=new Date();
   

//Выбор количества выводимых записей
function show_countSelector(){
    var d;

    
    if(!getObj("countSelector")){
        d=document.createElement("div");
        d.id="countSelector";
        d.className="countSelector";
        
        
        var boundes=$("#countSelect").position();
        d.style.top=boundes.top + 4 + $("#countSelect").height() + "px";
        d.style.left=boundes.left + "px";
        d.style.width=($("#countSelect").width() +6)+ "px";
        document.body.appendChild(d);
    }
    else
    {document.body.removeChild(getObj("countSelector"));}
    if(getObj("countSelector")){
        var htm="<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">"
        for(var i=0;i<countParams.length;i++){
            htm+="<tr class='selec_option'  onClick='selectNewCount("
             + i+");'><td id='tdgroup"+i+"' id='tdgroup"+i+"' class='" + ((countParams[i]-$("#countVal").attr("val")==0)? "bones_pointer over_color" : "bones_pointer") + "' onmouseover=\"colorit(this)\" onmouseout=\"blankit(this)\">"+countParams[i]+"</td></tr>";
        }
        htm+="</table>";
        d.innerHTML=htm;
        d.style.display="block";
        $("#cnttbl").one("mouseenter",function(){
            $('#tdgroup' + $('#countVal').attr('val')).attr('className','bones_pointer');
        })

    }        
}

function selectNewCount(i){ 
    $("#countVal").text(countParams[i]);
    $("#countVal").attr("val",countParams[i]);
    document.body.removeChild(getObj("countSelector"));
}

//Конец выбора кол-ва записей

//Выгрузка Excel
function DownLoadXLS(params){

    var form = document.createElement("form");
    form.action="/dbsearch/dbsearchua/modules/dosearch1252.asp";
    form.method="POST";
    if ( !document.addEventListener ) {
        showClock();
        form.target="xls_frame"
    }else{
        form.target="blank"
    }
      
    form.style.display="none"
    var element = null;
    for (var propName in params) {
        element = document.createElement("input");
        element.type="text"
        element.name=propName
        element.value=params[propName]
        form.appendChild(element);
    }
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
      
}*/
//Конец выгрузки