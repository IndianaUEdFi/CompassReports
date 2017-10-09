/// <reference path="api.module.ts" />

module App.Api.EnrollmentFilters {

    export interface IEnrollmentFiltersApi {
        getSchoolYears(): angular.IPromise<Models.FilterValueModel[]>;
    }

    class EnrollmentFiltersApi extends ApiBase implements IEnrollmentFiltersApi {
        resourceUrl = 'enrollment-filters';

        getSchoolYears(): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/school-years`).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.enrollment-filters", [])
        .service("api.enrollment-filters", EnrollmentFiltersApi);
} 
