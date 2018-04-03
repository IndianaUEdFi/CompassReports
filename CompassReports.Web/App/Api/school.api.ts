/// <reference path="api.module.ts" />

module App.Api.School {
    import SchoolModel = Models.SchoolModel;

    export interface ISchoolApi {
        getAll(): angular.IPromise<SchoolModel[]>;
    }

    class SchoolApi extends ApiBase implements ISchoolApi {
        resourceUrl = 'school';

        getAll(): angular.IPromise<SchoolModel[]> {
            return this.services.http.get<SchoolModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.school", [])
        .service("api.school", SchoolApi);
} 
