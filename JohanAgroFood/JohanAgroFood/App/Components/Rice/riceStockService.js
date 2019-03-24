app.service("riceStockService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getRiceStock: getRiceStock,
        getStock:getStock,
        save: save,
        edit: edit,
        deleteRiceStock: deleteRiceStock
    };
    return dataSvc;

    function deleteRiceStock(rice) {
        try {
            return baseDataSvc.executeQuery('/Rice/DeleteRiceStock', { riceStock: rice });
        } catch (e) {
            throw e;
        }
    }

    function edit(rice)
    {
        try {
            return baseDataSvc.executeQuery('/Rice/EditRiceStock', { riceStock: rice });
        } catch (e) {
            throw e;
        }
    }

    function getStock() {
        try {
            return baseDataSvc.executeQuery('/Rice/GetStock', {});
        } catch (e) {
            throw e;
        }
    }

    function getRiceStock() {
        try {
            return baseDataSvc.executeQuery('/Rice/GetRiceStock', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objRiceStock) {
        try {
            return baseDataSvc.executeQuery('/Rice/SaveRiceStock', objRiceStock);
        } catch (e) {
            throw e;
        }
    }
}]);