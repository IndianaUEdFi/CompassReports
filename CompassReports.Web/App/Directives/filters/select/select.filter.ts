module App.Directive.Filters {

    interface ISelectFilterScope {
        filter: Models.FilterModel<number>;
        report: Models.BaseReport;
        onChange: () => void;
    }

    class SelectFilterController {
        static $inject = ['$scope', 'api'];

        constructor(private readonly $scope: ISelectFilterScope,
            private readonly api: IApi) {

            $scope.onChange = () => {
                if (this.$scope.filter.OnChange != null) {
                    this.$scope.filter.OnChange(this.$scope.report.model, this.$scope.report.filters, this.api);
                }
            }
        }
    }

    function selectFilterDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            scope: {
                filter: '=',
                report: '='
            },
            templateUrl: `${settings.directiveBaseUri}/filters/select/select.view.html`,
            controller: SelectFilterController
        }
    }

    angular
        .module('app.directives.filters.select', [])
        .directive('selectFilter', ['settings', selectFilterDirective]);
}