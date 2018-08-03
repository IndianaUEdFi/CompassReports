/// <reference path="../Graduates-Report-View/graduates-report-view.module.ts" />

module App.Reports.GraduateTrends {

    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class GraduateTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PercentageTotalBarChartModel('graduateTrends', 'byStatus'),
                new PercentageTotalBarChartModel('graduateTrends', 'byWaiver')
            ];

            $stateProvider.state('app.reports.graduate-trends',
                {
                    url: '/graduate-trends',
                    views: {
                        'report@app.reports': new GraduatesReportView(settings, 'Graduate Trends', true, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.graduate-trends', [])
        .config(GraduateTrendsConfig);
}