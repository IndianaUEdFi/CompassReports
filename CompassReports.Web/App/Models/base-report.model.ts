module App.Models {
    export class BaseReport {
        title: string;
        api: string;
        charts: Models.ChartModel[];
        filters: Models.FilterModel<any>[];
        model: IReportFilterModel;
    }
}