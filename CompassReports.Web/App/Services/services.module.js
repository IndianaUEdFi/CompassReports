var App;
(function (App) {
    var Services = (function () {
        function Services(colorGradient, compile, document, filter, http, q, state, timeout, template, window) {
            this.colorGradient = colorGradient;
            this.compile = compile;
            this.document = document;
            this.filter = filter;
            this.http = http;
            this.q = q;
            this.state = state;
            this.timeout = timeout;
            this.template = template;
            this.window = window;
        }
        return Services;
    }());
    Services.$inject = [
        'app.services.color-gradient',
        '$compile',
        '$document',
        '$filter',
        '$http',
        '$q',
        '$state',
        '$timeout',
        '$templateRequest',
        '$window'
    ];
    angular
        .module("app.services", ['app.services.color-gradient'])
        .service("services", Services);
})(App || (App = {}));
//# sourceMappingURL=services.module.js.map