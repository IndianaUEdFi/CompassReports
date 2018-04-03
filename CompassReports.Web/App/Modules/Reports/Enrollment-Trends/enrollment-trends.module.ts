/// <reference path="../Enrollment-Report-View/enrollment-report-view.module.ts" />

module App.Reports.EnrollmentTrends {

    import BarChartModel = Models.BarChartModel;

    class EnrollmentTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new BarChartModel('enrollmentTrends', 'byGrade'),
                new BarChartModel('enrollmentTrends', 'byEthnicity'),
                new BarChartModel('enrollmentTrends', 'byLunchStatus'),
                new BarChartModel('enrollmentTrends', 'bySpecialEducation'),
                new BarChartModel('enrollmentTrends', 'byEnglishLanguageLearner')
            ];

            $stateProvider.state('app.reports.enrollment-trends',
                {
                    url: '/enrollment-trends',
                    views: {
                        'report@app.reports': new EnrollmentReportView(settings, 'Enrollment Trends', true, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.enrollment-trends', [])
        .config(EnrollmentTrendsConfig);
}