app.service("angularServiceLogin", function ($http) {

    this.getEmp = function () {
        return $http.get("/Employee/GetAllEmployees");
    };

    this.getEmpById = function (empID) {
        var response = $http({
            method: "post",
            url: "/Employee/GetEmployeeById",
            params: {
                id: empID
            }
        });
        return response;
    }

    //Save (Update)  
    this.update = function (employee) {
        var response = $http({
            method: "post",
            url: "/Employee/Update",
            data: JSON.stringify(employee),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (empID) {
        var response = $http({
            method: "post",
            url: "/Employee/Delete",
            params: {
                id: empID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (employee) {
        var response = $http({
            method: "post",
            url: "/Employee/Add",
            data: JSON.stringify(employee),
            dataType: "json"

        });
        return response;
    }

});