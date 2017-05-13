var c_ogrn, c_inn
function showSeekWin() {
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
    var txt = "<div id=\"dcontent\"><p align=\"center\" style=\"font-weight:700;\">Введите ИНН и/или ОГРН<br/><br/></p><div style=\"float:left;padding-left:15px;font-size:12px;\">ИНН:" +
                "	<input id=\"inn\" class=\"system_form\"  style=\"width:110px;\" type=\"text\" value=\"\" name=\"inn\"/></div>" +
                "<div style=\"float:left;padding-left:15px;font-size:12px;\">ОГРН:" +
                "	<input id=\"ogrn\" class=\"system_form\" style=\"width:110px;\" type=\"text\"  name=\"reg\"/>" +
                "</div>" +
                "<div style=\"float:right;\">" +
                "	<button id=\"btn_search_egrul\" class=\"btns blue\" >Найти</button>" +
                "</div><div id=\"comments\" style=\"margin-top:39px;text-align:center;\"></div></div>"
    $("#modal").html(txt)
    $("#modal").dialog("open");
    //showwin('info', txt, 0);		
    $("#btn_search_egrul").click(function () {
        SeekEGRUL()
    })
}
function SeekEGRUL(inn, ogrn, reload) {
    inn = $('#inn').val();
    ogrn = $('#ogrn').val();
    reload = false;
    $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРЮЛ.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
    $("#btn_search_egrul").addClass("dis").removeClass("blue");
    $("#btn_search_egrul").unbind()
    scanInt = setInterval(function () { checkReportEGRUL(reload); }, 5000);
    isFirst = true;
    c_ogrn = ogrn;
    c_inn = inn
    checkReportEGRUL(reload);
}



function checkReportEGRUL(reload) {

    if (!c_ogrn && !c_inn)
        return;
    $.post("/egrulgen/checkreportEGRUL.asp", { "inn": c_inn, "ogrn": c_ogrn },
        function (data) {

            stat = data[0].status;
            if (stat - 5 != 0 && isFirst) {

                isFirst = false;

            }
            switch (data[0].status) {
                case "5":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        errorMessage("Вами превышен временной лимит на заказ Выписок из ЕГРЮЛ.<br/>" +
                                      "Выписку из ЕГРЮЛ по данной компании Вы сможете заказать через " + data[0].m + " минут " + data[0].s + " секунд.<br/>" +
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
                        errorMessage("Выписка из ЕГРЮЛ по данной компании временно не доступна, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "3":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        errorMessage("Сервис заказа Выписок из ЕГРЮЛ временно не доступен, попробуйте зайти позже.");
                        getObj("rotor").innerHTML = ""
                        //getObj("span_button").value="Закрыть"
                        break;
                    }

                case "2":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        errorMessage("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                        getObj("rotor").innerHTML = ""
                        //$("#comments").html("Сведения о юридическом лице в базе ЕГРЮЛ не найдены.");
                        //restoreButton()
                        //getObj("span_button").value="Закрыть"
                        break;
                    }
                case "1":
                    {

                        $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРЮЛ.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /><div id=\"rotor\" style=\"text-align:center;padding-top:20px;\">	<img src=\"http://www.skrin.ru/images/wait.gif\" align=\"absmiddle\"/></div>");
                        break;
                    }
                case "0":
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        var txt = "<div style=\"text-align:center;width:466px;margin-top:58px;\"><h2 style=\"font-family: Arial,Helvetica,sans-serif;font-size: 14px;margin-bottom:15px;\"'>Выписка из ЕГРЮЛ сформирована</h2>" +
                                "<a class=\"word_ico\" style=\"color: #336699; font-size: 12px; font-weight: bold;margin-top:15px;\" onclick=\"showClock();getRTF('/egrulgen/reportAll.asp?ogrn=" + data[0].ogrn + "'); hideClock(); $('.header_r1').load('/modules/basket_div.asp'); \" href=\"#\">Скачать выписку</a>&nbsp;&nbsp;&nbsp;<div class=\"title\" style=\"z-index:5000; display:inline;\">" +
                                "</div>"
                        $("#comments").html(txt)
                        restoreButton();

                    }
            }
        }, "json");
}

function restoreButton() {

    $("#btn_search_egrul").click(function () {
        SeekEGRUL()
    })
    $("#btn_search_egrul").addClass("blue").removeClass("dis");
}