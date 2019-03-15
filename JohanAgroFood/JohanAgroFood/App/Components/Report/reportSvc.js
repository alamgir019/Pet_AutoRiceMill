app.service("reportSvc", ["$http","baseDataSvc",
    function (http, baseDataSvc) {
        var dataSvc = {
            
            showReport: showReport
            
        };
        return dataSvc;
        function showReport(dailySellRpt) {
            try {
                return baseDataSvc.save('/Report/GenerateAndDisplayReport');
                //return baseDataSvc.save('/Report/Preview', dailySellRpt);
            } catch (e) {
                throw e;
            }
        }
}]);