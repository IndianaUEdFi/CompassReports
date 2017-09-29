var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var Enrollment;
        (function (Enrollment) {
            var EnrollmentController = (function () {
                function EnrollmentController(api, services, $mdSidenav, englishLanguageLearnerStatuses, ethnicities, grades, lunchStatuses, specialEducationStatuses, schoolYears) {
                    var _this = this;
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
                            _this.api.enrollment[chart.ChartCall](_this.filters).then(function (result) {
                                chart.Chart = result;
                                chart.Colors = _this.services.colorGradient.getColors(result.Data.length);
                                chart.Options = { legend: { display: true, position: 'left' } };
                            });
                        });
                    };
                    angular.forEach(schoolYears, function (year) {
                        _this.displaySchoolYears[year.Value] = year.Display;
                    });
                    this.apply();
                }
                return EnrollmentController;
            }());
            EnrollmentController.$inject = ['api', 'services', '$mdSidenav', 'englishLanguageLearnerStatuses', 'ethnicities',
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
//# sourceMappingURL=enrollment.module.js.map