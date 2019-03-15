app.controller("duePaymentCltr", ["$scope", "partyService","convertSvc","sectorService","duePaymentSvc",
function (scope, partyService, convertSvc, sectorService, duePaymentSvc) {
    scope.duePayment = {};
    //scope.duePayments = [];    
    scope.parties = [];
    scope.sectors = [];
    scope.duePaidInfos = [];
    var async = 3;
    var curasync = 0;

    scope.save = save;   
    scope.getDuePayment = getDuePayment;
    scope.getDuePaid = getDuePaid;
        //scope.searchDuePayment = searchDuePayment;
        //scope.selectSector = selectSector;
        init();


        function init() {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            //getDuePaymentInfos();
            initialize();
            getParties();
            getSectors();
        }

        function initialize() {
            getDuePaid();
            scope.duePayment = {};
            scope.sector = {};
        }
         
        function getDuePayment() {
            var result = duePaymentSvc.getDuePayment(scope.duePayment.partyId);
            result.then(function (data) {
                scope.duePayment.totalDue = convertSvc.ConvtEngToBang(data.openingBalance);
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert(e);
            });
        }
        function getDuePaid() {

            //$('#myModal').modal({
            //    backdrop: 'static',
            //    keyboard: false,
            //    show: true
            //});
            var result = duePaymentSvc.getDuePaid(scope.duePayment.date);
            result.then(function (data) {
                
                scope.duePaidInfos = data;
                scope.duePaidInfos.forEach(function (con) {
                    con.amount = convertSvc.ConvtEngToBang(con.amount);
                    
                });
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert("get duePaid error");
            });
        }
        //function selectSector() {
        //    scope.duePayment.srcDescId = scope.sector.ID;
        //    scope.duePayment.sourceName = scope.sector.elementCode;

        //}
        function getParties() {
            var result = partyService.getParty();
            result.then(function (data) {
                scope.parties = data;
                scope.parties.forEach(function (con) {
                    con.contact = convertSvc.ConvtEngToBang(con.contactNo);
                    con.cash = con.isCashParty ? "হ্যা" : "না";
                });
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert(e);
            });
        }
        function getSectors() {
            var result = sectorService.getSector();
            result.then(function (data) {
                scope.sectors = data;
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert(e);
            });
        }
        function save() {
            if (!scope.duePaymentForm.$valid) {
                return;
            }
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var retVal = confirm(scope.duePayment.partyName + " এর বাকির বিপরীতে আপনি কি " + convertSvc.ConvtEngToBang(scope.duePayment.amount) + " টাকা পরিশোধ করতে চান ?");
            if (retVal == true) {


                scope.duePayment.isDue = 1;
                var result = duePaymentSvc.save(scope.duePayment);
                //scope.sector = {};
                result.then(function () {
                    async = 2;
                    curasync = 0;
                    alert("ডাটা সেভ হয়েছে");
                    getDuePayment();
                    initialize();
                }, function (e) {
                    alert("due payment save error");
                });
            }

            else {
                alert("ডাটা সেভ বাতিল করা হইল");
                $('#myModal').modal('hide');
            }
        }
        

    }]);