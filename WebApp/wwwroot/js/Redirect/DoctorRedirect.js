function RedirectController() {
    this.ViewName = "Redirect";
    var ca = new ControlActions();

    this.InitView = function () {
        console.log("Redirect init view");

        // Obtener el email de la URL
        let urlParams = new URLSearchParams(window.location.search);
        let email = urlParams.get("email");

        // Configurar redirección en función del rol o acción

        // Redirección para Appointments
        $("#appointmentsMenuId").click(function () {
            this.RedirectToPage("/Doctor-Appointment", email);
        }.bind(this));

        // Redirección para Examination
        $("#examinationMenuId").click(function () {
            this.RedirectToPage("/Doctor-Examination", email);
        }.bind(this));

        // Redirección para Prescriptions
        $("#prescriptionsMenuId").click(function () {
            this.RedirectToPage("/Doctor-Prescription", email);
        }.bind(this));

        // Redirección para Medical Report
        $("#medicalReportMenuId").click(function () {
            this.RedirectToPage("/Doctor-MedicalReport", email);
        }.bind(this));

        // Redirección para Profile
        $("#profileMenuId").click(function () {
            this.RedirectToPage("/Doctor-Profile", email);
        }.bind(this));

        $("#settingMenuId").click(function () {
            this.RedirectToPage("/Doctor-Settings", email);
        }.bind(this));
    }

    this.RedirectToPage = function (pageName, email) {
        // Construir la URL de redirección con el email como parámetro
        let redirectUrl = `${window.location.origin}${pageName}?email=${encodeURIComponent(email)}`;

        // Redirigir a la página correspondiente
        window.location.href = redirectUrl;
    }
}

$(document).ready(function () {
    let rc = new RedirectController();
    rc.InitView();
});