/// <reference path="./college-readiness.module.ts" />

//module App.Reports.CollegeReadiness.Took {

//    import PieChartModel = Models.PieChartModel;
//    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

//    class CollegeReadinessTookConfig {
//        static $inject = ['$stateProvider', 'settings'];

//        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

//            const charts = [
//                new PieChartModel('assessmentTakingTrend', 'get'),
//                new PercentageTotalBarChartModel('assessmentTakingTrend', 'byEthnicity'),
//                new PercentageTotalBarChartModel('assessmentTakingTrend', 'byLunchStatus'),
//                new PercentageTotalBarChartModel('assessmentTakingTrend', 'byEnglishLanguageLearner'),
//                new PercentageTotalBarChartModel('assessmentTakingTrend', 'bySpecialEducation')
//            ];

//            $stateProvider.state('app.reports.college-readiness-took',
//                {
//                    url: '/college-readiness/took?assessmentTitle',
//                    views: {
//                        'report@app.reports': new AssessmentReportView(settings, false, false, charts)
//                    }
//                });
//        }
//    }

//    angular
//        .module('app.reports.college-readiness.took', [])
//        .config(CollegeReadinessTookConfig);
//}