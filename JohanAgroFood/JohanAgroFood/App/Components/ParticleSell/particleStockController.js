app.controller("particleStockCtrl", ["$scope", "particleStockSvc", "convertSvc", "productService",
    function (scope, particleStockSvc, convertSvc, productSvc) {
        scope.particleStock = {};
        scope.particleGeneral = {};
    scope.submitted = false;
    scope.particleStocks = [];
    scope.stocks = [];
    scope.particles = [];
    scope.isEdit = null;
    var async = 3;
    var curasync = 0;

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.delete = deleteParticleStock;
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
        getProduct();
        getStock();
        initialize();
    }

    function initialize() {
        scope.particleStock = {};
        scope.isEdit = false;
        getParticleStocks();

    }

    function getProduct() {
        var particle = { parentId: 0 };
        var result = productSvc.getProduct(particle);
        result.then(function (data) {
            scope.particles = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function getStock() {
        var result = particleStockSvc.getStock();
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

    function getParticleStocks() {
        var result = particleStockSvc.getParticleStock();
        result.then(function (data) {
            scope.particleStocks = data;
            scope.particleStocks.forEach(function (con) {
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
        scope.particleStock = inc;
        //scope.particleStock.date = convertSvc.toDate(inc.date);

        scope.sector.ID = inc.srcDescId;
        scope.sector.elementName = inc.sourceName;
    }

    function save() {
        scope.submitted = true;
        if (scope.particleStockForm.$valid) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = null;
            var operation = null;
            if (scope.isEdit) {
                //scope.particleStock.sourceName = "";
                result = particleStockSvc.edit(scope.particleStock);
                operation = "save";
            }
            else {
                result = particleStockSvc.save(scope.particleStock);
                operation = "edit";
            }
            result.then(function (data) {
                async = 1;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                convertSvc.updateCollection(scope.particleStocks, scope.particleStock, operation, "ID");
                initialize();
            }, function (e) {
                alert(e);
            });
        }
    }

    function deleteParticleStock(sec) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = particleStockSvc.deleteParticleStock(sec);
        result.then(function (data) {
            async = 1;
            curasync = 0;
            alert("Deleted successfully");
            initialize();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }

}]);