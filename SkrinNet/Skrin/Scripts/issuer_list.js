var msgSelAll = "Выделить все";
var msgClrAll = "сброс";

function doLoad() { }
function saveSelected() { }

function doSearch() {
    var frm = document.searchForm;
    frm.userlistselector.value = 0;
    //	frm.loadlist.value = "no";
    //	frm.page.value = 1;
}

function doLoadList() {
    var frm = document.searchForm;
    if (frm.userlistselector.value != '0') {
        //		frm.loadlist.value = 'yes';
        //		frm.page.value = 1;
        frm.submit();
    }
}

function gotoULEdit() {
    var frm = document.searchForm;
    window.location.href = "/search/UserLists/default.asp?lstid=" + frm.userlistselector.value;
}



function doSetChecked(oInp) {
    var flag = (oInp.checked);
    var coll = oInp.form.elements;
    var arrImgs = new Array();
    for (var i = 0; i < coll.length; i++) {
        if (coll[i].name == "selsissuer") {
            coll[i].checked = flag;
        }
    }
}






function closeGroupDialog() {
    close_dialog();
}
var g_id = 0;
function doSaveGroups(issuer_id) {
    if ($("#ngroup").val().length > 0) {
        g_id = 0;
    }
    var issuers = (String(issuer_id) != "undefined" && String(issuer_id) != "0") ? issuer_id : "";

    $(".res_item").find("input:checkbox:checked").each(function (i) {
        issuers += this.value + ",";
    })

    var aiss = issuers.split(",");

    $.post("/Modules/SaveGroup/", { "id": g_id, "iss": issuers, "newname": $("#ngroup").val(),"is1000":false },
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





function doSaveToUserList(issuers, group_name,skip_mess) {

    $.post("/Modules/SaveGroup/", { "iss": issuers, "newname": group_name, "is1000": true },
        function (data) {
            if (data.length > 0) {
                showwin("critical", data, 0);
            } else {
                closeGroupDialog();
                if (!skip_mess) {
                    showwin("info", "<p align=\"center\">Сохранено</p>", 2000);
                }               
            }
        }
    )
}

function dispgroup(elem, name, id) {
    $("#dpw").find("a").each(function (i) {
        $(this).removeClass("selected");
    })

    $("#" + elem.id).addClass("selected");
    g_id = id;
    $("#ngroup").val("");


}
