app.service("sectorService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getSector: getSector,
        save: save,
        edit: edit,
        deleteSector: deleteSector
    };
    return dataSvc;

    function deleteSector(sec) {
        try {
            return baseDataSvc.executeQuery('/Sector/DeleteSector', { sector: sec });
        } catch (e) {
            throw e;
        }
    }

    function edit(sec)
    {
        try {
            return baseDataSvc.executeQuery('/Sector/EditSector', {sector:sec});
        } catch (e) {
            throw e;
        }
    }

    function getSector() {
        try {
            var code = 3; //3 for sector
            return baseDataSvc.executeQuery('/Sector/GetSector', { elemCode: code });
        } catch (e) {
            throw e;
        }
    }

    function save(objSector) {
        try {
            return baseDataSvc.executeQuery('/Sector/SaveSector', objSector);
        } catch (e) {
            throw e;
        }
    }
}]);