using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("PerformanceLevelDimension", Schema = "cmp")]
    public class PerformanceLevelDimension
    {
        [Key]
        public int PerformanceLevelKey { get; set; }

        [Required]
        [MaxLength(50)]
        public string PerformanceLevel { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}