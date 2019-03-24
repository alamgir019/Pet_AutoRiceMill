app.controller("prodStockCtrl", ["$scope", "prodStockSvc", "convertSvc", "commonSvc", "$filter","$q",
    function (scope, prodStockSvc, convertSvc, commonSvc, $filter, $q) {
        scope.prodStock = {};
        scope.submitted = false;
        scope.parents = [];
        scope.stocks = [];
        scope.prducts = [];
        scope.allProducts = [];
        scope.isEdit = null;
        scope.prodStocks = [];
        scope.stockReport = {};
        var async = 2;
        var curasync = 0;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.deleteProdStock = deleteProdStock;
    scope.getChildProd = getChildProd;
    scope.getChildProdRpt = getChildProdRpt;
    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $q.all([commonSvc.getProductByName("ধান"), commonSvc.getAllProduct()]).then(function (result) {
            var paddyId = result[0].ID;
            var param = "!" + paddyId;
            scope.allProducts = $filter('filter')(result[1], { ID: param,parentId:param });
            scope.parents = $filter('filter')(scope.allProducts, { parentId: 0 }, true);
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        });
        getStock();
        initialize();
    }

    function initialize() {
        scope.prodStock = {};
        scope.isEdit = false;
        getProdStocks();

    }
    function getChildProd() {
        scope.prducts = $filter('filter')(scope.allProducts, { parentId: scope.prodStock.parentId },true);
    }
    function getChildProdRpt() {
        scope.prducts = $filter('filter')(scope.allProducts, { parentId: scope.stockReport.parentId },true);
    }
    function getStock() {
        var result = prodStockSvc.getStock();
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

    function getProdStocks() {
        var result = prodStockSvc.getProdStocks();
        result.then(function (data) {
            scope.prodStocks = data;
            scope.prodStocks.forEach(function (con) {
                con.createDate = convertSvc.toDate(con.createDate);
                con.sckQuantity = convertSvc.ConvtEngToBang(con.sackQuantity);
                con.sckWeight = convertSvc.ConvtEngToBang(con.sackWeight);
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
        scope.prducts = scope.allProducts;
        scope.prodStock = inc;
        scope.prodStock.date = null;
        
        scope.sector.ID = inc.srcDescId;
        scope.sector.elementName = inc.sourceName;
    }

    function save() {
        scope.submitted = true;
        if (scope.prodStockForm.$valid) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = null;
            var operation = null;
            if (scope.isEdit) {
                //scope.prodStock.sourceName = "";
                result = prodStockSvc.edit(scope.prodStock);
                operation = "edit";
            }
            else {
                result = prodStockSvc.save(scope.prodStock);
                operation = "save";
            }
            result.then(function (data) {
                async = 1;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                convertSvc.updateCollection(scope.parents, scope.prodStock, operation, "ID");
                initialize();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteProdStock(sec) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = prodStockSvc.deleteProdStock(sec);
        result.then(function (data) {
            async = 1;
            curasync = 0;
            alert("ডাটা ডিলিট হয়েছে");
            initialize();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }
    function showReport() {
        var result = prodStockSvc.showReport(scope.riceReport);
    }

}]);