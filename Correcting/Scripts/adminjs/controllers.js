angular.module('starter.controllers', [])
.controller('adminCtrl', ['$scope', '$state', 'adminService', function ($scope, $state, adminService) {
    $scope.List = {};
    $scope.correctingIns = {};
    //加载提示
    $scope.loading = true;
    var promise = adminService.getCorrectingInss(); // 同步调用，获得承诺接口
    promise.then(function (data) {  // 调用承诺API获取数据 .resolve
        $scope.List = data;
        $scope.loading = false;
    });
    //查看详细
    $scope.detail = function (item) {
        $scope.correctingIns = item;
        $('#detaildialog').modal({
            keyboard: false,
            show: true
        });
    }
    $scope.warningdialog = { show: false, content: '' };
    //警告框确定
    $scope.warningdialogconfirm = function () {
        $scope.warningdialog.show = false;
        $scope.warningdialog.content = '';
    }
  
    ;    //$scope.redirectdetail = function (item) {
    //    var promiseCheckCanEdit = myInsService.CheckCanEdit(item.Id);
    //    promiseCheckCanEdit.then(function (data) {
    //        if (data.Error) {
    //            $scope.warningdialog.show = true;
    //            $scope.warningdialog.content = data.Message;
    //        } else {
    //            $state.go("detail", { id: item.Id, reload: true });
    //        }
    //    });
    //}
}]);
