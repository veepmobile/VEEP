(function () {

    var searching = 0;
    var excel_menu_open = false;

    window.search_init = function () {
        $("#base_block").bind("keypress", function (e) { try2search(e); });

        $('body').on('click', function (e) {
            hidepopups(e);
            _hide_command_menu(e);
        });
    }

    var hidepopups = function (event) {
        close_bones();
        close_dialog();
    };

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#ruler').css("border-radius", "4px");
        }
    };

    var try2search = function (e) {
        var kk
        if (window.event) {
            e = window.event;
        }
        kk = e.keyCode;
        if (e.stopPropagation) {
            e.stopPropagation();
        } else {
            e.cancelBubble = true;
        }
        if (kk == 13) {
            doSearch();
        }
    };

    window.doSearch = function (pg) {

        if (!roles_object.canSearch) {
            no_rights();
            return;
        }

        //close_bones();
        if (searching == 1) {
            doStop();
        } else {
            if (checkCanSearch()) {
                searching = 1;
                pg = (isNaN(String(pg))) ? 1 : pg;
                showClock();
                $("#btn_search").text("Остановить");
                mainSearcher(pg);
            } else {
                showwin('warning', 'Надо выбрать критерий поиска!', 2000);
            }
        }
    };

    var doStop = function () {
        hideClock();
        searching = 0;
        //$.post("/iss/modules/operations.asp", { "action": "6" });
        $("#btn_search").text("Найти");
        window.stop();
    };

    window.doClear = function () {
        $("#fio").val("");
    }

    var checkCanSearch = function () {
        var ss = $("#fio").val();
        return (ss.length == 0) ? false : true;
    };

    var mainSearcher = function (pg) {
        var params = {
            "fio": $("#fio").val(),
            "page": pg
        }

        if (pg == -1000) {
            $.post("/SearchFl/Search", params, function (data) {
                if (data) { DownLoadXLS1000(data); }
                hideClock();
                searching = 0;
            });
            return;
        }

        $.post("/SearchFL/Search", params, function (data) {
            var re = new RegExp("icon_error", "ig")
            if (pg > 0) {
                GenerateResult(pg, data, roles_object, user_id);
                hideClock();
                if (String(re.exec(data)) == "null") {
                    if ($("#search_table").css("display") == "block" || $("#search_table").css("display") == "table") {
                        $('#hide_form').show();
                        toggle_form();
                    }
                }
            }

            hideClock();
            $("#btn_search").text("Найти");
            searching = 0;
            location.href = "#top";
        }, "json");
    };

    var GenerateResult = function (page, data, ro, user_id) {
        $("#search_result").html('').show();

        var hits = data.hits;
        var res_arr = hits["hits"];
        var total_found = res_arr.length;
        var total = hits.total;
        var page_count = total / 20;
        if (total > 10000) page_count = 10000 / 20;

        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total && total > 0) {
            var total_text;

            total_text = "Всего найдено: " + total;
            if (total > 10000) {
                total_text += " (Выводятся первые 10000 физических лиц)";
            }
            $("#search_result").append("<span class=\"total_count\">" + total_text + "</span>");

            var $res_block = $('<div>').addClass("res_block");

            if (ro.canExport) {
                var $export_block = $('<div>').addClass("export_block");

                var $checkall_block = $('<div>').addClass("checkall_block");
                $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");
                $export_block.append($checkall_block);

                var $export_command_block = $('<div>').addClass("export_command_block");

                var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"icon-file-excel icon\"></span></div>");
                $export_command_block.append($add_excel_btn);
                $add_excel_btn.click(function (e) {
                    generate_sub_excelmenu(e, total);
                });


                $export_block.append($export_command_block);
                $res_block.append($export_block);
            }

            for (var i = 0, i_max = (res_arr.length - 1) ; i <= i_max; i++) {
                var it = res_arr[i];
                var $res_item = $('<div>').addClass("res_item");

                if (ro.canExport) {
                    var $check_block = $('<div>').addClass("check_block");
                    $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff(this);\" type=\"checkbox\" value=\"" + it.id + "\"></input>");
                    $res_item.append($check_block);
                }

                var $info_block = $('<div>').addClass("info_block_ip");
                $info_block.append('<a class="comp_title" href="/profilefl?fio=' + encodeURIComponent(it._source.fio) + "&inn=" + it._source.inn + '" target="_blank">' + it._source.fio + '</a>');

                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";
                if (it._source.inn != "") {
                    comp_info_text += "<p>ИНН: " + it._source.inn + "</p>";
                }
                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                $res_item.append($info_block);
                $res_block.append($res_item);
            }
            $("#search_result").append($res_block);
            $("#search_result").append(_get_paging(page, page_count))
        }
        else
        {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
        }
    }

    var _get_paging = function (page, page_count) {

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
        if (i < page_count + 1) {
            html += '<td onclick="doSearch(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    window.doSetCheckedAll = function (chb) {
        $(".res_item").find("input:checkbox").each(function (i) {
            this.checked = chb.checked;
        });

    };

    window.checkOnOff = function (o) {
        var allCheckboxes = $(".res_item").find('input:checkbox');
        var checkedCheckboxes = $(".res_item").find('input:checkbox:checked');
        var allChecked = allCheckboxes.length == checkedCheckboxes.length;
        $("#selallbox").get(0).checked = allChecked;
    };

    var generate_sub_excelmenu = function (e,total) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным физ.лицам").click(function () { doSave2XLS(); }).appendTo($ul);
        $("<li>").text("Экспорт по " + (total > 10000 ? "первым 10000" : total) + " физ.лицам").click(function () { doExport10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
    };

    var _hide_command_menu = function (e) {

        if (excel_menu_open) {
            $('.sub_command_menu').remove();
            excel_menu_open = false;
        }
    };

    var doSave2XLS = function () {
        var issuers = "";
        showClock();
        if ($(".res_item").find('input:checkbox:checked').length > 0) {
            $(".res_item").find("input:checkbox:checked").each(function (i) {
                if (String(this.value).length > 0) {
                    issuers += String(this.value) + "|";
                }
            });
            issuers = issuers.substring(0, issuers.length - 1);
            $.post("/SearchFL/GetExcel", { "issuers": issuers }, function (data) {
                if (data) { DownLoadXLS1000(data); }
                hideClock();
            });

        } else {
            showwin('critical', '<p align=center>Не отмечено ни одного физ.лица для экспорта</p>', 3000);
        }
    };

    var doExport10000 = function () {
        showClock();
        mainSearcher(-1000);
    };

    var DownLoadXLS1000 = function (filename) {
        var form = document.createElement("form");
        form.action = "/SearchFL/GetFile";
        form.method = "POST";
        if (!document.addEventListener) {
            showClock();
            form.target = "xls_frame"
        } else {
            form.target = "blank"
        }
        form.style.display = "none"
        form.appendChild(make_input("src", filename));
        form.appendChild(make_input("page", "dbsearchru/fl"));
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    };

    var make_input = function (name, value) {
        var element = null;
        element = document.createElement("input");
        element.type = "text";
        element.name = name;
        element.value = value;
        return element;
    };

})();

$().ready(function () {
    search_init();
});
