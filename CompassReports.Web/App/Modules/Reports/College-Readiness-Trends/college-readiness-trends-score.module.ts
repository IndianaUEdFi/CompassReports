/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.CollegeReadinessTrends.Score {

    import BarChartModel = Models.BarChartModel

    class CollegeReadinessTrendsTookConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const view = new CollegeReadinessTrendsReportView(settings);
            view.resolve.charts = [() => {
                return [
                    new BarChartModel('assessmentScoreTrend', 'get'),
                    new BarChartModel('assessmentScoreTrend', 'byEthnicity'),
                    new BarChartModel('assessmentScoreTrend', 'byLunchStatus'),
                    new BarChartModel('assessmentScoreTrend', 'byEnglishLanguageLearner'),
                    new BarChartModel('assessmentScoreTrend', 'bySpecialEducation')
                ];
             }];

            $stateProvider.state('app.reports.college-readiness-trends-score',
                {
                    url: '/college-readiness-trends/score?assessmentTitle',
                    views: {
                        'report@app.reports': view
                    }
                });
        }
    }

    angular
        .module('app.reports.college-readiness-trends.score', [])
        .config(CollegeReadinessTrendsTookConfig);
}