$(function () {
    $(".sortable_list").sortable({
        connectWith: ".connectedSortable",
        /*stop: function(event, ui) {
            var item_sortable_list_id = $(this).attr('id');
            console.log(ui);
            //alert($(ui.sender).attr('id'))
        },*/
        receive: function (event, ui) {
            //alert("dropped on = " + this.id); Where the item is dropped
            //alert("sender = " + ui.sender[0].id);  Where it came from
            //alert("item = " + ui.item[0].getAttribute("id")); Which item (or ui.item[0].id)
            var id = ui.item[0].getAttribute("id");
            var status = this.id
            UpdateStatus(id, status)
        }
    }).disableSelection();


});

function UpdateStatus(id, status) {
    $.ajax({
        url: "https://localhost:44374/Task/UpdateStatus",
        dataType: "json",
        dataSrc: "",
        data: {
            TaskId: id,
            Status: status
        }
    }).done(result => {
        alert("sukses");
    }).fail(error => {
        alert("Gagal");
    })
}