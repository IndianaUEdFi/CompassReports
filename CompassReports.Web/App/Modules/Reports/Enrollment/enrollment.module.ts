/// <reference path="../Enrollment-Report-View/enrollment-report-view.module.ts" />

module App.Reports.Enrollment {

    import PieChartModel = Models.PieChartModel;

    class EnrollmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PieChartModel('enrollment', 'byGrade'),
                new PieChartModel('enrollment', 'byEthnicity'),
                new PieChartModel('enrollment', 'byLunchStatus'),
                new PieChartModel('enrollment', 'bySpecialEducation'),
                new PieChartModel('enrollment', 'byEnglishLanguageLearner')
            ];

            $stateProvider.state('app.reports.enrollment',
                {
                    url: '/enrollment',
                    views: {
                        'report@app.reports': new EnrollmentReportView(settings, 'Enrollment', false, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.enrollment', [])
        .config(EnrollmentConfig);
}