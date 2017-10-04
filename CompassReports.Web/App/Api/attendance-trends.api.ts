/// <reference path="api.module.ts" />

module App.Api.AttendanceTrends {

    export interface IAttendanceTrendsApi {
        byEnglishLanguageLearner(model: Models.EnrollmentFilterModel);
        byEthnicity(model: Models.EnrollmentFilterModel);
        byGrade(model: Models.EnrollmentFilterModel);
        byLunchStatus(model: Models.EnrollmentFilterModel);
        bySpecialEducation(model: Models.EnrollmentFilterModel);
    }

    class AttendanceTrendsApi extends ApiBase implements IAttendanceTrendsApi {
        resourceUrl = 'attendance-trends';

        byEnglishLanguageLearner(model: Models.EnrollmentFilterModel): angular.IPromise<Models.LineChartModel<number>> {
            return this.services.http.post<Models.LineChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-english-language-learner`, model).then((data) => { return data.data; });
        }

        byEthnicity(model: Models.EnrollmentFilterModel): angular.IPromise<Models.LineChartModel<number>> {
            return this.services.http.post<Models.LineChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-ethnicity`, model).then((data) => { return data.data; });
        }

        byGrade(model: Models.EnrollmentFilterModel): angular.IPromise<Models.LineChartModel<number>> {
            return this.services.http.post<Models.LineChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-grade`, model).then((data) => { return data.data; });
        }

        byLunchStatus(model: Models.EnrollmentFilterModel): angular.IPromise<Models.LineChartModel<number>> {
            return this.services.http.post<Models.LineChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-lunch-status`, model).then((data) => { return data.data; });
        }

        bySpecialEducation(model: Models.EnrollmentFilterModel): angular.IPromise<Models.LineChartModel<number>> {
            return this.services.http.post<Models.LineChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-special-education`, model).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.attendance-trends", [])
        .service("api.attendance-trends", AttendanceTrendsApi);
} 
