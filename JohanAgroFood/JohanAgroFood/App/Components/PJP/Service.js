app.service("PJPService", function ($http) {

    //Get All Retailer
    this.getAllPJP = function () {
        return $http.get("/PJP/GetAllPJP");
    };

    // get Retailer By Route Id
    this.getRetailerByRouteId = function (routeId) {
        var response = $http({
            method: "get",
            url: "/PJP/GetRetailerByRoutId",
            data: routeId,
            dataType: "json",
            params: {
                id: routeId
            }
        });
        return response;
    }
  


    this.getPJPById = function (pjpID) {
        var response = $http({
            method: "post",
            url: "/PJP/GetPJPById",
            params: {
                id: pjpID
            }
        });
        return response;
    }

    //Add    
    this.Add = function (PJP) {
        var response=$http({
            method:"post",
            //url: "/PJP/Add",
            url: "/PJP/Insert",
            data:JSON.stringify(PJP),
            dateType: "json"
        });
        return response;
    }

    //Save (Update)  
    this.Update = function (Pjp) {
        var response = $http({
            method: "post",
            url: "/PJP/Update",
            data: JSON.stringify(Pjp),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (pjpID) {
        var response = $http({
            method: "post",
            url: "/PJP/Delete",
            params: {
                id: pjpID
            }
        });
        return response;
    }
    //CE-150408-ANIS
    this.getSrByBuyerId = function (srId) {
        var response = $http({
            method: "get",
            url: "/Sr/GetSrByBuyerId",
            data: srId,
            dataType: "json",
            params: {
                id: srId
            }
        });
        return response;
    }
    //CE-End-150408-ANIS

    //CE-150409-ANIS
    this.getRetailerInformationBySrId = function (ID) {
        var response = $http({
            method: "get",
            url: "/Retailer/GetRetailerInformationSrWise/",
            data: JSON.stringify(ID),
            dataType: "json",
            params: {
                id: ID
            }
        });
        return response;
        //return $http.get("/Retailer/GetRetailerInformationSrWise/41");
    }
    //CE-End-150409-ANIS
    this.getBuyer = function () {
        return $http.get("/Buyer/GetAllBuyer");
    };
    this.getRoute = function () {
        return $http.get("/Route/GetAllRoute");
    };
    
});