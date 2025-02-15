function EmailController() {
    this.InitView = function () {
        console.log("init view mail")
        /*let urlParams = new URLSearchParams(window.location.search);
        console.log(urlParams.get(`email`));*/

        $("#btnTry").click(function () {
            let ec = new EmailController();
            ec.SendEmail();
        });
    };

    this.SendEmail = function () {
        let srvRoute = "User/ForgotPassword";
        var ca = new ControlActions();
        const email = $("#email").val();

        console.log("email value " + email);
        if (email === "") {
            return;
        }

        let keysAuth = {
            "email": email
        };

        ca.PostToAPI(srvRoute, keysAuth, function (response) {
            console.log(response)
            setTimeout(function () {
                window.location.href = `/RecoverPassword?email=${email}`;
            }, 1000);
        })
    }
}


$(document).ready(function () {
    var ec = new EmailController();
    ec.InitView();
});

