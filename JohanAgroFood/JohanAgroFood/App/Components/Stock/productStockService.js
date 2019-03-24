app.service("prodStockSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getProdStocks: getProdStocks,
        getStock:getStock,
        save: save,
        edit: edit,
        deleteProdStock: deleteProdStock,
        showReport: showReport
    };
    return dataSvc;
    
    function deleteProdStock(objProdStock) {
        try {
            return baseDataSvc.executeQuery('/Stock/DeleteProdStock', { prodStk: objProdStock });
        } catch (e) {
            throw e;
        }
    }

    function edit(objProdStock)
    {
        try {
            return baseDataSvc.executeQuery('/Stock/EditProdStock', { prodStk: objProdStock });
        } catch (e) {
            throw e;
        }
    }

    function getStock() {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetAllStock', {});
        } catch (e) {
            throw e;
        }
    }

    function getProdStocks() {
        try {
            return baseDataSvc.executeQuery('/Stock/GetProdStocks', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objProdStock) {
        try {
            return baseDataSvc.executeQuery('/Stock/SaveProdStock', objProdStock);
        } catch (e) {
            throw e;
        }
    }
    function showReport(stockRpt) {
        try {
            return baseDataSvc.save('/Stock/GenerateAndDisplayReport');
            //return baseDataSvc.save('/Stock/Preview', stockRpt);
        } catch (e) {
            throw e;
        }
    }
}]);