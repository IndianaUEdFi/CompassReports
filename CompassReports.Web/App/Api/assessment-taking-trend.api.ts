/// <reference path="api.module.ts" />

module App.Api.AssessmentTakingTrend {

    export interface IAssessmentTakingTrendApi {
        get(model: Models.AssessmentFilterModel);
        byEnglishLanguageLearner(model: Models.AssessmentFilterModel);
        byEthnicity(model: Models.AssessmentFilterModel);
        byLunchStatus(model: Models.AssessmentFilterModel);
        bySpecialEducation(model: Models.AssessmentFilterModel);
    }

    class AssessmentTakingTrendApi extends ApiBase implements IAssessmentTakingTrendApi {
        resourceUrl = 'assessment-taking-trend';

        get(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`, model).then((data) => { return data.data; });
        }

        byEnglishLanguageLearner(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-language-learner`, model).then((data) => { return data.data; });
        }

        byEthnicity(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-ethnicity`, model).then((data) => { return data.data; });
        }

        byLunchStatus(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-lunch-status`, model).then((data) => { return data.data; });
        }

        bySpecialEducation(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-special-education`, model).then((data) => { return data.data; });
        }

    }

    angular
        .module("app.api.assessment-taking-trend", [])
        .service("api.assessment-taking-trend", AssessmentTakingTrendApi);
} 
