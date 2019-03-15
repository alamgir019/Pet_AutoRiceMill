app.controller("productController", ["$scope", "productService", function (scope, productService) {
    scope.product = null;
    scope.products = [];
    scope.parents = [];
    var async = 2;
    var curasync = 0;

    //methods
    scope.edit = edit;
    scope.save = save;
    scope.delete = deleteProduct;


    init();

    //To Get All Records  
    function init() {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        getProduct();
        getParents();
        initialize();
    }
    function initialize()
    {
        scope.product = {};
    }

    function getParents() {
        var result = productService.getParents();
        result.then(function (data) {
            scope.parents = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }
    function getProduct() {
        var result = productService.getProduct(null);
        result.then(function (data) {
            scope.products = data;
            curasync++;
            if (curasync == async) {
                $('#myModal').modal('hide');
            }
        }, function (e) {
            alert(e);
        });
    }

    function edit(prod) {
        scope.isEdit = true;
        scope.product = prod;
    }

    function save() {
        if (!scope.productForm.$valid) {
            return;
        }
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result=null;
        var operation = "save";
        if (scope.isEdit) {
            operation = "edit";
            result = productService.edit(scope.product);
        }
        else {
            result = productService.save(scope.product);
        }
        result.then(function (data) {
            async = 2;
            curasync = 0;
            alert("ডাটা সেভ হয়েছে");
            getProduct();
            getParents()
            initialize();
        }, function (e) {
            alert(e);
        });
    }

    function deleteProduct(prod) {
        $('#myModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        var result = productService.deleteProduct(prod);
        result.then(function (data) {
            async = 1;
            curasync = 0;
            alert("ডাটা ডিলিট হয়েছে");
            getProduct();
            initilize();
        }, function (e) {
            alert(e);
        });
    }
    
}]);