var app = angular.module('johanApp',
    ['ngRoute', 'ngAnimate', 'ui.grid']);

app.run(function ($rootScope, $templateCache) {
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (typeof (current) !== 'undefined') {
            $templateCache.remove(current.templateUrl);
        }
    });
    var banglaNumber = {};
    banglaNumber['০'] = "0";
    banglaNumber['১'] = "1";
    banglaNumber['২'] = "2";
    banglaNumber['৩'] = "3";
    banglaNumber['৪'] = "4";
    banglaNumber['৫'] = "5";
    banglaNumber['৬'] = "6";
    banglaNumber['৭'] = "7";
    banglaNumber['৮'] = "8";
    banglaNumber['৯'] = "9";
    banglaNumber['.'] = ".";
    banglaNumber['+'] = "+";
    banglaNumber['-'] = "-";
    banglaNumber['/'] = "/";

    var engNumber = {};
    engNumber['0'] = "০";
    engNumber['1'] = "১";
    engNumber['2'] = "২";
    engNumber['3'] = "৩";
    engNumber['4'] = "৪";
    engNumber['5'] = "৫";
    engNumber['6'] = "৬";
    engNumber['7'] = "৭";
    engNumber['8'] = "৮";
    engNumber['9'] = "৯";
    engNumber['.'] = ".";
    engNumber['+'] = "+";
    engNumber['-'] = "-";
    engNumber['/'] = "/";

    $rootScope.banglaNumber = banglaNumber;
    $rootScope.engNumber = engNumber;

});