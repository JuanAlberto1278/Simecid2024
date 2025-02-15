function UserInfoController() {
    this.ViewName = "User";
    this.ApiService = "User";

    this.InitViewUser = function () {
        console.log("User Info");

        $("#btnUpdate").click(function () {
            var us = new UserInfoController();
            us.Update();
        })

        $("#btnDelete").click(function () {
            var us = new UserInfoController();
            us.Delete();
        })
        

        $("#btnEmployee").click(function () {
            var us = new UserInfoController();
            us.UpdateSchedule();
        })

        this.LoadTableUserInfo();
        this.LoadTableUserRole();
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
        columns[5] = { 'data': "role" }
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

            var user = $('#tblUserInfo').DataTable().row(row).data();
            // Establecer el valor del campo TxtUserId con la cadena creada
            $("#id").val(user.id);
            $("#name").val(user.name);
            $("#lastName").val(user.lastName);
            $("#phoneNumber").val(user.phoneNumber);
            $("#email").val(user.email);
            $("#sex").val(user.role);
            $("#province").val(user.province);
            $("#address").val(user.address);

            


        });
     }
   

    this.Update = function () {
        var user = {};
        user.Id = $("#id").val();
        user.name = $("#name").val();
        user.lastName = $("#lastName").val();
        user.phoneNumber = $("#phoneNumber").val();
        user.email = $("#email").val();
        user.role = $("#sex").val();
        user.province = $("#province").val();
        user.address = $("#address").val();

        // Invocar la API para actualizar el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/UpdateUserData";

        ca.PutToAPI(serviceRoute, user, function () {
            console.log("User Updated --->" + JSON.stringify(user));
            $('#tblUserInfo').DataTable().ajax.reload();
            $("#id").val("");
            $("#name").val("");
            $("#lastName").val("");
            $("#phoneNumber").val("");
            $("#email").val("");
            $("#sex").val("");
            $("#province").val("");
            $("#address").val("");
        });
        $('#tblUserRole').DataTable().ajax.reload();

    }

    this.Delete = function () {
        var userId = $("#id").val();

        
        var user = {
            Id: userId,
            name: "Default",
            lastName: "Default",
            phoneNumber: 0,
            email: "Default",
            password: "Default",
            sex: "Default",
            birthDate: "2024-04-27T15:02:59.080Z",
            role: "Default",
            status:"Default",
            province: "Default",
            address: "Default",
            isValid: true,
            imageName: "Default"

        };

        
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";



        ca.DeleteToAPI(serviceRoute, user, function () {
            console.log("User Deleted --->" + userId);
            $('#tblUserInfo').DataTable().ajax.reload();
            $('#tblUserRole').DataTable().ajax.reload();
            $("#id").val("");
            $("#name").val("");
            $("#lastName").val("");
            $("#phoneNumber").val("");
            $("#email").val("");
            $("#sex").val("");
            $("#province").val("");
            $("#address").val("");

        });
        $('#tblUserInfo').DataTable().ajax.reload();
        $('#tblUserRole').DataTable().ajax.reload();
        $('#tblDoctorInfo').DataTable().ajax.reload();
    }
    this.LoadTableUserRole = function () {
        var us = new ControlActions();
        var self = this;
        $('#role').change(function () {
            var selectedRole = $('#role').val(); 
            self.GetUsersByRole(selectedRole);
            $("#employeeId").val("");
            $("#employeeName").val("");
            $("#employeeLastName").val("");
            $("#employeeEmail").val("");
            $("#branchSelect").val("");
            $("#schedule").val("");
        });

     
        var defaultRole = $('#role').val();
        this.GetUsersByRole(defaultRole);
        $('#tblUserRole tbody').on('click', 'tr', function () {
            var row = $(this).closest('tr');
            var userData = $('#tblUserRole').DataTable().row(row).data();
            $("#employeeId").val(userData.id);
            $("#employeeName").val(userData.name);
            $("#employeeLastName").val(userData.lastName);
            $("#employeeEmail").val(userData.email);
        });
    }


    this.GetUsersByRole = function (role) {
        var us = new ControlActions();
        var urlService = us.GetUrlApiService(this.ApiService + "/RetrieveUsersByRole?" + "role=" + role);


        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "lastName" }
        columns[3] = { 'data': "phoneNumber" }
        columns[4] = { 'data': "email"}
        columns[5] = { 'data': "role" }
        columns[6] = { 'data': "province" }
        columns[7] = { 'data': "address" }
        columns[8] = { 'data': "branchId" }
        columns[9] = { 'data': "schedule" }

        $("#tblUserRole").dataTable({
            "destroy": true,
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });
    }

    this.UpdateSchedule = function () {
        var user = {};
        user.Id = $("#employeeId").val();
        user.name = $("#employeeName").val();
        user.lastName = $("#employeeLastName").val();
        user.email = $("#employeeEmail").val();
        user.role = $("#role").val();
        user.branchId = $("#branchSelect").val();
        user.schedule = $("#schedule").val();


        // Invocar la API para actualizar el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/UpdateEmployeeData";

        ca.PutToAPI(serviceRoute, user, function () {
            console.log("User Updated --->" + JSON.stringify(user));
            $('#tblUserRole').DataTable().ajax.reload();
            $('#tblUserInfo').DataTable().ajax.reload();
            $('#tblDoctorInfo').DataTable().ajax.reload();
            $("#employeeId").val("");
            $("#employeeName").val("");
            $("#employeeLastName").val("");
            $("#employeeEmail").val("");
            $("#role").val("");
            $("#branchSelect").val("");
            $("#schedule").val("");
        });
    }
}

