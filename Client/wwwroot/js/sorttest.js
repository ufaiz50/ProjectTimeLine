function showmodal(id) {
    $('#mediumModal').modal('show')
    showLogTask(id);
    detailTask(id);
}


$(function () {
    $(".sortable_list").sortable({
        connectWith: ".connectedSortable",
        /*stop: function(event, ui) {
            var item_sortable_list_id = $(this).attr('id');
            console.log(ui);
            //alert($(ui.sender).attr('id'))
        },*/
        receive: function (event, ui) {
            //alert("dropped on = " + this.id); //Where the item is dropped
            //alert("sender = " + ui.sender[0].id);  //Where it came from
            //alert("item = " + ui.item[0].getAttribute("id")); //Which item (or ui.item[0].id)
            var id = ui.item[0].getAttribute("id");
            var status = this.id
            UpdateStatus(id, status)
            addHistory(ui.sender[0].id, this.id, ui.item[0].getAttribute("id"))
        }
    }).disableSelection();


});

function UpdateStatus(id, status) {
    $.ajax({
        url: "https://localhost:44374/Task/UpdateStatus",
        dataType: "json",
        dataSrc: "",
        data: {
            TaskId: id,
            Status: status
        }
    }).done(result => {

    }).fail(error => {

    })
}

function addHistory(stateBefore, stateAfter, TaskModulId) {
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = date + ' ' + time + ".0";
    var obj = new Object();
    obj.EndDate = dateTime;
    obj.StateBefore = stateBefore;
    obj.StateAfter = stateAfter;
    obj.NIK = $('#NIK').text();
    obj.TaskModulId = TaskModulId
    $.ajax({
        url: "https://localhost:44374/Task/AddHistory",
        method: "post",
        data: obj
    }).done(result => {
    }).fail(error => { })
}

// Log status in Modal
function showLogTask(id) {
    body = "";
    $.ajax({
        url: "https://localhost:44374/Task/LogStatus/",
        dataType: "JSON",
        dataSrc: "",
        data: {"id": id}
    }).done(result => {
        result.sort(function (a, b) {
            return new Date(b.endDate) - new Date(a.endDate);
        });
        $.each(result, function (key, val) {
            status = enumStatus(val.stateAfter);
            
            var today = new Date(val.endDate);
            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            var dateTime = date + ' ' + time;
            body += `<li class="list-unstyled">
                        <div class="d-flex">
                            <img class="gambar" src="/img/usercartoon.png" alt="" />
                            <div class="pl-3 email">
                                <p>${val.name} <span> Add this Task to ${status}</span></p>
                                <span> ${dateTime}</span>
                            </div>
                        </div>
                    </li>`
        })
        $('#LogHistory').html(body);
    }).fail(error => { })
}

function detailTask(id) {
    $.ajax({
        url: "https://localhost:44374/Task/DetailTask/",
        dataType: "JSON",
        dataSrc: "",
        data: { "id": id }
    }).done(result => { 
        $('#descriptionTask').text(result.description);
        status = enumStatus(result.status)
        $('#statusTask').text(status);
    }).fail(error => { })
}

// Gant View
$('button#GanttView').on('click', function () {
    $('#ListKanban').attr('hidden', true);
    $('#ganttchart').removeAttr('hidden', true);
    apexChart()
})

// Kanban View
$('button#KanbanView').on('click', function () {
    $('#ListKanban').removeAttr('hidden', true);
    $('#ganttchart').attr('hidden', true);
})



// Get Data Project ApexChart Version
function apexChart() {
    projectId = $('#ProjectId').text();
    $.ajax({
        url: "https://localhost:44374/GanttChart/GanttChartView",
        dataType: "Json",
        data: { "ProjectId": projectId }
    }).done(result => {
        apexData = [];
        //group by Data
        project = result.reduce((unique, o) => {
            if (!unique.some(obj => obj.projectName === o.projectName)) {
                unique.push(o);
            }
            return unique;
        }, []);
        modul = result.reduce((unique, o) => {
            if (!unique.some(obj => obj.modulName === o.modulName)) {
                unique.push(o);
            }
            return unique;
        }, []);
        task = result.reduce(function (r, a) {
            r[a.modulName] = r[a.modulName] || [];
            r[a.modulName].push(a);
            return r;
        }, Object.create(null));

        //shorting
        apexData.push({
            "x": project[0].projectName,
            "y": [new Date(project[0].startDate).getTime(), new Date(project[0].endDate).getTime()]
        })
        for (var i in modul) {
            //chartData.push(Object.values(modul[i]))
            apexData.push({
                "x": modul[i].modulName,
                "y": [new Date(modul[i].modulStartDate).getTime(), new Date(modul[i].modulEndDate).getTime()]
            })


            nama = modul[i].modulName
            nama2 = task[nama]
            for (var i in nama2) {
                //chartData.push(Object.values(nama2[i]))
                apexData.push({
                    "x": nama2[i].taskName,
                    "y": [new Date(nama2[i].taskStartDate).getTime(), new Date(nama2[i].taskEndDate).getTime()]
                })
            }
        }

        var options = {
            series: [
                {
                    data: apexData
                }
            ],
            chart: {
                height: 350,
                type: 'rangeBar'
            },
            plotOptions: {
                bar: {
                    horizontal: true
                }
            },
            dataLabels: {
                enabled: true,
                formatter: function (val, opts) {
                    var label = opts.w.globals.labels[opts.dataPointIndex]
                    var a = moment(val[0])
                    var b = moment(val[1])
                    var diff = b.diff(a, 'days')
                    return label + ': ' + diff + (diff > 1 ? ' days' : ' day')
                },
                style: {
                    colors: ['#f3f4f5', '#fff']
                }
            },
            xaxis: {
                type: 'datetime'
            }
        };

        var chart = new ApexCharts(document.querySelector("#ganttchart"), options);
        chart.render();



    }).fail(error => { })
}



