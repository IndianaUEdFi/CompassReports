module App.Directive.TruncateTooltip {

    interface ITruncateTooltipScope extends angular.IScope {
        showTooltip: boolean;
        value: string;
    }

    function truncateTooltipDirective($timeout: ng.ITimeoutService) {
        return {
            restrict: 'A',
            tempate: '<md-tooltip class="overflow-tooltip">' +
                         '<div style="max-width: 300px; line-height: 18px">{{value}}</div>' +
                     '</md-tooltip>',
            scope: {},
            link: (scope: ITruncateTooltipScope, element: JQuery) => {

                element.bind('mouseover',
                    () => {
                        var el = element[0];
                        console.log('moused over');
                        console.log(element.text());
                        if (el.offsetWidth < el.scrollWidth) {
                            console.log('showing tooltip');
                            scope.showTooltip = true;
                            scope.value = element.text();
                        }
                        else scope.showTooltip = false;
                    });
            }
        }
    }

    angular
        .module('app.directives.truncate-tooltip', [])
        .directive('truncateTooltip', ['$timeout', truncateTooltipDirective]);
}