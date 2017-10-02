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
        var EnrollmentChartModel = (function () {
            function EnrollmentChartModel() {
            }
            return EnrollmentChartModel;
        }());
        Models.EnrollmentChartModel = EnrollmentChartModel;
        var EnrollmentFilterModel = (function () {
            function EnrollmentFilterModel(schoolYear) {
                this.Grades = [];
                this.Ethnicities = [];
                this.SchoolYear = schoolYear;
                this.EnglishLanguageLearnerStatuses = [];
                this.LunchStatuses = [];
                this.SpecialEducationStatuses = [];
            }
            return EnrollmentFilterModel;
        }());
        Models.EnrollmentFilterModel = EnrollmentFilterModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
(function (App) {
    var Api;
    (function (Api) {
        var Enrollment;
        (function (Enrollment) {
            var EnrollmentApi = (function (_super) {
                __extends(EnrollmentApi, _super);
                function EnrollmentApi() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    _this.resourceUrl = 'enrollment';
                    return _this;
                }
                EnrollmentApi.prototype.byEnglishLanguageLearner = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-english-language-learner", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.byEthnicity = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-ethnicity", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.byGrade = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-grade", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.byLunchStatus = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-lunch-status", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.bySpecialEducation = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-special-education", model).then(function (data) { return data.data; });
                };
                return EnrollmentApi;
            }(App.ApiBase));
            angular
                .module("app.api.enrollment", [])
                .service("api.enrollment", EnrollmentApi);
        })(Enrollment = Api.Enrollment || (Api.Enrollment = {}));
    })(Api = App.Api || (App.Api = {}));
})(App || (App = {}));
//# sourceMappingURL=enrollment.api.js.map