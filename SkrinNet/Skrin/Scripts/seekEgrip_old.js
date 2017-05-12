var c_ogrn, c_inn
function showSeekWinIP() {
    var d = document.createElement("div")
    d.id = "modal";
    document.body.appendChild(d);
    var sch = window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;
    var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
    var ww, wh, top, left;
    ww = (scw < 480) ? scw - 44 : 480;
    wh = (sch < 350) ? sch - 30 : 350
    $("#modal").dialog({
        autoOpen: false,
        height: 350,
        width: 480,
        modal: true,
        resizable: false,
        draggable: true,
        closeOnEscape: true,
        close: function () {
            void (0);
        }
    });
    var txt = "<div id=\"dcontent\"><p align=\"center\" style=\"font-weight:700;\">Введите ИНН и/или ОГРНИП<br/><br/></p><div style=\"float:left;padding-left:15px;font-size:12px;\">ИНН:" +
                "	<input id=\"inn\" class=\"system_form\"  style=\"width:110px;\" type=\"text\" value=\"\" name=\"inn\"/></div>" +
                "<div style=\"float:left;padding-left:15px;font-size:12px;\">ОГРНИП:" +
                "	<input id=\"ogrn\" class=\"system_form\" style=\"width:110px;\" type=\"text\"  name=\"reg\"/>" +
                "</div>" +
                "<div style=\"float:right;\">" +
                "	<button id=\"btn_search_egrul\" class=\"btns blue\" >Найти</button>" +
                "</div><div id=\"comments\" style=\"margin-top:39px;text-align:center;\"></div></div>"
    $("#modal").html(txt)
    $("#modal").dialog("open");
    //showwin('info', txt, 0);		
    $("#btn_search_egrul").click(function () {
        SeekEGRIP()
    })
}

function SeekEGRIP(inn, ogrn, reload) {
    inn = $('#inn').val();
    ogrn = $('#ogrn').val();
    reload = false;
    $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
    $("#btn_search_egrul").addClass("dis").removeClass("blue");
    $("#btn_search_egrul").unbind()
    scanInt = setInterval(function () { checkReportEGRIP(reload); }, 5000);
    isFirst = true;
    c_ogrn = ogrn;
    c_inn = inn
    checkReportEGRIP(reload);
}

