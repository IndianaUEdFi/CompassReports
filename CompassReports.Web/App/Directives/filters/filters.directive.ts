module App.Directive.Filters {

    interface IFilterScope extends angular.IScope {
        filter: Models.FilterModel<number>;
        model: any;
    }

    function filterDirective(services: IServices) {
        return {
            restrict: 'E',
            scope: {
                filter: '=',
                report: '=',
            },
            link: (scope: IFilterScope, element: JQuery) => {
                var type = scope.filter.Multiple ? 'multi-select': 'select';
                var template = `<${type}-filter filter="filter" report="report"></${type}-filter`;
                var templateElement = angular.element(template);
                element.append(templateElement);
                services.compile(templateElement)(scope);
            }
        }
    }

    angular
        .module('app.directives.filters', ['app.directives.filters.multi-select', 'app.directives.filters.select'])
        .directive('filter', ['services', filterDirective]);
}