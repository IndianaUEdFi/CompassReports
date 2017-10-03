module App.Directive.Charts {

    interface IPieChartScope {
        chart: Models.PieChartModel<number>
    }

    class PieChartController {
        static $inject = ['$rootScope', '$scope', 'services'];

        resetColors = () => {
            if (this.scope.chart.Data && this.scope.chart.Data.length)
                this.scope.chart.Colors = this.services.colorGradient.getColors(this.scope.chart.Data.length);
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: IPieChartScope,
            private readonly services: IServices) {

            rootScope.$on('theme-change', this.resetColors);
        }
    }

    function pieChartDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: { chart: '=' },
            templateUrl: `${settings.directiveBaseUri}/charts/pie-chart/pie-chart.view.html`,
            controller: PieChartController
        }
    }

    angular
        .module('app.directives.charts.pie', [])
        .directive('pieChart', ['settings', pieChartDirective]);
}