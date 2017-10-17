/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.Enrollment {
    import PieChartModel = Models.PieChartModel;

    class EnrollmentReportView extends ReportBaseView {
        resolve = {
            report: ['englishLanguageLearnerStatuses', 'ethnicities', 'grades',
                'lunchStatuses', 'specialEducationStatuses', 'schoolYears', (
                    englishLanguageLearnerStatuses: string[], ethnicities: string[], grades: string[],
                    lunchStatuses: string[], specialEducationStatuses: string[], schoolYears: Models.FilterValueModel[]
                ) => {
                    var filters = [
                        new Models.FilterModel<number>(schoolYears, 'School Year', 'SchoolYear', false),
                        new Models.FilterModel<number>(grades, 'Grades', 'Grades', true),
                        new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                        new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                        new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                        new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                    ];

                    var charts = [
                        new PieChartModel('enrollment', 'byGrade'),
                        new PieChartModel('enrollment', 'byEthnicity'),
                        new PieChartModel('enrollment', 'byLunchStatus'),
                        new PieChartModel('enrollment', 'bySpecialEducation'),
                        new PieChartModel('enrollment', 'byEnglishLanguageLearner')
                    ];

                    return {
                        filters: filters,
                        charts: charts,
                        title: 'Enrollment',
                        model: new Models.EnrollmentFilterModel(schoolYears[0].Value as number)
                    }
                }],
            englishLanguageLearnerStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getEnglishLanguageLearnerStatuses();
            }],
            ethnicities: ['api', (api: IApi) => {
                return api.demographicFilters.getEthnicities();
            }],
            grades: ['api', (api: IApi) => {
                return api.demographicFilters.getGrades();
            }],
            lunchStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getLunchStatuses();
            }],
            specialEducationStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getSpecialEducationStatuses();
            }],
            schoolYears: ['api', (api: IApi) => {
                return api.enrollmentFilters.getSchoolYears();
            }]
        }
    }

    class EnrollmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.enrollment',
                {
                    url: '/enrollment',
                    views: {
                        'report@app.reports': new EnrollmentReportView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.enrollment', [])
        .config(EnrollmentConfig);
}