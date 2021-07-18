$(document).ready(function () {

    //get decode from JWT
    $.ajax({
        url: 'https://localhost:44374/Account/GetJWTNIK',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        GetData(result.nik);
        
    }).fail(error => {

    })


});

function GetData(id) {
    $.ajax({
        url: 'https://localhost:44374/Account/GetEmployee',
        dataType: "json",
        dataSrc: "",
        data: { nik: id }
    }).done(result => {
        $('#inputNik').val(result.nik);
        $('#showNik').val(result.nik);
        $('#inputName').val(result.name);
        $('#inputEmail').val(result.email);
        $('#inputPhoneNumber').val(result.phoneNumber);
        $('#inputBirthDate').val(result.birthDate);
        $('#inputAddress').val(result.address);
        result.gender == 0 ? $("#pria").prop("checked", true) : $("#wanita").prop("checked", true) ;
        
    }).fail(error => {

    })
}
