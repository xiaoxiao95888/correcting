angular.module('starter', ['ui.router', 'ngAnimate', 'starter.controllers', 'starter.services', 'starter.directives'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
       .state('institutions', {
           url: '/',
           templateUrl: '/mobile/institutions',
           controller: 'InstitutionsCtrl'
       })
      .state('detail', {
          url: '/',
          params: {
              id: null,
              reload: false,
              actiontype: 'root'
          },
          templateUrl: '/mobile/detail',
          controller: 'DetailCtrl'
      })
      .state('selectParentIns', {
          url: '/',
          templateUrl: '/mobile/selectParentIns',
          controller: 'SelectInsCtrl'
      })
       .state('selectLocation', {
           params: { actiontype: null },
           url: '/',
           templateUrl: '/mobile/selectLocation',
           controller: 'SelectLocationCtrl'
       })
      .state('selectChildrens', {
          url: '/',
          templateUrl: '/mobile/selectChildrens',
          controller: 'SelectInsCtrl'
      })
      .state('create', {
          url: '/',
          params: { actiontype: 'create' },
          templateUrl: '/mobile/create',
          controller: 'DetailCtrl'
      })
      // if none of the above states are matched, use this as the fallback
      $urlRouterProvider.otherwise('/');
  })


