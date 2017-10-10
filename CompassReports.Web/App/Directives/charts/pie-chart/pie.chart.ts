module App.Directive.Charts {

    interface IPieChartScope extends ng.IScope {
        chart: Models.PieChartModel,
        model: Models.IReportFilterModel;
        dataSetOverride: any;
        togglePercentage: () => void;
        viewDetails: () => void;
    }

    class PieChartController implements ng.IController {
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
            private readonly scope: IPieChartScope,
            private readonly api: IApi,
            private readonly services: IServices) {

            this.themeWatch = rootScope.$on('theme-change', this.resetColors);
            this.updateWatch = rootScope.$on('update-charts', this.updateChart);

            this.scope.togglePercentage = this.togglePercentage;
            this.scope.viewDetails = this.viewDetails;

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