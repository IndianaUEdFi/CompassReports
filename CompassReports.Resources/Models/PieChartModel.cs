using System;
using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class PieChartModel<T>
    {
        public List<T> Data { get; set; }

        public List<string> Headers { get; set; }

        public bool HideTotal { get; set; }

        public List<string> Labels { get; set; }

        public List<string> PercentageHeaders { get; set; }

        public List<double> Percentages { get; set; }

        public bool ShowChart { get; set; }

        public string Title { get; set; }

        public int Total { get; set; }

        public string TotalRowTitle { get; set; }
    }
}
