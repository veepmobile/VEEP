/// <reference path="jquery144.js" />
var StopLight = (function () {
    var ogrn;
    var content;
    var a_content;

    var yellow_color = "#fc8f00";
    var red_color = "#cd2b25";
    var green_color = "#03bc00";
    var grey_color = "#ddd";

    var showActionStopLight = function () {
        var el = document.getElementById('asl'); // get canvas

        var options = {
            percent: el.getAttribute('data-percent'),
            size: 80,
            lineWidth: 10,
            rotate: 0
        }



        var canvas = document.createElement('canvas');
        canvas.className += " asl_canvas"
        var span = document.createElement('span');
        span.className += " asl_span"
        span.textContent = options.percent>=0 ? options.percent : "*" ;

        var color = grey_color;
        if (options.percent > 50) {
            color = green_color;
        } else if (options.percent > 0) {
            color = yellow_color;
        } else if (options.percent == 0) {
            color = red_color;
        }

        span.style.color = color;


        if (typeof (G_vmlCanvasManager) !== 'undefined') {
            G_vmlCanvasManager.initElement(canvas);
        }

        var ctx = canvas.getContext('2d');
        canvas.width = canvas.height = options.size;

        el.appendChild(span);
        el.appendChild(canvas);

        ctx.translate(options.size / 2, options.size / 2); // change center
        ctx.rotate((-1 / 2 + options.rotate / 180) * Math.PI); // rotate -90 deg

        //imd = ctx.getImageData(0, 0, 240, 240);
        var radius = (options.size - options.lineWidth) / 2;

        var drawCircle = function (color, lineWidth, percent) {
            percent = Math.min(Math.max(0, percent || 1), 1);
            ctx.beginPath();
            ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
            ctx.strokeStyle = color;
            ctx.lineCap = 'round'; // butt, round or square
            ctx.lineWidth = lineWidth
            ctx.stroke();
        };


        drawCircle(grey_color, options.lineWidth, 100 / 100);
        if (options.percent >= 0) {
            drawCircle(color, options.lineWidth, options.percent / 100);
        }
    }

    var showStopLight = function () {
        var el = document.getElementById('sl'); // get canvas

        var options = {
            count: el.getAttribute('data-count'),
            color: el.getAttribute('data-color'),
            size: el.getAttribute('data-size') || 120,
            lineWidth: el.getAttribute('data-line') || 15,
            rotate: el.getAttribute('data-rotate') || 0
        }



        var canvas = document.createElement('canvas');
        canvas.className += " sl_canvas"
        var span = document.createElement('span');
        span.className += " sl_span"
        span.textContent = options.count;

        var color = grey_color;
        if (options.color === "Green") {
            color = green_color;
        } else if (options.color === "Yellow") {
            color = yellow_color;
        } else if (options.color === "Red") {
            color = red_color;
        }

        span.style.color = color;


        if (typeof (G_vmlCanvasManager) !== 'undefined') {
            G_vmlCanvasManager.initElement(canvas);
        }

        var ctx = canvas.getContext('2d');
        canvas.width = canvas.height = options.size;

        if (options.count != "0") {
            el.appendChild(span);
        }
        el.appendChild(canvas);

        ctx.translate(options.size / 2, options.size / 2); // change center
        ctx.rotate((-1 / 2 + options.rotate / 180) * Math.PI); // rotate -90 deg

        //imd = ctx.getImageData(0, 0, 240, 240);
        var radius = (options.size - options.lineWidth) / 2;

        var drawCircle = function (color, lineWidth, percent) {
            percent = Math.min(Math.max(0, percent || 1), 1);
            ctx.beginPath();
            ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
            ctx.strokeStyle = color;
            ctx.lineCap = 'round'; // butt, round or square
            ctx.lineWidth = lineWidth
            ctx.stroke();
        };

        drawCircle(color, options.lineWidth, 100 / 100);
    }

    var showprotocol = function () {
        show_dialog({ "content": content, "extra_style": "width:990px;", is_print: true });
    }

    var showaprotocol = function () {
        show_dialog({ "content": a_content, "extra_style": "width:790px;", is_print: true });
        $('#astoplight_issuer_name').html(NAME);
    }

    

    return {
        init: function (id,a_id) {
            var $elem = $("#" + id);
            if ($elem.length) {
                ogrn = $elem.attr("data-ogrn");
                $.get("/StopLight/Index/", { "ogrn": ogrn }, function (data) {
                    content = $(data).filter('#stoplight_content').html();
                    var show_report = $(data).filter('#stoplight_inner').attr("data-show") == "True";
                    $elem.append($(data).filter('#stoplight_inner').html());
                    showStopLight();
                    if (show_report) {
                        $elem.click(function () {
                            showprotocol();
                        });
                    } else {
                        $elem.click(function () {
                            no_rights();
                        });
                    }

                }, "html");
            }
               
            var $aelem = $("#" + a_id);
            if ($aelem.length) {
                $.get("/ActionStopLight/Index/", { "ticker": ISS }, function (data) {
                    a_content = $(data).filter('#stoplight_content').html();
                    var show_report = $(data).filter('#stoplight_inner').attr("data-show") == "True";
                    $aelem.append($(data).filter('#stoplight_inner').html());
                    showActionStopLight();
                    if (show_report) {
                        $aelem.click(function () { showaprotocol(); });
                    } else {
                        $aelem.click(function () { no_rights(); });
                    }

                }, "html");
            }                  
            
        },
        switch_rating_info: function (obj) {
            if ($(obj).find("span[class*='icon-angle-up']").length > 0) {
                $(obj).find("span[class*='icon-angle-up']").html("Подробнее");
                $(obj).find("span[class*='icon-angle-up']").attr("class", "icon-angle-down");

            } else {
                $(obj).find("span[class*='icon-angle-down']").html("Свернуть");
                $(obj).find("span[class*='icon-angle-down']").attr("class", "icon-angle-up");
            }
        },
        show_rating_info: function (id) {
            if ($('#tbl' + id + '_info').is(":visible")) {
                $('#tbl' + id + '_info').hide();
                $('#tbl' + id).hover(function () {
                    $(this).css({ 'background-color': "#f4f1f0" });
                }, function () {
                    $(this).css({ 'background-color': "" });
                });
            } else {
                $('#tbl' + id).css("background-color", "#EEE")
                $('#tbl' + id + '_info').show();
                $('#tbl' + id).hover(function () {
                    $(this).css({ 'background-color': "#f4f1f0" });
                }, function () {
                    $(this).css({ 'background-color': "#eee" });
                });
            }
        }
    }

})();


INIT_FUNCT["StopLight"] = function () {
    StopLight.init("sl_container", "asl_container");
};
/*
$(document).ready(function () {
    StopLight.init("StopLightBox");
});
*/
