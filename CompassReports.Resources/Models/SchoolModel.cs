using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class SchoolModel
    {
        public int Id { get; set; }

        public string SchoolName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName { get; set; }
    }
}
