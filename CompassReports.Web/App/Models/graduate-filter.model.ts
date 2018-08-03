module App.Models {
    export class GraduateFilterModel implements IReportFilterModel {
        ExpectedGraduationYear?: number;
        ExpectedGraduationYears?: number[];
        CohortYear: number;
        GradCohortYearDifference: number;

        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        LunchStatuses: string[];
        SpecialEducationStatuses: string[];

        Schools: number[];
        Districts: number[];

        filteringCount = () => {
            let count = 1; // Grades always shows

            if (this.ExpectedGraduationYear != null) count++;
            if (this.CohortYear != null) count++;
            if (this.GradCohortYearDifference != null) count++;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) count++;
            if (this.Ethnicities != null && this.Ethnicities.length) count++;
            if (this.ExpectedGraduationYears != null && this.ExpectedGraduationYears.length) count++;
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
            this.ExpectedGraduationYears = [];
            this.EnglishLanguageLearnerStatuses = [];
            this.LunchStatuses = [];
            this.SpecialEducationStatuses = [];
            this.ExpectedGraduationYear = null;
            this.CohortYear = null;
        }

        constructor() {
            this.reset();
        }
    }
}