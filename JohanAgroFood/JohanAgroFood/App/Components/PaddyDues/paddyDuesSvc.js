app.service("paddyDuesSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    var dataSvc = {
        save: save,
        saveBag:saveBag,
        getDues: getDues,
        getRemainingBag: getRemainingBag        
    };
    return dataSvc;
    
    function getRemainingBag(partyId)
    {
        try {
            return baseDataSvc.executeQuery('/PaddyDues/GetRemainingBag', { partyId: partyId });
        } catch (e) {
            throw e;
        }
    }
    function save(objDues) {
        try {
            return baseDataSvc.executeQuery('/PaddyDues/Save', objDues);
        } catch (e) {
            throw e;
        }
    }
    function getDues(partyId) {
        try {
            return baseDataSvc.executeQuery('/Paddydues/GetDues', { partyId: partyId });
        } catch (e) {
            throw e;
        }
    }
    function saveBag(objBag)
    {
        try {
            return baseDataSvc.executeQuery('/PaddyDues/SaveBag', objBag);
        } catch (e) {
            throw e;
        }
    }
}]);