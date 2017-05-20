// JScript File
var pin_reports = new Array();
var menu_selected;
var news_sel = "#";
var level_selected;
var current_user_id;
var iss;
var iid;
var tab_selected;
var first_tab_id;
var xls_params;
var tab_params;
var Service_Url = "/iss/modules/proxy_service.asp";
nn4 = (document.layers) ? true : false;

opera = (navigator.userAgent.indexOf('Opera') >= 0) ? true : false;

ie = (document.all && !opera) ? true : false;

dom = (document.getElementById && !ie && !opera) ? true : false;
/************************************/
function platformDetect() {
    if (navigator.appVersion.indexOf("Win") != -1) {
        return "Windows";
    }
    else if (navigator.appVersion.indexOf("Mac") != -1) {
        return "Macintosh";
    }
    else return "Other";
}
/************************************/
function OnEnterPress(e, proc) {
    if (e.keyCode == 13) {
        eval(proc);
    }
}
/************************************/
function BrowserInfo() {
    this.name = navigator.appName;
    this.codename = navigator.appCodeName;
    this.version = navigator.appVersion.substring(0, 4);
    this.platform = navigator.platform;
    if (String(this.platform) == "null") {
        this.platform = platformDetect();
    }
    this.javaEnabled = navigator.javaEnabled();
    this.screenWidth = screen.width;
    this.screenHeight = screen.height;
}
//открыть в окне для регистраторов
function openInWin(url) {
    var width = "400", height = "500";
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;
    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;
    var msgWindow = window.open(url, "WindowList", styleStr);
}
// Reading cookies
function readCookie(name) {
    var cookieValue = "";
    var search = name + "=";
    if (window.document.cookie.length > 0) {
        offset = window.document.cookie.indexOf(search);
        if (offset != -1) {
            offset += search.length;
            end = window.document.cookie.indexOf(";", offset);
            if (end == -1) end = window.document.cookie.length;
            cookieValue = unescape(window.document.cookie.substring(offset, end))
        }
    }
    return cookieValue;
}
// Writing cookies
function writeCookie(name, value, path, hours) {
    var expire = "";
    if (path != null) {
        path = "; path=" + path;
    }
    if (hours != null) {
        expire = new Date((new Date()).getTime() + hours * 3600000);
        expire = "; expires=" + expire.toGMTString();
    }
    window.document.cookie = name + "=" + escape(value) + path + expire;
}
/************************************/
function deleteCookie(name, path) {
    if (path != null) {
        path = "; path=" + path;
    }
    var curD = new Date(1901, 01, 01);
    window.document.cookie = name + "=" + path + "; expires=" + curD.toGMTString();
}
/************************************/
function init() {
    menu_selected = isNaN(menu_selected) ? 1 : menu_selected;
    level_selected = isNaN(level_selected) ? 0 : level_selected;
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
    var ww, wh, top, left;
    ww = (scw < 640) ? scw - 44 : 640;
    wh = (sch < 480) ? sch - 30 : 480
    $("#dialog_div").dialog({
        autoOpen: false,
        height: wh,
        width: ww,
        modal: false,
        resizable: true,
        draggable: true,
        closeOnEscape: true,
        close: function () {
            void (0);
        }
    });
    $("#prnb").unbind();
    $("#prnb").click(function () {
        doPrint("dcontent");
    })
}


/************************************/




function show_center_block() // функция отображает блок
{
    var block = document.getElementById("center_block");
    block.style.display = "block";
    block.style.top = Math.floor(getClientHeight() / 2 + getBodyScrollTop()) + "px";
    block.style.left = Math.floor(getClientWidth() / 2 + getBodyScrollLeft()) + "px";
    block.style.margin = "-" + Math.floor(200 / 2) + "px 0px 0px -" + Math.floor(300 / 2) + "px";
}

/************************************/
function LoadTabsJSON(id, iss, default_selected) {
    $.get(Service_Url, { "url": "/SkrinMenu/LoadTabs/", "params": "menu_id=" + id + "&ticker=" + iss }, function (data) {
        BuildTabs(data, default_selected);
        if (data[0].is_xls == 1) {
            getObj("bxls").className = "topmenu_button excel"
            $("#bxls").bind("click", function (e) { doExcell() });
        } else {
            getObj("bxls").className = "topmenu_button_locked excel_locked"
            $("#bxls").unbind();
        }
        if (data[0].is_prn == 1) {
            getObj("bprn").className = "topmenu_button printer"
        } else {
            getObj("bprn").className = "topmenu_button_locked printer_locked"
        }
    },
          "json"
   )
}

/*****/
function LoadIssuerMenu(iss) {
    $("#issuer_menu").load("/iss/modules/issuer_menu.asp?id=" + iss,
                            function () {
                                getObj("issuer_menu").style.zindex = "2";
                                getObj("mtd" + menu_selected).className = "tdmenu_sel";
                                if (ms_def > 0) {
                                    setclass(ms_def, ls_def, 'first');
                                }

                                hideClock();

                            });
}

/****/

/************************************/
function getBounds(element) {
    var left = element.offsetLeft;
    var top = element.offsetTop;
    for (var parent = element.offsetParent; parent; parent = parent.offsetParent) {
        left += parent.offsetLeft - parent.scrollLeft;
        top += parent.offsetTop - parent.scrollTop
    }

    return { left: left, top: top, width: element.offsetWidth, height: element.offsetHeight };
}
/************************************/

