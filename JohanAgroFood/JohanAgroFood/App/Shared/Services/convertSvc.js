app.service('convertSvc', ['$rootScope',

    function ($rootScope) {

        var convSvc = {
            toDate: toDate,
            numGenerator: numGenerator,
            isEmpty:isEmpty,
            updateCollection: updateCollection,
            ConvtEngToBang: ConvtEngToBang
    };
        return convSvc;

        // update grid when edit completed 
        function updateCollection(entityList, entity, operation, keyProperty) {
            try {

                keyProperty = typeof keyProperty === "undefined" ? "id" : keyProperty;

                //edit
                if (operation == "edit") {
                    var index = -1;

                    for (var i = 0; i < entityList.length; i++) {
                        if (entityList[i][keyProperty] === entity[keyProperty]) {
                            index = i;
                        }
                    }

                    if (index > -1)
                        entityList[index] = entity; // splice(index, 1);
                }
                else if (operation == "delete") {
                    for (var i = 0; i < entityList.length; i++) {
                        if (entityList[i][keyProperty] === entity[keyProperty]) {
                            entityList.splice(i, 1);
                            break;
                        }
                    }
                }
                else {
                    entityList.unshift(entity);

                }

            } catch (e) {
                throw e;
            }
        }

        function isEmpty(value)
        {
            if (value == "" || value == '' || value == undefined || value==null)
            {
                return true;
            }
            else {
                return false;
            }
        }
       
        function toDate(date) {
            try {
                if (date==null) {
                    return '';
                }
                var d = new Date(parseInt(date.substr("6")));
                var m = d.getMonth() + 1;
                var newDate = d.getDate() + "-" + m + "-" + d.getFullYear();
                return newDate;
            } catch (ex) {
                throw ex;
            }
        }

        function numGenerator(prefix)
        {
            var date = new Date();
            var components = [
                date.getYear(),
                date.getMonth(),
                date.getDate(),
                date.getHours(),
                date.getMinutes(),
                date.getSeconds()
                //date.getMilliseconds()
            ];

            var id = components.join("");
            return prefix + "_" + id;
        }
        
        function ConvtEngToBang(engVal) {
            var arr = String(engVal).split("");
            var banVal = [];
            arr.forEach(function (x) {
                if ($rootScope.engNumber.hasOwnProperty(x)) {
                    banVal.push($rootScope.engNumber[x]);
                }
                else {
                    banVal = [];
                    return banVal;
                }
            });
            return banVal.join("");
        }
    }
]);
