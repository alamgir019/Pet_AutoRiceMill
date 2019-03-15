app.controller("paddyCtrl", ["$scope", "$filter", "$timeout", "commonSvc", "paddySvc", "convertSvc", "$q",
    function (scope, $filter, $timeout,commonSvc, paddySvc, convertSvc, $q) {
    scope.paddyInfo = {};
    scope.paddyInfos = [];
    scope.paddies = [];
    scope.stocks = [];
    scope.isEdit = null;
    var async = 3;
    var curasync = 0;
    scope.getPaddyPrice = getPaddyPrice;
    //scope.getTotalPrice = getTotalPrice;
    scope.getTotalPerMon = getTotalPerMon;
        scope.kgConvertToMon = kgConvertToMon;
        //scope.monConvertTokg = monConvertTokg;
    scope.paddyInfo.unit = 2;

    var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ' +
        '<a href="#" ng-click="grid.appScope.deleteBuy(row.entity)"><span class="glyphicon glyphicon-trash">মূছুন</span></a>';
    scope.gridOptions = {
        data: 'paddyInfos',
        columnDefs: [
          { field: 'date', displayName: 'তারিখ' },
          { field: 'noOfBag', displayName: 'ব্যাগের সংখ্যা' },
          { field: 'partyName', displayName: 'পার্টির নাম' },
          { field: 'productName', displayName: 'ধানের নাম' },
          { field: 'stockName', displayName: 'স্টক নাম' },
          { field: 'qtyKg', displayName: 'প্রতি ব্যাগে ধান(কেজি)' },
          { field: 'bagPrice', displayName: 'প্রতি ব্যাগের মূল্য' },
          { field: 'uPrice', displayName: 'প্রতি মণের মূল্য' },
          { field: 'isSelect', displayName: '', cellTemplate: templateView }
        ],
        filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
        enableFiltering: true
    };

    init();

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.deleteBuy = deleteBuy;

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $q.all([commonSvc.getProductByName("ধান"), commonSvc.getAllParty(), commonSvc.getAllProduct()]).then(function (result) {
            var pids = [result[0].ID];
            scope.parties = $filter('filterById')(result[1], { pIds: pids });
            scope.paddies = $filter('filter')(result[2], { parentId: pids[0] }, true);
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        });
        var path = window.location.pathname.split('/');
        if (path[2] != "rptPaddyInfo") {
            initialize();
            getAllStock();
            getPaddyInfos();
        }
        else {
            var async = 1;
        }
    }
    function initialize() {
        scope.paddyInfo = {};
        scope.isEdit = false;
    }
    function getPaddyInfos()
    {
        var result = paddySvc.getPaddyBuyInfo();
        result.then(function (data) {
            scope.paddyInfos = data;
            scope.paddyInfos.forEach(function (r) {
                r.date = convertSvc.toDate(r.date);
                r.trNum = convertSvc.ConvtEngToBang(r.truckNumber);
                r.noBag = convertSvc.ConvtEngToBang(r.noOfBag);
                r.qtyKg = convertSvc.ConvtEngToBang(r.quantityPerBag);
                r.uPrice = convertSvc.ConvtEngToBang(r.price);
                r.transCost = convertSvc.ConvtEngToBang(r.transportCost);
                r.lcpb = convertSvc.ConvtEngToBang(r.labourCostPerBag);
                r.paidAmoun = convertSvc.ConvtEngToBang(r.amount);
                //r.noOfSackSen = convertSvc.ConvtEngToBang(r.noOfSackSent);
                r.noOfSackRcv = convertSvc.ConvtEngToBang(r.noOfSackRcvd);
                r.bagPric = convertSvc.ConvtEngToBang(r.bagPrice);

            });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert("getPaddySellInfo error");
        });
    }
    //function monConvertTokg() {
    //    var quantityPerBagKg = scope.paddyInfo.quantityPerBag * 40;
    //    scope.paddyInfo.quantityPerBagkg = convertSvc.ConvtEngToBang(quantityPerBagKg);
    //}
    function kgConvertToMon() {
        $timeout(function () {
            var totalinMon = (scope.paddyInfo.quantityPerBag * scope.paddyInfo.noOfBag)/40;
            scope.paddyInfo.qtyMon = convertSvc.ConvtEngToBang(totalinMon);
            scope.paddyInfo.quantityMon = totalinMon;
        }, 500);
    }

    //function getAllParty() {
    //    var result = commonSvc.getAllParty();
    //    result.then(function (data) {
    //        //scope.parties = data;
    //        scope.parties = $filter('filter')(data, { productId: 1 });
    //    }, function (e) {
    //        alert(e);
    //    });
    //}
    
    //function getAllProduct() {
    //    var result = commonSvc.getAllProduct();
    //    result.then(function (data) {
    //        scope.paddies = $filter('filter')(data, { parentId: 1 });
    //    }, function (e) {
    //        alert(e);
    //    });
    //}
    function getAllStock() {
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

    function getPaddyPrice() {
        scope.paddyInfo.padPrice = scope.paddyInfo.price * scope.paddyInfo.quantityMon;
        scope.paddyInfo.totPrice = convertSvc.ConvtEngToBang(scope.paddyInfo.padPrice);
    }

    //function getTotalPrice() {
    //    getPaddyPrice();
    //    scope.paddyInfo.totaPrice = scope.paddyInfo.padPrice + parseInt(scope.paddyInfo.transportCost);
    //    scope.paddyInfo.totalPrice = convertSvc.ConvtEngToBang(scope.paddyInfo.totaPrice);
    //}
    function getTotalPerMon() {
        if (scope.paddyInfo.transportCostInclude)
        {
            scope.paddyInfo.padPrice = scope.paddyInfo.price * scope.paddyInfo.quantityMon + parseInt(scope.paddyInfo.transportCost);
            var totalPerMon = scope.paddyInfo.padPrice / scope.paddyInfo.quantityMon;
            scope.paddyInfo.totalPerMon = convertSvc.ConvtEngToBang(totalPerMon);
        }
        else
        {
            scope.paddyInfo.padPrice = scope.paddyInfo.price * scope.paddyInfo.quantityMon;
            var totalPerMon = scope.paddyInfo.padPrice / scope.paddyInfo.quantityMon;
            scope.paddyInfo.totalPerMon = convertSvc.ConvtEngToBang(totalPerMon);
        }
        
    }
    function edit(row) {
        scope.isEdit = true;
        scope.paddyInfo = row;

    }
    function deleteBuy(row) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = paddySvc.deleteBuy(row);
        result.then(function (data) {
            alert("ডাটা ডিলিট হয়েছে");
            convertSvc.updateCollection(scope.paddyInfos, row, "delete", "ID");
            $('#myModal').modal('hide');
        }, function (e) {
            alert("Delete Error");
        });
    }
    function save() {
        if (!scope.paddyInfoForm.$valid) {
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
            result = paddySvc.edit(scope.paddyInfo);
        }
        else {
            operation = "save";

            result = paddySvc.save(scope.paddyInfo);
        }
        result.then(function (data) {
            alert("ডাটা সেভ হয়েছে");
            scope.paddyInfo.ID = data;
            convertSvc.updateCollection(scope.paddyInfos, scope.paddyInfo, operation, "ID");
            initialize();
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }

}]);