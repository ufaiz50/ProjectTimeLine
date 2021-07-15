// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    // Data table to show user/employe
    $('#userdata').DataTable({
        dom: 'Bfrtip',
        "ajax": {
            url: "https://localhost:44374/Dashboard/GetRegistrasiView",
            dataType: "json",
            dataSrc: ""
        },
        "columns": [
            { "data": "nik" },
            {
                "data": "name",
                "render": function (data, type, full) {
                    return `<div class="table-data__info">
                                <h6>${data}</h6>
                                <span>
                                    <a href="#">${full["email"]}</a>
                                </span>
                            </div>`
                }
            },
            { "data": "phoneNumber" },
            {
                "data": "birthDate",
                "render": function (data, type, full) {
                    tanggal = new Date(data).getDate();
                    bulan = new Date(data).toLocaleString('default', { month: 'short' })
                    tahun = new Date(data).getFullYear();

                    return `${tanggal}/${bulan}/${tahun}`;
                }
            },
            { "data": "address" },
            {
                "data": "gender",
                "render": function (data, type, full) {
                    return data == 0 ? "Pria" : "Wanita";
                }
            },
            {
                "data": "roleName",
                "render": function (data, type, full) {
                    if (data == "Admin") {
                        return `<span class="role admin">${data}</span>`;
                    }
                    return `<span class="role user">${data}</span>`;
                }
            },
            {
                "data": null,
                "render": function (data, type, full) {
                    return `<button id="${full["nik"]}" type='button' class='btn btn-primary' data-toggle="modal" data-target="#updateModal" onClick="getUserData(this.id)"><i class="fas fa-edit"></i></button>
                            <form method="delete" action="/Dashboard/DeleteEmployee">
                                <input type="hidden" value="${full["nik"]}" name="NIK"/>
                                <button class='btn btn-danger' type="submit"><i class="fas fa-trash"></i></button>
                            </form>`;
                },
                orderable: false
            }
        ],
        buttons: {
            buttons: [
                { extend: 'copy', className: 'btn btn-primary' },
                { extend: 'excel', className: 'btn btn-primary' },
                { extend: 'csv', className: 'btn btn-primary' },
                {
                    extend: 'pdf',
                    className: 'btn btn-primary',
                    orientation: 'landscape'
                },
                { extend: 'print', className: 'btn btn-primary' }
            ],
        }
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
        console.log(nik);
    }).fail(error => {
        alert("Data tidak berhasil di dapat");
    })

})



function getUserData(id) {
    $.ajax({
        url: 'https://localhost:44374/Dashboard/GetUserDataView',
        dataType: "json",
        dataSrc: "",
        data: { NIK: id }
    }).done(result => {
        document.getElementById('updateNIK').value = result.nik;
        document.getElementById('updateName').value = result.name;
        document.getElementById('updateEmail').value = result.email;
        document.getElementById('updatePhoneNumber').value = result.phoneNumber;
        document.getElementById('updateBirthDate').value = result.birthDate;
        result.gender == 0 ? document.getElementById("pria").checked = true : document.getElementById("wanita").checked = true ;
        document.getElementById('updateAddress').value = result.address;
        console.log(result);
    }).fail(error => {
        console.log(error);
    })
}
