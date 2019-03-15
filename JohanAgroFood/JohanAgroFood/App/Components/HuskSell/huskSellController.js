app.controller("huskSellController", ["$scope", "$q", "$filter", "huskSellService", "commonSvc", "convertSvc",
    function (scope,$q,$filter, huskSellService, commonSvc, convertSvc) {//productService,
        scope.huskInfo = {};
        scope.huskReport = {};
        scope.husks = [];
        scope.stocks = [];
        var async = 3;
        var curasync = 0;

        //scope.loadHusk = loadHusk;
        scope.getTotal = getTotal;
    scope.huskInfos = [];
    scope.parties = [];
        //var template = '<div><input type="text" ng-change="grid.appScope.selectSell(row.entity)" /></div>';

    var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ' +
    '<a href="#" ng-click="grid.appScope.deleteSell(row.entity)"><span class="glyphicon glyphicon-trash">মূছুন</span></a>';
    scope.gridOptions = {
        data: 'huskInfos',
        columnDefs: [
          //{ field: 'ID', displayName: 'ID' },
          { field: 'date', displayName: 'তারিখ' },
          { field: 'noBag', displayName: 'ব্যাগের সংখ্যা' },
          { field: 'partyName', displayName: 'পার্টির নাম' },
          { field: 'productName', displayName: 'তুষের নাম' },
          { field: 'stockName', displayName: 'স্টক নাম' },
          { field: 'qty', displayName: 'তুষের পরিমাণ(ঝুড়ি)' },
          //{ field: 'unit', displayName: 'একক' },
          { field: 'uPrice', displayName: 'একক মূল্য' },
          { field: 'isSelect', displayName: '', cellTemplate: templateView }
        ],
        enableRowSelection: true,
        enableSelectAll: true,
        multiSelect: true
    };


    //scope.selectSell = selectSell;
    scope.save = save;
    scope.edit = edit;
    scope.deleteSell = deleteSell;
    scope.showReport = showReport;
    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $q.all([commonSvc.getProductByName("তুষ"), commonSvc.getAllParty(), commonSvc.getAllProduct()]).then(function (result) {
            var pids = [result[0].ID];
            scope.parties = $filter('filterById')(result[1], { pIds: pids });
            scope.husks = $filter('filter')(result[2], { parentId: result[0].ID }, true);
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        });

        var path = window.location.pathname.split('/');
        if (path[2] != "rptHuskInfo") {
            getStock();
            getHuskSellInfo();
        }
        else {
            var async = 1;
        }
    }
    function initialize() {
        scope.isEdit = false;
        scope.huskInfo = {};
    }

    //function selectSell(row) {
    //    scope.huskInfo = row;
    //}

    //function getParty() {
    //    var result = partyService.getParty();
    //    result.then(function (data) {
    //        scope.parties = data;
    //    }, function (e) {
    //        alert(e);
    //    });
    //}

    //function loadHusk() {
    //    if (scope.huskInfo.stockId == null) {
    //        scope.husks = [];
    //        return;
    //    }
    //    var result = huskSellService.loadHusk(scope.huskInfo.stockId);
    //    result.then(function (data) {
    //        scope.husks = data;
    //    }, function (e) {
    //        alert(e);
    //    });
    //}

    function getStock()
    {
        var result = commonSvc.getAllStock();
        result.then(function (data) {
            scope.stocks = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }
    function getTotal() {
        if (scope.huskInfo.transportCostInclude) {
            if (scope.huskInfo.isMon) {
                scope.huskInfo.unit = 2;
                var mon = scope.huskInfo.quantity / 40;
                scope.huskInfo.totPrice = mon * scope.huskInfo.unitPrice * scope.huskInfo.noOfBag + scope.huskInfo.transportCost;
            }
            else {
                scope.huskInfo.unit = 1;
                scope.huskInfo.totPrice = scope.huskInfo.quantity * scope.huskInfo.unitPrice * scope.huskInfo.noOfBag + parseInt(scope.huskInfo.transportCost);
            }
        }
        else {
            if (scope.huskInfo.isMon) {
                scope.huskInfo.unit = 2;
                var mon = scope.huskInfo.quantity / 40;
                scope.huskInfo.totPrice = mon * scope.huskInfo.unitPrice * scope.huskInfo.noOfBag;
            }
            else {
                scope.huskInfo.unit = 1;
                scope.huskInfo.totPrice = scope.huskInfo.quantity * scope.huskInfo.unitPrice * scope.huskInfo.noOfBag;
            }
        }
        //scope.huskInfo.totalPr = scope.huskInfo.totPrice;
        scope.huskInfo.totalpr = convertSvc.ConvtEngToBang(scope.huskInfo.totPrice);
    }
    function getHuskSellInfo() {
        var result = huskSellService.getHuskSellInfo();
        result.then(function (data) {
            scope.huskInfos = data;
            scope.huskInfos.forEach(function (hsk) {
                hsk.date = convertSvc.toDate(hsk.date); 
                hsk.qty = convertSvc.ConvtEngToBang(hsk.quantity);
                hsk.uPrice = convertSvc.ConvtEngToBang(hsk.unitPrice);
                hsk.noBag = convertSvc.ConvtEngToBang(hsk.noOfBag);
                hsk.transCost = convertSvc.ConvtEngToBang(hsk.transportCost);
                hsk.paidAmnt = convertSvc.ConvtEngToBang(hsk.paidAmount);

            });
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
        scope.huskInfo = row;
    }
    function save() {
        if (!scope.huskSellForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = null;
        var operation = null;
        if (scope.isEdit) {
            operation = "edit";
            result = huskSellService.edit(scope.huskInfo);
        }
        else {
            operation = "save";
            result = huskSellService.save(scope.huskInfo);
        }
        result.then(function (data) {
            alert("ডাটা সেভ হয়েছে");
            scope.huskInfo.ID = data.ID;
            scope.huskInfo.incSrcId = data.incSrcId;
            convertSvc.updateCollection(scope.huskInfos, scope.huskInfo, operation, "ID");
            initialize(); 
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
     }
    function deleteSell(row) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = huskSellService.deleteSell(row);
        result.then(function (data) {
            alert("ডাটা ডিলিট হয়েছে");
            convertSvc.updateCollection(scope.huskInfos, row, "delete", "ID");
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
    function showReport() {
        var result = huskSellService.showReport(scope.huskReport);
    }
}]);