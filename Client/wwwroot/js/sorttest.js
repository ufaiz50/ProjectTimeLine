function showmodal() {
    $('#mediumModal').modal('show')
    console.log("teken")
}


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
            addHistory(ui.sender[0].id, this.id)
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

    }).fail(error => {

    })
}

function addHistory(stateBefore, stateAfter) {
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = date + ' ' + time+".0";
    var obj = new Object();
    obj.EndDate = dateTime;
    obj.StateBefore = stateBefore;
    obj.StateAfter = stateAfter;
    $.ajax({
        url: "https://localhost:44374/Task/AddHistory",
        method: "post",
        data: obj
    }).done(result => {
    }).fail(error => { })
}

