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
            projectList += `<div class="col-3 kartu kartu-project" onclick="goProject(${val['projectId']})">
                                    <h5 class="text-center text-truncate">${val["name"]}</h5>
                                    <br />
                                    <p class="text-center text-truncate">Start : ${start}</p>
                                    <p class="text-center text-truncate">End : ${end}</p>
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



