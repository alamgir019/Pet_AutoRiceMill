app.controller("LocationCtrl", function ($scope, LocationService) {
    $scope.RETAILEREMAIL = '';
    $scope.RETAILERPHONE = '';
    GetAllDistrict();
    GetAll();


    //To Get All Records  
    function GetAll() {
        var Data = LocationService.getAllLocation();
        Data.then(function (location) {
            $scope.locations = location.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.edit = function (location) {
        $scope.LOCATIONID = location.LOCATIONID;
        $scope.LOCATIONCODE = location.LOCATIONCODE;
        $scope.LOCATIONNAME = location.LOCATIONNAME;
        $scope.DISTRICTID = location.DISTRICTID;
        $scope.Operation = "Update";
    }
    $scope.add = function () {
        $scope.LOCATIONID = "";
        $scope.LOCATIONCODE = "";
        $scope.LOCATIONNAME = "";
        $scope.DISTRICTID = $scope.Districts[-1];

    }

    $scope.Save = function () {
        var Location = {
            LOCATIONID: $scope.LOCATIONID,
            LOCATIONCODE: $scope.LOCATIONCODE,
            LOCATIONNAME: $scope.LOCATIONNAME,
            DISTRICTID: $scope.DISTRICTID
        };

        var Operation = $scope.Operation;

        if (Operation == "Update") {
            Location.LOCATIONID = $scope.LOCATIONID

            var getMSG = LocationService.Update(Location);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.add();
                //CE-150328-ANIS
                $window.location.reload();
            }, function () {
                alert('আপডেট হয়নি। ');
            });
        }
        else {
            var getMSG = LocationService.Add(Location);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.add();
                //CE-150328-ANIS
                $window.location.reload();
                //$route.reload();
            }, function () {
                alert('Insert Location Error');
            });
        }
    }
    $scope.delete = function (location) {
        var getMSG = LocationService.Delete(location.LOCATIONID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
            //CE-150328-ANIS
            $window.location.reload();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }


    //To Get All District  
    function GetAllDistrict() {
        var Data = LocationService.getDistrict();
        Data.then(function (district) {
            $scope.Districts = district.data;
        }, function () {
            alert('Error District');
        });
    }


});