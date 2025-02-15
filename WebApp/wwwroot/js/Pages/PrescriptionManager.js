function PrescriptionController() {
    this.ViewName = "Prescription";
    this.ApiService = "Prescription";

    // Método para ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("Prescription view init");


        // Bind del click del botón create con la función correspondiente
        $("#btnCreate").click(function () {
            var vc = new PrescriptionController();
            vc.Create();
        });

        $("#btnUpdate").click(function () {
            var vc = new PrescriptionController();
            vc.Update();
        });
        $("#btnDelete").click(function () {
            var vc = new PrescriptionController();
            vc.Delete();
        });

        this.loadTable();

    };

    this.Create = function () {
        try {
            // Crear un dto
            let prescription = {};

            prescription.patientId = $("#patientId").val();
            prescription.doctorId = $("#doctorId").val();
            prescription.prescriptionName = $("#prescriptionName").val();
            prescription.medicationName = $("#medicationName").val();
            prescription.prescriptionDate = $("#prescriptionDate").val();
            prescription.recommendations = $("#recommendations").val();

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

            prescription.UploadedFile = files[0].name;

            // Invocar al API
            var ca = new ControlActions();
            var serviceRoute = this.ApiService + "/Create";

            ca.PostToAPI(serviceRoute, prescription, function () {
                console.log("Prescription Created --->" + JSON.stringify(prescription));
                $('#tblPrescriptions').DataTable().ajax.reload();
            });
        } catch (error) {
            console.error("Error occurred while creating prescription:", error);
        }
    };



    this.Update = function () {

        let prescription = {};

        prescription.id = $("#txtId").val();
        prescription.patientId = $("#patientId").val();
        prescription.doctorId = $("#doctorId").val();
        prescription.prescriptionName = $("#prescriptionName").val();
        prescription.medicationName = $("#medicationName").val();
        prescription.prescriptionDate = $("#prescriptionDate").val();
        prescription.recommendations = $("#recommendations").val();

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

        prescription.UploadedFile = files[0].name;

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, prescription, function () {
            console.log("Prescription Updated --->" + JSON.stringify(prescription));
            $('#tblPrescriptions').DataTable().ajax.reload();
        })
    }

    this.Delete = function () {

        let prescription = {};

        prescription.id = $("#txtId").val();
        prescription.patientId = 0;
        prescription.doctorId = 0;
        prescription.prescriptionName = "default";
        prescription.medicationName = "default";
        prescription.prescriptionDate = "2024-04-26";
        prescription.recommendations = "default";

        prescription.UploadedFile = "default";

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, prescription, function () {
            console.log("Prescription Deleted --->" + JSON.stringify(prescription));
            $('#tblPrescriptions').DataTable().ajax.reload();
        })
    }

    this.loadTable = function () {
        var ca = new ControlActions();

        // Ruta del api a consumir servicio

        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll")

        // Definir las columnas a extraer del json de  respuesta
        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'patientId' }
        columns[2] = { 'data': 'doctorId' }
        columns[3] = { 'data': 'prescriptionName' }
        columns[4] = { 'data': 'medicationName' }
        columns[5] = { 'data': 'prescriptionDate' }
        columns[6] = { 'data': 'recommendations' }
        columns[7] = { 'data': 'uploadedFile' }

        // Inicializar la tabla como un data table
        $("#tblPrescriptions").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblPrescriptions tbody').on('click', 'tr', function () {

            // extraer fila a la que le dio click
            var row = $(this).closest('tr')

            // extraer la data del registro contenido en la fila

            var prescription = $('#tblPrescriptions').DataTable().row(row).data();

            // mapeo de los valores del objeto data con el  formulario
            $("#txtId").val(prescription.id);
            $("#patientId").val(prescription.patientId);
            $("#doctorId").val(prescription.doctorId);
            $("#prescriptionName").val(prescription.prescriptionName);
            $("#medicationName").val(prescription.medicationName);
            $("#prescriptionDate").val(prescription.prescriptionDate);
            $("#recommendations").val(prescription.recommendations);
            $("#profileImage").val(prescription.UploadedFile);
        });
    }
}

function UserInfoController() {
    this.ViewName = "Patient";
    this.ApiService = "Patient";

    this.InitViewUser = function () {
        console.log("Patient Info");


        this.LoadTableUserInfo();
    }



    this.LoadTableUserInfo = function () {
        var us = new ControlActions();

        //Ruta del api
        var urlService = us.GetUrlApiService(this.ApiService + "/RetrieveAll")

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
            var patient = $('#tblUserInfo').DataTable().row(row).data();

            var userIdWithName = patient.id

            // Establecer el valor del campo TxtUserId con la cadena creada
            $("#patientId").val(userIdWithName);



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

            var doctorIdWithName = doctor.id

            // Establecer el valor del campo TxtUserId con la cadena creada
            $("#doctorId").val(doctorIdWithName);



        });
    }
}

function formatDate(dateString) {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var day = ('0' + date.getDate()).slice(-2);
    return year + '-' + month + '-' + day;
}

$(document).ready(function () {
    var vc = new PrescriptionController();
    vc.InitView();

    var us = new UserInfoController();
    us.InitViewUser();

    var dc = new DoctorInfoController();
    dc.InitViewDoctor();
});