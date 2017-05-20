function tree_toggle(e, node_id) {
    var clickedElem;
    e = e || window.e;
    clickedElem = e.target || e.srcElement;
    if (!hasClass(clickedElem, 'Expand')) {
        return;
    }
    node = clickedElem.parentNode;
    if (hasClass(node, 'ExpandLeaf')) {
        return;
    }
    if (!node.getElementsByTagName('LI').length) {
        getNodes(src, node.id, 0, 0);
    }
    var newClass = hasClass(node, 'ExpandOpen') ? 'ExpandClosed' : 'ExpandOpen';
    var re = /(^|\s)(ExpandOpen|ExpandClosed)(\s|$)/;
    node.className = node.className.replace(re, '$1' + newClass + '$3');
}
function hasClass(elem, className) {
    return new RegExp("(^|\\s)" + className + "(\\s|$)").test(elem.className);
}
function toggle_chb(obj) {
    if (obj == null)
        return;
    if (String(obj.id).substring(3, 10) != "0") {
        var closestNode = $(obj).closest("ul");
        var closestLI = $(obj).closest("li");
        var allCheckboxes = closestNode.find('input:checkbox');
        var checkedCheckboxes = closestNode.find('input:checkbox:checked');
        var allChecked = allCheckboxes.length == checkedCheckboxes.length;
        var parentCheckbox = closestNode.closest('li').find('input:checkbox');
        if (parentCheckbox.length > 0) {
            parentCheckbox.get(0).checked = allChecked;

            if (!allChecked && checkedCheckboxes.length > 0) {
                parentCheckbox.get(0).disabled = true;
                parentCheckbox.get(0).checked = true;
                DivOnCheckBox(parentCheckbox.get(0));
            } else {
                if (allChecked) {
                    parentCheckbox.get(0).disabled = false;
                    parentCheckbox.get(0).checked = true;
                    var span = document.getElementById("sp" + parentCheckbox.get(0).id.substring(3, 10));
                    span.style.display = "none";
                } else {
                    parentCheckbox.get(0).disabled = false;
                    parentCheckbox.get(0).checked = false;
                    var span = document.getElementById("sp" + parentCheckbox.get(0).id.substring(3, 10));
                    span.style.display = "none";
                }
            }
            toggle_chb(parentCheckbox.get(0));
        }
    }
}
function checkCheckbox(nNode) {
    $(nNode).closest('li').find('input:checkbox').each(function () {
        this.checked = $(nNode).prop('checked');
        this.disabled = (this.checked) ? this.disabled : false;

    });
    toggle_chb(nNode);
    chbClick();
};
function DivOnCheckBox(chb) {
    var span = document.getElementById("sp" + chb.id.substring(3, 10));
    span.style.display = "block";
}
function EnableChB(span) {
    var chb_id = "chb" + String(span.id).substring(2, 10);
    var chb = document.getElementById(chb_id);
    chb.disabled = false;
    chb.checked = false;
    checkCheckbox(chb);

}