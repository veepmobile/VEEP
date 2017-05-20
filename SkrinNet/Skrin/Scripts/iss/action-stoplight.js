(function () {

    var yellow_color = "#fc8f00";
    var red_color = "#cd2b25";
    var green_color = "#03bc00";

    var showActionStopLight = function(){
        var el = document.getElementById('asl'); // get canvas

        var options = {
            percent: el.getAttribute('data-percent'),
            size:  80,
            lineWidth: 10,
            rotate:  0
        }

       

        var canvas = document.createElement('canvas');
        canvas.className += " asl_canvas"
        var span = document.createElement('span');
        span.className += " asl_span"
        span.textContent = options.percent;

        var color = red_color;
        if (options.percent > 50) {
            color = green_color;
        } else if (options.percent > 0)
            color = yellow_color;

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

        
        drawCircle('#ddd', options.lineWidth, 100 / 100);
        drawCircle(color, options.lineWidth, options.percent / 100);
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

        var color = red_color;
        if (options.color === "Green") {
            color = green_color;
        } else if (options.color === "Yellow")
            color = yellow_color;

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

        drawCircle(color, options.lineWidth, 100 / 100);
    }


    $(document).ready(function () {
        showActionStopLight();
        showStopLight();
    })

}())


