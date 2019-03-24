app.controller("riceSellController", ["$scope", "riceSellService", "convertSvc", "commonSvc", "$filter", "$q",
   function (scope, riceSellService, convertSvc, commonSvc, $filter, $q) {
       scope.riceInfo = {};
       scope.riceReport = {};
       scope.isCash = null;
       scope.isEdit = null;
       scope.rices = [];
       scope.stocks = [];
       scope.riceInfos = [];
       scope.parties = [];
       var async = 4;
       var curasync = 0;
       //var template = '<div><input type="text" ng-change="grid.appScope.selectSell(row.entity)" /></div>';

       var templateView = '<a href="#" ng-click="grid.appScope.edit(row.entity)"><span class="glyphicon glyphicon-pencil">পরিবর্তন</span></a> ' +
       '<a href="#" ng-click="grid.appScope.deleteSell(row.entity)"><span class="glyphicon glyphicon-trash">মূছুন</span></a>';
       scope.gridOptions = {
           data: 'riceInfos',
           columnDefs: [
             //{ field: 'ID', displayName: 'ID' },
             { field: 'date', displayName: 'তারিখ' },
             { field: 'noBag', displayName: 'ব্যাগের সংখ্যা' },
             { field: 'partyName', displayName: 'পার্টির নাম' },
             { field: 'productName', displayName: 'চালের নাম' },
             { field: 'stockName', displayName: 'স্টক নাম' },
             { field: 'qty', displayName: 'চালের পরিমাণ(কেজি)' },
             //{ field: 'unit', displayName: 'একক' },
             { field: 'uPrice', displayName: 'একক মূল্য' },
             { field: 'isSelect', displayName: '', cellTemplate: templateView }
           ],
           filterOptions: { filterText: '<displayName>:<literal>', useExternalFilter: false },
           enableFiltering: true
       };


       scope.save = save;
       scope.edit = edit;
       scope.deleteSell = deleteSell;
       scope.getTotal = getTotal;
       scope.showReport = showReport;

       scope.cashParty = cashParty;

       init();

       //To Get All Records  
       function init() {
           $('#myModal').modal({
               backdrop: 'static',
               keyboard: false,
               show: true
           });
           $q.all([commonSvc.getProductByName("চাল"), commonSvc.getProductByName("ক্ষুদ"), commonSvc.getAllParty()]).then(function (result) {
               var pids = [result[0].ID, result[1].ID];
               scope.parties = $filter('filterById')(result[2], { pIds: pids });
               curasync++;
               if (curasync==async) {
                   $('#myModal').modal('hide');
               }
           });
           var path = window.location.pathname.split('/');
           if (path[2] != "rptRiceInfo") {
               getRice();
               getStock();
               getRiceSellInfo();
               initialize();
           }
           else {
               var async = 1;
           }
       }

       function getProductByName() {
           var result = commonSvc.getProductByName("চাল");
           result.then(function (data) {
               scope.currentProduct = data;
           }, function (ex) {
               alert(ex.Message);
           });
       }

       function initialize() {
           scope.isEdit = false;
           scope.riceInfo = {};
           scope.riceInfo.isMon = false;
           scope.isCash = false;
       }
       function getRice() {
           var result = commonSvc.getAllProduct();
           result.then(function (data) {
               scope.rices = $filter('filter')(data, { parentId: 2 });
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
           scope.riceInfo = row;

       }

       function getTotal() {
           if (scope.riceInfo.transportCostInclude) {
               if (scope.riceInfo.isMon) {
                   scope.riceInfo.unit = 2;
                   var mon = scope.riceInfo.quantity / 40;
                   scope.riceInfo.totPrice = mon * scope.riceInfo.unitPrice * scope.riceInfo.noOfBag + scope.riceInfo.transportCost;
               }
               else {
                   scope.riceInfo.unit = 1;
                   scope.riceInfo.totPrice = scope.riceInfo.quantity * scope.riceInfo.unitPrice * scope.riceInfo.noOfBag + parseInt(scope.riceInfo.transportCost);
               }
           }
           else {
               if (scope.riceInfo.isMon) {
                   scope.riceInfo.unit = 2;
                   var mon = scope.riceInfo.quantity / 40;
                   scope.riceInfo.totPrice = mon * scope.riceInfo.unitPrice * scope.riceInfo.noOfBag;
               }
               else {
                   scope.riceInfo.unit = 1;
                   scope.riceInfo.totPrice = scope.riceInfo.quantity * scope.riceInfo.unitPrice * scope.riceInfo.noOfBag;
               }
           }
           //scope.riceInfo.totalPr = scope.riceInfo.totPrice;
           scope.riceInfo.totalpr = convertSvc.ConvtEngToBang(scope.riceInfo.totPrice);
       }

       function cashParty() {
           if (scope.isCash) {
               scope.riceInfo.partyId = 3010;
               scope.riceInfo.partyName = "নগদ";
           }
       }

       function deleteSell(row) {
           $('#myModal').modal({
               backdrop: 'static',
               keyboard: false,
               show: true
           });
           var result = riceSellService.deleteSell(row);
           result.then(function (data) {
               alert("ডাটা ডিলিট হয়েছে");
               convertSvc.updateCollection(scope.riceInfos, row, "delete", "ID");
               $('#myModal').modal('hide');
           }, function (e) {
               alert(e);
               $('#myModal').modal('hide');
           });
       }

       //function getParty() {
       //    var result = commonSvc.getAllParty();
       //    result.then(function (data) {
       //        scope.parties = data;
       //    }, function (e) {
       //        alert(e);
       //    });
       //}

       //function loadRice()
       //{

       //    var result = riceSellService.loadRice();
       //    result.then(function (data) {
       //        scope.rices = data;
       //    }, function (e) {
       //        alert(e);
       //    });
       //}

       function getStock() {
           var result = commonSvc.getAllStock();
           result.then(function (data) {
               scope.stocks = data;
               curasync++;
               if (curasync == async) {
                   $('#myModal').modal('hide');
               }
           }, function (e) {
               alert(e);
           });
       }

       function getRiceSellInfo() {
           var result = riceSellService.getRiceSellInfo();
           result.then(function (data) {
               scope.riceInfos = data;
               scope.riceInfos.forEach(function (r) {
                   if (r.unit == 2) {
                       r.isMon = true;
                   }
                   else {
                       r.isMon = false;
                   }
                   r.date = convertSvc.toDate(r.date);
                   r.qty = convertSvc.ConvtEngToBang(r.quantity);
                   r.uPrice = convertSvc.ConvtEngToBang(r.unitPrice);
                   r.noBag = convertSvc.ConvtEngToBang(r.noOfBag);
                   r.paidAmnt = convertSvc.ConvtEngToBang(r.paidAmount);
                   r.transCost = convertSvc.ConvtEngToBang(r.transportCost);
               });
               curasync++;
               if (curasync == async) {
                   $('#myModal').modal('hide');
               }
           }, function (e) {
               alert(e);
           });
       }

       function save() {
           if (!scope.riceInfoForm.$valid) {
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
               result = riceSellService.edit(scope.riceInfo);
           }
           else {
               operation = "save";
               result = riceSellService.save(scope.riceInfo);
           }
           result.then(function (data) {
               alert("ডাটা সেভ হয়েছে");
               scope.riceInfo.ID = data.ID;
               scope.riceInfo.incSrcId = data.incSrcId;
               convertSvc.updateCollection(scope.riceInfos, scope.riceInfo, operation, "ID");
               initialize();
               $('#myModal').modal('hide');
           }, function (e) {
               alert(e);
               $('#myModal').modal('hide');
           });
       }

       function showReport() {
           var result = riceSellService.showReport(scope.riceReport);
       }

   }]);