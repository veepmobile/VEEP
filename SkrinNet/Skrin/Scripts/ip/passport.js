(function () {

    window.SeekPassport = function () {
        if (user_id == 0) {
            no_rights();
            return;
        }
        var num = $('#num').val();
        $.get("/Message/ShowPassport/", { "num": num }, function (data) {

            $("#t_content").html(data);
        });
    }

})();



