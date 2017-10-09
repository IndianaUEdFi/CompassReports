/// <reference path="api.module.ts" />

module App.Api.GraduateWaivers {

    export interface IGraduateWaiversApi {
        get(model: Models.GraduateFilterModel);
        byEnglishLanguageLearner(model: Models.GraduateFilterModel);
        byEthnicity(model: Models.GraduateFilterModel);
        byLunchStatus(model: Models.GraduateFilterModel);
        bySpecialEducation(model: Models.GraduateFilterModel);
    }

    class GraduateWaiversApi extends ApiBase implements IGraduateWaiversApi {
        resourceUrl = 'graduate-waivers';

        get(model: Models.GraduateFilterModel): angular.IPromise<Models.PieChartModel> {
            return this.services.http.post<Models.PieChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`, model).then((data) => { return data.data; });
        }

        byEnglishLanguageLearner(model: Models.GraduateFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-language-learner`, model).then((data) => { return data.data; });
        }

        byEthnicity(model: Models.GraduateFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-ethnicity`, model).then((data) => { return data.data; });
        }

        byLunchStatus(model: Models.GraduateFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-lunch-status`, model).then((data) => { return data.data; });
        }

        bySpecialEducation(model: Models.GraduateFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-special-education`, model).then((data) => { return data.data; });
        }

    }

    angular
        .module("app.api.graduate-waivers", [])
        .service("api.graduate-waivers", GraduateWaiversApi);
} 
