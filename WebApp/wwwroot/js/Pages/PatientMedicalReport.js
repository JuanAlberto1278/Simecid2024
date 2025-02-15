function MedicalReportInfoController() {
    this.ViewName = "MedicalReport"; //como se llama la pagina
    this.ApiService = "MedicalReport";

    this.InitViewMedicalReport = function () {
        console.log("Medical Report view init");
        this.LoadTableMedicalReportInfo();
    };

    this.LoadTableMedicalReportInfo = function () { //Metodo para la carga de la tabla de datos
        var bi = new ControlActions();

        // Obtener el correo electrónico de la URL
        const urlParams = new URLSearchParams(window.location.search);
        const userEmail = urlParams.get("email");

        // Ruta del API para obtener las citas por email
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveByEmail?userEmail=" + userEmail);

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

    };
}

function DiagnosticInfoController() {
    this.ViewName = "Diagnostic"; //como se llama la pagina
    this.ApiService = "Diagnostic";

    this.InitViewDiagnostic = function () {
        console.log("Diagnostic view init");
        this.LoadTableDiagnosticInfo();
    };

    this.LoadTableDiagnosticInfo = function () { //Metodo para la carga de la tabla de datos
        var dg = new ControlActions();

        // Obtener el correo electrónico de la URL
        const urlParams = new URLSearchParams(window.location.search);
        const userEmail = urlParams.get("email");

        // Ruta del API para obtener las citas por email
        var urlService = dg.GetUrlApiService(this.ApiService + "/RetrieveByEmail?userEmail=" + userEmail);

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
    };
}

function MedicalExamController() {
    this.ViewName = "MedicalExam"; // como se llama la página
    this.ApiService = "MedicalExam";

    this.InitViewAllMedicalExamn = function () {
        console.log("All Medical Exam view init");
        this.LoadTableMedicalExam();
    }

    this.LoadTableMedicalExam = function () {
        var bi = new ControlActions();

        // Obtener el correo electrónico de la URL
        const urlParams = new URLSearchParams(window.location.search);
        const userEmail = urlParams.get("email");

        // Ruta del API para obtener las citas por email
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveByEmail?userEmail=" + userEmail);

        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "patientId" };
        columns[2] = { 'data': "examtype" };
        columns[3] = { 'data': "examDate" };
        columns[4] = { 'data': "details" };
        columns[5] = { 'data': "weight" };
        columns[6] = { 'data': "size" };
        columns[7] = { 'data': "bodyMass" };
        columns[8] = { 'data': "result" };

        $("#tblMedicalExam").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });
    };
}

function PrescriptionController() {
    this.ViewName = "Prescription";
    this.ApiService = "Prescription";

    // Método para ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("Prescription view init");
        this.loadTable();

    }

    this.loadTable = function () {
        var ca = new ControlActions();

        // Obtener el correo electrónico de la URL
        const urlParams = new URLSearchParams(window.location.search);
        const userEmail = urlParams.get("email");

        // Ruta del API para obtener las citas por email
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveByEmail?userEmail=" + userEmail);

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
    };
}


$(document).ready(function () {

    var bi = new MedicalReportInfoController();
    bi.InitViewMedicalReport();

    var dg = new DiagnosticInfoController();
    dg.InitViewDiagnostic();

    var ba = new MedicalExamController();
    ba.InitViewAllMedicalExamn();

    var vc = new PrescriptionController();
    vc.InitView();
});