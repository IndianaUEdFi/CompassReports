/// <reference path="api.module.ts" />

module App.Api.GraduateFilters {

    export interface IGraduateFiltersApi {
        getCohorts(expectedGraduationYear: number): angular.IPromise<Models.FilterValueModel[]>;
        getSchoolYears(): angular.IPromise<Models.FilterValueModel[]>;
    }

    class GraduateFiltersApi extends ApiBase implements IGraduateFiltersApi {
        resourceUrl = 'graduate-filters';

        getCohorts(expectedGraduationYear: number): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/cohorts?expectedGraduationYear=${expectedGraduationYear}`).then((data) => { return data.data; });
        }

        getSchoolYears(): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/school-years`).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.graduate-filters", [])
        .service("api.graduate-filters", GraduateFiltersApi);
} 
