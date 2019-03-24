app.service("RouteService", function ($http) {
    //Get All Location
    this.getAllRoute = function () {
        //return $http.get(baseurl + "/Route/GetAllRoute");
        return $http.get("/Route/GetAllRoute");
    };

    this.getRouteById = function (routeID) {
        var response = $http({
            method: "post",
            //url: baseurl + "/Route/GetRouteById",
            url: "/Route/GetRouteById",
            params: {
                id: routeID
            }
        });
        return response;
    }

    //Add    
    this.Add = function (Route) {        
        var response=$http({
            method:"post",
            url: "/Route/Add",
            //url: baseurl + "/Route/Add",
            data:JSON.stringify(Route),
            dateType: "json"
        });
        return response;
    }

    //Save (Update)  
    this.Update = function (Route) {
        var response = $http({
            method: "post",
            //url: baseurl + "/Route/Update",
            url: "/Route/Update",
            data: JSON.stringify(Route),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (routeID) {
        var response = $http({
            method: "post",
            //url: baseurl + "/Route/Delete",
            url: "/Route/Delete",
            params: {
                id: routeID
            }
        });
        return response;
    }
    //CE-150325-ANIS
    this.getDistrict = function () {
        //return $http.get(baseurl + "/District/GetAllDistrict");
        return $http.get("/District/GetAllDistrict");
    };
    //CE-End-150325-ANIS
});