$(document).ready(function () {
    getAllPostedVacancies();
});

var table;

function getAllPostedVacancies() {
    var companyId = getCookie('User_TL_Id');

    $.post("/Home/GetVacanciesPerCompany", { companyId: companyId}, function (vacanciesList, status) {

        $("#vacancyDetails > tbody").empty();
        for (var vacancy of vacanciesList) {

            var tr = $("<tr/>");

            tr.append("<td>" + vacancy.jobTitle + "</td>");
            tr.append("<td>" + vacancy.jobDescription + "</td>");

            if (vacancy.isApproved) {
                tr.append("<td> Approved </td>");
            } else {
                tr.append("<td> Pending </td>");
            }
           
            let buttons = "<td class='d-flex'>";
            buttons = buttons + "&nbsp <a href='#' style='height: 24px;' data-toggle='tooltip' title='Applicants Vacancy' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+ShowApplicantData(\"" + escape(JSON.stringify(vacancy)) + "\")'><em class='fa fa-info'></em></a>  ";
            buttons = buttons + "&nbsp <a href='#' style='height: 24px;' data-toggle='tooltip' title='Edit Vacancy' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+showData(\"" + escape(JSON.stringify(vacancy)) + "\")'><em class='fa fa-pencil-square-o'></em></a>  ";
            buttons = buttons + "&nbsp <a href='#' style='height: 24px;' data-toggle='tooltip' title='Delete Vacancy' class='btn btn-sm btn-danger ' data-toggle='modal' onclick='+showRemove(\"" + escape(JSON.stringify(vacancy)) + "\")'><em class='fa fa-trash-o'></em></a>  ";
            buttons = buttons + "</td>";
            tr.append(buttons);

            $('#vacancyDetails').append(tr);

        }

    }).done(function () {
    });
}

function showData(vacanciesDataSet) {
    var vacancyInfo = JSON.parse(unescape(vacanciesDataSet));

    $('#txtId').text(vacancyInfo.id);
    $('#updJobTitle').val(vacancyInfo.jobTitle);
    $('#updJobDescription').val(vacancyInfo.jobDescription);
    $('#updDeadline').val(vacancyInfo.deadline);
    $('#txtApproval').text(vacancyInfo.isApproved);
    
    $('#vacancyEditModal').modal('show');
}

function AddNewVacancy() {
    var jobTitle = $('#insJobTitle').val();
    var jobDescription = $('#insJobDescription').val();
    var companyId = getCookie('User_TL_Id');
    var deadLine = $('#insDeadline').val();

    var request = {
        JobTitle: jobTitle,
        JobDescription: jobDescription,
        UserId: companyId,
        Deadline: deadLine,
    }

    $.post("/Home/CreateJobVacancy", { request: request }, function (response, status) {
        if (response == null)
        {
            alert("Something went wrong!");
        }
    }).done(function () {
    });
}

function EditVacancy()
{
    var id = $('#txtId').text();
    var jobTitle = $('#updJobTitle').val();
    var jobDescription = $('#updJobDescription').val();
    var companyId = getCookie('User_TL_Id');
    var deadLine = $('#updDeadline').val();
    var approval = $('#txtApproval').text();

    var request = {
        Id: id,
        JobTitle: jobTitle,
        JobDescription: jobDescription,
        UserId: companyId,
        Deadline: deadLine,
        IsApproved: approval,
    }

    $.post("/Home/UpdateVacancy", { request: request }, function (response, status) {
        if (!response) {
            alert("Something went wrong!");
        }
    }).done(function () {
    });
}

function showModal() {
    $('#vacancyAddModal').modal('show');
}

function downloadCertificate(quolification) {

    const data = quolification.certificateImage;
    const arrayBuffer = base64ToArrayBuffer(data);
    createAndDownloadBlobFile(arrayBuffer, quolification.qualificationName);
}

function base64ToArrayBuffer(base64) {
    const binaryString = window.atob(base64); // Comment this if not using base64
    const bytes = new Uint8Array(binaryString.length);
    return bytes.map((byte, i) => binaryString.charCodeAt(i));
}

function createAndDownloadBlobFile(body, filename, extension = 'pdf') {
    const blob = new Blob([body]);
    const fileName = `${filename}.${extension}`;
    if (navigator.msSaveBlob) {
        // IE 10+
        navigator.msSaveBlob(blob, fileName);
    } else {
        const link = document.createElement('a');
        // Browsers that support HTML5 download attribute
        if (link.download !== undefined) {
            const url = URL.createObjectURL(blob);
            link.setAttribute('href', url);
            link.setAttribute('download', fileName);
            link.style.visibility = 'hidden';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    }
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var c of ca) {
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function showRemove(data)
{
    var vacancyInfo = JSON.parse(unescape(data));
    $('#txtDelId').text(vacancyInfo.id);

    $('#vacancyDeleteModal').modal('show');
}

function RemoveVacancy()
{
    var id = $('#txtDelId').text();
    $.post("/Home/DeleteVacancy", { vacancyId: id }, function (response, status) {
        if (!response) {
            alert("Something went wrong!");
        }
    }).done(function () {

    });
}

function ShowApplicantData(vacancyInfo)
{
    var deserialzedVacancyInfo = JSON.parse(unescape(vacancyInfo));

    $.post("/Home/GetDetailedCitizenPerApplication", { vacancyId: deserialzedVacancyInfo.id }, function (detailedCitizensList, status) {

        $("#applicantDetails > tbody").empty();
        for (var detailCitizen of detailedCitizensList) {

            var tr = $("<tr/>"); 

            tr.append("<td>" + detailCitizen.citizenBasicDetails.name + "</td>");
            tr.append("<td>" + detailCitizen.citizenBasicDetails.email + "</td>");

            if (detailCitizen.quolificationsList != null) {
                let buttons = "<td class='d-flex'>";
                buttons = buttons + "<a href='#' style='height: 24px;' data-toggle='tooltip' title='Download Certificates' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+downloadCertificates(\"" + escape(JSON.stringify(detailCitizen.quolificationsList)) + "\")'><em class='fa fa-download'></em></a>  ";
                buttons = buttons + "</td>";
                tr.append(buttons);
            } else {
                tr.append("<td> N/A </td>");
            }
            
            $('#applicantDetails').append(tr);
        }
        $('#applicantDetailsModal').modal('show');
    }).done(function () {
    });
}

function downloadCertificates(certificateData)
{
    var certificates = JSON.parse(unescape(certificateData));

    if (certificates != null)
    {
        for (var certificate of certificates)
        {
            downloadCertificate(certificate);
        }
    }
}