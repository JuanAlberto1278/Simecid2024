function SignUpController() {
    this.ViewName = "User";
    this.ApiService = "User";
    var email;

    this.InitView = function () {
        console.log("init view sign up");
        let urlParams = new URLSearchParams(window.location.search);
        console.log(urlParams.get(`email`));
        $("#BtnSignIn").click(function () {
            let ec = new SignUpController();
            ec.Create();
            //mostrar algo que active el spinner
        });

        $("#verifyMe").click(function () {
            var sc = new EmailController2();
            var otp = $("#txtOTP").val();
            let urlParams = new URLSearchParams(window.location.search);
            email = urlParams.get(`email`)
            sc.ValidateOTP(email, otp);

        });
    };

    this.SendEmail = function () {
        let name = $("#txtName").val();
        let lastName = $("#txtLastName").val();
        let phoneNumber = $("#txtPhoneNumber").val();
        let email = $("#txtEmail").val();
        let password = $("#txtPassword").val();
        let sex = $("#txtSex").val();
        let birthdate = new Date($("#txtBirthdate").val());
        let province = $("#txtProvince").val();
        let address = $("#txtAddress").val();
        let user = {
            name: name,
            lastName: lastName,
            email: email,
            phoneNumber: phoneNumber,
            password: password,
            sex: sex,
            birthDate: birthdate,
            role: 'User',
            status: 'Default',
            province: province,
            address: address
        };

        var emailController = new EmailController2();
        emailController.SendEmail(email, function () {
            this.ValidateOTP(email);
        });

    };

    this.Create = function () {

        console.log("Attempting to create user...");

        let profileImageInput = $("#profileImage")[0];
        if (!profileImageInput) {
            console.error("Profile image input not found");
            return;
        }

        let files = profileImageInput.files;
        if (!files || files.length === 0) {
            console.error("No files selected");
            return;
        }

        let imageName = files[0].name;
        console.log("Image name:", imageName);

        let name1 = $("#txtName").val();
        let lastName = $("#txtLastName").val();
        let phoneNumber = $("#txtPhoneNumber").val();
        let email = $("#txtEmail").val();
        let password = $("#txtPassword").val();
        let sex = $("#txtSex").val();
        let birthdateValue = $("#txtBirthdate").val();
        let birthdate = new Date(birthdateValue);
        let province = $("#txtProvince").val();
        let address = $("#txtAddress").val();

        let user = {
            name: name1,
            lastName: lastName,
            email: email,
            phoneNumber: phoneNumber,
            password: password,
            sex: sex,
            birthDate: birthdate,
            role: 'User',
            status: 'Default',
            province: province,
            address: address,
            imageName: imageName
        };

        console.log("User object:", user);

        let ca = new ControlActions();
        let srvR = "User/Create";

        ca.PostToAPI(srvR, user, function (response) {
            console.log("API response:", response);
            setTimeout(function () {
                window.location.href = `/CodeVerification?email=${user.email}`;
            }, 1000);
        })
    }
}

function EmailController2() {
    this.ViewName = "Email";
    this.ApiService = "Email";
    var ca = new ControlActions();

    this.SendEmail = function (email, callback) {
        var keysAuth = {
            emailAddress: email,
            otp: 0
        };

        var serviceRoute = this.ApiService + "/SendEmail";
        ca.PostToAPI(serviceRoute, keysAuth, function (response) {
            console.log("Email sent successfully");
            if (callback && typeof callback === 'function') {
                callback();
            }
        }, function (error) {
            console.error("Error sending email:", error);
        });
    };


    this.ValidateOTP = function (email, otp) {
        if (email === "" || otp === "") {
            return;
        }

        let data = {
            Email: email,
            OTP: otp
        }

        var srv = "ValidateOTP/VerifyOtp";

        try {
            ca.PostToAPI(srv, data, function (response) {
                setTimeout(function () {
                    window.location.href = `/Login`;
                }, 500);
            });
        } catch (error)
        {
            return StatusCode(500, "Something went ");
        }
    };
}

$(document).ready(function () {
    window.history.pushState(null, "", window.location.href);
    window.onpopstate = function () {
        window.history.pushState(null, "", window.location.href);
    };

        var ec = new SignUpController();
        ec.InitView();
    });