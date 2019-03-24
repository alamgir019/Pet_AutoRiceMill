app.service("SrService", function ($http) {

    //Get All Retailer
    this.getAllRetailer = function () {
        return $http.get("/Retailer/GetAllRetailer");
    };

    // get Retailer By Route Id
    this.getRetailerByRouteId = function (routeId) {
        var response = $http({
            method: "get",
            url: "/Sr/GetRetailerByRoutId",
            data: routeId,
            dataType: "json",
            params: {
                id: routeId
            }
        });
        return response;
    }
  


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
    this.Add = function (Sr) {
        debugger
        var response=$http({
            method:"post",
            url: "/Sr/Add",
            data:JSON.stringify(Sr),
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
    this.getBuyer = function () {
        return $http.get("/Buyer/GetAllBuyer");
    };
    this.getRoute = function () {
        return $http.get("/Route/GetAllRoute");
    };

    
});