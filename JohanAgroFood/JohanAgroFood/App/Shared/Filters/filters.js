app.filter('filterById', function () {
    return function (input, params) {
        var out = [];
        for (var i = 0; i < input.length; i++) {
            if ((params.pIds.indexOf(input[i].productId) !== -1)) {
                out.push(input[i]);
            }
        }
        return out;
    };
});