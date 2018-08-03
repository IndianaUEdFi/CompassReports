/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports {

    import ChartModel = Models.ChartModel;
    import FilterModel = Models.FilterModel;

    function onExpectedGraduationYearChange(model: Models.GraduateFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.ExpectedGraduationYear) {

            model.CohortYear = null;

            filters[1].update([] as Models.FilterValueModel[]);

            api.graduateFilters.getCohorts(model.ExpectedGraduationYear).then((cohorts: Models.FilterValueModel[]) => {
                filters[1].update(cohorts);
                if (cohorts.length)
                    model.CohortYear = cohorts[0].Value as number;
            });
        }
    }

    export class GraduatesReportView extends ReportBaseView {

        constructor(settings: ISystemSettings, title: string, multipleSchoolYears: boolean, charts: ChartModel[]) {
            super(settings);

            this.resolve = new GraduatesReportResolve(title, multipleSchoolYears, charts);
        }
    }

    class GraduatesReportResolve extends ReportBaseResolve {

        report: any[];

        schoolYears: any[] = ['api', (api: IApi) => api.graduateFilters.getSchoolYears()];

        cohorts: any[] = ['api', (api: IApi) => api.graduateFilters.getCohorts()];

        constructor(title: string, multipleSchoolYears: boolean, charts: ChartModel[]) {
            super(multipleSchoolYears);

            if (!multipleSchoolYears)
                this.cohorts = ['$rootScope', 'api', 'schoolYears',
                    ($rootScope: IAppRootScope, api: IApi, schoolYears: Models.FilterValueModel[]) => {
                        if ($rootScope.filterModel) {
                            const model = $rootScope.filterModel as Models.GraduateFilterModel;
                            return api.graduateFilters.getCohorts(model.ExpectedGraduationYear);
                        } else if (schoolYears && schoolYears.length) {
                            return api.graduateFilters.getCohorts(schoolYears[0].Value as number);
                        }
                    }
                ];

            this.report = ['$rootScope', 'baseFilters', 'cohorts', 'districts', 'schoolYears',
                ($rootScope: IAppRootScope, baseFilters: FilterModel<any>[], cohorts: Models.FilterValueModel[], districts: Models.DistrictModel[], schoolYears: Models.FilterValueModel[]) => {

                    // Remove School Year Filter
                    baseFilters.splice(0, 1);

                    var expectedGraduationYearFilter = multipleSchoolYears
                        ? new FilterModel<number>(schoolYears, 'Expected Graduation Years', 'ExpectedGraduationYears', true, true)
                        : new FilterModel<number>(schoolYears, 'Expected Graduation Year', 'ExpectedGraduationYear', false, true, onExpectedGraduationYearChange);

                    var filters = [
                        expectedGraduationYearFilter,
                        new FilterModel<number>(cohorts, 'Cohort', multipleSchoolYears ? 'GradCohortYearDifference' : 'CohortYear', false, true)
                    ].concat(baseFilters);

                    let model = new Models.GraduateFilterModel();

                    if (!multipleSchoolYears) {
                        model.ExpectedGraduationYear = (schoolYears && schoolYears.length) ? schoolYears[0].Value as number : null;
                        model.CohortYear = (cohorts && cohorts.length) ? cohorts[0].Value as number : null;
                    } else if (cohorts.length)
                        model.GradCohortYearDifference = cohorts[0].Value as number;

                    if ($rootScope.filterModel) {
                        model = $rootScope.filterModel as Models.GraduateFilterModel;
                        $rootScope.filterModel = null;
                    } 

                    if (districts.length === 1) {
                        model.Districts = [districts[0].Id];
                    }

                    let backState = null;
                    if ($rootScope.backState) {
                        backState = $rootScope.backState;
                        $rootScope.backState = null;
                    }

                    return {
                        filters: filters,
                        charts: charts,
                        title: title,
                        model: model,
                        backState: backState
                    }
                }
            ];
        }
    }
}