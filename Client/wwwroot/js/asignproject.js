// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    var projectList = "";
    $.ajax({
        url: "https://localhost:44374/AsignProject/GetProjectView"
    }).done(result => {
        $.each(result, function (index, val) {
            start = new Date(val["startDate"]).toLocaleDateString();
            end = new Date(val["endDate"]).toLocaleDateString();
            projectList += `<div class="kartu" onclick="goProject(${val['projectId']})">
                                    <h5>${val["name"]}</h5>
                                    <p>Start : ${start}</p>
                                    <p>End : ${end}</p>
                              </div>`;
        })
        projectList += `<div>
                            <div class="kartu">
                                <button type="button" class="btn btn-secondary mb-1" data-toggle="modal" data-target="#mediumModal">
                                    <i class="fas fa-plus"></i>
                                </button>
                            </div>
                        </div>`;
        $('#listProject').html(projectList);
        console.log(projectList);
    }).fail(error => {
        alert("Data tidak berhasil di dapat");
    })


})

function goProject(id) {
    window.open("https://localhost:44374/AsignProject/ViewProject/"+id)
}



