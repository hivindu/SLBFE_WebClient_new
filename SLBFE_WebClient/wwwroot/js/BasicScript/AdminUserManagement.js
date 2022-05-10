$(document).ready(function () {
    getAllUsers();
});

var table;

function getAllUsers() {

    $.post("/Home/GetAllUsers", function (usersList, status) {

        $("#userDetails > tbody").empty();
        for (var singleUser of usersList) {

            var tr = $("<tr/>");

            tr.append("<td>" + singleUser.name + "</td>");
            tr.append("<td>" + singleUser.email + "</td>");

            if (singleUser.isActive) {
                tr.append("<td> Active </td>");
            } else {
                tr.append("<td> Deactivated </td>");
            }


            let buttons = "<td class='d-flex'>";

            if (singleUser.isActive) {
                buttons = buttons + "<a href='#' data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-danger ' data-toggle='modal' onclick='+changeStatus(\"" + escape(JSON.stringify(singleUser)) + "\")'>Deactivate</a>  ";
            } else {
                buttons = buttons + "<a href='#' data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-success ' data-toggle='modal' onclick='+changeStatus(\"" + escape(JSON.stringify(singleUser)) + "\")'>Activate</a>  ";
            }
            buttons = buttons + "&nbsp <a href='#' data-toggle='tooltip' title='Delete Vacancy' class='btn btn-sm btn-danger ' data-toggle='modal' onclick='+showRemove(\"" + escape(JSON.stringify(singleUser)) + "\")'><em class='fa fa-trash-o'></em></a>  ";
            buttons = buttons + "</td>";
            tr.append(buttons);

            $('#userDetails').append(tr);

        }

    }).done(function () {
    });
}

function AddNewUser() {
    var name = $('#insName').val();
    var email = $('#insEmail').val();
    var password = $('#insPassword').val();

    var request = {
        Name: name,
        Email: email,
        Password: password,
        UserType: 1,
    }

    $.post("/Home/CreateUser", { request: request }, function (response, status) {
        if (response == null) {
            alert("Something went wrong!");
        } else {
            alert("New user added Succefully!");
        }
    }).done(function () {
    });
}

function showModal() {
    $('#userAddModal').modal('show');
}

function showRemove(data) {
    var vacancyInfo = JSON.parse(unescape(data));
    $('#txtDelId').text(vacancyInfo.id);

    $('#userDeleteModal').modal('show');
}

function RemoveUser() {
    var id = $('#txtDelId').text();
    $.post("/Home/DeleteUser", { userId: id }, function (response, status) {
        if (!response) {
            alert("Something went wrong!");
        } else {
            alert("Deleted succesfuly!");
        }
    }).done(function () {

    });
}

function changeStatus(dataset) {
    var companyInfo = JSON.parse(unescape(dataset));
    var status;
    if (companyInfo.isActive) {
        status = false;
    } else {
        status = true;
    }

    $.post("/Home/ChangeCompanyActivationStatus", { companyId: companyInfo.id, status: status }, function (response, status) {

        if (response) {
            alert('Activation status changed!');
        } else {
            alert('something went wrong!');
        }

    }).done(function () {

    });
}