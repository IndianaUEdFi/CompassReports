/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.CollegeReadiness.Scores {

    import BarChartModel = Models.BarChartModel;

    class CollegeReadinessScoresConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new BarChartModel('assessmentScores', 'get'),
                new BarChartModel('assessmentScores', 'byEthnicity'),
                new BarChartModel('assessmentScores', 'byLunchStatus'),
                new BarChartModel('assessmentScores', 'byEnglishLanguageLearner'),
                new BarChartModel('assessmentScores', 'bySpecialEducation')
            ];

            $stateProvider.state('app.reports.college-readiness-scores',
                {
                    url: '/college-readiness/scores?assessmentTitle',
                    views: {
                        'report@app.reports': new CollegeReadinessReportView(settings, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.college-readiness.scores', [])
        .config(CollegeReadinessScoresConfig);
}