module App.Directive.Charts {

    interface ILineChartScope extends ng.IScope {
        chart: Models.BarChartModel,
        dataSetOverride: any;
        model: Models.IReportFilterModel;
    }

    class LineChartController {
        static $inject = ['$rootScope', '$scope', 'api', 'services'];

        resetColors = () => {
            if (this.scope.chart.Data && this.scope.chart.Data.length)
                this.scope.chart.Colors = this.services.colorGradient.getHexColors(this.scope.chart.Data.length);
        }

        setDataSetOverrride = () => {
            this.scope.dataSetOverride = [];
            angular.forEach(this.scope.chart.Data, set => {
                this.scope.dataSetOverride.push({
                    fill: false,
                    lineTension: 0
                });
            });
        }

        updateChart = () => {
            this.api[this.scope.chart.ApiCall][this.scope.chart.ChartCall](this.scope.model)
                .then((result: Models.BarChartModel) => {
                    this.scope.chart.Update(result);
                    this.setDataSetOverrride();
                    this.resetColors();
            });
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: ILineChartScope,
            private readonly api: IApi,
            private readonly services: IServices) {

            rootScope.$on('theme-change', this.resetColors);
            rootScope.$on('update-charts', this.updateChart);

            this.updateChart();
        }
    }

    function lineChartDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                chart: '=',
                model: '='
            },
            templateUrl: `${settings.directiveBaseUri}/charts/line-chart/line-chart.view.html`,
            controller: LineChartController
        }
    }

    angular
        .module('app.directives.charts.line', [])
        .directive('lineChart', ['settings', lineChartDirective]);
}