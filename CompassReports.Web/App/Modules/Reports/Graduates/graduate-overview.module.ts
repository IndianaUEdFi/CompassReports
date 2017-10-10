/// <reference path="graduates.module.ts" />

module App.Reports.Graduate.Overview {
    import PieChartModel = Models.PieChartModel;

    class GraduateOverviewConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PieChartModel('graduateStatus', 'get', {name: 'app.reports.graduate-status'}),
                new PieChartModel('graduateWaivers', 'get', { name: 'app.reports.graduate-waivers' }),
                new PieChartModel('graduateDiplomaType', 'get', {name: 'app.reports.graduate-diploma-type'})
            ];

            $stateProvider.state('app.reports.graduates',
                {
                    url: '/graduates',
                    views: {
                        'report@app.reports': new GraduatesReportView(settings, 'Graduates', charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.graduates.overview', [])
        .config(GraduateOverviewConfig);
}