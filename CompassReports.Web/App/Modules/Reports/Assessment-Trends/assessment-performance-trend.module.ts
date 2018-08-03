/// <reference path="../Assessment-Report-View/assessment-report-view.module.ts" />

module App.Reports.AssessmentPerformanceTrend {

    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel;

    class AssessmentPerformanceTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const chartResolve = ['$stateParams', ($stateParams: IAssessmentParams) => {
                return [
                    new PercentageTotalBarChartModel('assessmentPerformanceTrend', 'byEthnicity', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentPerformanceTrend', 'byLunchStatus', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentPerformanceTrend', 'byEnglishLanguageLearner', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentPerformanceTrend', 'bySpecialEducation', null, { PerformanceKey: $stateParams.performanceKey })
                ];
            }];

            $stateProvider.state('app.reports.assessment-performance-trend',
                {
                    url: '/assessment-peformance-trend?assessmentTitle&performanceKey',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings, true, false, chartResolve)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment-trends.performance', [])
        .config(AssessmentPerformanceTrendsConfig);
}