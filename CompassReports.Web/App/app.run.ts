/// <reference path="app.module.ts"/>

module App {

    interface IAppRootScope extends ng.IRootScopeService {
        currentState: ng.ui.IState,
        title: string;
    }

    class AppRun {
        static $inject = ['$rootScope'];

        constructor($rootScope: IAppRootScope) {

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

            $rootScope.title = "Compass Reports";
        }
    }
    
    angular
        .module('app')
        .run(AppRun);
}