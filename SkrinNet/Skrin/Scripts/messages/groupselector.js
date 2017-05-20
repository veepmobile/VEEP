(function () {

    var restrictGropName = function (groupName) {
        if (groupName.length > 65)
            groupName = groupName.substring(0, 65) + "...";
        return groupName;
    }

    window.selectNewGroup = function (i) {
        $("#gropVal").text(restrictGropName(extGroupList[i].txt));
        $("#gropVal").attr("val", extGroupList[i].code);
        document.body.removeChild(getObj("groupSelector"));
        $("#groupSelect").removeClass("customSelect_focus");
        search_check_changes();
    }

    window.show_groupSelector = function () {
        var d;

        if (!getObj("groupSelector")) {
            d = document.createElement("div");
            d.id = "groupSelector";
            d.className = "dp_window";


            var boundes = $("#groupSelect").parent().position();
            d.style.top = boundes.top + 94 + $("#groupSelect").height() + "px";
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
            for (var i = 0; i < extGroupList.length; i++) {
                if (extGroupList[i].code - $("#gropVal").attr("val") == 0) {
                    selected_i = i;
                }
                htm += "<tr class='bones_pointer'  onClick='selectNewGroup("
                 + i + ");'><td id='tdgroup" + i + "' class='bones_pointer' >" + extGroupList[i].txt + "</td></td><td class='bones_pointer' style='text-align:right;width:90px;color:#999;font-size:14px;white-space:nowrap;'>"
                  + (extGroupList[i].val == "" ? "все&nbsp;ЮЛ и ИП" : extGroupList[i].val) + "</td></tr>";
            }
            htm += "</table>";
            d.innerHTML = htm;
            if (extGroupList.length > 10)
                d.style.height = "180px";
            d.style.display = "block";
            d.scrollTop = 9 * selected_i
            $("#grouptbl").one("mouseenter", function () {
                $('#tdgroup' + $('#gropVal').attr('val')).attr('className', 'bones_pointer');
            })
        }
    }
})();