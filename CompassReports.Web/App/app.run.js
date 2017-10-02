/// <reference path="app.module.ts"/>
var App;
(function (App) {
    var AppRun = (function () {
        function AppRun($rootScope, $templateRequest) {
            //cache icon urls
            var urls = ['fonts/fontawesome-webfont.svg'];
            angular.forEach(urls, function (url) {
                $templateRequest(url);
            });
            $rootScope.currentTheme = 'compass-reports-theme';
            $rootScope.defaultPrimary = { color: '#003E69', name: 'dark-blue' };
            $rootScope.defaultSecondary = { color: '#FDCD0F', name: 'dark-yellow' };
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
    AppRun.$inject = ['$rootScope', '$templateRequest'];
    angular
        .module('app')
        .run(AppRun);
})(App || (App = {}));
//# sourceMappingURL=app.run.js.map