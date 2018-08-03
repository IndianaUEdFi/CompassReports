module App.Models {
    export interface IReportFilterModel {
        addChartFilters?: (chart: ChartModel) => void;
        filteringCount: () => number;
        isFiltering: () => boolean;
        reset: () => void;

        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        LunchStatuses: string[];
        SpecialEducationStatuses: string[];
        Districts: number[];
        Schools: number[];
    }
}