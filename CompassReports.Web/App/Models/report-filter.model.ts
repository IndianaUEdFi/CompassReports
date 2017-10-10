module App.Models {
    export interface IReportFilterModel {
        addChartFilters?: (chart: ChartModel) => void;
        filteringCount: () => number;
        isFiltering: () => boolean;
        reset: () => void;
    }
}