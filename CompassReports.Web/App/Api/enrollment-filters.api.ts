/// <reference path="api.module.ts" />

module App.Api.EnrollmentFilters {

    export interface IEnrollmentFiltersApi {
        getEnglishLanguageLearnerStatuses(): angular.IPromise<string[]>;
        getEthnicities(): angular.IPromise<string[]>;
        getGrades(): angular.IPromise<string[]>;
        getLunchStatuses(): angular.IPromise<string[]>;
        getSpecialEducationStatuses(): angular.IPromise<string[]>;
        getSchoolYears(): angular.IPromise<Models.FilterValueModel[]>;
    }

    class EnrollmentFiltersApi extends ApiBase implements IEnrollmentFiltersApi {
        resourceUrl = 'enrollment-filters';

        getEnglishLanguageLearnerStatuses(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/english-learner-statuses`).then((data) => { return data.data; });
        }

        getEthnicities(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/ethnicities`).then((data) => { return data.data; });
        }

        getGrades(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/grades`).then((data) => { return data.data; });
        }

        getLunchStatuses(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/lunch-statuses`).then((data) => { return data.data; });
        }

        getSpecialEducationStatuses(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/special-education-statuses`).then((data) => { return data.data; });
        }

        getSchoolYears(): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/school-years`).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.enrollment-filters", [])
        .service("api.enrollment-filters", EnrollmentFiltersApi);
} 
