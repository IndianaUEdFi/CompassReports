/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.Graduate {
    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    function onExpectedGraduationYearChange(model: Models.GraduateFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.ExpectedGraduationYear) {

            model.CohortYear = null;

            filters[1].update([] as Models.FilterValueModel[]);

            api.graduateFilters.getCohorts(model.ExpectedGraduationYear).then((cohorts: Models.FilterValueModel[]) => {
                filters[1].update(cohorts);
                if (cohorts.length)
                    model.CohortYear = cohorts[0].Value as number;
            });
        }
    }

    export class GraduatesReportView extends ReportBaseView {
        resolve = {
            report: ['$rootScope', 'schoolYears', 'cohorts',
                'englishLanguageLearnerStatuses', 'ethnicities', 'lunchStatuses',
                'specialEducationStatuses',
                (rootScope: IAppRootScope, schoolYears: Models.FilterValueModel[], cohorts: Models.FilterValueModel[],
                    englishLanguageLearnerStatuses: string[], ethnicities: string[], lunchStatuses: string[],
                    specialEducationStatuses: string[]) => {
                const filters = [
                    new Models.FilterModel<number>(schoolYears, 'Expected Graduation Year', 'ExpectedGraduationYear', false, true, onExpectedGraduationYearChange),
                    new Models.FilterModel<number>(cohorts, 'Cohort', 'CohortYear', false, true),
                    new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                    new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                    new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                    new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                ];

                let model = new Models.GraduateFilterModel();
                if (rootScope.filterModel) {
                    model = rootScope.filterModel as Models.GraduateFilterModel;
                    rootScope.filterModel = null;
                } else {
                    model.ExpectedGraduationYear = (schoolYears && schoolYears.length) ? schoolYears[0].Value as number : null;
                    model.CohortYear = (cohorts && cohorts.length) ? cohorts[0].Value as number : null;   
                }

                let backState = null;
                if (rootScope.backState) {
                    backState = rootScope.backState;
                    rootScope.backState = null;
                }


                return {
                    filters: filters,
                    charts: this.charts,
                    title: this.title,
                    model: model,
                    backState: backState
                }
            }],
            schoolYears: ['api', (api: IApi) => {
                return api.graduateFilters.getSchoolYears();
            }],
            cohorts: ['$rootScope', 'api', 'schoolYears', (rootScope: IAppRootScope, api: IApi, schoolYears: Models.FilterValueModel[]) => {
                if (rootScope.filterModel) {
                    const model = rootScope.filterModel as Models.GraduateFilterModel;
                    return api.graduateFilters.getCohorts(model.ExpectedGraduationYear);
                } else if (schoolYears && schoolYears.length) {
                    return api.graduateFilters.getCohorts(schoolYears[0].Value as number);
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

        constructor(settings: ISystemSettings,
            public title: string,
            public charts: Models.ChartModel[]) {
            super(settings);
        }
    }

    angular
        .module('app.reports.graduates', [
            'app.reports.graduates.diploma-type',
            'app.reports.graduates.overview',
            'app.reports.graduates.status',
            'app.reports.graduates.waviers'
        ]);
}