/// <reference path="api.module.ts" />

module App.Api.Assessment {

    export interface IAssessmentApi {
        byGoodCause(model: Models.AssessmentFilterModel);
        byPerformanceLevel(model: Models.AssessmentFilterModel);
        performanceLevelByEnglishLanguageLearner(model: Models.AssessmentFilterModel);
        performanceLevelByEthnicity(model: Models.AssessmentFilterModel);
        performanceLevelByLunchStatus(model: Models.AssessmentFilterModel);
        performanceLevelBySpecialEducation(model: Models.AssessmentFilterModel);
    }

    class AssessmentApi extends ApiBase implements IAssessmentApi {
        resourceUrl = 'assessment';

        byGoodCause(model: Models.AssessmentFilterModel): angular.IPromise<Models.PieChartModel> {
            return this.services.http.post<Models.PieChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-good-cause`, model).then((data) => { return data.data; });
        }

        byPerformanceLevel(model: Models.AssessmentFilterModel): angular.IPromise<Models.PieChartModel> {
            return this.services.http.post<Models.PieChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-performance-level`, model).then((data) => { return data.data; });
        }

        performanceLevelByEnglishLanguageLearner(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/performance-level-language`, model).then((data) => { return data.data; });
        }

        performanceLevelByEthnicity(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/performance-level-ethnicity`, model).then((data) => { return data.data; });
        }

        performanceLevelByLunchStatus(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/performance-level-lunch`, model).then((data) => { return data.data; });
        }

        performanceLevelBySpecialEducation(model: Models.AssessmentFilterModel): angular.IPromise<Models.PercentageTotalBarChartModel> {
            return this.services.http.post<Models.PercentageTotalBarChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/performance-level-special`, model).then((data) => { return data.data; });
        }

    }

    angular
        .module("app.api.assessment", [])
        .service("api.assessment", AssessmentApi);
} 
