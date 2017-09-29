using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("EnrollmentFact", Schema = "cmp")]
    public class EnrollmentFact : EntityBase
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

        public int EnrollmentStudentCount { get; set; }
    }
}