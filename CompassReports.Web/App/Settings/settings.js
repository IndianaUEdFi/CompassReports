var App;
(function (App) {
    var SystemSettings = (function () {
        function SystemSettings() {
            this.apiBaseUrl = 'api';
            this.directiveBaseUri = 'app/directives';
            this.moduleBaseUri = 'app/modules';
            this.componentBaseUri = 'app/components';
        }
        return SystemSettings;
    }());
    angular
        .module('app.settings', [])
        .constant('settings', new SystemSettings());
})(App || (App = {}));
//# sourceMappingURL=settings.js.map