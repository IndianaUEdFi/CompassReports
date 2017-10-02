module App.Reports.EnrollmentTrends {

    class EnrollmentTrendsController {
        static $inject = ['$rootScope', 'api', 'services', '$mdSidenav', 'englishLanguageLearnerStatuses', 'ethnicities',
            'grades', 'lunchStatuses', 'specialEducationStatuses', 'schoolYears'];

        displaySchoolYears: any = {};
        charts = [
            new BarChartModel<number>('byGrade'),
            new BarChartModel<number>('byEthnicity'),
            new BarChartModel<number>('byLunchStatus'),
            new BarChartModel<number>('bySpecialEducation'),
            new BarChartModel<number>('byEnglishLanguageLearner')
        ];

        filters = new Models.EnrollmentTrendsFilterModel();

        toggleFilters = () => this.$mdSidenav('filternav').toggle();

        reset = () => {
            this.filters = new Models.EnrollmentTrendsFilterModel();
        }

        resetColors = () => {
            angular.forEach(this.charts, chart => {
                if (chart.Chart) {
                    chart.Options.animation = { duration: 1000 },
                    chart.Colors = this.services.colorGradient.getColors(chart.Chart.Data.length);
                    //this.services.timeout(() => chart.Options.animation = false, 1500);
                }
            });
        }

        apply = () => {

            angular.forEach(this.charts, chart => {

                return this.api.enrollmentTrends[chart.ChartCall](this.filters)
                    .then((result: Models.EnrollmentTrendsChartModel<number>) => {
                        //Sets the current card state to default on the first call
                        if (!chart.Chart) {
                            chart.ShowChart = result.ShowChart;
                            chart.Chart = result;
                            chart.Options = {
                                responsive: true,
                                maintainAspectRatio: false,
                                legend: { display: true, position: 'bottom' },
                                scales: {
                                    yAxes: [{
                                        ticks: {
                                            beginAtZero: true
                                        }
                                    }]
                                }
                            };
                        } else {
                            chart.Options.animation = {duration: 1000},
                            chart.Chart.Labels = result.Labels;
                            chart.Chart.Data = result.Data;
                            chart.Chart.Headers = result.Headers;
                            chart.Chart.Series = result.Series;
                            chart.Chart.Totals = result.Totals;
                        }

                        chart.Colors = this.services.colorGradient.getColors(result.Data.length);

                        // Workout around redrawing causes messup animation
                        //this.services.timeout(() => chart.Options.animation = false, 1500);
                    });
            });

        }

        constructor(
            public $rootScope,
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

            this.services.timeout(() =>
            {
                $rootScope.$on('theme-change', () => {
                    this.resetColors();
                }); 
            }, 1000);

            angular.forEach(schoolYears, year => {
                this.displaySchoolYears[year.Value] = year.Display;
            });

            this.apply();
        }
    }

    class EnrollmentTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.enrollment-trends', {
                url: '/enrollment-trends',
                views: {
                    'report@app.reports': {
                        templateUrl: `${settings.moduleBaseUri}/reports/enrollment-trends/enrollment-trends.view.html`,
                        controller: EnrollmentTrendsController,
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
        .module('app.reports.enrollment-trends', [])
        .config(EnrollmentTrendsConfig);
}