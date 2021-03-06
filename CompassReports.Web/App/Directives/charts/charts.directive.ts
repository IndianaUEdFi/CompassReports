﻿module App.Directive.Charts {

    interface IChartScope extends angular.IScope {
        chart: Models.ChartModel;
    }

    function chartDirective(services: IServices) {
        return {
            restrict: 'E',
            scope: {
                chart: '=',
                model: '='
            },
            link: (scope: IChartScope, element: JQuery) => {
                var template = `<${scope.chart.Type}-chart chart="chart" model="model"></${scope.chart.Type}-chart`;
                var templateElement = angular.element(template);
                element.append(templateElement);
                services.compile(templateElement)(scope);
            }
        }
    }

    angular
        .module('app.directives.charts', [
            'app.directives.charts.bar',
            'app.directives.charts.line',
            'app.directives.charts.percentage-total',
            'app.directives.charts.pie'])
        .directive('chart', ['services', chartDirective]);
}