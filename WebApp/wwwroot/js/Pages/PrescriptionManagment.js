function UserInfoController() {
    this.ViewName = "User";
    this.ApiService = "User";

    this.InitViewUser = function () {
        console.log("User Info");


        this.LoadTableUserInfo();
    }

    

    this.LoadTableUserInfo = function () {
        var us = new ControlActions();

        //Ruta del api
        var urlService = us.GetUrlApiService(this.ApiService + "/RetrieveAllRoleUser")

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "lastName" }
        columns[3] = { 'data': "phoneNumber" }
        columns[4] = { 'data': "email" }
        columns[5] = { 'data': "sex" }
        columns[6] = { 'data': "province" }
        columns[7] = { 'data': "address" }

        // Crear la tabla utilizando DataTables
        $("#tblUserInfo").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblUserInfo tbody').on('click', 'tr', function () {

            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            //Extraer la data del registro contenido en la fila
            var user = $('#tblUserInfo').DataTable().row(row).data();

            var userIdWithName = user.id + ' - ' + user.name + '  ' + user.lastName;

            // Establecer el valor del campo TxtUserId con la cadena creada
            $("#TxtUserId").val(userIdWithName);
            


        });
    }
}

function DoctorInfoController() {
    this.ViewName = "Doctor";
    this.ApiService = "Doctor";

    this.InitViewDoctor = function () {
        console.log("Doctor Info");


        this.LoadTableDoctorInfo();
    }

    this.LoadTableDoctorInfo = function () {
        var dc = new ControlActions();

        //Ruta del api
        var urlService = dc.GetUrlApiService(this.ApiService + "/RetrieveAll")

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "lastName" }
        columns[3] = { 'data': "phoneNumber" }
        columns[4] = { 'data': "email" }
        columns[5] = { 'data': "sex" }
        columns[6] = { 'data': "branchID" }
        columns[7] = { 'data': "specialty" }

        // Crear la tabla utilizando DataTables
        $("#tblDoctorInfo").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblDoctorInfo tbody').on('click', 'tr', function () {

            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            //Extraer la data del registro contenido en la fila
            var doctor = $('#tblDoctorInfo').DataTable().row(row).data();

            var doctorIdWithName = doctor.id + ' - ' + doctor.name + '  ' + doctor.lastName;

            // Establecer el valor del campo TxtUserId con la cadena creada
            $("#TxtDoctorId").val(doctorIdWithName);



        });
    }
}

$(document).ready(function () {
    var us = new UserInfoController();
    us.InitViewUser();

    var dc = new DoctorInfoController();
    dc.InitViewDoctor();
})

