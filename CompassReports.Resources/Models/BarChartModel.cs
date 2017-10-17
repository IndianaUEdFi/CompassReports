using System;
using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class BarChartModel<T>
    {
        public List<List<double>> PercentageData { get; set; }

        public List<List<T>> TotalData { get; set; }

        public List<List<T>> Data { get; set; }

        public List<string> Headers { get; set; }

        public bool HideTotal { get; set; }

        public List<string> Labels { get; set; }

        public bool Percentage { get; set; }

        public List<string> Series { get; set; }

        public bool ShowChart { get; set; }

        public bool SingleSeries { get; set; }

        public string Title { get; set; }

        public string TotalRowTitle { get; set; }

        public List<int> Totals { get; set; }

        public int? MaxYAxisValue { get; set; }

    }
}
