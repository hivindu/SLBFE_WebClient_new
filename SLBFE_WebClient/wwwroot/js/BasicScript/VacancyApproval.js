$(document).ready(function () {
    getAllPendingVacancies();
});

var table;

function getAllPendingVacancies() {
    $.post("/Home/GetAllPendingVacancies", function (vacanciesList, status) {

        for (var vacancy of vacanciesList) {

            $("#pendingApprovalVacancies > tbody").empty();
            var tr = $("<tr/>");

            tr.append("<td>" + vacancy.jobTitle + "</td>");
            tr.append("<td>" + vacancy.jobDescription + "</td>");


            let buttons = "<td class='d-flex'>";
            buttons = buttons + "<a href='#' data-toggle='tooltip' title='Show Vacancy Details' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+ShowDetails(\"" + escape(JSON.stringify(vacancy)) + "\")'><em class='fa fa-pencil-square-o'></em></a>  ";
            buttons = buttons + "</td>";
            tr.append(buttons);

            $('#pendingApprovalVacancies').append(tr);

        }

    }).done(function () {
    });
}

function ShowDetails(details)
{
    var vacancy = JSON.parse(unescape(details));
    $('#txtTitle').text(vacancy.jobTitle);
    $('#txtDescripion').text(vacancy.jobDescription);
    $('#txtDeadline').text(vacancy.Deadline);
    $('#txtId').text(vacancy.id);

    $('#vacancyDetails').modal('show');
}

function ApproveVacancy() {
    var id = $('#txtId').text();
    $.post("/Home/ApproveVacancy", { vacancyId: id }, function (responseResult, status) {

        if (!responseResult) {
            alert("Cloudn't approve!");
        }

    }).done(function () {
    });
}