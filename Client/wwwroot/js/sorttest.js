function showmodal(id) {
    $('#mediumModal').modal('show')
    showLogTask(id);
    detailTask(id);
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
            //alert("dropped on = " + this.id); //Where the item is dropped
            //alert("sender = " + ui.sender[0].id);  //Where it came from
            //alert("item = " + ui.item[0].getAttribute("id")); //Which item (or ui.item[0].id)
            var id = ui.item[0].getAttribute("id");
            var status = this.id
            UpdateStatus(id, status)
            addHistory(ui.sender[0].id, this.id, ui.item[0].getAttribute("id"))
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

function addHistory(stateBefore, stateAfter, TaskModulId) {
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = date + ' ' + time + ".0";
    var obj = new Object();
    obj.EndDate = dateTime;
    obj.StateBefore = stateBefore;
    obj.StateAfter = stateAfter;
    obj.NIK = $('#NIK').text();
    obj.TaskModulId = TaskModulId
    $.ajax({
        url: "https://localhost:44374/Task/AddHistory",
        method: "post",
        data: obj
    }).done(result => {
    }).fail(error => { })
}

// Log status in Modal
function showLogTask(id) {
    body = "";
    $.ajax({
        url: "https://localhost:44374/Task/LogStatus/",
        dataType: "JSON",
        dataSrc: "",
        data: {"id": id}
    }).done(result => {
        result.sort(function (a, b) {
            return new Date(b.endDate) - new Date(a.endDate);
        });
        $.each(result, function (key, val) {
            status = enumStatus(val.status);
            
            var today = new Date(val.endDate);
            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            var dateTime = date + ' ' + time;
            body += `<li class="list-unstyled">
                        <div class="d-flex">
                            <img class="gambar" src="/img/usercartoon.png" alt="" />
                            <div class="pl-3 email">
                                <p>${val.name} <span> Add this Task to ${status}</span></p>
                                <span> ${dateTime}</span>
                            </div>
                        </div>
                    </li>`
        })
        $('#LogHistory').html(body);
    }).fail(error => { })
}

function detailTask(id) {
    $.ajax({
        url: "https://localhost:44374/Task/DetailTask/",
        dataType: "JSON",
        dataSrc: "",
        data: { "id": id }
    }).done(result => { 
        $('#descriptionTask').text(result.description);
        status = enumStatus(result.status)
        $('#statusTask').text(status);
    }).fail(error => { })
}

// Gant View
$('button#GanttView').on('click', function () {
    $('#ListKanban').attr('hidden', true);
})

$('button#KanbanView').on('click', function () {
    $('#ListKanban').removeAttr('hidden', true);
})

// Enum Status
function enumStatus(status) {
    switch (status) {
        case 1:
            return "Design";
            break;
        case 2:
            return "Doing"
            break;
        case 3:
            return "CodeReview"
            break;
        case 4:
            return "Testing"
            break;
        case 5:
            return "Done"
            break;
        default:
            return "To-Do"
    }
}