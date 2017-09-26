using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class EnrollmentFact
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

        public int Count { get; set; }
    }
}