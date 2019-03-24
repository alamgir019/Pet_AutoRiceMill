app.controller("employeeCtrl", ["$scope", "employeeSvc", "convertSvc", "$filter", //"$timeout", "commonSvc",   "$q",
    function (scope, employeeSvc, convertSvc, $filter) {
    scope.employee = {};
    scope.employees = [];
    scope.salary={};
    scope.months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    scope.years = [2015,2016,2017,2018];
    scope.isEdit = null;

    var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ';
    scope.gridOptions = {
        data: 'employees',
        columnDefs: [
          { field: 'empName', displayName: 'নাম' },
          { field: 'address', displayName: 'ঠিকানা' },
          { field: 'designation', displayName: 'পদবী' },
          { field: 'contact', displayName: 'মোবাইল' },
          { field: 'joiningDate', displayName: 'যোগদানের তারিখ' },
          { field: 'crntSalary', displayName: 'বর্তমান বেতন' },
          { field: 'isSelect', displayName: '', cellTemplate: templateView }
        ],
        filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
        enableFiltering: true
    };



    var templateSal = '<a href="#" ng-click="grid.appScope.editSalary(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ';
    scope.gridOptionsSalary = {
        data: 'salaries',
        columnDefs: [
          { field: 'date', displayName: 'তারিখ' },
          { field: 'month', displayName: 'মাস' },
          { field: 'year', displayName: 'বছর' },
          { field: 'pAmount', displayName: 'প্রদানকৃত পরিমাণ' },
          { field: 'isSelect', displayName: '', cellTemplate: templateSal }
        ],
        filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
        enableFiltering: true
    };


    init();

    //methods
    scope.save = save;
    scope.edit = edit;
    scope.getEmpSal = getEmpSal;
    scope.saveSalary = saveSalary;
    scope.editSalary = editSalary;

    //To Get All Records  
    function init() {
        initialize();
        getAllEmployee();
    }
    function initialize() {
        scope.employee = {};
        scope.isEdit = false;

        scope.salary = {};
        scope.salary.year = 2016;
    }
    function getAllEmployee()
    {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = employeeSvc.getAllEmployee();
        result.then(function (data) {
            scope.employees = data;
            scope.employees.forEach(function (r) {
                //r.cntact = convertSvc.ConvtEngToBang(r.contact);
                r.crntSalary = convertSvc.ConvtEngToBang(r.currentSalary);
                r.joiningDate = convertSvc.toDate(r.joiningDate);
            });
            $('#myModal').modal('hide');
        }, function (e) {
            alert("ভূল হয়েছে");
        });
    }
    function edit(row) {
        scope.isEdit = true;
        scope.employee = row;

    }
    function save() {
        if (!scope.empForm.$valid) {
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
            result = employeeSvc.edit(scope.employee);
        }
        else {
            operation = "save";

            result = employeeSvc.save(scope.employee);
        }
        result.then(function (data) {
            if (data) {
                alert("ডাটা সেভ হয়েছে");
                scope.employee.ID = data.ID;
                convertSvc.updateCollection(scope.employees, scope.employee, operation, "ID");
                initialize();
            }
            else {
                alert("ডাটা সেভ হয়নি");
            }
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }


    function getEmpSal()
    {
        var selectedemp = $filter('filter')(scope.employees, { ID: scope.salary.empId });
        scope.salary.curSalary = selectedemp[0].currentSalary;
        scope.salary.curSal = convertSvc.ConvtEngToBang(scope.salary.curSalary);
        if (scope.salary.month && scope.salary.year) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = employeeSvc.getEmpSal(scope.salary);
            result.then(function (data) {
                scope.salaries = data;
                scope.salaries.forEach(function (r) {
                    r.pAmount = convertSvc.ConvtEngToBang(r.paidAmount);
                    r.date = convertSvc.toDate(r.date);
                });
                $('#myModal').modal('hide');
            }, function (e) {
                alert("ভূল হয়েছে");
            });
        }
        else {
            alert("প্রথমে মাস এবং বছর বাছাই করুন");
        }
    }

    function editSalary(row) {
        scope.isEdit = true;
        scope.salary = row;
        var selectedemp = $filter('filter')(scope.employees, { ID: scope.salary.empId });
        scope.salary.curSalary = selectedemp[0].currentSalary;
        scope.salary.curSal = convertSvc.ConvtEngToBang(scope.salary.curSalary);
    }
    function saveSalary()
    {
        if (!scope.salryForm.$valid) {
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
            result = employeeSvc.editSalary(scope.salary);
        }
        else {
            operation = "save";

            result = employeeSvc.saveSalary(scope.salary);
        }
        result.then(function (data) {
            if (data) {
                alert("ডাটা সেভ হয়েছে");
                scope.salary.ID = data.ID;
                convertSvc.updateCollection(scope.salaries, scope.salary, operation, "ID");
                initialize();
            }
            else {
                alert("ডাটা সেভ হয়নি");
            }
            $('#myModal').modal('hide');
        }, function (e) {
            alert(e);
        });
    }
}]);