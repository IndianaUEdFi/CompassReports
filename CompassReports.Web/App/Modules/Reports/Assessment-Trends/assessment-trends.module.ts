/// <reference path="../Assessment-Report-View/assessment-report-view.module.ts" />

module App.Reports.AssessmentTrends {

    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel;


    class AssessmentTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const chartResolve = ['$stateParams', 'performanceLevels',
                ($stateParams: IAssessmentParams, performanceLevels: Models.FilterValueModel[]) => {

                    const charts = [
                        new PercentageTotalBarChartModel('assessmentPerformanceTrend', 'get')
                    ];

                    angular.forEach(performanceLevels, (level) => {
                        charts.push(new PercentageTotalBarChartModel(
                            'assessmentPerformanceTrend',
                            'get',
                            {
                                name: 'app.reports.assessment-performance-trend',
                                parameters: {
                                    assessmentTitle: $stateParams.assessmentTitle,
                                    performanceKey: level.Value
                                }
                            },
                            { PerformanceKey: level.Value }));
                    });

                    return charts;
                }];

            $stateProvider.state('app.reports.assessment-trends',
                {
                    url: '/assessment-trends?assessmentTitle',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings, true, true, chartResolve)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment-trends', ['app.reports.assessment-trends.performance'])
        .config(AssessmentTrendsConfig);
}