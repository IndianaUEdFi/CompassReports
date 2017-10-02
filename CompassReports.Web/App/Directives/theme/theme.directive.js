var App;
(function (App) {
    var Directive;
    (function (Directive) {
        var Theme;
        (function (Theme) {
            var ThemeController = (function () {
                function ThemeController($rootScope, $mdSidenav, $mdTheming, $mdThemingProvider) {
                    var _this = this;
                    this.$rootScope = $rootScope;
                    this.$mdSidenav = $mdSidenav;
                    this.$mdTheming = $mdTheming;
                    this.$mdThemingProvider = $mdThemingProvider;
                    this.colors = [
                        { color: '#F44336', name: 'red' },
                        { color: '#E91E63', name: 'pink' },
                        { color: '#9C27B0', name: 'purple' },
                        { color: '#673AB7', name: 'deep-purple' },
                        { color: '#3F51B5', name: 'indigo' },
                        { color: '#2196F3', name: 'blue' },
                        { color: '#03A9F4', name: 'light-blue' },
                        { color: '#00BCD4', name: 'cyan' },
                        { color: '#009688', name: 'teal' },
                        { color: '#4CAF50', name: 'green' },
                        { color: '#8BC34A', name: 'light-green' },
                        { color: '#CDDC39', name: 'lime' },
                        { color: '#FFEB3B', name: 'yellow' },
                        { color: '#FFC107', name: 'amber' },
                        { color: '#FF9800', name: 'orange' },
                        { color: '#FF5722', name: 'deep-orange' },
                        { color: '#795548', name: 'brown' },
                        { color: '#9E9E9E', name: 'grey' },
                        { color: '#607D8B', name: 'blue-grey' }
                    ];
                    this.colorCount = 1;
                    this.setTheme = function (primaryColor, secondaryColor) {
                        _this.colorCount++;
                        _this.$mdThemingProvider
                            .theme('compass-reports-theme' + _this.colorCount)
                            .primaryPalette(primaryColor.name)
                            .accentPalette(secondaryColor.name)
                            .warnPalette('red');
                        _this.$rootScope.currentTheme = 'compass-reports-theme' + _this.colorCount;
                        _this.$rootScope.primaryColor = primaryColor;
                        _this.$rootScope.secondaryColor = secondaryColor;
                        _this.$rootScope.$emit('theme-change');
                        _this.$mdTheming.generateTheme('compass-reports-theme' + _this.colorCount);
                        _this.$mdThemingProvider.setDefaultTheme('compass-reports-theme' + _this.colorCount);
                    };
                    this.toggleThemes = function () { return _this.$mdSidenav('colornav').toggle(); };
                    var primaryColor = { color: '#003E69', name: 'dark-blue' };
                    var secondaryColor = { color: '#FDCD0F', name: 'dark-yellow' };
                    this.setTheme(primaryColor, secondaryColor);
                }
                return ThemeController;
            }());
            ThemeController.$inject = ['$rootScope', '$mdSidenav', '$mdTheming', 'themeProvider'];
            function themeDirective(settings) {
                return {
                    restrict: 'E',
                    templateUrl: settings.directiveBaseUri + "/theme/theme.view.html",
                    scope: {},
                    controller: ThemeController,
                    controllerAs: 'ctrl'
                };
            }
            angular
                .module('app.directives.theme', [])
                .directive('themeNav', ['settings', themeDirective]);
        })(Theme = Directive.Theme || (Directive.Theme = {}));
    })(Directive = App.Directive || (App.Directive = {}));
})(App || (App = {}));
//# sourceMappingURL=theme.directive.js.map