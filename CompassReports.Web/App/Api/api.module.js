/// <reference path="../app.config.ts" />
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var App;
(function (App) {
    var ApiService = (function () {
        function ApiService(enrollment, enrollmentFilters) {
            this.enrollment = enrollment;
            this.enrollmentFilters = enrollmentFilters;
        }
        return ApiService;
    }());
    ApiService.$inject = [
        'api.enrollment',
        'api.enrollment-filters'
    ];
    var ApiBase = (function () {
        function ApiBase(services, settings) {
            this.services = services;
            this.settings = settings;
            this.resourceUrl = '';
        }
        return ApiBase;
    }());
    ApiBase.$inject = ['services', 'settings'];
    App.ApiBase = ApiBase;
    var ApiBaseDefault = (function (_super) {
        __extends(ApiBaseDefault, _super);
        function ApiBaseDefault() {
            return _super !== null && _super.apply(this, arguments) || this;
        }
        ApiBaseDefault.prototype.getAll = function () { return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.get = function (id) { return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/" + id).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.post = function (model) { return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl, model).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.put = function (id, model) { return this.services.http.put(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/" + id, model).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.delete = function (id) { return this.services.http.delete(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/" + id).then(function (data) { return data.data; }); };
        return ApiBaseDefault;
    }(ApiBase));
    App.ApiBaseDefault = ApiBaseDefault;
    angular
        .module("app.api", [
        'app.api.enrollment',
        'app.api.enrollment-filters'
    ])
        .service("api", ApiService);
})(App || (App = {}));
//# sourceMappingURL=api.module.js.map