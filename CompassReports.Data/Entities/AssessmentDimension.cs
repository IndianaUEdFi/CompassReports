using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class AssessmentDimension
    {
        [Key]
        public int AssessmentId { get; set; }

        [MaxLength(60)]
        public string AssessmentTitle { get; set; }

        [MaxLength(50)]
        public string AssessmentPeriod { get; set; }

        [MaxLength(50)]
        public string AssessedGradeLevel { get; set; }

        [MaxLength(50)]
        public string AcademicSubject { get; set; }
        
        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}