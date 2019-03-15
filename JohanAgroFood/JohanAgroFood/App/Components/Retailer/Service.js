app.service("retailerService", function ($http) {

    this.getRetailer = function () {      
        return $http.get("/Retailer/GetAllRetailer");
    };
 
    this.getThanaByDistrictId = function (districtId) {
        //return $http.get("/Retailer/GetThanaByDistrictId?id="+2);
        //var response= $http({
        //    url: "/Retailer/GetThanaByDistrictId",
        //    method: "GET",
        //    params: { id: districtId }
        //});

        var response = $http({
            method: "get",
            url: "/Retailer/GetThanaByDistrictId",
            data: districtId,
            dataType: "json",
            params: {
                id: districtId
            }
        });
        return response;
    }
    //CE-150325-ANIS
    this.getLocationByDistrictId = function (districtId) {
        var response = $http({
            method: "get",
            url: "/Retailer/GetLocationByDistrictId",
            data: districtId,
            dataType: "json",
            params: {
                id: districtId
            }
        });
        return response;
    }

    this.getRouteByDistrictId = function (districtId) {
        var response = $http({
            method: "get",
            url: "/Retailer/GetRouteByDistrictId",
            data: districtId,
            dataType: "json",
            params: {
                id: districtId
            }
        });
        return response;
    }
    //CE-150325-ANIS
  this.getRetailerById = function (retailerID) {
            var response =  $http({
                method: "post",
                url: "/Retailer/GetRetailerById",
                params: {
                    id: retailerID
                }
            });
            return response;
        }

    //Save (Update)  
        this.update = function (Retailer) {
            var response = $http({
                method: "post",
                url: "/Retailer/Update",
                data: JSON.stringify(Retailer),
                dataType: "json"
            });
            return response;
        }

    //Delete 
        this.Delete = function (retailerId) {
            var response = $http({
                method: "post",
                url: "/Retailer/Delete",
                params: {
                    id: retailerId
                }
            });
            return response;
        }


    //Add 
        this.Add = function (Retailer) {
            var response = $http({
                method: "post",
                url: "/Retailer/Add",
                data: JSON.stringify(Retailer),
                dataType: "json"
                
            });
            return response;
        }

        this.getRetailerType = function () {
            var retailerType = $http.get("/Retailer/GetAllRetailerType");
            return retailerType;
        };

        this.getDistrict = function () {
            return $http.get("/Retailer/GetAllDistrict");
        };
        this.getThana = function () {
            return $http.get("/Retailer/GetAllThana");
        };
        this.getZone = function () {
            return $http.get("/Retailer/GetAllZone");
           
        };
        this.getLocation = function () {
            return $http.get("/Retailer/GetAllLocation");           
        };
        this.getRoute = function () {
            return $http.get("/Retailer/GetAllRoute");           
        };

   
});

