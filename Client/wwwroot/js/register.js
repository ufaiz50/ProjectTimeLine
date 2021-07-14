﻿//=============================================================================================
//=================================insert new registration=====================================
//=============================================================================================
getNik();

function Insert() {
    var obj = new Object();
    obj.NIK = $("#nik").val();
    obj.Password = $("#password").val();
    obj.Name = $("#Name").val();
    obj.Gender = parseInt($("input[name=gender]:checked").val());
    obj.BirthDate = $("#birthDate").val();
    obj.PhoneNumber = $("#phoneNumber").val();
    obj.Email = $("#email").val();
    obj.Address = $("#address").val();

    console.log(obj);
    $.ajax({
        url: "https://localhost:44356/api/employees/register",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj)
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: result.message,
        });
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Registrasi Gagal',
            text: error.responseJSON.message,
        });
    })
}

function getNik() {
    var nik = "";
    $.ajax({
        url: "https://localhost:44356/api/employees"
    }).done(result => {
        $.each(result.result, function (index, val) {
            nik = val['nik'];
        })
        manipulat = nik.slice(-2);
        result = parseInt(manipulat);
        result++;
        result = result.toString();
        nik = nik.replace(nik.slice(-2), result)
        document.getElementById('inputNik').value = nik;
    }).fail(error => {
        alert("Data tidak berhasil di dapat");
    })
}
