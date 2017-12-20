using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("DemographicJunkDimension", Schema = "cmp")]
    public class DemographicJunkDimension : EntityBase
    {
        [Key]
        public int DemographicKey { get; set; }

        [Required]
        [MaxLength(50)]
        public string GradeLevel { get; set; }

        [Required]
        [MaxLength(10)]
        public string GradeLevelSort { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ethnicity { get; set; }

        [Required]
        [MaxLength(50)]
        public string FreeReducedLunchStatus { get; set; }

        [Required]
        [MaxLength(50)]
        public string SpecialEducationStatus { get; set; }

        [Required]
        [MaxLength(50)]
        public string EnglishLanguageLearnerStatus { get; set; }

        public short ExpectedGraduationYear { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
        public ICollection<AttendanceFact> AttendanceFacts { get; set; }
        public ICollection<EnrollmentFact> EnrollmentFacts { get; set; }
        public ICollection<GraduationFact> GraduationFacts { get; set; }
    }
}