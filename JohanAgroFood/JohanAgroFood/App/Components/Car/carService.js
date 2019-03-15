app.service("carSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        
        getCar: getCar,
        save: save,
        edit: edit,
        deletecar: deletecar
    };
    return dataSvc;

    function deletecar(pk) {
        try {
            return baseDataSvc.executeQuery('/Car/DeleteCar', { pk: pk });
        } catch (e) {
            throw e;
        }
    }

    function edit(objCar) {
        try {
            return baseDataSvc.executeQuery('/Car/EditCar', { objCar: objCar });
        } catch (e) {
            throw e;
        }
    }

    function getCar() {
        try {
            return baseDataSvc.executeQuery('/Car/GetCar', {});
        } catch (e) {
            throw e;
        }
    }

    
    function save(objcar) {
        try {
            return baseDataSvc.executeQuery('/Car/SaveCar', objcar);
        } catch (e) {
            throw e;
        }
    }
}]);