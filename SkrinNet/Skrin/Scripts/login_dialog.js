function united_login(type) {
    var content = "";
    var mes_type = "";
    if (type == 3) {
        no_rights();
    } else {
        need_login();
    }
    /*switch (type) {
        case 1: //1. Неверный логин для ПР и ПОД/ФТ
            content = "<div class=\"normal\" style=\"text-align:center;\">Неверно указан логин или пароль.<br/>" +
                            "По вопросам доступа Вы можете связаться с отделом продаж по тел.: + 7 (495) 787-17-67 <br/>или " +
	                         "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>оставив&nbsp;запрос</a></div>";
            mes_type = "critical";
            break;
        case 2: //2. Истек срок подписки для ПР и ПОД/ФТ
            content = "<div class=\"normal\" style=\"text-align:center;\">Доступ к сайту заблокирован или истек период подписки.<br/>" +
                            "По вопросам доступа Вы можете связаться с отделом продаж по тел.: + 7 (495) 787-17-67 <br/>или " +
	                         "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>оставив&nbsp;запрос</a></div>";
            mes_type = "critical";
            break;
        case 3: //3. Другой уровень доступа для ПР и ПОД/ФТ
            content = "<div class=\"normal\" style=\"text-align:center;\">Ваш уровень доступа не позволяет просматривать данный раздел.<br/>" +
                            "По вопросам доступа Вы можете связаться с отделом продаж по тел.: + 7 (495) 787-17-67 <br/>или " +
	                         "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>оставив&nbsp;запрос</a></div>";
            mes_type = "critical";
            break;
        case 4: //4. Логин используется для ПР и ПОД/ФТ
            content = "<div class=\"normal\" style=\"text-align:center;\">Данный логин уже используется.<br/>" +
                        "Для входа с данным логином необходимо выйти из системы на компьютере, на котором он уже используется,<br/>" +
                            "или связаться по поводу дополнительного пароля с отделом продаж по тел.:  + 7 (495) 787-17-67 <br/>или " +
	                         "<a href=\'/company/support/\'><img src=/images/mnu_icon_question1.gif width=22 height=22 border=0 align=\'absMiddle\'></a>&nbsp;<a href=\'/company/support/\'>оставив&nbsp;запрос</a></div>";
            mes_type = "critical";
            break;
        case 5://Вход без пароля для ПР и ПОД/ФТ
            content = "<div><h2>Вход для клиентов</h2><div id=\"error_login\" class=\"error_login\"></div><div class=\"form-group\" style=\"width:220px;\">" +
                        "<label for=\"user_login\">Логин</label><input type=\"email\" class=\"form-control\"  id=\"user_login\" placeholder=\"Логин\"></div>" +
                            "<div class=\"form-group\" style=\"width:220px;\"><label for=\"user_password\">Пароль</label><input type=\"password\" class=\"form-control\" id=\"user_password\" placeholder=\"Пароль\"></div>" +
	                         "<div class=\"form-group\" ><button type=\"button\" onClick=\"userLogin()\" class=\"btns blue\">Вход</button></div><div style=\"font-size: 12px;\">По вопросам подписки Вы можете связаться <br/> с отделом продаж  по тел.: + 7 (495) 787-17-67 <br/> или <a href=\"/company/support/\">оставив запрос</a></div></div></div>";
            mes_type = "warning";
    }
    showwin(mes_type, content, 0);*/
}