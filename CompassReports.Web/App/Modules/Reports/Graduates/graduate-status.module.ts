/// <reference path="graduates.module.ts" />

module App.Reports.Graduate.Status  {
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class GraduateStatusConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PercentageTotalBarChartModel('graduateStatus', 'byEthnicity'),
                new PercentageTotalBarChartModel('graduateStatus', 'byLunchStatus'),
                new PercentageTotalBarChartModel('graduateStatus', 'byEnglishLanguageLearner'),
                new PercentageTotalBarChartModel('graduateStatus', 'bySpecialEducation')
            ];

            $stateProvider.state('app.reports.graduate-status',
                {
                    url: '/graduate-status',
                    views: {
                        'report@app.reports': new GraduatesReportView(settings, 'Graduation Status', charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.graduates.status', [])
        .config(GraduateStatusConfig);
}