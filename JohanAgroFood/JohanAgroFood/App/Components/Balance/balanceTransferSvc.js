app.service("balanceTransferSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    var dataSvc = {
        save: save,
        edit: edit,
        deleteUser: deleteUser,
        getBalanceInfo: getBalanceInfo,
        getParty: getParty
    };
    return dataSvc;
    function save(objBalance) {
        try {
            return baseDataSvc.executeQuery('/BalanceTransfer/Save', objBalance);
        } catch (e) {
            throw e;
        }
    }
    function edit(objBalance) {
        try {
            return baseDataSvc.executeQuery('/BalanceTransfer/Edit', objBalance);
        } catch (e) {
            throw e;
        }
    }
    function deleteUser(objBalance) {
        try {
            return baseDataSvc.remove('/BalanceTransfer/Delete', objBalance, "ID");
        } catch (e) {
            throw e;
        }
    }
    function getBalanceInfo() {
        try {
            return baseDataSvc.executeQuery('/BalanceTransfer/GetBalanceInfo', {});
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