using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class DemographicJunkDimension
    {
        [Key]
        public int DemographicId { get; set; }

        [MaxLength(50)]
        public string GradeLevel { get; set; }

        [MaxLength(50)]
        public string Ethnicity { get; set; }

        [MaxLength(50)]
        public string FreeReducedLunchStatus { get; set; }

        [MaxLength(50)]
        public string SpecialEducationStatus { get; set; }

        [MaxLength(50)]
        public string EnglishLanguageLearnerStatus { get; set; }

        [MaxLength(50)]
        public string ExpectedGraduationYear { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
        public ICollection<AttendanceFact> AttendanceFacts { get; set; }
        public ICollection<EnrollmentFact> EnrollmentFacts { get; set; }
        public ICollection<GraduationFact> GraduationFacts { get; set; }
    }
}