function AllBranchInfoController() {
  
    this.ViewName = "Branch"; 
    this.ApiService = "Branch";

  
    this.InitViewAllBranch = function () {
        console.log("All Branch view init");

        
        $("#btnAdd").click(function () {
            var vc = new AllBranchInfoController();
            vc.Add();
        });

     
        
        this.LoadBranches();
    }

    
    this.LoadBranches = function () {
        var ba = new ControlActions();
        var urlService = this.ApiService + "/RetrieveAll";

       
        ba.GetToApi(urlService, function (response) {
            response.forEach(function (branch) {
                $("#branchSelect").append('<option value="' + branch.id + '">' + branch.id + ' - ' + branch.name + '</option>');
            });
        });
    }
}

function DoctorInfoController() {
    this.ViewName = "Doctor";
    this.ApiService = "Doctor";

    this.InitViewDoctor = function () {
        console.log("Doctor Info");

        $("#btnSpecialty").click(function () {
            var dc = new DoctorInfoController();
            dc.UpdateSpecialty();
        })

        this.LoadTableDoctorInfo();
        
    }



    this.LoadTableDoctorInfo = function () {
        var dc = new ControlActions();

        //Ruta del api
        var urlService = dc.GetUrlApiService(this.ApiService + "/RetrieveDoctorSpecialty")

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "lastName" }
        columns[3] = { 'data': "email" }
        columns[4] = { 'data': "specialty" }

        // Crear la tabla utilizando DataTables
        $("#tblDoctorInfo").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblDoctorInfo tbody').on('click', 'tr', function () {

            
            var row = $(this).closest('tr');

            var user = $('#tblDoctorInfo').DataTable().row(row).data();
            
            $("#doctorId").val(user.id);
            $("#doctorname").val(user.name);
            $("#doctorLastName").val(user.lastName);
            $("#doctorEmail").val(user.email);
            $("#doctorSpecialty").val(user.specialty);


        });
    }


    this.UpdateSpecialty = function () {
        var user = {};
        user.Id = $("#doctorId").val();
        user.name = $("#doctorname").val();
        user.lastName = $("#doctorLastName").val();
        user.email = $("#doctorEmail").val();
        user.specialty = $("#doctorSpecialty").val();


        // Invocar la API para actualizar el servicio
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/UpdateDoctorSpecialty";

        ca.PutToAPI(serviceRoute, user, function () {
            console.log("Doctor Updated --->" + JSON.stringify(user));
            $('#tblDoctorInfo').DataTable().ajax.reload();
            $('#tblUserInfo').DataTable().ajax.reload();
            $('#tblUserRole').DataTable().ajax.reload();
            $("#doctorId").val(""); 
            $("#doctorname").val("");
            $("#doctorLastName").val("");
            $("#doctorEmail").val("");
            $("#doctorSpecialty").val("");
        });
    }

    
    
}




$(document).ready(function () {
    var us = new UserInfoController();
    us.InitViewUser();

    var br = new AllBranchInfoController();
    br.InitViewAllBranch();

    var dc = new DoctorInfoController();
    dc.InitViewDoctor();

})
