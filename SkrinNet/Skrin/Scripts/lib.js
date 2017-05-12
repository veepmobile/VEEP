

$(document).ready(function () {

    lib_init();
    tram();
});


(function () {


    window.lib_init = function () {
        $('#errorbox').hide();
        $('#btnLogin').click(function () {
            get_login();
        })

        if (typeof $.fn.popover === 'function') {
            $('.login').popover({
                "content": showLogFrm,
                "container": '#authorization',
                "html": true
            });

            $('.login_info').popover({
                "content": $('#user_hidden_info').html(),
                "container": '#authorization',
                "html": true
            });

            $('#logo').popover({
                "content": $('#skrin_services_html').html(),
                "container": '#logo',
                "html": true
            });

            $('body').on('click', function (e) {
                hide_propover(e);
            });
        }
    };

    window.tram = function () {

        var tramInterval = setInterval(function () {
            $.post("/Authentication/Tramp", null, function (data) {
                if (data != 0) {
                    clearInterval(tramInterval);
                }
            });
        }, 60000)

    };


    var hide_propover = function (e) {
        $('[data-original-title]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    };

    var get_login = function () {
        $('#errorbox').hide();
        var login = $('#login').val();
        var pas = $('#password').val();
        $.post("/Authentication/Login", { login: login, password: pas }, function (data) {
            if (data.id == 0) {
                location.reload();
            } else {
                $('#errorbox').text(data.message).show();
            }
        }, "json");
    };

    var getBodyScrollTop=function() // координата верхнего левого угла (Y) видимой части с учетом скроллинга
    {
        return self.pageYOffset || (document.documentElement && document.documentElement.scrollTop) || (document.body && document.body.scrollTop);
    }

    var getBodyScrollLeft=function()  // координата верхнего левого угла (X) видимой части с учетом скроллинга
    {
        return self.pageXOffset || (document.documentElement && document.documentElement.scrollLeft) || (document.body && document.body.scrollLeft);
    }

    var getClientWidth=function() // ширина видимой части экрана
    {
        return document.compatMode == 'CSS1Compat' && !window.opera ? document.documentElement.clientWidth : document.body.clientWidth;
    }

    var getClientHeight=function()  // высота видимой части экрана
    {
        return document.compatMode == 'CSS1Compat' && !window.opera ? document.documentElement.clientHeight : document.body.clientHeight;
    }

    var generate_login_form = function () {
        var form_text = [];
        form_text.push("<p>Для доступа к информации необходимо авторизоваться:</p>")
        form_text.push("<h2>Вход для подписчиков</h2>");
        form_text.push("<div id=\"error_login\" class=\"error_login\"></div>");
        form_text.push("<div class=\"form-group\"><label for=\"user_login\">Логин</label><input type=\"email\" class=\"form-control\"  id=\"user_login\" placeholder=\"Логин\"></div>");
        form_text.push("<div class=\"form-group\"><label for=\"user_password\">Пароль</label><input type=\"password\" class=\"form-control\"  id=\"user_password\" placeholder=\"Пароль\"></div>");
        form_text.push("<div class=\"form-group\" ><button type=\"button\" onClick=\"userLogin()\" class=\"btns darkblue\">Вход</button></div>");
        form_text.push("<p><a href=\"#\" onClick=\"switch_modal_form('ask')\">Заказать обратный звонок по вопросам оформления подписки.</a></p>");
        return form_text.join('');
    }

    var generate_ask_form = function () {
        var form_text = [];
        form_text.push("<h2>Заказать обратный звонок по вопросам оформления подписки:</h2>");
        form_text.push("<div id=\"error_login\" class=\"error_login\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"text\" class=\"form-control\"  id=\"fio\" placeholder=\"Ф.И.О.\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"text\" class=\"form-control\"  id=\"phone\" placeholder=\"Телефон\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"email\" class=\"form-control\"  id=\"email\" placeholder=\"E-mail\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"text\" class=\"form-control\"  id=\"company\" placeholder=\"Название компании\"></div>");
        form_text.push("<div class=\"form-group\" style=\"margin-top:15px;\" ><button type=\"button\" onClick=\"askUser(event)\" class=\"btns darkblue\">Отправить запрос</button></div>");
        form_text.push("<p><a href=\"#\" onClick=\"switch_modal_form('login')\">Вход для подписчиков</a></p>");
        return form_text.join('');
    }

    var generate_askreg_form = function () {
        var form_text = [];
        form_text.push("<h4>Ваш уровень доступа не позволяет просматривать информацию в данном разделе.</h4>");
        form_text.push("<div id=\"error_login\" class=\"error_login\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"text\" class=\"form-control\"  id=\"fio\" placeholder=\"Ф.И.О.\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"text\" class=\"form-control\"  id=\"phone\" placeholder=\"Телефон\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"email\" class=\"form-control\"  id=\"email\" placeholder=\"E-mail\"></div>");
        form_text.push("<div class=\"form-group\"><input type=\"text\" class=\"form-control\"  id=\"company\" placeholder=\"Название компании\"></div>");
        form_text.push("<div class=\"form-group\" ><button type=\"button\" style=\"margin-top:15px;\" onClick=\"askUser(event)\" class=\"btns darkblue\">Отправить запрос</button></div>");
        return form_text.join('');
    }


    window.askUser = function (event) {


        var $error_login = $("#error_login");
        $error_login.html("");

        var errors = [];
        if ($('#fio').val().trim() == "") {
            errors.push("Пожалуйста, введите Ф.И.О.");
        }
        if ($('#phone').val().trim() == "") {
            errors.push("Пожалуйста, введите номер телефона");
        }

        if (errors.length > 0) {
            $error_login.html(errors.join('<br/>'));
            return;
        }

        var info = {
            "fio": $('#fio').val(),
            "phone": $('#phone').val(),
            "email": $('#email').val(),
            "company": $('#company').val()
        };
        showClock();
        $.post("/Company/AskUser", info, function (data) {
            hideClock();
            $('#modal_dialog_container').html(data);
        });
    }

    window.askBlackIP = function (event) {


        var $error_login = $("#error_login");
        $error_login.html("");

        var errors = [];
        if ($('#email').val().trim() == "") {
            errors.push("Пожалуйста, введите E-mail.");
        }
       
        
        if (errors.length > 0) {
            showwin("critical", errors.join('<br/>'), 0, false);
            return;
        }

        var info = {
            "login": $('#login').val(),
            "company": $('#company').val(),
            "fio": $('#fio').val(),
            "phone": $('#phone').val(),
            "email": $('#email').val(),
            "comment": $('#comment').val()
        };
        showClock();
        $.post("/Company/Block", info, function (data) {
            hideClock();
            $('#form_container').html(data);
        });
    }

    window.showLogFrm = function () {
        return '<div style="width:250px;" class="login_propover"><h2>Вход для клиентов</h2><div id="error_login" class="error_login"></div><div class="form-group" style="width:220px;">'
        + '<label for="user_login">Логин</label>'
        + '<input type="email" class="form-control"  id="user_login" placeholder="Логин">'
        + '</div>'
        + '<div class="form-group" style="width:220px;">'
        + '<label for="user_password">Пароль</label>'
        + '<input type="password" class="form-control" id="user_password" placeholder="Пароль"></div>'
        + '<div class="form-group" ><button type="button" onClick="userLogin()" class="btns darkblue">Вход</button><div style="margin: 10px 0 0;font-size: 12px;text-align: justify;">По вопросам подписки Вы можете связаться с отделом продаж по тел.: +&nbsp;7&nbsp;(495)&nbsp;787-17-67 или <a href="/company/ask/">оставив запрос</a>.</div></div></div></div>';
    };

    window.userLogin = function () {
        $('#error_login').text('');
        var user_login = $('#user_login').val();
        var user_password = $('#user_password').val();
        showClock();
        $.post("/Authentication/Login", { login: user_login, password: user_password },
            function (data) {
                hideClock();
                if (data.id == 0) {
                    location.reload();
                } else {
                    $('#error_login').html(data.message);
                }
            }, "json");
    };

    window.fomat_number = function (for_num) {
        return for_num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    };

    window.show_dialog = function (p) {        
        if ($(".modal-backdrop").length) {
            close_dialog();
        }
        var $placer = p.$placer || $("body"); // контенер для добавления
        var id = p.id || "modal_dialog"; //идентификатор диалогового окна
        var content = p.content || ""; //Выводимое содержимое
        var is_print = p.is_print || false; //Доступна ли печать
        var extra_style = p.extra_style || ""; //Дополнительные стили
        var extra_function = p.extra_function || null; //Дополнительная функция, запускаемая после создания диалогового окна

        var body = '<div id="' + id + '" class="modal fade" tabindex="-1" style="display: none;" aria-hidden="true">'
                    + '<div class="modal-dialog" style='+extra_style+'><div class="modal-content">'
                    + '<div class="modal-header"><button class="close" aria-hidden="true" data-dismiss="modal" type="button">×</button>'
                    + (is_print ? '<button class="btns grey small" onclick="print_dialog(\'' + id + '\',\'\');"><i class="icon-print"></i>Печать</button>' : '')
                    + '</div><div class="modal-body">' + content + '</div>'
                    + '<div class="modal-footer">'
                    + '</div>'
                    + '</div></div></div>';
        //Удалим старое содержимое
        $('#' + id).remove();

        $placer.append(body);

        $('#' + id).modal();

        if (extra_function != null) {
            extra_function();
        }
    };

    window.close_dialog = function () {

        $('.modal').modal('hide');
        $('.modal-backdrop').remove();
    }

    window.confirm_dialog = function (content,success, p) {
        content = content + "<div class=\"confirm-dialog\"><button class=\"btns darkblue\" id=\"btn-confirm-dialog-ok\">Применить</button><button data-dismiss=\"modal\" class=\"btns grey\" >Отмена</button></div>";
        var p = p || {};
        p.content = content;
        if (isFunction(success)) {
            p.extra_function = function () {
                $('#btn-confirm-dialog-ok').click(function () {
                    if (success()) {
                        close_dialog();
                    }
                });
            }
        }
        show_dialog(p);
    };

    window.print_dialog = function (id, title) {
        console.log('print');
        $('#' + id).find('.modal-body').printElement({ pageTitle: title, leaveOpen: false, printMode: 'popup' });
    };

    window.need_login = function () {
        switch_modal_form("ask");
    };

    window.switch_modal_form = function (form_type) {


        var content = "";

        switch (form_type) {
            case "ask":
                content = generate_ask_form();
                break;
            case "login":
                content = generate_login_form();
                break;
            case "ask_reg":
                content = generate_askreg_form();
                break;
        }


        setTimeout(function () {
            show_dialog({
                "content": "<div id=\"modal_dialog_container\">" + content + "</div>",
                "extra_style":"width:530px;",
                "extra_function": function () {
                    $('.modal-content').click(function (event) {
                        console.log(event);
                        if (event.target.className != "close") {
                            event.stopPropagation();
                        }
                    });
                    $("#phone").mask("+7(999) 999-9999");
                }
            });
        }, 30);
        
        //showwin('warning', , 0);
    }

    window.no_rights = function () {
        //Где определена переменная user_id и пользоавтель не аунтифицирован, лучше показывать другое сообщение, иначе сообщение по умолчанию;
        if (window.user_id !== undefined && window.user_id === 0) {
            return need_login();
        }

        switch_modal_form("ask_reg");        
    };

    window.getPictureByFileName = function (file_name, size) {
        var txt;
        var ext;
        var i;
        var alt_info;
        file_name = String(file_name);
        size = String(size) == "undefined" ? "16" : String(size);
        ext = "";
        for (i = file_name.length; i > 0; i--) {
            if (file_name.charAt(i) != ".") {
                ext = file_name.charAt(i) + ext;
            } else {
                break;
            }
        }
        ext = ext.toLowerCase();
        txt = "<img src=/images/";
        switch (ext) {
            case "html":
            case "xml":
            case "htm": {
                alt_info = "Документ HTML(XML)";
                txt += "icon_dochtm_" + size + ".gif";
                break;
            }
            case "doc":
            case "rtf": {
                alt_info = "Документ Word(RTF)";
                txt += "icon_docword_" + size + ".gif";
                break;
            }
            case "xls": {
                alt_info = "Документ Excel";
                txt += "icon_docexel_" + size + ".gif";
                break;
            }
            case "zip": {
                alt_info = "Документ ZIP(SMML)";
                txt += "icon_doczip_" + size + ".gif";
                break;
            }
            case "pdf": {
                alt_info = "Документ PDF";
                txt += "icon_docpdf_" + size + ".gif";
                break;
            }
            case "txt": {
                alt_info = "Документ TXT";
                txt += "icon_doctxt_" + size + ".gif";
                break;
            }
            case "gif":
            case "png":
            case "tif":
            case "jpg": {
                alt_info = "Отсканированная копия (изображение)";
                txt += "icon_docpict_" + size + ".gif";
                break;
            }
            default:
                alt_info = "Неопределенный тип документа";
                txt += "icon_docunknown_" + size + ".gif";
        }
        txt += " width=" + size + " height=" + size + " border=0 alt=\"" + alt_info + "\" align=absmiddle>";
        return txt;
    };

    window.getPictureByFileNameNew = function (file_name, size) {
        var txt;
        var ext;
        var i;
        var alt_info;
        file_name = String(file_name);
        size = String(size) == "undefined" ? "16" : String(size);
        ext = "";
        for (i = file_name.length; i > 0; i--) {
            if (file_name.charAt(i) != ".") {
                ext = file_name.charAt(i) + ext;
            } else {
                break;
            }
        }
        ext = ext.toLowerCase();
        txt = '<span class=\"';
        switch (ext) {
            case "html":
                txt += 'icon-file-doc doc';
                break;
            case "xml":
                txt += 'icon-file-code code';
                break;
            case "htm": {
                alt_info = "Документ XML";
                txt += "icon-file-doc doc";
                break;
            }
            case "doc":
                txt += 'icon-file-word word';
                break;
            case "docx":
                txt += 'icon-file-word word';
                break;
            case "rtf": {
                alt_info = "Документ Word(RTF)";
                txt += "icon-file-word word";
                break;
            }
            case "xls": {
                alt_info = "Документ Excel";
                txt += "icon-file-excel excel";
                break;
            }
            case "xlsx":
                txt += 'icon-file-excel excel';
                break;
            case "zip": {
                alt_info = "Документ ZIP(SMML)";
                txt += "icon-file-archive archive";
                break;
            }
            case "rar":
                txt += 'icon-file-archive archive';
                break;
            case "pdf": {
                alt_info = "Документ PDF";
                txt += "icon-file-pdf pdf";
                break;
            }
            case "txt": {
                alt_info = "Документ TXT";
                txt += "icon-doc doc";
                break;
            }
            case "gif":
            case "png":
            case "tif":
                txt += 'icon-file-image image';
                break;
            case "jpg": {
                alt_info = "Отсканированная копия (изображение)";
                txt += "icon-file-image image";
                break;
            }
            default:
                alt_info = "Неопределенный тип документа";
                txt += "icon_docunknown_" + size + ".gif";
        }
        txt += " d_icon\"></span>";
        return txt;
    };

    window.showClock = function () {
        if (!window.clock_show) {
            window.clock_show = true;
            setTimeout(function () {
                if (window.clock_show) {
                    if (getObj("clock_div")) {
                        //            document.body.removeChild(getObj("clock_div"));
                        $('#clock_div').remove();
                    }
                    var d = document.createElement("div");
                    d.id = "clock_div";
                    d.style.left = Math.floor(getClientWidth() / 2 + getBodyScrollLeft()) + "px";
                    d.style.top = Math.floor(getClientHeight() / 2 + getBodyScrollTop()) + "px";
                    d.style.zIndex = "1000";
                    document.body.appendChild(d);
                }
            }, 300);
        }
    };

    window.showContentClock = function (selector) {
        hideClock();

        var $element = $(selector);

        var left = $element.width() / 2;
        var top = $element.height() / 2;

        var d = $('<div id="clock_div">').offset({ top: top, left: left }).css("z-index", 10000);

        $element.append(d);
    };

    window.hideClock = function () {
       /* if (getObj("clock_div")) {
            document.body.removeChild(getObj("clock_div"));
        }*/
        $('#clock_div').remove();
        window.clock_show = null;
    };


    window.getObj = function (id) {
        return document.getElementById(id);
    };

    // формирует массив из пересекающихся элементов двух других массивов
    window.intersect_array = function (a, b) {
        var ai = 0, bi = 0;
        var result = [];

        while (ai < a.length && bi < b.length) {
            if (a[ai] < b[bi]) { ai++; }
            else if (a[ai] > b[bi]) { bi++; }
            else /* they're equal */
            {
                result.push(a[ai]);
                ai++;
                bi++;
            }
        }

        return result;
    }

  /*  
    window.showwin = function (type, text, timeout, no_close_button) {
        console.log("works");
        var p = {};
        p.content = text;
        p.prevent_closing = true;
        if (!no_close_button) {
            no_close_button = 0
        }
        if (timeout == 0 && no_close_button==0) {
            p.content += "<div class=\"confirm-dialog\"><button class=\"btns darkblue\" data-dismiss=\"modal\">Закрыть</button></div>";
        }
        show_dialog(p);
        
        if (timeout > 0 && $(".modal-backdrop").length) {
            window.setTimeout(function () {
                console.log("close after " + timeout);
                close_dialog(true);
            }, timeout);
        }
        
    }
 */   
    //window.showwin = function (type, text, timeout, no_close_button) {
    window.showwin = function (type, p1, p2, p3, p4) {

        var timeout = 0;
        var text = '';
        var no_close_button = 0;

        //p2 может быть как текстом (если есть header) так и таймаутом
        if ($.isNumeric(p2)) {
            text = p1;
            timeout = p2;
            no_close_button = p3;
        } else {
            text = p2;
            timeout = p3;
            no_close_button = p4;
        }

        $('<div class="dialog_backdrop"></div>').appendTo('body');
        var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
        var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
        var btn_class = "darkblue"
        if (!no_close_button) {
            no_close_button = 0
        }
        if (getObj("span_info")) {
            document.body.removeChild(document.getElementById("span_info"));
        }
        /*
        if (type == "critical" || type == "warning") {
            btn_class = "red";
        }
        */
        var s = document.createElement("span");
        s.className = "span_info " ;
        s.innerHTML = text;
        if (timeout == 0 && no_close_button == 0) {
            s.innerHTML += "<br/><br/><div align=\"center\"><input type=\"button\" id=\"span_button\" value=\"Закрыть\" class=\"btns " + btn_class + "\" onclick=\"closeSpan()\" onkeydown=\"closeSpan()\"/></div>"
        }
        s.innerHTML += "<br/>";
        s.style.display = "block";
        s.style.zIndex = "5000";
        s.id = "span_info"
        document.body.appendChild(s);
        if (timeout == 0 && no_close_button == 0) {
            getObj("span_button").focus();
        }
        s.style.left = Math.floor(getClientWidth() / 2 + getBodyScrollLeft() - $(s).width() / 2) + "px";
        s.style.top = Math.floor(getClientHeight() / 2 + getBodyScrollTop() - $(s).height() / 2) + "px";

        if (timeout > 0 && getObj("span_info")) {
            window.setTimeout("closeSpan()", timeout);
        }
}

    /************************************/
    window.closeSpan = function () {
        if (getObj("span_info")) {
            document.body.removeChild(getObj("span_info"))
        }
        $('.dialog_backdrop').remove();
        /* потом раскоментить
        if (scanInt)
            clearInterval(scanInt);
        if (statusInt)
            clearInterval(statusInt);
        if (massregInt)
            clearInterval(massregInt);
        ClearInts();
        */
    };

    window.stopEvent = function (e) {
        if (window.event) {
            e = window.event;
        }
        if (e.stopPropagation) {
            e.stopPropagation();
        } else {
            e.cancelBubble = true;
        }
    };

    window.clearISS = function (ticker) {
        var re = new RegExp("[a-z0-9!]*", "ig");
        ticker = String(re.exec(ticker));
        ticker = (ticker.length == 0 || ticker == "undefined") ? "" : ticker;
        return ticker;
    };

    window.clearLists = function (val) {
        var re = new RegExp("[0-9\.,]*", "ig");
        val = TrimStr(String(re.exec(val)));
        return val;
    };

    window.clearStr = function (str) {
        var re=new RegExp("[А-яA-z0-9!\.]*","ig");
        str=re.exec(str);
        return str;
    };

    window.trimStr = function(s) {
        s = s.replace( /^\s+/g, '');
        return s.replace( /\s+$/g, '');
    };


    window.isFunction = function(functionToCheck) {
        var getType = {};
        return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
    }


    window.lock_button = function (selector) {
        var $el = $(selector);
        if (!$el.hasClass("disabled")) {
            var inner = $el.html();
            $el.addClass("disabled").html('<span class="icon-spin3 animate-spin"></span>' + inner);
        }
    }

    window.unlock_button = function (selector) {
        $(selector + " .icon-spin3").remove();
        $(selector).removeClass("disabled");
    }
    

    window.CookieUtil = {

        get: function (name) {
            var cookieName = encodeURIComponent(name) + "=",
                cookieStart = document.cookie.indexOf(cookieName),
                cookieValue = null,
                cookieEnd;

            if (cookieStart > -1) {
                cookieEnd = document.cookie.indexOf(";", cookieStart);
                if (cookieEnd == -1) {
                    cookieEnd = document.cookie.length;
                }
                cookieValue = decodeURIComponent(document.cookie.substring(cookieStart + cookieName.length, cookieEnd));
            }

            return cookieValue;
        },

        set: function (name, value, expires, path, domain, secure) {
            var cookieText = encodeURIComponent(name) + "=" + encodeURIComponent(value);

            if (expires instanceof Date) {
                cookieText += "; expires=" + expires.toGMTString();
            }

            if (path) {
                cookieText += "; path=" + path;
            }

            if (domain) {
                cookieText += "; domain=" + domain;
            }

            if (secure) {
                cookieText += "; secure";
            }

            document.cookie = cookieText;
        },

        unset: function (name, path, domain, secure) {
            this.set(name, "", new Date(0), path, domain, secure);
        }

    };

    window.DateObj = function (date) {
        date = date || new Date();
        this.dd = String(date.getDate());
        this.mm = String(date.getMonth() + 1);
        this.yyyy = String(date.getFullYear());

        if (Number(this.dd) < 10) {
            this.dd = '0' + this.dd
        }

        if (Number(this.mm) < 10) {
            this.mm = '0' + this.mm
        }

    }

})();

/**
 * Emulate FormData for some browsers
 * MIT License
 * (c) 2010 François de Metz
 */
(function (w) {
    if (w.FormData)
        return;
    function FormData() {
        this.fake = true;
        this.boundary = "--------FormData" + Math.random();
        this._fields = [];
    }
    FormData.prototype.append = function (key, value) {
        this._fields.push([key, value]);
    }
    FormData.prototype.toString = function () {
        var boundary = this.boundary;
        var body = "";
        this._fields.forEach(function (field) {
            body += "--" + boundary + "\r\n";
            // file upload
            if (field[1].name) {
                var file = field[1];
                body += "Content-Disposition: form-data; name=\"" + field[0] + "\"; filename=\"" + file.name + "\"\r\n";
                body += "Content-Type: " + file.type + "\r\n\r\n";
                body += file.getAsBinary() + "\r\n";
            } else {
                body += "Content-Disposition: form-data; name=\"" + field[0] + "\";\r\n\r\n";
                body += field[1] + "\r\n";
            }
        });
        body += "--" + boundary + "--";
        return body;
    }
    w.FormData = FormData;
})(window);

