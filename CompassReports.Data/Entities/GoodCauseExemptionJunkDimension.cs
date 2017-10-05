using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("GoodCauseExemptionJunkDimension", Schema = "cmp")]
    public class GoodCauseExemptionJunkDimension : EntityBase
    {
        [Key]
        public int GoodCauseExemptionKey { get; set; }

        [Required]
        [MaxLength(50)]
        public string GoodCauseExemption { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}