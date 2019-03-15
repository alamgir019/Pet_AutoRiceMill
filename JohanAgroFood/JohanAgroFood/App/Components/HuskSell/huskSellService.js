app.service("huskSellService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getHuskSellInfo: getHuskSellInfo,
        getStock: getStock,
        deleteSell: deleteSell,
        edit: edit,
        save: save,
        showReport: showReport,
        loadHusk: loadHusk
    };
    return dataSvc;

    function getHuskSellInfo() {
        try {
            return baseDataSvc.executeQuery('/HuskSell/GetHuskInfo', {});
        } catch (e) {
            throw e;
        }
    }
    function edit(objHuskInfo) {
        try {
            return baseDataSvc.save('/HuskSell/EditHuskInfo', objHuskInfo);
        } catch (e) {
            throw e;
        }
    }
    function getStock() {
        try {
            return baseDataSvc.executeQuery('/RiceSell/GetStock', {});
        } catch (e) {
            throw e;
        }
    }
    function loadHusk(stockId) {
        try {
            return baseDataSvc.executeQuery('/HuskSell/LoadHusk', { StockId: stockId });
        } catch (e) {
            throw e;
        }
    }
    function showReport(huskRpt) {
        try {
            return baseDataSvc.save('/Husk/GenerateAndDisplayReport');
            //return baseDataSvc.save('/Husk/Preview', huskRpt);
        } catch (e) {
            throw e;
        }
    }
    function save(objHuskInfo) {
        try {
            return baseDataSvc.executeQuery('/HuskSell/SaveHuskInfo', objHuskInfo);
        } catch (e) {
            throw e;
        }
    }
    function deleteSell(objHuskInfo) {
        try {
            return baseDataSvc.remove('/HuskSell/DeleteHuskInfo', objHuskInfo, "ID");
        } catch (e) {
            throw e;
        }
    }
}]);