$().ready(function () {
    app_desctop_init();
});

(function () {

    this.app_desctop_init = function () {
        $('.report_list').on("click", function (e) {
            var iss = $(this).attr("iss");
            open_report(iss, e);
            $('#droplist').hide();
            e.stopPropagation();
        });
    };

    this.ToggleChB = function (obj, e) {
        if (e.target.nodeName != "INPUT") {
            if (!$(obj).children('input:checkbox').attr('disabled')) {
                if ($(obj).children('input:checkbox').prop("checked")) {
                    $(obj).children('input:checkbox').prop("checked", false)
                } else {
                    $(obj).children('input:checkbox').prop("checked", true);
                }
            }
        }
        stopEvent(e)
    }


    this.GetPDFReport = function (iss, cid) {
        var addr = String(location.href).split("/")[2]
        var choice = "";

        var i = 0
        $(".rchoice").each(
            function () {

                if (i > 0) {
                    if ($(this).prop("checked")) {
                        choice += "1";
                    } else {
                        choice += "0";
                    }
                    $(this).removeAttr("checked");
                }
                i++;
            }
            )
        var path = "/pdf_fr/?iss=" + iss + "&us_id=" + cid + "&choice=" + choice + "&source=1&url=" + addr

        showClock();

        $.post("/pdf_fr/", { "iss": iss, "us_id": cid, "choice": choice, "source": "1", "url": addr }, function (id) {
            if (id.length == 32) {
                LoadReport(id);
                showReports(iss)
                hideClock();
            } else {
                showwin('critical', 'При формировании отчета произошла ошибка. Попробуйте немного позже или обратитесь к вашему менеджеру', 2000);
                hideClock();
            }
        })
    }
    this.doDelReport = function (id) {
        $("#Z" + id).remove();
        $.post("/Operation/DelURep", { "id": id });
        var i = 0;
        $('#report_body .rep_arc').each(function () {
            if (i == 0) {
                $(this).attr("val", "0").show();
            }
            i++
        });

        if (i < 2) {

            $("#report_togler").parent().hide();
        }
    }
    this.LoadReport = function (id) {
        var ifr = document.createElement("iframe");
        ifr.id = "cframe";
        ifr.name = "cframe";
        ifr.style.display = "none";
        var form = document.createElement("form");
        form.action = "/Operation/LoadReport/";
        form.method = "post";
        form.target = "cframe"
        form.style.display = "none"
        element = document.createElement("input");
        element.type = "text";
        element.name = "id";
        element.value = id;
        form.appendChild(element)
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);

    }


    function showReports(iss) {
        $.post("/Operation/ShowReports", { "iss": iss }, function (data) {
            //$(".rep_arc").remove();
            $("#report_body .rep_arc").remove();
            $(".rep_arc1").remove();
            $("#report_body").append(data);
        });


    }
    this.showReps = function () {
        if ($("#report_togler").hasClass("icon-angle-down")) {
            $('#rlist').find('tr').each(
                function () {
                    $(this).show();
                }
           );
            $("#report_togler").removeClass("icon-angle-down").addClass("icon-angle-up").html("Свернуть")
        } else {

            $('#rlist').find("tr[val='1']").each(
                function () {
                    $(this).hide();

                }
           );
            $("#report_togler").removeClass("icon-angle-up").addClass("icon-angle-down").html("Архив отчетов")
        }
    };

    this.showEReps = function () {

        if ($("#report_toglerE").hasClass("icon-angle-down")) {
            $('#egrul_body').find('tr').each(
                function () {
                    $(this).show();
                }
            );
            $("#report_toglerE").removeClass("icon-angle-down").addClass("icon-angle-up").html("Свернуть")
        } else {

            $('#egrul_body').find("tr[val='1']").each(
                function () {
                    $(this).hide();

                }
            );
            $("#report_toglerE").removeClass("icon-angle-up").addClass("icon-angle-down").html("Развернуть архив")
        }
    };

    this.getRTF = function (path) {
        var form = document.createElement("form");
        form.action = path
        form.method = "post";
        form.target = "cframe"
        form.style.display = "none"
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    }

    this.checkEgrulPDF = function (e, ogrn) {
        e.stopPropagation();
        var job_id = "EgrulPDF" + "_" + ogrn;
        //<-- debug
        //        $('#top_egrul>span:first').removeClass('new').width("28px").height("28px");
        //        $('#top_egrul').off('click');
        //        push_job(job_id, "Выписка из ЕГРЮЛ", "top_egrul");
        // debug -->

        $.post("/Operation/CheckReportLimit/", { "report_type": 1, "report_code": ogrn }, function (data) {
            if (data.DayLimit > 0 && data.MonthLimit) {
                //$('#top_egrul>span:first').removeClass('icon-download').addClass('icon-spin3').addClass('animate-spin');
                $('#top_egrul>span:first').removeClass('new').width("28px").height("28px");
                $('#top_egrul').off('click');
                push_job(job_id, "Выписка из ЕГРЮЛ");
                var chEgrulPDF = setInterval(function () {
                    $.post("/Report/AskEgrulPdf/", { "ogrn": ogrn }, function (data) {
                        if (data.status == 2) {
                            $("#top_egrul").remove();
                            if (getObj("ep1")) {
                                $("#ep1").hide().attr("val", "1");
                                $("#rep_switch").show();
                            }
                            var dts;
                            var dt = new Date();
                            var smonth = ("0" + (dt.getMonth() + 1)).substring(("0" + (dt.getMonth() + 1)).length - 2, 2);
                            var sdate = ("0" + dt.getDate()).substr(("0" + dt.getDate()).length - 2, 2);
                            dts = sdate + "." + smonth + "." + dt.getFullYear();
                            var ahtml = "<a href='/Report/EgrulPdf/" + data.dt + "/" + ogrn + "'><span class='icon-file-pdf pdf ico' id='top_egrul'></span><span>Выписка из ЕГРЮЛ от " + dts + "</span></a>";
                            $(".egrul_list").html(ahtml + $(".egrul_list").html());
                            clearInterval(chEgrulPDF);
                            complite_job(job_id, ahtml); //"<a href='/Report/EgrulPdf/"+data.dt+"/"+ogrn+"'><span>Выписка из ЕГРЮЛ от "+dts+"</span></a>");
                        }
                    }, "json");
                }, 5000);
            } else {
                showwin("critical", "Документ недоступен", "Вами превышен дневной лимит на заказ Выписок из ЕГРЮЛ.<br/> \
                                                  Выписку из ЕГРЮЛ/ЕГРИП по данной компании/данному ИП Вы сможете заказать завтра.<br/> \
                                                  По всем вопросам, связанным с использованием сайта СКРИН, Вы можете связаться с <br/> \
                                                  Отделом продаж и маркетинга по телефонам (495) 787-17-67 или <a href='http://kontragent.skrin.ru/pw/?s=1' target='_blank'>Оставить запрос</a>", 0);
            }
        }, "json");
    }

    this.UseMobile = function () {
        $.get("/Issuers/Modules/MobileVersion", function (data) {
            location.reload(true);
        })
    };

    var open_report = function (iss, e) {
        if ($('.repchoice').has(e.target).length === 0) {
            $('.dropmenu').not('[id *= "reports_"]').hide();
            $('.dropmenu[id *= "reports_"]').toggle();
        }
    };

    this.push_job = function (id, tit) {
        if (typeof u == "undefined") u = "";
        for (var i = 0; i < job_street.length; i++) {
            if (job_street[i].id == id && job_street[i].s == "w") {
                return;
            }
        }
        job_street.push({ id: id, t: tit, s: "w" });
        $("#job_list").prepend("<div class=\"job_list_tr\" id=\"" + job_street.length + "_" + id + "\"><div class=\"job_list_tdf\">" + tit + "</div><div class=\"animate-spin job_list_tdl\"><span class=\"icon-spin3\"></span></div>");
        $("#job_list").show();
        $(".icon_job_cnt").text(job_street.length);
        if (!$("#rot_box").hasClass("animate-spin")) {
            $("#rot_box").addClass("animate-spin", true);
        }
        if (job_street.length === 1) {
            $("#job_stats").show();
        }
    }

    this.complite_job = function (id, res) {
        var is_w = false;
        for (var i = 0; i < job_street.length; i++) {
            if (job_street[i].id == id && job_street[i].s == "w") {
                job_street[i].s = "c";
                if (typeof res == "undefined") res = job_street[i].t;
                $("#" + (i + 1) + "_" + id).children(".job_list_tdf").first().text('').html(res);
                //                    $("#" + (i+1) + "_" + id).children(".job_list_tdl").first().removeClass("animate-spin");
                $("#" + (i + 1) + "_" + id).children(".job_list_tdl").first().removeClass("animate-spin").html("<span class=\"icon-ok\"></span>");
                break;
            }
        }
        for (var i = 0; i < job_street.length; i++) {
            if (job_street[i].s == "w") {
                is_w = true;
                break;
            }
        }
        if (!is_w) $("#rot_box").removeClass("animate-spin");
        //$("#rot_box").toggleClass("icon_rotate", is_w);
    }

    this.show_job_list = function (e) {
        e.stopPropagation();
        $("#job_list").show();
    }

    this.hide_job_list = function (e) {
        e.stopPropagation();
        $("#job_list").hide();
    }

})();

