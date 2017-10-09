module App.Models {
    export interface IReportFilterModel {
        filteringCount: () => number;
        isFiltering: () => boolean;
        reset: () => void;
    }
}