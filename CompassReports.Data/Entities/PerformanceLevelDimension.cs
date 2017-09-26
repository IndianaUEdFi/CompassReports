using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class PerformanceLevelDimension
    {
        [Key]
        public int PerformanceLevelId { get; set; }

        [MaxLength(50)]
        public string PerformanceLevel { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}