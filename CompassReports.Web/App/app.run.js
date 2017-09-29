/// <reference path="app.module.ts"/>
var App;
(function (App) {
    var AppRun = (function () {
        function AppRun($rootScope) {
            var contentLoadedEvent = $rootScope.$on('$viewContentLoaded', function (event, view) {
                if (event.targetScope && event.targetScope.ctrl)
                    console.log("Loaded controller " + event.targetScope.ctrl.constructor.name + " for view " + view);
            });
            var stateAuthorizeStartEvent = $rootScope.$on('$stateChangeStart', function (event, toState, toStateParams) {
                console.log('changing state');
                console.log(toState);
            });
            // Cleanup
            $rootScope.$on('$destroy', function () {
                contentLoadedEvent();
                stateAuthorizeStartEvent();
            });
            $rootScope.title = "Compass Reports";
        }
        return AppRun;
    }());
    AppRun.$inject = ['$rootScope'];
    angular
        .module('app')
        .run(AppRun);
})(App || (App = {}));
//# sourceMappingURL=app.run.js.map