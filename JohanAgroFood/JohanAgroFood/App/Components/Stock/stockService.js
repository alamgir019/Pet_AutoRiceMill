app.service("stockService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getStock: getStock,
        save: save,
        edit: edit,
        deleteStock: deleteStock
    };
    return dataSvc;
    function getStock(stock) {
        try {
            return baseDataSvc.executeQuery('/Stock/GetStock', { stk: stock });
        } catch (e) {
            throw e;
        }
    }
    
    function edit(objStock) {
        try {
            return baseDataSvc.executeQuery('/Stock/EditStock', objStock);
        } catch (e) {
            throw e;
        }
    }
    function save(objStock) {
        try {
            return baseDataSvc.executeQuery('/Stock/SaveStock', objStock);
        } catch (e) {
            throw e;
        }
    }

    function deleteStock(stk) {
        return baseDataSvc.remove('/Stock/DeleteStock', stk, "ID");
    }
}]);