using System;
using System.Collections.Generic;

namespace CompassReports.Resources.Models.Enrollment
{
    public class EnrollmentChartModel<T>
    {
        public string Title { get; set; }

        public List<string> Headers { get; set; }

        public List<string> Labels { get; set; }

        public List<T> Data { get; set; }

        public bool ShowChart { get; set; }
    }
}
