app.service("loanerService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getLoaner: getLoaner,
        save: save
    };
    return dataSvc;

    function getLoaner() {
        try {
            return baseDataSvc.executeQuery('/Loaner/GetLoaner', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objLoaner) {
        try {
            return baseDataSvc.executeQuery('/Loaner/SaveLoaner', objLoaner);
        } catch (e) {
            throw e;
        }
    }
}]);