function PatientInfoController() {
    this.ViewName = "Patient";
    this.ApiService = "Patient";

    this.InitViewPatient = function () {
        console.log("Patient Info");
        this.LoadTablePatientInfo();
    };

    this.LoadTablePatientInfo = function () {
        var us = new ControlActions();

        //Ruta del api
        var urlService = us.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "name" };
        columns[2] = { 'data': "lastName" };
        columns[3] = { 'data': "phoneNumber" };
        columns[4] = { 'data': "email" };
        columns[5] = { 'data': "sex" };
        columns[6] = { 'data': "province" };
        columns[7] = { 'data': "address" };

        // Crear la tabla utilizando DataTables
        $("#tblPatientInfo").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblPatientInfo tbody').on('click', 'tr', function () {
            // Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            // Extraer la data del registro contenido en la fila
            var patient = $('#tblPatientInfo').DataTable().row(row).data();

            // Asignar valores a los campos de entrada correspondientes
            $("#patientId").val(patient.id);
            $("#patientName").val(patient.name);
            $("#patientLastName").val(patient.lastName);
        });

    };
}

function MedicalReportInfoController() {
    this.ViewName = "MedicalReport"; //como se llama la pagina
    this.ApiService = "MedicalReport";

    this.InitViewMedicalReport = function () {
        console.log("Medical Report view init");

        $("#btnCreate").click(function () {
            var vc = new MedicalReportInfoController();
            console.log("Console log Vc", vc);
            vc.Create();
        });

        $("#btnUpdate").click(function () {
            var vc = new MedicalReportInfoController();
            vc.Update();
        });

        $("#btnDelete").click(function () {
            var vc = new MedicalReportInfoController();
            vc.Delete();
        });

        this.LoadTableMedicalReportInfo();
    };

    this.LoadTableMedicalReportInfo = function () { //Metodo para la carga de la tabla de datos
        var bi = new ControlActions();

        //Ruta del api
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveAll");

        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "patientId" };
        columns[2] = { 'data': "patientName" };
        columns[3] = { 'data': "patientLastName" };
        columns[4] = { 'data': "historial" };
        columns[5] = { 'data': "medicalNotes" };
        columns[6] = { 'data': "date" };

        $("#tblMedicalReport").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblMedicalReport tbody').on('click', 'tr', function () {
            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            // Extraer la data del registro contenido en la fila
            var medicalReport = $('#tblMedicalReport').DataTable().row(row).data();

            // Mapeo de los valores del objeto data con el formulario
            $("#TxtmedicalReportId").val(medicalReport.id);
            $("#patientId").val(medicalReport.patientId);
            $("#patientName").val(medicalReport.patientName);
            $("#patientLastName").val(medicalReport.patientLastName);
            $("#txtMedicalHistory").val(medicalReport.historial);
            $("#txtMedicalNotes").val(medicalReport.medicalNotes);
            $("#txtDate").val(medicalReport.date);
        });
    };

    this.Create = function () {
        var patientId = $("#patientId").val();
        var patientName = $("#patientName").val();
        var patientLastName = $("#patientLastName").val();
        var historial = $("#txtMedicalHistory").val();
        var medicalNotes = $("#txtMedicalNotes").val();
        var date = $("#txtDate").val();

        // Crear un Dto de Medical Report
        var MedicalReport = {
            patientId: patientId,
            patientName: patientName,
            patientLastName: patientLastName,
            historial: historial,
            medicalNotes: medicalNotes,
            date: date
        };
        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, MedicalReport, function () {
            console.log("Medical Report Created --->" + JSON.stringify(MedicalReport));
            $('#tblMedicalReport').DataTable().ajax.reload();
        });
    };

    this.Update = function () {

        // crear un dto
        var medicalReport = {};
        medicalReport.id = $("#TxtmedicalReportId").val();
        medicalReport.patientId = $("#patientId").val();
        medicalReport.patientName = $("#patientName").val();
        medicalReport.patientLastName = $("#patientLastName").val();
        medicalReport.historial = $("#txtMedicalHistory").val();
        medicalReport.medicalNotes = $("#txtMedicalNotes").val();
        medicalReport.date = $("#txtDate").val(); // Format date here

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, medicalReport, function () {
            console.log("Medical Report Updated --->" + JSON.stringify(medicalReport));
            $('#tblMedicalReport').DataTable().ajax.reload();
        })
    }

    this.Delete = function () {

        let medicalReport = {};

        medicalReport.id = $("#TxtmedicalReportId").val();
        medicalReport.patientId = 0;
        medicalReport.patientName = "default";
        medicalReport.patientLastName = "default";
        medicalReport.historial = "default";
        medicalReport.medicalNotes = "default";
        medicalReport.date = "2024-04-26";

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, medicalReport, function () {
            console.log("Medical Report Deleted --->" + JSON.stringify(medicalReport));
            $('#tblMedicalReport').DataTable().ajax.reload();
        })
    }
}

