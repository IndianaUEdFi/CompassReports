module App.Directive.Filters {

    interface ISelectFilterScope {
        filter: Models.FilterModel<number>;
        model:  any;
    }

    function selectFilterDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                filter: '=',
                model: '='
            },
            templateUrl: `${settings.directiveBaseUri}/filters/select/select.view.html`
       }
    }

    angular
        .module('app.directives.filters.select', [])
        .directive('selectFilter', ['settings', selectFilterDirective]);
}