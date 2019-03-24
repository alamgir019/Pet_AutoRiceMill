app.service("consumptionService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getConsumption: getConsumption,
        save: save,
        edit: edit,
        deleteConsumption: deleteConsumption
    };
    return dataSvc;

    function deleteConsumption(sec) {
        try {
            return baseDataSvc.executeQuery('/Consumption/DeleteConsumption', { consumption: sec });
        } catch (e) {
            throw e;
        }
    }

    function edit(sec)
    {
        try {
            return baseDataSvc.executeQuery('/Consumption/EditConsumption', {consumption:sec});
        } catch (e) {
            throw e;
        }
    }

    function getConsumption() {
        try {
            return baseDataSvc.executeQuery('/Consumption/GetConsumption', {});
        } catch (e) {
            throw e;
        }
    }

    function save(objConsumption) {
        try {
            return baseDataSvc.executeQuery('/Consumption/SaveConsumption', objConsumption);
        } catch (e) {
            throw e;
        }
    }
}]);