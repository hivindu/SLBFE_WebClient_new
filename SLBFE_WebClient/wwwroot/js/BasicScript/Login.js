function MyFunction() {
    var data = {
        Email: $('#citizensNIC').val(),
        Password: $('#password').val()
    };
    $.post("/Home/Create", { collection: data }, function (dataSet) {

        if (dataSet != null) {
            if (dataSet.isActive) {
                if (dataSet.userType == 1) {
                    window.location.assign("JobSeekers");
                }
                if (dataSet.userType == 2) {
                    window.location.assign("CompanyView");
                }
            } else {
                alert("Account deactivated!");
            }
        } else {
            alert("Invalid user Email or Password!");
        }
    });
}

function redirectToRegister()
{
    window.location.assign("CompanySignup");
}