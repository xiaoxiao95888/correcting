angular.module('starter.directives', [])
.directive('searchInput', ['$parse', function ($parse) {
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
////微信浏览器返回监听
//.directive("popstate", ['$window', '$timeout', function ($window, $timeout) {
//    return function (scope, element, attrs) {
//        var bool = false;
//        $timeout(function () {
//            bool = true;
//        }, 1500);
//        angular.element($window).bind("popstate", function () {
            
//            //$window.history.pushState(null, null, "#");
           
//                if (bool) {
//                    //alert("我监听到了浏览器的返回按钮事件啦");//根据自己的需求实现自己的功能  
//                    //var r=confirm("Press a button")
//                    //if (r==true)
//                    //{
//                    //}                    
                
//                }
//                var state = {};
//                $window.history.pushState(state, "title", "#");
           
//        });
//    };
//}]);