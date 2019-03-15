app.controller("userController", ["$scope", "userService","convertSvc",
    function (scope, userService, convertSvc) {
    scope.user = null;
    scope.users = [];
    
    // methods

    scope.Save = save;
    scope.edit = edit;
    scope.delete=deleteUser;

    init();
    //GetUser();
    //GetAll();


    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        initialize();
        getUsers();
    }

    function initialize()
    {
        scope.user = {};
        scope.isEdit = false;
    }

    function getUsers() {
        var Data = userService.getUser();
        Data.then(function (data) {
            scope.users = data;
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
    function edit(row) {
        scope.isEdit = true;
        scope.user = row;
        
    }
   
    function save() {
        if (!scope.userForm.$valid) {
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
            result = userService.edit(scope.user);
        }
        else {
            operation = "save";
            var result = userService.saveUser(scope.user);
        }
            result.then(function (data) {
                alert("ডাটা সেভ হয়েছে");
                getUsers();
                initialize();
            }, function (e) {
                alert(e);
        });
    }
    function deleteUser(row) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = userService.deleteUser(row);
        result.then(function (data) {
            alert("ডাটা ডিলিট হয়েছে");
            convertSvc.updateCollection(scope.users, row, "delete", "ID");
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }

       
}]);