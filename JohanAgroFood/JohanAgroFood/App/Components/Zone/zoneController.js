app.controller("zoneCtlr", ["$scope", "zoneSvc", function (scope, zoneSvc) {
    scope.zone = {};
    scope.submitted = false;
    scope.zones = [];
    scope.isEdit = null;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteZone;

    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        scope.zone = {};
        scope.isEdit = false;
        getZones();

    }

    function getZones() {
        var result = zoneSvc.getZone();
        result.then(function (data) {
            scope.zones = data;
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }


    function edit(zn) {
        scope.isEdit = true;
        scope.zone = zn;
    }

    function save() {
        scope.submitted = true;
        if (scope.zoneForm.$valid) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = null;
            if (scope.isEdit) {
                result = zoneSvc.edit(scope.zone);
            }
            else {
                scope.zone.elementCode = 4;
                result = zoneSvc.save(scope.zone);
            }
            result.then(function (data) {
                alert("ডাটা সেভ হয়েছে");
                init();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteZone(zn) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = zoneSvc.deleteZone(zn);
        result.then(function (data) {
            alert("Deleted successfully");
            init();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

}]);