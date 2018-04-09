using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompassReports.Resources.Models
{
    public class GraduateTrendGroupByModel
    {
        public short SchoolYear { get; set; }

        public string SchoolYearDescription { get; set; }

        public short ExpectedGraduationYear { get; set; }

        public string ChartGroupBy { get; set; }

        public string Property { get; set; }
    }
}
