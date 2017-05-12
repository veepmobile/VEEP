var ind_type = 24;

(function () {

    var qivparam_pattern = '                \
                <div class="form-group param_row" id="qivparam_{name}{id}"> \
                    <div class="sub_form"> \
                        <span style="padding: 0 5px 0 0;">Год</span> \
                        <div class="customSelect short_custom" style=\"width:70px;\" id="yearSelect{name}{id}" onclick="show_yearSelector(\'{name}{id}\');"> \
                            <span id="yearVal{name}{id}" class="customVal" data-check val=""></span> \
                            <span class="icon-angle-down"></span> \
                        </div> \
                        <span style="padding: 0 5px 0 10px;">Показатель</span> \
                        <div class="customSelect search_input" id="paramSelect{name}{id}" onclick="show_param_selector(event, \'{name}{id}\');"> \
                            <span id="paramVal{name}{id}" class="customVal" data-check  val=""></span> \
                        </div> \
                        <span style="padding: 0 5px 0 10px;">Значение от </span> \
                        <input id="fromVal{name}{id}" type="text" class="search_input shortest" placeholder="в рублях" data-check /> \
                        <span style="padding: 0 5px 0 10px;">до </span> \
                        <input id="toVal{name}{id}" type="text" class="search_input shortest" placeholder="в рублях" data-check /> \
                        <button class="btns red" id="btn_{name}{id}">Удалить</button> \
                    </div> \
                </div> \
    ';

    var maxParam = 5;
    var maxid = 0;
    var tree_active;
    var content_top = $("#header").height() + $("#search_block.tprice_search_block").position().top - 67;
    var countParams = [30, 50, 100];
    var extGroupList = [{ "code": 0, "txt": "Нет", "val": "" }];
    var titles = { "ind": "Общероссийский классификатор видов экономической деятельности", "okfs": "в соответствии с ОКФС - Общероссийский классификатор форм собственности", "reg": "Общероссийский классификатор объектов административно-территориального деления" };
    
    var tree_active;
    var tree_table;
    var src;
    var new_search = false;
    var selection_id = "";
    var group_menu_open = false;
    var excel_menu_open = false;

    var SO=function(template_params,extra_params){
        this.template_params=template_params;
        this.extra_params=extra_params;
    }

    


     window.addParam = function (param) {
        if ($('[id^="qivparam_gks').length < maxParam) {
            maxid++;
            $('#qivparam-group_gks').append(qivparam_pattern.split("{id}").join(maxid).split("{name}").join('gks'));
            addDelHandler(maxid);
            if (!param) {
                getDefaultYear('gks' + maxid);
            }
        }
        if (param) {
            $("#yearValgks" + maxid).attr("val",param.year).text(param.year);
            $("#paramValgks" + maxid).attr("val", param.param_id).data("typeid",param.type_id);
            $("#fromValgks" + maxid).val(param.from);
            $("#toValgks" + maxid).val(param.to);
            write_param_name($("#paramValgks" + maxid));
        }
        if ($('[id^="qivparam_gks').length > (maxParam - 1)) {
            $('#qiv_param_add').addClass("disabled");
        }
        if ($('[id^="qivparam_gks').length > 1) {
            $('[id^="btn_gks').removeClass("disabled");
        } else {
            $('[id^="btn_gks').addClass("disabled");
        }
     };

     var write_param_name = function ($el) {
         var id = Number($el.attr("val"));
         var type_id = $el.data("typeid");
         $.post("/QIVParam/GetCodeName", { id: id, type_id: type_id }, function (data) {
             $el.html(restrictGropName("<b>" + data.code + "</b> " + data.name,52));
         },"json")
     }

    var addDelHandler = function (id) {
        var _id = id;
        var handler = function () {
            console.log(_id);
            if ($('[id^="qivparam_gks"').length > 1)
                $("#qivparam_gks" + _id).remove();
            if ($('[id^="qivparam_gks"').length < maxParam)
                $('#qiv_param_add').removeClass("disabled");
            if ($('[id^="qivparam_gks"').length === 1)
                $('[id^="btn_gks"').addClass("disabled");
        }
        $("#btn_gks" + _id).click(handler);
    };

    var getDefaultYear = function (id) {
        var i = 0;
        $.post("/QIVParam/getDefaultYear", {},
               function (data) {
                   $("#yearVal" + id).text(data);
                   $("#yearVal" + id).attr("val", data);
                   getDefaultParam(id);
               }
       );
    };

    var getDefaultParam = function (id) {
        $("#paramVal" + id).html("");
        $("#paramVal" + id).attr("val", "0");
        var y = $("#yearVal" + id).attr("val");
        if (y !== "") {
            $.post("/QIVParam/getDefaultParam", { "period": y },
                   function (data) {
                       selectNewParam(data.id, "<b>" + data.line_code + "</b> " +  data.name, id,data.type_id);
                   },
           "json");
        }
    };

    window.selectNewParam = function (param_id, param_name, elem_id, type_id) {
        var max_length = 52;
        if (param_name.length > max_length) {
            $("#paramVal" + elem_id).html(param_name.substring(0, max_length) + '...');
        }
        else {
            $("#paramVal" + elem_id).html(param_name);
        }
        $("#paramVal" + elem_id).attr("val", param_id).data("typeid",type_id);
        hidepopups();
    };

    window.show_tree_selector = function (e, sr, is_tree, mult) {

        src = (sr == 99) ? 24 : sr;
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
            }
            var content = "<div id=\"td_" + tree_active + "\"></div>";
            show_dictionary();
            getObj("td_" + tree_active).appendChild(d);

            showContentClock('#dic_container .modal-dialog');

            $("#dic_container").click(function (event) {
                //                event.stopPropagation();
            })
        }
    };



    var hidepopups = function (event) {
        if (getObj(tree_active + "_window")) {
            getObj(tree_active + "_window").parentNode.removeChild(getObj(tree_active + "_window"));
            $("#" + tree_active).css({ 'background-color': '#FFFFFF' });
        }
        close_dialog();
        if (event) {
            remove_div(event, "#groupSelector", "#groupSelect");
            remove_div(event, "#countSelector", "#countSelect");
            remove_div(event, "#tmplSelector", "#tmplSelect");
            remove_div(event, "#nperiods", "#dnp");
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

    var remove_div = function (event, div_selector, exclude_selector) {

        var container = $(div_selector); //сам контейнер, который нужно убрать
        var exclude = $(exclude_selector);//окно поиска нажатием на которое появляется контейнер

        if (!container.is(event.target) // if the target of the click isn't the container...
        && container.has(event.target).length === 0  // ... nor a descendant of the container
        && !exclude.is(event.target)
        && exclude.has(event.target).length === 0) {
            container.remove();
            exclude.removeClass("customSelect_focus");

        }
    }



   

    

     window.get_so_params = function () {
        var tp = {};
        
        tp.pure_actives = (($("input:checkbox:checked[id=pa]").length > 0) ? 1 : 0);
        tp.loss = (($("input:checkbox:checked[id=loss]").length > 0) ? 1 : 0);
        tp.constitutors = (($("input:checkbox:checked[id=constitutors]").length > 0) ? 1 : 0);
        tp.ncons = Number($("#ncons").val());
        tp.subs = (($("input:checkbox:checked[id=subs]").length > 0) ? 1 : 0);
        tp.nsubs = Number($("#nsubs").val());
        tp.nperiods = Number($("#snp").attr("val"));
        tp.only_suitable = (($("input:checkbox:checked[id=suitable]").length > 0) ? 1 : 0);
        tp.group_id = Number($("#gropVal").attr("val"));
        tp.regions = $("#reg_val").val();
        tp.reg_excl = (String($("#reg_excl").val()).length == 0) ? 0 : Number($("#reg_excl").val());
        tp.industry = $("#ind_val").val();
        tp.ind_excl = (String($("#ind_excl").val()).length == 0) ? 0 : Number($("#ind_excl").val());
        tp.ind_main = ($("input[name='ind_type']:checked").val() - 99 == 0) ? "1" : "0";
        tp.okfs = $("#okfs_val").val();
        tp.okfs_excl = (String($("#okfs_excl").val()).length == 0) ? 0 : Number($("#okfs_excl").val());
        tp.rcount = Number($("#countVal").attr("val"));
        tp.andor = $("input:radio[name=cond_selector]").filter(":checked").val();
        tp.tparams = getParamJSON();
        return tp;
     }

     var restore_so_params = function (so,template) {
         $("#countVal").text(so.rcount);
         $("#countVal").attr("val", so.rcount);
         $("#gropVal").attr("val", so.group_id);
         $("#gropVal").text(extGroupList[scanGroups(so.group_id)].txt);

         $("#tmplVal").text(template.name);
         $("#tmplVal").attr("val", template.id);

         $("#snp").text(so.nperiods+" года");
         $("#snp").attr("val", so.nperiods);

         $("input:checkbox[id=pa]").prop("checked", so.pure_actives==1);
         $("input:checkbox[id=loss]").prop("checked", so.loss==1);

         $("input:checkbox[id=constitutors]").prop("checked", so.constitutors == 1);

         var $ncons = $("#ncons").val(so.ncons);
         if (so.constitutors == 1) {
             $ncons.prop('readOnly', false).removeClass("search_input_disabled").addClass("search_input");
         } else {
             $ncons.prop('readOnly', true).addClass("search_input_disabled").removeClass("search_input");
         }

         $("input:checkbox[id=subs]").prop("checked", so.subs == 1);

         var $nsubs = $("#nsubs").val(so.nsubs);
         if (so.subs == 1) {
             $nsubs.prop('readOnly', false).removeClass("search_input_disabled").addClass("search_input");
         } else {
             $nsubs.prop('readOnly', true).addClass("search_input_disabled").removeClass("search_input");
         }

         $("input:checkbox[id=suitable]").prop("checked", so.only_suitable == 1);

         $("#reg_val").val(so.regions);
         $("#reg_excl").val(so.reg_excl);
         Write_Loaded(so.regions, so.reg_excl, "reg", 0);

         $("#ind_val").val(so.industry);
         $("#ind_excl").val(so.ind_excl);
         Write_Loaded(so.industry, so.ind_excl, "ind", 24);
         
         $("#okfs_val").val(so.okfs);
         $("#okfs_excl").val(so.okfs_excl);
         Write_Loaded(so.okfs, so.okfs_excl, "okfs", 3);
        
         $("input:radio[name=ind_type][value='" + (so.ind_main == 1 ? "99":"24") + "']").prop("checked", true);
         $("input:radio[name=cond_selector][value='" + so.andor + "']").prop("checked", true);

         maxid = 0;
         $('.param_row').remove();
         for (var i = 0; i < so.tparams.length; i++) {
             addParam(so.tparams[i]);
         }
         
     }

     var scanGroups = function (code) {
         var ret = 0;
         for (var i = 0; i < extGroupList.length; i++) {
             if (extGroupList[i].code == code) {
                 ret = i
             }
         }
         return ret;
     }

     var Write_Loaded = function (retval, is_excl, objid, tree_id) {
         if (retval.length > 0) {
             $.post("/Tree/GetResultString", { "src": tree_id, "id": retval },
             function (data) {
                 $("#" + objid).val(((is_excl == -1) ? "Искл.: " : "") + data);
                 $("#" + objid + "_excl").val(is_excl);
                 $("#" + objid + "_val").val(retval);
             })
         } else {
             $("#" + objid).val("");
             $("#" + objid + "_excl").val(0);
             $("#" + objid + "_val").val("");

         }
         hidepopups();
     }

     var clear = function() {
         $("#countVal").text(countParams[0]);
         $("#countVal").attr("val", countParams[0]);
         $("#gropVal").text(restrictGropName(extGroupList[0].txt));
         $("#gropVal").attr("val", extGroupList[0].code);
         $("#tmplVal").text("Нет");
         $("#tmplVal").attr("val", 0);
         $("#snp").text("3 года");
         $("#snp").attr("val", 3);
         
         $("input:checkbox[id=pa]").prop("checked", true);
         $("input:checkbox[id=loss]").prop("checked", true);
         $("input:checkbox[id=constitutors]").prop("checked", true);
         $("#ncons").val("25").prop('readOnly', false).removeClass("search_input_disabled").addClass("search_input");
         $("input:checkbox[id=subs]").prop("checked", true);
         $("#nsubs").val("25").prop('readOnly', false).removeClass("search_input_disabled").addClass("search_input");
         $("input:checkbox[id=suitable]").prop("checked", true);
         $("#reg").val("");
         $("#reg_val").val("");
         $("#reg_excl").val(0);
         $("#ind").val("");
         $("#ind_val").val("");
         $("#ind_excl").val(0);
         $("#okfs").val("");
         $("#okfs_val").val("");
         $("#okfs_excl").val(0);
         $("input:radio[id=ind_default]").prop("checked", true);
         $("input:radio[id=cond_default]").prop("checked", true);

      
         maxid = 0;
         $('.param_row').remove();
         addParam();

     }

    var getParamJSON = function () {
        var res = [];

        $('.param_row').each(function () {
            var pid = $(this).attr('id').split("_")[1];
            res.push({
                "year": Number($("#yearVal" + pid).attr("val")),
                "param_id": Number($("#paramVal" + pid).attr("val")),
                "from": $("#fromVal" + pid).val(),
                "to": $("#toVal" + pid).val(),
                "type_id":$("#paramVal" + pid).data("typeid")
            });
        })
        return res;
    }

    window.SearchNow = function (pg,group_name,issuers) {
        var ep = {
            "page_no": pg,
            "sel": selection_id,
            "group_name": group_name,
            "iss":issuers
        };

        var tp = get_so_params();

        var  data=JSON.stringify({ "tp": tp ,
            "ep": ep
        });

        if (pg > 1 || pg==-1000) {
            showClock();
        }

        if (pg == -2000) {
            var form = document.createElement("form");
            form.action = "/TPrice/SearchExcel";
            form.method = "POST";
            element = document.createElement("input");
            element.type = "hidden";
            element.name = "string_params";
            element.value = JSON.stringify(new SO(tp, ep));
            form.appendChild(element);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        } else {

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: '/TPrice/Search',
                data: data,
                success: function (data) {
                    unlock_button('#btn_search');
                    hideClock();
                    if (pg >= 1) {
                        generate_result(pg, data);
                    }
                    if (pg == -1000) {
                        showwin(((data.code == 0) ? "info" : "critical"), data.msg, 0);
                    }

                },
                error: function () {
                    unlock_button('#btn_search');
                    hideClock();
                }
            });
        }

    }

    var generate_result = function (page, data) {
        $("#search_result").html('').show();

        if (data && data.length > 0) {

            var total = data[0].search_count;
            var searchsession_id = data[0].session_id;
            var page_count = total / 20;
            if (page_count % 1 > 0) {
                page_count = (page_count - (page_count % 1)) + 1;
            }

            var total_text = "<span class=\"total_count\">Всего найдено: " + total + " предприятий.</span>"
            if (total < 4) {
                total_text = "<span class=\"non_result\">Всего найдено менее 4 предприятий. Расчет невозможен. Измените условие.</span>"
            }
            if (total >= 10000) {
                total_text = "<span class=\"non_result\">Всего найдено более 10000 предприятий. Уточните запрос.</span>"
            }

            $("#search_result").append(total_text);

            $('#search_result').append("<input type=\"hidden\" value=\"\" id=\"ids\"/>");

            var $res_block = $('<div>').addClass("res_block");

            var $export_block = $('<div>').addClass("export_block");

            var $checkall_block = $('<div>').addClass("checkall_block");
            $checkall_block.append("<input type=\"checkbox\" name=\"selallbox\" id=\"selallbox\" onclick=\"doSetCheckedAll(this);\" alt=\"Выделить все\"/>");
            if (total > 3 && total < 10000) {
                $checkall_block.append("<button class=\"btns darkblue large\" style=\"margin: 0px 20px;\" id=\"btn_calc\">Рассчитать рентабельность</button>");
            }
            $export_block.append($checkall_block);

            var $export_command_block = $('<div>').addClass("export_command_block");
            if (roles_object.canAddToGroup) {
                var $add_group_btn = $("<div id=\"btnaddGroup\"><span class=\"icon-plus icon\"></span></div>");
                $export_command_block.append($add_group_btn);
                $add_group_btn.click(function (e) {
                    generate_sub_addGroupmenu(e, total);
                })
            }

            if (roles_object.canExport) {
                var $add_excel_btn = $("<div id=\"btnaddExcel\"><span class=\"icon-file-excel icon\"></span></div>");
                $export_command_block.append($add_excel_btn);
                $add_excel_btn.click(function (e) {
                    generate_sub_excelmenu(e, total);
                });
            }

            $export_block.append($export_command_block);
            $res_block.append($export_block);

            for (var i = 0; i < data.length; i++) {
                var $res_item = $('<div>').addClass("res_item");

                var $check_block = $('<div>').addClass("check_block");
                $check_block.append("<input name=\"selsissuer\" onclick=\"checkOnOff_Sel(this);\" v=\""+data[i].gks_id+"\" type=\"checkbox\" value=\"" + data[i].issuer_id + "_" + data[i].type_id + "\"></input>");
                $res_item.append($check_block);

                var $info_block = $('<div>').addClass("info_block");

                var suit = "";
                if (data[i].suit == 0) {
                    suit = "<span class=\"title\"> \
                          <em>По данной компании невозможен расчет<br/> всех показателей рентабельности</em> \
                          <span class=\"icon-attention-alt attention\" ></span> \
                        </span>";
                }

                $info_block.append(suit);

                if (data[i].ticker.length > 0) {
                    $info_block.append('<a class="comp_title" href="/issuers/' + data[i].ticker + '" target="_blank">' + data[i].name + '</a>');
                } else {
                    $info_block.append('<span class="comp_title">' + data[i].name + '</a>');
                }

                var $comp_info = $('<div>').addClass("comp_info");
                var comp_info_text = "";

                if (data[i].del != '0') {
                    comp_info_text += "<p class=\"attention\">Исключено из реестра Росстата " + data[i].del + "!</p>";
                }

                if (data[i].legal_address != "") {
                    comp_info_text += "<p>" + data[i].legal_address + "</p>";
                }
                if (data[i].ruler != "") {
                    comp_info_text += "<p>Руководитель: " + data[i].ruler + "</p>";
                }

                if (data[i].okved != "") {
                    comp_info_text += "<p>" + data[i].okved + "</p>";
                }

                $comp_info.html(comp_info_text);
                $info_block.append($comp_info);

                var $code_block = $('<div>').addClass("code_block");
                comp_info_text = "";
                if (data[i].ogrn != "") {
                    comp_info_text += "<p><span class=\"code_title\">ОГРН:</span>" + data[i].ogrn + "</p>";
                }
                if (data[i].inn != "") {
                    comp_info_text += "<p><span class=\"code_title\">ИНН:</span>" + data[i].inn + "</p>";
                }
                if (data[i].okpo != "") {
                    comp_info_text += "<p><span class=\"code_title\">ОКПО:</span>" + data[i].okpo + "</p>";
                }
                $code_block.html(comp_info_text);

                $res_item.append($info_block);
                $res_item.append($code_block);
                $res_block.append($res_item);

            }


            $("#search_result").append($res_block);
            $("#search_result").append(_get_paging(page, page_count));

            post_search_binding(searchsession_id);

        } else {
            var $el = $('<div>').addClass("non_result").text("Нет данных соответствующих заданному условию");
            $("#search_result").append($el);
        }


    }


    var post_search_binding = function (sess_id) {
        $('#btn_calc').click(function () {
            lock_button('#btn_calc');
            CalcTPrice(sess_id);
        })

        
    }

    var generate_sub_addGroupmenu = function (e, total) {
        if (group_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Добавить выбранные компании в Группу").click(function () { doSave2List(); }).appendTo($ul);
        $("<li>").text(total > group_limit ? "Добавить первые " + group_limit + " компаний в Группу" : "Добавить найденные компании (" + total + ") в Группу").click(function (event) { doCreateGroup1000(event); }).appendTo($ul);
        $("#btnaddGroup").append($ul);
        group_menu_open = true;
        e.stopPropagation();
    }

    var generate_sub_excelmenu = function (e,total) {
        if (excel_menu_open) {
            return;
        }
        var $ul = $("<ul>").addClass("sub_command_menu");
        $("<li>").text("Экспорт по выбранным компаниям").click(function () { doSave2XLSSelected(); }).appendTo($ul);
        $("<li>").text("Экспорт по " + (total > 10000 ? "первым 10000" : total) + " компаний").click(function () { xlsExportTop10000(); }).appendTo($ul);
        $("#btnaddExcel").append($ul);
        excel_menu_open = true;
        e.stopPropagation();
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
            html += '<td onclick="SearchNow(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="SearchNow(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="SearchNow(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<div id="page_counter"><table>' + html + '</table></div>';
    };

    var init_inputs = function () {
        $('#constitutors').click(function () {
            var $ncons = $('#ncons');
            if (this.checked) {
                $ncons.prop('readOnly', false).removeClass("search_input_disabled").addClass("search_input");
            } else {
                $ncons.prop('readOnly', true).removeClass("search_input").addClass("search_input_disabled");
            }
        });

        $('#ncons').change(function () {
            this.value = isNaN(this.value) ? 0 : this.value;
            this.value = this.value.length == 0 ? 0 : this.value
        });

        $('#subs').click(function () {
            var $nsubs = $('#nsubs');
            if (this.checked) {
                $nsubs.prop('readOnly', false).removeClass("search_input_disabled").addClass("search_input");
            } else {
                $nsubs.prop('readOnly', true).removeClass("search_input").addClass("search_input_disabled");
            }
        });

        $('#nsubs').change(function () {
            this.value = isNaN(this.value) ? 0 : this.value;
            this.value = this.value.length == 0 ? 0 : this.value
        });
    }


    var try2search = function (e) {
        e.stopPropagation;
        var kk;
        kk = e.keyCode;
        console.log(kk);
        if (!(kk == 13 || String(kk) == "undefined")) {
            return;
        }

        
        $.post("/TPrice/NewId", { "id": selection_id }, function (data) {
            lock_button('#btn_search');
            selection_id = data;
            SearchNow(1);
        },"html")
    }

    var CalcTPrice = function (id) {
        var ids = "";
        if ($("#ids").val().length > 0) {
            ids = $("#ids").val().substring(1, $("#ids").val().length);
        }

        var $container = $("#tprice_container");
        $container.empty();

        $.post("/TPrice/Calculate", { "id": id, "ids": ids }, function (data) {
            unlock_button('#btn_calc');
            $container.html();
            if (data.values.length == 0) {
                $container.html("<span class=\"non_result\">К сожалению, количество пригодных для расчет компаний менее минимально-допустимого (4 шт.). Уточните запрос.</span>")
            } else {
                $('#search_switcher').show();
                $("#base_block").add('#search_result').hide();


                var $export_block = $('<div>').addClass("export_block");

                $export_block.append('<div style="float:right;"><span class="icon-file-excel icon" id="btn_export_tprice"></span></div>');

                $container.append($export_block);

                $('#btn_export_tprice').click(function () { export_tprice(); });

                var result_table = ["<table class=\"data_table\">"];
                result_table.push("<tr><th>Значение усредненного показателя</th> \
                                    <th>Валовая рентабельность</th> \
                                    <th>Валовая рентабельность затрат</th> \
                                    <th>Рентабельность продаж</th> \
                                    <th>Рентабельность затрат</th> \
                                    <th>Рентабельность коммерческих и управленческих расходов</th> \
                                    <th>Рентабельность активов</th></tr>");
                for (var i = 0; i < data.values.length; i++) {
                    var val = data.values[i];
                    result_table.push("<tr><td>" + val.name + "</td>");
                    result_table.push("<td>" + val.v1 + "</td>");
                    result_table.push("<td>" + val.v2 + "</td>");
                    result_table.push("<td>" + val.v3 + "</td>");
                    result_table.push("<td>" + val.v4 + "</td>");
                    result_table.push("<td>" + val.v5 + "</td>");
                    result_table.push("<td>" + val.v6 + "</td></tr>");
                }
                result_table.push("</table>");
                $container.append(result_table.join(""));
                var $hidden = $("<input id=\"tprice_result\" type=\"hidden\"/>");
                $hidden.val(JSON.stringify(data));
                $container.append($hidden);
            }
        }, "json");
    }

    var export_tprice = function () {
        if (!getObj("calc_tab")) {
            ifr = document.createElement("iframe");
            ifr.className = "service_frame"
            ifr.name = "calc_tab"
            ifr.id = "calc_tab"
            ifr.src = "about:blank";
            document.body.appendChild(ifr);

            
        }
        iframepost({ "result": $("#tprice_result").val() }, "/TPrice/CalculateExcel", "calc_tab");
    }

    var restrictGropName = function (groupName, length) {
        length = length || 65;
        if (groupName.length > length)
            groupName = groupName.substring(0, length) + "...";
        return groupName;
    }

    var _hide_command_menu = function (e) {

        if (group_menu_open) {
            $('.sub_command_menu').remove();
            group_menu_open = false;
        }

        if (excel_menu_open) {
            $('.sub_command_menu').remove();
            excel_menu_open = false;
        }
    }

    var doSave2XLSSelected = function() {
        var issuers="";
        if ($("input:checkbox:checked[name=selsissuer]").length > 0) {
            $("input:checkbox:checked[name=selsissuer]").each(function (i) {
                issuers += String(this.value).split("_")[0] + ",";
            })
            issuers = issuers.substring(0, issuers.length - 1);
            SearchNow(-2000,null,issuers);
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для экспорта</p>', 3000);
        }

    }

    var xlsExportTop10000 = function () {
        SearchNow(-2000);
    }

    window.USERGROPS = [];

    window.doSave2List = function (issuer_id) {
        if ($(".res_item").find('input:checkbox:checked').length > 0 || issuer_id) {
            $.post("/Modules/GetGroups", null,
                        function (data) {
                            var err = 0
                            var htm = "<div id=\"group_dialog\"><h4>Выберите группу</h4><div class=\"scroll_select\"><table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"dpw\" >"
                            st_max = 0
                            try {
                                data = eval(data)
                                USERGROPS = data;
                            } catch (e) {
                                err = 1
                            }
                            if (err == 0) {
                                if (!data[0].name && getObj("modal_dialog")) {
                                    close_dialog();
                                } else {
                                    for (var i = 0; i < data.length; i++) {
                                        htm += "<tr><td  id=\"tdhover" + i + "\" class=\"bones_pointer\" ><a href=\"#\"   onclick=\"dispgroup(this,'" + data[i].name + "'," + data[i].lid + ")\" id=\"s" + data[i].lid + "\">" + data[i].name + "</a></td></tr>";
                                    }
                                }
                            }
                            htm += "</table></div>"
                            htm += "<h4>или создайте новую</h4><div><label class=\"label_form\" style=\"width:150px;\">Новая группа:</label><input type=\"text\" class=\"search_input\" style=\"width:350px;\" id=\"ngroup\"/></div><div id=\"error_groups\" class=\"error_login\"></div>"
                            htm += "<div style='text-align:center;margin:20px 0;'><button  class=\"btns darkblue\" onclick=\"doSaveGroups('" + issuer_id + "');\" >Сохранить</button></div></div>";

                            show_dialog({ "content": htm, "is_print": false });
                            $("#group_dialog").click(function (event) {
                                event.stopPropagation();

                            });

                        }, "html");
        } else {
            showwin('critical', '<p align=center>Не отмечено ни одно предприятие для включения в группу</p>', 3000);
        }
    };

    window.dispgroup= function(elem, name, id) {
        $("#dpw").find("a").each(function (i) {
            $(this).removeClass("selected");
        })

        $("#" + elem.id).addClass("selected");
        g_id = id;
        $("#ngroup").val("");


    }

    var doCreateGroup1000 = function (event) {
        event.stopPropagation();
        _hide_command_menu(event);
        var html = "<div id=\"group100dialog\"><div class=\"form-group\"><label class=\"label_form\" style=\"width:250px;\">Введите уникальное имя группы:</label><input type=\"text\" class=\"search_input\" style=\"width:250px;\" id=\"ngroup\"/></div>";
        html += "<div style='text-align:center;margin:20px 0;'><button  class=\"btns darkblue\" onclick=\"doSearch1000();\" >Сохранить</button></div></div>";
        show_dialog({ "content": html, "is_print": false, $placer: $("#main") });
        $("#group100dialog").click(function (event) {
            event.stopPropagation();

        });
    };

    var save_template_dialog = function (event) {
        event.stopPropagation();
        var html = "<div id=\"templatedialog\"><div class=\"form-group\"><label class=\"label_form\" style=\"width:250px;\">Введите имя шаблона:</label><input type=\"text\" class=\"search_input\" style=\"width:250px;\" id=\"ntemplate\"/></div>";
        html += "<div style='text-align:center;margin:20px 0;'><div class=\"error_list\"></div><button  class=\"btns darkblue\" onclick=\"save_template();\" >Сохранить</button></div></div>";
        show_dialog({ content: html, $placer: $("#main") });
        $("#templatedialog").click(function (event) {
            event.stopPropagation();

        });
    }

    window.save_template = function () {
        var $error_list = $('.error_list');
        $error_list.html('');
        var name = $('#ntemplate').val();
        if (name.length == 0) {
            $error_list.html("<ul><li>Введите имя шаблона</li></ul>");
        } else {
            var tp = get_so_params();
            $.post("/TPrice/SaveTemplate", { name: name, template: JSON.stringify(tp) }, function (data) {
                close_dialog();
                showwin("info", "<p align=\"center\">Сохранено</p>", 2000);
            });
        }
    }

    window.doSaveGroups = function(issuer_id) {
        if ($("#ngroup").val().length > 0) {
            g_id = 0;
            var new_name = $("#ngroup").val();
            for (var i = 0; i < USERGROPS.length; i++) {
                if (USERGROPS[i].name == new_name.trim()) {
                    $('#error_groups').html('Данное имя группы уже существует');
                    return;
                }
            }
        }
        var issuers = (String(issuer_id) != "undefined") ? issuer_id : "";

        $(".res_item").find("input:checkbox:checked").each(function (i) {
            issuers += this.value + ",";
        })

        var aiss = issuers.split(",");

        $.post("/Modules/SaveGroup/", { "id": g_id, "iss": issuers, "newname": $("#ngroup").val(), "is1000": false },
            function (data) {
                if (data.length > 0) {
                    showwin("critical", data, 0);
                } else {
                    close_dialog();
                    showwin("info", "<p align=\"center\">Сохранено</p>", 2000);

                }
            }
        )
    }

    window.doSearch1000 = function () {
        var group_name = $('#ngroup').val();
        if (group_name == "") {
            showwin('critical', '<p align=center>Отсутствует имя группы</p>', 2000);
            return;
        }
        $.get("/Modules/GetGroups", function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].name == group_name.trim()) {
                    showwin('critical', '<p align=center>Данное имя группы уже существует</p>', 2000);
                    return;
                }
            }
            showClock();
            SearchNow(-1000, group_name);
        }, "json");
        
    };



    window.show_param_selector = function (event, id) {
        if (window.event) {
            event = window.event;
        }
        if (event.stopPropagation) {
            event.stopPropagation();
        } else {
            event.cancelBubble = true;
        }
        var year = $("#yearVal" + id).attr("val");
        if (year && year !== "") {
            tree_active = "paramSelect" + id;
            $("#" + tree_active).css({ 'background-color': '#F0F0F0' });
            var scw = window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;
            if (!getObj(tree_active + "_window")) {
                d = document.createElement("iframe");
                d.className = "tree_frame";
                d.id = tree_active + "_window";

                d.frameBorder = "0"
                d.src = "/QIVParam/QIVParamSelector?year=" + year + "&id=" + id;
                var content = "<div id=\"td_" + tree_active + "\"></div>";
                show_dictionary();
                getObj("td_" + tree_active).appendChild(d);

                showContentClock('#dic_container .modal-dialog');

                $("#dic_container").click(function (event) {

                })
            }
        }
    };

    window.show_yearSelector = function (name) {
        var d;


        if (!getObj("yearSelector" + name)) {
            d = document.createElement("div");
            d.id = "yearSelector" + name;
            d.className = "dp_window";

            var elem = $("#yearSelect" + name);
            var boundes = elem.parent().position();
            d.style.top = boundes.top + content_top + $("#yearSelect" + name).height() + "px";
            d.style.left = boundes.left + elem.position().left + "px";
            d.style.width = (elem.outerWidth() - 2) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("yearSelector" + name));
            $("#yearSelect" + name).removeClass("customSelect_focus");
        }
        if (getObj("yearSelector" + name)) {
            $("#yearSelect" + name).addClass("customSelect_focus");
            var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">"
            for (var i = 0; i < yearList.length; i++) {
                htm += "<tr class='selec_option'  onClick='selectNewYear("
                 + i + ",\"" + name + "\");'><td id='tdgroup" + i + "' class='bones_pointer'>" + yearList[i] + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            d.style.display = "block";
            $("#cnttbl").one("mouseenter", function () {
                $('#tdgroup' + $('#yearVal' + name).attr('val')).attr('className', 'bones_pointer');
            })

        }
    }

    window.show_tmplSelector = function () {
        $.get("/Tprice/GetTemplates", function (data) {
            var d;


            if (!getObj("tmplSelector")) {
                d = document.createElement("div");
                d.id = "tmplSelector";
                d.className = "dp_window";


                var boundes = $("#tmplSelect").parent().position();
                d.style.top = boundes.top + content_top + $("#tmplSelect").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#tmplSelect").width() + 12) + "px";
                document.body.appendChild(d);
            }
            else {
                document.body.removeChild(getObj("tmplSelector"));
                $("#tmplSelect").removeClass("tmplSelect_focus");
            }
            if (getObj("tmplSelector")) {
                $("#tmplSelect").addClass("customSelect_focus");
                var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"tmpltbl\">"
                for (var i = 0; i < data.length; i++) {
                    htm += "<tr class='selec_option'  onClick='selectNewTemplate("
                     + data[i].id + ",\""+data[i].name+"\");'><td  class='bones_pointer'>" + data[i].name + "</td></tr>";
                }
                htm += "</table>";
                d.innerHTML = htm;
                d.style.display = "block";

            }
        });
    }

    window.show_groupSelector = function () {
        $.get("/Tprice/GetGroups", function (data) {
            extGroupList = data;
            var d;
            if (!getObj("groupSelector")) {
                d = document.createElement("div");
                d.id = "groupSelector";
                d.className = "dp_window";


                var boundes = $("#groupSelect").parent().position();
                d.style.top = boundes.top + content_top + $("#groupSelect").height() + "px";
                d.style.left = boundes.left + "px";
                d.style.width = ($("#groupSelect").width() + 12) + "px";
                document.body.appendChild(d);
            }
            else {
                document.body.removeChild(getObj("groupSelector"));
                $("#groupSelect").removeClass("customSelect_focus");
            };
            if (getObj("groupSelector")) {
                $("#groupSelect").addClass("customSelect_focus");
                var selected_i = 0;
                var htm = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" id=\"grouptbl\" >"
                for (var i = 0; i < data.length; i++) {
                    if (data[i].code - $("#gropVal").attr("val") == 0) {
                        selected_i = i;
                    }
                    htm += "<tr class='bones_pointer'  onClick='selectNewGroup("
                     + i + ");'><td class='bones_pointer' >" + data[i].txt + "</td></td><td class='bones_pointer' style='text-align:right;width:90px;color:#999;font-size:14px;white-space:nowrap;'>"
                      + (data[i].val == "" ? "все&nbsp;ЮЛ и ИП" : data[i].val) + "</td></tr>";
                }
                htm += "</table>";
                d.innerHTML = htm;
                if (data.length > 10)
                    d.style.height = "180px";
                d.style.display = "block";
                d.scrollTop = 9 * selected_i
            }
        }, "json");
        
    }


    window.show_countSelector = function () {
        var d;


        if (!getObj("countSelector")) {
            d = document.createElement("div");
            d.id = "countSelector";
            d.className = "dp_window";


            var boundes = $("#countSelect").parent().position();
            d.style.top = boundes.top + content_top + $("#countSelect").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#countSelect").width() + 12) + "px";
            document.body.appendChild(d);
        }
        else {
            document.body.removeChild(getObj("countSelector"));
            $("#countSelect").removeClass("customSelect_focus");
        }
        if (getObj("countSelector")) {
            $("#countSelect").addClass("customSelect_focus");
            var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">"
            for (var i = 0; i < countParams.length; i++) {
                htm += "<tr class='selec_option'  onClick='selectNewCount("
                 + i + ");'><td class='bones_pointer'>" + countParams[i] + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            d.style.display = "block";

        }
    }

    window.selectNewCount = function (i) {
        $("#countVal").text(countParams[i]);
        $("#countVal").attr("val", countParams[i]);
        document.body.removeChild(getObj("countSelector"));
        $("#countSelect").removeClass("customSelect_focus");
    }

    window.show_PSelector = function () {
        var d;


        if (!getObj("nperiods")) {
            d = document.createElement("div");
            d.id = "nperiods";
            d.className = "dp_window";


            var boundes = $("#dnp").parent().position();
            d.style.top = boundes.top + content_top + $("#dnp").height() + "px";
            d.style.left = boundes.left + "px";
            d.style.width = ($("#dnp").width() + 12) + "px";
            document.body.appendChild(d);            
        }
        else {
            document.body.removeChild(getObj("nperiods"));
            $("#dnp").removeClass("customSelect_focus");
        }
        if (getObj("nperiods")) {
            $("#dnp").addClass("customSelect_focus");
            var htm = "<table width=\"100%\" cellspacin=\"0\" cellpadding=\"0\" id=\"cnttbl\">"
            for (var i = 2; i < 4; i++) {
                htm += "<tr class='selec_option'  onClick='select_periods("
                 + i + ");'><td id='tdgroup" + i + "' id='tdgroup" + i + "' class='bones_pointer' >" + i + " года</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            d.style.display = "block";
            $("#cnttbl").one("mouseenter", function () {
                $('#tdgroup' + $('#countVal').attr('val')).attr('className', 'bones_pointer');
            })

        }
    }

    window.select_periods =  function (i) {

        $("#snp").text(i + " года");
        $("#snp").attr("val", i);
        document.body.removeChild(getObj("nperiods"));
    }


    window.selectNewGroup = function (i) {
        $("#gropVal").text(restrictGropName(extGroupList[i].txt));
        $("#gropVal").attr("val", extGroupList[i].code);
        document.body.removeChild(getObj("groupSelector"));
        $("#groupSelect").removeClass("customSelect_focus");
    }

    window.selectNewTemplate = function (id, name) {
        $("#tmplVal").text(restrictGropName(name));
        document.body.removeChild(getObj("tmplSelector"));
        $.get("/TPrice/GetTemplate", { id: id }, function (data) {
            clear();
            restore_so_params(data, { id: id, name: name });
        });
    }


    window.selectNewYear = function (i, name) {
        $("#yearVal" + name).text(yearList[i]);
        $("#yearVal" + name).attr("val", yearList[i]);
        document.body.removeChild(getObj("yearSelector" + name));
        $("#yearSelect" + name).removeClass("customSelect_focus");
        getDefaultParam(name);
    }

    window.ch_ind_type = function (i_type) {
        $("#ind").val("");
        $("#ind_val").val("");
        $("#ind_excl").val("");
        ind_type = i_type.value;
    }

    window.Write_TS = function (retval, is_excl) {
        if (retval.length > 0) {
            $.post("/Tree/GetResultString", { "src": tree_table, "id": retval },
            function (data) {
                $("#" + tree_active).val(((is_excl == -1) ? "Искл.: " : "") + data);
                $("#" + tree_active).attr({ "title": ((is_excl == -1) ? "Исключая: " : "") + data.replace(",", ",\n") });
                $("#" + tree_active + "_excl").val(is_excl);
                $("#" + tree_active + "_val").val(retval);
            })
        } else {
            $("#" + tree_active).val("");
            $("#" + tree_active).attr({ "title": eval("titles." + tree_active) });
            $("#" + tree_active + "_excl").val(0);
            $("#" + tree_active + "_val").val("");
        }
        hidepopups();

    };


    window.checkOnOff_Sel = function (o) {
        if (o.checked) {
            $("#ids").val($("#ids").val() + "," + $(o).attr("v"))
        } else {
            var Re = new RegExp("(," + $(o).attr("v") + ")", "ig");
            $("#ids").val($("#ids").val().replace(Re, ""));
        }
    }

    window.doSetCheckedAll = function (chb) {
        $("#search_result").find("input:checkbox").each(function (i) {
            this.checked = chb.checked;
            if ($(this).attr("id") != "selallbox") {
                if (this.checked) {
                    $("#ids").val($("#ids").val() + "," + $(this).attr("v"))
                } else {
                    var Re = new RegExp("(," + $(this).attr("v") + ")", "ig");
                    $("#ids").val($("#ids").val().replace(Re, ""));
                }
            }
        });
    }

    $(document).ready(function () {
        addParam();
        init_inputs();
        $('body').on('click', function (e) {
            hidepopups(e);
            _hide_command_menu(e);
        })
        .on("keypress", function (e) { try2search(e); });
        $('#btn_search').click(function (event) {
            if (roles_object.canSearch) {
                try2search(event);
            } else {
                no_rights();
            }
        });

        $('#btn_clear').click(function (event) {
            clear();
        })

        $('#btn_save_template').click(function (event) {
            if (roles_object.canSearch) {
                save_template_dialog(event);
            } else {
                no_rights();
            }
        })

        $('#search_switcher').click(function () {

            var $switch = $(this);

            if ($switch.hasClass("icon-angle-up")) {
                $switch.removeClass("icon-angle-up").addClass("icon-angle-down").text('Показать результаты поиска');
                $("#base_block").add('#search_result').hide();
            } else {
                $switch.removeClass("icon-angle-down").addClass("icon-angle-up").text('Скрыть результаты поиска');
                $("#base_block").add('#search_result').show();
            }
        })
    })


})();