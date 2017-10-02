/// <reference path="api.module.ts" />

module App.Models {
    export class EnrollmentTrendsChartModel<T> {
        Title: string;
        Headers: string[];
        Labels: string[];
        Series: string[];
        Data: T[][];
        ShowChart: boolean;
    }

    export class EnrollmentTrendsFilterModel {
        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        Grades: string[];
        SchoolYears: number[];
        LunchStatuses: string[];
        SpecialEducationStatuses: string[];

        isFiltering = () => {
            if (this.EnglishLanguageLearnerStatuses != null || this.EnglishLanguageLearnerStatuses.length) return true;
            if (this.Ethnicities != null || this.Ethnicities.length) return true;
            if (this.Grades != null || this.Grades.length) return true;
            if (this.SchoolYears != null || this.SchoolYears.length) return true;
            if (this.LunchStatuses != null || this.LunchStatuses.length) return true;
            if (this.SpecialEducationStatuses != null || this.SpecialEducationStatuses.length) return true;
            return false;
        }

        constructor() {
            this.Grades = [];
            this.Ethnicities = [];
            this.SchoolYears = [];
            this.EnglishLanguageLearnerStatuses = [];
            this.LunchStatuses = [];
            this.SpecialEducationStatuses = [];
        }
    }
}

module App.Api.EnrollmentTrends {

    export interface IEnrollmentTrendsApi {
        byEnglishLanguageLearner(model: Models.EnrollmentTrendsFilterModel);
        byEthnicity(model: Models.EnrollmentTrendsFilterModel);
        byGrade(model: Models.EnrollmentTrendsFilterModel);
        byLunchStatus(model: Models.EnrollmentTrendsFilterModel);
        bySpecialEducation(model: Models.EnrollmentTrendsFilterModel);
    }

    class EnrollmentTrendsApi extends ApiBase implements IEnrollmentTrendsApi {
        resourceUrl = 'enrollment-trends';

        byEnglishLanguageLearner(model: Models.EnrollmentTrendsFilterModel): angular.IPromise<Models.EnrollmentTrendsChartModel<number>> {
            return this.services.http.post<Models.EnrollmentTrendsChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-english-language-learner`, model).then((data) => { return data.data; });
        }

        byEthnicity(model: Models.EnrollmentTrendsFilterModel): angular.IPromise<Models.EnrollmentTrendsChartModel<number>> {
            return this.services.http.post<Models.EnrollmentTrendsChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-ethnicity`, model).then((data) => { return data.data; });
        }

        byGrade(model: Models.EnrollmentTrendsFilterModel): angular.IPromise<Models.EnrollmentTrendsChartModel<number>> {
            return this.services.http.post<Models.EnrollmentTrendsChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-grade`, model).then((data) => { return data.data; });
        }

        byLunchStatus(model: Models.EnrollmentTrendsFilterModel): angular.IPromise<Models.EnrollmentTrendsChartModel<number>> {
            return this.services.http.post<Models.EnrollmentTrendsChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-lunch-status`, model).then((data) => { return data.data; });
        }

        bySpecialEducation(model: Models.EnrollmentTrendsFilterModel): angular.IPromise<Models.EnrollmentTrendsChartModel<number>> {
            return this.services.http.post<Models.EnrollmentTrendsChartModel<number>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-special-education`, model).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.enrollment-trends", [])
        .service("api.enrollment-trends", EnrollmentTrendsApi);
} 
