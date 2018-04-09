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
    public interface IGraduationFactService
    {
        IQueryable<GraduationFact> BaseQuery(GraduateFilterModel model);

        Task<PieChartModel<int>> CreateBaseChart(IQueryable<IGrouping<string, GraduationFact>> groupings, string type);

        Task<PercentageTotalBarChartModel> CreateChart(IQueryable<IGrouping<GraduateGroupByModel, GraduationFact>> groupings, string type, string header, string title);

        Task<PercentageTotalBarChartModel> CreateTrendChart(IQueryable<IGrouping<GraduateTrendGroupByModel, GraduationFact>> groupings, GraduateFilterModel model, string type);
    }

    public class GraduationFactService : IGraduationFactService
    {
        private readonly IRepository<GraduationFact> _graduationFactRepository;

        public GraduationFactService(IRepository<GraduationFact> graduationFactRepository)
        {
            _graduationFactRepository = graduationFactRepository;
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double)subTotal / (double)total), 2);
        }

        public IQueryable<GraduationFact> BaseQuery(GraduateFilterModel model)
        {
            var query = _graduationFactRepository
                .GetAll()
                .AsQueryable();

            if (model.CohortYear != 0)
                query = query.Where(x => x.SchoolYearKey == model.CohortYear);

            if (model.ExpectedGraduationYear != 0)
                query = query.Where(x => x.Demographic.ExpectedGraduationYear == model.ExpectedGraduationYear);

            if (model.EnglishLanguageLearnerStatuses != null && model.EnglishLanguageLearnerStatuses.Any())
                query = query.Where(x => model.EnglishLanguageLearnerStatuses.Contains(x.Demographic.EnglishLanguageLearnerStatus));

            if (model.Ethnicities != null && model.Ethnicities.Any())
                query = query.Where(x => model.Ethnicities.Contains(x.Demographic.Ethnicity));

            if (model.LunchStatuses != null && model.LunchStatuses.Any())
                query = query.Where(x => model.LunchStatuses.Contains(x.Demographic.FreeReducedLunchStatus));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            if (model.ExpectedGraduationYears.Any())
                query = query.Where(x => model.ExpectedGraduationYears.Contains(x.Demographic.ExpectedGraduationYear));

            return query;
        }

        public async Task<PieChartModel<int>> CreateBaseChart(IQueryable<IGrouping<string, GraduationFact>> groupings, string type)
        {
            var results = await groupings
                .Select(x => new
                {
                    GraduationGroupBy = x.Key,
                    Total = x.Sum(y => y.GraduationStudentCount)
                }).OrderBy(x => x.GraduationGroupBy)
                .ToListAsync();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = type,
                TotalRowTitle = type + " Total",
                Headers = new List<string> { "", type, "Status Count" },
                PercentageHeaders = new List<string> { "", type, "Status Percentage" },
                Labels = results.Select(x => x.GraduationGroupBy).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                Total = total
            };
        }

        public async Task<PercentageTotalBarChartModel> CreateChart(IQueryable<IGrouping<GraduateGroupByModel, GraduationFact>> groupings, string type, string header, string title)
        {
            var results = groupings
                .Select(x => new
                {
                    x.Key.ChartGroupBy,
                    x.Key.Property,
                    Total = x.Sum(y => y.GraduationStudentCount)
                }).ToList();

            var labels = results.Select(x => x.ChartGroupBy).Distinct().OrderBy(x => x).ToList();

            var headers = new List<string> { "", header };
            headers.AddRange(labels);

            var propertyValues = results.Select(x => x.Property).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var value in propertyValues)
            {
                var values = new List<PercentageTotalDataModel>();
                var propertyTotal = results.Where(x => x.Property == value).Sum(x => x.Total);
                var properties = results.Where(x => x.Property == value).ToList();

                foreach (var label in labels)
                {
                    var row = properties.FirstOrDefault(x => x.ChartGroupBy == label);
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
            var totals = results.GroupBy(x => x.ChartGroupBy)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Sum(y => y.Total), total),
                    Total = x.Sum(y => y.Total)
                }).ToList();

            return new PercentageTotalBarChartModel
            {
                Title = type + " by " + title,
                Headers = headers,
                Labels = labels,
                Series = propertyValues.Select(x => x.ToString()).ToList(),
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = type,
                Totals = totals
            };
        }

        public async Task<PercentageTotalBarChartModel> CreateTrendChart(IQueryable<IGrouping<GraduateTrendGroupByModel, GraduationFact>> groupings, GraduateFilterModel model, string type)
        {

            var results = await groupings
                .Select(x => new
                {
                    x.Key.ChartGroupBy,
                    x.Key.SchoolYear,
                    x.Key.ExpectedGraduationYear,
                    x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.GraduationStudentCount)
                })
                .Where(x => x.SchoolYear - x.ExpectedGraduationYear == model.GradCohortYearDifference)
                .OrderBy(x => x.SchoolYear)
                .ToListAsync();

            var headers = new List<string> { "", type };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var series = results
                .Select(x => x.ChartGroupBy).Distinct().OrderBy(x => x).ToList();

            var total = results.Sum(x => x.Total);
            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Sum(y => y.Total), total),
                    Total = x.Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var label in series)
            {
                var values = new List<PercentageTotalDataModel>();

                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.ChartGroupBy == label && x.SchoolYear == schoolYear);
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

            return new PercentageTotalBarChartModel
            {
                Title = type + " Trend",
                Headers = headers,
                HidePercentageTotal = true,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = series,
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = type,
                Totals = totals
            };
        }
    }
}
