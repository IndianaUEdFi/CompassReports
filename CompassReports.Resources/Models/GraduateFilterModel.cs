using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class GraduateFilterModel
    {
        public short CohortYear { get; set; }
        public int GradCohortYearDifference { get; set; }

        public List<string> EnglishLanguageLearnerStatuses { get; set; }
        public List<string> Ethnicities { get; set; }
        public short ExpectedGraduationYear { get; set; }
        public List<short> ExpectedGraduationYears { get; set; }
        public List<string> LunchStatuses { get; set; }
        public List<string> SpecialEducationStatuses { get; set; }
    }
}
