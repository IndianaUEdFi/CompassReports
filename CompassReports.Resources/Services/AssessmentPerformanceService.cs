using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentPerformanceService
    {
        PieChartModel<int> Get(AssessmentFilterModel model);
        PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentFilterModel model);
        PercentageTotalBarChartModel ByEthnicity(AssessmentFilterModel model);
        PercentageTotalBarChartModel ByLunchStatus(AssessmentFilterModel model);
        PercentageTotalBarChartModel BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentPerformanceService : IAssessmentPerformanceService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;
        private readonly IRepository<GoodCauseExemptionJunkDimension> _goodCauseExemptionRepository;

        public AssessmentPerformanceService(IRepository<AssessmentFact> assessmentRepository,
            IRepository<GoodCauseExemptionJunkDimension> goodCauseExemptionRepository)
        {
            _assessmentRepository = assessmentRepository;
            _goodCauseExemptionRepository = goodCauseExemptionRepository;
        }

        public PieChartModel<int> Get(AssessmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Performance.PerformanceLevel)
                .Select(x => new
                {
                    PerformanceLevel = x.Key,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).OrderBy(x => x.PerformanceLevel)
                .ToList();

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

        public PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var query = BaseQuery(model).ToList();

            var results = query.GroupBy(x => new { x.PerformanceKey, x.Performance.PerformanceLevel, Property = x.Demographic.EnglishLanguageLearnerStatus })
                .Select(x => new
                {
                    PerformanceLevel = x.Key.PerformanceLevel,
                    PerformanceKey = x.Key.PerformanceKey,
                    Property = x.Key.Property,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).ToList();

            var performanceLevels = results.Select(x => x.PerformanceLevel).Distinct().OrderBy(x => x).ToList();
            var performanceKeys = results.OrderBy(x => x.PerformanceLevel).Select(x => x.PerformanceKey).Distinct().ToList();

            var headers = new List<string> { "", "Language Statuses" };
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
                Title = "Performance Level by English Language Learners",
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

        public PercentageTotalBarChartModel ByEthnicity(AssessmentFilterModel model)
        {
            var query = BaseQuery(model).ToList();

            var results = query.GroupBy(x => new { x.PerformanceKey, x.Performance.PerformanceLevel, Property = x.Demographic.Ethnicity })
                .Select(x => new
                {
                    PerformanceLevel = x.Key.PerformanceLevel,
                    PerformanceKey = x.Key.PerformanceKey,
                    Property = x.Key.Property,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).ToList();

            var performanceLevels = results.Select(x => x.PerformanceLevel).Distinct().OrderBy(x => x).ToList();
            var performanceKeys = results.OrderBy(x => x.PerformanceLevel).Select(x => x.PerformanceKey).Distinct().ToList();

            var headers = new List<string> { "", "Ethnciites" };
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
                Title = "Performance Level by Ethnicity",
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

        public PercentageTotalBarChartModel ByLunchStatus(AssessmentFilterModel model)
        {
            var query = BaseQuery(model).ToList();

            var results = query.GroupBy(x => new { x.PerformanceKey, x.Performance.PerformanceLevel, Property = x.Demographic.FreeReducedLunchStatus })
                .Select(x => new
                {
                    PerformanceLevel = x.Key.PerformanceLevel,
                    PerformanceKey = x.Key.PerformanceKey,
                    Property = x.Key.Property,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).ToList();

            var performanceLevels = results.Select(x => x.PerformanceLevel).Distinct().OrderBy(x => x).ToList();
            var performanceKeys = results.OrderBy(x => x.PerformanceLevel).Select(x => x.PerformanceKey).Distinct().ToList();

            var headers = new List<string> { "", "Lunch Statuses" };
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
                Title = "Performance Level by Free/Reduced Price Meals",
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

        public PercentageTotalBarChartModel BySpecialEducation(AssessmentFilterModel model)
        {
            var query = BaseQuery(model).ToList();

            var results = query.GroupBy(x => new { x.PerformanceKey, x.Performance.PerformanceLevel, Property = x.Demographic.SpecialEducationStatus })
                .Select(x => new
                {
                    PerformanceLevel = x.Key.PerformanceLevel,
                    PerformanceKey = x.Key.PerformanceKey,
                    Property = x.Key.Property,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                }).ToList();

            var performanceLevels = results.Select(x => x.PerformanceLevel).Distinct().OrderBy(x => x).ToList();
            var performanceKeys = results.OrderBy(x => x.PerformanceLevel).Select(x => x.PerformanceKey).Distinct().ToList();

            var headers = new List<string> { "", "Education Statuses" };
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
                Title = "Performance Level by Special Education",
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

        public PercentageTotalBarChartModel ByGoodCauseExcemption(AssessmentFilterModel model)
        {
            var baseQuery = BaseQuery(model);

            if (baseQuery.All(x => x.GoodCauseExemptionKey == 3)) return null;

            var results = (
                from cause in
                _goodCauseExemptionRepository.GetAll().Where(x => (new[] {1, 2}).Contains(x.GoodCauseExemptionKey))
                join fact in baseQuery on cause.GoodCauseExemptionKey equals fact.GoodCauseExemptionKey into facts
                from fact in facts.DefaultIfEmpty()
                select new
                {
                    GoodCauseExemptionKey = cause.GoodCauseExemptionKey,
                    GoodCauseExemption = cause.GoodCauseExemption,
                    AssessmentStudentCount = fact == null ? 0 : fact.AssessmentStudentCount
                }
            ).GroupBy(x => new
            {
                GoodCauseExemptionKey = x.GoodCauseExemptionKey,
                GoodCauseExemption = x.GoodCauseExemption
            }).Select(x => new
            {
                GoodCauseExemption = x.Key.GoodCauseExemption,
                GoodCauseExemptionKey = x.Key.GoodCauseExemptionKey,
                Total = x.Sum(y => y.AssessmentStudentCount)
            }).ToList();

            var goodCause = results.Where(x => x.GoodCauseExemptionKey == 1).ToList();

            return new PercentageTotalBarChartModel
            {
                Title = "Good Cause Exemptions",
                Headers = new List<string> { "", "Good Cause Exemptions", "Total"},
                Labels = goodCause.Select(x => x.GoodCauseExemption).ToList(),
                Series = new List<string> { goodCause.First().GoodCauseExemption },
                Data = new List<List<PercentageTotalDataModel>> {
                    goodCause.Select( x => new PercentageTotalDataModel
                    {
                        Percentage = GetPercentage(x.Total, results.Sum(y => y.Total)),
                        Total = x.Total
                    }).ToList()
                },
                ShowChart = true,
                ShowPercentage = true,
                HideTotal = true
            };
        }

        private IQueryable<AssessmentFact> BaseQuery(AssessmentFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .Where(x => x.SchoolYearKey == model.SchoolYear);

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

            if (model.PerformanceLevels != null && model.PerformanceLevels.Any())
                query = query.Where(x => model.PerformanceLevels.Contains(x.PerformanceKey));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            return query;
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double) subTotal / (double) total), 2);
        }
    }
}
