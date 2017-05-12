document.all?document.attachEvent('onclick',checkClick):document.addEventListener('click',checkClick,false);
var st_selected=-1;  
var bones_pressed=false;
var st_max=0;  
var reg_type=0;
var ind_type=1;
var default_selected = {"strict":"1","user_groups":0,"rc":0,"ind_default":1};
var searchNamesParams=["по части слова","по началу слова","по целому слову","строгий поиск"];
var countParams=[30,50,100]
var acodes = new Array();
acodes[0]={};
acodes[1]={"code": 1, "txt":  "По компаниям, чьи акции торгуются на Фондовой бирже РТС, совпадает с кодом/биржевым тикером РТС"};
acodes[2]={"code": 2, "txt":  "Идентификационный номер налогоплательщика (для юридических лиц - 10 арабских цифр)"};
acodes[3]={"code": 3, "txt":  "Общероссийский классификатор предприятий и организаций (для юридических лиц - 8 арабских цифр) "};
acodes[4]={"code": 4, "txt":  "Основной государственный регистрационный номер (для юридических лиц - 13 арабских цифр) "};
acodes[5]={"code": 5, "txt":  "Код эмитента, присвоенный Федеральной службой по финансовым рынкам (5 арабских цифр, дефис, латинская буква)"};
var titles = {"okopf" : "в соответствии с ОКОПФ - Общероссийский классификатор организационно-правовых форм","okfs"  : "в соответствии с ОКФС - Общероссийский классификатор форм собственности"};
var indtitles={"i1" : "Общероссийский классификатор видов экономической деятельности", "i99" : "Общероссийский классификатор видов экономической деятельности", "i2" : 'Общесоюзный классификатор [Отрасли народного хозяйства]'}
var regtitles={"i0" : "Общероссийский классификатор объектов административно-территориального деления",  "i5" : "по регистрации в налоговых органах"}
var searching=0

function mainSearcher(pg,group_name){
    bones_pressed=false;
    var codes = ($("#codes").val() != "ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН") ? $("#codes").val() : '';
    var group_id = $("#gropVal").attr("val");
    var rcount = $("#countVal").attr("val");
    var top1000 = ((group_name.length>0 && pg==-1000)?"1":(pg==-2000)? "2":"0");
    var params={"company"       : $("#comp").val(),
        "strict"        : ((getObj("rsv").checked)?3:0), //$("#sel_div_val").attr("sv"),//$("#strict").val(),
        "phone"         : $("#phone").val(),
        "address"       : $("#addr").val(),
        "ruler"         : $("#ruler").val(),
        "constitutor"   : $("#constitutor").val(),
        "regions"       : $("#reg_val").val(),
        "is_okato"      : (String($("#td_regtype").closest("td").find("input:radio:checked").val())=="0")?1:0,
        "reg_excl"      : (String($("#reg_excl").val()).length==0)? 0:$("#reg_excl").val(),
        "industry"      : $("#ind_val").val(),
        "is_okonh"      : ($("#td_indtype").closest("td").find("input:radio:checked").val()-2==0)? "1":"0",
        "ind_excl"      : (String($("#ind_excl").val()).length==0)? 0:$("#ind_excl").val(),
        "ind_main"      : ($("#td_indtype").closest("td").find("input:radio:checked").val()-99==0)? "1" : "0",
        "okopf"         : $("#okopf_val").val(),
        "okopf_excl"    : (String($("#okopf_excl").val()).length==0)? 0:$("#okopf_excl").val(), 
        "okfs"          : $("#okfs_val").val(),
        "okfs_excl"     : (String($("#okfs_excl").val()).length==0)? 0:$("#okfs_excl").val(), 
        "rfi"           :  -1, //$("#rfi_val").val(), //не используется
        "rfi_excl"      : 0, //не используется
        "status"        : "", //не используется
        "status_excl"   : 0,  //не используется
        "trades"        : '0' + /*((getObj("l1").checked)?"1":"0")+*/ ((getObj("l2").checked)?"1":"0")+ ((getObj("l3").checked)?"1":"0"),
        "gaap"          : ((getObj("gaap").checked)?1:0),
        "bankrupt"      : ((getObj("bankr").checked)?1:0),
        "kod"           : codes, //$("#codes").val(),
        "kod_type"      : 0,//$("#code_selector").closest("td").find("input:radio:checked").val(),//не используется
        "dbeg"          : $("#dfrom").val(),
        "dend"          : $("#dto").val(),
        "group_id"      : group_id,//$("#user_groups").val(),
        "page_no"       : pg,
        "rcount"        : rcount,//$("#rc").val(),
        "user_id"       : user_id, //<%=current_user_id%>,
        "top1000"       : top1000,
        "group_name"    : ((group_name.length>0 && pg==-1000)? group_name:""),
        "fas"           : $("#fas_val").val(),
        "fas_excl"      : (String($("#fas_excl").val()).length==0)? 0:$("#fas_excl").val(),
        "rgstr"         : ((getObj("rgstr").checked)?1:0)
    }
    //ключевые слова
    var key_name = ClearText(String($("#comp").val())).split(";");
    var key_ruler = ClearText(String($("#ruler").val())).split(";");
    var key_constr = ClearText(String($("#constitutor").val())).split(";");

    if (pg == -2000)
    {           
        $.post("/DBSearchRu/CompaniesDoSearch", params, function (data) {
            if(data){ DownLoadXLS1000(data);}
            hideClock();
            searching=0;
        }); 
    } 
    else {           

        //Новый поиск
        $.post("/DBSearchRu/CompaniesDoSearch", params, function (data) {
            var re=new RegExp("icon_error","ig")
            if (pg > 0) { //if(String(re_group.exec(data))=="null"){ 
                //getObj("search_result").innerHTML=data;
                GenerateResult(pg, rcount, data, roles_object, key_name, key_ruler, key_constr, group_id, user_id);
                check_monitor();
                hideClock();
                if(String(re.exec(data))=="null"){
                    if($("#search_table").css("display")=="block" || $("#search_table").css("display")=="table"){
                        $('#hide_form').show();
                        toggle_form();
                    }
                }
            }
            if (pg == -1000)
            {
                // Добавление 10000 в группу
                var ret = SaveResultGroup(data,group_name);
                var amsg=String(ret).split("_");
                showwin((String(amsg[0])=="0")?'info':'critical',amsg[1],0);
            }    


            hideClock();
            $("#btn_search").val("Найти");
            searching=0;
            location.href = "#top";
        },
"json");
    }

   
}

