var c_ogrn, c_inn
function showSeekWinIP() {
    if (user_id > 0) {
    var htm = "<div id=\"egrip_dialog\" style=\"padding-left:30px;\"><h3>Введите ИНН и/или ОГРНИП</h3><div class=\"form-group\"><span style=\"margin-left:5px;margin-top:5px;width:50px;\">ИНН:</span><input type=\"text\" class=\"search_input\" style=\"width:150px;margin-left:5px;\" id=\"inn\" /><span style=\"margin-left:10px;margin-top:5px;width:70px;\">ОГРНИП:</span><input type=\"text\" class=\"search_input\" style=\"width:150px;margin-left:5px;\" id=\"ogrn\" /><button id=\"btn_search_egrip\" class=\"btns darkblue large\" style=\"margin-left:5px;\" onclick=\"SeekEGRIP();\">Найти</button></div><div id=\"comments\" style=\"margin-top:39px;text-align:center;\"></div></div>";

    htm += "<div style='text-align:center;margin:20px 0;'></div></div>";

    show_dialog({ "content": htm, "is_print": false,"extra_style":"width:700px;" });
    $("#egrip_dialog").click(function (event) {
        event.stopPropagation();
    });
    }
    else {
        no_rights();
        return;
    }
}

function SeekEGRUL(inn, ogrn, reload) {
    inn = $('#inn').val();
    ogrn = $('#ogrn').val();
    reload = false;
    $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br />");
    $("#btn_search_egrip").addClass("disabled").removeClass("darkblue");
    $("#btn_search_egrip").unbind();
    showClock();
    scanInt = setInterval(function () { checkReportEGRUL(reload); }, 5000);
    isFirst = true;
    c_ogrn = ogrn;
    c_inn = inn
    checkReportEGRUL(reload);
}

function SeekEGRIP(inn, ogrn, reload) {
    inn = $('#inn').val();
    ogrn = $('#ogrn').val();
    reload = false;
    $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br /></div>");
    $("#btn_search_egrip").addClass("disabled").removeClass("darkblue");
    $("#btn_search_egrip").unbind();
    showClock();
    scanInt = setInterval(function () { checkReportEGRIP(reload); }, 5000);
    isFirst = true;
    c_ogrn = ogrn;
    c_inn = inn
    checkReportEGRIP(reload);
}


function checkReportEGRIP(reload) {

    if (!c_ogrn && !c_inn)
        return;
    $.post("/Report/AskEgrip/", { "inn": c_inn, "ogrnip": c_ogrn },
        function (data) {
            stat = data.status;
            if (stat - 5 != 0 && isFirst) {
                isFirst = false;
            }
            switch (data.status) {
                case 5:
                    {
                        /*
                        if (scanInt)
                            clearInterval(scanInt);
                        showwin("critical", "Документ недоступен", "Вами превышен временной лимит на заказ Выписок из ЕГРИП.<br/>" +
                        "Выписку из ЕГРЮЛ по данной компании Вы сможете заказать через " + data.m + " минут " + data.s + " секунд.<br/>" +
                        "По всем вопросам, связанным с использованием сервиса СКРИН \"Контрагент\", Вы можете связаться с<br/>" +
                        "Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href='http://www.skrin.ru/company/support/questions/' target='_blank'>Оставить запрос</a>", 0);
                        hideClock();*/
                        break;
                    }
                case 4:
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        showwin("critical", "Документ недоступен", "Выписка из ЕГРИП по данному индивидуальному предпринимателю временно не доступна, попробуйте зайти позже.", 0);
                        hideClock();
                        break;
                    }
                case 3:
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        showwin("critical", "Документ недоступен", "Сервис заказа Выписок из ЕГРИП временно не доступен, попробуйте зайти позже.", 0);
                        hideClock();
                        break;
                    }

                case 2:
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        showwin("critical", "Документ недоступен", "Сведения об ИП в базе ЕГРИП не найдены.", 0);
                        hideClock();
                        break;
                    }
                case 1:
                    {

                        $("#comments").html("<span id=\"lblError\" style=\"color:#000;\">Формируется Выписка из ЕГРИП.<br/> Пожалуйста, подождите. Формирование выписки может занять несколько минут.</span><br />");
                        break;
                    }
                case 0:
                    {
                        if (scanInt)
                            clearInterval(scanInt);
                        var txt = "<div style=\"text-align:center;margin-top:50px;\"><h4 style=\"font-family: Arial,Helvetica,sans-serif;font-size: 16px;margin-bottom:20px;\"'>Выписка из ЕГРИП сформирована</h4>" +
                                "<a style=\"color: #344f77; font-size: 16px; font-weight: bold;margin-top:15px;\" onclick=\"showClock();getRTF('/Report/Egrip/" + data.ogrnip + "'); hideClock(); \" href=\"#\">Скачать выписку</a>" +
                                "</div>";
                        hideClock();
                        $("#comments").html(txt);
                        restoreButton();

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



function restoreButton() {

    $("#btn_search_egrip").click(function () {
        SeekEGRIP()
    })
    $("#btn_search_egrip").addClass("darkblue").removeClass("disabled");
}