angular.module('starter.services', [])
.service('adminService', ['$http', '$q', function ($http, $q) {
    //获取CorrectingIns list
    this.getCorrectingInss = function () {
        var deferred = $q.defer();//申明deferred对象
        $http({
            method: 'get',
            url: '/api/Admin'
        }).success(function (data) {
            deferred.resolve(data);// 声明执行成功，即http请求数据成功，可以返回数据了
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise; // 返回承诺，这里并不是最终数据，而是访问最终数据的API
    }  
    this.correctingInsUpdate = function (model) {
        var deferred = $q.defer();//申明deferred对象
        $http({
            method: 'post',
            data:model,
            url: '/api/Admin'
        }).success(function (data) {
            deferred.resolve(data);// 声明执行成功，即http请求数据成功，可以返回数据了
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise; // 返回承诺，这里并不是最终数据，而是访问最终数据的API
    }
}]);
