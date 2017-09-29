using System.Collections.Generic;

namespace CompassReports.Resources.Models.Enrollment
{
    public class EnrollmentFilterModel
    {
        public List<string> EnglishLanguageLearnerStatuses { get; set; }
        public List<string> Ethnicities { get; set; }
        public List<string> Grades { get; set; }
        public List<string> LunchStatuses { get; set; }
        public List<string> SpecialEducationStatuses { get; set; }
        public short SchoolYear { get; set; }
    }
}
