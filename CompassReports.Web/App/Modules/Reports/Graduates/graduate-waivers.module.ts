/// <reference path="graduates.module.ts" />

module App.Reports.Graduate.Waviers  {
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class GraduateWaviersConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PercentageTotalBarChartModel('graduateWaivers', 'byEthnicity'),
                new PercentageTotalBarChartModel('graduateWaivers', 'byLunchStatus'),
                new PercentageTotalBarChartModel('graduateWaivers', 'byEnglishLanguageLearner'),
                new PercentageTotalBarChartModel('graduateWaivers', 'bySpecialEducation')
            ];

            $stateProvider.state('app.reports.graduate-waivers',
                {
                    url: '/graduate-waviers',
                    views: {
                        'report@app.reports': new GraduatesReportView(settings, 'Graduation Waviers', false, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.graduates.waviers', [])
        .config(GraduateWaviersConfig);
}