angular.module('starter.controllers', [])
.controller('adminCtrl', ['$scope', '$state', 'adminService', function ($scope, $state, adminService) {
    $scope.List = {};
    //选中的
    $scope.correctingIns = {};
    //加载提示
    $scope.loading = true;
    //加载
    $scope.load = function () {
        var promise = adminService.getCorrectingInss(); // 同步调用，获得承诺接口
        promise.then(function (data) {  // 调用承诺API获取数据 .resolve
            $scope.List = data;
            $scope.loading = false;
        });
    }
    $scope.load();
    $scope.update = function () {
        var promise = adminService.correctingInsUpdate($scope.correctingIns); // 同步调用，获得承诺接口
        promise.then(function () {  // 调用承诺API获取数据 .resolve           
            $scope.loading = false;
            $('#detaildialog').modal("hide");
            $scope.load();
        });
    }
    //查看详细
    $scope.detail = function (item) {
        $scope.correctingIns = item;
        $('#detaildialog').modal({
            keyboard: false,
            show: true,
            backdrop: 'static'
        });
    }
    //通过
    $scope.pass = function () {
        $scope.correctingIns.IsApproved = true;
        $scope.update()
        
    }
    //删除
    $scope.delete = function () {
        $scope.correctingIns.IsDeleted = true;
        $scope.update()
    }  
    
}]);
