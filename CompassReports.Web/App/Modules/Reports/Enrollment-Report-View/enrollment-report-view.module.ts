/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports {

    import ChartModel = Models.ChartModel;
    import FilterModel = Models.FilterModel;
    import FilterValueModel = Models.FilterValueModel;

    export class EnrollmentReportView extends ReportBaseView {

        constructor(settings: ISystemSettings, title: string, multipleSchoolYears: boolean, charts: ChartModel[]) {
            super(settings);

            this.resolve = new EnrollmentReportResolve(title, multipleSchoolYears, charts);
        }
    }

    class EnrollmentReportResolve extends ReportBaseResolve {

        report: any[];

        constructor(title: string, multipleSchoolYears: boolean, charts: ChartModel[]) {
            super(multipleSchoolYears);

            this.report = ['baseFilters', 'schoolYears',
                (baseFilters: FilterModel<any>[], schoolYears: FilterValueModel[]) => {

                    var model = multipleSchoolYears
                        ? new Models.EnrollmentFilterModel()
                        : new Models.EnrollmentFilterModel(schoolYears[0].Value as number);

                    return {
                        filters: baseFilters,
                        charts: charts,
                        title: title,
                        model: model
                    }
                }
            ];
        }
    }
}