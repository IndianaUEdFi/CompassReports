using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class AssessmentFilterModel
    {
        public List<int> Assessments { get; set; }
        public string AssessmentTitle { get; set; }
        public List<string> EnglishLanguageLearnerStatuses { get; set; }
        public List<string> Ethnicities { get; set; }
        public List<int> GoodCauseExcemptions { get; set; }
        public List<string> LunchStatuses { get; set; }
        public List<int> PerformanceLevels { get; set; }
        public short SchoolYear { get; set; }
        public List<short> SchoolYears { get; set; }
        public List<string> SpecialEducationStatuses { get; set; }
        public string Subject { get; set; }
    }
}
