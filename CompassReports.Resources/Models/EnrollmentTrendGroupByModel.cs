using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompassReports.Resources.Models
{
    public class EnrollmentTrendGroupByModel
    {
        public short SchoolYear { get; set; }

        public string SchoolYearDescription { get; set; }

        public string Property { get; set; }

        public string SortOrder { get; set; }
    }
}