/************************************/
function showDiv(kind, text) {
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
    d = document.createElement("div");
    d.id = "infodiv";
    d.style.display = "block";
    d.style.position = "absolute";
    d.innerHTML = text;
    d.className = "div_" + kind;
    document.body.appendChild(d);
    var left = (scw / 2) - d.offsetWidth / 2;
    var top = (sch / 2) - d.offsetHeight / 2;
    d.style.left = left + "px";
    d.style.top = top + "px";
    window.setTimeout("document.body.removeChild(getObj(\"infodiv\"))", 2000);
}
/************************************/
function RemoveTabs() {
    var a = document.body.getElementsByTagName("div");
    var aids = new Array();
    for (var i = 0; i < a.length; i++) {
        aids[aids.length] = a[i].id;
    }
    for (var i = 0; i < aids.length; i++) {
        if (String(aids[i]).substring(0, 3) == "tab") {
            document.body.removeChild(getObj(aids[i]));
        }
    }
}
/************************************/
function BuildTabs(data, ds) {
    var tab;
    var cont;
    RemoveTabs();
    getObj("issuer_content").innerHTML = "";
    if (getObj("dcontent")) {
        getObj("dcontent").innerHTML = "";
    }
    var top = getObj("menu_item").offsetTop + getObj("menu_item").offsetHeight + 2;
    var right_border = 271
    for (var i = 0; i < data.length; i++) {
        tab = document.createElement("DIV");
        tab.id = "tab" + data[i].id;
        tab.prn = data[i].is_prn;
        tab.xls = data[i].is_xls;
        tab.className = "divtab"
        tab.style.position = "absolute";
        tab.style.top = top + "px"
        tab.style.border = "solid 1px #ABABAB";
        if (!isNaN(ds)) {
            tab_def = ds;
        }
        if ((tab_def == 0) ? (i == 0) : (data[i].id == tab_def)) {
            tab.style.borderBottom = "solid 1px #FFFFFF";
            tab_selected = data[i].id;
            loadcontent(tab_selected);
            WriteLog();
            first_tab_id = "tab" + tab_selected;
        } else {
            tab.style.background = "#F0F0F0";
            tab.style.cursor = "pointer";


        }
        tab.style.left = (right_border) + "px";
        tab.innerHTML = "&nbsp;&nbsp;" + data[i].name + "&nbsp;&nbsp;";
        document.body.appendChild(tab);
        /*    if (i!=0){*/
        $("#" + "tab" + data[i].id).click(TabSelect);
        /*}*/
        right_border += getObj("tab" + data[i].id).offsetWidth + 2;
    }
    drawIssuer_content_Div()
}
/************************************/
function drawIssuer_content_Div() {
    if (getObj(first_tab_id)) {
        var cont = getObj("issuer_content")
        cont.style.top = (getObj(first_tab_id).offsetTop + getObj(first_tab_id).offsetHeight - 1) + "px";
        cont.style.width = ((window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth) - 310) + "px";
        //cont.style.height=((window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight)-100) + "px";
        //cont.style.height= getObj("issuer_menu").style.height;
        cont.style.borderTop = "solid 1px #ABABAB";
        cont.style.borderLeft = "solid 1px #ABABAB"
    }
}
/************************************/
function TabSelect(e, tab_id) {

    WriteLog()
    var tab;

    if (!tab_id) {
        tab_selected = String(e.currentTarget.id).substring(3, 10);
        var a = document.body.getElementsByTagName("div");
        for (var i = 0; i < a.length; i++) {
            if (String(a[i].id).substring(0, 3) == "tab") {
                tab_id = String(a[i].id).substring(3, 10)
                tab = getObj(a[i].id)
                if (tab_id == tab_selected) {
                    tab.style.borderBottom = "solid 1px #FFFFFF";
                    $("#" + a[i].id).unbind();
                    tab.style.background = "#FFFFFF";
                    tab.style.cursor = "default";
                    getObj("bprn").className = (tab.prn == 1) ? "topmenu_button printer" : "topmenu_button_locked printer_locked";
                    getObj("bxls").className = (tab.xls == 1) ? "topmenu_button excel" : "topmenu_button_locked excel_locked";
                    if (tab.xls == 1) {
                        $("#bxls").bind("click", function (e) { doExcell() });
                    } else {
                        $("#bxls").unbind();
                    }

                } else {
                    tab.style.borderBottom = "dotted 1px #000000";
                    $("#" + a[i].id).unbind();
                    $("#" + a[i].id).click(TabSelect);
                    tab.style.background = "#F0F0F0";
                    tab.style.cursor = "pointer";
                }
            }
        }
    } else {
        tab_selected = tab_id
        $("div").each(function (i) {
            if (this.id.substring(0, 3) == "tab") {
                this.style.borderBottom = "dotted 1px #000000";
                $("#" + this.id).unbind();
                $("#" + this.id).click(TabSelect);
                this.style.background = "#F0F0F0";
                this.style.cursor = "pointer";
            }
        })
        tab = getObj("tab" + tab_id)
        tab.style.borderBottom = "solid 1px #FFFFFF";
        $("#tab" + tab_id).unbind();
        tab.style.background = "#FFFFFF";
        tab.style.cursor = "default";
    }
    loadcontent(tab_selected);
}
/************************************/
function MenuIssuer(id) {
    $("#issuer_menu").load("/iss/modules/issuer_menu.asp", { id: id })
}
/************************************/
function showLogin(ltype, text, proc) {
    if (!proc) {
        proc = '';

    }
    if (text.length == 0) {
        text = "Вход для клиентов";
    }
    showwin(ltype, "<div class=\"normal\">" + text + "<form id=\"loginform\" method=\"post\">" +
    "<input type=hidden name=\"stored_id\" value=\"\" id=\"stored_id\" value=\"" + readCookie("storedID") + "\"/>" +
    "<table align=\"left\" style=\"width:100%\">" +
    "<tr><td>Логин :</td><td><input class=\"system_form\" type=\"text\" name=\"login\" id=\"login\" value=\"" + readCookie("user%5Flogin") + "\"/></td></tr>" +
    "<tr><td>Пароль:</td><td><input class=\"system_form\"  type=\"password\" name=\"pswd\" id=\"pswd\"/></td></tr>" +
    "<tr><td colspan=\"2\"><input type=\"button\" class=\"btns blue\" id=\"btn_go\" value=\"ВХОД\" onclick=\"doLogin('" + proc + "')\"/><img src=\"/images/null.gif\" width=\"15px\" height=\"1px\" alt=\"\"/><input type=\"button\" class=\"btns blue\" id=\"btn_close\" value=\"ЗАКРЫТЬ\" onclick=\"closeSpan()\"/></td></tr>" +
    "</table></form>По вопросам подписки Вы можете связаться с отделом продаж по тел.: <b>(495)787-17-67<br/> </b> или " +
    "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>оставив&nbsp;запрос</a></div>", -1);

}
/************************************/
function doLogin(proc) {
    var info = new BrowserInfo();
    $.post("/iss/modules/operations.asp", { action: 4, m0: info.name, m1: info.codename, m2: info.version, m3: info.platform, m4: ((info.javaEnabled) ? 1 : 0), m5: info.screenWidth, m6: info.screenHeight, iss: iss, login: getObj("login").value, pswd: getObj("pswd").value },
        function (data) {


            switch (String(data[0].code)) {
                case "0": {
                    u = data[0].user_id;
                    if (getObj("issuer_content")) {
                        loadcontent(tab_selected);
                    }
                    current_user_id = data[0].user_id;
                    showwin("info", "Добро пожаловать в закрытый раздел системы &laquo;СКРИН&raquo;<div align=\"center\"><input type=\"button\" class=\"btns blue\" value=\"ЗАКРЫТЬ\" onclick=\"closeSpan();\"></div>", 2000);
                    if (getObj("brep")) {
                        getObj("brep").className = "topmenu_button report";
                        getObj("begrul").className = "topmenu_button egrul";
                        getObj("mrimg").className = "topmenu_button massreg";
                        getObj("visio").className = "topmenu_button massreg";
                        $("#brep").click(buildPDF);
                        $("#begrul").bind("click", function (e) {
                            doEGRUL(iss, data[0].user_id)
                        })
                        $("#mrimg").bind("click", function (e) {
                            searchMassReg(iss, data[0].user_id)
                        })
                        $("#visio").bind("click", function (e) {
                            showFounders(iss)
                        })
                    }
                    if (getObj("search_login")) {
                        //Это страница поиска. Надо вывести содержимое о логине.
                        $("#search_login").load("/iss/modules/operations.asp", { u: current_user_id, action: 14 })
                        if (proc.length > 0) {
                            switch (proc) {
                                case "events": {
                                    $("#ev_search").unbind();
                                    $("#ev_search").bind("click", function (e) { EventsSearch(); });
                                    if (issearch_pressed == 1) {
                                        EventsSearch()
                                    } else {
                                        showeventslist(1)
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
                case "1": {
                    showLogin("warning", data[0].text, proc);
                    break;
                }
                case "2": {
                    accessdenied();
                    break;
                }
                case "3": {
                    showLogin("critical", data[0].text);
                    break;
                }


            }
        },
        "json"
    )
}
/************************************/
function accessdenied(event, iss_trans) {

    showwin("critical", "<p align=\"justify\">Для просмотра информации по данной компании необходим уровень доступа СКРИН \"Предприятия\".<br/>" +
            "По вопросам изменения уровня доступа Вы можете связаться с <br/>Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <br/>" +
            "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>Оставить&nbsp;запрос</a><br/><br/>" +
            "Бесплатно ознакомиться с Профилем данной компании Вы можете в <a href=\"http://shop.skrin.ru/issuers/" + ((!iss) ? iss_trans : iss) + "/reports/\" target=\"_blank\">Интернет-магазине СКРИН</a><a href=\"http://shop.skrin.ru/issuers/" + ((!iss) ? iss_trans : iss) + "/reports/\" target=\"_blank\"><img src=/images/mnu_icon_bask.gif width=22 height=22 border=0 align=\"absMiddle\"></a></p>", 0);

}
/************************************/
function loadcontent(id, params) {
    //DrawPin();
    var send_params;

    $.post("/iss/modules/operations.asp", { id: id, action: 2, iss: iss },
            function (data) {
                showClock();
                if (data[0].result == 1) {
                    $('#brep').click(function (e) { doReport(e) });
                    $("#issuer_content").load("/iss/content/" + data[0].module + "/code.asp", { iss: iss },
                    function (data) {
                        hideClock();
                        if ($("#tmpl_job").get().length > 0 && $("#tmpl_job").val() != '') {
                            eval($("#tmpl_job").val());
                        }
                    });
                }
                if (data[0].result == 2) {
                    hideClock();
                    showLogin("warning", "Вход для клиентов");
                }
                if (data[0].result == 4) {
                    hideClock();
                    $('#brep').click(function () { LoginErr(1) });
                    showwin("critical", "<div class=\"normal\">Ваш вариант подписки не дает доступ к выбранному разделу/предприятию.<br/><br/>По вопросам <b>расширения уровня доступа</b> Вы можете связаться с Отделом продаж и маркетинга по телефонам <b>(495) 787-17-67 " +
					                    "</b> или <a href=\"/company/support/\"><img src=\"/images/mnu_icon_question1.gif\" width=\"22\" height=\"22\" border=\"0\" align=\"absMiddle\"></a>&nbsp;<a href=\"/company/support/\">Оставьте&nbsp;запрос</a>" +
					                    ", и специалисты СКРИН сами свяжутся с Вами в ближайшее время.</div>", 0)


                }
            }
    , "json");

    $("#menu_item").load("/iss/modules/operations.asp", { id: menu_selected, action: 9 });
}

window.onresize = function () {
    drawIssuer_content_Div();

};
/************************************/
function setclass(id, level, tab_sel, params) {
    if (params) {
        tab_params = params;
    }
    var aDivs = ["scandiv", "pap_div", "news_div"]
    for (var i = 0; i < aDivs.length; i++) {
        if (getObj(aDivs[i])) {
            document.body.removeChild(getObj(aDivs[i]));
        }
    }
    if (id != menu_selected) {
        id = ((id == 20) ? 54 : id)
        if (!menu_selected) {
            menu_selected = id
        }
        var td_clicked = getObj("mtd" + id)

        var td_selected = getObj("mtd" + menu_selected)
        if (getObj("mtd" + level)) {
            getObj("mtd" + level).className = "tdmenu_sel";
        }
        td_clicked.className = (level == 0) ? "tdmenu_sel" : "tdsubmenu_sel";
        td_selected.className = (level_selected == 0) ? "tdmenu" : "tdsubmenu";
        menu_selected = id;
        if (getObj("mtd" + level_selected) && level != level_selected) {
            getObj("mtd" + level_selected).className = "tdparentmenu";
        }
        level_selected = level;
        LoadTabsJSON(id, iss, tab_sel);


    }
}
/************************************/
function move2item(id) {
    //id - это из menu_tabs..menu_tabs собираем все о закладке
    $.get("/iss/modules/operations.asp", { iss: iss, action: 5, tab_id: id },
        function (data) {
            var menu_id = data[0].menu_id;
            setclass(data[0].menu_id, data[0].parent_id, data[0].tab_id);
        }, "json"
    )


}
/************************************/
function move2ticker() {
    var new_ticker = getObj("new_ticker").value;
    pin_reports = new Array();
    showClock();
    $.get("/iss/modules/operations.asp", { "iss": new_ticker, "action": 0, "menu_id": menu_selected, "tab_id": tab_selected },
            function (data) {
                if (String(data[0].exist) == "0") {
                    hideClock();
                    showwin("critical", "Эмитента с таким кодом (" + String(data[0].t) + ") СКРИН не существует.<br/>Попробуйте ввести другой код.", 0)
                } else {

                    if (String(data[0].menu_act) == "1") {
                        var form = document.createElement("form");
                        form.action = "/issuers/" + new_ticker;
                        form.method = "post";
                        form.style.display = "none"
                        var element = null;
                        element = document.createElement("input");
                        element.type = "text";
                        element.name = "id";
                        element.value = menu_selected;
                        form.appendChild(element);
                        element = document.createElement("input");
                        element.type = "text";
                        element.name = "tab_id";
                        element.value = tab_selected;
                        form.appendChild(element);
                        document.body.appendChild(form);
                        form.submit();
                    } else {
                        document.location.href = "/issuers/" + new_ticker;
                    }
                }
            },
    "json")
}
/************************************/
function WriteLog() {
    $.post("/iss/modules/operations.asp",
        { menu_id: menu_selected, tab_id: tab_selected, iss: iss, action: 1 }
    );
}
/************************************/
function iframepost(params, url, name, metod) {
    if (getObj(name)) {
        var form = document.createElement("form");
        form.action = url;
        form.method = ((metod) ? metod : "post");
        form.target = name;
        form.style.display = "none";
        var element = null;
        for (var propName in params) {
            element = document.createElement("input");
            element.type = "text"
            element.name = propName
            element.value = params[propName]
            form.appendChild(element);
        }
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    }
}
/************************************/

function Form2Popup(params, actionUrl, name, popupConfig, get) {
    var method = (get == undefined || !get) ? 'POST' : 'GET';
    if (name == undefined || name == '') {
        name = 'tmpPopup';
    }
    var form = document.createElement("form");
    form.action = actionUrl;
    form.method = method;
    form.target = name
    form.style.display = "none"
    var element = null;
    for (var propName in params) {
        element = document.createElement("input");
        element.type = "text"
        element.name = propName
        element.value = params[propName]
        form.appendChild(element);
    }
    document.body.appendChild(form);
    var win = window.open('about:blank', name, popupConfig);
    win.focus();
    form.submit();
    document.body.removeChild(form);
    return win;
}
/************************************/
function GoLPT(iss) {
    var frm
    var ch = ""
    frm = document.getElementById("frmxls")
    for (var i = 2; i < frm.length - 2; i++) {
        if (frm[i].type == "checkbox") {
            if (frm[i].checked) {
                ch += (String(frm[i].name).substr(3, 3) + ",")
            }
        }
    }
    ch = ch.substr(0, ch.length - 1)
    var params = { ids: ch, iss: iss };
    var width = "1024", height = "768";
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;

    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;

    var previewPopup = Form2Popup(params, "/issuers/lpt/index.asp", "print", styleStr);
}

/************************************/
function Shownews1(id, search_text, iss_code) {

    var params = { id: id, ss: search_text, iss: iss };
    var width = "800", height = "600";
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;
    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;

    var previewPopup = Form2Popup(params, "/issuers/news/selected.asp", iss_code + "_news", styleStr);
}

/************************************/
function doCanOpen() {
    var i;
}
/*****************************************/
//function Shownews(id, search_text, iss_code, pos, should_sel, proc, ntt, is_bunkrupt, is_event) {

//    if (typeof (ntt) == 'undefined') ntt = '';
//    if (typeof (is_bunkrupt) == 'undefined') is_bunkrupt = '0';
//    if (typeof (is_event) == 'undefined') is_event = '0';
//    /*if (should_sel==1 && getObj("i" + pos)){
//    var img=getObj("i" + pos)
//    img.src="/images/icon_page_sel.gif";
//    news_sel=pos;
//    } */
//    showClock();
//    $.post("/iss/modules/news_selected.asp", { news_id: id, ss: search_text, iss_code: iss_code, ntt: ntt, is_bunkrupt: is_bunkrupt, is_event: is_event },
//        function (data) {
//            hideClock();
//            //alert('ZZZZZZZZZZZZZZad = ' + action);
//            var action = data[0].action + '';
//            switch (action) {
//                case "1":
//                    {
//                        $("#dcontent").html(data[0].text);
//                        $("#dialog_div").dialog("open");
//                        $("#dialog_div").scrollTop(-5)
//                        break;
//                    }
//                case "2":
//                    {
//                        showLogin("warning", "Вход для клиентов", proc);
//                        break;
//                    }
//                case "4":
//                    {
//                        accessdenied();
//                        break;
//                    }
//                default:
//                    alert('ZZZZZZZZZZZZZZad = ' + action);
//            }
//        },
//        "json"

//    )
//}

function shownews(id, search_text, iss_code, pos, should_sel, proc) {  
    showClock();
    if (typeof (iss_code) == 'undefined') iss_code = '';
    $.post("/Bargains/GetMessage", { events_id: id, ss: search_text, issuser_id: iss_code },
        function (data) {
            hideClock();           
            show_dialog({ "content": data, "is_print": true });
        });
}
/*****************************************/
//function Showevent(search_text, iss_code, eid, multy_sel) {
//    showClock();
//    if (typeof (iss_code) == 'undefined') iss_code = '';
//    $.post("/iss/modules/events_selected.asp", { events_id: eid, ss: search_text, iss_code: iss_code, ms: multy_sel },
//        function (data) {
//            hideClock();

//            switch (data[0].action) {
//                case "0": {
//                    $("#dcontent").html(data[0].text);
//                    $("#dialog_div").dialog("open");
//                    break;
//                }
//                case "2": {
//                    showLogin("warning", "Вход для клиентов", proc);
//                    break;
//                }
//                case "4": {
//                    accessdenied();
//                    break;
//                }
//            }
//        },
//        "json"
//    )
//}

function Showevent(search_text, iss_code, eid, multy_sel) {
    showClock();
    if (typeof (iss_code) == 'undefined') iss_code = '';
    $.post("/Message/EventsSelected/", { events_id: eid, ss: search_text, iss_code: iss_code, ms: multy_sel },
        function (data) {     
            hideClock();
            var content = $(data).html();       
            show_dialog({ "content": data, "is_print": true });
        });
}
/************************************/
function showSelectedEvents(search_text,all) {
    var tmp_str = "";
    //for (i = 0; i < document.info.elements.length; i++) {
    //    if (document.info.elements[i].type == "checkbox") {
    //        if (document.info.elements[i].checked) {
    //            tmp_str += document.info.elements[i].value;
    //        }
    //    }
    //}
    if (all) {
        $('input[name="ch_bankrot"]:checkbox ').each(function (i) {
        if ($(this).attr('needprint') == 1) tmp_str += this.value + ","; 
        });
    }
    else {
        $('input[name="ch_bankrot"]:checkbox:checked').each(function (i) {
            if ($(this).attr('needprint') == 1) tmp_str += this.value + ",";
        });
    }
   

    if (tmp_str != "") {
        Showevent(search_text, iss, tmp_str, 1)
    } else {
        showwin("warning", "Нет ни одного выбранного корпоративного события.", 2000);
    }
}
/************************************/
function showonpageEvents(kw) {
    $("input:checkbox").each(function (i) {
        if (this.value.length >= 32) {
            this.checked = true;
        }
    });
    showSelectedEvents(kw);
}
/************************************/
function closenews() {
    var d = getObj("news_div");
    if (d) {
        document.body.removeChild(d);
    }
    if (!isNaN(news_sel)) {
        if (getObj("i" + news_sel))
            getObj("i" + news_sel).src = "/images/icon_page.gif";
        news_sel = "#"
    }
}
/************************************/
function doPrint(id) {
    $("#" + id).printElement({
        pageTitle: iss + ".html", leaveOpen: false,
        printMode: 'popup'
    });
}
/************************************/
function doPrintTown(id, nm) {
    $("#" + id).printElement({
        pageTitle: nm + ".html", leaveOpen: false,
        printMode: 'popup'
    });
}
/************************************/
function doPrintRegion(id, nm) {
    $("#" + id).printElement({
        pageTitle: nm + ".html", leaveOpen: false,
        printMode: 'popup'
    });
}
/************************************/
function showSelected(ss, iss, ntt, is_bunkrupt, is_event) {
    if (typeof (ntt) == 'undefined') ntt = '';
    var tmp_str = "";
    for (i = 0; i < document.info.elements.length; i++) {
        if (document.info.elements[i].type == "checkbox") {
            if (document.info.elements[i].checked) {
                tmp_str += document.info.elements[i].name + ",";
            }
        }
    }
    if (tmp_str != "") {
        Shownews(tmp_str, ss, iss, 1, 0, '', ntt, is_bunkrupt, is_event)
    } else {
        showwin("warning", "Нет ни  " + ((String(is_event) == "1") ? "одного выбранного корпоративного события." : "одной выбранной новости."), 2000);
    }
}
///***********************************/
function loadJSFile(fn, sid) {
    if (!getObj(sid)) {
        var script = document.createElement("script");
        script.type = 'text/javascript';
        script.id = sid;
        document.getElementsByTagName('head')[0].appendChild(script);
        $.ajax({
            url: fn + "?tt=" + String(new Date()),
            async: false,
            success: function (src) {
                getObj(sid).innerTEXT = src;

            },
            dataType: "script"
        })


    }

}
/************************************/
function openIssuer(ticker) {
    var width = screen.width, height = screen.height;
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;
    var win_name = String(ticker).replace("%21", "_").replace("!", "_").replace("-", "_")
    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;
    window.open('/issuers/' + ticker, win_name, styleStr);
}
/*function openIssuer(ticker){
	var width="1024", height="900";
	var left = (screen.width/2) - width/2;
	var top = (screen.height/2) - height/2;
	var win_name=String(ticker).replace("%21","_").replace("!","_")
	var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
	window.open('/issuers/' + ticker + '/', win_name, styleStr);
}*/
/************************************/
function openProfileFL(fio, inn) {
    if (window.CanFL != null) {
        if (!window.CanFL) {
            no_rights();
            return void(0);
        }
    }
    var width = screen.width, height = screen.height;
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;
//    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;
//    window.open('/profilefl?fio=' + encodeURIComponent(fio) + '&inn=' + encodeURIComponent(inn), '', styleStr);
     window.open('/profilefl?fio=' + encodeURIComponent(fio) + '&inn=' + encodeURIComponent(inn), '_blank');
}
/************************************/
function killObj(id) {
    if (getObj(id)) {
        document.body.removeChild(document.getElementById(id));
    }
}
/************************************/
function myPrompt(obj_id, txt, val, func, max_pages, so) {
    var bounds = getBounds(getObj(obj_id));
    if (!getObj("myprompt_d")) {
        var d = document.createElement("div");
        d.id = "myprompt_d";
        d.className = "prompt";
        d.style.left = bounds.left + "px";
        d.style.top = (bounds.top + bounds.height - 1) + "px";
        d.style.width = "250px";
        d.style.height = "80px";
        var compiled_function = "&quot;" + func + "((isNaN($(\'#mp_val\').val()) || $(\'#mp_val\').val()-$(\'#maxp\').val()>0 || $(\'#mp_val\').val()<=0)?1:$(\'#mp_val\').val(),$(\'#so\').val())&quot;";
        d.innerHTML = "<b>" + txt + "</b><span style=\"text-align:center\"><form id=\"frm_pg\" onkeypress=\"var e=window.event?window.event:event;if(e.keyCode==13){eval(" + compiled_function + ");killObj(&quot;myprompt_d&quot;)} if(e.keyCode==27){killObj(&quot;myprompt_d&quot;)}\"><input class=\"system_form\" type=\"text\" id=\"mp_val\" value=\"" + val + "\"/><img src=\"/images/form_action_btn.gif\" alt=\"\" style=\"cursor:pointer;\" onclick=\"eval(" + compiled_function + ");killObj(&quot;myprompt_d&quot;)\" align=\"absbottom\"/><img src=\"/images/form_cancel_btn.gif\" alt=\"\" style=\"cursor:pointer;\" onclick=\"killObj(&quot;myprompt_d&quot;)\" align=\"absbottom\"/><input type=\"hidden\" id=\"maxp\" value=\"" + max_pages + "\"/>" + ((so) ? "<input type=\"hidden\" id=\"so\" value=\"" + so + "\"/>" : "") + "</form></span>";
        window.setTimeout("getObj(\"mp_val\").focus()", 500);
        document.body.appendChild(d);
    }

}
/****************************************/
var trans = [];
for (var i = 0x410; i <= 0x44F; i++)
    trans[i] = i - 0x350; // А-Яа-я
trans[0x401] = 0xA8;    // Ё
trans[0x451] = 0xB8;    // ё
// Сохраняем стандартную функцию escape()
var escapeOrig = window.escape;
// Переопределяем функцию escape()
window.escape = function (str) {
    var ret = [];
    // Составляем массив кодов символов, попутно переводим кириллицу
    for (var i = 0; i < str.length; i++) {
        var n = str.charCodeAt(i);
        if (typeof trans[n] != 'undefined')
            n = trans[n];
        if (n <= 0xFF)
            ret.push(n);
    }
    return escapeOrig(String.fromCharCode.apply(null, ret));
}
//*********************************************
function openTown_PRN(menu, soato, period) {

    var width = "1024", height = "768";
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;

    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;
    var wndprn = window.open("/gmc/towns/" + soato + "/" + menu + "/prn/?p=" + period, "wndprn", styleStr);
}
//*********************************************
function openRegion_PRN(menu, okato, period, do2010, per) {

    var width = "1024", height = "768";
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;

    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;
    var wndprn = window.open("/gmc/regions/" + okato + "/" + menu + "/prn/?years=" + period + "&per=" + per + "&do2010=" + do2010, "wndprn", styleStr);
}
/*********************************/
function openXLS_Town(menu, soato, period) {
    var path = "/gmc/towns/" + soato + "/" + menu + "/xls/?p=" + period;
    location.href = path;
}
/*********************************/
function openTown(ticker) {
    var width = screen.width, height = screen.height;
    var left = (screen.width / 2) - width / 2;
    var top = (screen.height / 2) - height / 2;
    var win_name = String(ticker).replace("%21", "_").replace("!", "_")
    var styleStr = 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top;
    window.open('/gmc/towns/' + ticker + '/', win_name, styleStr);
}
/******************************************/
//function showRegister(reg_id, reg_type) {
//    var d;
//    if (!getObj("regdiv")) {

//        d = document.createElement("div");
//        d.id = "regdiv";
//        d.style.height = "320px";
//        d.className = "infodiv"
//        var boundes = $("#reghref").position();

//        d.style.top = (boundes.top + 80 + $("#reghref").height()) + "px";
//        d.style.left = (boundes.left + 260) + "px";
//        d.style.width = "640px";
//        document.body.appendChild(d);
//    } else {
//        document.body.removeChild(getObj("regdiv"));
//    }
//    if (getObj("regdiv")) {
//        $.post("/iss/modules/register_clients.asp", { "id": reg_id, "type": reg_type },
//        function (data) {
//            d.innerHTML = data;
//            $("html").click(closeRegs);
//        })
//    }

//}
function showRegister(reg_id, reg_type) {
    showClock();
    if (typeof (iss_code) == 'undefined') iss_code = '';
    $.post("/Message/GetRegister/", { reg_id: reg_id, reg_type: reg_type },
        function (data) {
            hideClock();
            // var content = $(data).html();
            show_dialog({ "content": data, "is_print": true });
        });
}

function closeRegs() {
    if (getObj("regdiv")) {
        document.body.removeChild(getObj("regdiv"));
        $("html").unbind();

    }
}
function TrimStr(s) {
    s = String(s).replace(/^\s+/g, '');
    return s.replace(/\s+$/g, '');
}
/*******************************************/
function padl(st, ch, len) {
    var retval = String(st);
    while (retval.length < len) {
        retval = "" + ch + retval;
    }
    return retval;
}
function showscans(doc_id, pages, issuer_id, page_no) {
    var d;
    showClock();
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
    var ww, wh, top, left;
    //btnback=(page_no==1)?"<input type=\"button\" class=\"btns dis\" value=\"Предыдущий\">":"<input type=\"button\" class=\"btns blue\" onclick=\"showscans('" + doc_id+"'," + pages + ",'" + issuer_id + "'," + (page_no-1) +")\" value=\"Предыдущий\">";
    //btnff=(page_no==pages)?"<input type=\"button\" class=\"btns dis\" value=\"Следующий\">":"<input type=\"button\" class=\"btns blue\" onclick=\"showscans('" + doc_id+"'," + pages + ",'" + issuer_id + "'," + (page_no+1) +")\" value=\"Следующий\">";
    // btnback=((page_no==1)?"<a class='ui-dialog-titlebar-back ui-corner-all' href='#' onclick='void(0)' role='button' title='Следующий'><span class=\"title_back_na\">":"<a class='ui-dialog-titlebar-back ui-corner-all' href='#' onclick=\"showscans('" + doc_id+"'," + pages + ",'" + issuer_id + "'," + (page_no-1) +")\" role='button' title='Предидущий'><span class=\"title_back\" title=\"Предыдущий\">") + "</span></a>";
    //btnff=((page_no==pages)?"<a class='ui-dialog-titlebar-ff ui-corner-all' href='#' onclick='void(0)' role='button' title='Следующий'><span class=\"title_ff_na\">":"<a class='ui-dialog-titlebar-ff ui-corner-all' href='#' onclick=\"showscans('" + doc_id+"'," + pages + ",'" + issuer_id + "'," + (page_no+1) +")\" role='button' title='Следующий'><span class=\"title_ff\"  title=\"Следующий\">") + "</span></a>";
    btnback = ((page_no == 1) ? "<a href='#' onclick='void(0)' role='button' title='Следующий'><span class=\"title_back_na\">" : "<a  href='#' onclick=\"showscans('" + doc_id + "'," + pages + ",'" + issuer_id + "'," + (page_no - 1) + ")\" role='button' title='Предидущий'><span class=\"title_back\" title=\"Предыдущий\">") + "</span></a>";
    btnff = ((page_no == pages) ? "<a href='#' onclick='void(0)' role='button' title='Следующий'><span class=\"title_ff_na\">" : "<a  href='#' onclick=\"showscans('" + doc_id + "'," + pages + ",'" + issuer_id + "'," + (page_no + 1) + ")\" role='button' title='Следующий'><span class=\"title_ff\"  title=\"Следующий\">") + "</span></a>";

    if (!getObj("scan_dialog_div")) {
        d = document.createElement("div");
        d.id = "scan_dialog_div"
        d.style.display = "none";
        d.style.textAlign = "center";

        document.body.appendChild(d);
        //        $("#scan_dialog_div").attr("title","<div style='height:10px;'><a class='ui-dialog-titlebar-print ui-corner-all' href='#' id='prnb' onclick='doPrint(&quot;sc_content&quot;);'role='button' title='Печать'><span class='title_printer'></span></a></div>");
    } else {
        d = getObj("scan_dialog_div")
    }

    ww = (scw < 840) ? scw - 44 : 840;
    wh = (sch < 700) ? sch - 30 : 700
    $("#scan_dialog_div").dialog({
        autoOpen: false,
        height: wh,
        width: ww,
        modal: false,
        resizable: true,
        draggable: true,
        closeOnEscape: true,
        close: function () {
            void (0);
        }
    });

    d.innerHTML = "<div style=\"background-color:#FFFFFF;text-align:center;border:none;\" id=\"sc_content\"></div>";



    $.post("/iss/modules/operations.asp", { action: 7, pn: page_no, id: doc_id },
						function (data) {
						    $("#sc_content").html("<img src=\"/docs/" + issuer_id + "/" + doc_id + "/" + data + "\" alt=\"\" id=\"scan_img\" style=\"text-align:center;\"/>");
						    hideClock();
						    $("#scan_dialog_div").dialog("open");
						    $("#scan_dialog_div").dialog("option", "title", "<div style='height:10px;'><a class='ui-dialog-titlebar-print ui-corner-all' href='#' id='prnb' onclick='doPrint(&quot;sc_content&quot;);'role='button' title='Печать'><span class='title_printer'></span></a></div><table class=\"ui-dialog-titlebar-ff ui-corner-all\"><tr><td>" + btnback + "</td><td nowrap=\"nowrap\">[стр. " + page_no + "]</td><td>" + btnff + "</td></tr></table>");
						}
						, "html");


}
/*******************************************/

function showtxt(doc_id, issuer_id, fn, real_path) {
    showClock();
    $.post("/iss/modules/operations.asp", { action: 8, iss: issuer_id, id: TrimStr(doc_id), fn: fn, real_path: real_path },
           function (data) {
               hideClock();
               $("#dcontent").html(data);
               $("#dialog_div").dialog("open");
               $("#dialog_div").scrollTop(-5)
           },
        "html"
    )
}
//function showbarg(doc_id, issuer_id, fn, real_path) {
//    showClock();
//    $.post("/iss/modules/operations.asp", { action: 16, iss: issuer_id, id: TrimStr(doc_id), fn: fn, real_path: real_path },
//           function (data) {
//               hideClock();
//               $("#dcontent").html(data);
//               $("#dialog_div").dialog("open");
//               $("#dialog_div").scrollTop(-5)
//           },
//        "html"
//    )
//}
function showbarg(ticker,doc_id, issuer_id, fn) {
    showClock();
    if (typeof (iss_code) == 'undefined') iss_code = '';
    $.post("/Bargains/GetBarg/", {ticker:ticker, issuer_id: issuer_id, id: TrimStr(doc_id), fn: fn },
        function (data) {
            hideClock();
           // var content = $(data).html();
            show_dialog({ "content": data, "is_print": true });
   });
    //showClock();
    //$.post("/iss/modules/operations.asp", { action: 16, iss: issuer_id, id: TrimStr(doc_id), fn: fn, real_path: real_path },
    //       function (data) {
    //           hideClock();
    //           $("#dcontent").html(data);
    //           $("#dialog_div").dialog("open");
    //           $("#dialog_div").scrollTop(-5)
    //       },
    //    "html"
    //)

}
/*******************************/
function closeScandiv() {
    $("html").unbind();
    if (getObj("scandiv")) {
        document.body.removeChild(getObj("scandiv"));
    }
}
/*******************************/
function stopevent(e) {

    if (window.event) {
        e = window.event;
    }
    kk = e.keyCode;
    if (e.stopPropagation) {
        e.stopPropagation();
    } else {
        e.cancelBubble = true;
    }
}
//Функция возвращает ширину клиентской области окна
function getViewportWidth() {
    if (window.innerWidth) {//Все броузеры, кроме IE
        return window.innerWidth;
    }
    else if (document.documentElement && document.documentElement.clientWidth) {//для IE 6 и документов с объявлением DOCTYPE
        return document.documentElement.clientWidth;
    }
    else {
        return document.body.clientWidth;
    }
}
function doReport(e) {
    if (window.event) { e = window.event }
    if (e.stopPropagation) {
        e.stopPropagation();
    } else {
        e.cancelBubble = true;
    }

    if (!getObj("dreport")) {
        var d = document.createElement("div");
        var bounds = getBounds(getObj("brep"));
        d.style.top = bounds.top + $("#brep").height() + 6 + "px";
        d.style.left = bounds.left + "px";
        d.className = "egrul_selector";
        d.id = "dreport";
        d.innerHTML = "<span id='doPDF' class='getReport' onclick='buildPDF()'><img src='/images/icon_docpdf_16.gif' alt=''/> В формате PDF</span><br/>";
        d.innerHTML += "<span id='doXLS' class='getReport' onclick='buildXLS()'><img src='/images/icon_docexel_16.gif' alt=''/> В формате XLS</span>";
        document.body.appendChild(d);
        $("html").bind("click", function (e) {
            if (getObj("dreport")) {
                document.body.removeChild(getObj("dreport"));
                $("html").unbind();
            }
        })
    }
}

function doEGRUL(iss, user_id, isFromStatus) {

    if (user_id > 0) {
        txtIss = iss;
        var d = document.createElement("div");
        var bounds;
        if (isFromStatus) {
            bounds = getBounds(getObj("egrstate_get"));
            d.style.top = bounds.top + $("#egrstate_get").height() + 6 + "px";
            //d.style.left = bounds.left + "px";
            d.style.right = (getViewportWidth() - bounds.left - $("#egrstate_get").outerWidth()) - 18 + "px";
            //d.style.border = "solid black 1px";
            /*d.style.backgroundImage = "url(/images/button_back.gif)";
            d.style.backgroundRepeat = "repeat-x";*/
        }
        else {
            bounds = $("#begrul").position();
            d.style.top = bounds.top + 6 + $("#begrul").height() + "px";
            d.style.left = bounds.left + "px";
        }
        //d.style.backgroundColor = "#DBD8D1";
        d.className = "egrul_selector";
        d.id = "degrul";
        document.body.appendChild(d);
        showClock();
        $.get("/iss/modules/getegrul.asp", { "iss": iss, "user_id": user_id, "id": 0 },
            function (data) {
                if (data[0].kod == 0) {
                    d.innerHTML = data[0].txt;
                    $("html").bind("click", function (e) {
                        if (getObj("degrul")) {
                            document.body.removeChild(getObj("degrul"));
                            $("html").unbind();
                        }
                    })
                } else {
                    if (getObj("degrul")) {
                        document.body.removeChild(getObj("degrul"));
                        $("html").unbind();
                    }
                    if (data[0].kod == -1) {
                        showwin("info", "<div style=\"text-align:center;\">Информация по данной компании отсутствует в ЕГРЮЛ.<br/>" +
                                       "Для уточнения запроса предлагаем связаться со специалистами Отдела продаж и маркетинга ЗАО \"СКРИН\"<br/>" +
                                       "телефоны: (495) 787-17-67,  e-mail: sales@skrin.ru</div>", 0);
                    } else {
                        showwin("info", "<div style=\"text-align:center;\">Сервис заказа Выписок из ЕГРЮЛ временно не доступен, попробуйте зайти позже.", 0);
                    }
                }
                hideClock();
            }
        , "json");
    } else {
        showwin('warning', '<p align=center>Данный сервис доступен<br/>только зарегистрированным пользователям.<br/>Отдел продаж СКРИН тел.: (495) 787-17-67, e-mail: sales@skrin.ru</p>', 5000);
    }
}
var stat = "-1";
var scanInt;
var txtOgrn;
var txtInn;
var opWin;
function getReport(ogrn, inn, rep_ready) {
    ogrn = ogrn == "null" ? "" : ogrn;
    if (rep_ready == 1) {
        window.open("/egrulgen/reportAll.asp?ogrn=" + ogrn + "&inn=" + inn);
        location.href = "/issuers/" + txtIss + "/";

    } else {
        txtOgrn = ogrn;
        txtInn = inn;
        showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРЮЛ, пожалуйста, подождите</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\"><img src="/images/wait.gif" align="absmiddle"/></div>', 0);
        if (!scanInt)
            scanInt = setInterval(checkReport, 5000);
        checkReport();
    }
}


function checkReport() {
    if (getObj("span_button")) {
        getObj("span_button").value = "Отмена";
    }
    $.get("/egrulgen/checkreportAll.asp", { "ogrn": txtOgrn, "inn": txtInn },
    function (data) {
        stat = data[0].status;
        switch (data[0].status) {
            case "4":
                {
                    if (scanInt)
                        clearInterval(scanInt);
                    errorMessage("Выписка из ЕГРЮЛ по данной компании временно не доступна, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "3":
                {
                    if (scanInt)
                        clearInterval(scanInt);
                    errorMessage("Сервис заказа Выписок из ЕГРЮЛ временно не доступен. Ваша заявка поставлена в очередь, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }

            case "2":
                {
                    if (scanInt)
                        clearInterval(scanInt);
                    errorMessage("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "5":
                {
                    if (scanInt)
                        clearInterval(scanInt);
                    errorMessage("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "1":
                {
                    errorMessage("Формируется Выписка из ЕГРЮЛ, пожалуйста, подождите");
                    break;
                }
            case "0":
                {

                    closeSpan();
                    if (scanInt) {
                        clearInterval(scanInt);
                    }
                    /*$.post("/egrulgen/save_egrul.asp",{"o" : txtOgrn},function(){*/
                    if (!opWin)
                        opWin = window.open("/egrulgen/reportAll.asp?ogrn=" + txtOgrn + "&inn=" + txtInn);
                    location.href = "/issuers/" + txtIss + "/";
                    /*} )*/
                }
        }
    }, "json");
}

function errorMessage(message) {
    $("#lblError").html(message);
}
/*
function cancelReport() {
    if (scanInt)
        clearInterval(scanInt);
    closeSpan();
} */
function remInputs(html) {
    var re = new RegExp("<input\/?[^>]+(>|$)", "ig")
    return html.replace(re, "");
}

/************************************/


/************************************/

function doSearchParams(id, val) {
    if (u > 0) {
        var params;

        switch (id) {
            case 8: {
                params = { "profile": "1", "field": "addr", "val": val };
                break;
            }
            case 17: {
                params = { "profile": "1", "field": "ruler", "val": val };
                break;
            }
            case 13: {
                params = { "profile": "1", "field": "phone", "val": val };
                break;
            }
            case 0:
                {
                    params = { "profile": "1", "field": "comp", "val": val };
                    break;
                }

        }
        var styleStr = 'toolbar=yes,location=yes,directories=no,status=yes,menubar=yes,scrollbars=yes,resizable=yes,copyhistory=no,width=1024,height=768';
        Form2Popup(params, "/dbsearch/dbsearchru/companies/default.asp", "prof_search", styleStr)
    } else {
        showwin('warning', '<p align=center>Поиск по этим реквизитам доступен<br/>только зарегистрированным пользователям.<br/>Отдел продаж СКРИН тел.: (495) 787-17-67, e-mail: sales@skrin.ru</p>', 5000);
    }
}


/*************************************************************/
function goEnglish(iss) {
    var styleStr = 'toolbar=yes,location=yes,directories=no,status=yes,menubar=yes,scrollbars=yes,resizable=yes,copyhistory=no,width=1024,height=768';
    Form2Popup({}, "http://www.skrin.com/issuers/" + iss + "/", "eng", styleStr)
}
/*************************************************************/
function DrawPin() {
    void (0);
}
function switch_rc(id, im) {
    if (jQuery.inArray(id, pin_reports) >= 0) {
        im.src = '/images/chbox.gif'
        im.title = "Включить в отчет";
        pin_reports.splice(jQuery.inArray(id, pin_reports), 1);
    } else {
        im.src = '/images/chbox_c.gif'
        pin_reports[pin_reports.length] = id;
        im.title = "Исключить из отчета";
    }
}
function buildPDF() {
    var r = pin_reports.join(",");
    var i = 1;
    if (r.length > 0) {
        if (getObj("reports")) {
            document.body.removeChild(getObj("reports"))
        }
        $.post("/iss/modules/operations.asp", { "action": 11, "nid": nid, "uid": u, "iss": iss })             //Запись в таблицу ids метки.
        var ifr
        if (!getObj("reports")) {
            ifr = document.createElement("iframe");
            ifr.className = "service_frame"
            ifr.name = "reports"
            ifr.id = "reports"
            ifr.src = "about:blank";
            document.body.appendChild(ifr);
        } else {
            ifr = getObj("reports")
        }
        showClock();
        iframepost({ "iss": iss, "r": r, "uid": u, "nid": nid }, "/iss/report/default.asp", "reports");
        iid = window.setInterval(function () {
            $.post("/iss/modules/operations.asp", { "action": 12, "nid": nid },
                function (data) {
                    if (data[0].done - 1 == 0) {
                        hideClock();
                        window.clearInterval(iid);
                        if (getObj("reports")) {
                            document.body.removeChild(getObj("reports"));
                        }

                    }
                }, "json");
        }, 15000);


    } else {
        showwin("critical", "Выделите необходимы пункты меню для включения в отчет", 2000);
    }

}

function doExcell() {
    var ifr
    if (can_xls) {
        showClock();
        if (xls_params["module"]) {
            if (!getObj("reports")) {
                ifr = document.createElement("iframe");
                ifr.className = "service_frame"
                ifr.name = "reports"
                ifr.id = "reports"
                ifr.src = "about:blank";
                document.body.appendChild(ifr);
            } else {
                ifr = getObj("reports")
            }
            iframepost(xls_params, "/iss/content/" + xls_params["module"] + "code.asp", "reports");
        } else {
            loadXLS()
        }
        hideClock();
    } else {
        LoginErr(1)
    }
}
/*****************************************************/

var massregInt;

function searchMassReg(iss, user_id) {
    var acc = 0
    $.post("/iss/modules/operations.asp", { "user_id": user_id, action: 18 }, function (data) {
        if (String(data) == "1") {
            txt_ticker = iss;
            waitMassReg();
            massregInt = setInterval(checkMassReg, 5000);
            checkMassReg();
        } else {
            showwin("critical", "Для проверки адресов массовой регистрации по данным ФНС России необходим уровень доступа СКРИН \"Предприятия\".<br/>По вопросам изменения уровня доступа Вы можете связаться с<br/>Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href=/company/support/questions/>Оставить запрос</a>", 0);
        }

    }, "html");


}

var badrInt;
var disfInt;

function ClearInts() {
    if (massregInt)
        clearInterval(massregInt);
    if (badrInt)
        clearInterval(badrInt);
    if (disfInt)
        clearInterval(disfInt);
    if (scanegrlinksInt)
        clearInterval(scanegrlinksInt);
    if (scanlinksInt)
        clearInterval(scanlinksInt);
}
/*******************************************************/
function waitMassReg() {
    showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Осуществляется поиск адресов массовой регистрации, пожалуйста, подождите</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\"><img src="/images/wait.gif" align="absmiddle"/></div>', 0);
}

function get_cur_date() {
    var current_date = new Date();
    var month = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
    var day = current_date.getDate()
    day = (parseInt(day, 10) < 10) ? ('0' + day) : (day);
    return day + "." + month[current_date.getMonth()] + "." + current_date.getFullYear();
}


function checkMassReg() {
    var req = '/egrulgen/proxy_massreg.asp';
    $.get(req, { "iss": txt_ticker, "rnd": Math.random() }, function (data) {
        q_stat = data.QStatus;
        switch (q_stat) {
            case 0:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    var searchResult = data.SearchResult;
                    var origAddress = data.OriginalAddress;
                    //Обрежем исходный адрес до дома
                    var pos = 0;
                    for (var i = 0; i < 7; i++) {
                        pos = origAddress.indexOf(",", pos + 1);
                    }
                    origAddress = origAddress.substring(0, pos);
                    var message_text = "";
                    closeSpan();
                    if (searchResult == "1") {
                        var street = data.Street;
                        var address_row = "";
                        var i_count = 0;
                        $.each(data.Items, function (entryIndex, entry) {
                            var address = "";
                            var b_color = "#f0f0f0";
                            if (i_count % 2 == 0)
                                b_color = "#ffffff";
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
                            address_row += '<tr bgcolor="' + b_color + '"><td style="padding-left:5px">' + street + ' ' + address + '</td><td align="center">' + rcount + '</td></tr>';
                        });



                        message_text = '<br/><span class="minicaption">АДРЕСА МАССОВОЙ РЕГИСТРАЦИИ</span><table width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                        message_text += '<p><span class="bluecaption">Адрес по которому искали: ' + origAddress + '</span></p><hr>';
                        message_text += '<table width="100%" cellspacing="0" cellpadding="0"><tr>';
                        message_text += '<tr><td class="table_caption" style="text-align:center;">Адрес</td><td class="table_caption" style="text-align:center;">Количество ЮЛ</td></tr>';
                        message_text += '<tr><td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer height="1px" width="1px" type="block"></spacer></div></td>';
                        message_text += '<td class="table_shadow"><div style="width: 1px; height: 1px;"><spacer height="1px" width="1px" type="block"></spacer></div></td></tr>';
                        message_text += address_row;
                        message_text += '</table>';
                        ShowTable(message_text);
                    }
                    if (searchResult == "0") {
                        message_text = '<br/><span class="minicaption">АДРЕСА МАССОВОЙ РЕГИСТРАЦИИ</span><table width="100%" cellspacing="0" cellpadding="4" border="0"><tbody><tr></td><td width="50%">Источник данных: ФНС России.</td><td width="50%" align="right">Дата выгрузки результатов поиска:  ' + get_cur_date() + '</td></tr></tbody></table>';
                        message_text += '<p><span class="bluecaption">Адрес по которому искали: ' + origAddress + '</span></p><hr>';
                        message_text += "По адресу не  найдены  записи массовых регистраций";
                        ShowTable(message_text);
                    }

                    break;
                }
            case 1:
                {
                    errorMessage("Осуществляется поиск адресов массовой регистрации, пожалуйста, подождите");
                    break;
                }
            case 2:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Адрес юридического лица не позволяет произвести автоматический поиск в реестре адресов массовой регистрации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 3:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Сервис временно не доступен, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 4:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Адрес юридического лица не позволяет произвести автоматический поиск в реестре адресов массовой регистрации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 5:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Адрес юридического лица не позволяет произвести автоматический поиск в реестре адресов массовой регистрации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 6:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Сервис временно не доступен, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case 7:
                {
                    if (massregInt)
                        clearInterval(massregInt);
                    errorMessage("Адрес юридического лица не позволяет произвести автоматический поиск в реестре адресов массовой регистрации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
        }
    }, "json");
}

function ShowTable(tabletext) {
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;



    inner_html = '<div id="news_text" class="news_text">' + tabletext + '<hr><br>';
    inner_html += '<table width="100%" cellpadding="0" cellspacing="0"><tr><td><span class="data_comment limitation">';
    inner_html += 'Внимание: в связи с особенностями функционирования и обновления указанного источника информации ЗАО "СКРИН" не может гарантировать абсолютную актуальность и достоверность данных.';
    inner_html += '</span></td></tr></table></div>';


    var d;
    if (!getObj("table_dialog_div")) {
        d = document.createElement("div");
        d.id = "table_dialog_div"
        d.style.display = "none";
        d.style.textAlign = "center";

        document.body.appendChild(d);
        //        $("#scan_dialog_div").attr("title","<div style='height:10px;'><a class='ui-dialog-titlebar-print ui-corner-all' href='#' id='prnb' onclick='doPrint(&quot;sc_content&quot;);'role='button' title='Печать'><span class='title_printer'></span></a></div>");
    } else {
        d = getObj("table_dialog_div")
    }

    ww = (scw < 640) ? scw - 44 : 640;
    wh = (sch < 480) ? sch - 30 : 480
    $("#table_dialog_div").dialog({
        autoOpen: false,
        height: wh,
        width: ww,
        modal: false,
        resizable: true,
        draggable: true,
        closeOnEscape: true,
        close: function () {
            void (0);
        }
    });

    d.innerHTML = "<div style=\"background-color:#FFFFFF;text-align:center;border:none;\" id=\"t_content\">" + inner_html + "</div>";
    $("#table_dialog_div").dialog("open");
    $("#table_dialog_div").dialog("option", "title", "<div style='height:10px;'><a class='ui-dialog-titlebar-print ui-corner-all' href='#' id='prnb' onclick='doPrint(&quot;t_content&quot;);'role='button' title='Печать'><span class='title_printer'></span></a></div>");



}

function closetable() {
    var d = document.getElementById("news_div");
    if (d) {
        document.body.removeChild(d);
    }
}
function showonpage(kw, n, ntt, is_bunkrupt, is_event) {
    if (typeof (ntt) == 'undefined') ntt = '';
    $("input:checkbox").each(function (i) {
        if (this.name.length >= 32) {
            this.checked = true;
        }
    });
    showSelected(kw, n, ntt, is_bunkrupt, is_event);
}

/*********************************************************************/
function buildXLS() {
    var r = pin_reports.join(",");
    var i = 1;
    if (r.length > 0) {
        if (getObj("reports")) {
            document.body.removeChild(getObj("reports"))
        }
        $.post("/iss/modules/operations.asp", { "action": 11, "nid": nid, "uid": u, "iss": iss });
        var ifr
        if (!getObj("reports")) {
            ifr = document.createElement("iframe");
            ifr.className = "service_frame"
            ifr.name = "reports"
            ifr.id = "reports"
            ifr.src = "about:blank";
            document.body.appendChild(ifr);
        } else {
            ifr = getObj("reports")
        }
        showClock();

        iframepost({ "iss": iss, "r": r, "uid": u, "tfn": nid + ".xls" }, "/iss/excell/default.asp", "reports");
        iid = setInterval(function () {
            $.post("/iss/modules/operations.asp", { "action": 12, "nid": nid },
                function (data) {
                    if (data[0].done - 1 == 0) {
                        hideClock();
                        clearInterval(iid);
                    }
                }
                  , "json");
        }
            , 3000);

    } else {
        showwin("critical", "Выделите необходимы пункты меню для включения в отчет", 2000);
    }


}
/***************************************************************/
function showFounders(iss) {
    Form2Popup({ "id": iss }, "/iss/visual/index.asp", "visual", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no,width=1280,height=1000");
}
/******************************************************************/
function showDepend(iss) {
    Form2Popup({ "id": iss, "s": 1 }, "/iss/visual/", "visual", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no,width=1280,height=1000");
}
/******************************************************************/
function showVisioSelector(iss, user_id, e) {
    var acc = 0
    showClock()

    if (user_id > 0) {
        $.get("/iss/modules/operations.asp", { "user_id": user_id, action: 18 }, function (data) {
            hideClock()
            if (String(data) == "1") {

                var d = document.createElement("div");
                var bounds;
                bounds = $("#visio").position();
                d.style.top = bounds.top + 6 + $("#visio").height() + "px";
                d.style.left = bounds.left + "px";
                d.className = "egrul_selector";
                d.id = "dvisio";
                document.body.appendChild(d);
                d.innerHTML = "<span id=\"fvisio\" class=\"get_egrul\" onclick=\"showFounders('" + iss + "');\">Визуализация взаимосвязей</span>";
                stopevent(e)
                $("html").bind("click", function (e) {
                    if (getObj("dvisio")) {
                        document.body.removeChild(getObj("dvisio"));
                        $("html").unbind();
                    }
                })

            } else {
                showwin("critical", "Для использования данного сервиса<br/> необходим уровень доступа СКРИН \"Предприятия\".<br/>По вопросам изменения уровня доступа Вы можете связаться с<br/>Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href=/company/support/questions/>Оставить запрос</a>", 0);
            }

        }, "html");


    } else {
        showwin('warning', '<p align=center>Данный сервис доступен<br/>только зарегистрированным пользователям.<br/>Отдел продаж СКРИН тел.: (495) 787-17-67, e-mail: sales@skrin.ru</p>', 5000);
    }




}
/**********************************************************/
function searchStatus(iss, user_id) {
    var acc = 0
    $.post("/iss/modules/operations.asp", { "user_id": user_id, action: 18 }, function (data) {
        if (String(data) == "1") {
            txt_ticker = iss;
            waitStatus();
            statusInt = setInterval(checkStatus, 5000);
            checkStatus();
        } else {
            showwin("critical", "Для проверки статуса предприятия  по ЕГРЮЛ необходим уровень доступа СКРИН \"Предприятия\".<br/>По вопросам изменения уровня доступа Вы можете связаться с<br/>Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href=/company/support/questions/>Оставить запрос</a>", 0);
        }

    }, "html");
}
function waitStatus() {
    showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Осуществляется проверка статуса предприятия, пожалуйста, подождите</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\"><img src="/images/wait.gif" align="absmiddle"/></div>', 0);
}

var statusInt;
function checkStatus() {
    var req = '/egrulgen/checkreportStatus.asp';
    $.get(req, { "iss": txt_ticker, "rnd": Math.random() }, function (data) {
        q_stat = data[0].status;
        switch (q_stat) {
            case "0":
                {
                    closeSpan();
                    if (statusInt)
                        clearInterval(statusInt);
                    location.href = "/issuers/" + txt_ticker + "/";
                    break;
                }
            case "1":
                {
                    errorMessage("Осуществляется проверка статуса предприятия, пожалуйста, подождите");
                    break;
                }
            case "2":
                {
                    if (statusInt)
                        clearInterval(statusInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "3":
                {
                    if (statusInt)
                        clearInterval(statusInt);
                    errorMessage("Сервис временно не доступен, попробуйте зайти позже.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "4":
                {
                    if (statusInt)
                        clearInterval(statusInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
            case "5":
                {
                    if (statusInt)
                        clearInterval(statusInt);
                    errorMessage("Недостаточно информации.");
                    getObj("rotor").innerHTML = ""
                    getObj("span_button").value = "Закрыть"
                    break;
                }
        }
    }, "json");
}
function news_ShowBankrot(ids, ss) {
    showClock();
    if (!ss) {
        var prm = { "b_id": ids, "iss": $("#iss").val() };
    } else {
        var prm = { "b_id": ids, "iss": $("#iss").val(), "ss": ss };


    }
    $.post("/dbsearch/dbsearchru/news/modules/babkrot_selected.asp", prm,
        function (data) {
            hideClock();
            switch (data[0].action) {
                case "0": {
                    $("#dcontent").html(data[0].text);
                    $("#dialog_div").dialog("open");
                    break;
                }

            }
        },
        "json"
    );
}

function news_ShowBankrot1(ids, ss) {
    showClock();
    if (!ss) {
        var prm = { "b_id": ids, "iss": $("#iss").val() };
    } else {
        var prm = { "b_id": ids, "iss": $("#iss").val(), "ss": ss };


    }
    $.post("/dbsearch/dbsearchru/news/modules/babkrot_selected.asp", prm,
        function (data) {
            hideClock();
            switch (data[0].action) {
                case "0": {
                    var content = data[0].text;
                    show_dialog({ "content": content, "is_print": true });
                    break;
                }

            }
        },
        "json"
    );
}

function news_showBankrotSelected() {
    var tmp_str = "";
    var ch_ckd = $("input:checkbox[name='ch_bankrot']:checked").get();
    if (ch_ckd.length == 0) {
        showwin("warning", "Нет ни одного выбранного сообщения.", 2000);
    }
    else {
        var vals;
        var ids = ch_ckd[0].id;
        for (var i = 1; i < ch_ckd.length; i++) {
            ids += ',' + ch_ckd[i].id;
        }
        news_ShowBankrot(ids);
    }
}

function news_showBankrotOnPage() {
    var ch_ckd = $("input:checkbox[name='ch_bankrot']").get();
    if (ch_ckd.length > 0) {
        var vals;
        var ids = ch_ckd[0].id;
        for (var i = 1; i < ch_ckd.length; i++) {
            ids += ',' + ch_ckd[i].id;
        }
        news_ShowBankrot(ids);
    }
}

function doAddMess(user_id) {
    var ids = ""

    $("input:checkbox:checked").each(function () {
        if (this.name == "selsissuer") {
            ids += this.value.split("_")[0] + ",";
        }
    })
    if (ids.length > 0) {
        showClock()
        $.post("/dbsearch/dbsearchru/monitor/operations.asp", { "o": "8", "id": ids },
			function (data) {
			    hideClock();
			    if (data >= 0) {
			        showwin("info", "Добавлено компаний в список монитринга сообщений:" + data, 2000);
			    }
			    if (data < 0) {
			        showwin("critical", "Превышено допустимое количество <br/>компаний в списке мониторинга сообщений", 2000);
			    }
			}
		)
    } else {
        showwin("warning", "Отметьте организации для включения<br/>в список мониторинга сообщений", 2000);
    }
}

/*********************/
function win_confirm(header, message, callback) {
    var html = "<div style='margin-top:20px;'><p>" + message + "</p>";
    html += "<div style='margin-top:30px;text-align:center;'>";
    html += "<button class='btns darkblue' id='btn_confirm_ok'/>OK</button>&nbsp;<button id='btn_confirm_none'  class='btns darkblue'/>Отмена</button></div></div>";
    showwin('info', html, 0, 1);
    $('#btn_confirm_ok').click(function () {
        document.body.removeChild(document.getElementById('span_info'));
        callback(true);
    });
    $('#btn_confirm_none').click(function () {
        document.body.removeChild(document.getElementById('span_info'));
        callback(false);
    })
}


function LoginErr(id) {
    var login_message;
    if (id == 0) {
        login_message = "<div class=\"normal\">Вход в данный раздел разрешен только<br/> авторизированным пользователям системы (подписчикам).<br>" +
          "По вопросам оформления подписки Вы можете связаться<br/> с Отделом продаж и маркетинга<br/> по телефонам <b>(495)787-17-67</b>  или " +
                 "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>Оставить&nbsp;запрос</a></div>";
    }
    if (id == 1) {
        login_message = "<div class=\"normal\">Ваш вариант подписки не дает доступ к выбранному разделу/предприятию.<br/><br/>По вопросам <b>расширения уровня доступа</b> Вы можете связаться с Отделом продаж и маркетинга по телефонам <b>(495) 787-17-67 " +
                                    "</b> или <a href=\"/company/support/\"><img src=\"/images/mnu_icon_question1.gif\" width=\"22\" height=\"22\" border=\"0\" align=\"absMiddle\"></a>&nbsp;<a href=\"/company/support/\">Оставьте&nbsp;запрос</a>" +
                                    ", и специалисты СКРИН сами свяжутся с Вами в ближайшее время.</div>";
    }
    if (id == 2) {
        login_message = "<div class=\"normal\">Данная функция доступна только авторизированным пользователям системы (подписчикам).<br>" +
          "По вопросам оформления подписки Вы можете связаться с Отделом продаж и маркетинга<br/> по телефонам <b>(495)787-17-67</b>  или " +
                 "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>Оставить&nbsp;запрос</a></div>";
    }
    showwin('critical', login_message, 0);
}
function getRTF(path) {
    var ifr = document.createElement("iframe");
    ifr.id = "cframe";
    ifr.name = "cframe";
    ifr.style.display = "none";
    var form = document.createElement("form");
    form.action = path
    form.method = "post";
    form.target = "cframe"
    form.style.display = "none"
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
}
/*Связи ЕГРЮЛ*/

var scanlinksInt;
var linksFirst = true;
function getLinksReport(id, reload) {
    ticker = id

    showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Формируется Отчет по взаимосвязанным лицам.<br/> Пожалуйста, подождите. Формирование отчета может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src="http://www.skrin.ru/images/wait.gif" align="absmiddle"/></div>', 0);
    scanlinksInt = setInterval(function () { checkLinksReport(reload); }, 5000);
    linksFirst = true;
    checkLinksReport(reload);

}



function checkLinksReport(reload) {
    if (!ticker)
        return;
    $.post("/iss/modules/checklinksreport.asp", { "ticker": ticker },
        function (data) {
            stat = data[0].status;

            switch (data[0].status) {

                case "4":
                    {
                        if (scanlinksInt)
                            clearInterval(scanlinksInt);
                        errorMessage("Отчет по взаимосвязанным лицам по данной компании временно не доступен, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        break;
                    }
                case "3":
                    {
                        if (scanlinksInt)
                            clearInterval(scanlinksInt);
                        errorMessage("Сервис отчетов по взаимосвязанным лицам временно не доступен, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        break;
                    }

                case "2":
                    {
                        if (scanlinksInt)
                            clearInterval(scanlinksInt);
                        errorMessage("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                        getObj("rotor").innerHTML = ""
                        break;
                    }
                case "1":
                    {
                        errorMessage("Формируется Отчет по взаимосвязанным лицам.<br/> Пожалуйста, подождите. Формирование отчета может занять несколько минут.");
                        break;
                    }
                case "0":
                    {
                        closeSpan();
                        loadcontent(107);
                        if (scanlinksInt)
                            clearInterval(scanlinksInt);


                    }
            }
        }, "json");
}

var scanegrlinksInt;
var egrlinksFirst = true;
function getEgripLinksReport(inn, fio) {

    showwin('info', '<span id=\"lblError\" style=\"color:#000;\">Формируется Отчет по взаимосвязанным лицам.<br/> Пожалуйста, подождите. Формирование отчета может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src="http://www.skrin.ru/images/wait.gif" align="absmiddle"/></div>', 0);
    scanegrlinksInt = setInterval(function () { checkEgripLinksReport(inn, fio); }, 5000);
    egrlinksFirst = true;
    checkEgripLinksReport(inn, fio);
}



function checkEgripLinksReport(inn, fio) {

    $.post("/iss/modules/checkegriplinksreport.asp", { "inn": inn, "fio": fio },
        function (data) {
            stat = data[0].status;
            if (stat - 5 != 0 && egrlinksFirst) {

                egrlinksFirst = false;

            }
            switch (data[0].status) {

                case "3":
                    {
                        if (scanegrlinksInt)
                            clearInterval(scanegrlinksInt);
                        errorMessage("Отчет по взаимосвязанным лицам  временно не доступен, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "2":
                    {
                        if (scanegrlinksInt)
                            clearInterval(scanegrlinksInt);
                        errorMessage("Отчет по взаимосвязанным лицам  временно не доступен, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "0":
                    {
                        errorMessage("Формируется Отчет по взаимосвязанным лицам.<br/> Пожалуйста, подождите. Формирование отчета может занять несколько минут.");
                        break;
                    }
                case "1":
                    {
                        closeSpan();
                        if (scanegrlinksInt)
                            clearInterval(scanegrlinksInt);

                        loadcontent(108);
                    }
            }
        }, "json");
}
function paging(page, total, proc) {

    var PCount = Math.ceil(total / 20);
    var Page = page;
    var html = "";
    var StartPage
    if (PCount < 8)
        StartPage = 1
    else
        StartPage = ((Page - 3 > 0) ? ((PCount - Page < 3) ? PCount * 1 + (PCount - Page) - 8 : Page - 3) : 1);


    if (Page * 1 > 3 && PCount > 7)
        html += '<td><a href="#" onclick="' + proc + '(' + (StartPage - 1) + ')"><<</a></td>';
    for (var i = StartPage; i < ((Page == PCount) ? PCount * 1 + 1 : ((StartPage + 7 < PCount) ? StartPage + 7 : PCount * 1 + 1)) ; i++) {
        if (i - Page == 0)
            html += '<td>' + i + '-я страница</td>';
        else
            html += '<td><a href="#" onclick="' + proc + '(' + i + ')">' + i + '</a></td>';
    }
    if (i < PCount)
        html += '<td><a href="#" onclick="' + proc + '(' + (Page * 1 + 4) + ')">>></a></td>';
    if (PCount > 7)
        html += '<td>(Всего: <a href="#" id="amover" onclick="MoveTo(' + Page + ',' + PCount + ')">' + PCount + ' страниц</a>)</td>';

    return html
}
function GenPages(page, total, proc) {

    var PCount = Math.ceil(total / 20);
    var Page = page;
    var html = "";
    var StartPage
    if (PCount > 8)
        StartPage = 1
    else
        StartPage = ((Page - 3 > 0) ? ((PCount - Page < 3) ? PCount * 1 + (PCount - Page) - 8 : Page - 3) : 1);


    if (Page * 1 > 3 && PCount > 7)
        html += '<td><a href="#" onclick="' + proc + '(' + (StartPage - 1) + ')"><<</a></td>';
    for (var i = StartPage; i < ((Page == PCount) ? PCount * 1 + 1 : ((StartPage + 7 < PCount) ? StartPage + 7 : PCount * 1 + 1)) ; i++) {
        if (i == Page)
            html += '<td>' + i + '-я страница</td>';
        else
            html += '<td><a href="#" onclick="' + proc + '(' + i + ')">' + i + '</a></td>';
    }
    if (i < PCount)
        html += '<td><a href="#" onclick="' + proc + '(' + (Page * 1 + 4) + ')">>></a></td>';
    if (PCount > 7)
        html += '<td>(Всего: <a href="#" id="amover" onclick="MoveTo(' + Page + ',' + PCount + ')">' + PCount + ' страниц</a>)</td>';

    return html
}