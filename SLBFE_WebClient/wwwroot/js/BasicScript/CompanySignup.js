function CreateCompanyAccount()
{
    var name = $('#insCompanyName').val();
    var email = $('#insCompanyEmail').val();
    var address = $('#insCompanyAddress').val();
    var pw = $('#insPassword').val();
    var request = {
        CompanyName : name,
        CompanyAddress : address,
        Email : email,
        Password : pw,
        UserType : 2
    }

    $.post("/Home/CreateCompanyProfile", { request: request }, function (response, status) {
        if (response == null) {
            alert("Something went wrong!");
        } else {
            alert("Inserted successfully!");
        }
    }).done(function () {
        clearFeilds();
    });
}

function clearFeilds() {
    $('#insCompanyName').val('');
    $('#insCompanyEmail').val('');
    $('#insCompanyAddress').val('');
    $('#insPassword').val('');
}