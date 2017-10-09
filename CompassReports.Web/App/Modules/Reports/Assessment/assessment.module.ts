/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.Assessment {

    import BarChartModel = Models.BarChartModel;
    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    const DefaultAssessmentTitle = 'ISTAR';
    const DefaultSubject = 'English/Language Arts Only';

    function onAssessmentChange(model: Models.AssessmentFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.AssessmentTitle) {

            model.Subject = null;
            model.SchoolYear = null;
            model.Assessments = [];
            //model.PerformanceLevels = [];
            //model.GoodCauseExcemptions = [];

            filters[2].update([] as Models.FilterValueModel[]);
            filters[3].update([] as Models.FilterValueModel[]);

            api.assessmentFilters.getSubjects(model.AssessmentTitle).then((subjects: string[]) => {
                filters[1].update(subjects);
                if (subjects.length === 1)
                {
                    model.Subject = subjects[0];
                    onSubjectChange(model,filters, api);
                }
            });
        }
    }

    function onSubjectChange(model: Models.AssessmentFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.AssessmentTitle && model.Subject) {

            model.SchoolYear = null;
            model.Assessments = [];

            filters[3].update([] as Models.FilterValueModel[]);

            api.assessmentFilters.getSchoolYears(model.AssessmentTitle, model.Subject).then((schoolYears: Models.FilterValueModel[]) => {
                filters[2].update(schoolYears);
                model.SchoolYear = schoolYears[0].Value as number;
            });

            api.assessmentFilters.getGrades(model.AssessmentTitle, model.Subject).then((grades: Models.FilterValueModel[]) => {
                filters[3].update(grades);
            });
        }
    }

    class AssessmentReportView extends ReportBaseView {
        resolve = {
            report: ['assessments', 'subjects', 'schoolYears',
                'grades', 'performanceLevels', 'goodCauseExcemptions',
                'englishLanguageLearnerStatuses', 'ethnicities', 'lunchStatuses',
                'specialEducationStatuses',
                (assessments: Models.FilterValueModel[], subjects: string[], schoolYears: Models.FilterValueModel[],
                    grades: Models.FilterValueModel[], peformanceLevels: string[], goodCauseExcemptions: string[],
                    englishLanguageLearnerStatuses: string[], ethnicities: string[], lunchStatuses: string[],
                    specialEducationStatuses: string[]) => {

                var filters = [
                    new Models.FilterModel<string>(assessments, 'Assessment', 'AssessmentTitle', false, true, onAssessmentChange),
                    new Models.FilterModel<number>(subjects, 'Subject', 'Subject', false, true, onSubjectChange),
                    new Models.FilterModel<number>(schoolYears, 'School Year', 'SchoolYear', false, true),
                    new Models.FilterModel<number>(grades, 'Grades', 'Assessments', true, true),
                    //new Models.FilterModel<number>(peformanceLevels, 'Performance Levels', 'PerformanceLevels', true),
                    //new Models.FilterModel<number>(goodCauseExcemptions, 'Good Cause Excemptions', 'GoodCauseExcemptions', true),
                    new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                    new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                    new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                    new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                ];

                var charts = [
                    new PieChartModel('assessment', 'byPerformanceLevel'),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelByEthnicity'),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelByLunchStatus'),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelByEnglishLanguageLearner'),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelBySpecialEducation'),
                    new PercentageTotalBarChartModel('assessment', 'byGoodCause')
                ];

                var model = new Models.AssessmentFilterModel();
                model.AssessmentTitle = DefaultAssessmentTitle;
                model.Subject = DefaultSubject;
                model.SchoolYear = (schoolYears && schoolYears.length) ? schoolYears[0].Value as number : null;

                return {
                    filters: filters,
                    charts: charts,
                    title: 'Assessment',
                    model: model
                }
            }],
            assessments: ['api', (api: IApi) => {
                return api.assessmentFilters.getAssessments();
            }],
            subjects: ['api', (api: IApi) => {
                return api.assessmentFilters.getSubjects(DefaultAssessmentTitle);
            }],
            schoolYears: ['api', (api: IApi) => {
                return api.assessmentFilters.getSchoolYears(DefaultAssessmentTitle, DefaultSubject);
            }],
            grades: ['api', (api: IApi) => {
                return api.assessmentFilters.getGrades(DefaultAssessmentTitle, DefaultSubject);
            }],
            performanceLevels: ['api', (api: IApi) => {
                return api.assessmentFilters.getPerformanceLevels(DefaultAssessmentTitle, DefaultSubject);
            }],
            goodCauseExcemptions: ['api', (api: IApi) => {
                return api.assessmentFilters.getGoodCauseExcemptions(DefaultAssessmentTitle, DefaultSubject);
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
            }],
        }
    }

    class AssessmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.assessment',
                {
                    url: '/assessment',
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