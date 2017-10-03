module App.Directive.Charts {

    interface IBarChartScope {
        chart: Models.BarChartModel<number>
    }

    class BarChartController {
        static $inject = ['$rootScope', '$scope', 'services'];

        resetColors = () => {
            if(this.scope.chart.Data && this.scope.chart.Data.length)
                this.scope.chart.Colors = this.services.colorGradient.getColors(this.scope.chart.Data.length);
        }

        constructor(private readonly rootScope: IAppRootScope,
            private readonly scope: IBarChartScope,
            private readonly services: IServices) {

            rootScope.$on('theme-change', this.resetColors);
        }
    }

    function barChartDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: { chart: '=' },
            templateUrl: `${settings.directiveBaseUri}/charts/bar-chart/bar-chart.view.html`,
            controller: BarChartController
        }
    }

    angular
        .module('app.directives.charts.bar', [])
        .directive('barChart', ['settings', barChartDirective]);
}