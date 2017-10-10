/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.CollegeReadinessTrends.Pass {

    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel

    class CollegeReadinessTrendsPassConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            let view = new CollegeReadinessTrendsReportView(settings);
            view.resolve.charts = ['$stateParams', 'performanceLevels', ($stateParams: any, performanceLevels: Models.FilterValueModel[]) => {
                var charts = [new PercentageTotalBarChartModel('assessmentPassTrend', 'get') ];

                angular.forEach(performanceLevels, (performanceLevel: Models.FilterValueModel) => {
                    var display = (performanceLevel.Display as string).toLowerCase();
                    if (display.indexOf('pass') !== -1 || display.indexOf('fail') !== -1) {
                        charts.push(new PercentageTotalBarChartModel(
                            'assessmentPassTrend',
                            'get',
                            {
                                name: 'app.reports.college-readiness-trends-pass-details',
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

            $stateProvider.state('app.reports.college-readiness-trends-pass',
                {
                    url: '/college-readiness-trends/pass?assessmentTitle',
                    views: {
                        'report@app.reports': view
                    }
                });
        }
    }

    class CollegeReadinessTrendsPassDetailsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            let view = new CollegeReadinessTrendsReportView(settings);
            view.resolve.charts = ['$stateParams', ($stateParams: any) => {
                var charts = [
                    new PercentageTotalBarChartModel('assessmentPassTrend', 'byEthnicity', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentPassTrend', 'byLunchStatus', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentPassTrend', 'byEnglishLanguageLearner', null, { PerformanceKey: $stateParams.performanceKey }),
                    new PercentageTotalBarChartModel('assessmentPassTrend', 'bySpecialEducation', null, { PerformanceKey: $stateParams.performanceKey })
                ];
                return charts;
            }];

            $stateProvider.state('app.reports.college-readiness-trends-pass-details',
                {
                    url: '/college-readiness-trends/pass-details?assessmentTitle&performanceKey',
                    views: {
                        'report@app.reports': view
                    }
                });
        }
    }

    angular
        .module('app.reports.college-readiness-trends.pass', [])
        .config(CollegeReadinessTrendsPassConfig)
        .config(CollegeReadinessTrendsPassDetailsConfig);
}