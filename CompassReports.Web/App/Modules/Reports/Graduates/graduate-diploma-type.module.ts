/// <reference path="graduates.module.ts" />

module App.Reports.Graduate.DiplomaType  {
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class GraduateDiplomaTypeConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PercentageTotalBarChartModel('graduateDiplomaType', 'byEthnicity'),
                new PercentageTotalBarChartModel('graduateDiplomaType', 'byLunchStatus'),
                new PercentageTotalBarChartModel('graduateDiplomaType', 'byEnglishLanguageLearner'),
                new PercentageTotalBarChartModel('graduateDiplomaType', 'bySpecialEducation')
            ];

            $stateProvider.state('app.reports.graduate-diploma-type',
                {
                    url: '/graduate-diploma-type',
                    views: {
                        'report@app.reports': new GraduatesReportView(settings, 'Diploma Types', charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.graduates.diploma-type', [])
        .config(GraduateDiplomaTypeConfig);
}