function GenerateResult(page, rcount, data, ro, key_name, key_ruler, key_constr, group_id, user_id) {

    var total = data.total;
    var total_found = data.total_found;
    var page_count = total/rcount;
    if(page_count%1>0) {
        page_count = (page_count - (page_count%1)) + 1;
    }
    if (total) {
        if (total==0){
            var error = "<img src=\"/images/icon_error.gif\" width=\"16\" height=\"16\" border=\"0\"/><font class=\"error\">Нет данных соответствующих заданному условию</font>";
            error += "<div style=\"text-align:center; padding-top: 100px;\"><a href=\"javascript:showSeekWin()\">Если Вы не нашли интересующую Вас компанию, попробуйте поискать в ЕГРЮЛ (сформировать выписку)</a></div>";
            $("#search_result").html(error);
        }
        else
        {
            var res_arr = data["results"];
            var result = "<img src=\"/images/mnu_bullet_10.gif\" width=\"14\" height=\"11\" border=\"0\" align=\"absmiddle\"/>"
            if (group_id=="0") {
                result += "<font class=\"minicaption\">Всего найдено: " + total_found + " предприятий.</font>";
            }
            else
            {
                result += "<font class=\"minicaption\">Предприятий в группе: " + total_found + ".</font>";
            }
            if (total_found > 10000){
                result += "<font class=\"minicaption\"> Выводятся первые 10000 предприятий.</font>";
            }

            if (ro.canAddToGroup) {
                result += "<table style=\"width:100%;\"><tr><td colspan=\"4\" width=\"100%\" class=\"table_item_left\"><input type=\"button\" name=\"SaveSels\"  class=\"func_button group_sel\"  title=\"Добавить выбранные компании в Группу\" onclick=\"doSave2List(" + user_id + ");\" /><input type=\"button\" id=\"Save1000\" class=\"func_button group\"  title=\"Добавить первые 10 000 компаний в Группу\" onclick=\"doCreateGroup1000('Save1000');\" /></td>";
                    
                if (ro.canExport) {

                    result += "<td style=\"text-align:right;white-space:nowrap;\"><input type=\"button\" id=\"SaveXLS\" class=\"func_button excel_sel\"  title=\"Экспортировать контактную информацию по выбранным компаниям\" onclick=\"doSave2XLS(" + user_id + ");\" /><input type=\"button\" id=\"SaveXLS1000\" class=\"func_button excels\"  title=\"Экспортировать контактную информацию по первым 10 000 компаний\" onclick=\"doExport10000();\" /></td>";
                }
                result += "</tr></table>";
            }
            result += "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tbody id=\"tb_main\"><tr>";
            if (ro.canAddToGroup || ro.canExport) {
                result += "<td class=\"table_caption_left\" style=\"width:20px;\"><input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/></td><td class=\"table_caption\" style=\"width:15px;\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td>";
            }
            if (ro.canMonitor) {
                result += "<td class=\"table_caption\" style=\"width:15px;\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td>";
            }
            result += "<td class=\"table_caption\" style=\"width:40%;\">Наименование</td>";
            result += "<td class=\"table_caption\">Отрасль</td><td class=\"table_caption\" style=\"width:10%;\">Регион</td><td class=\"table_caption\" style=\"width:10%;\">ИНН</td>";
            result += "</tr><tr>";

            result += "<td class=\"table_shadow_left\" style=\"width:20px;\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td><td class=\"table_shadow_left\" style=\"width:15px;\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td>";
            if (ro.canMonitor) {

                result += "<td class=\"table_shadow_left\" style=\"width:15px;\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td><td class=\"table_shadow_left\"  style=\"width:30%;\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td><td class=\"table_shadow_left\" ><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td>";
            }

            result += "<td class=\"table_shadow_left\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td><td class=\"table_shadow_left\"><img src=\"/images/null.gif\" width=\"1\" height=\"1\" border=\"0\"/></td>";
            result += "</tr>";
                   
            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var history = ResultHistory(res_arr[i], key_name, key_ruler, key_constr);
                var emitent = res_arr[i].isemitent;
                result += "<tr class=\"drow\">";
                if (user_id > 0) { 
                    result += "<td class=\"table_item\" style=\"width:20px;\" align=\"center\">";
                    if (ro.canShowAll || (emitent == "1" && ro.canShowEmit)) {
                        result += "<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + res_arr[i].issuer_id + "_" + res_arr[i].type_id + "\"></input>";
                    }
                    result += "</td><td class=\"table_item\" align=\"center\" style=\"width:15px;\" align=\"center\">";
                    if(res_arr[i].del != '0') { 
                        result += "<span class=\"title_u\" style=\"width:15px;\"><em>Исключено из реестра Росстата " + res_arr[i].del + "</em><img src=\"/images/vskl.png?1\" style=\"vertical-align:middle;\" alt=\"Исключено из реестра Росстата " + res_arr[i].del + "\" </img></span>";
                    }
                    result += "</td>";
                    if (ro.canMonitor) {
 
                        if(res_arr[i].uniq == 1) {
                            result += "<td class=\"table_item\" id=\"tdiss_" + res_arr[i].issuer_id + "\" issuer=\"" + res_arr[i].issuer_id + "\" style=\"width:15px;\" align=\"center\">";
                            if(res_arr[i].ogrn.length != 13){

                                result += "<span class=\"title_u\" style=\"width:15px;\"><em>Недоступно для данной организации</em><img style=\"vertical-align:middle;\" src=\"/images/add_to_egrul_dis.png\" alt=\"Недоступно для данной организации\" onclick=\"void(0)\"></img></span>";
                            }
                            else
                            {
                                result += "<span class=\"title_u\" style=\"width:15px;\"></span>";
                            }
                            result +="</td>";
                        }
                        else
                        {
                            result += "<td class=\"table_item\" style=\"width:15px;\" align=\"center\"><span class=\"title_u\" style=\"width:20px;\"><em>Недоступно для данной организации</em><img style=\"vertical-align:middle;\" src=\"/images/add_to_egrul_dis.png\" alt=\"Недоступно для данной организации\" onclick=\"void(0)\"></img></span></td>";
                        }
                    }
                }

                result += "<td  class=\"table_item\" align=\"left\" style=\"width:30%;\">";
                if (res_arr[i].ticker.length > 0) {
                    if (!ro.canShowAll && !ro.canShowEmit && res_arr[i].information!=4) { 
                        result += "<a href=\"#\" onclick=\"united_login(5);\">" + ((res_arr[i].nm.length > 2) ? res_arr[i].nm : res_arr[i].name) + "</a>";
                    }
                    else
                    {
                        if (ro.canShowAll) {
                            result += "<a href=\"javascript:openIssuer('" + res_arr[i].ticker + "');\">" + ((res_arr[i].nm.length > 2) ? res_arr[i].nm : res_arr[i].name) + "</a>";
                        }
                        else 
                        {
                            if(emitent == "1" || res_arr[i].information==4) {
                                result += "<a href=\"javascript:openIssuer('" + res_arr[i].ticker + "');\">" + ((res_arr[i].nm.length > 2) ? res_arr[i].nm : res_arr[i].name) + "</a>";
                            }
                            else {
                                result += "<a href=\"javascript:united_login(3);\"><font color=\"#C0C0C0\">" + ((res_arr[i].nm.length > 2) ? res_arr[i].nm : res_arr[i].name) + "</font></a>";
                            }
                        }
                    }
                }
                else
                {
                    result += ((res_arr[i].nm.length > 2) ? res_arr[i].nm : res_arr[i].name);
                }
                result += history + "</td>";

                result += "</td><td class=\"table_item\">" + ((res_arr[i].okved.length > 0) ? res_arr[i].okved : "-") + "</td><td class=\"table_item\">" + ((res_arr[i].region.length > 0) ? res_arr[i].region : "-") + "</td><td class=\"table_item\">" + ((res_arr[i].inn.length > 0) ? res_arr[i].inn : "-") + "</td>";

                result += "</tr>";
            }
                    
            result += "</tbody></table><br/>";

            result += _get_paging(page,page_count) + "<br/>";

            if (user_id > 0) { 
                result += "<div style=\"text-align:center\"><a href=\"javascript:showSeekWin()\">Если Вы не нашли интересующую Вас компанию, попробуйте поискать в ЕГРЮЛ (сформировать выписку)</a></div>";
            }

            $("#search_result").html(result);
        }
    }
    else
    {
        getObj("search_result").innerHTML="<img src=\"/images/icon_error.gif\" width=\"16\" height=\"16\" border=\"0\"><font class=\"error\">Нет данных соответствующих заданному условию</font>";
    }
}