// Get Data Project GoogleChart Version
function googleChart() {
    projectId = $('#ProjectId').text()
    $.ajax({
        url: "https://localhost:44374/GanttChart/GanttChartView",
        dataType: "Json",
        data: { "ProjectId": projectId },
    }).done(result => {
        var chartData = []

        //group by Data
        project = result.reduce((unique, o) => {
            if (!unique.some(obj => obj.projectName === o.projectName)) {
                unique.push(o);
            }
            return unique;
        }, []);
        modul = result.reduce((unique, o) => {
            if (!unique.some(obj => obj.modulName === o.modulName)) {
                unique.push(o);
            }
            return unique;
        }, []);
        task = result.reduce(function (r, a) {
            r[a.modulName] = r[a.modulName] || [];
            r[a.modulName].push(a);
            return r;
        }, Object.create(null));
        // sorting Data
        date1 = new Date(project[0].startDate).getTime();
        date2 = new Date(project[0].endDate).getTime();
        var InDays = date2 - date1;
        chartData.push([project[0].projectName, project[0].projectName,
            new Date(project[0].startDate), new Date(project[0].endDate), null , 100, null])


        for (var i in modul) {
            //chartData.push(Object.values(modul[i]))
            chartData.push([modul[i].modulName, modul[i].modulName,
                new Date(modul[i].modulStartDate), new Date(modul[i].modulEndDate), null, 60, null])


            nama = modul[i].modulName
            nama2 = task[nama]
            for (var i in nama2) {
                //chartData.push(Object.values(nama2[i]))
                if (i == 0) {
                chartData.push([nama2[i].taskName, nama2[i].taskName,
                    new Date(nama2[i].taskStartDate), new Date(nama2[i].taskEndDate), null, 49, null])
                } else {
                    chartData.push([nama2[i].taskName, nama2[i].taskName,
                    new Date(nama2[i].taskStartDate), new Date(nama2[i].taskEndDate), null, 49, nama2[i - 1].taskName])
                }

            }
        }
        google.charts.load('current', { 'packages': ['gantt'] });
        google.charts.setOnLoadCallback(drawCharts);


        function drawCharts() {

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Task ID');
            data.addColumn('string', 'Task Name');
            data.addColumn('date', 'Start Date');
            data.addColumn('date', 'End Date');
            data.addColumn('number', 'Duration');
            data.addColumn('number', 'Percent Complete');
            data.addColumn('string', 'Dependencies');

            data.addRows(chartData);

            var options = {
                height: data.getNumberOfRows() * 45,
                gantt: {
                    trackHeight: 40
                }
            };

            var chart_div = document.getElementById('ganttchart');
            var chart = new google.visualization.Gantt(chart_div);

            // Wait for the chart to finish drawing before calling the getImageURI() method.
            google.visualization.events.addListener(chart, 'ready', function () {
                document.getElementById('png').innerHTML = '<a href="' + chart.getImageURI() + '">Printable version</a>';
            });


            chart.draw(data, options);
        }
    }).fail(error => { })
}

// Enum Status
function enumStatus(status) {
    switch (status) {
        case 1:
            return "Design";
            break;
        case 2:
            return "Doing"
            break;
        case 3:
            return "CodeReview"
            break;
        case 4:
            return "Testing"
            break;
        case 5:
            return "Done"
            break;
        default:
            return "To-Do"
    }
}

// Format DateTime
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month, year].join('/');
}

// TimeLIneChart
function daysToMilliseconds(days) {
    return days * 24 * 60 * 60 * 1000;
}