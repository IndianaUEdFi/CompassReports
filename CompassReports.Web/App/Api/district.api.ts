/// <reference path="api.module.ts" />

module App.Api.District {
    import DistrictModel = Models.DistrictModel;

    export interface IDistrictApi {
        getAll(): angular.IPromise<DistrictModel[]>;
    }

    class DistrictApi extends ApiBase implements IDistrictApi {
        resourceUrl = 'district';

        getAll(): angular.IPromise<DistrictModel[]> {
            return this.services.http.get<DistrictModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.district", [])
        .service("api.district", DistrictApi);
} 