function SaveResultGroup(data,group_name) {
    if(data) {
        for (var j = 0, j_max = (extGroupList.length-1); j <= j_max; j++) {
            if (extGroupList[j][1]==group_name){
                return "1_Группа с таким именем уже есть";
            }
        } 
        var res_arr = data["results"];
        var issuers = "";
        for (var i = 0; i <= (res_arr.length-1); i++) {
            issuers += res_arr[i].issuer_id + "_" + res_arr[i].type_id + ","; 
        }
        doSaveToUserList(issuers, group_name);
        hideClock();
        return "0_В группе сохранено " + res_arr.length + " компаний"
    }
    else
    {
        return "1_Внутренняя ошибка";
    }

}
    
function ResultHistory(res_arr, key_name, key_ruler, key_constr) {
    var name = "";
    var ruler = "";
    var constr = "";
        
    if (key_name.length > 0 && key_name != "") {
        for (var i = 0; i < key_name.length; i++) {
            if(ClearText(res_arr.name).indexOf(key_name[i])<0 && ClearText(res_arr.nm).indexOf(key_name[i])<0){
                var names = res_arr.search_name.split('&');
                for (var j = 0; j < names.length; j++) {
                    if(ClearText(names[j]).indexOf(key_name[i])>=0 && key_name[i] != ""){
                        name += "<br/><s>" + names[j].replace("&","").replace("amp;","").replace(/\?/gi,", ") + "</s>";
                        break;
                    }
                }
            }
            if (name != "") { break;}
        }
    }

    ruler += "<br/>Руководитель: " + ((res_arr.ruler.length > 0) ? res_arr.ruler : "-");
    if (key_ruler.length > 0 && key_ruler != "") {
        for (var i = 0; i < key_ruler.length; i++) {
            if(ClearText(res_arr.ruler).indexOf(key_ruler[i])<0){
                var managers = res_arr.manager_history.split('?');
                for (var j = 0; j < managers.length; j++) {
                    if(ClearText(managers[j]).indexOf(key_ruler[i])>=0 && key_ruler[i] != ""){
                        ruler += "<br/><s>" + managers[j].replace("amp;","").replace(/\?/gi,", ") + "</s>";
                        break;
                    }
                }
            }
            if (ruler != "") { break;}
        }
    }

    if (key_constr.length > 0 && key_constr != "") {
        for (var i = 0; i < key_constr.length; i++) {
            var constrs = res_arr.const_list.split('?');
            for (var j = 0; j < constrs.length; j++) {
                if(ClearText(constrs[j]).indexOf(key_constr[i])>=0 && key_constr[i] != ""){
                    if(ClearText(res_arr.constr).indexOf(key_constr[i])<0){
                        constr += "<br/>Учредитель: <s>" + constrs[j].replace("|","").replace(/\?/gi,", ") + "</s>";
                    }
                    else {
                        constr += "<br/>Учредитель: " + constrs[j].replace("|","").replace(/\?/gi,", ");
                    }
                    break;
                }
            }
            if (constr != "") { break;}
        }
    }

    return name + ruler + constr;
}


