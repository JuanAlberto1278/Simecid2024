function AppointmentController() {
    this.ViewName = "Appointment";
    this.ApiService = "Appointment";

    // Método para ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("appointment view init");

        var self = this;

        // Bind del click del botón create con la función correspondiente
        $("#btnCreate").click(function (event) {
            // Prevenir que el botón cause la recarga de la página
            event.preventDefault();

            // Llamar a la función Create del contexto actual
            self.Create();
        });

        $("#btnUpdate").click(function (event) {
            event.preventDefault();

            // Llamar a la función Create del contexto actual
            self.Update();
        });
        $("#btnDelete").click(function (event) {
            event.preventDefault();

            // Llamar a la función Create del contexto actual
            self.Delete();
        });

        this.loadTable();

    };

    this.Create = function () {
        try {
            // Obtener el correo electrónico de la URL
            const urlParams = new URLSearchParams(window.location.search);
            const email = urlParams.get("email");

            // Hacer una solicitud AJAX GET para obtener el patientId asociado al email
            var ca = new ControlActions();
            var patientUrl = ca.GetUrlApiService("Patient/RetrieveIdByEmail?email=" + email); // Cambia la ruta según tu API
            $.get(patientUrl, function (data) {
                // Verificar si se obtuvo el paciente correctamente
                if (data && data.id) {
                    // Crear un dto para la cita con el patientId obtenido
                    var appointment = {
                        patientId: data.id,
                        patientName: "Default",
                        patientLastName: "Default",
                        doctorId: 26, // Cambio doctor
                        doctorName: "Default",
                        doctorLastName: "Default",
                        serviceId: $("#ServiceId").val(),
                        serviceName: "Default",
                        branchId: $("#BranchId").val(),
                        branchName: "Default",
                        startTime: $("#startTime").val(), // Formato de fecha aquí
                        endTime: $("#startTime").val(),
                        text: $("#txtReason").val(),
                        status: $("#txtStatus").val(),
                        userEmail: email // Agregar el campo de correo electrónico
                    };

                    // Invocar al API para crear la cita
                    var serviceRoute = "Appointment/Create"; // Ajusta la ruta según tu API
                    ca.PostToAPI(serviceRoute, appointment, function () {
                        console.log("Appointment Created --->" + JSON.stringify(appointment));
                        $('#tblAppointments').DataTable().ajax.reload();
                    });
                } else {
                    console.error("No se encontró el paciente asociado al correo electrónico proporcionado.");
                }
            });
        } catch (error) {
            console.error("Error occurred while creating appointment:", error);
        }
    };


    // Obtener el correo electrónico de la URL
    

    this.Update = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const email = urlParams.get("email");

        // crear un dto
        var appointment = {};
        appointment.id = $("#txtId").val();
        appointment.patientId = $("#patientId").val();
        appointment.patientName = $("#txtPatientName").val();
        appointment.patientLastName = $("#txtPatientLastName").val();
        appointment.doctorId = $("#doctorId").val();
        appointment.doctorName = $("#doctorName").val();
        appointment.doctorLastName = $("#doctorLastName").val();
        appointment.serviceId = $("#serviceId").val();
        appointment.serviceName = $("#txtServiceName").val();
        appointment.branchId = $("#branchId").val();
        appointment.branchName = $("#txtBranchName").val();
        appointment.startTime = $("#txtStartTime").val(); // Format date here
        appointment.endTime = $("#txtStartTime").val();
        appointment.text = $("#text").val();
        appointment.status = $("#status").val();
        appointment.userEmail = email; // Agregar el campo de correo electrónico

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, appointment, function () {
            console.log("Appointment Updated --->" + JSON.stringify(appointment));
            $('#tblAppointments').DataTable().ajax.reload();
        })
    }

    this.Delete = function () {

        // crear un dto
        var appointment = {};
        appointment.id = $("#txtId").val();
        appointment.patientId = $("#patientId").val();
        appointment.patientName = $("#txtPatientName").val();
        appointment.patientLastName = $("#txtPatientLastName").val();
        appointment.doctorId = $("#doctorId").val();
        appointment.doctorName = $("#doctorName").val();
        appointment.doctorLastName = $("#doctorLastName").val();
        appointment.serviceId = $("#serviceId").val();
        appointment.serviceName = $("#txtServiceName").val();
        appointment.branchId = $("#branchId").val();
        appointment.branchName = $("#txtBranchName").val();
        appointment.startTime = formatDate($("#txtStartTime").val()); // Format date here
        appointment.endTime = formatDate($("#txtStartTime").val());
        appointment.text = $("#text").val();
        appointment.status = $("#status").val();
        appointment.userEmail = "defautl";


        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, appointment, function () {
            console.log("Appointment Deleted --->" + JSON.stringify(appointment));
            $('#tblAppointments').DataTable().ajax.reload();
        })
    }

    this.loadTable = function () {
        var ca = new ControlActions();

        // Obtener el correo electrónico de la URL
        const urlParams = new URLSearchParams(window.location.search);
        const userEmail = urlParams.get("email");

        // Ruta del API para obtener las citas por email
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveByEmail?userEmail=" + userEmail);

        // Definir las columnas a extraer del JSON de respuesta
        var columns = [
            { 'data': 'id' },
            { 'data': 'patientId' },
            { 'data': 'patientName' },
            { 'data': 'patientLastName' },
            { 'data': 'doctorId' },
            { 'data': 'doctorName' },
            { 'data': 'doctorLastName' },
            { 'data': 'serviceId' },
            { 'data': 'serviceName' },
            { 'data': 'branchId' },
            { 'data': 'branchName' },
            { 'data': 'startTime' },
            { 'data': 'endTime' },
            { 'data': 'text' },
            { 'data': 'status' }
        ];

        // Inicializar la tabla como un DataTable
        $("#tblAppointments").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns,
            "columnDefs": [
                { "targets": [1, 4, 5, 6, 7, 8, 9, 12], "visible": false } // Ocultar ciertas columnas si es necesario
            ]
        });

        $('#tblAppointments tbody').on('click', 'tr', function () {
            // Extraer fila a la que se le dio clic
            var row = $(this).closest('tr');
            // Extraer la data del registro contenido en la fila
            var appointment = $('#tblAppointments').DataTable().row(row).data();
            // Mapeo de los valores del objeto data con el formulario
            $("#txtId").val(appointment.id);
            $("#patientId").val(appointment.patientId);
            $("#txtPatientName").val(appointment.patientName);
            $("#txtPatientLastName").val(appointment.patientLastName);
            $("#doctorId").val(appointment.doctorId);
            $("#doctorName").val(appointment.doctorName);
            $("#doctorLastName").val(appointment.doctorLastName);
            $("#serviceId").val(appointment.serviceId);
            $("#txtServiceName").val(appointment.serviceName);
            $("#branchId").val(appointment.branchId);
            $("#txtBranchName").val(appointment.branchName);
            $("#text").val(appointment.text);
            $("#status").val(appointment.status);
        });
    }

    function formatDate(dateString) {
        var date = new Date(dateString);
        var year = date.getFullYear();
        var month = ('0' + (date.getMonth() + 1)).slice(-2);
        var day = ('0' + date.getDate()).slice(-2);
        return year + '-' + month + '-' + day;
    }

}

