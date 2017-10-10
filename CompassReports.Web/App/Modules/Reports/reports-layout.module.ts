module App.Reports {

    class ReportsLayoutController {
        static $inject = ['assessments', '$mdSidenav'];

        isOpen = false;
        menuId = 0;

        toggleExpanded = (selectedMenuId: number) => {
            if (!this.isOpen) {
                this.isOpen = true;
                this.menuId = selectedMenuId;
            } else {
                if (this.menuId === selectedMenuId) {
                    this.isOpen = false;
                    this.menuId = 0;
                } else {
                    this.menuId = selectedMenuId;
                }
            }
        }

        toggleSidenav = () => {
            this.isOpen = !this.isOpen;
            if (this.menuId === 0) this.menuId = 1;
        }

        isSelected = (value: number) => {
            return this.menuId === value;
        }

        toggleThemes = () => this.$mdSidenav('colornav').toggle();

        constructor(
            public assessments: Models.FilterValueModel[],
            private readonly $mdSidenav: ng.material.ISidenavService) {
        }            
    }

    class ReportsLayoutConfig {

        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {
            $stateProvider
                .state('app.reports', {
                    abstract: true,
                    views: {
                        'content@': {
                            templateUrl: `${settings.moduleBaseUri}/reports/reports-layout.view.html`,
                            controller: ReportsLayoutController,
                            controllerAs: 'ctrl',
                            resolve: {
                                assessments: ['api', (api: IApi) => {
                                    return api.assessmentFilters.getAssessments();
                                }]
                            }
                        }
                    }
                });
        }
    }

    angular
        .module('app.reports', [
            'app.reports.assessment',
            'app.reports.assessment-trends',
            'app.reports.attendance',
            'app.reports.attendance-trends',
            'app.reports.college-readiness',
            'app.reports.college-readiness-trends',
            'app.reports.enrollment',
            'app.reports.enrollment-trends',
            'app.reports.graduate-trends',
            'app.reports.graduates',
            'app.reports.home'
        ])
        .config(ReportsLayoutConfig);
}