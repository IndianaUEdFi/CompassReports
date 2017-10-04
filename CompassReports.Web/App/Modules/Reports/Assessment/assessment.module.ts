/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.Assessment {
    import BarChartModel = Models.BarChartModel;

    class AssessmentReportView extends ReportBaseView {
        resolve = {
            report: [() => {
                var filters = [];

                var charts = [];

                return {
                    filters: filters,
                    charts: charts,
                    title: 'Assessment',
                    model: {
                        isFiltering: () => { return false },
                        reset: () => { }
                    }
                }
            }]
        }
    }

    class AssessmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.assessment',
                {
                    url: '/assessment',
                    views: {
                        'report@app.reports': new AssessmentReportView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment', [])
        .config(AssessmentConfig);
}