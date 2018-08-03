/// <reference path="app.module.ts"/>

module App {
    class AppConfig {
        static $inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', '$mdIconProvider', '$mdThemingProvider', '$provide'];

        constructor(
            $locationProvider: ng.ILocationProvider,
            $stateProvider: ng.ui.IStateProvider,
            $urlRouterProvider: ng.ui.IUrlRouterProvider,
            $mdIconProvider: ng.material.IIconProvider,
            $mdThemingProvider: angular.material.IThemingProvider,
            $provide: angular.IModule) {

            $locationProvider.hashPrefix('');

            $mdThemingProvider.definePalette('primary-color', {
                '50': '#ff1a1a',
                '100': '#ff0000',
                '200': '#e60000',
                '300': '#cc0000',
                '400': '#b30000',
                '500': '#990000',
                '600': '#800000',
                '700': '#660000',
                '800': '#4d0000',
                '900': '#330000',
                'A100': '#990000',
                'A200': '#b30000',
                'A400': '#cc0000',
                'A700': '#e60000',
                'contrastDefaultColor': 'light',
                'contrastDarkColors': '50 100 200 A100',
                'contrastStrongLightColors': '300 400'
            });

            $mdThemingProvider.definePalette('secondary-color', {
                '50': '#ffffff',
                '100': '#ffffff',
                '200': '#FEE687',
                '300': '#f3f2f2',
                '400': '#f3f2f2',
                '500': '#EDEBEB',
                '600': '#e7e4e4',
                '700': '#dbd7d7',
                '800': '#cfc9c9',
                '900': '#c2bcbc',
                'A100': '#EDEBEB',
                'A200': '#e7e4e4',
                'A400': '#cfc9c9',
                'A700': '#b6afaf',
                'contrastDefaultColor': 'dark',
                //'contrastDarkColors': '50 100 200 A100',
                //'contrastStrongLightColors': '300 400'
            });

            $mdThemingProvider.theme('compass-reports-theme')
                .primaryPalette('primary-color',
                {
                    'hue-2': '100'
                })
                .accentPalette('secondary-color',
                {
                    'hue-1': '200',
                    'hue-2': '300',
                    'hue-3': '100'
                })
                .warnPalette('red');

            $mdThemingProvider.setDefaultTheme('compass-reports-theme');
            $provide.value('themeProvider', $mdThemingProvider);

            $urlRouterProvider.otherwise('/enrollment');

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