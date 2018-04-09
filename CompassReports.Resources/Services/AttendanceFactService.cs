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
    public interface IAttendanceFactService
    {
        IQueryable<AttendanceFact> BaseQuery(EnrollmentFilterModel model);

        Task<BarChartModel<double>> CreateChart(IQueryable<IGrouping<EnrollmentGroupByModel, AttendanceFact>> groupings, string header, string series, string title);

        Task<LineChartModel<double>> CreateTrendChart(IQueryable<IGrouping<EnrollmentTrendGroupByModel, AttendanceFact>> groupings, string header, string title);
    }

    public class AttendanceFactService : IAttendanceFactService
    {
        private readonly IRepository<AttendanceFact> _attendanceRepository;

        public AttendanceFactService(IRepository<AttendanceFact> attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public IQueryable<AttendanceFact> BaseQuery(EnrollmentFilterModel model)
        {
            var query = _attendanceRepository
                .GetAll()
                .AsQueryable();

            if(model.SchoolYear > 0)
                query = query.Where(x => x.SchoolYearKey == model.SchoolYear);

            if (model.SchoolYears != null && model.SchoolYears.Any())
                query = query.Where(x => model.SchoolYears.Contains(x.SchoolYearKey));

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

        public async Task<BarChartModel<double>> CreateChart(IQueryable<IGrouping<EnrollmentGroupByModel, AttendanceFact>> groupings, string header, string series, string title)
        {
            var results = await groupings
                    .Select(x => new
                    {
                        x.Key.Property,
                        x.Key.SortOrder,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    })
                    .OrderBy(x => x.SortOrder)
                    .ToListAsync();

            return new BarChartModel<double>
            {
                Title = title,
                Headers = new List<string> { "", header, "Attendance Rate" },
                Labels = results.Select(x => x.Property).ToList(),
                Series = new List<string> { series },
                Data = new List<List<double>> { results.Select(x => Math.Round(100 * (((double)x.TotalInstructionalDays - (double)x.TotalAbsences) / (double)x.TotalInstructionalDays), 2)).ToList() },
                SingleSeries = true,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        public async Task<LineChartModel<double>> CreateTrendChart(IQueryable<IGrouping<EnrollmentTrendGroupByModel, AttendanceFact>> groupings, string header, string title)
        {
            var results = await groupings
                .Select(x => new
                {
                    x.Key.SchoolYear,
                    x.Key.SchoolYearDescription,
                    x.Key.Property,
                    x.Key.SortOrder,
                    TotalAbsences = x.Sum(y => y.TotalAbsences),
                    TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                })
                .OrderBy(x => x.SchoolYear)
                .ToListAsync();

            var headers = new List<string> { "", header };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var properties = results.OrderBy(x => x.SortOrder).Select(x => x.Property).Distinct().ToList();

            var data = new List<List<double>>();
            foreach (var property in properties)
            {
                var rates = new List<double>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Property == property && x.SchoolYear == schoolYear);
                    var rate = Math.Round(100 * (((double)row.TotalInstructionalDays - (double)row.TotalAbsences) / (double)row.TotalInstructionalDays), 2);
                    rates.Add(rate);
                }
                data.Add(rates);
            }

            return new LineChartModel<double>()
            {
                Title = title,
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = properties,
                Data = data,
                ShowChart = false,
                HideTotal = true,
                Percentage = true
            };
        }
    }
}
