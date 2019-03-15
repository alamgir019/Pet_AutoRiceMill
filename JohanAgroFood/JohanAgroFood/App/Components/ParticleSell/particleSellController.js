app.controller("particleSellController", ["$scope", "$filter","$q", "particleSellService", "commonSvc", "convertSvc",
function (scope,$filter,$q, particleSellService, commonSvc, convertSvc) {
    scope.particleInfo = {};
    scope.particleReport = {};
        scope.isEdit = false;
        scope.particles = [];
        scope.stocks = [];
        scope.particleInfos = [];
        scope.parties = [];
        var async = 4;
        var curasync = 0;
    //var template = '<div><input type="text" ng-change="grid.appScope.selectSell(row.entity)" /></div>';
        var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ' +
        '<a href="#" ng-click="grid.appScope.deleteSell(row.entity)"><span class="glyphicon glyphicon-trash">মূছুন</span></a>';
    scope.gridOptions = {
        data: 'particleInfos',
        columnDefs: [
          { field: 'date', displayName: 'তারিখ' },
          { field: 'noBag', displayName: 'ব্যাগের সংখ্যা' },
          { field: 'partyName', displayName: 'পার্টির নাম' },
          { field: 'productName', displayName: 'ক্ষুদের নাম' },
          { field: 'stockName', displayName: 'স্টক নাম' },
          { field: 'qty', displayName: 'ক্ষুদের পরিমাণ(কেজি)' },
          { field: 'uPrice', displayName: 'প্রতি কেজির মূল্য' },
          { field: 'isSelect', displayName: '', cellTemplate: templateView }
],
        enableRowSelection: true,
        enableSelectAll: true,
        multiSelect: true
    };


    scope.save = save;
    scope.getTotal = getTotal;
    scope.edit = edit;
    scope.deleteSell = deleteSell;
    //scope.loadParticle = loadParticle;
    init();

    //GetUser();
    //GetAll();


    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $q.all([commonSvc.getProductByName("চাল"), commonSvc.getProductByName("ক্ষুদ"), commonSvc.getAllParty()]).then(function (result) {
            var pids = [result[0].ID, result[1].ID];
            scope.parties = $filter('filterById')(result[2], { pIds: pids });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        });
        getParticle();
        getStock();
        getParticleSellInfo();
        initialize();

    }
    function initialize() {
        scope.isEdit = false;
        scope.particleInfo = {};
        scope.particleInfo.isMon = false;
    }
    function edit(row) {
        scope.isEdit = true;
        scope.particleInfo = row;
    }

    function deleteSell(row) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = particleSellService.deleteSell(row);
        result.then(function (data) {
            alert("Deleted successfully");
            convertSvc.updateCollection(scope.particleInfos, row, "delete", "ID");
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }

    function getParticle()
    {
        var result = commonSvc.getAllProduct();
        result.then(function (data) {
            scope.particles = $filter('filter')(data, { parentId: 11 });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function getStock()
    {
        var result = particleSellService.getStock();
        result.then(function (data) {
            scope.stocks = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert("getStock error");
        });
    }

    function getParticleSellInfo() {
        var result = particleSellService.getParticleSellInfo();
        result.then(function (data) {
            scope.particleInfos = data;
            scope.particleInfos.forEach(function (r) {
                if (r.unit==2) {
                    r.isMon = true;
                }
                else {
                    r.isMon = false;
                }
                r.date = convertSvc.toDate(r.date);
                r.noBag = convertSvc.ConvtEngToBang(r.noOfBag);
                r.uPrice = convertSvc.ConvtEngToBang(r.unitPrice);
                r.qty = convertSvc.ConvtEngToBang(r.quantity);
                r.paidAmnt = convertSvc.ConvtEngToBang(r.paidAmount);
                r.transCost = convertSvc.ConvtEngToBang(r.transportCost);
            });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert("getParticleSellInfo error");
        });
    }
    
    function getTotal() {
        if (scope.particleInfo.isMon) {
            scope.particleInfo.unit = 2;
            var mon = scope.particleInfo.quantity / 40;
            scope.particleInfo.totPrice = mon * scope.particleInfo.unitPrice * scope.particleInfo.noOfBag;
        }
        else {
            scope.particleInfo.unit = 1;
            scope.particleInfo.totPrice = scope.particleInfo.quantity * scope.particleInfo.unitPrice * scope.particleInfo.noOfBag;
        }
        scope.particleInfo.totalpr = convertSvc.ConvtEngToBang(scope.particleInfo.totPrice);
    }

    function save() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = null;
        var operation = null;
        if (scope.isEdit) {
            operation = "edit";
            result = particleSellService.edit(scope.particleInfo);
        }
        else {
            operation = "save";
            result = particleSellService.save(scope.particleInfo);
        }
        result.then(function (data) {
            alert("ডাটা সেভ হয়েছে");
            scope.particleInfo.ID = data.ID;
            scope.particleInfo.incSrcId = data.incSrcId;            
            convertSvc.updateCollection(scope.particleInfos, scope.particleInfo, operation, "ID");
            initialize();
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
    
}]);