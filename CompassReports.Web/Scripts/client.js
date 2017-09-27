var App;
(function (App) {
    angular
        .module('app', [
        'ngAnimate',
        'ngMaterial',
        'ngMaterialSidemenu',
        'ui.router',
        'app.settings',
        'app.reports'
    ]);
})(App || (App = {}));
var App;
(function (App) {
    var AppConfig = (function () {
        function AppConfig($locationProvider, $stateProvider, $urlRouterProvider, $mdThemingProvider) {
            $locationProvider.hashPrefix('');
            $mdThemingProvider.definePalette('dark-blue', {
                '50': '#E0E8ED',
                '100': '#B3C5D2',
                '200': '#809FB4',
                '300': '#4D7896',
                '400': '#265B80',
                '500': '#003E69',
                '600': '#003861',
                '700': '#003056',
                '800': '#00284C',
                '900': '#001B3B',
                'A100': '#71A3FF',
                'A200': '#3E81FF',
                'A400': '#0B60FF',
                'A700': '#0054F1',
                'contrastDefaultColor': 'light',
                'contrastDarkColors': '50 100 200 A100',
                'contrastStrongLightColors': '300 400'
            });
            $mdThemingProvider.definePalette('dark-yellow', {
                '50': '#FFF9E2',
                '100': '#FEF0B7',
                '200': '#FEE687',
                '300': '#FEDC57',
                '400': '#FDD533',
                '500': '#FDCD0F',
                '600': '#FDC80D',
                '700': '#FCC10B',
                '800': '#FCBA08',
                '900': '#FCAE04',
                'A100': '#FEDC57',
                'A200': '#FDCD0F',
                'A400': '#FCC10B',
                'A700': '#FCAE04',
                'contrastDefaultColor': 'dark',
            });
            $mdThemingProvider.theme('compass-reports-theme')
                .primaryPalette('dark-blue')
                .accentPalette('dark-yellow')
                .warnPalette('red');
            $urlRouterProvider.otherwise('/home');
            $stateProvider
                .state('app', {
                abstract: true
            });
        }
        return AppConfig;
    }());
    AppConfig.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', '$mdThemingProvider'];
    angular
        .module('app')
        .config(AppConfig);
})(App || (App = {}));
var App;
(function (App) {
    var AppController = (function () {
        function AppController() {
        }
        return AppController;
    }());
    AppController.$inject = [];
    angular
        .module('app')
        .controller('app', AppController);
})(App || (App = {}));
var App;
(function (App) {
    var AppRun = (function () {
        function AppRun($rootScope) {
            var contentLoadedEvent = $rootScope.$on('$viewContentLoaded', function (event, view) {
                if (event.targetScope && event.targetScope.ctrl)
                    console.log("Loaded controller " + event.targetScope.ctrl.constructor.name + " for view " + view);
            });
            var stateAuthorizeStartEvent = $rootScope.$on('$stateChangeStart', function (event, toState, toStateParams) {
                console.log('changing state');
                console.log(toState);
            });
            $rootScope.$on('$destroy', function () {
                contentLoadedEvent();
                stateAuthorizeStartEvent();
            });
            $rootScope.title = "Compass Reports";
        }
        return AppRun;
    }());
    AppRun.$inject = ['$rootScope'];
    angular
        .module('app')
        .run(AppRun);
})(App || (App = {}));
var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var Home;
        (function (Home) {
            var HomeController = (function () {
                function HomeController() {
                    console.log('home app');
                }
                return HomeController;
            }());
            HomeController.$inject = [];
            var HomeConfig = (function () {
                function HomeConfig($stateProvider, settings) {
                    $stateProvider.state('app.reports.home', {
                        url: '/home',
                        views: {
                            'report@app.reports': {
                                templateUrl: settings.moduleBaseUri + "/reports/home/home.view.html",
                                controller: HomeController,
                                controllerAs: 'ctrl'
                            }
                        }
                    });
                }
                return HomeConfig;
            }());
            HomeConfig.$inject = ['$stateProvider', 'settings'];
            angular
                .module('app.reports.home', [])
                .config(HomeConfig);
        })(Home = Reports.Home || (Reports.Home = {}));
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var ReportsLayoutController = (function () {
            function ReportsLayoutController() {
                var _this = this;
                this.isOpen = false;
                this.menuId = 0;
                this.toggleExpanded = function (selectedMenuId) {
                    if (!_this.isOpen) {
                        _this.isOpen = true;
                        _this.menuId = selectedMenuId;
                    }
                    else {
                        if (_this.menuId === selectedMenuId) {
                            _this.isOpen = false;
                            _this.menuId = 0;
                        }
                        else {
                            _this.menuId = selectedMenuId;
                        }
                    }
                };
                this.toggleSidenav = function () {
                    _this.isOpen = !_this.isOpen;
                    if (_this.menuId === 0)
                        _this.menuId = 1;
                };
                this.isSelected = function (value) {
                    return _this.menuId === value;
                };
            }
            return ReportsLayoutController;
        }());
        ReportsLayoutController.$inject = [];
        var ReportsLayoutConfig = (function () {
            function ReportsLayoutConfig($stateProvider, settings) {
                $stateProvider
                    .state('app.reports', {
                    abstract: true,
                    views: {
                        'content@': {
                            templateUrl: settings.moduleBaseUri + "/reports/reports-layout.view.html",
                            controller: ReportsLayoutController,
                            controllerAs: 'ctrl'
                        }
                    }
                });
            }
            return ReportsLayoutConfig;
        }());
        ReportsLayoutConfig.$inject = ['$stateProvider', 'settings'];
        angular
            .module('app.reports', ['app.reports.home'])
            .config(ReportsLayoutConfig);
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
if (!(String.prototype).startsWith) {
    (String.prototype).startsWith = function (searchString, position) {
        position = position || 0;
        return this.indexOf(searchString, position) === position;
    };
}
if (!(String.prototype).includes) {
    (String.prototype).includes = function (search, start) {
        if (typeof start !== 'number') {
            start = 0;
        }
        if (start + search.length > this.length) {
            return false;
        }
        else {
            return this.indexOf(search, start) !== -1;
        }
    };
}
var App;
(function (App) {
    var SystemSettings = (function () {
        function SystemSettings() {
            this.apiBaseUrl = 'api';
            this.directiveBaseUri = 'app/directives';
            this.moduleBaseUri = 'app/modules';
            this.componentBaseUri = 'app/components';
        }
        return SystemSettings;
    }());
    angular
        .module('app.settings', [])
        .constant('settings', new SystemSettings());
})(App || (App = {}));
//# sourceMappingURL=client.js.map