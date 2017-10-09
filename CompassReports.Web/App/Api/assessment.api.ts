/// <reference path="api.module.ts" />

module App.Api.Assessment {

    export interface IAssessmentApi {
        byGoodCause(model: Models.AssessmentFilterModel);
    }

    class AssessmentApi extends ApiBase implements IAssessmentApi {
        resourceUrl = 'assessment';

        byGoodCause(model: Models.AssessmentFilterModel): angular.IPromise<Models.PieChartModel> {
            return this.services.http.post<Models.PieChartModel>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/by-good-cause`, model).then((data) => { return data.data; });
        }
    }

    angular
        .module("app.api.assessment", [])
        .service("api.assessment", AssessmentApi);
} 
