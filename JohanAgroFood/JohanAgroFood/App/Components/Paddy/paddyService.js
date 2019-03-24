app.service("paddySvc", ["baseDataSvc", function (baseDataSvc) {
    //service instant
    var dataSvc = {
        save: save,
        getPaddyBuyInfo: getPaddyBuyInfo,
        edit: edit,
        deleteBuy: deleteBuy
    };
    return dataSvc;
    
    function save(objPaddy) {
        try {
            return baseDataSvc.executeQuery('/Paddy/SavePaddy', objPaddy);
        } catch (e) {
            throw e;
        }
    }
    function getPaddyBuyInfo() {
        try {
            return baseDataSvc.executeQuery('/Paddy/GetPaddyInfo', {});
        } catch (e) {
            throw e;
        }
    }
    function edit(objPaddyInfo) {
        try {
            return baseDataSvc.save('/Paddy/EditPaddyInfo', objPaddyInfo);
        } catch (e) {
            throw e;
        }
    }
    function deleteBuy(objPaddyInfo) {
        try {
            return baseDataSvc.remove('/Paddy/DeletePaddy', objPaddyInfo, "ID");
        } catch (e) {
            throw e;
        }
    }
}]);