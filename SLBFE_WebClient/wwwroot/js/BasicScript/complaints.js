$(document).ready(function () {
    getAllPendingVacancies();
});

var table;

function getAllPendingVacancies() {
    $.post("/Home/GetAllComplaints", function (complaintCitizenData, status) {

        for (var complaint of complaintCitizenData) {
            $("#complaintDetails > tbody").empty();
            var tr = $("<tr/>");

            tr.append("<td>" + complaint.complaintUserName + "</td>");
            tr.append("<td>" + complaint.complainMessage + "</td>");


            let buttons = "<td class='d-flex'>";
            buttons = buttons + " <a href='#' style='height: 24px;' data-toggle='tooltip' title='Complaint Reply' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+showData(\"" + escape(JSON.stringify(complaint)) + "\")'><em class='fa fa-comments'></em></a>  ";
            buttons = buttons + "</td>";
            tr.append(buttons);

            $('#complaintDetails').append(tr);
        }

    }).done(function () {
    });
}

function showData(dataSet)
{
    var complaint = JSON.parse(unescape(dataSet));

    $('#txtComplaintId').text(complaint.complaintId);
    $('#txtComplain').text(complaint.complainMessage);

    $('#CommentModel').modal('show');
}

function SendReply() {
    var complaintid = $('#txtComplaintId').text();
    var replyMessage = $('#replyMessage').val();

    $.post("/Home/ReplyToComplaint", { complaintId: complaintid, replyMessage: replyMessage }, function (reponse, status) {

        if (!reponse) {
            alert("Couldn't reply!");
        }
        getAllPendingVacancies();
    }).done(function () {

        $('#CommentModel').hide();

    });
}