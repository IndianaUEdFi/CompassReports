/// <reference path="../Enrollment-Report-View/enrollment-report-view.module.ts" />

module App.Reports.Attendance {

    import BarChartModel = Models.BarChartModel;

    class AttendanceConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new BarChartModel('attendance', 'byGrade'),
                new BarChartModel('attendance', 'byEthnicity'),
                new BarChartModel('attendance', 'byLunchStatus'),
                new BarChartModel('attendance', 'bySpecialEducation'),
                new BarChartModel('attendance', 'byEnglishLanguageLearner')
            ];


            $stateProvider.state('app.reports.attendance',
                {
                    url: '/attendance',
                    views: {
                        'report@app.reports': new EnrollmentReportView(settings, 'Attendance', false, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.attendance', [])
        .config(AttendanceConfig);
}