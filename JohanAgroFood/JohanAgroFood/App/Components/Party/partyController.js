app.controller("partyController", ["$scope", "convertSvc", "partyService", "zoneSvc", "productService",
    function (scope,convertSvc, partyService, zoneService, productService) {
        scope.party = {};
    scope.product = {};
    scope.parties = [];
    scope.zones = [];
    scope.products = [];
    scope.district = [];
    var async = 4;
    var curasync = 0;

    // methods
    scope.edit = edit;
    scope.delete = deleteUser;
    scope.save = save;
    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        initialize();
        getParties();
        getZones();
        getDistrict();
        getParentProduct();
        

    }

    function initialize()
    {
        scope.party = {};
        scope.isEdit = false;
    }

    function getParties() {
        var result = partyService.getParty();
        result.then(function (data) {
            scope.parties = data;
            scope.parties.forEach(function (con) {
                con.contact = convertSvc.ConvtEngToBang(con.contactNo);
                con.cash = con.isCashParty?"হ্যা":"না";
            });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }
    function getZones() {
        var result = zoneService.getZone();
        result.then(function (data) {
            scope.zones = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }
    function getDistrict() {
        var result = partyService.getDistrict();
        result.then(function (data) {
            scope.district = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }
    function getParentProduct() {
        scope.product.parentId = 0;
        var result = productService.getProduct(scope.product);
        result.then(function (data) {
            scope.products = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function edit(row) {
        scope.isEdit = true;
        scope.party = row;
    }
        
    function save() {
        if (!scope.partyForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var operation = null;
        var result = null;
        if (scope.isEdit) {
            operation = "edit";
            result = partyService.edit(scope.party);
        }
        else {
            operation = "save";
            var result = partyService.save(scope.party);
        }
        
        result.then(function (data) {
            async = 1;
            curasync = 0;
            alert("ডাটা সেভ হয়েছে");
            getParties();
            initialize();
        }, function (e) {
            alert(e);
        });
    }

    function deleteUser(row) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = partyService.deleteUser(row);
        result.then(function (data) {
            alert("ডাটা ডিলিট হয়েছে");
            convertSvc.updateCollection(scope.parties, row, "delete", "ID");
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }


    //To Get All District  
    function GetAllDistrict() {
        var Data = userService.getDistrict();
        Data.then(function (district) {
            $scope.Districts = district.data;
        }, function () {
            alert('Error District');
        });
    }


}]);