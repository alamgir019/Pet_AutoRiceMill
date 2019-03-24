app.service("paddyTransferSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getSackWeights: getSackWeights,
        save: save,
        //edit: edit,
        //deleteCell: deleteCell,
        getPaddyTransferInfos: getPaddyTransferInfos
        

    };
    return dataSvc;
    function getSackWeights(productId, stockId) {
        try {
            return baseDataSvc.executeQuery('/PaddyTransfer/GetSackWeights', { productId: productId,stockId:stockId });
        } catch (e) {
            throw e;
        }
    }
    function save(objPaddyTransfer) {
        try {
            return baseDataSvc.executeQuery('/PaddyTransfer/Save', objPaddyTransfer);
        } catch (e) {
            throw e;
        }
    }
    //function edit(objPaddyTransfer) {
    //    try {
    //        return baseDataSvc.executeQuery('/PaddyTransfer/EditPaddyTransfer', objPaddyTransfer);
    //    } catch (e) {
    //        throw e;
    //    }
    //}
    //function deleteCell(objPaddyTransfer) {
    //    try {
    //        return baseDataSvc.remove('/PaddyTransfer/DeletePaddyTransfer', objPaddyTransfer, "ID");
    //    } catch (e) {
    //        throw e;
    //    }
    //}
    function getPaddyTransferInfos() {
        try {
            return baseDataSvc.executeQuery('/PaddyTransfer/GetPaddyTransferInfos',{});
        } catch (e) {
            throw e;
        }
    };

}]);