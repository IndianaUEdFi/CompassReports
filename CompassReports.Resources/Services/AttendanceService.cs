using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAttendanceService
    {
        BarChartModel<double> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        BarChartModel<double> ByEthnicity(EnrollmentFilterModel model);
        BarChartModel<double> ByGrade(EnrollmentFilterModel model);
        BarChartModel<double> ByLunchStatus(EnrollmentFilterModel model);
        BarChartModel<double> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IRepository<AttendanceFact> _attendanceRepository;

        public AttendanceService(IRepository<AttendanceFact> attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public BarChartModel<double> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.EnglishLanguageLearnerStatus)
                    .Select(x => new
                    {
                        EnglishLanguageLearnerStatus = x.Key,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.EnglishLanguageLearnerStatus)
                    .ToList();

            return new BarChartModel<double>()
            {
                Data = new List<List<double>> { results.Select(x => Math.Round(100 * (((double)x.TotalInstructionalDays - (double)x.TotalAbsences) / (double)x.TotalInstructionalDays), 2)).ToList() },
                Headers = new List<string> { "", "EnglishLanguageLearnerStatuses", "Attendance Rate" },
                HideTotal = true,
                Labels = results.Select(x => x.EnglishLanguageLearnerStatus).ToList(),
                Percentage = true,
                Series = new List<string> { "EnglishLanguageLearnerStatuses" },
                ShowChart = true,
                SingleSeries = true,
                Title = "English Language Learner"
            };
        }

        public BarChartModel<double> ByEthnicity(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.Ethnicity )
                    .Select(x => new
                    {
                        Ethnicity = x.Key,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.Ethnicity)
                    .ToList();

            return new BarChartModel<double>()
            {
                Title = "Ethnicity",
                Headers = new List<string> { "", "Ethncities", "Attendance Rate" },
                Labels = results.Select(x => x.Ethnicity).ToList(),
                Series = new List<string> { "Ethnicities" },
                Data = new List<List<double>> { results.Select(x => Math.Round(100 * (((double)x.TotalInstructionalDays - (double)x.TotalAbsences) / (double)x.TotalInstructionalDays), 2)).ToList() },
                SingleSeries = true,
                ShowChart = true,
                HideTotal = true,
                Percentage = true,
            };
        }

        public BarChartModel<double> ByGrade(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Demographic.GradeLevel, x.Demographic.GradeLevelSort})
                    .Select(x => new
                    {
                        GradeLevel = x.Key.GradeLevel,
                        GradeLevelSort = x.Key.GradeLevelSort,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.GradeLevelSort)
                    .ToList();

            return new BarChartModel<double>()
            {
                Title = "Grade",
                Headers = new List<string> {"", "Grades", "Attendance Rate"},
                Labels = results.Select(x => x.GradeLevel).ToList(),
                Series = new List<string> { "Grades" },
                Data = new List<List<double>> { results.Select(x => Math.Round(100 * (((double)x.TotalInstructionalDays - (double)x.TotalAbsences)/(double)x.TotalInstructionalDays), 2)).ToList() },
                SingleSeries = true,
                ShowChart = false,
                HideTotal = true,
                Percentage = true
            };
        }

        public BarChartModel<double> ByLunchStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.FreeReducedLunchStatus)
                    .Select(x => new
                    {
                        FreeReducedLunchStatus = x.Key,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.FreeReducedLunchStatus)
                    .ToList();

            return new BarChartModel<double>()
            {
                Title = "Free/Reduced Price Meals",
                Headers = new List<string> { "", "Lunch Statuses", "Attendance Rate" },
                Labels = results.Select(x => x.FreeReducedLunchStatus).ToList(),
                Series = new List<string> { "Lunch Statuses" },
                Data = new List<List<double>> { results.Select(x => Math.Round(100 * (((double)x.TotalInstructionalDays - (double)x.TotalAbsences) / (double)x.TotalInstructionalDays), 2)).ToList() },
                SingleSeries = true,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        public BarChartModel<double> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.SpecialEducationStatus)
                    .Select(x => new
                    {
                        SpecialEducationStatus = x.Key,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.SpecialEducationStatus)
                    .ToList();

            return new BarChartModel<double>()
            {
                Title = "Special Education",
                Headers = new List<string> { "", "Special Education Status", "Attendance Rate" },
                Labels = results.Select(x => x.SpecialEducationStatus).ToList(),
                Series = new List<string> { "Special Education Statuses" },
                Data = new List<List<double>> { results.Select(x => Math.Round(100 * (((double)x.TotalInstructionalDays - (double)x.TotalAbsences) / (double)x.TotalInstructionalDays), 2)).ToList() },
                SingleSeries = true,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        private IQueryable<AttendanceFact> BaseQuery(EnrollmentFilterModel model)
        {
            var query = _attendanceRepository
                .GetAll()
                .Where(x => x.SchoolYearKey == model.SchoolYear);

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

    }
}
