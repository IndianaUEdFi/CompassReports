using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentPerformanceTrendService
    {
        PercentageTotalBarChartModel Get(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel ByEthnicity(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel ByLunchStatus(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel BySpecialEducation(AssessmentTrendFilterModel model);
    }

    public class AssessmentPerformanceTrendService : IAssessmentPerformanceTrendService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;
        private readonly IRepository<PerformanceDimension> _performanceRepository;

        public AssessmentPerformanceTrendService(IRepository<AssessmentFact> assessmentRepository,
            IRepository<PerformanceDimension> performanceRepository)
        {
            _assessmentRepository = assessmentRepository;
            _performanceRepository = performanceRepository;
        }

        public PercentageTotalBarChartModel Get(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

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

        public PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.PerformanceKey, Property = x.Demographic.EnglishLanguageLearnerStatus })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Property = x.Key.Property,
                    PerformanceKey = x.Key.PerformanceKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var performance = _performanceRepository.GetAll().FirstOrDefault(x => x.PerformanceKey == model.PerformanceKey.Value);
            if (performance == null || !model.PerformanceKey.HasValue)
                throw new Exception("Performance Level does not Exist");

            var headers = new List<string> { "", "Language Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total), x.Sum(y => y.Total)),
                    Total = x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var property in properties)
            {
                var values = new List<PercentageTotalDataModel>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear && x.PerformanceKey == model.PerformanceKey.Value);
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
                Title = performance.PerformanceLevel + " Trend By English Language Learners",
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

        public PercentageTotalBarChartModel ByEthnicity(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.PerformanceKey, Property = x.Demographic.Ethnicity })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Property = x.Key.Property,
                    PerformanceKey = x.Key.PerformanceKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var performance = _performanceRepository.GetAll().FirstOrDefault(x => x.PerformanceKey == model.PerformanceKey.Value);
            if (performance == null || !model.PerformanceKey.HasValue)
                throw new Exception("Performance Level does not Exist");

            var headers = new List<string> { "", "Ethnicities" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total), x.Sum(y => y.Total)),
                    Total = x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var property in properties)
            {
                var values = new List<PercentageTotalDataModel>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear && x.PerformanceKey == model.PerformanceKey.Value);
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
                Title = performance.PerformanceLevel + " Trend By Ethnicity",
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

        public PercentageTotalBarChartModel ByLunchStatus(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.PerformanceKey, Property = x.Demographic.FreeReducedLunchStatus })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Property = x.Key.Property,
                    PerformanceKey = x.Key.PerformanceKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var performance = _performanceRepository.GetAll().FirstOrDefault(x => x.PerformanceKey == model.PerformanceKey.Value);
            if (performance == null || !model.PerformanceKey.HasValue)
                throw new Exception("Performance Level does not Exist");

            var headers = new List<string> { "", "Lunch Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total), x.Sum(y => y.Total)),
                    Total = x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var property in properties)
            {
                var values = new List<PercentageTotalDataModel>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear && x.PerformanceKey == model.PerformanceKey.Value);
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
                Title = performance.PerformanceLevel + " Trend By Free/Reduced Price Meals",
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

        public PercentageTotalBarChartModel BySpecialEducation(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.PerformanceKey, Property = x.Demographic.SpecialEducationStatus })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Property = x.Key.Property,
                    PerformanceKey = x.Key.PerformanceKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var performance = _performanceRepository.GetAll().FirstOrDefault(x => x.PerformanceKey == model.PerformanceKey.Value);
            if (performance == null || !model.PerformanceKey.HasValue)
                throw new Exception("Performance Level does not Exist");

            var headers = new List<string> { "", "Education Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total), x.Sum(y => y.Total)),
                    Total = x.Where(y => y.PerformanceKey == model.PerformanceKey.Value).Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var property in properties)
            {
                var values = new List<PercentageTotalDataModel>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear && x.PerformanceKey == model.PerformanceKey.Value);
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
                Title = performance.PerformanceLevel + " Trend By Special Education",
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

        private IQueryable<AssessmentFact> BaseQuery(AssessmentTrendFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .AsQueryable();

            if (model.SchoolYears != null && model.SchoolYears.Any())
                query = query.Where(x => model.SchoolYears.Contains(x.SchoolYearKey));

            if (model.Assessments != null && model.Assessments.Any())
                query = query.Where(x => model.Assessments.Contains(x.AssessmentKey));
            else
                query = query.Where(x => x.Assessment.AssessmentTitle == model.AssessmentTitle && x.Assessment.AcademicSubject == model.Subject);

            if (model.EnglishLanguageLearnerStatuses != null && model.EnglishLanguageLearnerStatuses.Any())
                query = query.Where(x => model.EnglishLanguageLearnerStatuses.Contains(x.Demographic.EnglishLanguageLearnerStatus));

            if (model.Ethnicities != null && model.Ethnicities.Any())
                query = query.Where(x => model.Ethnicities.Contains(x.Demographic.Ethnicity));

            if (model.GoodCauseExcemptions != null && model.GoodCauseExcemptions.Any())
                query = query.Where(x => model.GoodCauseExcemptions.Contains(x.GoodCauseExemptionKey));

            if (model.LunchStatuses != null && model.LunchStatuses.Any())
                query = query.Where(x => model.LunchStatuses.Contains(x.Demographic.FreeReducedLunchStatus));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            return query;
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double)subTotal / (double)total), 2);
        }
    }
}
