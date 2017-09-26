using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class GoodCauseExemptionJunkDimension
    {
        [Key]
        public int GoodCauseExemptionId { get; set; }

        [MaxLength(50)]
        public string GoodCauseExemptionGranted { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}