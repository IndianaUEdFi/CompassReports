module App.Models {
    export class AssessmentFilterModel implements IReportFilterModel {
        SchoolYear?: number;
        SchoolYears?: number[];

        Assessments: number[];
        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        LunchStatuses: string[];
        SpecialEducationStatuses: string[];

        AssessmentTitle: string;
        Subject: string;
        PerformanceKey: number;

        addChartFilters = (chart: Models.ChartModel) => {
            if(chart.ChartFilters)
             this.PerformanceKey = chart.ChartFilters.PerformanceKey;
        }

        filteringCount = () => {
            let count = 0;

            if (this.Subject) count++;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) count++;
            if (this.Ethnicities != null && this.Ethnicities.length) count++;
            if (this.SchoolYears != null && this.SchoolYears.length) count++;
            if (this.LunchStatuses != null && this.LunchStatuses.length) count++;
            if (this.SpecialEducationStatuses != null && this.SpecialEducationStatuses.length) count++;
            if (this.Assessments != null && this.Assessments.length) count++;

            return count;
        }

        isFiltering = () => {
            if (this.Subject) return true;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) return true;
            if (this.Ethnicities != null && this.Ethnicities.length) return true;
            if (this.SchoolYears != null && this.SchoolYears.length) return true;
            if (this.LunchStatuses != null && this.LunchStatuses.length) return true;
            if (this.SpecialEducationStatuses != null && this.SpecialEducationStatuses.length) return true;
            if (this.Assessments != null && this.Assessments.length) return true;

            return false;
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