using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("GraduationFact", Schema = "cmp")]
    public class GraduationFact
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
        public int GraduationStatusKey { get; set; }
        public virtual GraduationStatusJunkDimension GraduationStatus { get; set; }

        public int GraduationStudentCount { get; set; }
    }
}