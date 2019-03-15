app.service("employeeSvc", ["baseDataSvc", function (baseDataSvc) {
    //service instant
    var dataSvc = {
        save: save,
        getAllEmployee: getAllEmployee,
        edit: edit,
        getEmpSal: getEmpSal,
        saveSalary: saveSalary,
        editSalary: editSalary
    };
    return dataSvc;
    
    function getEmpSal(salary)
    {
        try {
            return baseDataSvc.executeQuery('/Employee/GetEmpSal', {salary:salary});
        } catch (e) {
            throw e;
        }
    }

    function save(employee) {
        try {
            return baseDataSvc.executeQuery('/Employee/SaveEmp', employee);
        } catch (e) {
            throw e;
        }
    }
    function getAllEmployee() {
        try {
            return baseDataSvc.executeQuery('/Employee/GetAllEmployee', {});
        } catch (e) {
            throw e;
        }
    }
    function edit(employee) {
        try {
            return baseDataSvc.save('/Employee/EditEmp', employee);
        } catch (e) {
            throw e;
        }
    }

    function saveSalary(salary) {
        try {
            return baseDataSvc.executeQuery('/Employee/SaveSalary', salary);
        } catch (e) {
            throw e;
        }
    }
    function editSalary(salary) {
        try {
            return baseDataSvc.save('/Employee/EditSalary', salary);
        } catch (e) {
            throw e;
        }
    }

}]);