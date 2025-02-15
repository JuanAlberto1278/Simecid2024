function ProfileData() {
    this.ViewName = "User";
    this.ApiService = "User";
    var email;

    this.InitView = function () {
        console.log("init view sign up");
        let urlParams = new URLSearchParams(window.location.search);
        email = urlParams.get(`email`); // Obtener el valor de email de los parámetros de la URL
        console.log(email);

        this.RetrieveUserByEmail(email); // Pasar email como argumento al llamar al método
    };

    this.RetrieveUserByEmail = function (email) {
        let route = "User/RetrieveUserByEmail?email=" + email;
        let ca = new ControlActions();

        ca.GetToApi(route, function (response) {
            console.log("User retrieved successfully:");
            console.log(response);

            // Verificar si los datos están disponibles y no son undefined
            if (response) {
                // Establecer los datos recibidos como placeholders en los inputs correspondientes
                document.getElementById("name").placeholder = response.name || '';
                document.getElementById("lastName").placeholder = response.lastName || '';
                document.getElementById("phoneNumber").placeholder = response.phoneNumber || '';
                document.getElementById("email").placeholder = response.email || '';
                document.getElementById("sex").value = response.sex || ''; // Establecer el valor seleccionado en el select
                document.getElementById("birthdate").placeholder = response.birthDate || '';
                document.getElementById("address").placeholder = response.address || '';
            } else {
                console.error("No data received from server");
            }

            // Obtener el nombre de la imagen del usuario desde la respuesta
            let imageName = response.imageName;

            // Establecer la ruta de la imagen en el atributo src del elemento con clase 'profilePic'
            let profilePic = document.querySelector('.profilePic');
            profilePic.src = "/ProfilePictureUploads/" + imageName;

            let profileIcon = document.querySelector('.userIcon');
            profileIcon.src = "/ProfilePictureUploads/" + imageName;


        }, function (error) {
            console.error("Error retrieving user:", error);
        });
    }
}

$(document).ready(function () {
    var pd = new ProfileData();
    pd.InitView();
})
