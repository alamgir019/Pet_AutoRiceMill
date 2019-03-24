app.controller("balanceTransferCtrl", ["$scope", "balanceTransferSvc","convertSvc",
    function (scope, balanceTransferSvc,convertSvc) {
        scope.balanceInfo = {};
        scope.balanceInfos = [];
        scope.parties = [];
        var async = 2;
        var curasync = 0;

        //methods
        //scope.edit = edit;
        scope.save = save;
        scope.delete = deleteUser;
        scope.edit = edit;
        init();

    //To Get All Records
        function init() {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        initialize();
        getBalanceInfos();
        getParties();
    }

    function initialize() {
        scope.balanceInfo = {};
        scope.isEdit = false;
        
    }

    function save() {
        if (!scope.balanceInfoForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = null;
        var operation = null;
        if (scope.isEdit) {
            operation = "edit";
            result = balanceTransferSvc.edit(scope.balanceInfo);
        }
        else {
            operation = "save";
            result = balanceTransferSvc.save(scope.balanceInfo);
        }
        result.then(function () {
            async = 1;
            curasync = 0;
            alert("ডাটা সেভ হয়েছে");
            getBalanceInfos();
            initialize();
        }, function (e) {
            alert(e);
        });
    }
    function getBalanceInfos() {
        var result = balanceTransferSvc.getBalanceInfo();
        result.then(function(data) {
            scope.balanceInfos = data;
            scope.balanceInfos.forEach(function(r) {
                r.date = convertSvc.toDate(r.date);
                r.amt = convertSvc.ConvtEngToBang(r.amount);
                r.openingBalance = convertSvc.ConvtEngToBang(r.openingBalance);
            });
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        },function(e) {
                alert(e);
        });
    }
  
    function getParties() {
        var result = balanceTransferSvc.getParty();
        result.then(function(data) {
            scope.parties = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        },function(e) {
            alert(e);
        });
    }
    function edit(row) {
        scope.isEdit = true;
        scope.balanceInfo = row;
        scope.balanceInfo.amount = convertSvc.ConvtEngToBang(scope.balanceInfo.amount);
    }
    function deleteUser(row) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = balanceTransferSvc.deleteUser(row);
        result.then(function (data) {
            alert("ডাটা ডিলিট হয়েছে");
            convertSvc.updateCollection(scope.balanceInfos, row, "delete", "ID");
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }






    }]);
