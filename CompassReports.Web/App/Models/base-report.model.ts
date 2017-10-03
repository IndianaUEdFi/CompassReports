module App.Models {
    export class BaseReport {
        title: string;
        api: string;
        charts: Models.ChartModel<number>[];
        filters: Models.FilterModel<any>[];
        model: IReportFilterModel;
    }
}