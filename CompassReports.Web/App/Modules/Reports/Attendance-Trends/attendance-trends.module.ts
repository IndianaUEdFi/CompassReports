/// <reference path="../Enrollment-Report-View/enrollment-report-view.module.ts" />

module App.Reports.AttendanceTrends {

    import LineChartModel = Models.LineChartModel;

    class AttendanceTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {
       
            const charts = [
                new LineChartModel('attendanceTrends', 'byGrade'),
                new LineChartModel('attendanceTrends', 'byEthnicity'),
                new LineChartModel('attendanceTrends', 'byLunchStatus'),
                new LineChartModel('attendanceTrends', 'bySpecialEducation'),
                new LineChartModel('attendanceTrends', 'byEnglishLanguageLearner')
            ];

            $stateProvider.state('app.reports.attendance-trends',
                {
                    url: '/attendance-trends',
                    views: {
                        'report@app.reports': new EnrollmentReportView(settings, 'Attendance Trends', false, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.attendance-trends', [])
        .config(AttendanceTrendsConfig);
}