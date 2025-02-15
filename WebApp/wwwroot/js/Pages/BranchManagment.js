//Clase Javascript que es el controlador de la vista
//Nos referimos a controlador partiendo del supuesto que esta clase controla el compartamiento de la
//vista o pagina

/*Definmos la clase */
function AllBranchInfoController() {
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";


    this.InitViewAllBranch = function () {
        console.log("All Branch view init");
       


        $("#btnCreate").click(function () {
            var vc = new AllBranchInfoController();
            console.log("Console log Vc", vc);
            vc.Create();

        })

        $("#btnUpdate").click(function () {
            var vc = new AllBranchInfoController();
            vc.Update();

        })

        $("#btnDelete").click(function () {
            var vc = new AllBranchInfoController();
            vc.Delete();

        })

        this.LoadTableAllInfoBranch();
    }

    this.LoadTableAllInfoBranch = function () { //Metodo para la carga de la tabla de datos
        var bi = new ControlActions();

        //Ruta del api
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveAll")

        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "address" }
        columns[3] = { 'data': "description" }

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
            $("#TxtAddress").val(branch.address);
            $("#TxtDescription").val(branch.description);
             

        });
    }

    

    this.Create = function () {

        var branchName = $("#TxtName").val();
        var branchAddress = $("#TxtAddress").val();
        var branchDescription = $("#TxtDescription").val();
        //Crear un Dto de User
        var Branch = {
            name: branchName,
            address: branchAddress,
            description: branchDescription,
            serviceId: 0,
            serviceName: "Default",
            servicePrice: 0,
            serviceTax: 0
        };
        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, Branch, function () {
            console.log("Branch Created --->" + JSON.stringify(Branch));
            $('#tblAllBranches').DataTable().ajax.reload();
        });
    }

    this.Update = function () {
        var branchId = $("#BranchId").val();
        var branchName = $("#TxtName").val();
        var branchAddress = $("#TxtAddress").val();
        var branchDescription = $("#TxtDescription").val();

        var Branch = {
            Id: branchId,
            name: branchName,
            address: branchAddress,
            description: branchDescription,
            serviceId: 0,
            serviceName: "Default",
            servicePrice: 0,
            serviceTax: 0
        };

        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, Branch, function () {

            console.log("Branch Updated --->" + JSON.stringify(Branch));
            $('#tblAllBranches').DataTable().ajax.reload();

        });
    }

    this.Delete = function () {
        var branchId = $("#BranchId").val();

        // Construir un objeto user con solo el ID del usuario
        var Branch = {

            Id: branchId,
            name: "Default",
            address: "Default",
            description: "Default",
            serviceId: 0,
            serviceName: "Default",
            servicePrice: 0,
            serviceTax: 0
        };

        // Invocar la API DELETE con el objeto user
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, Branch, function (response) {

            if (response.statusCode == 200) {
                console.log("Branch Deleted --->" + branchId);
                $('#tblAllBranches').DataTable().ajax.reload(); //Esto para que si se elimina un service se actualice el branch que lo tenia y se elimine
 

            } else if (response.statusCode == 500) {
                console.log("El branch con el Id " + branchId + " no existe.");

            } else {
                console.log("Error al eliminar el branch. CÃ³digo de estado: " + response.statusCode);

            }
            $('#tblAllBranches').DataTable().ajax.reload();


        });
    }

}

//Instanciamineto de la clase 
$(document).ready(function () {
    var ba = new AllBranchInfoController();
    ba.InitViewAllBranch();
})


