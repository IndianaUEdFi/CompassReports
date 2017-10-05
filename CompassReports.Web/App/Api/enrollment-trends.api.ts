/// <reference path="api.module.ts" />

module App.Api.EnrollmentTrends {

    export interface IEnrollmentTrendsApi {
        byEnglishLanguageLearner(model: Models.EnrollmentFilterModel);
        byEthnicity(model: Models.EnrollmentFilterModel);
        byGrade(model: Models.EnrollmentFilterModel);
        byLunchStatus(model: Models.EnrollmentFilterModel);
        bySpecialEducation(model: Models.EnrollmentFilterModel);
    }

    class EnrollmentTrendsApi extends ApiBase implements IEnrollmentTrendsApi {
        resourceUrl = 'enrollment-trends';

        byEnglishLanguageLearner(model: Models.EnrollmentFilterModel): angular.IPromise<Models.BarChartModel> {
            return this.services.http.post<Models.BarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-english-language-learner`, model).then((data) => { return data.data; });
        }

        byEthnicity(model: Models.EnrollmentFilterModel): angular.IPromise<Models.BarChartModel> {
            return this.services.http.post<Models.BarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-ethnicity`, model).then((data) => { return data.data; });
        }

        byGrade(model: Models.EnrollmentFilterModel): angular.IPromise<Models.BarChartModel> {
            return this.services.http.post<Models.BarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-grade`, model).then((data) => { return data.data; });
        }

        byLunchStatus(model: Models.EnrollmentFilterModel): angular.IPromise<Models.BarChartModel> {
            return this.services.http.post<Models.BarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-lunch-status`, model).then((data) => { return data.data; });
        }

        bySpecialEducation(model: Models.EnrollmentFilterModel): angular.IPromise<Models.BarChartModel> {
            return this.services.http.post<Models.BarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-special-education`, model).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.enrollment-trends", [])
        .service("api.enrollment-trends", EnrollmentTrendsApi);
} 
