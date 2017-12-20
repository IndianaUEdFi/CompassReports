/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.GraduateTrends {
    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class GraduateTrendsReportView extends ReportBaseView {
        resolve = {
            report: ['schoolYears', 'cohorts',
                'englishLanguageLearnerStatuses', 'ethnicities', 'lunchStatuses',
                'specialEducationStatuses',
                (schoolYears: Models.FilterValueModel[], cohorts: Models.FilterValueModel[],
                    englishLanguageLearnerStatuses: string[], ethnicities: string[], lunchStatuses: string[],
                    specialEducationStatuses: string[]) => {

                const filters = [
                    new Models.FilterModel<number>(schoolYears, 'Expected Graduation Years', 'ExpectedGraduationYears', true, true),
                    new Models.FilterModel<number>(cohorts, 'Cohort', 'GradCohortYearDifference', false, true),
                    new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                    new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                    new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                    new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                ];

                const charts = [
                    new PercentageTotalBarChartModel('graduateTrends', 'byStatus'),
                    new PercentageTotalBarChartModel('graduateTrends', 'byWaiver')
                ];

                const model = new Models.GraduateFilterModel();
                if (cohorts.length) model.GradCohortYearDifference = cohorts[0].Value as number;

                return {
                    filters: filters,
                    charts: charts,
                    title: 'Graduate Trends',
                    model: model
                }
            }],
            schoolYears: ['api', (api: IApi) => {
                return api.graduateFilters.getSchoolYears();
            }],
            cohorts: ['api', (api: IApi) => {
                return api.graduateFilters.getCohorts();
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


    class GraduateTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.graduate-trends',
                {
                    url: '/graduate-trends',
                    views: {
                        'report@app.reports': new GraduateTrendsReportView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.graduate-trends', [])
        .config(GraduateTrendsConfig);
}