/// <reference path="./college-readiness-trends.module.ts" />

module App.Reports.CollegeReadinessTrends.Took {

    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class CollegeReadinessTrendsTookConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const chartResolve = ['$stateParams', 'performanceLevels', ($stateParams: any, performanceLevels: Models.FilterValueModel[]) => {
                var charts = [new PercentageTotalBarChartModel('assessmentTakingTrend', 'get')];

                angular.forEach(performanceLevels, (performanceLevel: Models.FilterValueModel) => {
                    var display = (performanceLevel.Display as string).toLowerCase();
                    if (display.indexOf('take') !== -1 || display.indexOf('took') !== -1) {
                        charts.push(new PercentageTotalBarChartModel(
                            'assessmentTakingTrend',
                            'get',
                            {
                                name: 'app.reports.college-readiness-trends-took-details',
                                parameters: {
                                    assessmentTitle: $stateParams.assessmentTitle,
                                    performanceKey: performanceLevel.Value
                                }
                            },
                            { PerformanceKey: performanceLevel.Value }));
                    }
                });

                return charts;
            }];

            $stateProvider.state('app.reports.college-readiness-trends-took',
                {
                    url: '/college-readiness-trends/took?assessmentTitle',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings, true, true, chartResolve)
                    }
                });
        }
    }

    class CollegeReadinessTrendsTookDetailsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            const chartResolve = ['$stateParams', ($stateParams: any) => {
                var charts = [
                    new PercentageTotalBarChartModel('assessmentTakingTrend', 'byEthnicity', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentTakingTrend', 'byLunchStatus', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentTakingTrend', 'byEnglishLanguageLearner', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentTakingTrend', 'bySpecialEducation', null, { PerformanceKey: $stateParams.performanceKey })
                ];
                return charts;
            }];

            $stateProvider.state('app.reports.college-readiness-trends-took-details',
                {
                    url: '/college-readiness-trends/took-details?assessmentTitle&performanceKey',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings, true, false, chartResolve)
                    }
                });
        }
    }

    angular
        .module('app.reports.college-readiness-trends.took', [])
        .config(CollegeReadinessTrendsTookConfig)
        .config(CollegeReadinessTrendsTookDetailsConfig);
}