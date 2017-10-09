module App.Directive.Charts {

    interface IPercentageTotalChart extends ng.IScope {
        chart: Models.PercentageTotalBarChartModel,
        dataSetOverride: any;
        data: number[][];
        model: Models.IReportFilterModel;
        togglePercentage: () => void;
    }

    class PercentageTotalChartController {
        static $inject = ['$rootScope', '$scope', 'api', 'services'];

        resetColors = () => {
            if (this.scope.chart.Data && this.scope.chart.Data.length) {
                this.scope.chart.Colors = this.services.colorGradient.getHexColors(this.scope.chart.Data.length);
            }
        }

        togglePercentage = () => {
            this.scope.chart.ShowPercentage = !this.scope.chart.ShowPercentage;
            this.updatePercentage();
        }

        updateChart = () => {
            this.api[this.scope.chart.ApiCall][this.scope.chart.ChartCall](this.scope.model)
                .then((result: Models.PercentageTotalBarChartModel) => {
                    this.scope.chart.Update(result);
                    this.updatePercentage();
                    this.resetColors();
            });
        }

        updatePercentage = () => {
            var data = [];
            angular.forEach(this.scope.chart.Data, (seriesValues: Models.PercentageTotalData[]) => {
                var seriesData = [];
                angular.forEach(seriesValues, (value: Models.PercentageTotalData) => {
                    if (this.scope.chart.ShowPercentage) seriesData.push(value.Percentage);
                    else seriesData.push(value.Total);
                });
                data.push(seriesData);
            });

            this.scope.data = data;
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: IPercentageTotalChart,
            private readonly api: IApi,
            private readonly services: IServices) {

            rootScope.$on('theme-change', this.resetColors);
            rootScope.$on('update-charts', this.updateChart);

            scope.togglePercentage = this.togglePercentage;
            this.updateChart();
        }
    }

    function percentageTotalChartDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                chart: '=',
                model: '='
            },
            templateUrl: `${settings.directiveBaseUri}/charts/percentage-total-chart/percentage-total-chart.view.html`,
            controller: PercentageTotalChartController
        }
    }

    angular
        .module('app.directives.charts.percentage-total', [])
        .directive('percentageTotalChart', ['settings', percentageTotalChartDirective]);
}