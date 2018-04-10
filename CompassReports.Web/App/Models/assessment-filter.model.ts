module App.Models {
    export class AssessmentFilterModel implements IReportFilterModel {
        SchoolYear?: number;
        SchoolYears?: number[];

        Assessments: number[];
        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        LunchStatuses: string[];
        SpecialEducationStatuses: string[];

        Schools: number[];
        Districts: number[];

        AssessmentTitle: string;
        Subject: string;
        PerformanceKey: number;

        addChartFilters = (chart: Models.ChartModel) => {
            if(chart.ChartFilters)
             this.PerformanceKey = chart.ChartFilters.PerformanceKey;
        }

        filteringCount = () => {
            //Starts at one since Grades always shows
            let count = 1;

            if (this.Subject) count++;
            if (this.SchoolYear != null) count++;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) count++;
            if (this.Ethnicities != null && this.Ethnicities.length) count++;
            if (this.SchoolYears != null && this.SchoolYears.length) count++;
            if (this.LunchStatuses != null && this.LunchStatuses.length) count++;
            if (this.SpecialEducationStatuses != null && this.SpecialEducationStatuses.length) count++;
            if (this.Schools != null && this.Schools.length) count++;
            if (this.Districts != null && this.Districts.length) count++;

            return count;
        }

        isFiltering = () => {
            return this.filteringCount() > 0;
        }

        reset = () => {
            this.Ethnicities = [];
            this.SchoolYears = [];
            this.EnglishLanguageLearnerStatuses = [];
            this.LunchStatuses = [];
            this.SpecialEducationStatuses = [];
            this.Assessments = [];
            this.SchoolYear = null;
            this.Subject = null;
        }

        constructor() {
            this.reset();
        }
    }
}