/// <reference path="api.module.ts" />

module App.Api.School {
    import SchoolModel = Models.SchoolModel;

    export interface ISchoolApi {
        getAll(districts?: number[]): angular.IPromise<SchoolModel[]>;
    }

    class SchoolApi extends ApiBase implements ISchoolApi {
        resourceUrl = 'school';

        getAll(districts?: number[]): angular.IPromise<SchoolModel[]> {

            let query = '';
            if (districts && districts.length) {
                query += '?';
                angular.forEach(districts, (district, index) => {
                    if (index > 0) query += '&';
                    query += `districtId=${district}`;
                });
            }

            return this.services.http.get<SchoolModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}${query}`).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.school", [])
        .service("api.school", SchoolApi);
} 
