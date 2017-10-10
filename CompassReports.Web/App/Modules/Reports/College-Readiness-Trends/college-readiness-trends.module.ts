/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.CollegeReadinessTrends {

    interface ICollegeReadinessTrendsParams {
        assessmentTitle: string
    }

    function onSubjectChange(model: Models.AssessmentFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.AssessmentTitle && model.Subject) {

            model.SchoolYear = null;
            model.Assessments = [];

            filters[2].update([] as Models.FilterValueModel[]);

            api.assessmentFilters.getSchoolYears(model.AssessmentTitle, model.Subject).then((schoolYears: Models.FilterValueModel[]) => {
                filters[1].update(schoolYears);
                model.SchoolYear = schoolYears[0].Value as number;
            });

            api.assessmentFilters.getGrades(model.AssessmentTitle, model.Subject).then((grades: Models.FilterValueModel[]) => {
                filters[2].update(grades);
            });
        }
    }

    export class CollegeReadinessTrendsReportView extends ReportBaseView {
        resolve = {
            report: ['$rootScope', '$stateParams', 'charts', 'subjects',
                'schoolYears', 'grades', 'englishLanguageLearnerStatuses',
                'ethnicities', 'lunchStatuses', 'specialEducationStatuses',
                ($rootScope: IAppRootScope, $stateParams: ICollegeReadinessTrendsParams, charts: Models.ChartModel[], subjects: string[],
                    schoolYears: Models.FilterValueModel[], grades: Models.FilterValueModel[], englishLanguageLearnerStatuses: string[],
                    ethnicities: string[], lunchStatuses: string[], specialEducationStatuses: string[]) => {

                var filters = [
                    new Models.FilterModel<number>(subjects, 'Subject', 'Subject', false, true, onSubjectChange),
                    new Models.FilterModel<number>(schoolYears, 'School Years', 'SchoolYears', true, false),
                    new Models.FilterModel<number>(grades, 'Grades', 'Assessments', true, true),
                    new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                    new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                    new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                    new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                ];

                var model = new Models.AssessmentFilterModel();

                if ($rootScope.filterModel) {
                    model = $rootScope.filterModel as Models.AssessmentFilterModel;
                    $rootScope.filterModel = null;
                } else {
                    model.Subject = ($stateParams.assessmentTitle) ? ((subjects && subjects.length) ? subjects[0] : null) : null;
                }

                model.AssessmentTitle = $stateParams.assessmentTitle;

                let backState = null;
                let backParameters = null;

                if ($rootScope.backState) {
                    backState = $rootScope.backState;
                    backParameters = $rootScope.backParameters;
                    $rootScope.backState = null;
                    $rootScope.backParameters = null;
                }

                return {
                    filters: filters,
                    charts: charts,
                    title: $stateParams.assessmentTitle,
                    model: model,
                    backState: backState,
                    backParameters: backParameters
                }
            }],
            charts: {},
            subjects: ['$stateParams', 'api', ($stateParams: ICollegeReadinessTrendsParams, api: IApi) => {
                var assessmentTitle = $stateParams.assessmentTitle;
                return api.assessmentFilters.getSubjects(assessmentTitle);
            }],
            schoolYears: ['$stateParams', 'api', 'subjects', ($stateParams: ICollegeReadinessTrendsParams, api: IApi, subjects: string[]) => {
                return (subjects && subjects.length) ? api.assessmentFilters.getSchoolYears($stateParams.assessmentTitle, subjects[0]) : [];
            }],
            grades: ['$stateParams', 'api', 'subjects', ($stateParams: ICollegeReadinessTrendsParams, api: IApi, subjects: string[]) => {
                return (subjects && subjects.length) ? api.assessmentFilters.getGrades($stateParams.assessmentTitle, subjects[0]) : [];
            }],
            performanceLevels: ['$stateParams', 'api', ($stateParams: ICollegeReadinessTrendsParams, api: IApi) => {
                return api.assessmentFilters.getPerformanceLevels($stateParams.assessmentTitle);
            }],
            englishLanguageLearnerStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getEnglishLanguageLearnerStatuses();
            }],
            ethnicities: ['api', (api: IApi) => {
                return api.demographicFilters.getEthnicities();
            }],
            lunchStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getLunchStatuses();
            }],
            specialEducationStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getSpecialEducationStatuses();
            }]
        }
    }

    angular
        .module('app.reports.college-readiness-trends', [
            'app.reports.college-readiness-trends.pass',
            'app.reports.college-readiness-trends.score',
            'app.reports.college-readiness-trends.took'
        ]);
}