app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) return ''
                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

users = [{ name: '4545' }, { name: '1010' }, { name: 'chuck' }, { name: 'norris' }];

app.directive('nameUnique', function ($resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (name) {
                $timeout.cancel(stop_timeout);

                if (name === '')
                    ngModel.$setValidity('unique', true);

                var Model = $resource("/api/" + attrs.nameUnique);

                stop_timeout = $timeout(function () {
                    Model.query({
                        name: name,
                    }, function (models) {
                        return ngModel.$setValidity('unique', models.length === 0);
                    });
                }, 200);
            });
        }
    };
});

app.directive('duplicate', function () {
    return {
        restrict: 'E',
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {
                debugeer;
                var duplicate = scope[attrs.duplicate];
                if (scope.duplicate.indexOf(viewValue) !== -1) {
                    ctrl.$setValidity('duplicate', false);
                    return undefined;
                } else {
                    ctrl.$setValidity('duplicate', true);
                    return viewValue;
                }
            });
        }
    };
});

app.directive('jhSelect', [
    function () {
        return {
            restrict: 'A',
            scope: false,
            link: function (scope, element, attrs) {
                scope.$watch(attrs.ngModel, function (value) {
                    var index = parseInt(element.val()) + 1;
                    var itemList = element.children();

                    var i = 0;
                    for (i in itemList) {
                        if (i === index.toString()) {
                            var item = itemList[index];
                            var res = attrs.jhSelect.split(".");
                            var entity = scope.$eval(res[0]);

                            for (var j = 0; j < res.length - 2; j++) {
                                entity = entity[res[j + 1]];
                            }
                            entity[res[res.length - 1]] = item.text;
                        }
                    }
                });
            }
        }
    }]);

app.directive('banglaNumber', function ($rootScope) {
    return {
        restrict: 'EA',
        template: '<input class="form-control" placeholder="{{placeHolder}}" name="{{inputName}}" ng-model="inputValue" />',
        scope: {
            inputValue: '=',
            required: '=',
            outputValue: '=',
            placeHolder: '='
        },
        link: function (scope) {
            scope.$watch('inputValue', function (newValue, oldValue) {
                if (newValue == undefined) {
                    scope.required = false;
                    return;
                }
                var arr = String(newValue).split("");
                if (arr.length === 0) { scope.outputValue = ""; return; }
                if (arr.length === 1 && (arr[0] == '-' || arr[0] === '.')) return;
                if (arr.length === 2 && newValue === '-.') return;
                var engArr = ConvtBangToEng(arr);
                if (engArr.length <= 0) {
                    scope.inputValue = oldValue;
                    return;
                }
                scope.outputValue = engArr.join("");
                //var engVal = scope.inputValue;

            });
        }
    };

    function ConvtBangToEng(arr) {
        var engVal = [];
        arr.forEach(function (x) {
            if ($rootScope.banglaNumber.hasOwnProperty(x)) {
                engVal.push($rootScope.banglaNumber[x]);
            }
            else {
                engVal = [];
                return engVal;
            }
        });
        return engVal;
    }

});

app.directive('typeahead', function ($timeout) {
    return {
        restrict: 'AEC',
        scope: {
            items: '=',
            value: '=',
            model: '=',
            onSelect: '&'
        },
        link: function (scope, elem, attrs) {
            scope.handleSelection = function (selectedItem) {
                scope.model = selectedItem.name;
                scope.value = selectedItem.ID;
                scope.current = 0;
                scope.selected = true;
                $timeout(function () {
                    scope.onSelect();
                }, 200);
            };
            scope.current = 0;
            scope.selected = true; // hides the list initially
            scope.isCurrent = function (index) {
                return scope.current == index;
            };
            scope.setCurrent = function (index) {
                scope.current = index;
            };
        },
        templateUrl: 'http://localhost:7737/template.html'
    }
});

