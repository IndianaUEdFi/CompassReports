/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.CollegeReadiness.Took {

    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class CollegeReadinessTookConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PieChartModel('assessmentTaking', 'get'),
                new PercentageTotalBarChartModel('assessmentTaking', 'byEthnicity'),
                new PercentageTotalBarChartModel('assessmentTaking', 'byLunchStatus'),
                new PercentageTotalBarChartModel('assessmentTaking', 'byEnglishLanguageLearner'),
                new PercentageTotalBarChartModel('assessmentTaking', 'bySpecialEducation')
            ];

            $stateProvider.state('app.reports.college-readiness-took',
                {
                    url: '/college-readiness/took?assessmentTitle',
                    views: {
                        'report@app.reports': new CollegeReadinessReportView(settings, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.college-readiness.took', [])
        .config(CollegeReadinessTookConfig);
}