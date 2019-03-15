app.service("commonSvc", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getAllProduct: getAllProduct,
        getAllParty: getAllParty,
        getAllStock: getAllStock,
        getProductByName: getProductByName,
        getAllSector: getAllSector
    };
    return dataSvc;


    function getAllProduct() {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetAllProduct', {});
        } catch (e) {
            throw e;
        }
    }
    function getAllSector() {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetAllSector', {});
        } catch (e) {
            throw e;
        }
    }
    function getAllParty() {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetAllParty', {});
        } catch (e) {
            throw e;
        }
    }
    function getAllStock() {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetAllStock', {});
        } catch (e) {
            throw e;
        }
    }
    function  getProductByName(pName)
    {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetProductByName', { productName: pName });
        } catch (e) {
            throw e;
        }
    }
}]);