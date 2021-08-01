getNik();

function getNik() {
    var nik = "";
    $.ajax({
        url: "/login/getEmployeeView"
    }).done(result => {
        $.each(result, function (index, val) {
            nik = val['nik'];
        })
        manipulat = nik.slice(-2);
        result = parseInt(manipulat);
        result++;
        result = result.toString();
        nik = nik.replace(nik.slice(-2), result)
        document.getElementById("inputNik").value = nik;
    }).fail(error => {
        //alert("Data tidak berhasil di dapat");
    })
}

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('regis');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                PostRegis();
            }
            form.classList.add('was-validated');
        });
    }
});


//=========================Update Modul==================================

function PostRegis() {
    var obj = new Object();
    obj.NIK = $("#inputNik").val();
    obj.Name = $("#nameReg").val();
    obj.Email = $("#emailReg").val();
    obj.Address = $("#Address").val();
    obj.PhoneNumber = $("#PhoneNumber").val();
    obj.BirthDate = $("#BirthDate").val();
    obj.Password = $("#PasswordReg").val();
    obj.Gender = parseInt($("input[name=gender]:checked").val());
    $.ajax({
        url: "/login/EmployeesView",
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result);
        Swal.fire({
            icon: 'success',
            title: result.message,
        });
        $('#regis').modal('hide');
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Register Failed ',
            text: 'Email has Taken, Use another Email'
        });
    })
}