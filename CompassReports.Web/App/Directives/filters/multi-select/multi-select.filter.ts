module App.Directive.Filters {

    interface IMultiSelectFitlerScope {
        filter: Models.FilterModel<number>;
        report:  Models.BaseReport;
    }

    function multiSelectFilterDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                filter: '=',
                report: '='
            },
            templateUrl: `${settings.directiveBaseUri}/filters/multi-select/multi-select.view.html`
       }
    }

    angular
        .module('app.directives.filters.multi-select', [])
        .directive('multiSelectFilter', ['settings', multiSelectFilterDirective]);
}