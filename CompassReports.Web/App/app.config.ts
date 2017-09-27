/// <reference path="app.module.ts"/>

module App {
    class AppConfig {
        static $inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', '$mdThemingProvider'];

        constructor(
            $locationProvider: ng.ILocationProvider,
            $stateProvider: ng.ui.IStateProvider,
            $urlRouterProvider: ng.ui.IUrlRouterProvider,
            $mdThemingProvider: angular.material.IThemingProvider) {

            $locationProvider.hashPrefix('');

            $mdThemingProvider.definePalette('dark-blue', {
                '50': '#E0E8ED',
                '100': '#B3C5D2',
                '200': '#809FB4',
                '300': '#4D7896',
                '400': '#265B80',
                '500': '#003E69',
                '600': '#003861',
                '700': '#003056',
                '800': '#00284C',
                '900': '#001B3B',
                'A100': '#71A3FF',
                'A200': '#3E81FF',
                'A400': '#0B60FF',
                'A700': '#0054F1',
                'contrastDefaultColor': 'light',
                'contrastDarkColors': '50 100 200 A100',
                'contrastStrongLightColors': '300 400'
            });

            $mdThemingProvider.definePalette('dark-yellow', {
                '50': '#FFF9E2',
                '100': '#FEF0B7',
                '200': '#FEE687',
                '300': '#FEDC57',
                '400': '#FDD533',
                '500': '#FDCD0F',
                '600': '#FDC80D',
                '700': '#FCC10B',
                '800': '#FCBA08',
                '900': '#FCAE04',
                'A100': '#FEDC57',
                'A200': '#FDCD0F',
                'A400': '#FCC10B',
                'A700': '#FCAE04',
                'contrastDefaultColor': 'dark',
                //'contrastDarkColors': '50 100 200 A100',
                //'contrastStrongLightColors': '300 400'
            });

            $mdThemingProvider.theme('compass-reports-theme')
                .primaryPalette('dark-blue')
                .accentPalette('dark-yellow')
                .warnPalette('red');

            $urlRouterProvider.otherwise('/home');

            $stateProvider
                .state('app', {
                    abstract: true
                });
        }
    }

    angular
        .module('app')
        .config(AppConfig);
}