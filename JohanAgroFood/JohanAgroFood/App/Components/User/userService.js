app.service("userService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getUser: getUser,
        saveUser: saveUser,
        edit: edit,
        deleteUser: deleteUser
    };
    return dataSvc;

    function getUser() {
        try {
            return baseDataSvc.executeQuery('/User/GetUser', {  });
        } catch (e) {
            throw e;
        }
    }

    function saveUser(objUser)
    {
        try {
            return baseDataSvc.executeQuery('/User/SaveUser', objUser);
        } catch (e) {
            throw e;
        }
    }
    function edit(objUser) {
        try{
            return baseDataSvc.executeQuery('/User/Edit', objUser);
        } catch (e) {
            throw e;
        }
    }
    function deleteUser(objUser) {
        try {
            return baseDataSvc.remove('/User/Delete', objUser, "ID");
        } catch (e) {
            throw e;
        }
    }
}]);