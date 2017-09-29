using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("AssessmentFact", Schema = "cmp")]
    public class AssessmentFact : EntityBase
    {
        [Key, Column(Order = 0)]
        public int DemographicKey { get; set; }
        public virtual DemographicJunkDimension Demographic { get; set; }

        [Key, Column(Order = 1)]
        public int SchoolKey { get; set; }
        public virtual SchoolDimension School { get; set; }

        [Key, Column(Order = 2)]
        public short SchoolYearKey { get; set; }
        public virtual SchoolYearDimension SchoolYearDimension { get; set; }

        [Key, Column(Order = 3)]
        public int AssessmentKey { get; set; }
        public virtual AssessmentDimension Assessment { get; set; }

        [Key, Column(Order = 4)]
        public int PerformanceLevelKey { get; set; }
        public virtual PerformanceLevelDimension PerformanceLevel { get; set; }

        [Key, Column(Order = 5)]
        public int GoodCauseExemptionKey { get; set; }
        public virtual GoodCauseExemptionJunkDimension GoodCauseExemption { get; set; }

        [Required]
        public int AssessmentStudentCount { get; set; }
    }
}