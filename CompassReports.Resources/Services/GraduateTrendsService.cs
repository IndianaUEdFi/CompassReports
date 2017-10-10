using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IGraduateTrendsService
    {
        PercentageTotalBarChartModel ByStatus(GraduateFilterModel model);
        PercentageTotalBarChartModel ByWaiver(GraduateFilterModel model);
    }

    public class GraduateTrendsService : IGraduateTrendsService
    {
        private readonly IRepository<GraduationFact> _graduationFactRepository;

        public GraduateTrendsService(IRepository<GraduationFact> graduationFactRepository)
        {
            _graduationFactRepository = graduationFactRepository;
        }

        public PercentageTotalBarChartModel ByStatus(GraduateFilterModel model)
        {
            var query = BaseQuery(model);

            var results =
                query.GroupBy(
                        x =>
                            new
                            {
                                x.Demographic.ExpectedGraduationYear,
                                x.SchoolYearKey,
                                x.SchoolYearDimension.SchoolYearDescription,
                                x.GraduationStatus.GraduationStatus
                            })
                    .Select(x => new
                    {
                        GraduationStatus = x.Key.GraduationStatus,
                        SchoolYear = x.Key.SchoolYearKey,
                        ExpectedGraduationYear = x.Key.ExpectedGraduationYear,
                        SchoolYearDescription = x.Key.SchoolYearDescription,
                        Total = x.Sum(y => y.GraduationStudentCount)
                    })
                    .ToList()
                    .Where(x => x.SchoolYear - short.Parse(x.ExpectedGraduationYear) == model.GradCohortYearDifference)
                    .OrderBy(x => x.SchoolYear)
                    .ToList();

            var headers = new List<string> {"", "Graduation Status"};
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var graduationStatuses = results
                .Select(x => x.GraduationStatus).Distinct().OrderBy(x => x).ToList();

            var total = results.Sum(x => x.Total);
            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Sum(y => y.Total), total),
                    Total = x.Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var graduationStatus in graduationStatuses)
            {
                var values = new List<PercentageTotalDataModel>();

                foreach (var schoolYear in schoolYears)
                {
                    var row =
                        results.FirstOrDefault(x => x.GraduationStatus == graduationStatus && x.SchoolYear == schoolYear);
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
                Title = "Graduation Status Trend",
                Headers = headers,
                HidePercentageTotal = true,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = graduationStatuses,
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = "Graduate Status ",
                Totals = totals
            };

        }

        public PercentageTotalBarChartModel ByWaiver(GraduateFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => new { x.Demographic.ExpectedGraduationYear, x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.GraduationStatus.GraduationWaiver })
                .Select(x => new
                {
                    GraduationWaiver = x.Key.GraduationWaiver,
                    SchoolYear = x.Key.SchoolYearKey,
                    ExpectedGraduationYear = x.Key.ExpectedGraduationYear,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.GraduationStudentCount)
                })
                .ToList()
                .Where(x => x.SchoolYear - short.Parse(x.ExpectedGraduationYear) == model.GradCohortYearDifference)
                .OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", "Graduation Status" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var graduationWaivers = results
                .Select(x => x.GraduationWaiver).Distinct().OrderBy(x => x).ToList();

            var total = results.Sum(x => x.Total);
            var totals = results.GroupBy(x => x.SchoolYear)
                .OrderBy(x => x.Key)
                .Select(x => new PercentageTotalDataModel
                {
                    Percentage = GetPercentage(x.Sum(y => y.Total), total),
                    Total = x.Sum(y => y.Total)
                }).ToList();

            var data = new List<List<PercentageTotalDataModel>>();
            foreach (var graduationWavier in graduationWaivers)
            {
                var values = new List<PercentageTotalDataModel>();

                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.GraduationWaiver == graduationWavier && x.SchoolYear == schoolYear);
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
                Title = "Graduation Waiver Trend",
                Headers = headers,
                HidePercentageTotal = true,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = graduationWaivers,
                Data = data,
                ShowChart = true,
                ShowPercentage = true,
                TotalRowTitle = "Graduate Wavier ",
                Totals = totals
            };
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double)subTotal / (double)total), 2);
        }

        private IQueryable<GraduationFact> BaseQuery(GraduateFilterModel model)
        {
            var query = _graduationFactRepository
                .GetAll()
                .AsQueryable();

            if (model.EnglishLanguageLearnerStatuses != null && model.EnglishLanguageLearnerStatuses.Any())
                query = query.Where(x => model.EnglishLanguageLearnerStatuses.Contains(x.Demographic.EnglishLanguageLearnerStatus));

            if (model.Ethnicities != null && model.Ethnicities.Any())
                query = query.Where(x => model.Ethnicities.Contains(x.Demographic.Ethnicity));

            if (model.LunchStatuses != null && model.LunchStatuses.Any())
                query = query.Where(x => model.LunchStatuses.Contains(x.Demographic.FreeReducedLunchStatus));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            return query;
        }

    }
}
