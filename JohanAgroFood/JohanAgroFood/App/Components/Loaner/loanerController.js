app.controller("loanerController", ["$scope", "loanerService", "convertSvc", function (scope, loanerService, convertSvc) {
    scope.loaner = {};
    scope.loaners = [];
    scope.isEdit = null;

    scope.save = save;
    init();

    //GetUser();
    //GetAll();


    //To Get All Records  
    function init() {
        getLoaners();
        initialize();
    }

    function initialize()
    {
        scope.loaner = {};
        scope.isEdit = false;
    }

    function getLoaners() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = loanerService.getLoaner();
        result.then(function (data) {
            scope.loaners = data;
            scope.loaners.forEach(function (con) {
                con.contact = convertSvc.ConvtEngToBang(con.contactNo);
                con.amnt = convertSvc.ConvtEngToBang(con.amount);
                con.intst = convertSvc.ConvtEngToBang(con.interest);
            });
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }

    scope.edit = edit;

    function edit(location) {
        $scope.LOCATIONID = location.LOCATIONID;
        $scope.LOCATIONCODE = location.LOCATIONCODE;
        $scope.LOCATIONNAME = location.LOCATIONNAME;
        $scope.DISTRICTID = location.DISTRICTID;
        $scope.Operation = "Update";
    }

    function save(loaner) {
        if (scope.loanerForm.$valid) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = loanerService.save(loaner);
            result.then(function (data) {
                alert("ডাটা সেভ হয়েছে");
                getLoaners();
                initialize();
            }, function (e) {
                alert(e);
            });
        }
    }
    scope.delete = deleteUser;

    function deleteUser(location) {
        var getMSG = userService.Delete(location.LOCATIONID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
            //CE-150328-ANIS
            $window.location.reload();
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }
    
}]);