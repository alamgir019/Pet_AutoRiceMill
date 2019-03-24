app.controller("reportCtrl", ["$scope","reportSvc","commonSvc","$q","$filter",
    function (scope, reportSvc, commonSvc, $q, $filter) {
        scope.dailySellReport = {};
        scope.consumptionRpt = {};
        scope.hollarRpt = {};
        scope.stockReport = {};
        scope.showReport = showReport;
        scope.paddys = [];
        scope.stocks = [];;
        init();
        
        function init()
        {
            scope.product = [
            { productName: "প্রোডাক্ট", ID: 1 },
            { productName: "তুষ", ID: 2 }
            ];
            getSectors();
            getPaddys();
            getStocks();

        }
        function getStocks()
        {
            var result = commonSvc.getAllStock();
            result.then(function (data) {
                scope.stocks = data;
            });
        }

        function getPaddys()
        {
            $q.all([commonSvc.getProductByName("ধান"), commonSvc.getAllProduct()]).then(function (result) {
                var pid = result[0].ID;
                scope.paddys = $filter('filter')(result[1], { parentId: pid });
            });
        }

        function getSectors() {
            var result = commonSvc.getAllSector();
            result.then(function (data) {
                scope.sectors = data;
                scope.sectors.push({ID:0,elementName:"সকল"});
            }, function (e) {
                alert(e);
            });
        }
        function showReport() {
            var result = reportSvc.showReport(scope.dailySellReport);
        }
}])