module App.Models {
    export interface IReportFilterModel {
        isFiltering: () => boolean;
        reset: () => void;
    }
}