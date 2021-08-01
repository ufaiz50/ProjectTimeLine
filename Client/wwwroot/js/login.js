//=========================Login=================================

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('login');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                PostLogin();
            }
            form.classList.add('was-validated');
        });
    }
});

function PostLogin() {
    var obj = new Object();
    obj.Email = $("#Email").val();
    obj.Password = $("#Password").val();
    console.log(obj);
    $.ajax({
        url: "/login/auth",
        type: "POST",
        data: obj
    }).done((result) => {
        console.log(result);
        window.location.href = "https://localhost:44374/dashboard";
    }).fail((error) => {
        console.log(error);
        var x = document.getElementById("alertLogin");
        x.style.display = "block";
    })
}

//=========================Reset Password==================================

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('reset');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                ResetPassword();
            }
            form.classList.add('was-validated');
        });
    }
});

function ResetPassword() {
    obj = $('#EmailReset').serialize();
    $("#btnreset").attr("disabled", true);
    console.log(obj);
    $.ajax({
        url: "/account/resetpassword",
        type: "POST",
        dataType: "JSON",
        data: obj
    }).done((result) => {
        console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'New Password has been Send to Your Email',
        });
        $('#resetpassword').modal('hide');
        $('#btnreset').removeAttr("disabled");
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Reset Failed',
            text: 'make sure to use valid email!',
        });
        $('#btnreset').removeAttr("disabled");
    })
}
