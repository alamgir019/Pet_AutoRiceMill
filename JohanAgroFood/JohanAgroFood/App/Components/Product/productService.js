app.service("productService", ["$http", "baseDataSvc", function ($http, baseDataSvc) {
    //service instant
    var dataSvc = {
        getProduct: getProduct,
        getParents: getParents,
        save: save,
        edit: edit,
        deleteProduct: deleteProduct
    };
    return dataSvc;
    function getProduct(product) {
        try {
            return baseDataSvc.executeQuery('/CommonData/GetAllProduct', { prod: product });
        } catch (e) {
            throw e;
        }
    }
    function getParents() {
        try {
            product = {};
            product.parentId = 0;
            return baseDataSvc.executeQuery('/Product/GetProduct', product);
        } catch (e) {
            throw e;
        }
    }
    
    function edit(objProduct) {
        try {
            return baseDataSvc.executeQuery('/Product/EditProduct', objProduct);
        } catch (e) {
            throw e;
        }
    }
    function save(objProduct) {
        try {
            return baseDataSvc.executeQuery('/Product/SaveProduct', objProduct);
        } catch (e) {
            throw e;
        }
    }

    function deleteProduct(prod) {
        return baseDataSvc.remove('/Product/DeleteProduct', prod, "ID");
    }
}]);