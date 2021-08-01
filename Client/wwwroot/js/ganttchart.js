//$('#select').on('change', function () {
//    apexChart(this.value)
//});

function apexChart(id) {
    projectId = id.value;
    console.log(projectId);
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
                id: 'mychart',
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

        var chart = new ApexCharts(document.querySelector("#chart-container"), options);
        //chart.destroy();
        chart.render();



    }).fail(error => { })
}

function ganttChart(idProject) {
    id = idProject.value
    $.ajax({
        url: "https://localhost:44374/GanttChart/GanttChartView",
        dataType: "Json",
        data: {"ProjectId": id},
    }).done(result => {

        var chartLabel = []
        var chartTask = []
        var chartStart = []
        var chartEnd = []
        var chartStartEnd = []
        var chartCategoryM = []

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

        // sorting Data and passing to Label And Task
        chartLabel.push({ "label": project[0].projectName })
        chartTask.push({ "start": formatDate(project[0].startDate), "end": formatDate(project[0].endDate) })
        for (var i in modul) {
            chartLabel.push({ "label": modul[i].modulName })
            chartTask.push({
                "start": formatDate(modul[i].modulStartDate),
                "end": formatDate(modul[i].modulEndDate)
            })
            nama = modul[i].modulName
            nama2 = task[nama]
            for (var i in nama2) {
                chartLabel.push({ "label": nama2[i].taskName })
                chartTask.push({
                    "start": formatDate(nama2[i].taskStartDate),
                    "end": formatDate(nama2[i].taskEndDate)
                })
            }
        }

        // Get Start and End Date for table
        for (var i in chartTask) {
            chartStart.push({"label": chartTask[i].start})
            chartEnd.push({"label": chartTask[i].end})
        }

        // Get Header Duration
        chartStartEnd.push({"start": chartTask[0].start, "end": chartTask[0].end, "label": "Project Duration"})



        const dataSource = {
            chart: {
                dateformat: "dd/mm/yyyy",
                caption: chartLabel[0].label,
                theme: "fusion",
                canvasborderalpha: "40",
                ganttlinealpha: "50"
            },
            tasks: {
                color: "#5D62B5",
                task: chartTask
               
            },
            processes: {
                "headertext": "Task",
                "fontcolor": "#000000",
                "fontsize": "11",
                "isanimated": "1",
                "bgcolor": "#96BAFF",
                "headervalign": "bottom",
                "headeralign": "left",
                "headerbgcolor": "#999999",
                "headerfontcolor": "#ffffff",
                "headerfontsize": "12",
                "align": "left",
                "isbold": "1",
                "bgalpha": "25",
                process: chartLabel
            },
            categories: [
                {
                    category: chartStartEnd
                },
                /*{
                    category: [
                        {
                            start: "01/06/2021",
                            end: "06/06/2021",
                            label: "W 1"
                        },
                        {
                            start: "07/06/2021",
                            end: "13/06/2021",
                            label: "W 2"
                        },
                        {
                            start: "14/06/2021",
                            end: "20/06/2021",
                            label: "W 3"
                        },
                        {
                            start: "21/06/2021",
                            end: "27/06/2021",
                            label: "W 4"
                        },
                        {
                            start: "28/06/2021",
                            end: "04/07/2021",
                            label: "W 5"
                        },
                        {
                            start: "05/07/2021",
                            end: "11/07/2021",
                            label: "W 6"
                        },
                        {
                            start: "12/07/2021",
                            end: "18/07/2021",
                            label: "W 7"
                        },
                        {
                            start: "19/07/2021",
                            end: "25/07/2021",
                            label: "W 8"
                        },
                        {
                            start: "26/07/2021",
                            end: "31/07/2021",
                            label: "W 9"
                        }
                    ]
                }*/
            ],
            datatable: {
                "showprocessname": "1",
                "namealign": "left",
                "fontcolor": "#000000",
                "fontsize": "10",
                "headerbgcolor": "#999999",
                "headerfontcolor": "#ffffff",
                "headerfontsize": "12",
                "datacolumn": [{
                    "bgcolor": "#eeeeee",
                    "headertext": "Start Date",
                    "text": chartStart
                }, {
                    "bgcolor": "#eeeeee",
                    "headertext": "End Date",
                    "text": chartEnd
                }]
            },
        };

        FusionCharts.ready(function () {
            var myChart = new FusionCharts({
                type: "gantt",
                renderAt: "chart-container",
                width: "100%",
                height: "100%",
                dataFormat: "json",
                dataSource
            }).render();
        });

    }).fail(error =>{})
}


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



