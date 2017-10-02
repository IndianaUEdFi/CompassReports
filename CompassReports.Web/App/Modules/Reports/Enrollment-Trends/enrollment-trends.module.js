var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var EnrollmentTrends;
        (function (EnrollmentTrends) {
            var EnrollmentTrendsController = (function () {
                function EnrollmentTrendsController($rootScope, api, services, $mdSidenav, englishLanguageLearnerStatuses, ethnicities, grades, lunchStatuses, specialEducationStatuses, schoolYears) {
                    var _this = this;
                    this.$rootScope = $rootScope;
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
                        new Reports.BarChartModel('byGrade'),
                        new Reports.BarChartModel('byEthnicity'),
                        new Reports.BarChartModel('byLunchStatus'),
                        new Reports.BarChartModel('bySpecialEducation'),
                        new Reports.BarChartModel('byEnglishLanguageLearner')
                    ];
                    this.filters = new App.Models.EnrollmentTrendsFilterModel();
                    this.toggleFilters = function () { return _this.$mdSidenav('filternav').toggle(); };
                    this.reset = function () {
                        _this.filters = new App.Models.EnrollmentTrendsFilterModel();
                    };
                    this.apply = function () {
                        angular.forEach(_this.charts, function (chart) {
                            return _this.api.enrollmentTrends[chart.ChartCall](_this.filters)
                                .then(function (result) {
                                //Sets the current card state to default on the first call
                                if (!chart.Chart) {
                                    chart.ShowChart = result.ShowChart;
                                    chart.Chart = result;
                                    chart.Options = {
                                        responsive: true,
                                        legend: { display: true, position: 'left' },
                                        scales: {
                                            yAxes: [{
                                                    ticks: {
                                                        beginAtZero: true
                                                    }
                                                }]
                                        }
                                    };
                                }
                                else {
                                    chart.Options.animation = { duration: 1000 },
                                        chart.Chart.Labels = result.Labels;
                                    chart.Chart.Data = result.Data;
                                    chart.Chart.Headers = result.Headers;
                                    chart.Chart.Series = result.Series;
                                }
                                chart.Colors = _this.services.colorGradient.getColors(result.Data.length);
                                // Workout around redrawing causes messup animation
                                _this.services.timeout(function () { return chart.Options.animation = false; }, 1500);
                            });
                        });
                    };
                    this.services.timeout(function () {
                        $rootScope.$on('theme-change', function () {
                            _this.apply();
                        });
                    }, 1000);
                    angular.forEach(schoolYears, function (year) {
                        _this.displaySchoolYears[year.Value] = year.Display;
                    });
                    this.apply();
                }
                return EnrollmentTrendsController;
            }());
            EnrollmentTrendsController.$inject = ['$rootScope', 'api', 'services', '$mdSidenav', 'englishLanguageLearnerStatuses', 'ethnicities',
                'grades', 'lunchStatuses', 'specialEducationStatuses', 'schoolYears'];
            var EnrollmentTrendsConfig = (function () {
                function EnrollmentTrendsConfig($stateProvider, settings) {
                    $stateProvider.state('app.reports.enrollment-trends', {
                        url: '/enrollment-trends',
                        views: {
                            'report@app.reports': {
                                templateUrl: settings.moduleBaseUri + "/reports/enrollment-trends/enrollment-trends.view.html",
                                controller: EnrollmentTrendsController,
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
                return EnrollmentTrendsConfig;
            }());
            EnrollmentTrendsConfig.$inject = ['$stateProvider', 'settings'];
            angular
                .module('app.reports.enrollment-trends', [])
                .config(EnrollmentTrendsConfig);
        })(EnrollmentTrends = Reports.EnrollmentTrends || (Reports.EnrollmentTrends = {}));
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
//# sourceMappingURL=enrollment-trends.module.js.map