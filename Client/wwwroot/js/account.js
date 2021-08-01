Triggerdata()

// JWT
function Triggerdata() {
    $.ajax({
        url: 'https://localhost:44374/Account/GetJWTNIK',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        GetData(result.nik);

    }).fail(error => {

    })
}

function GetData(id) {
    $.ajax({
        url: 'https://localhost:44374/Account/GetEmployee',
        dataType: "json",
        dataSrc: "",
        data: { nik: id }
    }).done(result => {
        tgl = new Date(result.birthDate).toLocaleDateString('en-CA');
        $('[id="name"]').text(result.name);
        $('[id="email"]').text(result.email);
        $('[id="phonenumber"]').text(result.phoneNumber);
        $('[id="address"]').text(result.address);
        $('[id="birthdate"]').text(tgl);

        tgl = new Date(result.birthDate).toLocaleDateString('en-CA');
        $('[id="inputNik"]').val(result.nik);
        $('#showNik').val(result.nik);
        $('#inputName').val(result.name);
        $('#inputEmail').val(result.email);
        $('#inputPhoneNumber').val(result.phoneNumber);
        $('#inputBirthDate').val(tgl);
        $('#inputAddress').val(result.address);
        result.gender == 0 ? $("#pria").prop("checked", true) : $("#wanita").prop("checked", true);
        
    }).fail(error => {

    })
}

function UpdateEmployee() {
    obj = $('#UpdateEmployee').serialize();
    $.ajax({
        url: "https://localhost:44374/Account/UpdateEmployee",
        method: "POST",
        dataType: "JSON",
        data: obj
    }).done(result => {
        Swal.fire(
            'Good job!',
            'Employee Has Been Update',
            'success'
        )
        Triggerdata();
    }).fail(error => { })
    Triggerdata();
}

function UpdatePassword() {
    update = $('#ChangePassword').serialize();
    $.ajax({
        url: "https://localhost:44374/Account/UpdatePassword",
        method: "POST",
        dataType: "JSON",
        data: update
    }).done(result => {
        Swal.fire(
            'Good job!',
            'Password Updated',
            'success'
        )
    }).fail(error => {
        console.log(error)
        Swal.fire(
            'Error!',
            'Wrong Password',
            'error'
        )
    })

    $('#ChangePassword').trigger("reset");
    Triggerdata();
}