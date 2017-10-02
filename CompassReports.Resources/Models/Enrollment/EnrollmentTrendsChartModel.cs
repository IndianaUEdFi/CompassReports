using System;
using System.Collections.Generic;

namespace CompassReports.Resources.Models.Enrollment
{
    public class EnrollmentTrendsChartModel<T>
    {
        public string Title { get; set; }

        public List<string> Headers { get; set; }

        public List<string> Series { get; set; }

        public List<string> Labels { get; set; }

        public List<List<T>> Data { get; set; }

        public bool ShowChart { get; set; }
    }
}
