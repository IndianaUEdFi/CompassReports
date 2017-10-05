/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.EnrollmentTrends {
    import BarChartModel = Models.BarChartModel;

    class EnrollmentTrendsView extends ReportBaseView {
        resolve = {
            report: ['englishLanguageLearnerStatuses', 'ethnicities', 'grades',
                'lunchStatuses', 'specialEducationStatuses', 'schoolYears', (
                    englishLanguageLearnerStatuses: string[], ethnicities: string[], grades: string[],
                    lunchStatuses: string[], specialEducationStatuses: string[], schoolYears: Models.FilterValueModel[]
                ) => {
                    var filters = [
                        new Models.FilterModel<number>(schoolYears, 'School Years', 'SchoolYears', true),
                        new Models.FilterModel<number>(grades, 'Grade Levels', 'Grades', true),
                        new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                        new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                        new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                        new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                    ];

                    var charts = [
                        new BarChartModel('enrollmentTrends', 'byGrade'),
                        new BarChartModel('enrollmentTrends', 'byEthnicity'),
                        new BarChartModel('enrollmentTrends', 'byLunchStatus'),
                        new BarChartModel('enrollmentTrends', 'bySpecialEducation'),
                        new BarChartModel('enrollmentTrends', 'byEnglishLanguageLearner')
                    ];

                    return {
                        filters: filters,
                        charts: charts,
                        title: 'Enrollment Trends',
                        model: new Models.EnrollmentFilterModel()
                    }
                }],
            englishLanguageLearnerStatuses: ['api', (api: IApi) => {
                return api.enrollmentFilters.getEnglishLanguageLearnerStatuses();
            }],
            ethnicities: ['api', (api: IApi) => {
                return api.enrollmentFilters.getEthnicities();
            }],
            grades: ['api', (api: IApi) => {
                return api.enrollmentFilters.getGrades();
            }],
            lunchStatuses: ['api', (api: IApi) => {
                return api.enrollmentFilters.getLunchStatuses();
            }],
            specialEducationStatuses: ['api', (api: IApi) => {
                return api.enrollmentFilters.getSpecialEducationStatuses();
            }],
            schoolYears: ['api', (api: IApi) => {
                return api.enrollmentFilters.getSchoolYears();
            }]
        }
    }

    class EnrollmentTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.enrollment-trends',
                {
                    url: '/enrollment-trends',
                    views: {
                        'report@app.reports': new EnrollmentTrendsView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.enrollment-trends', [])
        .config(EnrollmentTrendsConfig);
}