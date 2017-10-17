module App.Directive.Charts {

    interface IPercentageTotalChart extends ng.IScope {
        chart: Models.PercentageTotalBarChartModel,
        dataSetOverride: any;
        data: number[][];
        loading: boolean;
        model: Models.IReportFilterModel;
        togglePercentage: () => void;
        viewDetails: () => void;
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
            var model = {};

            for (let key in this.scope.model) {
                if(typeof this.scope.model[key] !== 'function')
                    model[key] = this.scope.model[key];
            }

            if (this.scope.chart.ChartFilters) {
                for (let key in this.scope.chart.ChartFilters) {
                    model[key] = this.scope.chart.ChartFilters[key];
                }
            }

            this.scope.loading = true;

            this.api[this.scope.chart.ApiCall][this.scope.chart.ChartCall](model)
                .then((result: Models.PercentageTotalBarChartModel) => {
                    this.scope.chart.Update(result);
                    this.updatePercentage();
                    this.resetColors();
                }).finally(() => {
                    this.scope.loading = false;
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

        viewDetails = () => {
            this.rootScope.backState = this.services.state.current.name;
            this.rootScope.backParameters = this.services.state.params;
            this.rootScope.filterModel = this.scope.model;
            this.services.state.go(this.scope.chart.DetailState.name, this.scope.chart.DetailState.parameters);
        }

        themeWatch: () => void;
        updateWatch: () => void;

        $onDestroy(): void {
            this.themeWatch();
            this.updateWatch();
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: IPercentageTotalChart,
            private readonly api: IApi,
            private readonly services: IServices) {

            this.themeWatch = rootScope.$on('theme-change', this.resetColors);
            this.updateWatch = rootScope.$on('update-charts', this.updateChart);

            scope.togglePercentage = this.togglePercentage;
            scope.viewDetails = this.viewDetails;

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