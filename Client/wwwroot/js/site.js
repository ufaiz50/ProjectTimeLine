addmodul();

$(document).ready(function () {

    //get decode from JWT
    $.ajax({
        url: 'https://localhost:44374/dashboard/getJwt',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        $('#name1').text(result['name'])
        $('#name2').text(result['name'])
        $('#email').text(result['email'])

    }).fail(error => {

    })
});

$.extend($.fn.dataTable.defaults, {
    responsive: true
})

//=========================
var url = window.location.href.split('?')[0];
var id = url.substring(url.lastIndexOf('/') + 1);

console.log(url);
console.log(id);

if (id == "") {

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
                    return `<button value="${data}" onclick="getUpdateTask(this.value)" class="btn btn-primary" type="button" data-toggle="modal" data-target="#updatetaskmodul">Edit</button> &nbsp
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

} else {

    var table = $('#ajaxSW').DataTable({
        ajax: {
            url: '/modul/ViewModulTask/' + id,
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
                    return `<button value="${data}" onclick="getUpdateTask(this.value)" class="btn btn-primary" type="button" data-toggle="modal" data-target="#updatetaskmodul">Edit</button> &nbsp
                        <button value="${data}" onclick="delTask(this.value)" class="btn btn-danger">Delete</button> `;
                }
            }
        ]
    });

    var tableModul = $('#modulTable').DataTable({
        ajax: {
            url: '/modul/ViewProjectModul/' + id,
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

}

//===================add form update modul========================================

function getModul(id) {
    $.ajax({
        url: '/modul/getmodulidview/' + id,
        dataType: "json",
        dataSrc: "",
    }).done(result => {
        document.getElementById('ModId').value = result.modulId;
        document.getElementById('ProId').value = result.projectId;
        document.getElementById('ModName').value = result.modulName;
        document.getElementById('DueDate').value = result.date.split('T')[0];
    }).fail(error => {
        console.log(error);
    })
}

//=================Add form Validation Update Modul========================

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('updateModul');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                updateModul();
            }
            form.classList.add('was-validated');
        });
    }
});


//=========================Update Modul==================================

function updateModul() {
    var obj = new Object();
    obj.ModulId = $("#ModId").val();
    obj.ModulName = $("#ModName").val();
    obj.Date = $("#DueDate").val();
    obj.ProjectId = parseInt($("#ProId").val());
    console.log(obj);
    $.ajax({
        url: "/modul/updatemodul",
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result)
        tableModul.ajax.reload();
        $('#updateModal').modal('hide');
    }).fail((error) => {
        console.log(error)
    })
}


//===================add option modul========================================

function addmodul() {
    $.ajax({
        url: "/modul/getmodulview"
    }).done((result) => {
        text = "<option selected disabled value=\"\">Choose...</option>";
        $.each(result, function (key, val) {
            text += `<option id="m${val.modulId}" value="${val.modulId}">${val.modulName}</option>`;
        });
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
            text += `<option id="t${val.nik}" value="${val.nik}">${val.name}(${val.roleName})</option>`;
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
        $('#exampleModalCenter').on('hidden.bs.modal', function () {
            $('.modal-body').find('input').val('');
        });
        addmodul()
    }).fail((error) => {
        console.log(error)
    })
}

//=================Add form Validation Task========================

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
                    $('#taskmodul').on('hidden.bs.modal', function () {
                        $('.modal-body').find('input').val('');
                        $('.modal-body').find('textarea').val('');
                        $('#priority').find('option:selected').removeAttr('selected');
                    });
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

//======================================Delete Task========================================

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

//=====================================================Delete Modul ===================================

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



//=================Add form Value Update Task========================

function getUpdateTask(id) {
    $.ajax({
        url: '/modul/gettaskbyid/' + id,
        dataType: "json",
        dataSrc: "",
    }).done(result => {

        var modID = result[0].modulId

        $.ajax({
            url: "/modul/getmodulview"
        }).done((res) => {

            text = "<option selected disabled value=\"\">Choose...</option>";

            $.each(res, function (key, val) {
                if (modID == val.modulId) {
                    text += `<option id="m${val.modulId}" selected value="${val.modulId}">${val.modulName}</option>`;
                } else {
                    text += `<option id="m${val.modulId}" value="${val.modulId}">${val.modulName}</option>`;
                }
            });
            $("#modtask").html(text);
        }).fail((error) => {
            console.log(error);
        });

        var accNik = result[0].nikMember;

        $.ajax({
            url: "/dashboard/getregistrasiview"
        }).done((result) => {
            let text = "";
            $.each(result, function (key, val) {
                if (new RegExp(accNik.join("|")).test(val.nik)) {
                    text += `<option selected value="${val.nik}">${val.name}(${val.roleName})</option>`;
                } else {
                    text += `<option value="${val.nik}">${val.name}(${val.roleName})</option>`;
                }
            });
            console.log(text);
            $("#memberTask").html(text);
            $('#memberTask').multiselect({
                nonSelectedText: 'Select Member',
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                buttonWidth: '100%'
            });
        }).fail((error) => {
            console.log(error);
        });

        var pTask = `p${result[0].priorityTask}`;
        document.getElementById(pTask).selected = 'selected';

        document.getElementById('tId').value = result[0].taskId;
        document.getElementById('sId').value = result[0].status;
        document.getElementById('tName').value = result[0].taskName;
        document.getElementById('updateDesc').value = result[0].description;
        document.getElementById('updatedatetask').value = result[0].date.split('T')[0];

    }).fail(error => {
        console.log(error);
    })
}
//=================Add form Validation Update Task======================

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('updatetask');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                updateTask();
            }
            form.classList.add('was-validated');
        });
    }
});


//=========================Update Task==================================

function updateTask() {
    var obj = new Object();
    obj.TaskId = $("#tId").val();
    obj.TaskName = $("#tName").val();
    obj.Date = $("#updatedatetask").val();
    obj.ModulId = parseInt($("#modtask").val());
    obj.Description = $("#updateDesc").val();
    obj.Status = $("#tId").val();
    obj.PriorityTask = parseInt($("#uptPrio").val());

    $.ajax({
        url: "/modul/UpdateTaskModul",
        type: "POST",
        data: obj
    }).done((result) => {

        //$.ajax({
        //    url: "/task/DeletetaskMember/" + obj.TaskId
        //}).done((result) => {
        //    console.log(result)
        //}).fail((error) => {
        //    console.log(error)
        //})

        //var values = $('#memberTask').val();
        //$.each(values, function (key, val) {
        //    var acctask = new Object();
        //    let nik = val;
        //    let taskID = "";

        //    $.ajax({
        //        url: "/modul/GetTaskModelView"
        //    }).done((result) => {
        //        $.each(result, function (key, val) {
        //            taskID = val.taskId;
        //        });

        //        acctask.NIK = nik;
        //        acctask.TaskModulId = taskID;

        //        $.ajax({
        //            url: "/modul/InsertAccountTask",
        //            type: "POST",
        //            data: acctask
        //        }).done((result) => {
                    console.log(result);
                    table.ajax.reload();
                    $('#updatetaskmodul').modal('hide');
                }).fail((error) => {
        //            console.log(error)
        //        })

        //    }).fail((error) => {
        //        console.log(error)
        //    })
        //})
    }).fail((error) => {
        console.log(error)
    })
}