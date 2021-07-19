/* Index */

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
    var projectList = "";

    // Get Data To showing project Task
    $.ajax({
        url: "https://localhost:44374/Task/GetJWT"
    }).done(res => {
        $.ajax({
            url: "https://localhost:44374/Task/GetProjectView",
            data: {NIK : res.nik}
        }).done(result => {
            $.each(result, function (index, val) {
                start = new Date(val["startDate"]).toLocaleDateString();
                end = new Date(val["endDate"]).toLocaleDateString();
                projectList += `<div class="kartu kartu-project" onclick="goProject(${val['modulId']})">
                                        <h5>${val["name"]}</h5>
                                        <p>${val['modulName']}</p>
                                        <p>Start : ${start}</p>
                                        <p>End : ${end}</p>
                                  </div>`;
            })
            $('#listProject').html(projectList);
        
        }).fail(error => {
        
        })
    }).fail(err => {})
})

// send Page to task view
function goProject(id) {
    $.ajax({
        url: "https://localhost:44374/Task/GetJWT"
    }).done(result => {
        window.open("https://localhost:44374/Task/TaskView/?"+ "NIK="+ result.nik +"&ModulId=" + id)
    }).fail(error => {})

}





