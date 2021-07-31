getNik();

function getNik() {
    var nik = "";
    $.ajax({
        url: "/login/getEmployeeView"
    }).done(result => {
        $.each(result, function (index, val) {
            nik = val['nik'];
        })
        /*manipulat = nik.slice(-2);
        result = parseInt(manipulat);
        result++;
        result = result.toString();*/
        var numberPattern = /\d+/g;
        res = nik.match(numberPattern)
        number = parseInt(res);
        number++;
        nik = "E" + number
        console.log(nik, number)
        document.getElementById("inputNik").value = nik;
    }).fail(error => {
        //alert("Data tidak berhasil di dapat");
    })
}
