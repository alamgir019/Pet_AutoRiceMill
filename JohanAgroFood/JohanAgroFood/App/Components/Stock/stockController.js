app.controller("stockController", ["$scope", "stockService", function (scope, stockService) {
    scope.stock = null;
    scope.stocks = [];

    //methods
    scope.edit = edit;
    scope.save = save;
    scope.delete = deleteStock;


    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        getStock();
        initialize();
    }
    function initialize()
    {
        scope.stock = {};
    }

    function getStock() {
        var result = stockService.getStock(null);
        result.then(function (data) {
            scope.stocks = data; curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function edit(stk) {
        scope.isEdit = true;
        scope.stock = stk;
    }

    function save() {
        if (!scope.stockForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result=null;
        var operation = "save";
        if (scope.isEdit) {
            operation="edit"
            result = stockService.edit(scope.stock);
        }
        else {
            result = stockService.save(scope.stock);
        }
        result.then(function (data) {
            alert("ডাটা সেভ হয়েছে");
            getStock();
            initialize();
        }, function (e) {
            alert(e);
        });
    }

    function deleteStock(stk) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = stockService.deleteStock(stk);
        result.then(function (data) {
            alert("ডাটা ডিলিট হয়েছে");
            getStock();
            initilize();
        }, function (e) {
            alert(e);
        });
    }
    
}]);