module App.Reports {

    import FilterModel = Models.FilterModel;
    import SchoolFilterModel = Models.SchoolFilterModel;

    export class ReportBaseView {
        templateUrl: string;
        controller: ng.IController;
        controllerAs: string;
        resolve: any;

        constructor(settings: ISystemSettings) {
            this.templateUrl = `${settings.moduleBaseUri}/reports/report-base/report-base.view.html`;
            this.controller = ReportBaseController;
            this.controllerAs = 'ctrl';
        }
    }

    export class ReportBaseController implements ng.IController {
        static $inject = ['$rootScope', '$mdSidenav', 'services', 'report'];

        filterForm: ng.IFormController;

        apply = () => {
            this.$rootScope.$emit('update-charts');
            this.filterForm.$setPristine();
        };

        filteringCount = () => this.report.model.filteringCount();

        goBack = () => {
            this.$rootScope.filterModel = this.report.model;
            this.services.state.go(this.report.backState, this.report.backParameters);
        }

        isFiltering = () => this.report.model.isFiltering();

        reset = () => this.report.model.reset();

        toggleFilters = () => this.$mdSidenav('filternav').toggle();

        constructor(
            public $rootScope,
            private readonly $mdSidenav: ng.material.ISidenavService,
            private readonly services: IServices,
            private readonly report: Models.BaseReport
        ) {
            services.timeout(() => {
                $mdSidenav('filternav').onClose(() => {
                    if (!this.filterForm.$pristine) {
                        this.apply();
                    }
                });
            });
        }
    }

    export class ReportBaseResolve {

        baseFilters: any[];

        englishLanguageLearnerStatuses: any[] = ['api', (api: IApi) => api.demographicFilters.getEnglishLanguageLearnerStatuses()];

        ethnicities: any[] = ['api', (api: IApi) => api.demographicFilters.getEthnicities()];

        grades: any[] = ['api', (api: IApi) => api.demographicFilters.getGrades()];

        lunchStatuses: any[] = ['api', (api: IApi) => api.demographicFilters.getLunchStatuses()];

        specialEducationStatuses: any[] = ['api', (api: IApi) => api.demographicFilters.getSpecialEducationStatuses()];

        schools: any[] = ['api', (api: IApi) => api.school.getAll()];

        schoolYears: any[] = ['api', (api: IApi) => api.enrollmentFilters.getSchoolYears() ];

        constructor(multipleSchoolYears?: boolean) {

            this.baseFilters = ['englishLanguageLearnerStatuses', 'ethnicities', 'grades',
                'lunchStatuses', 'specialEducationStatuses', 'schools', 'schoolYears', (
                    englishLanguageLearnerStatuses: string[], ethnicities: string[], grades: string[],
                    lunchStatuses: string[], specialEducationStatuses: string[], schools: Models.SchoolModel[],
                    schoolYears: Models.FilterValueModel[]
                ) => {


                    const schoolYearFilter = (multipleSchoolYears)
                        ? new Models.FilterModel<number>(schoolYears, 'School Years', 'SchoolYears', true)
                        : new FilterModel<number>(schoolYears, 'School Year', 'SchoolYear', false);

                    return [
                        schoolYearFilter,
                        new SchoolFilterModel(schools),
                        new FilterModel<number>(grades, 'Grades', 'Grades', true),
                        new FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                        new FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                        new FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                        new FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                        new FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                    ];
                }];
        }
    }
}