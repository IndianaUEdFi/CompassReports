var App;
(function (App) {
    var Directive;
    (function (Directive) {
        var TruncateTooltip;
        (function (TruncateTooltip) {
            function truncateTooltipDirective($timeout) {
                return {
                    restrict: 'A',
                    tempate: '<md-tooltip class="overflow-tooltip">' +
                        '<div style="max-width: 300px; line-height: 18px">{{value}}</div>' +
                        '</md-tooltip>',
                    scope: {},
                    link: function (scope, element) {
                        element.bind('mouseover', function () {
                            var el = element[0];
                            console.log('moused over');
                            console.log(element.text());
                            if (el.offsetWidth < el.scrollWidth) {
                                console.log('showing tooltip');
                                scope.showTooltip = true;
                                scope.value = element.text();
                            }
                            else
                                scope.showTooltip = false;
                        });
                    }
                };
            }
            angular
                .module('app.directives.truncate-tooltip', [])
                .directive('truncateTooltip', ['$timeout', truncateTooltipDirective]);
        })(TruncateTooltip = Directive.TruncateTooltip || (Directive.TruncateTooltip = {}));
    })(Directive = App.Directive || (App.Directive = {}));
})(App || (App = {}));
//# sourceMappingURL=truncate-tooltip.directive.js.map