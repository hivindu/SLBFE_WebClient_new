$(document).ready(function () {
    getAllJobSeekers();
});

var table;

function getAllJobSeekers()
{
    $.post("/Home/GetAllJobSeekersAsync", function (jobSeekersData, status) {

        $("#citizensDetails > tbody").empty();

        for (var jobSeeker of jobSeekersData) {

            var tr = $("<tr/>");

            tr.append("<td>" + jobSeeker.name + "</td>");
            tr.append("<td>" + jobSeeker.email + "</td>");
            tr.append("<td>" + jobSeeker.profession + "</td>");

            if (jobSeeker.employed) {
                tr.append("<td> Employed </td>");
            } else {
                tr.append("<td> Un-Employed </td>");
            }

            let options = "<td class='d-flex'>";
            options = options + "<a href='#'  data-toggle='tooltip' title='Details of citizen' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+showData(\"" + escape(JSON.stringify(jobSeeker)) + "\")'><em class='fa fa-pencil-square-o'></em></a>  ";
            options = options + "&nbsp<a href='#'  data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-info ' data-toggle='modal' onclick='+viewConnections(\"" + escape(JSON.stringify(jobSeeker)) + "\")'><em class='fa fa-plug'></em></a>  ";

            if (jobSeeker.isActive) {
                options = options + "&nbsp<a href='#'  data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-danger ' data-toggle='modal' onclick='+changeStatus(\"" + escape(JSON.stringify(jobSeeker)) + "\")'>Deactivate</a>  ";
            } else {
                options = options + "&nbsp<a href='#'  data-toggle='tooltip' title='Connections of citizen' class='btn btn-sm btn-success ' data-toggle='modal' onclick='+changeStatus(\"" + escape(JSON.stringify(jobSeeker)) + "\")'>Activate</a>  ";
            }
            
            options = options + "</td>";
            tr.append(options);

            $('#citizensDetails').append(tr);
        }
    }).done(function () {
    });
}

function changeStatus(dataset)
{
    var citizensInfo = JSON.parse(unescape(dataset));
    var status;
    if (citizensInfo.isActive) {
        status = false;
    } else {
        status = true;
    }

    $.post("/Home/ChangeActivationStatus", { citizenId: citizensInfo.id, status: status }, function (response, status) {

        if (response) {
            alert('Activation status changed!');
        } else {
            alert('something went wrong!');
        }

    }).done(function () {

    });
}

function viewConnections(jobseekerData)
{
    var citizensInfo = JSON.parse(unescape(jobseekerData));

    var connections = citizensInfo.connections;

    if (connections != null)
    {
        $.post("/Home/GetCitizenById", { userId: citizensInfo.id }, function (citizen, status) {
            var tr = $("<tr/>");
            $("#connectionDetails > tbody").empty();

            tr.append("<td>" + citizen.name + "</td>");
            tr.append("<td>" + citizen.email + "</td>");
            tr.append("<td>" + citizen.profession + "</td>");

            $('#connectionDetails').append(tr);

        }).done(function () {
        });
    }

    $('#citizensConnectionsModel').modal('show');
}

function showData(citizenDataSet) {
    var citizensInfo = JSON.parse(unescape(citizenDataSet));
    $('#txtName').text(citizensInfo.name);
    $('#txtNic').text(citizensInfo.nic);
    $('#txtAddress').text(citizensInfo.address);
    $('#txtEmail').text(citizensInfo.email);
    $('#txtValidated').text(citizensInfo.va);

    if (!citizensInfo.verified) {
        $('#verifyButton').prop('disabled', true);
    } else {
        $('#verifyButton').prop('disabled', false);
    }

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

function downloadCertificate(quolification) {
    var quolification = JSON.parse(unescape(quolification));

    const data = quolification.certificateImage;
    const arrayBuffer = base64ToArrayBuffer(data);
    createAndDownloadBlobFile(arrayBuffer, quolification.qualificationName);
}

function base64ToArrayBuffer(base64) {
    const binaryString = window.atob(base64); 
    const bytes = new Uint8Array(binaryString.length);
    return bytes.map((byte, i) => binaryString.charCodeAt(i));
}

function createAndDownloadBlobFile(body, filename, extension = 'pdf') {
    const blob = new Blob([body]);
    const fileName = `${filename}.${extension}`;
    if (navigator.msSaveBlob) {
        navigator.msSaveBlob(blob, fileName);
    } else {
        const link = document.createElement('a');
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

function VerifyInformation()
{
    var citizenNic = $('#txtNic').text();

    if (citizenNic != null)
    {
        $.post("/Home/VerifyCitizen", { citizenNic: citizenNic }, function (verificationResponse, status) {

            if (!verificationResponse)
            {
                alert("Something went wrong! couldn't verify information");
            }

        }).done(function () {

            $('#citizensDetailModel').hide();

        });
    }
}