module App.Reports {

    import DistrictFilterModel = Models.DistrictFilterModel;
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


    function onDistrictSelect(model: Models.IReportFilterModel, filters: Models.FilterModel<any>[], api: IApi) {

        let schoolFilterIndex = null;

        angular.forEach(filters, (filter, index) => {
            if (filter.Title === 'Schools') schoolFilterIndex = index;
        });

        if (model.Districts && model.Districts.length) {
            api.school.getAll(model.Districts).then((schools: Models.SchoolModel[]) => {
                const values: Models.FilterValueModel[] = [];

                for (let i = 0; i < schools.length; i++) {
                    values.push(new Models.FilterValueModel(schools[i].Id,
                        schools[i].SchoolName,
                        schools[i].DistrictName));
                }

                filters[schoolFilterIndex].update(values);
            });
        } else {
            model.Schools = [];
            filters[schoolFilterIndex].update([] as Models.FilterValueModel[]);
        }
    }

    export class ReportBaseResolve {

        baseFilters: any[];

        districts: any[] = ['api', (api: IApi) => api.district.getAll()];

        englishLanguageLearnerStatuses: any[] = ['api', (api: IApi) => api.demographicFilters.getEnglishLanguageLearnerStatuses()];

        ethnicities: any[] = ['api', (api: IApi) => api.demographicFilters.getEthnicities()];

        grades: any[] = ['api', (api: IApi) => api.demographicFilters.getGrades()];

        lunchStatuses: any[] = ['api', (api: IApi) => api.demographicFilters.getLunchStatuses()];

        specialEducationStatuses: any[] = ['api', (api: IApi) => api.demographicFilters.getSpecialEducationStatuses()];

        schools: any[] = ['api', 'districts', (api: IApi, districts: Models.DistrictModel[]) => {
            if (districts.length > 1) return [];
            else return api.school.getAll([districts[0].Id]);
        }];

        schoolYears: any[] = ['api', (api: IApi) => api.enrollmentFilters.getSchoolYears() ];

        constructor(multipleSchoolYears?: boolean) {

            this.baseFilters = ['districts', 'englishLanguageLearnerStatuses', 'ethnicities', 'grades',
                'lunchStatuses', 'specialEducationStatuses', 'schools', 'schoolYears', (
                    districts: Models.DistrictModel[], englishLanguageLearnerStatuses: string[], ethnicities: string[],
                    grades: string[], lunchStatuses: string[], specialEducationStatuses: string[], schools: Models.SchoolModel[],
                    schoolYears: Models.FilterValueModel[]
                ) => {

                    const schoolYearFilter = (multipleSchoolYears)
                        ? new Models.FilterModel<number>(schoolYears, 'School Years', 'SchoolYears', true)
                        : new FilterModel<number>(schoolYears, 'School Year', 'SchoolYear', false);

                    return [
                        schoolYearFilter,
                        new DistrictFilterModel(districts, onDistrictSelect),
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