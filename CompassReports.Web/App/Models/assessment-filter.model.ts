module App.Models {
    export class AssessmentFilterModel implements IReportFilterModel {
        SchoolYear?: number;
        SchoolYears?: number[];

        Assessments: number[];
        EnglishLanguageLearnerStatuses: string[];
        Ethnicities: string[];
        GoodCauseExcemptions: number[];
        LunchStatuses: string[];
        PerformanceLevels: number[];
        SpecialEducationStatuses: string[];

        AssessmentTitle: string;
        Subject: string;

        isFiltering = () => {
            if (this.AssessmentTitle) return true;
            if (this.Subject) return true;
            if (this.EnglishLanguageLearnerStatuses != null && this.EnglishLanguageLearnerStatuses.length) return true;
            if (this.Ethnicities != null && this.Ethnicities.length) return true;
            if (this.SchoolYears != null && this.SchoolYears.length) return true;
            if (this.LunchStatuses != null && this.LunchStatuses.length) return true;
            if (this.SpecialEducationStatuses != null && this.SpecialEducationStatuses.length) return true;
            if (this.PerformanceLevels != null && this.PerformanceLevels.length) return true;
            if (this.GoodCauseExcemptions != null && this.GoodCauseExcemptions.length) return true;
            if (this.Assessments != null && this.Assessments.length) return true;

            return false;
        }

        reset = () => {
            this.Ethnicities = [];
            this.SchoolYears = [];
            this.EnglishLanguageLearnerStatuses = [];
            this.LunchStatuses = [];
            this.SpecialEducationStatuses = [];
            this.PerformanceLevels = [];
            this.GoodCauseExcemptions = [];
            this.Assessments = [];
            this.AssessmentTitle = null;
            this.SchoolYear = null;
            this.Subject = null;
        }

        constructor() {
            this.reset();
        }
    }
}