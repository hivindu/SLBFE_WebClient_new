$(document).ready(function () {
    getAllCompanies();
});

var table;

function getAllCompanies() {

    $.post("/Home/GetAllCompanies", function (companyDetails, status) {

        $("#companyDetails > tbody").empty();

        for (var company of companyDetails) {

            var tr = $("<tr/>");

            tr.append("<td>" + company.name + "</td>");
            tr.append("<td>" + company.email + "</td>");

            if (company.companyDetails != null) {
                tr.append("<td>" + company.companyDetails.companyAddress + "</td>");
            } else {
                tr.append("<td> N/A </td>");
            }
            
            let options = "<td class='d-flex'>";
          
            if (company.isActive) {
                options = options + "<a href='#' data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-danger ' data-toggle='modal' onclick='+changeStatus(\"" + escape(JSON.stringify(company)) + "\")'>Deactivate</a>  ";
            } else {
                options = options + "<a href='#'data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-success ' data-toggle='modal' onclick='+changeStatus(\"" + escape(JSON.stringify(company)) + "\")'>Activate</a>  ";
            }

            options = options + "</td>";
            tr.append(options);

            $('#companyDetails').append(tr);
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