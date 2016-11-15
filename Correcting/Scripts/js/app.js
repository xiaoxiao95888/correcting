angular.module('starter', ['ui.router', 'ngAnimate', 'starter.services', 'starter.directives', 'starter.controllers'])
  .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
      $stateProvider
       .state('institutions', {
           url: '/',
           templateUrl: '/mobile/institutions',
           controller: 'InstitutionsCtrl'
       })
      .state('detail', {
          url: '/detail',
          params: {
              id: null,
              reload: false,
              actiontype: 'root'
          },
          templateUrl: '/mobile/detail',
          controller: 'DetailCtrl'
      })
      .state('selectParentIns', {
          url: '/selectParentIns',
          templateUrl: '/mobile/selectParentIns',
          controller: 'SelectInsCtrl'
      })
       .state('selectLocation', {
           params: { actiontype: null },
           url: '/selectLocation',
           templateUrl: '/mobile/selectLocation',
           controller: 'SelectLocationCtrl'
       })
      .state('selectChildrens', {
          url: '/selectChildrens',
          templateUrl: '/mobile/selectChildrens',
          controller: 'SelectInsCtrl'
      })
      .state('create', {
          url: '/create',
          params: { actiontype: 'create' },
          templateUrl: '/mobile/create',
          controller: 'DetailCtrl'
      })
      .state('scoreboard', {
          url: '/scoreboard',
          templateUrl: '/mobile/scoreboard',
          controller: 'ScoreBoardCtrl'
      })

      // if none of the above states are matched, use this as the fallback
      $urlRouterProvider.otherwise('/');
  }])


