using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentPerformanceService
    {
        Task<PieChartModel<int>> Get(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentPerformanceService : IAssessmentPerformanceService
    {
        private readonly IAssessmentFactService _assessmentFactService;

        public AssessmentPerformanceService(IAssessmentFactService assessmentFactService)
        {
            _assessmentFactService = assessmentFactService;
        }

        public class AssessmentChartGroupBy
        {
            public int PerformanceKey { get; set; }
            public string PerformanceLevel { get; set; }
            public string GroupByProperty { get; set; }
        }

        public async Task<PieChartModel<int>> Get(AssessmentFilterModel model)
        {
            var results = await _assessmentFactService.BaseQuery(model)
                .GroupBy(x => x.Performance.PerformanceLevel)
                .Select(x => new
                {
                    PerformanceLevel = x.Key,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.PerformanceLevel)
                .ToListAsync();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = "Performance Level",
                TotalRowTitle = "Assessment Peformance Total",
                Headers = new List<string> { "", "Performance Level", "Performance Count" },
                PercentageHeaders = new List<string> { "", "Performance Level", "Performance Percentage" },
                Labels = results.Select(x => x.PerformanceLevel).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                Total = total
            };
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentChartGroupBy
                {
                    PerformanceKey = x.PerformanceKey,
                    PerformanceLevel = x.Performance.PerformanceLevel,
                    GroupByProperty = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await CreateChart(grouping, "Language Statuses", "Language Learners");
        }

        public async Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentChartGroupBy
                {
                    PerformanceKey = x.PerformanceKey,
                    PerformanceLevel = x.Performance.PerformanceLevel,
                    GroupByProperty = x.Demographic.Ethnicity
                });

            return await CreateChart(grouping, "Ethnicities", "Ethnicity");
        }

        public async Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentChartGroupBy
                {
                    PerformanceKey = x.PerformanceKey,
                    PerformanceLevel = x.Performance.PerformanceLevel,
                    GroupByProperty = x.Demographic.FreeReducedLunchStatus
                });

            return await CreateChart(grouping, "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentChartGroupBy
                {
                    PerformanceKey = x.PerformanceKey,
                    PerformanceLevel = x.Performance.PerformanceLevel,
                    GroupByProperty = x.Demographic.SpecialEducationStatus
                });

            return await CreateChart(grouping, "Education Statuses", "Special Education");
        }

        private static async Task<PercentageTotalBarChartModel> CreateChart(IQueryable<IGrouping<AssessmentChartGroupBy, AssessmentFact>> grouping, string label, string type)
        {
            var results = await grouping
                .Select(x => new
                 {
                     x.Key.PerformanceLevel,
                     x.Key.PerformanceKey,
                     Property = x.Key.GroupByProperty,
                     Total = x.Sum(y => y.AssessmentStudentCount)
                 }).ToListAsync();

            var performanceLevels = results.Select(x => x.PerformanceLevel).Distinct().OrderBy(x => x).ToList();
            var performanceKeys = results.OrderBy(x => x.PerformanceLevel).Select(x => x.PerformanceKey).Distinct().ToList();

            var headers = new List<string> { "", label };
            headers.AddRange(performanceLevels);

            var propertyValues = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var value in propertyValues)
            {
                var values = new List<PercentageTotalDataModel>();
                var propertyTotal = results.Where(x => x.Property == value).Sum(x => x.Total);
                var properties = results.Where(x => x.Property == value).ToList();

                foreach (var performanceKey in performanceKeys)
                {
                    var rows = properties.Where(x => x.PerformanceKey == performanceKey);
                    var row = rows.FirstOrDefault();
                    var rowTotal = row == null ? 0 : row.Total;
                    values.Add(new PercentageTotalDataModel
                    {
                        Percentage = rowTotal == 0 ? 0 : GetPercentage(rowTotal, propertyTotal),
                        Total = rowTotal
                    });
                }
                data.Add(values);
            }

            var total = results.Sum(x => x.Total);
            var totals = results.GroupBy(x => x.PerformanceLevel)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Sum(y => y.Total), total),
                    Total = x.Sum(y => y.Total)
                }).ToList();

            return new PercentageTotalBarChartModel
            {
                Title = "Performance Level by " + type,
                Headers = headers,
                Labels = performanceLevels,
                Series = propertyValues.Select(x => x.ToString()).ToList(),
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = "Performance Level",
                Totals = totals
            };
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double) subTotal / (double) total), 2);
        }
    }
}
