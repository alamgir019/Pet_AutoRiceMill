app.service("duePaymentSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    var dataSvc = {
        save: save,
        getDuePayment: getDuePayment,
        getDuePaid: getDuePaid
        
    };
    return dataSvc;
    function save(objDuePayment) {
        try {
            return baseDataSvc.executeQuery('/DuePayment/Save', objDuePayment);
        } catch (e) {
            throw e;
        }
    }
    function getDuePayment(objPartyId) {
        try {
            return baseDataSvc.executeQuery('/DuePayment/GetDuePayment', { objPartyId: objPartyId });
        } catch (e) {
            throw e;
        }
    }
    function getDuePaid(objDate) {
        try {
            return baseDataSvc.executeQuery('/DuePayment/GetDuePaid', { objDate: objDate });
        } catch (e) {
            throw e;
        }
    }
}]);