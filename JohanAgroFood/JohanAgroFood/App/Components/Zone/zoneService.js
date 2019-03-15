app.service("zoneSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getZone: getZone,
        save: save,
        edit: edit,
        deleteZone: deleteZone
    };
    return dataSvc;

    function deleteZone(zon) {
        try {
            return baseDataSvc.executeQuery('/Sector/DeleteSector', { sector: zon });
        } catch (e) {
            throw e;
        }
    }

    function edit(zon) {
        try {
            return baseDataSvc.executeQuery('/Sector/EditSector', { sector: zon });
        } catch (e) {
            throw e;
        }
    }

    function getZone() {
        try {
            var code=4;//4 for zone
            return baseDataSvc.executeQuery('/Sector/GetSector', {elemCode: code});
        } catch (e) {
            throw e;
        }
    }

    function save(objZone) {
        try {
            return baseDataSvc.executeQuery('/Sector/SaveSector', objZone);
        } catch (e) {
            throw e;
        }
    }
}]);