app.controller("SrCtrl", function ($scope, SrService) {
    GetAllBuyer();
    GetAllRoute();
   // GetAllRetailer();
    $scope.divRetailerByRoute = false;


   /* $scope.selection = [];
    var objData;
    // toggle selection for a given employee by name
    $scope.toggleSelection = function toggleSelection(retailerId) {
        debugger
        var idx = $scope.selection.indexOf(retailerId);   
        // is currently selected
        if (idx > -1) {
            $scope.selection.splice(idx, 1);
        }

            // is newly selected
        else {
            $scope.selection = $scope.selection.push(retailerId);           
        }
        objData = JSON.stringify($scope.selection);
       
    };*/

 
    //To Get Retailer By Route 
    $scope.GetRetailerByRouteId = function () {
        debugger
        var routeId = $scope.RoutId;
        var Data = SrService.getRetailerByRouteId(routeId);
        Data.then(function (retailerByRoutId) {
            $scope.retailerIds = retailerByRoutId.data;
            $scope.divRetailerByRoute = true;
        }, function () {
            alert('Errot Get Retailer By Route Id')
        });
    };

    
    //To Get All Retailer Records  
    function GetAllRetailer() {
        var Data = SrService.getAllRetailer();
        Data.then(function (retailer) {
            $scope.retailers = retailer.data;
        }, function () {
            alert('Error');
        });
    }

    //

    $scope.edit = function (location) {
        $scope.LOCATIONID = location.LOCATIONID;
        $scope.LOCATIONCODE = location.LOCATIONCODE;
        $scope.LOCATIONNAME = location.LOCATIONNAME;     
        $scope.DISTRICTID = $scope.Districts[location.DISTRICTID];
       // $scope.DISTRICTID = locations[location.DISTRICTID];

        $scope.Operation = "Update";     
    }
    $scope.add = function () {
        $scope.SrId = "";
        $scope.SrName = "";
        $scope.DistrubutorName = $scope.DistrubutorName;
        $scope.SrMobile1 = "";
        $scope.SrMobile2 = "";
        $scope.SrEmail = "";
        $scope.Operation = "Add";
        
    }
    // Item List Arrays
    $scope.retailerIds = [];
    $scope.checked = [];
    $scope.checkRetailerAdd = function () {
        debugger
        $scope.retailerIds.push({
            RETAILERID: $scope.retailer.RETAILERID
        });
    }
    $scope.Save = function () {
        debugger
        var Sr = {
           // SRID: $scope.SrId,
            SRNAME: $scope.SrName,
            SRBUYERID:$scope.DistrubutorName,
            SRMOBILE1:$scope.SrMobile1,
            SRMOBILE2:$scope.SrMobile2,
            SREMAIL: $scope.SrEmail,
           SRDETAILS: $scope.retailerIds

           // DISTRICTID:$scope.DISTRICTID.DISTRICTID
        };
      
    //    var SRDETAILS = [127,162];

        var Operation = $scope.Operation;
        
        if (Operation == "Update") {
            Sr.SRID = $scope.SrId;        
            var getMSG = SrService.Update(Sr);
            getMSG.then(function (messagefromController) {
              //  GetAllSr();
                alert(messagefromController.data);
                $scope.add();
            }, function () {
                alert('আপডেট হয়নি। ');
            });
        }
        else {
            var getMSG = SrService.Add(Sr);
            getMSG.then(function (messagefromController) {
               // GetAll();
                alert(messagefromController.data);
             //   $scope.add();
               // $route.reload();
            }, function () {
                alert('Insert Sr Error');
            });
        }

        $scope.delete = function (location) {
            var getMSG = LocationService.Delete(location.LOCATIONID);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
            }, function () {
                alert('ডিলিট হয়নি। ');
            });
        }
    }
        
    //To Get All Distributor/Buyer   
    function GetAllBuyer() {
        debugger;
        var Data = SrService.getBuyer();
        Data.then(function (buyer) {
            $scope.Buyers = buyer.data;
        }, function () {
            alert('Error Buyer');
        });
    }
    //To Get All Routr   
    function GetAllRoute() {
        var Data = SrService.getRoute();
        Data.then(function (route) {
            $scope.Routes = route.data;
        }, function () {
            alert('Error Route');
        });
    }

});