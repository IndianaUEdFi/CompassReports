module App.Reports.Enrollment {

    class EnrollmentController {
        static $inject = ['$scope', 'api', 'services', '$mdSidenav', 'englishLanguageLearnerStatuses', 'ethnicities',
            'grades', 'lunchStatuses', 'specialEducationStatuses', 'schoolYears'];

        displaySchoolYears: any = {};
        charts = [
            new PieChartModel<number>('byGrade'),
            new PieChartModel<number>('byEthnicity'),
            new PieChartModel<number>('byLunchStatus'),
            new PieChartModel<number>('bySpecialEducation'),
            new PieChartModel<number>('byEnglishLanguageLearner')
        ];

        filters = new Models.EnrollmentFilterModel(this.schoolYears[0].Value);

        toggleFilters = () => this.$mdSidenav('filternav').toggle();

        reset = () => {
            this.filters = new Models.EnrollmentFilterModel(this.schoolYears[0].Value);
        }

        apply = () => {

            angular.forEach(this.charts, chart => {

                return this.api.enrollment[chart.ChartCall](this.filters)
                    .then((result: Models.EnrollmentChartModel<number>) => {
                        //Sets the current card state to default on the first call
                        if (!chart.Chart) {
                            chart.ShowChart = result.ShowChart;
                            chart.Chart = result;
                            chart.Options = {
                                responsive: true,
                                legend: { display: true, position: 'left' }
                            };
                        } else {
                            chart.Chart.Labels = result.Labels;
                            chart.Chart.Data = result.Data;
                        }

                        chart.Colors = this.services.colorGradient.getColors(result.Data.length);
                    });
            });

        }

        constructor(
            public $scope,
            private readonly api: IApi,
            private readonly services: IServices,
            private readonly $mdSidenav: ng.material.ISidenavService,
            public englishLanguageLearnerStatuses: string[],
            public ethnicities: string[],
            public grades: string[],
            public lunchStatuses: string[],
            public specialEducationStatuses: string[],
            public schoolYears: Models.FilterModel<number>[]
        ) {

            angular.forEach(schoolYears, year => {
                this.displaySchoolYears[year.Value] = year.Display;
            });

            this.apply();

            $scope.$on('chart-update', (evt, chart) => {
                console.log(chart);
            });
        }
    }

    class EnrollmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.enrollment', {
                url: '/enrollment',
                views: {
                    'report@app.reports': {
                        templateUrl: `${settings.moduleBaseUri}/reports/enrollment/enrollment.view.html`,
                        controller: EnrollmentController,
                        controllerAs: 'ctrl',
                        resolve: {
                            englishLanguageLearnerStatuses: ['api', (api: IApi) => {
                                return api.enrollmentFilters.getEnglishLanguageLearnerStatuses();
                            }],
                            ethnicities: ['api', (api: IApi) => {
                                return api.enrollmentFilters.getEthnicities();
                            }],
                            grades: ['api', (api: IApi) => {
                                return api.enrollmentFilters.getGrades();
                            }],
                            lunchStatuses: ['api', (api: IApi) => {
                                return api.enrollmentFilters.getLunchStatuses();
                            }],
                            specialEducationStatuses: ['api', (api: IApi) => {
                                return api.enrollmentFilters.getSpecialEducationStatuses();
                            }],
                            schoolYears: ['api', (api: IApi) => {
                                return api.enrollmentFilters.getSchoolYears();
                            }]
                        }
                    }
                }
            });
        }
    }

    angular
        .module('app.reports.enrollment', [])
        .config(EnrollmentConfig);
}