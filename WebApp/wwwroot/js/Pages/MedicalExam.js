// Clase Javascript que es el controlador de la vista
// Nos referimos a controlador partiendo del supuesto que esta clase controla el comportamiento de la
// vista o página

/* Definimos la clase */

/* AQUI VA LO DE PACIENTES */
function PatientInfoController() {
    this.ViewName = "Patient";
    this.ApiService = "Patient";

    this.InitViewPatient = function () {
        console.log("Patient Info");
        this.LoadTablePatientInfo();
    };

    this.LoadTablePatientInfo = function (callback) {
        var us = new ControlActions();

        // Ruta del api
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

        // Llamar a la función de devolución de llamada una vez que los datos se hayan cargado completamente
        if (typeof callback === "function") {
            callback();
        }
    };
}

/* AQUI VA LO DE EXAMEN MEDICO */
function MedicalExamController() {
    this.ViewName = "MedicalExam"; // como se llama la página
    this.ApiService = "MedicalExam";

    this.InitViewAllMedicalExamn = function () {
        console.log("All Medical Exam view init");
        $("#btnCreate").click(function () {
            var vc = new MedicalExamController();
            console.log("Console log Vc", vc);
            vc.Create();
        })
        $("#btnUpdate").click(function () {
            var vc = new MedicalExamController();
            console.log("C0nsole log vc",vc)
            vc.Update();
        })
        $("#btnDelete").click(function () {
            var vc = new MedicalExamController();
            vc.Delete();
        })
        this.LoadTableMedicalExam();
    }

    this.LoadTableMedicalExam = function () {
        var bi = new ControlActions();

        // Ruta del API
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveAll");

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

        $('#tblMedicalExam tbody').on('click', 'tr', function () {
            // Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            // Extraer la data del registro contenido en la fila
            var medicalExam = $('#tblMedicalExam').DataTable().row(row).data();

            // Mapeo de los valores del objeto data con el formulario
            $("#TxtmedicalExamId").val(medicalExam.id);
            $("#patientId").val(medicalExam.patientId);
            $("#TxtexamType").val(medicalExam.examtype);
            $("#TxtexamDate").val(medicalExam.examDate);
            $("#Txtdetails").val(medicalExam.details);
            $("#Txtweight").val(medicalExam.weight);
            $("#Txtsize").val(medicalExam.size);
            $("#TxtbodyMass").val(medicalExam.bodyMass);
            $("#Txtresult").val(medicalExam.result);
        });
    };

    this.Create = function () {

        var patientId = $("#patientId").val();
        var medicalExamType = $("#TxtexamType").val();
        var examDate = $("#TxtexamDate").val();
        var medicalExamdetails = $("#Txtdetails").val();
        var medicalExamweight = $("#Txtweight").val();
        var medicalExamsize = $("#Txtsize").val();
        var medicalExambodyMass = $("#TxtbodyMass").val();
        var medicalExamresult = $("#Txtresult").val();

        // Crear un DTO de MedicalExam
        var medicalExam = {
            patientId: patientId,
            examtype: medicalExamType,
            examDate: examDate,
            details: medicalExamdetails,
            weight: medicalExamweight,
            size: medicalExamsize,
            bodyMass: medicalExambodyMass,
            result: medicalExamresult
        };

        // Invocar el API
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, medicalExam, function () {
            console.log("Medical Exam Created --->" + JSON.stringify(medicalExam));
            $('#tblMedicalExam').DataTable().ajax.reload();
        });
    };

    this.Update = function () {
        var medicalExam = {};

        medicalExam.id = $("#TxtmedicalExamId").val();
        medicalExam.patientId = $("#patientId").val();
        medicalExam.examtype = $("#TxtexamType").val();

        // Obtener la fecha y hora del campo datetime-local
        var examDateTime = $("#TxtexamDate").val();

        // Si necesitas manipular la fecha y hora antes de enviarla al servidor, puedes hacerlo aquí

        medicalExam.examDate = examDateTime; // La fecha y hora se envían tal como están

        medicalExam.weight = $("#Txtweight").val();
        medicalExam.size = $("#Txtsize").val();
        medicalExam.bodyMass = $("#TxtbodyMass").val();
        medicalExam.details = $("#Txtdetails").val();
        medicalExam.result = $("#Txtresult").val();

        // Invocar API
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, medicalExam, function () {
            console.log("Medical Exam Updated --->" + JSON.stringify(medicalExam));
            $('#tblMedicalExam').DataTable().ajax.reload();
        });
    };

    this.Delete = function () {

        let medicalExam = {};

        medicalExam.id = $("#TxtmedicalExamId").val();
        medicalExam.patientId = 0;
        medicalExam.examtype = "default";
        medicalExam.examDate = "2024-04-26";
        medicalExam.details = "default";
        medicalExam.weight = 0;
        medicalExam.size = 0;
        medicalExam.bodyMass = "default";
        medicalExam.result = "default";

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, medicalExam, function () {
            console.log("Medical Exam Deleted --->" + JSON.stringify(medicalExam));
            $('#tblMedicalExam').DataTable().ajax.reload();
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

// Instanciamineto de la clase 
$(document).ready(function () {
    var us = new PatientInfoController();
    us.InitViewPatient();

    var ba = new MedicalExamController();
    ba.InitViewAllMedicalExamn();
});
