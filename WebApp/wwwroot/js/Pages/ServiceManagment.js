//Clase Javascript que es el controlador de la vista
//Nos referimos a controlador partiendo del supuesto que esta clase controla el comportamiento de la
//vista o pagina

/*CONTROLLER PARA AGREGAR SERVICE A BRANCH */
function AllBranchInfoController() {
    // Propiedades de la clase
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";

    // Método para inicializar la vista de todas las sucursales
    this.InitViewAllBranch = function () {
        console.log("All Branch view init");

        // Asignar evento click al botón de agregar
        $("#btnAdd").click(function () {
            var vc = new AllBranchInfoController();
            vc.Add();
        });

        // Cargar la tabla de información de todas las sucursales
        this.LoadTableAllInfoBranch();

        // Cargar las sucursales
        this.LoadBranches();
    }

    // Método para cargar las sucursales en un dropdown
    this.LoadBranches = function () {
        var ba = new ControlActions();
        var urlService = this.ApiService + "/RetrieveAll";

        // Obtener las sucursales desde la API y agregarlas al dropdown
        ba.GetToApi(urlService, function (response) {
            response.forEach(function (branch) {
                $("#branchSelect").append('<option value="' + branch.id + '">' + branch.id + ' - ' + branch.name + '</option>');
            });
        });
    }

    // Método para agregar un servicio a una sucursal
    this.Add = function () {
        //Crear un Dto
        var branchId = $("#branchSelect").val();
        var serviceId = $("#serviceSelect").val();
        var Branchservice = {
            id: branchId,
            name: "Default",
            address: "Default",
            description: "Default",
            serviceId: serviceId,
            serviceName: "Default"
        };

        //Invocar Api para agregar el servicio a la sucursal
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/AddServiceToBranch";

        ca.PostToAPI(serviceRoute, Branchservice, function () {
            console.log("Service Added to branch --->" + JSON.stringify(Branchservice));
            $('#tblBranch').DataTable().ajax.reload();
        });
    }

    // Método para crear un nuevo servicio
    this.Create = function () {
        // Crear un objeto con los datos del nuevo servicio
        var service = {};
        service.name = $("#textName").val();
        service.description = $("#textDescription").val();
        service.price = $("#textPrice").val();
        service.tax = $("#textTax").val();

        // Invocar la API para crear el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, service, function () {
            let loadsrv = new ServiceController();
            console.log("Service Created --->" + JSON.stringify(service));
            $('#tblServices').DataTable().ajax.reload();
            loadsrv.LoadAllServices();
        });
    }

    // Método para cargar la tabla con la información de todas las sucursales
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
    }
}

//CONTROLLER DE BRANCH 
function BranchController() {
    // Propiedades de la clase
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";

    // Método para inicializar la vista de la sucursal
    this.InitViewBranch = function () {
        console.log("Branch view init");
        this.LoadTableBranch();
    }

    // Método para cargar la tabla con la información de la sucursal y sus servicios
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

        console.log(urlService);
    }
}

//CONTROLLER DE SERVICIOS 
function ServiceController() {
    // Propiedades de la clase
    this.ViewName = "Service"; //como se llama la pagina
    this.ApiService = "Service"  //Servicio de Api

    // Método a ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("Services view init");

        // Asignar eventos a los botones de crear, actualizar y eliminar servicios
        $("#btnCreate").click(function () {
            var vc = new ServiceController();
            vc.Create();
        })

        $("#btnUpdate").click(function () {
            var vc = new ServiceController();
            vc.Update();
        })

        $("#btnDelete").click(function () {
            var vc = new ServiceController();
            vc.Delete();
        })

        // Cargar la tabla con la información de los servicios
        this.LoadTableService();

        // Cargar todos los servicios en un dropdown
        this.LoadAllServices();
    }

    // Método para cargar todos los servicios en un dropdown
    this.LoadAllServices = function (callback) {
        var ca = new ControlActions();
        var urlService = this.ApiService + "/RetrieveAll";

        // Limpiar el dropdown antes de cargar los servicios nuevamente
        $("#serviceSelect").empty();

        // Obtener los servicios desde la API y agregarlos al dropdown
        ca.GetToApi(urlService, function (response) {
            response.forEach(function (service) {
                $("#serviceSelect").append('<option value="' + service.id + '">' + service.id + ' - ' + service.name + '</option>');
            });

            // Ejecutar una función de callback si se proporciona
            if (typeof callback === 'function') {
                callback();
            }
        });
    }


    // Método para crear un nuevo servicio
    this.Create = function () {
        // Crear un objeto con los datos del nuevo servicio
        var service = {};
        service.name = $("#textName").val();
        service.description = $("#textDescription").val();
        service.price = $("#textPrice").val();
        service.tax = $("#textTax").val();

        // Invocar la API para crear el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, service, function () {
            let loadsrv = new ServiceController();
            console.log("Service Created --->" + JSON.stringify(service));
            $('#tblServices').DataTable().ajax.reload();
            loadsrv.LoadAllServices();
        });
    }

    // Método para actualizar un servicio existente
    this.Update = function () {
        var service = {};
        service.Id = $("#ServiceId").val();
        service.name = $("#textName").val();
        service.description = $("#textDescription").val();
        service.price = $("#textPrice").val();
        service.tax = $("#textTax").val();

        // Invocar la API para actualizar el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, service, function () {
            console.log("Service Updated --->" + JSON.stringify(service));
            $('#tblServices').DataTable().ajax.reload();
        });
    }

    // Método para eliminar un servicio
    this.Delete = function () {
        var serviceId = $("#ServiceId").val();

        // Construir un objeto con el ID del servicio a eliminar
        var service = {
            Id: serviceId,
            name: "Default",
            description: "Default",
            price: 0,
            tax: 0
        };

        // Invocar la API para eliminar el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";



        ca.DeleteToAPI(serviceRoute, service, function () {
            console.log("Service Deleted --->" + serviceId);
            $('#tblServices').DataTable().ajax.reload();

            // Eliminar el servicio del dropdown después de eliminarlo
            $("#serviceSelect option[value='" + serviceId + "']").remove();
            // Cargar nuevamente la lista de servicios en el dropdown
            let loadsrv = new ServiceController();
            loadsrv.LoadAllServices();
        });
    }



    // Método para cargar la tabla con la información de todos los servicios
    this.LoadTableService = function () {
        var ca = new ControlActions();
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll")

        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "description" }
        columns[3] = { 'data': "price" }
        columns[4] = { 'data': "tax" }

        $("#tblServices").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        // Asignar evento al hacer clic en una fila de la tabla
        $('#tblServices tbody').on('click', 'tr', function () {
            // Extraer la fila en la que se hizo clic
            var row = $(this).closest('tr');
            // Extraer los datos del servicio de la fila
            var service = $('#tblServices').DataTable().row(row).data();
            // Mapear los valores del objeto de datos con los campos del formulario
            $("#ServiceId").val(service.id);
            $("#textName").val(service.name);
            $("#textDescription").val(service.description);
            $("#textPrice").val(service.price);
            $("#textTax").val(service.tax);
        });
    }
}

//Instanciamiento de la clase 
$(document).ready(function () {
    // Inicializar los controladores de las vistas
    var vc = new ServiceController();
    vc.InitView();
    var bc = new BranchController();
    bc.InitViewBranch();
    var ba = new AllBranchInfoController();
    ba.InitViewAllBranch();
})
