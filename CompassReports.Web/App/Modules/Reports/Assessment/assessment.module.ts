/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.Assessment {

    import BarChartModel = Models.BarChartModel;
    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    const DefaultAssessmentTitle = 'ISTAR';
    const DefaultSubject = 'English/Language Arts Only';

    interface IAssessmentParams {
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

    class AssessmentReportView extends ReportBaseView {
        resolve = {
            report: ['$stateParams', 'subjects', 'schoolYears',
                'grades', 'englishLanguageLearnerStatuses', 'ethnicities',
                'lunchStatuses', 'specialEducationStatuses',
                ($stateParams: IAssessmentParams, subjects: string[], schoolYears: Models.FilterValueModel[],
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

                var charts = [
                    new PieChartModel('assessmentPerformance', 'get'),
                    new PercentageTotalBarChartModel('assessmentPerformance', 'byEthnicity'),
                    new PercentageTotalBarChartModel('assessmentPerformance', 'byLunchStatus'),
                    new PercentageTotalBarChartModel('assessmentPerformance', 'byEnglishLanguageLearner'),
                    new PercentageTotalBarChartModel('assessmentPerformance', 'bySpecialEducation'),
                    new PercentageTotalBarChartModel('assessment', 'byGoodCause')
                ];

                var model = new Models.AssessmentFilterModel();
                model.AssessmentTitle = $stateParams.assessmentTitle || DefaultAssessmentTitle;
                model.Subject = ($stateParams.assessmentTitle) ? ((subjects && subjects.length) ? subjects[0] : null) : DefaultSubject;
                model.SchoolYear = (schoolYears && schoolYears.length) ? schoolYears[0].Value as number : null;
                model.Assessments = (grades && grades.length === 1) ? [grades[0].Value as number] : [];

                return {
                    filters: filters,
                    charts: charts,
                    title: $stateParams.assessmentTitle || DefaultAssessmentTitle,
                    model: model
                }
            }],
            subjects: ['$stateParams', 'api', ($stateParams: IAssessmentParams, api: IApi) => {
                var assessmentTitle = $stateParams.assessmentTitle || DefaultAssessmentTitle;
                return api.assessmentFilters.getSubjects(assessmentTitle);
            }],
            schoolYears: ['$stateParams', 'api', 'subjects', ($stateParams: IAssessmentParams, api: IApi, subjects: string[]) => {
                if ($stateParams.assessmentTitle) {
                    return (subjects && subjects.length) ? api.assessmentFilters.getSchoolYears($stateParams.assessmentTitle, subjects[0]) : [];
                } else {
                    return api.assessmentFilters.getSchoolYears(DefaultAssessmentTitle, DefaultSubject);
                }
            }],
            grades: ['$stateParams', 'api', 'subjects', ($stateParams: IAssessmentParams, api: IApi, subjects: string[]) => {
                if ($stateParams.assessmentTitle) {
                    return (subjects && subjects.length) ? api.assessmentFilters.getGrades($stateParams.assessmentTitle, subjects[0]) : [];
                } else {
                    return api.assessmentFilters.getGrades(DefaultAssessmentTitle, DefaultSubject);
                }
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

    class AssessmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.assessment',
                {
                    url: '/assessment?assessmentTitle',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment', [])
        .config(AssessmentConfig);
}