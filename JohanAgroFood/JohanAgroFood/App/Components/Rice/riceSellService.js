app.service("riceSellService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getRiceSellInfo: getRiceSellInfo,
        save: save,
        edit: edit,
        deleteSell: deleteSell,
        showReport: showReport,
        loadRice: loadRice
    };
    return dataSvc;

    function getRiceSellInfo() {
        try {
            return baseDataSvc.executeQuery('/Rice/GetRiceInfo', {});
        } catch (e) {
            throw e;
        }
    }

    function loadRice()
    {
        try {
            return baseDataSvc.executeQuery('/Rice/LoadRice', {});
        } catch (e) {
            throw e;
        }
    }
    
    function save(objRiceInfo) {
        try {
            return baseDataSvc.save('/Rice/SaveRiceInfo', objRiceInfo);
        } catch (e) {
            throw e;
        }
    }
    function edit(objRiceInfo) {
        try {
            return baseDataSvc.save('/Rice/EditRiceInfo', objRiceInfo);
        } catch (e) {
            throw e;
        }
    }
    function deleteSell(objRiceInfo) {
        try {
            return baseDataSvc.remove('/Rice/DeleteRiceInfo', objRiceInfo,"ID");
        } catch (e) {
            throw e;
        }
    }

    function showReport(riceRpt)
        {
            try {
                return baseDataSvc.save('/Rice/GenerateAndDisplayReport');
                //return baseDataSvc.save('/Rice/Preview', riceRpt);
            } catch (e) {
                throw e;
            }
        }
}]);