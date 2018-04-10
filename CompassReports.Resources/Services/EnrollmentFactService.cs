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
    public interface IEnrollmentFactService
    {
        IQueryable<EnrollmentFact> BaseQuery(EnrollmentFilterModel model);

        Task<PieChartModel<int>> CreateChart(IQueryable<IGrouping<EnrollmentGroupByModel, EnrollmentFact>> groupings, string header, string title);

        Task<BarChartModel<int>> CreateTrendChart(IQueryable<IGrouping<EnrollmentTrendGroupByModel, EnrollmentFact>> groupings, string header, string title);
    }

    public class EnrollmentFactService : IEnrollmentFactService
    {
        private readonly IRepository<EnrollmentFact> _enrollmentRepository;

        public EnrollmentFactService(IRepository<EnrollmentFact> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public IQueryable<EnrollmentFact> BaseQuery(EnrollmentFilterModel model)
        {
            var query = _enrollmentRepository
                .GetAll()
                .AsQueryable();

            if(model.SchoolYear > 0)
                query = query.Where(x => x.SchoolYearKey == model.SchoolYear);

            if (model.SchoolYears != null && model.SchoolYears.Any())
                query = query.Where(x => model.SchoolYears.Contains(x.SchoolYearKey));

            if (model.Schools != null && model.Schools.Any())
                query = query.Where(x => model.Schools.Contains(x.SchoolKey));

            if (model.Districts != null && model.Districts.Any())
                query = query.Where(x => model.Districts.Contains(x.School.LocalEducationAgencyKey));

            if (model.EnglishLanguageLearnerStatuses != null && model.EnglishLanguageLearnerStatuses.Any())
                query = query.Where(x => model.EnglishLanguageLearnerStatuses.Contains(x.Demographic.EnglishLanguageLearnerStatus));

            if (model.Ethnicities != null && model.Ethnicities.Any())
                query = query.Where(x => model.Ethnicities.Contains(x.Demographic.Ethnicity));

            if (model.Grades != null && model.Grades.Any())
                query = query.Where(x => model.Grades.Contains(x.Demographic.GradeLevel));

            if (model.LunchStatuses != null && model.LunchStatuses.Any())
                query = query.Where(x => model.LunchStatuses.Contains(x.Demographic.FreeReducedLunchStatus));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            return query;
        }

        private double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double)subTotal / (double)total), 2);
        }

        public async Task<PieChartModel<int>> CreateChart(IQueryable<IGrouping<EnrollmentGroupByModel, EnrollmentFact>> groupings, string header, string title)
        {
            var results = await groupings
                .Select(x => new
                {
                    x.Key.Property,
                    x.Key.SortOrder,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                })
                .OrderBy(x => x.SortOrder)
                .ToListAsync();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = title,
                Headers = new List<string> { "", header, "Enrollment Count" },
                PercentageHeaders = new List<string> { "", header, "Enrollment Percentage" },
                Labels = results.Select(x => x.Property).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                TotalRowTitle = "Enrollment Total",
                Total = total
            };
        }

        public async Task<BarChartModel<int>> CreateTrendChart(IQueryable<IGrouping<EnrollmentTrendGroupByModel, EnrollmentFact>> groupings, string header, string title)
        {
            var results = groupings
                .Select(x => new
                {
                    x.Key.SchoolYear,
                    x.Key.SchoolYearDescription,
                    x.Key.Property,
                    x.Key.SortOrder,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                })
                .OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", header };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.OrderBy(x => x.SortOrder).Select(x => x.Property).Distinct().ToList();

            var data = new List<List<int>>();
            foreach (var property in properties)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var total = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear);
                    values.Add(total == null ? 0 : total.Total);
                }
                data.Add(values);
            }

            return new BarChartModel<int>
            {
                Title = title,
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = properties,
                Data = data,
                ShowChart = true,
                Totals = results.GroupBy(x => x.SchoolYear).OrderBy(x => x.Key).Select(x => x.Sum(y => y.Total)).ToList()
            };
        }
    }
}
