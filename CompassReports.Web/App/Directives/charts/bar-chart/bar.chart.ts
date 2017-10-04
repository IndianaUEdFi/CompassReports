module App.Directive.Charts {

    interface IBarChartScope extends ng.IScope {
        chart: Models.BarChartModel<number>,
        dataSetOverride: any;
        model: Models.IReportFilterModel;
    }

    class BarChartController {
        static $inject = ['$rootScope', '$scope', 'api', 'services'];

        resetColors = () => {
            if (this.scope.chart.Data && this.scope.chart.Data.length) {

                let hexColors = [];
                if (this.scope.chart.SingleSeries) {
                    hexColors = this.services.colorGradient.getHexColors(this.scope.chart.Data[0].length);

                    const backgroundColors = [];
                    const colors = this.services.colorGradient.getRgbColors(this.scope.chart.Data[0].length);
                    angular.forEach(colors, color => {
                        backgroundColors.push(`rgba(${color.r},${color.g},${color.b}, .3)`);
                    });

                    this.scope.dataSetOverride = [{
                        borderColor: hexColors,
                        backgroundColor: backgroundColors
                    }];

                } else {
                    hexColors = this.services.colorGradient.getHexColors(this.scope.chart.Data.length);
                }

                this.scope.chart.Colors = hexColors;
            }
        }

        updateChart = () => {
            this.api[this.scope.chart.ApiCall][this.scope.chart.ChartCall](this.scope.model)
                .then((result: Models.BarChartModel<number>) => {
                    this.scope.chart.Update(result);
                    this.resetColors();
            });
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: IBarChartScope,
            private readonly api: IApi,
            private readonly services: IServices) {

            rootScope.$on('theme-change', this.resetColors);
            rootScope.$on('update-charts', this.updateChart);

            this.updateChart();
        }
    }

    function barChartDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                chart: '=',
                model: '='
            },
            templateUrl: `${settings.directiveBaseUri}/charts/bar-chart/bar-chart.view.html`,
            controller: BarChartController
        }
    }

    angular
        .module('app.directives.charts.bar', [])
        .directive('barChart', ['settings', barChartDirective]);
}