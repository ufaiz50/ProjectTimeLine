// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { post } = require("jquery");

/*<form method="delete" action="/Dashboard/DeleteEmployee">
                                <input type="hidden" value="${full["nik"]}" name="NIK"/>
                                <button class='btn btn-danger' type="submit"><i class="fas fa-trash"></i></button>
                            </form>*/


// Write your JavaScript code.
$(document).ready(function () {

    // Data table to show user/employe
    $('#userdata').DataTable({
        "ajax": {
            url: "https://localhost:44374/Dashboard/Userdataview",
            dataType: "json",
            dataSrc: ""
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            { "data": "name"},
            { "data": "email"},
            { "data": "phoneNumber" },
            {
                "data": null,
                "render": function (data, type, full) {
                    if (full.allRoleName == null) return "No role";
                    bod = "";
                    for (var i in full.allRoleName) {
                        full.allRoleName[i] == "Admin" ? bod += `<span class="role admin">${full.allRoleName[i]}</span>` : bod += `<span class="role user">${full.allRoleName[i]}</span>`;
                    }
                    return bod;
                    
                }
            },
            {
                "data": null,
                "render": function (data, type, full) {
                    return `<button id="${full["nik"]}" type='button' class='btn btn-primary' data-toggle="modal" data-target="#updateModal" onClick="addRole(this.id)"><i class="fas fa-edit"></i></button>
                        <button id="${full["nik"]}" type="button" class="btn btn-danger" data-toggle="modal" data-target="#smallmodal" onClick="deleteRole(this.id)"><i class="fas fa-trash"></i></button>
                            `;
                },
                orderable: false
            }
        ],
    });

    //GetNik
    var nik = "";
    $.ajax({
        url: "https://localhost:44374/Dashboard/GetRegistrasiView"
    }).done(result => {
        $.each(result, function (index, val) {
            nik = val['nik'];
        })
        manipulat = nik.slice(-1);
        result = parseInt(manipulat);
        result++;
        result = result.toString();
        nik = nik.replace(nik.slice(-1), result)
        document.getElementById('inputNik').value = nik;
        
    }).fail(error => {
        alert("Data tidak berhasil di dapat");
    })

    $('#userdata').DataTable().ajax.reload();
})


// Add List Role In Modal
function addRole(id) {
    opsi = `<option>Please select</option>
            <option id="Admin" value="1">Admin</option>
            <option id="Manager" value="2">Manager</option>
            <option id="SA" value="3">SA</option>
            <option id="BA" value="4">BA</option>
            <option id="Developer" value="5">Developer</option>
            <option id="QA" value="6">QA</option>`;
    $('#add').html(opsi)
    $.ajax({
        url: 'https://localhost:44374/Dashboard/Userdataviewnik',
        dataType: "json",
        dataSrc: "",
        data: { nik: id }
    }).done(result => {
        
        document.getElementById('updateNIK').value = result.nik;
        document.getElementById('showNik').value = result.nik;
        for (var i in result.allRoleName) {
            if (result.allRoleName[i] == "Admin") $("#Admin").remove();
            if (result.allRoleName[i] == "Manager") $("#Manager").remove();
            if (result.allRoleName[i] == "SA") $("#SA").remove();
            if (result.allRoleName[i] == "BA") $("#BA").remove();
            if (result.allRoleName[i] == "Developer") $("#Developer").remove();
            if (result.allRoleName[i] == "QA") $("#QA").remove();
        }
        /*for (var i in result.allRoleName) {
            if (result.allRoleName[i] == "Admin")  $("#Admin").prop("checked", true);
            if (result.allRoleName[i] == "Manager") $("#Manager").prop("checked", true);
            if (result.allRoleName[i] == "BA") $("#BA").prop("checked", true);
        }*/

    }).fail(error => {
        
    })
}

// Add Role
function AddRoles() {

    addrole = $('#addRole').serialize()
    $.ajax({
        url: "https://localhost:44374/Dashboard/UpdateUserData",
        method: 'post',
        dataType: "JSON",
        data: addrole
    }).done(result => {
        $('#userdata').DataTable().ajax.reload()
        Swal.fire(
            'Good job!',
            'Role Has Been Added',
            'success'
        )
    }).fail(error => {
        Swal.fire(
            'Error!',
            'Role Failed to add',
            'error'
        )
    })
}

// Delete List Role in Modal
function deleteRole(id) {
    opsi = `<option>Please select</option>`;
    
    $.ajax({
        url: 'https://localhost:44374/Dashboard/Userdataviewnik',
        dataType: "json",
        dataSrc: "",
        data: { nik: id }
    }).done(result => {

        document.getElementById('deleteNIK').value = result.nik;
        document.getElementById('showNik2').value = result.nik;
        for (var i in result.allRoleName) {
            if (result.allRoleName[i] == "Admin") opsi += '<option id="Admin" value="1">Admin</option>';
            if (result.allRoleName[i] == "Manager") opsi += '<option id="Manager" value="2">Manager</option>';
            if (result.allRoleName[i] == "SA") opsi += '<option id="SA" value="3">SA</option>';
            if (result.allRoleName[i] == "BA") opsi += '<option id="BA" value="4">BA</option>';
            if (result.allRoleName[i] == "Developer") opsi += '<option id="Developer" value="5">Developer</option>';
            if (result.allRoleName[i] == "QA") opsi += '<option id="QA" value="6">QA</option>';
        }
        $('#delete').html(opsi)
       
    }).fail(error => {
    })

}

function DeleteRoles() {
    deleterole = $('#deleteRole').serialize()
    NIK = $('#deleteNIK').val()
    RoleID = $('#delete').val()
    $.ajax({
        url: "https://localhost:44374/Dashboard/DeleteUserData",
        method: 'POST',
        dataType: "JSON",
        data: deleterole
    }).done(result => {
        $('#userdata').DataTable().ajax.reload()
        Swal.fire(
            'Good job!',
            'Role Has Been Deleted',
            'success'
        );
    }).fail(error => {
        Swal.fire(
            'Error!',
            'Role Failed to Deleted',
            'error'
        )
    })
}


