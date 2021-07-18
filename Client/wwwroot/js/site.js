addmodul();

$.extend($.fn.dataTable.defaults, {
    responsive: true
});

var table = $('#ajaxSW').DataTable({
    ajax: {
        url: '/modul/GetTaskAccView',
        dataSrc: ''
    },
    columns: [
        { data: 'taskName' },
        {
            data: 'date',
            render: function (data, type, row) {
                return data.split('T')[0];
            }
        },
        { data: 'description' },
        { data: 'modulName' },

        { data: 'member' },
        {
            data: 'priorityTask',
            render: function (data, type, row) {
                if (data === 0) {
                    return "Priority";
                }
                else if (data === 1) {
                    return "Medium";
                }
                else {
                    return "Normal";
                }
            }
        },
        {
            data: 'status',
            render: function (data, type, row) {
                if (data == 0) {
                    return "To Do";
                }
                else {
                    return "Design";
                }
            }
        },
        {
            data: 'taskId',
            targets: 'no-sort', orderable: false,
            render: function (data, type, row) {
                return `<button value="${data}" class="btn btn-primary" >Edit</button> &nbsp
                        <button value="${data}" onclick="delTask(this.value)" class="btn btn-danger">Delete</button> `;
            }
        }
    ]
});

var tableModul = $('#modulTable').DataTable({
    ajax: {
        url: '/modul/Getmodultable',
        dataSrc: ''
    },
    columns: [
        { data: 'modulName' },
        {
            data: 'date',
            render: function (data, type, row) {
                return data.split('T')[0];
            }
        },
        { data: 'name' },
        {
            data: 'modulId',
            targets: 'no-sort', orderable: false,
            render: function (data, type, row) {
                return `<button value="${data}" onclick="getModul(this.value)" class="btn btn-primary" type="button" data-toggle="modal" data-target="#updateModal" >Edit</button> &nbsp
                        <button value="${data}" onclick="delModul(this.value)" class="btn btn-danger">Delete</button> `;
            }
        }
    ]
});

//===================add form update modul========================================

function getModul(id) {
    $.ajax({
        url: '/modul/getmodulidview/' + id,
        dataType: "json",
        dataSrc: "",
    }).done(result => {
        date = new Date(result.date).toLocaleDateString();
        console.log(date);
        document.getElementById('ProId').value = result.projectId;
        document.getElementById('ModName').value = result.modulName;
        document.getElementById('DueDate').value = date;
        console.log(result);
    }).fail(error => {
        console.log(error);
    })
}


//===================add option modul========================================

function addmodul() {
    $.ajax({
        url: "/modul/getmodulview"
    }).done((result) => {
        text = "<option selected disabled value=\"\">Choose...</option>";
        $.each(result, function (key, val) {
            text += `<option value="${val.modulId}">${val.modulName}</option>`;
        });
        $("#modul").html(text);
        $("#modultask").html(text);
    }).fail((error) => {
        console.log(error);
    });
}

//=========================add option member==================================

$(document).ready(function () {
    $.ajax({
        url: "/dashboard/getregistrasiview"
    }).done((result) => {
        let text = "";
        $.each(result, function (key, val) {
            text += `<option value="${val.nik}">${val.name}(${val.roleName})</option>`;
        });
        $("#member").html(text);
        $('#member').multiselect({
            nonSelectedText: 'Select Member',
            enableFiltering: true,
            enableCaseInsensitiveFiltering: true,
            buttonWidth: '100%'
        });
    }).fail((error) => {
        console.log(error);
    });
});
//=================Add form Validation Modul========================

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('modul');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                Insert();
            }
            form.classList.add('was-validated');
        });
    }
});

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('task');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                InsertTask();
            }
            form.classList.add('was-validated');
        });
    }
});


//=========================insert Modul==================================

function Insert() {
    var obj = new Object();
    obj.ModulName = $("#ModulName").val();
    obj.Date = $("#Date").val();
    obj.ProjectId = parseInt($("#ProjectId").val());

    $.ajax({
        url: "/modul/insertmodul",
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result)
        tableModul.ajax.reload();
        $('#exampleModalCenter').modal('hide');
    }).fail((error) => {
        console.log(error)
    })
}

//===========================insert Task==================================

function InsertTask() {

    var obj = new Object();
    obj.TaskName = $("#taskName").val();
    obj.Date = $("#datetask").val();
    obj.ModulId = parseInt($("#modultask").val());
    obj.Description = $("#description").val();
    obj.Status = 0;
    obj.PriorityTask = parseInt($("#priority").val());

    $.ajax({
        url: "/modul/InsertTaskModul",
        type: "POST",
        data: obj

    }).done((result) => {

        var values = $('#member').val();

        $.each(values, function (key, val) {
            var acctask = new Object();
            let nik = val;
            let taskID = "";

            $.ajax({
                url: "/modul/GetTaskModelView"
            }).done((result) => {
                $.each(result, function (key, val) {
                    taskID = val.taskId;
                });

                acctask.NIK = nik;
                acctask.TaskModulId = taskID;

                $.ajax({
                    url: "/modul/InsertAccountTask",
                    type: "POST",
                    data: acctask
                }).done((result) => {
                    console.log(result);
                    table.ajax.reload();
                    $('#taskmodul').modal('hide');
                }).fail((error) => {
                    console.log(error)
                })

            }).fail((error) => {
                console.log(error)
            })
        })

    }).fail((error) => {
        console.log(error)
    })
}

function delTask(del) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/modul/DeleteTaskModul/" + del,
                type: 'delete'
            }).done((result) => {
                Swal.fire({
                    icon: 'success',
                    title: 'Deleted.',
                    text: result.message,
                });
                table.ajax.reload();
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed',
                    text: error.responseJSON.message,
                });
            })
        }
    })
}

function delModul(del) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/modul/DeleteModul/" + del,
                type: 'delete'
            }).done((result) => {
                Swal.fire({
                    icon: 'success',
                    title: 'Deleted.',
                    text: result.message,
                });
                tableModul.ajax.reload();
                table.ajax.reload();
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed',
                    text: error.responseJSON.message,
                });
            })
        }
    })
}