function qSearch(fld,val){
    if(val.length>0){
        showClock()
        $("#" + fld).attr("value",val);
        doSearch(1);
    }
}
function fillFields(nm,kod){
    if(nm.length>0 || kod.length>0){
        showClock();
        $("#comp").attr("value",nm);
        $("#codes").attr("value",kod);
        doSearch(1);
    }
}
function checkGAAP(){
    getObj("gaap").checked=true;
    doSearch(1);
}
function goINN(inn){
    getObj("c2").checked=true;
    $("#codes").attr("value",inn);
    doSearch(1);
    
}

function doSave2XLS(uid) {
    var issuers = "";

    if ($("#tb_main").find('input:checkbox:checked').length > 0) {
        $("#tb_main").find("input:checkbox:checked").each(function (i) {
            if (String(this.value).length > 0) {
                if (String(this.value).indexOf("_") >= 0) {
                    issuers += "'" + String(this.value).substring(0, String(this.value).indexOf("_")) + "',";
                } else {
                    issuers += "'" + String(this.value) + "',";
                }
            }
        });
        issuers = issuers.substring(0, issuers.length - 1);
        $.post("/DBSearchRu/CompaniesGetExcel", { "issuers": issuers }, function (data) {
            if (data.filename) {
                DownLoadXLS1000(data.filename);
            } else if (data.error) {
                showwin('critical', '<p align=center>'+data.error+'</p>', 3000);
            }
        },"json");

    } else {
        showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
    }

}
    
