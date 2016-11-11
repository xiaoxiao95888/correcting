angular.module('starter', ['ui.router', 'starter.services', 'starter.controllers'])
  .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
      $stateProvider
       .state('admin', {
           url: '/',
           templateUrl: '/admin/list',
           controller: 'adminCtrl'
       })
      // if none of the above states are matched, use this as the fallback
      $urlRouterProvider.otherwise('/');
  }])


