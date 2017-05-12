var get_chars = function (result) {

    if (result.year && result.year.length > 0) {
        var data = {
            labels: result.year,
            datasets: [
                {
                    label: "Предприятие",
                    borderColor: 'rgba(85,132,255,1)',
                    backgroundColor: 'rgba(85,132,255,0.3)',
                    borderWidth: 3,
                    data: result.al,
                    fill: false
                },
                 {
                     label: "Среднее по отрасли",
                     borderColor: "rgba(135,135,135,1)",
                     backgroundColor: 'rgba(135,135,135,0.3)',
                     borderWidth: 3,
                     data: result.ali,
                     fill: false
                 },
                {
                    label: "70% - хорошо",
                    borderColor: "rgba(117,200,77,1)",
                    backgroundColor: 'rgba(117,200,77,0.3)',
                    borderWidth: 1,
                    borderDash: [5, 5],
                    data: [70, 70, 70, 70, 70],
                    pointRadius: 0,
                    fill: false
                },
                {
                    label: "30% - плохо",
                    borderColor: "rgba(204,51,0,1)",
                    backgroundColor: 'rgba(204,51,0,0.3)',
                    borderWidth: 1,
                    borderDash: [5, 5],
                    data: [30, 30, 30, 30, 30],
                    pointRadius: 0,
                    fill: false
                }
            ]
        };

        var data2 = {
            labels: result.year,
            datasets: [
                {
                    label: "Предприятие",
                    borderColor: 'rgba(85,132,255,1)',
                    backgroundColor: 'rgba(85,132,255,0.3)',
                    borderWidth: 3,
                    data: result.os,
                    fill: false
                },
                 {
                     label: "Среднее по отрасли",
                     borderColor: "rgba(135,135,135,1)",
                     backgroundColor: 'rgba(135,135,135,0.3)',
                     borderWidth: 3,
                     data: result.osi,
                     fill: false
                 },
                {
                    label: "50% - хорошо",
                    borderColor: "rgba(117,200,77,1)",
                    backgroundColor: 'rgba(117,200,77,0.3)',
                    borderWidth: 1,
                    borderDash: [5, 5],
                    data: [50, 50, 50, 50, 50],
                    pointRadius: 0,
                    fill: false
                },
                {
                    label: "10% - плохо",
                    borderColor: "rgba(204,51,0,1)",
                    backgroundColor: 'rgba(204,51,0,0.3)',
                    borderWidth: 1,
                    borderDash: [5, 5],
                    data: [10, 10, 10, 10, 10],
                    pointRadius: 0,
                    fill: false
                }
            ]
        };

        var options = {
            tooltips: {
                enabled: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        max: 105,
                        min: -5,
                        stepSize: 20
                    },
                    display: false
                }]
            }
        };

        var options1 = {
            tooltips: {
                enabled: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        max: 75,
                        min: -5,
                        stepSize: 20
                    },
                    display: false
                }]
            }
        };

        var ctx = document.getElementById("myChart");
        var myBarChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
        });

        var ctx1 = document.getElementById("myChart1");
        var myBarChart1 = new Chart(ctx1, {
            type: 'line',
            data: data2,
            options: options1
        });
    }
}

INIT_FUNCT["Graph"] = function () {
    $.post("/Graph/Index/", { "ticker": ISS }, function (data) {
        get_chars(data);
    }, "json");
    
};