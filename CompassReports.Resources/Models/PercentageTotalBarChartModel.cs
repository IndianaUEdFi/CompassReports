using System;
using System.Collections.Generic;

namespace CompassReports.Resources.Models
{
    public class PercentageTotalDataModel
    {
        public double Percentage { get; set; }

        public int Total { get; set; }
    }

    public class PercentageTotalBarChartModel
    {
        public List<List<PercentageTotalDataModel>> Data { get; set; }

        public List<string> Headers { get; set; }

        public bool HideTotal { get; set; }

        public List<string> Labels { get; set; }

        public List<string> Series { get; set; }

        public bool ShowChart { get; set; }

        public bool ShowPercentage { get; set; }

        public bool SingleSeries { get; set; }

        public string Title { get; set; }
        
        public string TotalRowTitle { get; set; }

        public List<PercentageTotalDataModel> Totals { get; set; }

    }
}
