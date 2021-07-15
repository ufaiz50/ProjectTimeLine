const draggables = document.querySelectorAll('.draggable')
const containers = document.querySelectorAll('.container')

draggables.forEach(draggable => {
    draggable.addEventListener('dragstart', () => {
        draggable.classList.add('dragging')
    })

    draggable.addEventListener('dragend', () => {
        draggable.classList.remove('dragging')
    })
})

containers.forEach(container => {
    container.addEventListener('dragover', e => {
        e.preventDefault()
        const afterElement = getDragAfterElement(container, e.clientY)
        const draggable = document.querySelector('.dragging')
        if (afterElement == null) {
            container.appendChild(draggable)
        } else {
            container.insertBefore(draggable, afterElement)
        }
    })
})

function getDragAfterElement(container, y) {
    const draggableElements = [...container.querySelectorAll('.draggable:not(.dragging)')]

    return draggableElements.reduce((closest, child) => {
        const box = child.getBoundingClientRect()
        const offset = y - box.top - box.height / 2
        if (offset < 0 && offset > closest.offset) {
            return { offset: offset, element: child }
        } else {
            return closest
        }
    }, { offset: Number.NEGATIVE_INFINITY }).element
}


/* Index */

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
        $('#listProject').html(projectList);
        console.log(projectList);
    }).fail(error => {
        alert("Data tidak berhasil di dapat");
    })


})

function goProject(id) {
    window.open("https://localhost:44374/Task/Baview")
}




