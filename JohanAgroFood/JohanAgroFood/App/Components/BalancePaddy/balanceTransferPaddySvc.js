app.service("balanceTransferPaddySvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    var dataSvc = {
        save: save,
        edit: edit,
        deleteUser: deleteUser,
        getBalancePaddyInfo: getBalancePaddyInfo,
        getParty: getParty
    };
    return dataSvc;
    function save(objBalancePaddy) {
        try {
            return baseDataSvc.executeQuery('/BalanceTransferPaddy/Save', objBalancePaddy);
        } catch (e) {
            throw e;
        }
    }
    function edit(objBalancePaddy) {
        try {
            return baseDataSvc.executeQuery('/BalanceTransferPaddy/Edit', objBalancePaddy);
        } catch (e) {
            throw e;
        }
    }
    function deleteUser(objBalancePaddy) {
        try {
            return baseDataSvc.remove('/BalanceTransferPaddy/Delete', objBalancePaddy, "ID");
        } catch (e) {
            throw e;
        }
    }
    function getBalancePaddyInfo() {
        try {
            return baseDataSvc.executeQuery('/BalanceTransferPaddy/GetBalancePaddyInfo', {});
        } catch (e) {
            throw e;
        }
    }
    function getParty() {
        try {
            return baseDataSvc.executeQuery('/Party/GetParty', {});
        } catch (e) {
            throw e;
        }
    }
}]);