function AllBranchInfoController() {
    // Propiedades de la clase
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";

    // Método para inicializar la vista de todas las sucursales
    this.InitViewAllBranch = function () {

        // Asignar evento click al botón de agregar
        $("#btnAdd").click(function () {
            var vc = new AllBranchInfoController();
            vc.Add();
        });

        // Cargar la tabla de información de todas las sucursales
        this.LoadTableAllInfoBranch();

        this.LoadTableBranch();

    }

    // Cargar branchs al form
    this.LoadTableAllInfoBranch = function () {
        var bi = new ControlActions();

        //Ruta del api
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveAll")

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "address" }
        columns[3] = { 'data': "description" }

        // Crear la tabla utilizando DataTables
        $("#tblAllBranches").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblAllBranches tbody').on('click', 'tr', function () {

            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            //Extraer la data del registro contenido en la fila
            var branch = $('#tblAllBranches').DataTable().row(row).data();

            //Mapeo de los valores del objeto data con el formulario
            $("#BranchId").val(branch.id);
            $("#TxtName").val(branch.name);


        });
    }

    // Cargar servicios al form
    this.LoadTableBranch = function () {
        var ba = new ControlActions();

        //Ruta del api
        var urlService = ba.GetUrlApiService(this.ApiService + "/RetrieveAllBranchServices")
        console.log(urlService);

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "serviceId" }
        columns[3] = { 'data': "serviceName" }
        columns[4] = { 'data': "servicePrice" }
        columns[5] = { 'data': "serviceTax" }
        console.log(columns);

        // Crear la tabla utilizando DataTables
        $("#tblBranch").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        // Asignar evento al hacer clic en una fila de la tabla
        $('#tblBranch tbody').on('click', 'tr', function () {
            // Extraer la fila en la que se hizo clic
            var row = $(this).closest('tr');
            // Extraer los datos del servicio de la fila
            var rowData = $('#tblBranch').DataTable().row(row).data();
            // Mapear los valores del objeto de datos con los campos del formulario
            $("#ServiceId").val(rowData.serviceId);
            $("#textServiceName").val(rowData.serviceName);
        });
    }
}

// Instanciamiento de la clase 
$(document).ready(function () {
    var vc = new AppointmentController();
    vc.InitView();
    var ba = new AllBranchInfoController();
    ba.InitViewAllBranch();
});