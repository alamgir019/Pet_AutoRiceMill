app.controller("RouteCtrl", function ($scope, RouteService) {
    GetAllDistrict();
    GetAll();
    //To Get All Records  
    function GetAll() {
        var Data = RouteService.getAllRoute();
        Data.then(function (route) {
            $scope.routes = route.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.edit = function (route) {
        $scope.ROUTEID = route.ROUTEID;
        $scope.ROUTECODE = route.ROUTECODE;
        $scope.ROUTENAME = route.ROUTENAME;
        $scope.DISTRICTID = route.DISTRICTID;
        $scope.Operation = "Update";
    }
    $scope.add = function () {
        $scope.ROUTEID = "";
        $scope.ROUTECODE = "";
        $scope.ROUTENAME = "";
        $scope.DISTRICTID = $scope.Districts[-1];
    }
    $scope.Save = function () {
        var Route = {
            ROUTEID: $scope.ROUTEID,
            ROUTECODE: $scope.ROUTECODE,
            ROUTENAME: $scope.ROUTENAME,
            DISTRICTID: $scope.DISTRICTID
        };

        var Operation = $scope.Operation;

        if (Operation == "Update") {
            Route.ROUTEID = $scope.ROUTEID
            var getMSG = RouteService.Update(Route);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.add();
            }, function () {
                alert('আপডেট হয়নি। ');
            });
        }
        else {
            var getMSG = RouteService.Add(Route);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.add();
            }, function () {
                alert('Insert Route Error');
            });
        }
    }
    $scope.delete = function (route) {
        var getMSG = RouteService.Delete(route.ROUTEID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

    //CE-150325-ANIS
    //To Get All District  
    function GetAllDistrict() {
        debugger;
        var Data = RouteService.getDistrict();
        Data.then(function (district) {
            $scope.Districts = district.data;
        }, function () {
            alert('Error District');
        });
    }
    //CE-edit-150325-ANIS

});