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

        filteringCount = () => {
            let count = 0;

            if (this.ExpectedGraduationYear) count++;
            if (this.CohortYear != null) count++;
            if (this.GradCohortYearDifference != null) count++;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) count++;
            if (this.Ethnicities != null && this.Ethnicities.length) count++;
            if (this.ExpectedGraduationYears != null && this.ExpectedGraduationYears.length) count++;
            if (this.LunchStatuses != null && this.LunchStatuses.length) count++;
            if (this.SpecialEducationStatuses != null && this.SpecialEducationStatuses.length) count++;

            return count;
        }

        isFiltering = () => {
            if (this.ExpectedGraduationYear) return true;
            if (this.CohortYear != null) return true;
            if (this.GradCohortYearDifference != null) return true;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) return true;
            if (this.Ethnicities != null && this.Ethnicities.length) return true;
            if (this.ExpectedGraduationYears != null && this.ExpectedGraduationYears.length) return true;
            if (this.LunchStatuses != null && this.LunchStatuses.length) return true;
            if (this.SpecialEducationStatuses != null && this.SpecialEducationStatuses.length) return true;
   
            return false;
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