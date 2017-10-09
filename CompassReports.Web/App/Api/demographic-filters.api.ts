/// <reference path="api.module.ts" />

module App.Api.DemographicFilters {

    export interface IDemographicFiltersApi {
        getEnglishLanguageLearnerStatuses(): angular.IPromise<string[]>;
        getEthnicities(): angular.IPromise<string[]>;
        getGrades(): angular.IPromise<string[]>;
        getLunchStatuses(): angular.IPromise<string[]>;
        getSpecialEducationStatuses(): angular.IPromise<string[]>;
    }

    class DemographicFiltersApi extends ApiBase implements IDemographicFiltersApi {
        resourceUrl = 'demographic-filters';

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
    }

    angular
        .module("app.api.demographic-filters", [])
        .service("api.demographic-filters", DemographicFiltersApi);
} 
