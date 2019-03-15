app.controller("incomeController", ["$scope", "incomeService", "sectorService", "convertSvc",
    function (scope, incomeService, sectorService, convertSvc) {
    scope.income = {};
    scope.sector = {};
    scope.submitted = false;
    scope.incomes = [];
    scope.sectors = [];
    scope.isEdit = null;
    var async = 2;
    var curasync = 0;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteIncome;
    //scope.selectSector = selectSector;
    init();

    //GetUser();
    //GetAll();

    //function selectSector() {
    //    //scope.income.srcDescId = scope.sector.ID;
    //    scope.income.sourceName = scope.sector.elementName;
    //}

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        initialize();
    }

    function initialize() {
        scope.income = {};
        scope.isEdit = false;
        getSectors();
        getIncomes();

    }

    function getSectors() {
        var result = sectorService.getSector();
        result.then(function (data) {
            scope.sectors = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function getIncomes() {
        var result = incomeService.getIncome();
        result.then(function (data) {
            scope.incomes = data;
            scope.incomes.forEach(function (con) {
                con.date = convertSvc.toDate(con.date);
                con.amt = convertSvc.ConvtEngToBang(con.amount);
            });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }


    function edit(inc) {
        scope.isEdit = true;
        scope.income = inc;
        //scope.income.date = convertSvc.toDate(inc.date);
        scope.income.amt = convertSvc.ConvtEngToBang(scope.income.amount);
        //scope.sector.ID = inc.srcDescId;
        //scope.sector.elementName = inc.sourceName;
        scope.income.srcDescId = inc.srcDescId;
        scope.income.sourceName = inc.sourceName;
    }

    function save() {
        scope.submitted = true;
        if (scope.incomeForm.$valid) {

            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });

            var result = null;
            if (scope.isEdit) {
                //scope.income.sourceName = "";
                result = incomeService.edit(scope.income);
            }
            else {
                result = incomeService.save(scope.income);
            }
            result.then(function (data) {
                async = 2;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                initialize();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteIncome(sec) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = incomeService.deleteIncome(sec);
        result.then(function (data) {
            async = 2;
            curasync = 0;
            alert("Deleted successfully");
            initialize();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

}]);