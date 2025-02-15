function LogoutController() {

    this.InitView = function () {
        console.log("logout init view")

        $("#LogOut").click(function () {
            window.history.pushState(null, "", window.location.href);
            window.onpopstate = function () {
                window.history.pushState(null, "", window.location.href);
            };

            window.location.href = "LandingPageSIMECID";
        });

    }
}

$(document).ready(function () {
    let lc = new LogoutController();
    lc.InitView();
})