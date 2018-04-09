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
    public interface IAssessmentPerformanceTrendService
    {
        Task<PercentageTotalBarChartModel> Get(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentPerformanceTrendService : IAssessmentPerformanceTrendService
    {
        private readonly IAssessmentFactService _assessmentFactService;
        private readonly IRepository<PerformanceDimension> _performanceRepository;

        public AssessmentPerformanceTrendService(IAssessmentFactService assessmentFactService,
            IRepository<PerformanceDimension> performanceRepository)
        {
            _assessmentFactService = assessmentFactService;
            _performanceRepository = performanceRepository;
        }

        public class AssessmentTrendChartGroupBy
        {
            public short SchoolYear { get; set; }
            public string SchoolYearDescription { get; set; }
            public int PerformanceKey { get; set; }
            public string Property { get; set; }
        }

        public async Task<PercentageTotalBarChartModel> Get(AssessmentFilterModel model)
        {
            var query = _assessmentFactService.BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Performance.PerformanceLevel, x.PerformanceKey })
                .Select(x => new
                {
                    PerformanceLevel = x.Key.PerformanceLevel,
                    PerformanceKey = x.Key.PerformanceKey,
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", "Performance Levels" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();

            var performanceLevels = (model.PerformanceKey.HasValue ? results.Where(x => x.PerformanceKey == model.PerformanceKey) : results)
                .Select(x => x.PerformanceLevel).Distinct().OrderBy(x => x).ToList();

            var total = results.Sum(x => x.Total);
            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Sum(y => y.Total), total),
                    Total = x.Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var performanceLevel in performanceLevels)
            {
                var values = new List<PercentageTotalDataModel>();

                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.PerformanceLevel == performanceLevel && x.SchoolYear == schoolYear);
                    var rowTotal = row == null ? 0 : row.Total;
                    var schoolYearTotal = results.Where(x => x.SchoolYear == schoolYear).Sum(x => x.Total);
                    values.Add(new PercentageTotalDataModel
                    {
                        Percentage = rowTotal == 0 ? 0 : GetPercentage(rowTotal, schoolYearTotal),
                        Total = rowTotal
                    });
                }
                data.Add(values);
            }

            string title = null;
            if (model.PerformanceKey.HasValue)
            {
                var performance = _performanceRepository.GetAll().FirstOrDefault(x => x.PerformanceKey == model.PerformanceKey.Value);

                if (performance == null) title = "Performance Trend";
                else title = performance.PerformanceLevel + " Trend";

            } else
                title = "Performance Trend";

            return new PercentageTotalBarChartModel
            {
                Title = title,
                Headers = headers,
                HideTotal = model.PerformanceKey.HasValue,
                HidePercentageTotal = true,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = performanceLevels.Select(x => x.ToString()).ToList(),
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = "Participation ",
                Totals = totals
            };
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentTrendChartGroupBy
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    PerformanceKey = x.PerformanceKey,
                    Property = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await CreateChart(grouping, model.PerformanceKey, "Language Statues", "English Language Learner");
        }

        public async Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentTrendChartGroupBy
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    PerformanceKey = x.PerformanceKey,
                    Property = x.Demographic.Ethnicity
                });

            return await CreateChart(grouping, model.PerformanceKey, "Ethnicities", "Ethnicity");
        }
      
        public async Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentTrendChartGroupBy
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    PerformanceKey = x.PerformanceKey,
                    Property = x.Demographic.FreeReducedLunchStatus
                });

            return await CreateChart(grouping, model.PerformanceKey, "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model)
        {
            var grouping = _assessmentFactService.BaseQuery(model)
                .GroupBy(x => new AssessmentTrendChartGroupBy
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    PerformanceKey = x.PerformanceKey,
                    Property = x.Demographic.SpecialEducationStatus
                });

            return await CreateChart(grouping, model.PerformanceKey, "Education Statuses", "Special Education");
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double)subTotal / (double)total), 2);
        }

        private async Task<PercentageTotalBarChartModel> CreateChart(IQueryable<IGrouping<AssessmentTrendChartGroupBy, AssessmentFact>> groupings, int? performanceKey, string header, string title)
        {
            var results = await groupings
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYear,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Property = x.Key.Property,
                    PerformanceKey = x.Key.PerformanceKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToListAsync();

            var performance = await _performanceRepository.GetAll().FirstOrDefaultAsync(x => x.PerformanceKey == performanceKey.Value);
            if (performance == null || !performanceKey.HasValue)
                throw new Exception("Performance Level does not Exist");

            var headers = new List<string> { "", header };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Where(y => y.PerformanceKey == performanceKey.Value).Sum(y => y.Total), x.Sum(y => y.Total)),
                    Total = x.Where(y => y.PerformanceKey == performanceKey.Value).Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var property in properties)
            {
                var values = new List<PercentageTotalDataModel>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear && x.PerformanceKey == performanceKey.Value);
                    var rowTotal = row == null ? 0 : row.Total;
                    var propertyTotal = results.Where(x => x.Property == property && x.SchoolYear == schoolYear).Sum(x => x.Total);
                    values.Add(new PercentageTotalDataModel
                    {
                        Percentage = rowTotal == 0 ? 0 : GetPercentage(rowTotal, propertyTotal),
                        Total = rowTotal
                    });
                }
                data.Add(values);
            }

            return new PercentageTotalBarChartModel
            {
                Title = performance.PerformanceLevel + " Trend By " + title,
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = properties,
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = performance.PerformanceLevel,
                Totals = totals
            };
        }
    }
}
