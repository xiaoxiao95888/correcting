angular.module('starter.directives', [])
.directive('searchInput',['$parse', function ($parse) {
    return {
        link: function (scope, element, attrs) {
            var model = $parse(attrs.searchInput);
            scope.$watch(model, function (value) {
                //console.log('value=', value);
                if (value === true) {
                    element[0].focus();
                }
            });
            element.bind('blur', function () {
                //console.log('blur')                
                //scope.$apply(model.assign(scope, false));
            })
        }
    };
}])
.directive("scroll", ['$window', function ($window) {
    return function (scope, element, attrs) {
        angular.element($window).bind("scroll", function () {
            var t, h;
            if (document.documentElement && document.documentElement.scrollTop) {
                t = document.documentElement.scrollTop;
                h = document.documentElement.scrollHeight;
            } else if (document.body) {
                t = document.body.scrollTop;
                h = document.body.scrollHeight;
            }
            if (t + document.documentElement.clientHeight == h) {
                scope.turned();
            }

            scope.$apply();
        });
    };
}]);