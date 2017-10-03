using System;
using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class PieChartModel<T>
    {
        public string Title { get; set; }

        public List<string> Headers { get; set; }

        public List<string> Labels { get; set; }

        public List<T> Data { get; set; }

        public bool ShowChart { get; set; }

        public int Total { get; set; }
    }
}
