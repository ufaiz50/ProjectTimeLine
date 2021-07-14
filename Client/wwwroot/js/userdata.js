// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
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
                "render": function () {
                    return `<button type='button' class='btn btn-primary' data-toggle='modal' data-target='#exampleModal'><i class="fas fa-edit"></i></button>
                            <button type='button' class='btn btn-danger' data-toggle='modal' data-target='#exampleModal'><i class="fas fa-trash"></i></button>`;
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
})