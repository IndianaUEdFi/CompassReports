/// <reference path="./college-readiness.module.ts" />

module App.Reports.CollegeReadiness.Pass {

    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class CollegeReadinessPassConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PieChartModel('assessmentPass', 'get'),
                new PercentageTotalBarChartModel('assessmentPass', 'byEthnicity'),
                new PercentageTotalBarChartModel('assessmentPass', 'byLunchStatus'),
                new PercentageTotalBarChartModel('assessmentPass', 'byEnglishLanguageLearner'),
                new PercentageTotalBarChartModel('assessmentPass', 'bySpecialEducation')
            ];

            $stateProvider.state('app.reports.college-readiness-pass',
                {
                    url: '/college-readiness/pass?assessmentTitle',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings, false, false, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.college-readiness.pass', [])
        .config(CollegeReadinessPassConfig);
}