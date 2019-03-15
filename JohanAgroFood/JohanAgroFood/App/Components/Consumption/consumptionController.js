app.controller("consumptionController", ["$scope", "consumptionService", "convertSvc","commonSvc","$q","$filter",
    function (scope, consumptionService, convertSvc, commonSvc, $q, $filter) {
    scope.consumption = {};
    scope.sector = {};
    scope.submitted = false;
    scope.consumptions = [];
    scope.sectors = [];
    scope.isEdit = null;
    var async = 3;
    var curasync = 0;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteConsumption;
    scope.selectSector = selectSector;
    init();

    //GetUser();
    //GetAll();

    function selectSector() {
        scope.consumption.srcDescId = scope.sector.ID;
        scope.consumption.sourceName = scope.sector.elementName;
    }

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
        scope.consumption = {};
        scope.isEdit = false;
        getParties();
        getSectors();
        getConsumptions();

    }
    function getParties()
    {
        $q.all([commonSvc.getProductByName("অফিস"), commonSvc.getAllParty()]).then(function (result) {
            var pid = result[0].ID;
            scope.parties = $filter('filter')(result[1], { productId: pid }, true);
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        });
    }

    var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ' +
           '<a href="#" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash">মূছুন</span></a>';
    scope.gridOptions = {
        data: 'consumptions',
        columnDefs: [
          //{ field: 'ID', displayName: 'ID' },
          { field: 'date', displayName: 'তারিখ' },
          { field: 'sourceName', displayName: 'খাত', },
          { field: 'srcDescription', displayName: 'বর্ণনা', width: "35%"},
          { field: 'amt', displayName: 'পরিমাণ' },
          { field: 'isSelect', displayName: '', cellTemplate: templateView }
        ],
        enableRowSelection: true,
        enableSelectAll: true,
        multiSelect: true
    };
    function getSectors() {
        var result = commonSvc.getAllSector();
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

    function getConsumptions() {
        var result = consumptionService.getConsumption();
        result.then(function (data) {
            scope.consumptions = data;
            scope.consumptions.forEach(function (con) {
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


    function edit(sect) {
        scope.isEdit = true;
        scope.consumption = sect;
        //scope.consumption.date = convertSvc.toDate(sect.date);
        scope.sector = null;//$filter('filter')(scope.sectors, { ID: sect.srcDescId }, true);

        //scope.sector.ID = sect.srcDescId;
        //scope.sector.elementName = sect.sourceName;
    }

    function save() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        scope.submitted = true;
        if (scope.consumptionForm.$valid) {
            var result = null;
            if (scope.isEdit) {
                //scope.consumption.sourceName = "";
                result = consumptionService.edit(scope.consumption);
            }
            else {
                result = consumptionService.save(scope.consumption);
            }
            result.then(function (data) {
                async = 3;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                initialize();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteConsumption(sec) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = consumptionService.deleteConsumption(sec);
        result.then(function (data) {
            async = 3;
            curasync = 0;
            alert("Deleted successfully");
            initialize();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

}]);