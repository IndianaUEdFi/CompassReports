/// <reference path="../Assessment-Report-View/assessment-report-view.module.ts" />

module App.Reports.Assessment {

    import PieChartModel = Models.PieChartModel;
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class AssessmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const charts = [
                new PieChartModel('assessmentPerformance', 'get'),
                new PercentageTotalBarChartModel('assessmentPerformance', 'byEthnicity'),
                new PercentageTotalBarChartModel('assessmentPerformance', 'byLunchStatus'),
                new PercentageTotalBarChartModel('assessmentPerformance', 'byEnglishLanguageLearner'),
                new PercentageTotalBarChartModel('assessmentPerformance', 'bySpecialEducation'),
                new PercentageTotalBarChartModel('assessment', 'byGoodCause')
            ];

            $stateProvider.state('app.reports.assessment',
                {
                    url: '/assessment?assessmentTitle',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings, false, false, charts)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment', [])
        .config(AssessmentConfig);
}