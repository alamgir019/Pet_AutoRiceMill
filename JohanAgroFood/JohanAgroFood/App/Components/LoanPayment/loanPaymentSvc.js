app.service("loanPaymentSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    var dataSvc = {
        getLoanPayment: getLoanPayment,
        save:save
    };
    return dataSvc;
    function getLoanPayment(objPartyId) {
        try {
            return baseDataSvc.executeQuery('/LoanPayment/GetLoanPayment', { objPartyId: objPartyId });
        } catch (e) {
            throw e;
        }
    }
    function save(objLoanPayment) {
        try {
            return baseDataSvc.executeQuery('/LoanPayment/Save', objLoanPayment);
        } catch (e) {
            throw e;
        }
    }
}]);