/// <reference path="api.module.ts" />

module App.Api.GraduateTrends {

    export interface IGraduateTrendsApi {
        byStatus(model: Models.GraduateFilterModel);
        byWaiver(model: Models.GraduateFilterModel);
    }

    class GraduateTrendsApi extends ApiBase implements IGraduateTrendsApi {
        resourceUrl = 'graduate-trends';

        byStatus(model: Models.GraduateFilterModel): angular.IPromise<Models.PieChartModel> {
            return this.services.http.post<Models.PieChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-status`, model).then((data) => { return data.data; });
        }

        byWaiver(model: Models.GraduateFilterModel): angular.IPromise<Models.PieChartModel> {
            return this.services.http.post<Models.PieChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-waiver`, model).then((data) => { return data.data; });
        }

    }

    angular
        .module("app.api.graduate-trends", [])
        .service("api.graduate-trends", GraduateTrendsApi);
} 