function checkReportEGRIP(reload) {

    if (!c_ogrn && !c_inn)
        return;
    $.post("/egrulgen/checkreportEgrip.asp", { "inn": c_inn, "ogrnip": c_ogrn },
        function (data) {

            stat = data[0].status;
            if (stat - 5 != 0 && isFirst) {

                isFirst = false;
                //$.post("/modules/cnt_report.asp", { "rid": "20" }, function (data) {
                //    $(".header_r1").load("/modules/basket_div.asp");
                //}, "html"
                //)
            }
            switch (data[0].status) {
                case "5":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        errorMessage("Вами превышен временной лимит на заказ Выписок из ЕГРИП.<br/>" +
                                      "Выписку из ЕГРИП по данному индивидуальному предпринимателю Вы сможете заказать через " + data[0].m + " минут " + data[0].s + " секунд.<br/>" +
                                      "По всем вопросам, связанным с использованием сервиса СКРИН \"Контрагент\", Вы можете связаться с<br/>" +
                                      "Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href='http://www.skrin.ru/company/support/questions/' target='_blank'>Оставить запрос</a>");
                        if (getObj("rotor")) {
                            getObj("rotor").innerHTML = ""
                        }



                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "4":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        errorMessage("Выписка из ЕГРИП по данному индивидуальному предпринимателю временно не доступна, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "3":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        errorMessage("Сервис заказа Выписок из ЕГРИП временно не доступен, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        //getObj("span_button").value="Закрыть"
                        break;
                    }

                case "2":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        /*if (getObj("windiv")) {
                            $("#comments").html("Сведения о юридическом лице в базе ЕГРИП не найдены.");
                        }*/
                        errorMessage("Сведения об ИП в базе ЕГРИП не найдены.");
                        getObj("rotor").innerHTML = ""
                        restoreButton()
                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "1":
                    {
                        $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
                        break;

                        /*  if (getObj("windiv")) {
                              $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
                          } else {
                              errorMessage("Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.<br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
                          }
                          break;*/
                    }
                case "0":
                    {
                        /* if (getObj('windiv')) {
                             document.body.removeChild(document.getElementById('windiv'))
                         }*/
                        if (scanInt)
                            clearInterval(scanInt);
                        var txt = "<div style=\"text-align:center;width:466px;margin-top:58px;\"><h2 style=\"font-family: Arial,Helvetica,sans-serif;font-size: 14px;margin-bottom:15px;\"'>Выписка из ЕГРИП сформирована</h2>" +
                                "<a class=\"word_ico\" style=\"color: #336699; font-size: 12px; font-weight: bold;margin-top:15px;\" onclick=\"showClock();getRTF('/egrulgen/reportEgrip.asp?ogrnip=" + c_ogrn + "&inn=" + c_inn + "'); hideClock(); $('.header_r1').load('/modules/basket_div.asp'); \" href=\"#\">Скачать выписку</a>&nbsp;&nbsp;&nbsp;<div class=\"title\" style=\"z-index:5000; display:inline;\">" +
                                "</div>"
                        $("#comments").html(txt)
                        restoreButton();

                        /*
                        if (scanInt)
                            clearInterval(scanInt);
                        var txt = "<div style=\"text-align:center;width:705px;margin-top:58px;\"><h2 style=\"font-family: Arial,Helvetica,sans-serif;font-size: 14px;margin-bottom:15px;\"'>Выписка из ЕГРИП сформирована</h2>" +
                                "<a class=\"word_ico\" style=\"color: #336699; font-size: 12px; font-weight: bold;margin-top:15px;\" onclick=\"showClock();getRTF('/web_reports/RTF/egrip_seek.asp?ogrn=" + data[0].ogrn + "'); hideClock(); $('.header_r1').load('/modules/basket_div.asp'); \" href=\"#\">Скачать выписку</a>&nbsp;&nbsp;&nbsp;<div class=\"title\" style=\"z-index:5000; display:inline;\">" +
                                "</div>"
                        $("#comments").html(txt)
                        restoreButton();
                        */
                    }
            }
        }, "json");
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

/*
// старый
function _showSeekWinIP() {
    var txt = "<div style=\"float:left;margin:3px 0 5px 0;padding-left:15px;\">ИНН:</div>" +
                "<div class=\"content_in2_top2l\" style=\"width:240px;\">" +
                "	<input id=\"inn\" class=\"fill02\"  style=\"width:220px;\" type=\"text\" value=\"\" name=\"inn\"/>" +
                "	<input class=\"cancel_but\" type=\"button\" onclick=\"$('#inn').val('');\" value=\"\"/>" +
                "</div>" +
                "<div style=\"float:left;margin:3px 0 5px 0;padding-left:15px;\">ОГРН:</div>" +
                "<div id=\"sss\" class=\"content_in2_top2l\"  style=\"width:240px;\">" +
                "	<input id=\"ogrn\" class=\"fill02\" style=\"width:220px;\" type=\"text\"  name=\"reg\"/>" +
                "	<input class=\"cancel_but\" type=\"button\" onclick=\"$('#ogrn').val('');\" value=\"\"/>" +
                "</div>" +
                "<div style=\"float:right;\">" +
                "	<button id=\"btn_search_egrul\" class=\"but1\" >Найти >></button>" +
                "</div><div id=\"comments\" style=\"margin-top:39px;\"></div>"
    showwin('info', 'Введите ИНН и/или ОГРНИП', txt, 0);
    $("#btn_search_egrul").click(function () {
        SeekEGRIP()
    })
}

//старый
function _SeekEGRIP(inn, ogrn, reload) {
    if (!($('#ogrn').val().length > 15 || $('#inn').val().length > 12)) {
        inn = $('#inn').val();
        ogrn = $('#ogrn').val();
        reload = false;
        if (getObj("amount")) {
            if (Number($("#amount").html().split("/")[1]) > 0) {
                if (getObj("windiv")) {
                    $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
                    $("#btn_search_egrul").addClass("but2").removeClass("but1");
                    $("#btn_search_egrul").unbind()
                } else {
                    showwin('info', 'Формирование отчета', '<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>', 0);
                }
                scanInt = setInterval(function () { checkReportEGRIP(reload); }, 5000);
                isFirst = true;
                c_ogrn = ogrn;
                c_inn = inn
                checkReportEGRIP(reload);
            } else {

                showwin("critical", "Документ недоступен", "Вами превышен дневной лимит на заказ Выписок из ЕГРИП.<br/>" +
                                        "Выписку из ЕГРЮЛ/ЕГРИП по данному индивидуальному предпринимателю/данному ИП Вы сможете заказать завтра.<br/>" +
                                        "По всем вопросам, связанным с использованием Интернет-магазина СКРИН, Вы можете связаться с <br/>" +
                                        "Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href='http://www.skrin.ru/company/support/questions/' target='_blank'>Оставить запрос</a>", 0);

            }
        } else {
            if (getObj("windiv")) {
                $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
            } else {
                showwin('info', 'Формирование отчета', '<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>', 0);
            }
            scanInt = setInterval(function () { checkReportEGRIP(reload); }, 5000);
            isFirst = true;
            checkReportEGRIP(reload);
        }
    } else {
        showwin("critical", "Ошибка", "ОГРНИП или ИНН введен неверно.", 0)
    }
}

*/


function restoreButton() {

    $("#btn_search_egrul").click(function () {
        SeekEGRIP()
    })
    $("#btn_search_egrul").addClass("but1").removeClass("but2");
}