﻿module App.Directive.Charts {

    interface IPieChartScope extends ng.IScope {
        chart: Models.PieChartModel,
        model: Models.IReportFilterModel;
        dataSetOverride: any;
        togglePercentage: () => void;
    }

    class PieChartController {
        static $inject = ['$rootScope', '$scope', 'api', 'services'];

        resetColors = () => {
            if (this.scope.chart.Data && this.scope.chart.Data.length) {
                this.scope.chart.Colors = this.services.colorGradient.getHexColors(this.scope.chart.Data.length);
            }
        }

        togglePercentage = () => this.scope.chart.ShowPercentage = !this.scope.chart.ShowPercentage;

        updateChart = () => {
            this.api[this.scope.chart.ApiCall][this.scope.chart.ChartCall](this.scope.model)
                .then((result: Models.PieChartModel) => {
                    this.scope.chart.Update(result);
                    this.resetColors();
                });
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: IPieChartScope,
            private readonly api: IApi,
            private readonly services: IServices) {

            rootScope.$on('theme-change', this.resetColors);
            rootScope.$on('update-charts', this.updateChart);

            this.scope.togglePercentage = this.togglePercentage;

            if (!scope.chart.DelayDataCall)
                this.updateChart();
        }
    }

    function pieChartDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                chart: '=',
                model: '='
            },
            templateUrl: `${settings.directiveBaseUri}/charts/pie-chart/pie-chart.view.html`,
            controller: PieChartController
        }
    }

    angular
        .module('app.directives.charts.pie', [])
        .directive('pieChart', ['settings', pieChartDirective]);
}