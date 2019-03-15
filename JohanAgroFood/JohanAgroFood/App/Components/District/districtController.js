app.controller("districtCtlr", ["$scope", "districtSvc", function (scope, districtSvc) {
    scope.district = {};
    scope.submitted = false;
    scope.districts = [];
    scope.isEdit = null;
    var async = 1;
    var curasync = 0;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteDistrict;

    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        scope.district = {};
        scope.isEdit = false;
        getDistricts();

    }

    function getDistricts() {
        var result = districtSvc.getDistrict();
        result.then(function (data) {
            scope.districts = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }


    function edit(dst) {
        scope.isEdit = true;
        scope.district = dst;
    }

    function save() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        scope.submitted = true;
        if (scope.districtForm.$valid) {
            var result = null;
            if (scope.isEdit) {
                result = districtSvc.edit(scope.district);
            }
            else {
                scope.district.elementCode = 1;
                result = districtSvc.save(scope.district);
            }
            result.then(function (data) {
                async = 1;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                init();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteDistrict(dst) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = districtSvc.deleteDistrict(dst);
        result.then(function (data) {
            async = 1;
            curasync = 0;
            alert("ডাটা ডিলিট হয়েছে");
            init();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

}]);