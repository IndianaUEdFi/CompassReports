using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class AssessmentFact
    {
        [Key, Column(Order = 0)]
        public int DemographicId { get; set; }
        public virtual DemographicJunkDimension Demographic { get; set; }

        [Key, Column(Order = 1)]
        public int SchoolId { get; set; }
        public virtual SchoolDimension School { get; set; }

        [Key, Column(Order = 2)]
        public short SchoolYear { get; set; }
        public virtual SchoolYearDimension SchoolYearDimension { get; set; }

        [Key, Column(Order = 3)]
        public int AssessmentId { get; set; }
        public virtual AssessmentDimension Assessment { get; set; }

        [Key, Column(Order = 4)]
        public int PerformanceLeveld { get; set; }
        public virtual PerformanceLevelDimension PerformanceLevel { get; set; }

        [Key, Column(Order = 5)]
        public int GoodCauseExemptionId { get; set; }
        public virtual GoodCauseExemptionJunkDimension GoodCauseExemption { get; set; }

        public int Count { get; set; }
    }
}