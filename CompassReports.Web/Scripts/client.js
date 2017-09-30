var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var App;
(function (App) {
    angular
        .module('app', [
        //Api
        'app.api',
        //Vendors
        'chart.js',
        'ngAnimate',
        'ngMaterial',
        'ngMaterialSidemenu',
        'ui.router',
        ////Settings
        'app.settings',
        ////Services
        'app.services',
        ////Directives
        'app.directives',
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
                .primaryPalette('dark-blue', {
                'hue-2': '100'
            })
                .accentPalette('dark-yellow', {
                'hue-1': '200',
                'hue-2': '300',
                'hue-3': '100'
            })
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
/// <reference path="../app.config.ts" />
var App;
(function (App) {
    var ApiService = (function () {
        function ApiService(enrollment, enrollmentFilters) {
            this.enrollment = enrollment;
            this.enrollmentFilters = enrollmentFilters;
        }
        return ApiService;
    }());
    ApiService.$inject = [
        'api.enrollment',
        'api.enrollment-filters'
    ];
    var ApiBase = (function () {
        function ApiBase(services, settings) {
            this.services = services;
            this.settings = settings;
            this.resourceUrl = '';
        }
        return ApiBase;
    }());
    ApiBase.$inject = ['services', 'settings'];
    App.ApiBase = ApiBase;
    var ApiBaseDefault = (function (_super) {
        __extends(ApiBaseDefault, _super);
        function ApiBaseDefault() {
            return _super !== null && _super.apply(this, arguments) || this;
        }
        ApiBaseDefault.prototype.getAll = function () { return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.get = function (id) { return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/" + id).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.post = function (model) { return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl, model).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.put = function (id, model) { return this.services.http.put(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/" + id, model).then(function (data) { return data.data; }); };
        ApiBaseDefault.prototype.delete = function (id) { return this.services.http.delete(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/" + id).then(function (data) { return data.data; }); };
        return ApiBaseDefault;
    }(ApiBase));
    App.ApiBaseDefault = ApiBaseDefault;
    angular
        .module("app.api", [
        'app.api.enrollment',
        'app.api.enrollment-filters'
    ])
        .service("api", ApiService);
})(App || (App = {}));
/// <reference path="api.module.ts" />
var App;
(function (App) {
    var Models;
    (function (Models) {
        var FilterModel = (function () {
            function FilterModel() {
            }
            return FilterModel;
        }());
        Models.FilterModel = FilterModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
(function (App) {
    var Api;
    (function (Api) {
        var EnrollmentFilters;
        (function (EnrollmentFilters) {
            var EnrollmentFiltersApi = (function (_super) {
                __extends(EnrollmentFiltersApi, _super);
                function EnrollmentFiltersApi() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    _this.resourceUrl = 'enrollment-filters';
                    return _this;
                }
                EnrollmentFiltersApi.prototype.getEnglishLanguageLearnerStatuses = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/english-learner-statuses").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getEthnicities = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/ethnicities").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getGrades = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/grades").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getLunchStatuses = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/lunch-statuses").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getSpecialEducationStatuses = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/special-education-statuses").then(function (data) { return data.data; });
                };
                EnrollmentFiltersApi.prototype.getSchoolYears = function () {
                    return this.services.http.get(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/school-years").then(function (data) { return data.data; });
                };
                return EnrollmentFiltersApi;
            }(App.ApiBase));
            angular
                .module("app.api.enrollment-filters", [])
                .service("api.enrollment-filters", EnrollmentFiltersApi);
        })(EnrollmentFilters = Api.EnrollmentFilters || (Api.EnrollmentFilters = {}));
    })(Api = App.Api || (App.Api = {}));
})(App || (App = {}));
/// <reference path="api.module.ts" />
var App;
(function (App) {
    var Models;
    (function (Models) {
        var EnrollmentChartModel = (function () {
            function EnrollmentChartModel() {
            }
            return EnrollmentChartModel;
        }());
        Models.EnrollmentChartModel = EnrollmentChartModel;
        var EnrollmentFilterModel = (function () {
            function EnrollmentFilterModel(schoolYear) {
                this.Grades = [];
                this.Ethnicities = [];
                this.SchoolYear = schoolYear;
            }
            return EnrollmentFilterModel;
        }());
        Models.EnrollmentFilterModel = EnrollmentFilterModel;
    })(Models = App.Models || (App.Models = {}));
})(App || (App = {}));
(function (App) {
    var Api;
    (function (Api) {
        var Enrollment;
        (function (Enrollment) {
            var EnrollmentApi = (function (_super) {
                __extends(EnrollmentApi, _super);
                function EnrollmentApi() {
                    var _this = _super !== null && _super.apply(this, arguments) || this;
                    _this.resourceUrl = 'enrollment';
                    return _this;
                }
                EnrollmentApi.prototype.byEnglishLanguageLearner = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-english-language-learner", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.byEthnicity = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-ethnicity", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.byGrade = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-grade", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.byLunchStatus = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-lunch-status", model).then(function (data) { return data.data; });
                };
                EnrollmentApi.prototype.bySpecialEducation = function (model) {
                    return this.services.http.post(this.settings.apiBaseUrl + "/" + this.resourceUrl + "/by-special-education", model).then(function (data) { return data.data; });
                };
                return EnrollmentApi;
            }(App.ApiBase));
            angular
                .module("app.api.enrollment", [])
                .service("api.enrollment", EnrollmentApi);
        })(Enrollment = Api.Enrollment || (Api.Enrollment = {}));
    })(Api = App.Api || (App.Api = {}));
})(App || (App = {}));
angular
    .module('app.directives', [
    'app.directives.truncate-tooltip'
]);
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
var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var PieChartModel = (function () {
            function PieChartModel(chartCall) {
                this.ChartCall = chartCall;
            }
            return PieChartModel;
        }());
        Reports.PieChartModel = PieChartModel;
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
                function EnrollmentController($scope, api, services, $mdSidenav, englishLanguageLearnerStatuses, ethnicities, grades, lunchStatuses, specialEducationStatuses, schoolYears) {
                    var _this = this;
                    this.$scope = $scope;
                    this.api = api;
                    this.services = services;
                    this.$mdSidenav = $mdSidenav;
                    this.englishLanguageLearnerStatuses = englishLanguageLearnerStatuses;
                    this.ethnicities = ethnicities;
                    this.grades = grades;
                    this.lunchStatuses = lunchStatuses;
                    this.specialEducationStatuses = specialEducationStatuses;
                    this.schoolYears = schoolYears;
                    this.displaySchoolYears = {};
                    this.charts = [
                        new Reports.PieChartModel('byGrade'),
                        new Reports.PieChartModel('byEthnicity'),
                        new Reports.PieChartModel('byLunchStatus'),
                        new Reports.PieChartModel('bySpecialEducation'),
                        new Reports.PieChartModel('byEnglishLanguageLearner')
                    ];
                    this.filters = new App.Models.EnrollmentFilterModel(this.schoolYears[0].Value);
                    this.toggleFilters = function () { return _this.$mdSidenav('filternav').toggle(); };
                    this.reset = function () {
                        _this.filters = new App.Models.EnrollmentFilterModel(_this.schoolYears[0].Value);
                    };
                    this.apply = function () {
                        angular.forEach(_this.charts, function (chart) {
                            return _this.api.enrollment[chart.ChartCall](_this.filters)
                                .then(function (result) {
                                //Sets the current card state to default on the first call
                                if (!chart.Chart) {
                                    chart.ShowChart = result.ShowChart;
                                    chart.Chart = result;
                                    chart.Options = {
                                        responsive: true,
                                        legend: { display: true, position: 'left' }
                                    };
                                }
                                else {
                                    chart.Options.animation = { duration: 1000 },
                                        chart.Chart.Labels = result.Labels;
                                    chart.Chart.Data = result.Data;
                                }
                                chart.Colors = _this.services.colorGradient.getColors(result.Data.length);
                                // Workout around redrawing causes messup animation
                                _this.services.timeout(function () { return chart.Options.animation = false; }, 1500);
                            });
                        });
                    };
                    angular.forEach(schoolYears, function (year) {
                        _this.displaySchoolYears[year.Value] = year.Display;
                    });
                    this.apply();
                    $scope.$on('chart-update', function (evt, chart) {
                        console.log(chart);
                    });
                }
                return EnrollmentController;
            }());
            EnrollmentController.$inject = ['$scope', 'api', 'services', '$mdSidenav', 'englishLanguageLearnerStatuses', 'ethnicities',
                'grades', 'lunchStatuses', 'specialEducationStatuses', 'schoolYears'];
            var EnrollmentConfig = (function () {
                function EnrollmentConfig($stateProvider, settings) {
                    $stateProvider.state('app.reports.enrollment', {
                        url: '/enrollment',
                        views: {
                            'report@app.reports': {
                                templateUrl: settings.moduleBaseUri + "/reports/enrollment/enrollment.view.html",
                                controller: EnrollmentController,
                                controllerAs: 'ctrl',
                                resolve: {
                                    englishLanguageLearnerStatuses: ['api', function (api) {
                                            return api.enrollmentFilters.getEnglishLanguageLearnerStatuses();
                                        }],
                                    ethnicities: ['api', function (api) {
                                            return api.enrollmentFilters.getEthnicities();
                                        }],
                                    grades: ['api', function (api) {
                                            return api.enrollmentFilters.getGrades();
                                        }],
                                    lunchStatuses: ['api', function (api) {
                                            return api.enrollmentFilters.getLunchStatuses();
                                        }],
                                    specialEducationStatuses: ['api', function (api) {
                                            return api.enrollmentFilters.getSpecialEducationStatuses();
                                        }],
                                    schoolYears: ['api', function (api) {
                                            return api.enrollmentFilters.getSchoolYears();
                                        }]
                                }
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
    var Services;
    (function (Services) {
        var ColorGradient = (function () {
            function ColorGradient() {
            }
            ColorGradient.prototype.getColors = function (colorCount) {
                var color1Hash = '#003E69';
                var color2Hash = '#FDCD0F';
                var color1Hex = color1Hash.split('#')[1];
                var color2Hex = color2Hash.split('#')[1];
                var color1Red = parseInt(color1Hex.substr(0, 2), 16);
                var color1Green = parseInt(color1Hex.substr(2, 2), 16);
                var color1Blue = parseInt(color1Hex.substr(4, 2), 16);
                var color2Red = parseInt(color2Hex.substr(0, 2), 16);
                var color2Green = parseInt(color2Hex.substr(2, 2), 16);
                var color2Blue = parseInt(color2Hex.substr(4, 2), 16);
                var redDiff = (color1Red > color2Red) ? color1Red - color2Red : color2Red - color1Red;
                var greenDiff = (color1Green > color2Green) ? color1Green - color2Green : color2Green - color1Green;
                var blueDiff = (color1Blue > color2Blue) ? color1Blue - color2Blue : color2Blue - color1Blue;
                var redAvg = redDiff / (colorCount - 1);
                var greenAvg = greenDiff / (colorCount - 1);
                var blueAvg = blueDiff / (colorCount - 1);
                var colors = [color1Hash];
                var currentRed = color1Red;
                var currentGreen = color1Green;
                var currentBlue = color1Blue;
                for (var i = 0; i < (colorCount - 1); i++) {
                    if (color1Red > color2Red)
                        currentRed -= redAvg;
                    else
                        currentRed += redAvg;
                    if (color1Green > color2Green)
                        currentGreen -= greenAvg;
                    else
                        currentGreen += greenAvg;
                    if (color1Blue > color2Blue)
                        currentBlue -= blueAvg;
                    else
                        currentBlue += blueAvg;
                    var color = '#' +
                        ((currentRed < 16) ? '0' : '') + Math.round(currentRed).toString(16) +
                        ((currentGreen < 16) ? '0' : '') + Math.round(currentGreen).toString(16) +
                        ((currentBlue < 16) ? '0' : '') + Math.round(currentBlue).toString(16);
                    colors.push(color);
                }
                return colors;
            };
            return ColorGradient;
        }());
        angular
            .module('app.services.color-gradient', [])
            .service('app.services.color-gradient', ColorGradient);
    })(Services = App.Services || (App.Services = {}));
})(App || (App = {}));
var App;
(function (App) {
    var Services = (function () {
        function Services(colorGradient, compile, document, filter, http, q, state, timeout, template, window) {
            this.colorGradient = colorGradient;
            this.compile = compile;
            this.document = document;
            this.filter = filter;
            this.http = http;
            this.q = q;
            this.state = state;
            this.timeout = timeout;
            this.template = template;
            this.window = window;
        }
        return Services;
    }());
    Services.$inject = [
        'app.services.color-gradient',
        '$compile',
        '$document',
        '$filter',
        '$http',
        '$q',
        '$state',
        '$timeout',
        '$templateRequest',
        '$window'
    ];
    angular
        .module("app.services", ['app.services.color-gradient'])
        .service("services", Services);
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