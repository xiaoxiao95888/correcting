angular.module('starter.services', [])
.service('myInsService', ['$http', '$q', function ($http, $q) {
    //获取list
    this.getEmployeeInstitutions = function () {
        var deferred = $q.defer();//申明deferred对象
        $http({
            method: 'get',
            url: '/api/EmployeeInstitution'
        }).success(function (data) {
            deferred.resolve(data);// 声明执行成功，即http请求数据成功，可以返回数据了
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise; // 返回承诺，这里并不是最终数据，而是访问最终数据的API
    }
    //机构Id
    this.getInstitution = function (id) {
        var deferred = $q.defer();
        $http({
            method: 'get',
            url: '/api/Institution/' + id
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise;
    }
    //机构关键字
    this.getSearchInstitutions = function (key, page) {
        var deferred = $q.defer();
        $http({
            method: 'get',
            url: '/api/Institution?key=' + key + "&page=" + page
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise;
    }
    //保存最终结果
    this.postInstitution = function (institution) {
        var deferred = $q.defer();
        $http({
            method: 'post',
            data: institution,
            url: '/api/CorrectingIns'
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise;
    }
    //检查是否有权限编辑
    this.CheckCanEdit = function (id) {
        var deferred = $q.defer();
        $http({
            method: 'get',            
            url: '/api/CheckCanEdit/'+id
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise;
    }
    //获取积分榜数据
    this.getScoreBoards = function () {
        var deferred = $q.defer();
        $http({
            method: 'get',
            url: '/api/ScoreBoard'
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise;
    }
}])
.service('cacheService', [function () {
    var rootIns = {};
    var createIns = {};
    function setRootIns(data) {
        rootIns = data;
    }
    function getRootIns() {
        return rootIns;
    }
    function setCreateIns(data) {
        createIns = data;
    }
    function getCreateIns() {
        return createIns;
    }
    return {
        setRootIns: setRootIns,
        getRootIns: getRootIns,
        setCreateIns: setCreateIns,
        getCreateIns: getCreateIns
    }
}])
.service('locationService', ['$http', '$q', function ($http, $q) {
    //获取地理信息list
    this.getLocations = function () {
        var deferred = $q.defer();//申明deferred对象
        $http({
            method: 'get',
            url: '/api/Location'
        }).success(function (data) {
            deferred.resolve(data);// 声明执行成功，即http请求数据成功，可以返回数据了
        }).error(function () {
            deferred.reject('There was an error')
        })
        return deferred.promise; // 返回承诺，这里并不是最终数据，而是访问最终数据的API
    }

}])
