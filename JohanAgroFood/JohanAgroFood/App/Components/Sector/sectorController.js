app.controller("sectorController", ["$scope", "sectorService", function (scope, sectorService) {
    scope.sector = {};
    scope.submitted = false;
    scope.sectors = [];
    scope.isEdit = null;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteSector;

    init();

    //GetUser();
    //GetAll();


    //To Get All Records  
    function init() {
        scope.sector = {};
        scope.isEdit = false;
        getSectors();

    }

    function getSectors() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = sectorService.getSector();
        result.then(function (data) {
            scope.sectors = data;
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }


    function edit(sect) {
        scope.isEdit = true;
        scope.sector = sect;
    }

    function save() {
        scope.submitted = true;
        if (scope.sectorForm.$valid)
        {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = null;
            if (scope.isEdit) {
                result = sectorService.edit(scope.sector);
            }
            else {
                scope.sector.elementCode = 3;
                result = sectorService.save(scope.sector);
            }
            result.then(function (data) {
                alert("ডাটা সেভ হয়েছে");
                init();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteSector(sec) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = sectorService.deleteSector(sec);
        result.then(function (data) {
            alert("Deleted successfully");
            init();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }
    
}]);