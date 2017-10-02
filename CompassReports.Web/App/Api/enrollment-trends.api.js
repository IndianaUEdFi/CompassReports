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
        var EnrollmentTrendsChartModel = (function () {
            function EnrollmentTrendsChartModel() {
            }
            return EnrollmentTrendsChartModel;
        }());
        Models.EnrollmentTrendsChartModel = EnrollmentTrendsChartModel;
        var EnrollmentTrendsFilterModel = (function () {
            function EnrollmentTrendsFilterModel() {
                var _this = this;
                this.isFiltering = function () {
                    if (_this.EnglishLanguageLearnerStatuses != null || _this.EnglishLanguageLearnerStatuses.length)
                        return true;
                    if (_this.Ethnicities != null || _this.Ethnicities.length)
                        return true;
                    if (_this.Grades != null || _this.Grades.length)
                        return true;
                    if (_this.SchoolYears != null || _this.SchoolYears.length)
                        return true;
                    if (_this.LunchStatuses != null || _this.LunchStatuses.length)
                        return true;
                    if (_this.SpecialEducationStatuses != null || _this.SpecialEducationStatuses.length)
                        return true;
                    return false;
                };
                this.Grades = [];
                this.Ethnicities = [];
                this.SchoolYears = [];
                this.EnglishLanguageLearnerStatuses = [];
                this.LunchStatuses = [];
                this.SpecialEducationStatuses = [];
            }
            return EnrollmentTrendsFilterModel;
        }());
        Models.EnrollmentTrendsFilterModel = EnrollmentTrendsFilterModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
(function (App) {
    var Api;
    (function (Api) {
        var EnrollmentTrends;
        (function (EnrollmentTrends) {
            var EnrollmentTrendsApi = (function (_super) {
                __extends(EnrollmentTrendsApi, _super);
                function EnrollmentTrendsApi() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    _this.resourceUrl = 'enrollment-trends';
                    return _this;
                }
                EnrollmentTrendsApi.prototype.byEnglishLanguageLearner = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-english-language-learner", model).then(function (data) { return data.data; });
                };
                EnrollmentTrendsApi.prototype.byEthnicity = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-ethnicity", model).then(function (data) { return data.data; });
                };
                EnrollmentTrendsApi.prototype.byGrade = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-grade", model).then(function (data) { return data.data; });
                };
                EnrollmentTrendsApi.prototype.byLunchStatus = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-lunch-status", model).then(function (data) { return data.data; });
                };
                EnrollmentTrendsApi.prototype.bySpecialEducation = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-special-education", model).then(function (data) { return data.data; });
                };
                return EnrollmentTrendsApi;
            }(App.ApiBase));
            angular
                .module("app.api.enrollment-trends", [])
                .service("api.enrollment-trends", EnrollmentTrendsApi);
        })(EnrollmentTrends = Api.EnrollmentTrends || (Api.EnrollmentTrends = {}));
    })(Api = App.Api || (App.Api = {}));
})(App || (App = {}));
//# sourceMappingURL=enrollment-trends.api.js.map