/// <reference path="api.module.ts" />

module App.Api.AssessmentFilters {

    export interface IAssessmentFiltersApi {
        getAssessments(): angular.IPromise<string[]>;
        getEnglishLanguageLearnerStatuses(): angular.IPromise<string[]>;
        getEthnicities(): angular.IPromise<string[]>;
        getGoodCauseExcemptions(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]>;
        getGrades(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]>;
        getLunchStatuses(): angular.IPromise<string[]>;
        getPerformanceLevels(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]>;
        getSchoolYears(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]>;
        getSubjects(assessmentTitle: string): angular.IPromise<string[]>;
        getSpecialEducationStatuses(): angular.IPromise<string[]>;
    }

    class AssessmentFiltersApi extends ApiBase implements IAssessmentFiltersApi {

        resourceUrl = 'assessment-filters';

        getAssessments(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/assessments`).then((data) => { return data.data; });
        }

        getEnglishLanguageLearnerStatuses(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/english-learner-statuses`).then((data) => { return data.data; });
        }

        getEthnicities(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/ethnicities`).then((data) => { return data.data; });
        }

        getGoodCauseExcemptions(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/good-cause-excemptions?assessmentTitle=${assessmentTitle}&subject=${subject}`).then((data) => { return data.data; });
        }

        getGrades(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/grades?assessmentTitle=${assessmentTitle}&subject=${subject}`).then((data) => { return data.data; });
        }

        getLunchStatuses(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/lunch-statuses`).then((data) => { return data.data; });
        }

        getPerformanceLevels(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/performance-levels?assessmentTitle=${assessmentTitle}&subject=${subject}`).then((data) => { return data.data; });
        }

        getSchoolYears(assessmentTitle: string, subject: string): angular.IPromise<Models.FilterValueModel[]> {
            return this.services.http.get<Models.FilterValueModel[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/school-years?assessmentTitle=${assessmentTitle}&subject=${subject}`).then((data) => { return data.data; });
        }

        getSpecialEducationStatuses(): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/special-education-statuses`).then((data) => { return data.data; });
        }

        getSubjects(assessmentTitle: string): angular.IPromise<string[]> {
            return this.services.http.get<string[]>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/subjects?assessmentTitle=${assessmentTitle}`).then((data) => { return data.data; });
        }

    }

    angular
        .module("app.api.assessment-filters", [])
        .service("api.assessment-filters", AssessmentFiltersApi);
} 
