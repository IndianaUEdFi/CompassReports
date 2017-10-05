/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.Assessment {

    import BarChartModel = Models.BarChartModel;
    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    function onAssessmentChange(model: Models.AssessmentFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.AssessmentTitle) {

            model.Subject = null;
            model.SchoolYear = null;
            model.Assessments = [];
            model.PerformanceLevels = [];
            model.GoodCauseExcemptions = [];

            filters[2].update([] as Models.FilterValueModel[]);
            filters[3].update([] as Models.FilterValueModel[]);
            filters[4].update([] as Models.FilterValueModel[]);
            filters[5].update([] as Models.FilterValueModel[]);

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
            model.PerformanceLevels = [];
            model.GoodCauseExcemptions = [];

            filters[3].update([] as Models.FilterValueModel[]);
            filters[4].update([] as Models.FilterValueModel[]);
            filters[5].update([] as Models.FilterValueModel[]);

            api.assessmentFilters.getSchoolYears(model.AssessmentTitle, model.Subject).then((schoolYears: Models.FilterValueModel[]) => {
                filters[2].update(schoolYears);
                model.SchoolYear = schoolYears[0].Value as number;
            });

            api.assessmentFilters.getGrades(model.AssessmentTitle, model.Subject).then((grades: Models.FilterValueModel[]) => {
                filters[3].update(grades);
            });

            api.assessmentFilters.getPerformanceLevels(model.AssessmentTitle, model.Subject).then((performanceLevels: Models.FilterValueModel[]) => {
                filters[4].update(performanceLevels);
            });

            api.assessmentFilters.getGoodCauseExcemptions(model.AssessmentTitle, model.Subject).then((goodCauseExcemptions: Models.FilterValueModel[]) => {
                filters[5].update(goodCauseExcemptions);
            });
        }
    }

    class AssessmentReportView extends ReportBaseView {
        resolve = {
            report: ['assessments', 'englishLanguageLearnerStatuses', 'ethnicities',
                'lunchStatuses', 'specialEducationStatuses',
                (assessments: Models.FilterValueModel[], englishLanguageLearnerStatuses: string[], ethnicities: string[],
                    lunchStatuses: string[], specialEducationStatuses: string[]) => {
                var filters = [
                    new Models.FilterModel<string>(assessments, 'Assessment', 'AssessmentTitle', false, true, onAssessmentChange),
                    new Models.FilterModel<number>([] as Models.FilterValueModel[], 'Subject', 'Subject', false, true, onSubjectChange),
                    new Models.FilterModel<number>([] as Models.FilterValueModel[], 'School Year', 'SchoolYear', false, true),
                    new Models.FilterModel<number>([] as Models.FilterValueModel[], 'Grades', 'Assessments', true, true),
                    new Models.FilterModel<number>([] as Models.FilterValueModel[], 'Performance Levels', 'PerformanceLevels', true),
                    new Models.FilterModel<number>([] as Models.FilterValueModel[], 'Good Cause Excemptions', 'GoodCauseExcemptions', true),
                    new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                    new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                    new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                    new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                ];

                var charts = [
                    new PieChartModel('assessment', 'byPerformanceLevel', true),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelByEthnicity', true),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelByLunchStatus', true),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelByEnglishLanguageLearner', true),
                    new PercentageTotalBarChartModel('assessment', 'performanceLevelBySpecialEducation', true),
                    new PercentageTotalBarChartModel('assessment', 'byGoodCause', true)
                ];

                return {
                    filters: filters,
                    charts: charts,
                    title: 'Assessment',
                    model: new Models.AssessmentFilterModel()
                }
            }],
            assessments: ['api', (api: IApi) => {
                return api.assessmentFilters.getAssessments();
            }],
            englishLanguageLearnerStatuses: ['api', (api: IApi) => {
                return api.assessmentFilters.getEnglishLanguageLearnerStatuses();
            }],
            ethnicities: ['api', (api: IApi) => {
                return api.assessmentFilters.getEthnicities();
            }],
            lunchStatuses: ['api', (api: IApi) => {
                return api.assessmentFilters.getLunchStatuses();
            }],
            specialEducationStatuses: ['api', (api: IApi) => {
                return api.assessmentFilters.getSpecialEducationStatuses();
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