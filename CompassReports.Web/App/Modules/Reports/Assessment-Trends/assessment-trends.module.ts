/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.AssessmentTrends {
    import BarChartModel = Models.BarChartModel;

    class AssessmentTrendsReportView extends ReportBaseView {
        resolve = {
            report: [() => {
                var filters = [];

                var charts = [];

                return {
                    filters: filters,
                    charts: charts,
                    title: 'Assessment Trends',
                    model: {
                        isFiltering: () => { return false },
                        reset: () => { }
                    }
                }
            }]
        }
    }

    class AssessmentTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.assessment-trends',
                {
                    url: '/assessment-trends',
                    views: {
                        'report@app.reports': new AssessmentTrendsReportView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment-trends', [])
        .config(AssessmentTrendsConfig);
}