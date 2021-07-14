//=============================================================================================
//=================================Add Validasi bootstrap======================================
//=============================================================================================

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('needs-validation');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
            }
            form.classList.add('was-validated');
        });
    }
});