function DownLoadXLS1000(filename){
    var form = document.createElement("form");
    form.action = "/DBSearchRu/GetFile";
    form.method="POST";
    if ( !document.addEventListener ) {
        showClock();
        form.target="xls_frame"
    }else{
        form.target="blank"
    }      
    form.style.display="none"
    form.appendChild(make_input("src",filename));
    form.appendChild(make_input("page", "dbsearchru/companies"));
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);      
}

function make_input(name, value) {
    var element = null;
    element = document.createElement("input");
    element.type = "text";
    element.name = name;
    element.value = value;
    return element;
}
    
function doSearch(pg){
    close_bones();
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

function doSearch1000(group_name){
    showClock();
    mainSearcher(-1000,group_name);
}

function checkCanSearch(){
    var ss=$("#comp").val()+$("#phone").val()+$("#addr").val()+$("#ruler").val()+
        $("#reg_val").val()+$("#ind_val").val()+$("#okopf_val").val()+$("#okfs_val").val()+
        $("#status_val").val()+
        ((getObj("l2").checked)?"1":"")+ ((getObj("l3").checked)?"1":"")+
        ((getObj("gaap").checked)?"1":"")+((getObj("bankr").checked)?"1":"")+$("#codes").val()+
        $("#dfrom").val()+$("#dto").val()+((String($("#gropVal").attr("val"))=="0")?"":"1");
    return (ss.length==0)? false:true;
}
function doStop(){
    hideClock();
    searching=0;
    $.post("/iss/modules/operations.asp",{"action" : "6"});
    $("#btn_search").val("Найти");
    window.stop();
}
function doSetCheckedAll(chb){
    $("#tb_main").find("input:checkbox").each(function(i){
        this.checked=chb.checked;
    });
    
}
function MoveTo(page,pcount){
    var sel_page = 0;
    sel_page=myPrompt('amover','Укажите номер страницы на которую вы хотите перейти. Число должно быть в диапазоне от 1 до ' + pcount, page,"doSearch",pcount);
}

function doClear(){
    $("#countVal").text(countParams[0]);
    $("#countVal").attr("val",countParams[0]);
    $("#gropVal").text(restrictGropName(extGroupList[0].txt));
    $("#gropVal").attr("val",extGroupList[0].code);

    
    $("input").each(function(i){
    
        if(this.type=="text"){
            this.value="";
            this.title="";
            if(this.id=="codes"){
                this.value="ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН";
                $(this).removeClass('system_form').addClass('system_form_grey');
                codes_show = true;
            }
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
            if(this.id=="rsv"){
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
            if(this.id=="c2"){
                this.checked=true;
            }
            
        }
       

    });
    $("select").each(function(i){
        this.options.selectedIndex=eval("default_selected." + this.id );
    });
        
}
function Write_TS(retval,is_excl){
    if(retval.length>0){
        $.post("/Tree/GetResultString", { "src": tree_table, "id": retval },
        function(data){
            $("#" + tree_active).val(((is_excl==-1)? "Искл.: " : "") + data); 
            $("#" + tree_active).attr({"title": ((is_excl==-1)? "Исключая: " : "") + data.replace(",",",\n")});      
            $("#" + tree_active + "_excl").val(is_excl);
            $("#" + tree_active + "_val").val(retval);
        })
    }else{
        $("#" + tree_active).val("");
        $("#" + tree_active).attr({"title": eval("titles." + tree_active)});
        $("#" + tree_active + "_excl").val(0);
        $("#" + tree_active + "_val").val("");
        
    }  
    hidepopups();
}
function ch_ind_type(i_type){
    if((ind_type-2==0 && (i_type.value-1==0 || i_type.value-99==0)) || (i_type.value-2==0 && (ind_type-1==0 || ind_type-99==0))){
        $("#ind").val("");
        $("#ind_val").val("");
        $("#ind_excl").val("");
        $("#ind").attr({"title":""});
        $("#ind").attr({"title" : eval("indtitles.i" + i_type.value)});
        
    }
    ind_type=i_type.value;
}

function ch_reg_type(r_type){
    $("#reg").val("");
    $("#reg_val").val("");
    $("#reg_excl").val("");
    $("#reg").attr({"title":""});
    reg_type=r_type.value;
    $("#reg").attr({"title" : eval("regtitles.i" + r_type.value)});
   
    
}
function close_bones(){
    if(getObj("dp_window")){
        document.body.removeChild(getObj("dp_window"));   
        $("html").unbind();
    }
}
function show_bones(e){
    var d;
    $("#search_table").unbind();
    if (window.event){
        e = window.event;
    }
    if (e.stopPropagation) {
        e.stopPropagation();
    }else{
        e.cancelBubble=true;
    }    
	
    if((e.keyCode==40 || e.keyCode==38) && getObj("dp_window")){
        skip(e.keyCode);
    }else{
        
        if(!getObj("dp_window") && !(e.keyCode==13 || e.keyCode==27)){
            d=document.createElement("div");
            d.id="dp_window";
            st_selected=-1;
            d.className="dp_window";
            d.style.height="120px";
            
            var boundes=$("#comp").position();
            d.style.top=boundes.top + 4 + $("#comp").height() + "px";
            d.style.left=boundes.left + "px";
            d.style.width=($("#comp").width() +1)+ "px";
            d.style.display="none";
            document.body.appendChild(d);
        }else{
            d=getObj("dp_window");
        }
        if((e.keyCode==13 || e.keyCode==27) && getObj("dp_window")){
            document.body.removeChild(getObj("dp_window"));   
            if(e.keyCode==13){
                bones_pressed=true;
                if(st_selected>=0){
                    $('input:checkbox[name=rsv]').attr('checked', true);

                }    
                try2search(e);
                st_selected=-1;
            }
        }
        $("html").click(close_bones);
        if(getObj("dp_window")){
            $.post("/Modules/GetBones",{"input":$("#comp").val()},
                function(data){
                    var htm="<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"dpw\" onkeydown=\"skip(event)\">"
                    st_max=0
                    if (!data[0].name && getObj("dp_window")){
                        document.body.removeChild(d);   
                        st_selected=-1;                 
                    }else{
                        for(var i=0; i<data.length; i++){
                            htm += "<tr><td id=\"tdhover" + i +  "\" class=\"bones_pointer\" onclick=\"dispissuer('" + data[i].name + "')\" onmouseover=\"colorit(this);\" onmouseout=\"blankit(this);\">" + data[i].name + "</td><td class=\"bones_pointer\" style=\"text-align:right;width:150px;\">" + data[i].cnt + " совпадений</td></tr>";
                            st_max++;
                        }    
                        htm+="</table>"
                        d.innerHTML=htm;
                        d.style.display="block";
                    }
                },"json"
            );
        }
    } 
    $("search_table").bind("keypress",function(e){try2search(e)});
}

function draw_selection(i){
    if (getObj("tdhover" + i)){
        var d=getObj("tdhover" + i)
        $("#dpw").find('td').each(function(){
            this.className = "bones_pointer";
        }); 
        d.className="bones_pointer over_color";
        $("#comp").val($("#tdhover" + i).text());
    }
    
}
function skip(kod){
    if(kod==40 && st_selected<st_max-1){
        st_selected++;
        draw_selection(st_selected);
    } 
    if(kod==38){
        if(st_selected>0){
            st_selected--;
            draw_selection(st_selected);
        }else{
            $("#dpw").find('td').each(function(){
                this.className = "bones_pointer";
            }); 
            st_selected=-1
            getObj("comp").focus();
        }
    }
    
    
}

// Выбор типа поиска

function show_divSelector(){
    var d;

    if(!getObj("divSelector")){
        d=document.createElement("div");
        d.id="divSelector";
        d.className="divSelector";
        d.style.height=searchNamesParams.length*18+"px";
        
        var boundes=$("#select_div").position();
        d.style.top=boundes.top + 4 + $("#select_div").height() + "px";
        d.style.left=boundes.left + "px";
        d.style.width=($("#select_div").width() +6)+ "px";
        document.body.appendChild(d);
    }
    else
    {document.body.removeChild(getObj("divSelector"));};
    if(getObj("divSelector")){
        var htm="<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"seltbl\">"
        for(var i=0;i<searchNamesParams.length;i++){
            htm+="<tr><td id=\"tddiv" + i + "\" class='" + ((i-$("#sel_div_val").attr("sv")==0)? "bones_pointer over_color" : "bones_pointer") + "' onClick='selectNewSP("+i+");' onmouseover=\"colorit(this)\" onmouseout=\"blankit(this);\">"
                +searchNamesParams[i]+"</td></tr>";
        }
        htm+="</table>";
        d.innerHTML=htm;
        d.style.display="block";
        $("#seltbl").one("mouseenter",function(){
            $('#tddiv' + $('#sel_div_val').attr('sv')).attr('className','bones_pointer');
        })
    }        
}

function colorit(obj){
    obj.className="bones_pointer over_color";
}
function blankit(obj){
    obj.className="bones_pointer out_color";
    
}

function selectNewSP(i){
    $("#sel_div_val").text(searchNamesParams[i]);
    $("#sel_div_val").attr("sv",i);
    document.body.removeChild(getObj("divSelector"));
}

function isChild(s,d) {
    while(s) {
        if (s==d) 
            return true;
        s=s.parentNode;
    }
    return false;
}

function checkClick(e) {
    e?evt=e:evt=event;
    CSE=evt.target?evt.target:evt.srcElement;
    if (getObj("divSelector") && !(CSE.id=="sel_div_val" || CSE.id=="sel_div_btn" || CSE.id=="selTd"))
        if (!isChild(CSE,getObj("divSelector")))
            document.body.removeChild(getObj("divSelector"));
    if (getObj("groupSelector") && !isChild(CSE,getObj("td3")))
        if (!isChild(CSE,getObj("groupSelector")))
            document.body.removeChild(getObj("groupSelector"));
    if (getObj("countSelector") && !isChild(CSE,getObj("td4")))
        if (!isChild(CSE,getObj("countSelector")))
            document.body.removeChild(getObj("countSelector"));
}
// конец выбора типа поиска
//Выбор группы
function show_groupSelector(){
    var d;

    
    if(!getObj("groupSelector")){
        d=document.createElement("div");
        d.id="groupSelector";
        d.className="groupSelector";
        
        
        var boundes=$("#groupSelect").position();
        d.style.top=boundes.top + 4 + $("#groupSelect").height() + "px";
        d.style.left=boundes.left + "px";
        d.style.width=($("#groupSelect").width() +6)+ "px";
        document.body.appendChild(d);
    }
    else
    {document.body.removeChild(getObj("groupSelector"));};
    if(getObj("groupSelector")){
        var selected_i=0;
        var htm="<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"grouptbl\" >"
        for(var i=0;i<extGroupList.length;i++){
            if(extGroupList[i].code-$("#gropVal").attr("val")==0){
                selected_i=i;
            }
            htm+="<tr class='bones_pointer' onmouseover=\"colorit(getObj('tdgroup"+i+"'))\" onmouseout=\"blankit(getObj('tdgroup"+i+"'))\" onClick='selectNewGroup("
             + i+");'><td id='tdgroup"+i+"' class='" + ((extGroupList[i].code-$("#gropVal").attr("val")==0)? "bones_pointer over_color" : "bones_pointer") + "' >"+extGroupList[i].txt+"</td></td><td class='bones_pointer1' style='text-align:right;width:90px;'>"
              + (extGroupList[i].val==""? "все предприятия" : extGroupList[i].val + "&nbsp;предприятий")+"</td></tr>";
        }
        htm+="</table>";
        d.innerHTML=htm;
        if (extGroupList.length>10)
            d.style.height="180px";
        d.style.display="block";
        d.scrollTop=9*selected_i
        $("#grouptbl").one("mouseenter",function(){
            $('#tdgroup' + $('#gropVal').attr('val')).attr('className','bones_pointer');
        })
        
    }        
}

function restrictGropName(groupName){
    if(groupName.length>65)
        groupName=groupName.substring(0,65)+"..."; 
    return groupName;
}

function selectNewGroup(i){ 
    $("#gropVal").text(restrictGropName(extGroupList[i].txt));
    $("#gropVal").attr("val",extGroupList[i].code);
    document.body.removeChild(getObj("groupSelector"));
}

//Конец выбора группы

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
    /*if(bones_pressed){
        $("#sel_div_val").attr("sv",3);
        getObj("sel_div_val").innerHTML=searchNamesParams[3];

    }*/
    if(kk==13){
        doSearch();
    }

}

function dispissuer(text){
    $("#comp").val(text);
    $('input:checkbox[name=rsv]').attr('checked', true);
    //$("#sel_div_val").attr("sv",3);
    //getObj("sel_div_val").innerHTML=searchNamesParams[3];
    if(getObj("dp_window")){
        document.body.removeChild(getObj("dp_window"));   
    }
}
function hidepopups(){
    if(getObj("dp_window")){
        document.body.removeChild(getObj("dp_window"));   
    }
    if(getObj(tree_active + "_window")){
        getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window")); 
        $("#" + tree_active).css({'background-color' : '#FFFFFF'});
    }
}
var tree_active;
var tree_table;
var src;
function show_tree_selector(e,sr,is_tree,mult){
    
    src=(sr==99)? 1 : sr;
    tree_table=src;
    hidepopups()
    if (window.event){
        e = window.event;
        caller=e.srcElement;
    }
    if (e.stopPropagation) {
        e.stopPropagation();
        caller=e.target;
    }else{
         
        e.cancelBubble=true;
    }
    tree_active=caller.id;
    $("#" + tree_active).css({'background-color' : '#F0F0F0'});
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth; 
    if(!getObj(tree_active + "_window")){
        d=document.createElement("iframe");
        d.className="tree_frame";
        d.id=tree_active + "_window";
        bounds=$("#" + tree_active).position();
        d.style.top=bounds.top + 4 + $("#" + tree_active).height() + "px";
        if(scw>=bounds.left + 642){
            d.style.left=bounds.left+5 + "px";
        } else {
            d.style.left=bounds.left + $("#" + tree_active).width() - 638 + "px";
        }    
        d.frameBorder="0"
        if(is_tree==1){
            d.src = "/Tree/TreeSelector?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val();
        }else{
            tree_table+=6;
            d.src="/iss/selector/selector.asp?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val() + "&mult=" + mult;
        }
        getObj("td_" + tree_active).appendChild(d);
        $("#" + tree_active + "_window").click(function(event){
            event.stopPropagation();
      
        })
    }
}
function change_title(){
    $("#codes").attr({"title":$("#code_selector").find("input:radio:checked").attr("title")});
}
function displaytitle(e){
    var coords;
    var caller;
    if (window.event){
        e = window.event;
        caller=e.srcElement;
    }
    if (e.stopPropagation) {
        e.stopPropagation();
        caller=e.target;
    }else{
        e.cancelBubble=true;
    }
    coords={"x" : e.clientX, "y" : e.clientY};
    if($("#" + caller.id).attr("title").length>0){
        
        var d=document.createElement("div")
        d.id="dtitle";
        d.className="castom_title"
        d.style.position="absolute";
        d.style.top=coords.y + "px";
        d.style.left=coords.x + "px";
        document.body.appendChild(d);
        d.innerHTML=$("#" + caller.id).attr("title");
    }
    
}
function hidetitle(e){
    if(getObj("dtitle")){
        document.body.removeChild(getObj("dtitle"));
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

var codes_show=true;
function CodesClick() {
    if (codes_show == true) {
        $("#codes").attr("value","").removeClass('system_form_grey').addClass('system_form');
        codes_show = false;
    }
}

function CodesFocus() {
    if (codes_show == true) {
        $('#codes').val('').removeClass('system_form_grey').addClass('system_form');
        codes_show = false;
    }
}

function CodesBlur() {
    if ($("#codes").val() == "") {
        $("#codes").val("ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН").removeClass('system_form').addClass('system_form_grey');
        codes_show = true;
    }
}

function ClearText(txt) {
    txt = txt.toLowerCase().replace(/\s+\s+/g, ';').replace(/\s+/g, ';').replace(/\"/g, "").replace(/\'/g, "");
    return txt;
}

function check_monitor(){
    $.post('/Operation/EgrulMonitoringList', null, function (data) {
        if(data){
            check_monitor_callback(data);
        }
        else{
            check_monitor_callback("");
        }
    });
}
    
function check_monitor_callback(iss_list){
    var issuers=iss_list.split(',');
    $('td[id*="tdiss_"]').each(function(i){
        var issuer_id=$(this).attr('issuer');
        if($.inArray(issuer_id,issuers)>-1){
            //Удалить из списка мониторинга ЕГРЮЛ
            $(this).html("<span class=\"title_u\"><em>Удалить из списка мониторинга ЕГРЮЛ</em><img style=\"vertical-align:middle;cursor:pointer;\" src=\"/images/del_to_egrul.png\" alt=\"Удалить из списка мониторинга ЕГРЮЛ\" onclick=\"delIssuer('"+ issuer_id + "',1,true);\"></img></span>");
        }
        else{
            //Добавить в список мониторинга ЕГРЮЛ
            $(this).html("<span class=\"title_u\"><em>Добавить в список мониторинга ЕГРЮЛ</em><img style=\"vertical-align:middle;cursor:pointer;\" src=\"/images/add_to_egrul.png\" alt=\"Добавить в список мониторинга ЕГРЮЛ\" onclick=\"doAddEGRUL('"+ issuer_id + "');\"></img></span>");
        }
    });
}

function _get_paging(page, page_count) {

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
}

$(document).ready(function(){
    $("#comp").attr({"autocomplete":"off"});
    $("#search_table").bind("keypress",function(e){try2search(e);});
    $("#codes").val("ИНН, ОКПО, ОГРН, ФСФР, РТС/СКРИН");
    codes_show = 1;
    for(var i=1; i<acodes.length; i++){
        $("#c" + i).attr({"title":acodes[i].txt});
        $("#tc" + i).attr({"title":acodes[i].txt});
    }

    $('body').on('click',function(){hidepopups();});
  
    getObj("xls_frame").onreadystatechange=hideClock;
    document.onreadystatechange=hideClock;

    $("#xls_frame").ready(function(){
        hideClock();
    })

    if (user_id>0)
    {
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
    }
    });
var now=new Date();