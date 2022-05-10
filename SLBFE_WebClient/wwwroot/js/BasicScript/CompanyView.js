$(document).ready(function () {
    getAllJobSeekers();
});

var table;

function getAllJobSeekers() {
    $.post("/Home/GetAllJobSeekersAsync", function (jobSeekersData, status) {

        $("#citizensDetails > tbody").empty();
        for (var jobSeeker of jobSeekersData) {

            var tr = $("<tr/>");

            tr.append("<td>" + jobSeeker.name + "</td>");
            tr.append("<td>" + jobSeeker.email + "</td>");
            tr.append("<td>" + jobSeeker.profession + "</td>");

            tr.append("<td>" + jobSeeker.employed + "</td>");

            let buttons = "<td class='d-flex'>";
            buttons = buttons + "&nbsp <a href='#' style='height: 24px;' data-toggle='tooltip' title='Edit Deposit' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+showData(\"" + escape(JSON.stringify(jobSeeker)) + "\")'><em class='fa fa-pencil-square-o'></em></a>  ";
            buttons = buttons + "</td>";
            tr.append(buttons);

            $('#citizensDetails').append(tr);

        }

    }).done(function () {
    });
}

function showData(citizenDataSet)
{
    var citizensInfo = JSON.parse(unescape(citizenDataSet));
    $('#txtName').text(citizensInfo.name);
    $('#txtNic').text(citizensInfo.nic);
    $('#txtAddress').text(citizensInfo.address);
    $('#txtEmail').text(citizensInfo.email);
    $('#txtValidated').text(citizensInfo.va);

    $.post("/Home/GetCitizensDetailsById", { citizenId: citizensInfo.id }, function (cetizensDetailedResponse, status) {

        var qualifications = cetizensDetailedResponse.quolificationsList;

        $("#qualificationDetaisl > tbody").empty();
        for (var quolification of qualifications) {

            var tr = $("<tr/>");

            tr.append("<td>" + quolification.qualificationName + "</td>");

            let options = "<td class='d-flex'>";
            options = options + "<a href='#' style='height: 24px;' data-toggle='tooltip' title='Download Quolification' class='btn btn-sm btn-info' onclick='+downloadCertificate(\"" + escape(JSON.stringify(quolification)) + "\")' data-toggle='modal'><em class='fa fa-pencil-square-o'></em></a>  ";
            options = options + "</td>";
            tr.append(options);

            $('#qualificationDetaisl').append(tr);

        }

    }).done(function () {
    });

    $('#citizensDetailModel').modal('show');
}

function downloadCertificate(quolification)
{
    var quolification = JSON.parse(unescape(quolification));

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

function resetFilter() {
    $('#advanceFilterForm')[0].reset()
    $('#txtEmployment').val(null).change();
    $('#txtProduct').val(null).change();

    $('#txtUser').val(null).change();
    $('#txtAgent').val(null).change();
    $('#txtCurrency').val(null).change();
    $('#txtStatus').val(null).change();
    $('#txtSupplier').val(null).change();

}

$('#btnSearchToggle').click(function () {
    resetFilter()
    $('#CitizensAdvanceSearch').toggle("slow");

});