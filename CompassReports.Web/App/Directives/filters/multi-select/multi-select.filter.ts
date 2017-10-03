module App.Directive.Filters {

    interface IMultiSelectFitlerScope {
        filter: Models.FilterModel<number>;
        model:  any;
    }

    function multiSelectFilterDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                filter: '=',
                model: '='
            },
            templateUrl: `${settings.directiveBaseUri}/filters/multi-select/multi-select.view.html`
       }
    }

    angular
        .module('app.directives.filters.multi-select', [])
        .directive('multiSelectFilter', ['settings', multiSelectFilterDirective]);
}