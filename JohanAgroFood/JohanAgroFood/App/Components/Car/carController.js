app.controller("carCtrl", ["$scope", "carSvc", "convertSvc",
    function (scope, carSvc, convertSvc) {
        scope.car = {};
        
        scope.submitted = false;
        scope.cars = [];
       
        var async = 1;
        var curasync = 0;

        //methods
        scope.save = save;
        scope.edit = edit;
        scope.delete = deletecar;
        init();

        

        //To Get All Records  
        function init() {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
           
            getCars();
            initialize();
        }

        function initialize() {
            scope.car = {};
            scope.isEdit = false;
            getCars();

        }

        
        function getCars() {
            var result = carSvc.getCar();
            result.then(function (data) {
                scope.cars = data;
                scope.cars.forEach(function (con) {
                    con.carWeightDate = convertSvc.toDate(con.carWeightDate);
                    con.carN = convertSvc.ConvtEngToBang(con.carNo);
                    con.firsttweight = convertSvc.ConvtEngToBang(con.firstTimeWeight);
                    con.secondtweight = convertSvc.ConvtEngToBang(con.secondTimeWeight);
                    con.expend = convertSvc.ConvtEngToBang(con.expenditure);
                    con.amt = convertSvc.ConvtEngToBang(con.amount);
                });
                curasync++;
                if (curasync == async) {
                    $('#myModal').modal('hide');
                }
            }, function (e) {
                alert(e);
            });
        }

        
        function edit(inc) {
            scope.isEdit = true;
            scope.car = inc;
           
        }

        function save() {
            scope.submitted = true;
            if (scope.carForm.$valid) {
                $('#myModal').modal({
                    backdrop: 'static',
                    keyboard: false,
                    show: true
                });
                var result = null;
                var operation = null;
                if (scope.isEdit) {
                    
                    result = carSvc.edit(scope.car);
                    operation = "edit";
                }
                else {
                    result = carSvc.save(scope.car);
                    operation = "save";
                }
                result.then(function (data) {
                    async = 1;
                    curasync = 0;
                    alert("ডাটা সেভ হয়েছে");
                    convertSvc.updateCollection(scope.cars, scope.car, operation, "ID");
                    initialize();
                }, function (e) {
                    alert(e);
                });
            }
        }

        function deletecar(sec) {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
            var result = carSvc.deletecar(sec.ID);
            result.then(function (data) {
                async = 1;
                curasync = 0;
                alert("Deleted successfully");
                initialize();
            }, function () {
                alert('ডিলিট হয়নি। ');
            });
        }

    }]);