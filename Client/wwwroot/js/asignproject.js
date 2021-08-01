// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    ready()
})

function ready() {
    var projectList = "";
    $.ajax({
        url: "https://localhost:44374/AsignProject/GetProjectView"
    }).done(result => {
        result.sort(function (a, b) {
            return new Date(b.projectId) - new Date(a.projectId);
        });
        $.each(result, function (index, val) {
            start = new Date(val["startDate"]).toLocaleDateString();
            end = new Date(val["endDate"]).toLocaleDateString();
            projectList += `<div class="col-3 kartu kartu-project">
                                    <div style="top: 0; right:0" class="position-absolute btn btn-dark" data-toggle="collapse" href="#collapseExample${val['projectId']}" role="button" aria-expanded="false" aria-controls="collapseExample">
                                        <i class="text-white fa fa-bars fa-sm" aria-hidden="true"></i>
                                    </div>
                                    <div onclick="goProject(${val['projectId']})">
                                    <h5 class="text-center text-truncate">${val["name"]}</h5>
                                    <br />
                                    <p class="text-center text-truncate">Start : ${start}</p>
                                    <p class="text-center text-truncate">End : ${end}</p>
                                    </div>
                                    <div class="position-absolute" style="top: 38px; right: 0;">
                                        <div style="width: 100px; height: 100px" class="list-group collapse" id="collapseExample${val['projectId']}">
                                          <div onclick="goProjectBoard(${val['projectId']})" style="font-size: 12px;" class="btn btn-light list-group-item"><i class="fa fa-window-maximize text-primary" aria-hidden="true"></i> Board</div>
                                          <div onclick="goProject(${val['projectId']})" style="font-size: 12px;" class="btn btn-light list-group-item"><i class="fa fa-table text-primary" aria-hidden="true"></i> Table</div>
                                        </div>
                                    </div>
                              </div>`;
        })
        projectList += `
                        <div class="col-3 kartu">
                            <button type="button" class="btn btn-secondary mb-1" data-toggle="modal" data-target="#mediumModal">
                                <i class="fas fa-plus"></i>
                            </button>
                        </div>
                        `;
        $('#listProject').html(projectList);

    }).fail(error => {

    })
}

function insertProject() {
    var obj = $('#formInsertProject').serialize()
    $.ajax({
        url: "https://localhost:44374/AsignProject/InsertProject",
        method: 'post',
        data: obj
    }).done(result => {
        ready()
        Swal.fire(
            'Good job!',
            'Project Has Been Added',
            'success'
        )
        
    }).fail(error => { })
}

function goProject(id) {
    window.location.href = 'https://localhost:44374/Dashboard/AssignTask/'+id;
}

function goProjectBoard(id) {
    window.open('https://localhost:44374/AsignProject/project/?id=' + id);
}

