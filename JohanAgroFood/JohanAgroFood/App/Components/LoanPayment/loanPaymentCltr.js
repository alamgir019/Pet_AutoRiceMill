app.controller("loanPaymentCltr", ["$scope", "convertSvc", "loanPaymentSvc","$q","commonSvc","$filter",
function (scope, convertSvc, loanPaymentSvc, $q, commonSvc, $filter) {
    scope.loanPayment = {};
   
    scope.parties = [];
    scope.sectors = [];
   
    scope.save = save;

    scope.getloanPayment = getloanPayment;
    
    init();


    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $q.all([commonSvc.getProductByName("ধান"), commonSvc.getAllParty()]).then(function (result) {
            var pids = [result[0].ID];
            scope.parties = $filter('filterById')(result[1], { pIds: pids });
            $('#myModal').modal('hide');
        });
        initialize();
        //getParties();
        
    }

    function initialize() {
        scope.loanPayment = {};
        
    }

    function getloanPayment() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = loanPaymentSvc.getLoanPayment(scope.loanPayment.partyId);
        result.then(function (data) {
            scope.loanPayment.totalloan = convertSvc.ConvtEngToBang(data.openingBalance);
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
    
    //function getParties() {
    //    var result = partyService.getParty();
    //    result.then(function (data) {
    //        scope.parties = data;
    //        scope.parties.forEach(function (con) {
    //            con.contact = convertSvc.ConvtEngToBang(con.contactNo);
    //            con.cash = con.isCashParty ? "হ্যা" : "না";
    //        });
    //    }, function (e) {
    //        alert(e);
    //    });
    //}
    
    function save() {
        if (!scope.loanPaymentForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = loanPaymentSvc.save(scope.loanPayment);
        result.then(function () {
            alert("ডাটা সেভ হয়েছে");
            getloanPayment();
            initialize();
        }, function (e) {
            alert(e);
        });
    }


}]);