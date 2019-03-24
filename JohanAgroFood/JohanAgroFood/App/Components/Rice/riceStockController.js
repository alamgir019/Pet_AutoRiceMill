app.controller("riceStockController", ["$scope", "riceStockService", "convertSvc", "productService",
    function (scope, riceStockService, convertSvc, productService) {
    scope.riceStock = {};
    scope.submitted = false;
    scope.riceStocks = [];
    scope.stocks = [];
    scope.rices = [];
    scope.isEdit = null;
    var async = 3;
    var curasync = 0;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteRiceStock;
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
        getRice();
        getStock();
        initialize();
    }

    function initialize() {
        scope.riceStock = {};
        scope.isEdit = false;
        getRiceStocks();

    }

    function getRice() {
        var rice = { parentId: 2 };
        var result = productService.getProduct(rice);
        result.then(function (data) {
            scope.rices = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function getStock() {
        var result = riceStockService.getStock();
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

    function getRiceStocks() {
        var result = riceStockService.getRiceStock();
        result.then(function (data) {
            scope.riceStocks = data;
            scope.riceStocks.forEach(function (con) {
                con.createDate = convertSvc.toDate(con.createDate);
                con.sckQuantity = convertSvc.ConvtEngToBang(con.sackQuantity);
                //con.sckWeight = convertSvc.ConvtEngToBang(con.sackWeight);
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
        scope.riceStock = inc;
        //scope.riceStock.date = convertSvc.toDate(inc.date);

        scope.sector.ID = inc.srcDescId;
        scope.sector.elementName = inc.sourceName;
    }

    function save() {
        scope.submitted = true;
        if (scope.riceStockForm.$valid) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
            var result = null;
            var operation = null;
            if (scope.isEdit) {
                //scope.riceStock.sourceName = "";
                result = riceStockService.edit(scope.riceStock);
                operation = "save";
            }
            else {
                result = riceStockService.save(scope.riceStock);
                operation = "edit";
            }
            result.then(function (data) {
                async = 1;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                convertSvc.updateCollection(scope.riceStocks, scope.riceStock, operation, "ID");
                initialize();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteRiceStock(sec) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = riceStockService.deleteRiceStock(sec);
        result.then(function (data) {
            async = 1;
            curasync = 0;
            alert("ডাটা ডিলিট হয়েছে");
            initialize();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

}]);