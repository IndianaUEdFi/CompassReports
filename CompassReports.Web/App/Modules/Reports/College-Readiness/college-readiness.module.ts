/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.CollegeReadiness {

    interface ICollegeReadinessParams {
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
                if (grades.length === 1) {
                    model.Assessments = [grades[0].Value as number];
                }
            });
        }
    }

    export class CollegeReadinessReportView extends ReportBaseView {
        resolve = {
            report: ['$stateParams', 'subjects', 'schoolYears',
                'grades', 'englishLanguageLearnerStatuses', 'ethnicities',
                'lunchStatuses', 'specialEducationStatuses',
                ($stateParams: ICollegeReadinessParams, subjects: string[], schoolYears: Models.FilterValueModel[],
                    grades: Models.FilterValueModel[], englishLanguageLearnerStatuses: string[], ethnicities: string[],
                    lunchStatuses: string[], specialEducationStatuses: string[]) => {

                var filters = [
                    new Models.FilterModel<number>(subjects, 'Subject', 'Subject', false, true, onSubjectChange),
                    new Models.FilterModel<number>(schoolYears, 'School Year', 'SchoolYear', false, true),
                    new Models.FilterModel<number>(grades, 'Grades', 'Assessments', true, true),
                    new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                    new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                    new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                    new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                ];

                var model = new Models.AssessmentFilterModel();
                model.AssessmentTitle = $stateParams.assessmentTitle;
                model.Subject = ($stateParams.assessmentTitle) ? ((subjects && subjects.length) ? subjects[0] : null) : null;
                model.SchoolYear = (schoolYears && schoolYears.length) ? schoolYears[0].Value as number : null;
                model.Assessments = (grades && grades.length === 1) ? [grades[0].Value as number] : [];

                return {
                    filters: filters,
                    charts: this.charts,
                    title: $stateParams.assessmentTitle,
                    model: model
                }
            }],
            subjects: ['$stateParams', 'api', ($stateParams: ICollegeReadinessParams, api: IApi) => {
                var assessmentTitle = $stateParams.assessmentTitle;
                return api.assessmentFilters.getSubjects(assessmentTitle);
            }],
            schoolYears: ['$stateParams', 'api', 'subjects', ($stateParams: ICollegeReadinessParams, api: IApi, subjects: string[]) => {
                return (subjects && subjects.length) ? api.assessmentFilters.getSchoolYears($stateParams.assessmentTitle, subjects[0]) : [];
            }],
            grades: ['$stateParams', 'api', 'subjects', ($stateParams: ICollegeReadinessParams, api: IApi, subjects: string[]) => {
                return (subjects && subjects.length) ? api.assessmentFilters.getGrades($stateParams.assessmentTitle, subjects[0]) : [];
  
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

        constructor(settings: ISystemSettings,
            public charts: Models.ChartModel[]) {
            super(settings);
        }
    }

    angular
        .module('app.reports.college-readiness', [
            'app.reports.college-readiness.pass',
            'app.reports.college-readiness.scores',
            'app.reports.college-readiness.took'
        ]);
}