/// <reference path="../app.config.ts" />

module App {

    export interface IApi {
        assessment: Api.Assessment.IAssessmentApi;
        assessmentFilters: Api.AssessmentFilters.IAssessmentFiltersApi;
        assessmentPass: Api.AssessmentPass.IAssessmentPassApi;
        assessmentPassTrend: Api.AssessmentPassTrend.IAssessmentPassTrendApi;
        assessmentPerformance: Api.AssessmentPerformance.IAssessmentPerformanceApi;
        assessmentPerformanceTrend: Api.AssessmentPerformanceTrend.IAssessmentPerformanceTrendApi;
        assessmentScores: Api.AssessmentScores.IAssessmentScoresApi;
        assessmentScoreTrend: Api.AssessmentScoreTrend.IAssessmentScoreTrendApi;
        assessmentTaking: Api.AssessmentTaking.IAssessmentTakingApi;
        assessmentTakingTrend: Api.AssessmentTakingTrend.IAssessmentTakingTrendApi,
        attendance: Api.Attendance.IAttendanceApi;
        attendanceTrends: Api.AttendanceTrends.IAttendanceTrendsApi;
        demographicFilters: Api.DemographicFilters.IDemographicFiltersApi;
        enrollment: Api.Enrollment.IEnrollmentApi;
        enrollmentFilters: Api.EnrollmentFilters.IEnrollmentFiltersApi;
        enrollmentTrends: Api.EnrollmentTrends.IEnrollmentTrendsApi;
        graduateDiplomaType: Api.GraduateDiplomaType.IGraduateDiplomaTypeApi;
        graduateFilters: Api.GraduateFilters.IGraduateFiltersApi;
        graduateStatus: Api.GraduateStatus.IGraduateStatusApi;
        graduateTrends: Api.GraduateTrends.IGraduateTrendsApi;
        graduateWaivers: Api.GraduateWaivers.IGraduateWaiversApi;
    }

    class ApiService implements IApi {

        static $inject = [
            'api.assessment',
            'api.assessment-filters',
            'api.assessment-pass',
            'api.assessment-pass-trend',
            'api.assessment-performance',
            'api.assessment-performance-trend',
            'api.assessment-scores',
            'api.assessment-score-trend',
            'api.assessment-taking',
            'api.assessment-taking-trend',
            'api.attendance',
            'api.attendance-trends',
            'api.demographic-filters',
            'api.enrollment',
            'api.enrollment-filters',
            'api.enrollment-trends',
            'api.graduate-diploma-type',
            'api.graduate-filters',
            'api.graduate-status',
            'api.graduate-trends',
            'api.graduate-waivers'
        ];

        constructor(
            public assessment: Api.Assessment.IAssessmentApi,
            public assessmentFilters: Api.AssessmentFilters.IAssessmentFiltersApi,
            public assessmentPass: Api.AssessmentPass.IAssessmentPassApi,
            public assessmentPassTrend: Api.AssessmentPassTrend.IAssessmentPassTrendApi,
            public assessmentPerformance: Api.AssessmentPerformance.IAssessmentPerformanceApi,
            public assessmentPerformanceTrend: Api.AssessmentPerformanceTrend.IAssessmentPerformanceTrendApi,
            public assessmentScores: Api.AssessmentScores.IAssessmentScoresApi,
            public assessmentScoreTrend: Api.AssessmentScoreTrend.IAssessmentScoreTrendApi,
            public assessmentTaking: Api.AssessmentTaking.IAssessmentTakingApi,
            public assessmentTakingTrend: Api.AssessmentTakingTrend.IAssessmentTakingTrendApi,
            public attendance: Api.Attendance.IAttendanceApi,
            public attendanceTrends: Api.AttendanceTrends.IAttendanceTrendsApi,
            public demographicFilters: Api.DemographicFilters.IDemographicFiltersApi,
            public enrollment: Api.Enrollment.IEnrollmentApi,
            public enrollmentFilters: Api.EnrollmentFilters.IEnrollmentFiltersApi,
            public enrollmentTrends: Api.EnrollmentTrends.IEnrollmentTrendsApi,
            public graduateDiplomaType: Api.GraduateDiplomaType.IGraduateDiplomaTypeApi,
            public graduateFilters: Api.GraduateFilters.IGraduateFiltersApi,
            public graduateStatus: Api.GraduateStatus.IGraduateStatusApi,
            public graduateTrends: Api.GraduateTrends.IGraduateTrendsApi,
            public graduateWaivers: Api.GraduateWaivers.IGraduateWaiversApi
        ) {
        }
    }

    export interface IApiBaseDefault<T> {
        resourceUrl: string;
        getAll(): angular.IPromise<Array<T>>;
        get(id: number): angular.IPromise<T>;
        post(model: T): angular.IPromise<T>;
        put(id: number, model: T): angular.IPromise<T>;
        delete(id: number): angular.IPromise<boolean>;
    }

    export class ApiBase {

        static $inject = ['services', 'settings'];

        resourceUrl = '';

        constructor(protected services: IServices, protected settings: ISystemSettings) {
        }
    }


    export class ApiBaseDefault<T> extends ApiBase implements IApiBaseDefault<T> {
        getAll(): angular.IPromise<Array<T>> { return this.services.http.get<Array<T>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`).then((data) => { return data.data; }); }
        get(id: number): angular.IPromise<T> { return this.services.http.get<T>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/${id}`).then((data) => { return data.data; }); }
        post(model: T): angular.IPromise<T> { return this.services.http.post<T>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`, model).then((data) => { return data.data; }); }
        put(id: number, model: T): angular.IPromise<T> { return this.services.http.put<T>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/${id}`, model).then((data) => { return data.data; }); }
        delete(id: number): angular.IPromise<boolean> { return this.services.http.delete<boolean>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/${id}`).then((data) => { return data.data; }); }
    }

    angular
        .module("app.api", [
            'app.api.assessment',
            'app.api.assessment-filters',
            'app.api.assessment-pass',
            'app.api.assessment-pass-trend',
            'app.api.assessment-performance',
            'app.api.assessment-performance-trend',
            'app.api.assessment-scores',
            'app.api.assessment-score-trend',
            'app.api.assessment-taking',
            'app.api.assessment-taking-trend',
            'app.api.attendance',
            'app.api.attendance-trends',
            'app.api.demographic-filters',
            'app.api.enrollment',
            'app.api.enrollment-filters',
            'app.api.enrollment-trends',
            'app.api.graduate-diploma-type',
            'app.api.graduate-filters',
            'app.api.graduate-status',
            'app.api.graduate-trends',
            'app.api.graduate-waivers'
        ])
        .service("api", ApiService);

} 