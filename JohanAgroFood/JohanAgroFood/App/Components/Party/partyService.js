app.service("partyService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getParty: getParty,
        save: save,
        edit: edit,
        deleteUser: deleteUser,
        getDistrict: getDistrict
        
    };
    return dataSvc;

    function getParty() {
        try {
            return baseDataSvc.executeQuery('/Party/GetParty', {});
        } catch (e) {
            throw e;
        }
    }
    function getDistrict() {
        try {
            return baseDataSvc.executeQuery('/Party/GetDistrict', {});
        } catch (e) {
            throw e;
        }
    }
    function save(objParty) {
        try {
            return baseDataSvc.executeQuery('/Party/SaveParty', objParty);
        } catch (e) {
            throw e;
        }
    }
    function edit(objEditParty) {
        try{
            return baseDataSvc.executeQuery('/Party/EditParty',objEditParty);
        } catch (e) {
            throw e;
        }
    }
    function deleteUser(objParty) {
        try {
            return baseDataSvc.remove('/Party/Delete', objParty, "ID");
        } catch (e) {
            throw e;
        }
    }
}]);