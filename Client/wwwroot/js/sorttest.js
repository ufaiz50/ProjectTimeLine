
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

$(document).ready(function () {
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

    $(".search-member").focus(function () {
        $(".show-member").collapse('show');
    });
    /*$(".search-member").focusout(function () {
        $(".show-member").collapse('hide');
    });*/



    //project
    GetModul();

});
// Task

function showmodal(id) {
    $('#mediumModal').modal('show')
    showLogTask(id);
    detailTask(id);
}

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
        $('[id="TaskId"]').val(id)
        $('#descriptionTask').text(result.description);
        status = enumStatus(result.status)
        $('#statusTask').text(status);
        datemodal = new Date(result[0].startDate).toLocaleDateString() + " - " + new Date(result[0].date).toLocaleDateString()
        $('#datemodal').text(datemodal);
        if (result[0].nik == null) {
            $('#membermodal').html("");
        } else {
            $.each(result, function (index, val) {
                memberbody += `<div class="d-flex">
                                <p style="font-size: 14px">${val.name}</p>
                                <p id="${val.nik}" class="btn btn-danger ml-3" onclick="deleteMember(${val.taskId}, this.id)">X</p>
                            </div>`
            })
            $('#membermodal').html(memberbody);
        }
    }).fail(error => { })
}



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
        console.log(project)
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



// ProjectView

function showmodalProject(id) {
    $('#mediumModal').modal('show')
    showLogTask(id);
    detailTaskAccount(id);
}

function detailTaskAccount(id) {
    var memberbody = `<p style="font-size: 14px" class="font-weight-bold mb-0">Member</p>`
    $.ajax({
        url: "https://localhost:44374/AsignProject/GetTaskAccount/",
        dataType: "JSON",
        dataSrc: "",
        data: { "id": id }
    }).done(result => {
        $('[id="TaskId"]').val(id)
        $('#descriptionTask').text(result[0].description);
        status = enumStatus(result[0].status)
        $('#statusTask').text(status);
        datemodal = new Date(result[0].startDate).toLocaleDateString() + " - " + new Date(result[0].date).toLocaleDateString()
        $('#datemodal').text(datemodal);
        if (result[0].nik == null) {
            $('#membermodal').html("");
        } else {
            $.each(result, function (index, val) {
                memberbody += `<div class="d-flex">
                                <p style="font-size: 14px">${val.name}</p>
                                <p id="${val.nik}" class="btn btn-danger ml-3" onclick="deleteMember(${val.taskId}, this.id)">X</p>
                            </div>`
            })
            $('#membermodal').html(memberbody);
        }
        getMember(result)
    }).fail(error => { })
}

// Add Modul
function AddModul() {
    obj = $('#insertModul').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertModul",
        method: "ost",
        data: obj
    }).done(result => {
        GetModul()
        
    }).fail(error => { })
}

function GetModul() {
    projectId = $('#ProjectId').text();
    var body = '<option selected disabled>Open this select menu</option>';
    $.ajax({
        url: "https://localhost:44374/AsignProject/GetModul",
        method: 'post',
        data: {ProjectId: projectId}
    }).done(result => {
        $.each(result, function (index, val) {
            body += `<option value="${val.modulId}">${val.modulName}</option>` 
        })
        $('[id=selectModul ]').html(body)
    }).fail(error => { })
}

function AddTask() {
    var obj = $('#insertTask').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertTask",
        method: 'post',
        data: obj
    }).done(result => {
        location.reload();
    }).fail(error => { })
}

function AddTaskDesign() {
    var obj = $('#insertTaskDesign').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertTask",
        method: 'post',
        data: obj
    }).done(result => {
        location.reload();
    }).fail(error => { })
}

function AddTaskDoing() {
    var obj = $('#insertTaskDoing').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertTask",
        method: 'post',
        data: obj
    }).done(result => {
        location.reload();
    }).fail(error => { })
}

function AddTaskCodeReview() {
    var obj = $('#insertTaskCodeReview').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertTask",
        method: 'post',
        data: obj
    }).done(result => {
        location.reload();
    }).fail(error => { })
}

function AddTaskTesting() {
    var obj = $('#insertTaskTesting').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertTask",
        method: 'post',
        data: obj
    }).done(result => {
        location.reload();
    }).fail(error => { })
}

function AddTaskDone() {
    var obj = $('#insertTaskDone').serialize();
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertTask",
        method: 'post',
        data: obj
    }).done(result => {
        location.reload();
    }).fail(error => { })
}
// DateTimePicker for start - end date
jQuery(function () {
    jQuery('#date_timepicker_start').datetimepicker({
        format: 'Y/m/d',
        onShow: function (ct) {
            this.setOptions({
                maxDate: jQuery('#date_timepicker_end').val() ? jQuery('#date_timepicker_end').val() : false
            })
        },
        timepicker: false
    });
    jQuery('#date_timepicker_end').datetimepicker({
        format: 'Y/m/d',
        onShow: function (ct) {
            this.setOptions({
                minDate: jQuery('#date_timepicker_start').val() ? jQuery('#date_timepicker_start').val() : false
            })
        },
        timepicker: false
    });
});

function insertDescription() {
    var obj = $('#insertDescription').serialize();
    var id = $('#TaskId').val()
    $.ajax({
        url: "https://localhost:44374/AsignProject/UpdateDescription",
        method: "post",
        data: obj
    }).done(result => {
        detailTaskAccount(id)
        $("#insertDescription")[0].reset();
    }).fail(error => { })
}

function insertDate() {
    var obj = $('#insertDate').serialize();
    var id = $('#TaskId').val()
    $.ajax({
        url: "https://localhost:44374/AsignProject/UpdateDate",
        method: "post",
        data: obj
    }).done(result => {
        detailTaskAccount(id)
        location.reload();
    }).fail(error => { })
}

function getMember(arrays) {
    var selected = ""
    $.ajax({
        url: "https://localhost:44374/AsignProject/GetAccounts"
    }).done(result => {
        var r = result.filter(({ nik }) => !arrays.some(x => x.nik == nik))
       
        $.each(r, function (index, val) {
            var role = ""
            var isTask = false
            for (var i in val.allRoleName) {
                if (val.allRoleName[i] != "Admin" && val.allRoleName[i] != "Manager" && val.allRoleName[i] != "Employee") {
                    isTask = true
                    role += `<span> ${val.allRoleName[i]}</span> `
                }
            }
            if (isTask) {
                selected += `<li style="font-size: 14px" class="btn list-group-item" id=${val.nik} onclick="insertMember(this.id)">${val.name} [${role}]</li>`
            }
        })
        $('#selectMember').html(selected)
    }).fail(error => { })
}

function insertMember(nik) {
    var id = $('#TaskId').val()
    var obj = new Object()
    obj.NIK = nik
    obj.TaskModulId = id
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertMember",
        method: 'post',
        data: obj
    }).done(result => {
        detailTaskAccount(id)
    }).fail(error => { })
}

function deleteMember(taskId, nik) {
    $.ajax({
        url: "https://localhost:44374/AsignProject/DeleteMember",
        method: "post",
        data: { NIK: nik, TaskModulId: taskId}
    }).done(result => {
       detailTaskAccount(taskId)
    }).fail(error => { })
}

