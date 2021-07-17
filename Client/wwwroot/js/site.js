$(document).ready(function () {

    //get decode from JWT
    $.ajax({
        url: 'https://localhost:44374/dashboard/getJwt',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        $('#name1').text(result['name'])
        $('#name2').text(result['name'])
        $('#email').text(result['email'])

    }).fail(error => {

    })



    $('#assigntask').DataTable(
        //{
        //    ajax: {
        //        url: '/employee/GetRegistrasiView/',
        //        dataSrc: ''
        //    },
        //    columns: [
        //        { data: '' },
        //        { data: 'nik' },
        //        { data: 'nik' },
        //        { data: 'nik' },
        //        { data: 'nik' },
        //        {
        //            data: 'nik',
        //            targets: 'no-sort', orderable: false,
        //            render: function (data, type, row) {
        //                return `<button value="${data}" class="btn btn-primary" >Edit</button> &nbsp
        //                <button value="${data}" onclick="delEmployee(this.value)" class="btn btn-danger">Delete</button> `;
        //            }
        //        }
        //    ]
        //}
    );
});
