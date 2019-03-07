/// <reference path="app.module.ts"/>

module App {

    export interface IAppRootScope extends ng.IRootScopeService {
        currentState: ng.ui.IState,
        title: string;
        currentTheme: string;
        defaultPrimary: {color: string, name: string};
        defaultSecondary: { color: string, name: string };
        defaultTertiary: { color: string };
        filterModel?: Models.IReportFilterModel;
        backState?: string;
        backParameters?: any;
    }

    class AppRun {
        static $inject = ['$rootScope', '$templateRequest'];

        constructor($rootScope: IAppRootScope,
            $templateRequest: ng.ITemplateRequestService) {

            //cache icon urls
            const urls = ['fonts/fontawesome-webfont.svg'];

            angular.forEach(urls, function (url) {
                $templateRequest(url);
            });

            $rootScope.currentTheme = 'compass-reports-theme';
            $rootScope.defaultPrimary = { color: '#990000', name: 'primary-color' };
            $rootScope.defaultSecondary = { color: '#EDEBEB', name: 'secondary-color' };
            $rootScope.defaultTertiary = { color: '#4A3C31' };

            var contentLoadedEvent = $rootScope.$on('$viewContentLoaded', (event: ng.IAngularEvent, view: string) => {

                if (event.targetScope && event.targetScope.ctrl)
                    console.log(`Loaded controller ${event.targetScope.ctrl.constructor.name} for view ${view}`);

            });

            var stateAuthorizeStartEvent = $rootScope.$on('$stateChangeStart', (event: ng.IAngularEvent, toState: ng.ui.IState, toStateParams: any) => {
                console.log('changing state');
                console.log(toState);
            });

            // Cleanup
            $rootScope.$on('$destroy', () => {
                contentLoadedEvent();
                stateAuthorizeStartEvent();
            });

            $rootScope.title = "INsite Compass";
        }
    }
    
    angular
        .module('app')
        .run(AppRun);
}