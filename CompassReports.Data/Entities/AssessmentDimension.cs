using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("AssessmentDimension", Schema = "cmp")]
    public class AssessmentDimension : EntityBase
    {
        [Key]
        public int AssessmentKey { get; set; }

        [Required]
        [MaxLength(60)]
        public string AssessmentTitle { get; set; }

        [Required]
        [MaxLength(50)]
        public string AssessedGradeLevel { get; set; }

        [Required]
        [MaxLength(50)]
        public string AcademicSubject { get; set; }

        public int? MaxScore { get; set; }
        
        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}