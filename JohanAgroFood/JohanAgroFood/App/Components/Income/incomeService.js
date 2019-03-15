app.service("incomeService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getIncome: getIncome,
        save: save,
        edit: edit,
        deleteIncome: deleteIncome
    };
    return dataSvc;

    function deleteIncome(sec) {
        try {
            return baseDataSvc.executeQuery('/Income/DeleteIncome', { income: sec });
        } catch (e) {
            throw e;
        }
    }

    function edit(sec)
    {
        try {
            return baseDataSvc.executeQuery('/Income/EditIncome', {income:sec});
        } catch (e) {
            throw e;
        }
    }

    function getIncome() {
        try {
            return baseDataSvc.executeQuery('/Income/GetIncome', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objIncome) {
        try {
            return baseDataSvc.executeQuery('/Income/SaveIncome', objIncome);
        } catch (e) {
            throw e;
        }
    }
}]);