/// <reference path="api.module.ts" />
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
    var Models;
    (function (Models) {
        var FilterModel = (function () {
            function FilterModel() {
            }
            return FilterModel;
        }());
        Models.FilterModel = FilterModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
(function (App) {
    var Api;
    (function (Api) {
        var EnrollmentFilters;
        (function (EnrollmentFilters) {
            var EnrollmentFiltersApi = (function (_super) {
                __extends(EnrollmentFiltersApi, _super);
                function EnrollmentFiltersApi() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    _this.resourceUrl = 'enrollment-filters';
                    return _this;
                }
                EnrollmentFiltersApi.prototype.getEnglishLanguageLearnerStatuses = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/english-learner-statuses").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getEthnicities = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/ethnicities").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getGrades = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/grades").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getLunchStatuses = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/lunch-statuses").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getSpecialEducationStatuses = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/special-education-statuses").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getSchoolYears = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/school-years").then(function (data) { return data.data; });
                };
                return EnrollmentFiltersApi;
            }(App.ApiBase));
            angular
                .module("app.api.enrollment-filters", [])
                .service("api.enrollment-filters", EnrollmentFiltersApi);
        })(EnrollmentFilters = Api.EnrollmentFilters || (Api.EnrollmentFilters = {}));
    })(Api = App.Api || (App.Api = {}));
})(App || (App = {}));
//# sourceMappingURL=enrollment-filters.api.js.map