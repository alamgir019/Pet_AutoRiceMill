app.controller("paddyTransferCtrl", ["$scope", "$filter", "commonSvc", "$q","$timeout", "paddyTransferSvc", "convertSvc",
    function (scope, $filter, commonSvc, $q,$timeout, paddyTransferSvc, convertSvc) {
        scope.paddyTransfer = {};
        scope.paddyTransferInfos = [];
        scope.paddies = [];
        scope.stocks = [];
        scope.sackTypes = [];
        var async = 3;
        var curasync = 0;

        scope.getSackWeights = getSackWeights;
        scope.totalAmount = totalAmount;
        scope.save = save;
        //scope.edit = edit;
        scope.delete = deleteCell;

        init();
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
            getStock();
            
            initialize();
        }

        //var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ' +
        //'<a href="#" ng-click="grid.appScope.deleteSell(row.entity)"><span class="glyphicon glyphicon-trash">মূছুন</span></a>';
        scope.gridOptions = {
            data: 'paddyTransferInfos',
            columnDefs: [
              { field: 'stockName', displayName: 'গোডাউনের নাম' },
              { field: 'productName', displayName: 'ধানের নাম' },
              { field: 'sackQuantity', displayName: 'ব্যাগের সংখ্যা' },
              { field: 'sackWeight', displayName: 'ব্যাগের ওজন' }
              //{ field: 'isSelect', displayName: '', cellTemplate: templateView }
            ],
            filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
            enableFiltering: true
        };

        function initialize() {
            scope.paddyTransfer = {};
            scope.placeholder = "সিরিয়াল";
            scope.millCost = "প্রতি মণে মিল খরচ";
            scope.isEdit = false;
            getPaddyTranferInfos();
        }

        //function edit(padTrans) {
        //    scope.isEdit = true;
        //    scope.paddyTransfer = padTrans;
        //}
        function getPaddyTranferInfos() {
            var result = paddyTransferSvc.getPaddyTransferInfos();
            result.then(function(data) {
                scope.paddyTransferInfos = data;
                scope.paddyTransferInfos.forEach(function(r) {
                    r.sackWeight = convertSvc.ConvtEngToBang(r.sackWeight);
                    r.sackQuantity = convertSvc.ConvtEngToBang(r.sackQuantity);
                });
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function(e) {
                    alert("get paddy transfer error");
                });
        }
        function getStock() {
            var result = commonSvc.getAllStock();
            result.then(function (data) {
                scope.stocks = data;
                scope.hollar = $filter('filter')(scope.stocks, { stockName: "হোলার" })[0];
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert("get stock paddy error");
            });
        }
        function getSackWeights() {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = paddyTransferSvc.getSackWeights(scope.paddyTransfer.productId, scope.paddyTransfer.stockId);
            result.then(function (data) {
                scope.sackTypes = data;
                scope.sackTypes.forEach(function (r) {                    
                    r.bagWeight = convertSvc.ConvtEngToBang(r.sackWeight);
                });
                $('#myModal').modal('hide');
            }, function (e) {
                alert("get sack type error");
            });
        }
        function totalAmount() {
            $timeout(function () {
                var total = (scope.paddyTransfer.sackQuantity * scope.paddyTransfer.sackWeight)/40;
                scope.paddyTransfer.totQuantityMon = convertSvc.ConvtEngToBang(total);
                scope.paddyTransfer.totalQuantityMon = total;
            }, 500);
        }
        function save() {
            if (!scope.paddyTransferForm.$valid) {
                return;
            }
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = null;
            var operation = null;
            //if (scope.isEdit) {
            //    operation = "edit";
            //    result = paddyTransferSvc.edit(scope.paddyTransfer);
            //}
            //else {
            operation = "save";
            result = paddyTransferSvc.save(scope.paddyTransfer);
            //}
            result.then(function (data) {
                if (data==0) {
                    alert("ডাটা সেভ হয়নি");
                }
                else {
                    alert("ডাটা সেভ হয়েছে");
                    initialize();
                }
                $('#myModal').modal('hide');
            }, function (e) {
                alert("ডাটা সেভ হয়নি");
            });
        }
        function deleteCell(padTrans) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = paddyTransferSvc.deleteCell(padTrans);
            result.then(function (data) {
                alert("ডিলিট হয়েছে");
                initialize();
                $('#myModal').modal('hide');
            }, function () {
                alert('ডিলিট হয়নি। ');
            });
        }
    }]);