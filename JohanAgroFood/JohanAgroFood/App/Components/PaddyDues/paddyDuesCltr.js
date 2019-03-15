app.controller("paddyDuesCltr", ["$scope", "convertSvc", "paddyDuesSvc", "commonSvc", "$filter", "$q",
function (scope, convertSvc, paddyDuesSvc, commonSvc, $filter, $q) {
    scope.paddyDues = {};
    scope.bagReturn = {};
    scope.parties = [];
    //scope.sectors = [];

    scope.save = save;
    scope.getDues = getDues;
    scope.getRemainingBag = getRemainingBag;
    scope.saveBag = saveBag;
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
        //getSectors();
    }
    function initialize() {
        scope.paddyDues = {};
        //scope.sector = {};
        scope.bagReturn = {};
    }
    function getDues() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = paddyDuesSvc.getDuePayment(scope.bagReturn.partyId);
        result.then(function (data) {
            scope.duePayment.totalDue = convertSvc.ConvtEngToBang(data.openingBalance);
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
    function getRemainingBag() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = paddyDuesSvc.getRemainingBag(scope.bagReturn.partyId);
        result.then(function (data) {
            scope.bagReturn.bagDues = data.bagDues;
            scope.bagReturn.bagDue = convertSvc.ConvtEngToBang(data.bagDues);
            scope.bagReturn.priceDues = data.priceDues;
            scope.bagReturn.priceDue = convertSvc.ConvtEngToBang(data.priceDues);
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
    function saveBag()
    { 
        var conf = confirm("আপনি কি ব্যাগের তথ্য সেভ করতে চান ?");
        if (conf == false) return;
        if (!scope.bagReturnForm.$valid) {
            return;
        }
        if (scope.bagReturn.sentPrice > 0) {
            var bag = scope.bagReturn.comSentBag > 0 ? scope.bagReturn.comSentBag : 1;
            scope.bagReturn.priceDues -= scope.bagReturn.sentPrice * bag;
            scope.bagReturn.priceDue = convertSvc.ConvtEngToBang(scope.bagReturn.priceDues);
            scope.bagReturn.bagDues = 0;
        }
        else {
            scope.bagReturn.bagDues -= scope.bagReturn.comSentBag;
            scope.bagReturn.bagDue = convertSvc.ConvtEngToBang(scope.bagReturn.bagDues);
            scope.bagReturn.priceDues = 0;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = paddyDuesSvc.saveBag(scope.bagReturn);
        result.then(function () {
            alert("ডাটা সেভ হয়েছে");
            initialize();
            $('#myModal').modal('hide');
        }, function (e) {
            alert("ডাটা সেভ হয়নি");
        });
    }
    //function getSectors() {
    //    var result = sectorService.getSector();
    //    result.then(function (data) {
    //        scope.sectors = data;               
    //    }, function (e) {
    //        alert(e);
    //    });
    //}

    function save() {
        if (!scope.paddyDuseForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        scope.paddyDues.isDue = 1;
        var result = paddyDuesSvc.save(scope.paddyDues);
        result.then(function () {
            alert("ডাটা সেভ হয়েছে");
            getDues();
            //initialize();
        }, function (e) {
            alert(e);
        });
    }
    }]);