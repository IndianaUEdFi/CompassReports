module App.Models {
    export class GraduateFilterModel implements IReportFilterModel {
        ExpectedGraduationYear?: number;
        ExpectedGraduationYears?: number[];
        CohortYear: number;

        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        LunchStatuses: string[];
        SpecialEducationStatuses: string[];

        isFiltering = () => {
            if (this.ExpectedGraduationYear) return true;
            if (this.CohortYear) return true;
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