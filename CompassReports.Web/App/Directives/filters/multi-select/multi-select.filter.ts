module App.Directive.Filters {

    interface IMultiSelectFitlerScope {
        filter: Models.FilterModel<number>;
        report: Models.BaseReport;
        onChange: () => void;

    }

    class MultiSelectFilterController {
        static $inject = ['$scope', 'api'];

        constructor(private readonly $scope: IMultiSelectFitlerScope,
            private readonly api: IApi) {

            $scope.onChange = () => {
                if (this.$scope.filter.OnChange != null) {
                    this.$scope.filter.OnChange(this.$scope.report.model, this.$scope.report.filters, this.api);
                }
            }
        }
    }

    function multiSelectFilterDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                filter: '=',
                report: '='
            },
            templateUrl: `${settings.directiveBaseUri}/filters/multi-select/multi-select.view.html`,
            controller: MultiSelectFilterController
       }
    }

    angular
        .module('app.directives.filters.multi-select', [])
        .directive('multiSelectFilter', ['settings', multiSelectFilterDirective]);
}