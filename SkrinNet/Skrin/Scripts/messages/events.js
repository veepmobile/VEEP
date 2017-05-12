
(function () {
    var ev = {};
    ev.type_id = '';
    var tree_active;
    var tree_table;
    var src;
    //var now = new Date();
    //now.setDate(now.getDate() + 1);


    var init_ev_calendar = function () {
        if (user_id != 0) {
            //if ($('#dfrom').val() == "") { $('#dfrom').val(now.format("dd.mm.yyyy")); }
            //if ($('#dto').val() == "") { $('#dto').val(now.format("dd.mm.yyyy")); }
            if ($('#dfrom').val() == "") { start_date(); }
            
            var dates = $("#dfrom, #dto").datepicker({
                changeMonth: true,
                showButtonPanel: true,
                changeYear: true,
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 6);
                    }, 0);
                },
                onSelect: function (selectedDate) {
                    var option = this.id == "dfrom" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat ||
                    $.datepicker._defaults.dateFormat,
                    selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        }
    }

    window.start_date = function () {
        $.post("/EventSearch/GetStartDate", function (data) {
            if (data) {
                if ($('#dfrom').val() == "") { $('#dfrom').val(data); }
                if ($('#dto').val() == "") { $('#dto').val(data); }
            }
        }, "html");
    }


    window.search_init = function () {
        start_date();
        init_ev_calendar();
        ev_save_search_params();
        ev_search(1);

        $('#btn_find').click(function () {
            if (!roles_object.canSearch) {
                no_rights();
                return;
            }

            ev_save_search_params();
            //if ($('#dfrom').val() == "") { $('#dfrom').val(now.format("dd.mm.yyyy")); }
            //if ($('#dto').val() == "") { $('#dto').val(now.format("dd.mm.yyyy")); }
            if ($('#dfrom').val() == "") { start_date(); }
            ev_search(1);
        });


        $('#btn_clear').click(function () {
            $("#search_name").val('');
            $("#dfrom").val('');
            $("#dto").val('');
            $("#types").val('');
            $("#types_val").val('');
            $("#types_excl").val('0');
            
        });

        $('html').on('click', function () {
            $('input').css({ 'background-color': '#FFFFFF' });
        });
    }

    window.ev_save_search_params = function () {
        var dfrom = $('#dfrom').val();
        ev.dfrom = /\d{2}\.\d{2}\.\d{4}/.test(dfrom) ? dfrom : "";
        var dto = $('#dto').val();
        ev.dto = /\d{2}\.\d{2}\.\d{4}/.test(dto) ? dto : "";
        var search_name = $('#search_name').val();
        ev.search_name = (search_name !== null || search_name !== 'undefined') ? search_name : '';
        var type_id = $('#types_val').val();
        ev.type_id = (type_id !== null && type_id != "999") ? type_id : '';
        var types_excl = $('#types_excl').val();
        ev.types_excl = types_excl !== null ? types_excl : 0;
        var grp_val = $('#gropVal').attr('val');
        ev.grp_val = grp_val !== null ? grp_val : 0;
    }

    window.ev_search = function (page) {
        showClock();
        ev.page = page;
        ev.rcount = 20;
        var url = "/EventSearch/EventSearchAsync";
        //var params = "dfrom=" + _get_date(ev.dfrom) + "&dto=" + _get_date(ev.dto) + "&type_id=" + ev.type_id + "&types_excl=" + ev.types_excl + "&search_name=" + ev.search_name + "&page=" + ev.page + "&page_length=20";
        var params = "dfrom=" + ev.dfrom + "&dto=" + ev.dto + "&type_id=" + ev.type_id + "&types_excl=" + ev.types_excl + "&search_name=" + ev.search_name + "&page=" + ev.page + "&page_length=20&grp=" + ev.grp_val;
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    GenerateResult(data, page, ev.rcount);
                }
            }
        });
    }
    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }

    window._get_date = function (cdate) {
        //if (cdate == "") { cdate = now.format("dd.mm.yyyy"); }
        if (cdate == "") { start_date(); cdate = $('#dfrom').val(); }
        var cd = cdate.split('.');
        return cd[2] + cd[1] + cd[0];
    }

    var GenerateResult = function (data, page, rcount) {
        $('#event_search_result').html('');

        //var total = data.length;
        var total = (data.length > 0) ? data[0]["event_count"] : 0;
        var page_count = total / rcount;
        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total && total > 0) {
            var total_text;
            if (total == 0) {
                total_text = "<span class=\"total_count\">Нет данных соответствующих заданному условию</span>";
            }
            else {
                total_text = "<span class=\"total_count\">Всего найдено " + total + " сообщений.</span>";
            }
            $("#event_search_result").append(total_text);

            var res_arr = data;
            var i_max = res_arr.length > 20 ? 20 : res_arr.length;
            //var i_start = (page - 1) * 20;
            ////var i_end = (page * 20) > res_arr.length ? res_arr.length : page * 20;
            //var i_end = (page * 20) > total ? total : page * 20;

            var $res_block = $('<div>').addClass("res_block");
            //for (var i = i_start; i < i_end; i++) {
            for (var i = 0; i < i_max; i++) {
                var $res_item = $('<div>').addClass("res_item");


                var content = [];
                content.push('<span class="news_data news_data_sm">' + res_arr[i].rd + '</span>')
                if (res_arr[i].ticker.length > 0) {
                    content.push('<a class="issuer" href="/issuers/' + res_arr[i].ticker + '" target="_blank">' + res_arr[i].short_name + '</a>');
                } else {
                    content.push('<span class="issuer"> ' + res_arr[i].short_name + '</span>');
                }
                content.push(': <span class="event_header">' + res_arr[i].name + '</span>');
                var comp_info_text = "";
                comp_info_text += res_arr[i].ec_date.length == 10 ? ('<span class="event_comment" >' + (res_arr[i].ec_news.length >= 32 ? ('<a class="comment"  href="#" onclick="Showevent(\'\',\'' + iss + '\', \'' + res_arr[i].ECID + '\',0)">' + res_arr[i].ec_date + ' ' + res_arr[i].ec_headline + '</a>') : (res_arr[i].ec_date + ' ' + res_arr[i].ec_headline)) + '</span>') : '';
                comp_info_text += res_arr[i].ce_date.length == 10 ? ('<span class="event_comment" >' + (res_arr[i].ce_news.length >= 32 ? ('<a class="comment"  href="#" onclick="Showevent(\'\',\'' + iss + '\', \'' + res_arr[i].CEID + '\',0)">' + res_arr[i].ce_date + ' ' + res_arr[i].ce_headline + '</a>') : (res_arr[i].ce_date + ' ' + res_arr[i].ce_headline)) + '</span>') : '';
                content.push(comp_info_text);
                $res_item.html(content.join(''));

                /*var $info_block = $('<div>').addClass("info_block");
                
                $info_block.append('<span class="event_title">' + res_arr[i].rd + "&nbsp;&nbsp;&nbsp;" + res_arr[i].name + '</span><br/>');
                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";
                //if (res_arr[i].ticker.length > 0) {
                //    $info_block.append('<a class="comp_title_sm" href="/issuers/' + res_arr[i].ticker + '" target="_blank">' + res_arr[i].short_name + '</a>');
                //} else {
               //     $info_block.append('<span class="comp_title_sm">' + res_arr[i].short_name + '</a>');
                //}
                comp_info_text += res_arr[i].ec_date.length == 10 ? ('<span class="event_comment" style="margin:0; padding-left:115px;">' + (res_arr[i].ec_news.length >= 32 ? ('<a class="comment" style="margin:0" href="#" onclick="Showevent(\'\',\'' + iss + '\', \'' + res_arr[i].ECID + '\',0)">' + res_arr[i].ec_date + ' ' + res_arr[i].ec_headline + '</a>') : (res_arr[i].ec_date + ' ' + res_arr[i].ec_headline)) + '</span>') : '';
                comp_info_text += res_arr[i].ce_date.length == 10 ? ('<span class="event_comment" style="margin:0; padding-left:115px;">' + (res_arr[i].ce_news.length >= 32 ? ('<a class="comment" style="margin:0" href="#" onclick="Showevent(\'\',\'' + iss + '\', \'' + res_arr[i].CEID + '\',0)">' + res_arr[i].ce_date + ' ' + res_arr[i].ce_headline + '</a>') : (res_arr[i].ce_date + ' ' + res_arr[i].ce_headline)) + '</span>') : '';
                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);
                var $code_block = $('<div>').addClass("code_block_top");
                //comp_info_text = "";
                if (res_arr[i].ticker.length > 0) {
                    $code_block.append('<a class="comp_title_16" href="/issuers/' + res_arr[i].ticker + '" target="_blank">' + res_arr[i].short_name + '</a>');
                } else {
                    $code_block.append('<span class="comp_title_16">' + res_arr[i].short_name + '</a>');
                }
                //comp_info_text += "<span class=\"code_title\">Дата:</span>" + res_arr[i].rd;
                //$code_block.html(comp_info_text);
                $res_item.append($info_block);
                $res_item.append($code_block);*/
                $res_block.append($res_item);
            }
            $("#event_search_result").append($res_block);
            $("#event_search_result").append(_get_paging(page, page_count));

        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных, соответствующих заданному условию");
            $("#event_search_result").append($el);            
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
            html += '<td onclick="ev_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="ev_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="ev_search(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<br/><div id="page_counter"><table>' + html + '</table></div>';
    };


    window.show_tree_selector = function (e, sr, is_tree, mult) {

        src = (sr == 99) ? 1 : sr;
        tree_table = src;
        hidepopups()
        if (window.event) {
            e = window.event;
            caller = e.srcElement;
        }
        if (e.stopPropagation) {
            e.stopPropagation();
            caller = e.target;
        } else {

            e.cancelBubble = true;
        }
        tree_active = caller.id;
        $("#" + tree_active).css({ 'background-color': '#F0F0F0' });
        var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
        if (!getObj(tree_active + "_window")) {
            d = document.createElement("iframe");
            d.className = "tree_frame";
            d.id = tree_active + "_window";

            d.frameBorder = "0"
            if (is_tree == 1) {
                d.src = "/Tree/TreeSelector?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val();
            } else {
                tree_table += 6;
                d.src = "/iss/selector/selector.asp?src=" + src + "&nodes=" + $("#" + tree_active + "_val").val() + "&mult=" + mult;
            }
            var content = "<div id=\"td_" + tree_active + "\"></div>";
            show_dictionary();
            getObj("td_" + tree_active).appendChild(d);

            showContentClock('#dic_container .modal-dialog');

            $("#dic_container").click(function (event) {
                event.stopPropagation();

            })
        }
    };

    window.Write_TS = function (retval, is_excl) {
        if (retval.length > 0) {
            $.post("/Tree/GetResultString", { "src": tree_table, "id": retval },
            function (data) {
                $("#" + tree_active).val(((is_excl == -1) ? "Искл.: " : "") + data);
                $("#" + tree_active).attr({ "title": ((is_excl == -1) ? "Исключая: " : "") + data.replace(",", ",\n") });
                $("#" + tree_active + "_excl").val(is_excl);
                $("#" + tree_active + "_val").val(retval);
                search_check_changes();
            })
        } else {
            $("#" + tree_active).val("");
            $("#" + tree_active).attr({ "title": eval("titles." + tree_active) });
            $("#" + tree_active + "_excl").val(0);
            $("#" + tree_active + "_val").val("");
            search_check_changes();
        }
        hidepopups();

    };

    var hidepopups = function (event) {
        close_bones();

        if (getObj(tree_active + "_window")) {
            getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window"));
            $("#" + tree_active).css({ 'background-color': '#FFFFFF' });
        }
        close_dialog();
        if (event) {
            remove_div(event, "#groupSelector", "#groupSelect");
            remove_div(event, "#countSelector", "#countSelect");
        }

    };

    var close_bones = function () {
        if (getObj("dp_window")) {
            document.body.removeChild(getObj("dp_window"));
            $("html").unbind();
            $('#comp').css("border-radius", "4px");
        }
    };

    var show_dictionary = function () {
        var $placer = $("body"); // контенер для добавления
        var id = "dic_container"; //идентификатор диалогового окна
        var body = '<div id="' + id + '" class="modal fade" tabindex="-1" style="display: none;" aria-hidden="true">'
                    + '<div class="modal-dialog"><div class="modal-content">'
                    + '<div class="modal-header"><button class="close" aria-hidden="true" data-dismiss="modal" type="button">×</button>'
                    + '</div><div class="modal-body"><div id="td_' + tree_active + '"></div></div>'
                    + '<div class="modal-footer">'
                    + '</div>'
                    + '</div></div></div>';
        //Удалим старое содержимое
        $('#' + id).remove();

        $placer.append(body);

        $('#' + id).modal();
    };

    window.search_check_changes = function () {
        var changes_count = 0;
        $('[data-check]').each(function () {
            var $el = $(this);
             if ($el.attr("type") == "text" || $el.attr("type") == "hidden") {
                if ($el.val() != "") {
                    changes_count++;
                }
            } else {
                if ($el.prop("checked")) {
                    changes_count++;
                }
            }
        });

    }




    Date.prototype.format = function (mask, utc) {
        return dateFormat(this, mask, utc);
    };

})();

$().ready(function () {
    search_init();
});

