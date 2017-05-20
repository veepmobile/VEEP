(function () {


    $(document).ready(function () {
        $('#btnSend').click(function () {
            check_ask();
        })
    });

    var check_ask = function () {
        var msg = [];
        var info = {};
        var Re = new RegExp("([A-Za-z0-9][A-Za-z0-9-_]*)(.[A-Za-z0-9-_]*)*@([A-Za-z0-9][A-Za-z0-9-_]*).[A-Za-z0-9-_]+(.[A-Za-z0-9-_]*)*$", "ig");
        var Tel = new RegExp("[0-9-_]{1,16}");

        var theme = $('#theme').val();

        info.company = $('#company').val();
        info.fio = $('#fio').val();
        info.phone = $('#phone').val();
        info.email = $('#email').val();
        info.comment = $('#comment').val();

        if (info.fio.length == 0) {
            msg.push("Пожалуйста, заполните поле \"Ф.И.О.\".");
        }

        if (info.email.length == 0 && info.phone.length == 0) {
            msg.push("Надо заполнить хотя бы одно поле из \"Телефон\" и \"E-mail\".");
        }

        if (info.email.length != 0 && info.email.replace(Re, "#") != "#") {
            msg.push("Вы неправильно ввели свой e-mail.");
        }

        if (msg.length > 0) {
            show_dialog({ "content": msg.join("<br/>"), "is_print": false });
        } else {
            $.post("/Annonce/FeedbackSend/", {
                feedback: JSON.stringify({
                    info: info,
                    theme: theme
                })
            }, function (data) {
                show_dialog({ "content": data, "is_print": false });
            })
        }
    }

})();