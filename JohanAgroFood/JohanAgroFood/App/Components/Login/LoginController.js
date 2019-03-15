app.controller("AngularCtrlLogin", function ($scope, angularServiceLogin) {
   
    $scope.ShowAll = function () {
        GetAll();
    }

    $scope.SearchById = function () {
        var empID = $scope.empid;

        var Data = angularService.getEmpById(empID);
        Data.then(function (emp) {
            $scope.employees = emp.data;
        }, function () {
            alert('ডাটা পাওয়া যায়নি।');
        });
    }

    $scope.edit = function (employee) {

        $scope.ID = employee.ID;
        $scope.FirstName = employee.FirstName;
        $scope.LastName = employee.LastName;
        $scope.UserName = employee.UserName;
        $scope.Password = employee.Password;
        $scope.Operation = "Update";
        $scope.divEmpModification = true;
    }

    $scope.add = function () {

        $scope.ID = "";
        $scope.FirstName = "";
        $scope.LastName = "";
        $scope.UserName = "";
        $scope.Password = "";
        $scope.Operation = "Add";
        $scope.divEmpModification = true;
    }

    $scope.Save = function () {
        var Employee = {
            FirstName: $scope.FirstName,
            LastName: $scope.LastName,
            UserName: $scope.UserName,
            Password: $scope.Password
        };
        var Operation = $scope.Operation;

        if (Operation == "Update") {
            Employee.ID = $scope.ID;
            var getMSG = angularService.update(Employee);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.divEmpModification = false;
            }, function () {
                alert('আপডেট হয়নি। ');
            });
        }
        else {
            var getMSG = angularService.Add(Employee);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.divEmpModification = false;
            }, function () {
                alert('Insert Error');
            });
        }
    }

    $scope.delete = function (employee) {
        var getMSG = angularService.Delete(employee.ID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('ডিলিট হয়নি। ');
        });
    }
});