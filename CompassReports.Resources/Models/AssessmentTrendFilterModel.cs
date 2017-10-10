using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class AssessmentTrendFilterModel
    {
        public List<int> Assessments { get; set; }
        public string AssessmentTitle { get; set; }
        public List<string> EnglishLanguageLearnerStatuses { get; set; }
        public List<string> Ethnicities { get; set; }
        public List<int> GoodCauseExcemptions { get; set; }
        public List<string> LunchStatuses { get; set; }
        public int? PerformanceKey { get; set; }
        public List<short> SchoolYears { get; set; }
        public List<string> SpecialEducationStatuses { get; set; }
        public string Subject { get; set; }
    }
}
