var App;
(function (App) {
    angular
        .module('app', [
        //Api
        //'app.api',
        //Vendors
        'chart.js',
        'ngAnimate',
        'ngMaterial',
        'ngMaterialSidemenu',
        'ui.router',
        ////Settings
        'app.settings',
        ////Services
        //'app.services',
        ////Directives
        //'app.directives',
        ////Modules
        'app.reports'
    ]);
})(App || (App = {}));
/// <reference path="app.module.ts"/>
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
            $urlRouterProvider.otherwise('/enrollment');
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
/// <reference path="app.module.ts"/>
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
/// <reference path="app.module.ts"/>
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
            // Cleanup
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
// Internet Explorer does not support startsWith or includes
// Typescript allows extension of interfaces through merging all with the same name together
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
            .module('app.reports', [
            'app.reports.enrollment',
            'app.reports.home'
        ])
            .config(ReportsLayoutConfig);
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var Enrollment;
        (function (Enrollment) {
            var EnrollmentController = (function () {
                function EnrollmentController($mdSidenav) {
                    var _this = this;
                    this.$mdSidenav = $mdSidenav;
                    this.charts = [
                        {
                            title: 'Grade',
                            headers: ['', 'Grade', 'Count'],
                            labels: ['Pre-Kindergarden', 'Kindergarten', 'Grade 1', 'Grade 2', 'Grade 3', 'Grade 4', 'Grade 5', 'Grade 6', 'Grade 7', 'Grade 8', 'Grade 9', 'Grade 10', 'Grade 11', 'Grade 12', 'Grade 12+/Adult'],
                            data: [23097, 83263, 83750, 84596, 89159, 84813, 85383, 84284, 85791, 83981, 97023, 88652, 85913, 82367, 1308],
                            colors: [
                                '#003E69', '#124862', '#24525C',
                                '#365C55', '#48664F', '#5A7148',
                                '#6C7B42', '#7E853C', '#908F35',
                                '#A2992F', '#B4A428', '#C6AE22',
                                '#D8B81B', '#EAC215', '#FDCD0F'
                            ],
                            options: { legend: { display: true, position: 'left' } }
                        },
                        {
                            title: 'Ethnicity',
                            headers: ['', 'Ethnicity', 'Count'],
                            labels: ['American Indian', 'Asian', 'Black', 'Hispanic', 'Multiracial', 'Navite Hawaiian or Other Pacific Islander', 'White'],
                            data: [2301, 26090, 137338, 130842, 54109, 817, 781883],
                            options: { legend: { display: true, position: 'left' } },
                            colors: ['#003E69', '#2A555A', '#546D4B', '#7E853C', '#A89D2D', '#D2B51E', '#FDCD0F']
                        },
                        {
                            title: 'Free/Reduced Price Meals',
                            headers: ['', 'Meal Type', 'Count'],
                            labels: ['Free meals', 'Reduced price meals', 'Paid meals'],
                            data: [432677, 85564, 615139],
                            options: { legend: { display: true, position: 'left' } },
                            colors: ['#003E69', '#7E853C', '#FDCD0F']
                        },
                        {
                            title: 'Spcial Education',
                            headers: ['', 'Education', 'Count'],
                            labels: ['Special Education', 'General Education'],
                            data: [164706, 968674],
                            options: { legend: { display: true, position: 'left' } },
                            colors: ['#003E69', '#FDCD0F']
                        },
                        {
                            title: 'English Language Learners',
                            headers: ['', 'Language Learner', 'Count'],
                            labels: ['English Language Learner', 'Non-English Language Learner'],
                            data: [1082703, 50677],
                            options: { legend: { display: true, position: 'left' } },
                            colors: ['#003E69', '#FDCD0F']
                        }
                    ];
                    this.toggleFilters = function () {
                        _this.$mdSidenav('filternav').toggle();
                    };
                    console.log('home app');
                }
                return EnrollmentController;
            }());
            EnrollmentController.$inject = ['$mdSidenav'];
            var EnrollmentConfig = (function () {
                function EnrollmentConfig($stateProvider, settings) {
                    $stateProvider.state('app.reports.enrollment', {
                        url: '/enrollment',
                        views: {
                            'report@app.reports': {
                                templateUrl: settings.moduleBaseUri + "/reports/enrollment/enrollment.view.html",
                                controller: EnrollmentController,
                                controllerAs: 'ctrl'
                            }
                        }
                    });
                }
                return EnrollmentConfig;
            }());
            EnrollmentConfig.$inject = ['$stateProvider', 'settings'];
            angular
                .module('app.reports.enrollment', [])
                .config(EnrollmentConfig);
        })(Enrollment = Reports.Enrollment || (Reports.Enrollment = {}));
    })(Reports = App.Reports || (App.Reports = {}));
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