app.service("districtSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getDistrict: getDistrict,
        save: save,
        edit: edit,
        deleteDistrict: deleteDistrict
    };
    return dataSvc;

    function deleteDistrict(dst) {
        try {
            return baseDataSvc.executeQuery('/Sector/DeleteSector', { sector: dst });
        } catch (e) {
            throw e;
        }
    }

    function edit(dst) {
        try {
            return baseDataSvc.executeQuery('/Sector/EditSector', { sector: dst });
        } catch (e) {
            throw e;
        }
    }

    function getDistrict() {
        try {
            var code=1;//1 for district
            return baseDataSvc.executeQuery('/Sector/GetSector', {elemCode: code});
        } catch (e) {
            throw e;
        }
    }

    function save(objDistrict) {
        try {
            return baseDataSvc.executeQuery('/Sector/SaveSector', objDistrict);
        } catch (e) {
            throw e;
        }
    }
}]);