function DiagnosticInfoController() {
    this.ViewName = "Diagnostic"; //como se llama la pagina
    this.ApiService = "Diagnostic";

    this.InitViewDiagnostic = function () {
        console.log("Diagnostic view init");

        $("#btnCreateDiagnostic").click(function () {
            var vc = new DiagnosticInfoController();
            console.log("Console log Vc", vc);
            vc.Create();
        });

        $("#btnUpdateDiagnostic").click(function () {
            var vc = new DiagnosticInfoController();
            vc.Update();
        });

        $("#btnDeleteDiagnostic").click(function () {
            var vc = new DiagnosticInfoController();
            vc.Delete();
        });

        this.LoadTableDiagnosticInfo();
    };

    this.LoadTableDiagnosticInfo = function () { //Metodo para la carga de la tabla de datos
        var dg = new ControlActions();

        //Ruta del api
        var urlService = dg.GetUrlApiService(this.ApiService + "/RetrieveAll");

        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "patientId" };
        columns[2] = { 'data': "diagnoseName" };
        columns[3] = { 'data': "date" };
        columns[4] = { 'data': "description" };

        $("#tblDiagnostic").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblDiagnostic tbody').on('click', 'tr', function () {
            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            // Extraer la data del registro contenido en la fila
            var diagnostic = $('#tblDiagnostic').DataTable().row(row).data();

            // Mapeo de los valores del objeto data con el formulario
            $("#TxtdiagnosticId").val(diagnostic.id);
            $("#TxtPatientId").val(diagnostic.patientId);
            $("#TxtDiagnoseName").val(diagnostic.diagnoseName);
            $("#TxtDate").val(diagnostic.date);
            $("#TxtDescription").val(diagnostic.description);
        });
    };

    this.Create = function () {
        var patientId = $("#TxtPatientId").val();
        var diagnoseName = $("#TxtDiagnoseName").val();
        var date = $("#TxtDate").val();
        var description = $("#TxtDescription").val();

        // Crear un Dto de Medical Report
        var Diagnostic = {
            patientId: patientId,
            diagnoseName: diagnoseName,
            date: date,
            description: description
        };
        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, Diagnostic, function () {
            console.log("Diagnostic Created --->" + JSON.stringify(Diagnostic));
            $('#tblDiagnostic').DataTable().ajax.reload();
        });
    };

    this.Update = function () {

        // crear un dto
        var diagnostic = {};
        diagnostic.id = $("#TxtdiagnosticId").val();
        diagnostic.patientId = $("#TxtPatientId").val();
        diagnostic.diagnoseName = $("#TxtDiagnoseName").val();
        diagnostic.date = $("#TxtDate").val(); // Format date here
        diagnostic.description = $("#TxtDescription").val();;
        

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, diagnostic, function () {
            console.log("Diagnostic Updated --->" + JSON.stringify(diagnostic));
            $('#tblDiagnostic').DataTable().ajax.reload();
        })
    }

    this.Delete = function () {

        let diagnostic = {};

        diagnostic.id = $("#TxtdiagnosticId").val();
        diagnostic.patientId = 0;
        diagnostic.diagnoseName = "default";
        diagnostic.date = "2024-04-26";
        diagnostic.description = "default";

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, diagnostic, function () {
            console.log("Diagnostic Deleted --->" + JSON.stringify(diagnostic));
            $('#tblDiagnostic').DataTable().ajax.reload();
        })
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
    var us = new PatientInfoController();
    us.InitViewPatient();

    var bi = new MedicalReportInfoController();
    bi.InitViewMedicalReport();

    var dg = new DiagnosticInfoController();
    dg.InitViewDiagnostic();
});
