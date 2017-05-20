(function () {

    var now = new Date();
    var tree_active;
    var tree_table;
    var src;
    var SO = {};
    var Groups = [];
    var Types = [];
    var IsCheckAll = 0;


    var multiple_options = {
        selectAllText: 'Все',
        allSelected: 'Все',
        countSelected: '# из %',
        selectAllDelimiter: ['', ''],
        onCheckAll: function () {IsCheckAll = 1;},
        onUncheckAll: function () { IsCheckAll = 0; },
        onClick: function () { if (IsCheckAll == 1) IsCheckAll = 0 },
        onOptgroupClick: function () { IsCheckAll = 0; }
    }


    var init_ev_calendar = function () {
       // if (user_id != 0) {

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
       // }
    }

    function save_search_params() {
        SO.dBeg = $('#dfrom').val();
        SO.dBeg = /\d{2}\.\d{2}\.\d{4}/.test(SO.dBeg) ? SO.dBeg : "";
        SO.dEnd = $('#dto').val();
        SO.dEnd = /\d{2}\.\d{2}\.\d{4}/.test(SO.dEnd) ? SO.dEnd : "";
        if (typeof (SO.type_id) == 'undefined') SO.type_id = '';
        if (IsCheckAll == 0) {
            $('select').multipleSelect(multiple_options);
            SO.type_id = $('select').multipleSelect('getSelects').toString();
        }
        else {
            if (SO.type_id !== '') SO.type_id = '';
            $.each(Groups, function (i, item) { SO.type_id += item.id + ','; }); SO.type_id = SO.type_id.substring(0, (SO.type_id.length - 1));
        }
        check_parents();
        SO.author_search = $('#author_search').val();
        SO.text_search = $('#text_search').val();
    }



    window.reviews_init = function () {
        init_group_select();
        init_ev_calendar();   

        $('#btn_find').click(function () {          
            rev_search(1);
        });

        $('#btn_clear').click(function () {
            $("#author_search").val('');
            $("#text_search").val('');
            $("#dfrom").val('');
            $("#dto").val('');
            $("#types").val('');
            $("#types_val").val('');
            $("#types_excl").val('0');
            $('select').multipleSelect('uncheckAll');
        });

        rev_search(1);
    }



    window.rev_search = function (page) {
        showClock();
        save_search_params();
        SO.page = page;
        SO.rcount = 20;
        var url = "/Analytics/ReviewsSearchAsync";
        var params = SO.toString();
        $.ajax({
            url: url,
            type: "POST",
            data: params,
            success: function (data) {
                hideClock();
                if (data) {
                    GenerateResult(data, page, SO.rcount);
                }
            }
        });
    }


    function GenerateResult(data, page, count) {
        var $content = $('#search_result');
        var $s_count = $('#search_count');
        var $table;

        $content.text('');
        var total = data.total;
        var page_count = total / 20;
        if (page_count % 1 > 0) {
            page_count = (page_count - (page_count % 1)) + 1;
        }
        if (total == 0) {
            total_text = "<span class=\"total_count\">Нет данных соответствующих заданному условию</span>";
        }
        else {
            total_text = "<span class=\"total_count\">Всего найдено " + total + " сообщений.</span>";
        }
        $('#search_result').append(total_text);


        if (total > 0) {
            $table = '<div class="res_block">' + generate_rows(data.s_result) + '</div>';
            $content.append($table);
            $content.append(_get_paging(page, page_count));
        }
    }

    function generate_rows(rows) {
        var ret = "";        
        for (var i = 0; i < rows.length; i++) {
            var url = get_picture_link(rows[i]);
            ret += '<div class="res_item analytic_result"><div class="check_block" onclick="window.open(\''+url+'\', \'_blank\');">' + getPictureByFileNameNew(rows[i].file_name, '') + '</a></div>' +
                    '<div class="info_block"><span class="news_data news_data_sm">' + rows[i].date + '</span>' +
                    '<div class="comp_info">' + get_document_link(rows[i]) + '</div></div>';
            ret += '<div class="code_block">Тип обзора: ' + rows[i].a_type + '<br/>Автор: <span class="anal_review_author">' + rows[i].author_name + '</span></div>';
            ret += '</div>';
        }
        return ret;
    }

    function get_document_link(row) {
        return '<a style="margin:0;padding:0;" target="_blank" href="/analytics/stats?url=/Documents/Index&doc=' + row.doc_id + '&author=' + row.author_id + '">' + row.headline + '</a>';
    }

    function get_picture_link(row) {
        return '/analytics/stats?url=/Documents/Index&doc=' + row.doc_id + '&author=' + row.author_id;
    }

    var getval = function (val) {
        if (!val) {
            return "";
        }
        return val;
    }


    SO.toString = function () {   
        return "dBeg=" + SO.dBeg + "&dEnd=" + SO.dEnd + "&anal_types=" + SO.type_id + "&anal_types_excl=" + SO.types_excl + "&author_search=" + SO.author_search +
            "&text_search=" + SO.text_search + "&page_no=" + SO.page + "&r_count=20";
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
            html += '<td onclick="rev_search(1);">&laquo;</td>';
        }
        if (page == 1 && page_count > 7) {
            html += '<td class="page_disabled">&laquo;</td>';
        }

        for (var i = StartPage; i < ((page == page_count) ? page_count + 1 : ((StartPage + 7 < page_count) ? StartPage + 7 : page_count + 1)) ; i++) {
            if (i == page) {
                html += '<td class="page_disabled">' + i + '</td>';
            } else {
                html += '<td onclick="rev_search(' + i + ');">' + i + '</td>';
            }
        }
        if (i < page_count + 1) {
            html += '<td onclick="rev_search(' + (page_count) + ');">&raquo;</td>';
        }
        if (i == page_count + 1) {
            html += '<td class="page_disabled">&raquo;</td>';
        }
        //console.log(page_count);
        return '<br/><div id="page_counter"><table>' + html + '</table></div>';
    };

    //Генерация селектора групп пользователя
    var init_group_select = function () {
        $.post("/Analytics/GetTypes/", null, function (data) {
            var option = '';
            $.each(data, function (i, item) {
                var MesType = { id: item.id, name: item.name, parent_id: item.parent_id };
                if (item.parent_id == 0) {
                    Groups.push(MesType);
                }
                else {
                    Types.push(MesType);
                }
            });
            $.each(Groups, function (i, item) {
                option += '<optgroup value="' + item.id + '" label="' + item.name + '">';
                $.each(Types, function (j, item2) {
                    if (item2.parent_id == item.id) {
                        option += '<option value="' + item2.id + '">' + item2.name + '</option>';
                    }
                });
                option += '</optgroup>';
            });
            $('select#mt_list1').append(option).multipleSelect(); 
        });
    }
    
    //Удаляет детей, если полная группа
    var check_parents = function()
    {      
        var types = '';
        var k = 0;
        var kk = 0;      
        $.each(Groups, function (i, group) {           
            var tmp_types = '';
            var sotypes = SO.type_id.split(',');
            for (var i = 0; i < Types.length; i++) {
                if (Types[i].parent_id == group.id) {
                    k++;
                }
            }
            for (var j = 0; j < sotypes.length; j++) {  
                for (var i = 0; i < Types.length; i++) {
                    if ((Types[i].id == sotypes[j]) && (Types[i].parent_id == group.id)) {
                        tmp_types += sotypes[j] + ',';
                        kk++;
                    }
                }
            }        
            if (k == kk) {
                types += group.id + ',';               
            }
            else {                
                if ((types != tmp_types)&& (kk != 0)) {
                     types += tmp_types;
                }          
            }            
            k = 0; kk = 0;
        });

        types = types.substring(0, types.length - 1);
        SO.type_id = types;     
    }

})();

$().ready(function () {
    reviews_init();
});