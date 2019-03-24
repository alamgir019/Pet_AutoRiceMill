app.controller("balanceTransferPaddyCtrl", ["$scope", "balanceTransferPaddySvc", "convertSvc",
    function (scope, balanceTransferPaddySvc, convertSvc) {
        scope.balancePaddyInfo = {};
        scope.balancePaddyInfos = [];
        scope.parties = [];
        var async = 4;
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
            getBalancePaddyInfos();
            getParties();
        }
        function initialize() {
            scope.balancePaddyInfo = {};
            scope.isEdit = false;
        }
        function save() {
            if (!scope.balancePaddyInfoForm.$valid) {
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
                result = balanceTransferPaddySvc.edit(scope.balancePaddyInfo);
            }
            else {
                operation = "save";
                result = balanceTransferPaddySvc.save(scope.balancePaddyInfo);
            }
            result.then(function () {
                async = 1;
                curasync = 0;
                alert("ডাটা সেভ হয়েছে");
                getBalancePaddyInfos();
                initialize();
            }, function (e) {
                alert(e);
            });
        }
        function getBalancePaddyInfos() {
            var result = balanceTransferPaddySvc.getBalancePaddyInfo();
            result.then(function (data) {
                scope.balancePaddyInfos = data;
                scope.balancePaddyInfos.forEach(function (r) {
                    r.date = convertSvc.toDate(r.date);
                    r.amt = convertSvc.ConvtEngToBang(r.amount);
                    r.openingBalance = convertSvc.ConvtEngToBang(r.openingBalance);
                });
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert(e);
            });
        }
        function getParties() {
            var result = balanceTransferPaddySvc.getParty();
            result.then(function (data) {
                scope.parties = data;
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert(e);
            });
        }
        function edit(row) {
            scope.isEdit = true;
            scope.balancePaddyInfo = row;
            scope.balancePaddyInfo.amount = convertSvc.ConvtEngToBang(scope.balancePaddyInfo.amount);
        }
        function deleteUser(row) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = balanceTransferPaddySvc.deleteUser(row);
            result.then(function (data) {
                alert("ডাটা ডিলিট হয়েছে");
                convertSvc.updateCollection(scope.balancePaddyInfos, row, "delete", "ID");
                $('#myModal').modal('hide');
            }, function (e) {
                alert(e);
            });
        }
    }]);
