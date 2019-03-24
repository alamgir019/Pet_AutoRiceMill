app.service("LocationService", function ($http) {
    //Get All Location
    this.getAllLocation = function () {
        var response = $http({
            method: "GET",
            url: "/Location/GetAllLocation",
            data: JSON.stringify(Location),
            dateType: "json"
        });
        return response;

       // return $http.get("/Location/GetAllLocation");
    };

    this.getLocationById = function (locationID) {
        var response = $http({
            method: "post",
            url: "/Location/GetLocationById",
            params: {
                id: empID
            }
        });
        return response;
    }

    //Add    
    this.Add = function (Location) {
        
        var response=$http({
            method:"post",
            url: "/Location/Add",
            data:JSON.stringify(Location),
            dateType: "json"
        });
        return response;
    }

    //Save (Update)  
    this.Update = function (Location) {
        var response = $http({
            method: "post",
            url: "/Location/Update",
            data: JSON.stringify(Location),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (locationID) {
        var response = $http({
            method: "post",
            url: "/Location/Delete",
            params: {
                id: locationID
            }
        });
        return response;
    }
    this.getDistrict = function () {
        return $http.get("/District/GetAllDistrict");
    };
});