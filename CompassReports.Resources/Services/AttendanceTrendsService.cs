using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAttendanceTrendsService
    {
        LineChartModel<double> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        LineChartModel<double> ByEthnicity(EnrollmentFilterModel model);
        LineChartModel<double> ByGrade(EnrollmentFilterModel model);
        LineChartModel<double> ByLunchStatus(EnrollmentFilterModel model);
        LineChartModel<double> BySpecialEducationStatus(EnrollmentFilterModel model);
    }
    class AttendanceTrendsService : IAttendanceTrendsService
    {
        private readonly IRepository<AttendanceFact> _attendanceRepository;

        public AttendanceTrendsService(IRepository<AttendanceFact> attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public LineChartModel<double> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.EnglishLanguageLearnerStatus })
                    .Select(x => new
                    {
                        SchoolYear = x.Key.SchoolYearKey,
                        SchoolYearDescription = x.Key.SchoolYearDescription,
                        EnglishLanguageLearnerStatus = x.Key.EnglishLanguageLearnerStatus,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.SchoolYear)
                    .ToList();

            var headers = new List<string> { "", "English Language Learner Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var englishLanguageLearnerStatuses = results.Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<double>>();
            foreach (var englishLanguageLearnerStatus in englishLanguageLearnerStatuses)
            {
                var rates = new List<double>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.EnglishLanguageLearnerStatus == englishLanguageLearnerStatus && x.SchoolYear == schoolYear);
                    var rate = Math.Round(100 * (((double)row.TotalInstructionalDays - (double)row.TotalAbsences) / (double)row.TotalInstructionalDays), 2);
                    rates.Add(rate);
                }
                data.Add(rates);
            }

            return new LineChartModel<double>()
            {
                Title = "English Language Learner",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = englishLanguageLearnerStatuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        public LineChartModel<double> ByEthnicity(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.Ethnicity })
                    .Select(x => new
                    {
                        SchoolYear = x.Key.SchoolYearKey,
                        SchoolYearDescription = x.Key.SchoolYearDescription,
                        Ethnicity = x.Key.Ethnicity,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.SchoolYear)
                    .ToList();

            var headers = new List<string> { "", "Ethncities" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var ethnicities = results.Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<double>>();
            foreach (var ethnicity in ethnicities)
            {
                var rates = new List<double>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.Ethnicity == ethnicity && x.SchoolYear == schoolYear);
                    var rate = Math.Round(100 * (((double)row.TotalInstructionalDays - (double)row.TotalAbsences) / (double)row.TotalInstructionalDays), 2);
                    rates.Add(rate);
                }
                data.Add(rates);
            }

            return new LineChartModel<double>()
            {
                Title = "Ethnicity",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = ethnicities,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        public LineChartModel<double> ByGrade(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.GradeLevel, x.Demographic.GradeLevelSort })
                    .Select(x => new
                    {
                        SchoolYear = x.Key.SchoolYearKey,
                        SchoolYearDescription = x.Key.SchoolYearDescription,
                        GradeLevel = x.Key.GradeLevel,
                        GradeLevelSort = x.Key.GradeLevelSort,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.SchoolYear)
                    .ToList();

            var headers = new List<string> { "", "Grades" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var gradeLevels = results.OrderBy(x => x.GradeLevelSort).Select(x => x.GradeLevel).Distinct().ToList();

            var data = new List<List<double>>();
            foreach (var grade in gradeLevels)
            {
                var rates = new List<double>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.GradeLevel == grade && x.SchoolYear == schoolYear);
                    var rate = Math.Round(100 * (((double)row.TotalInstructionalDays - (double)row.TotalAbsences) / (double)row.TotalInstructionalDays), 2);
                    rates.Add(rate);
                }
                data.Add(rates);
            }

            return new LineChartModel<double>()
            {
                Title = "Grade",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = gradeLevels,
                Data = data,
                ShowChart = false,
                HideTotal = true,
                Percentage = true
            };
        }

        public LineChartModel<double> ByLunchStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.FreeReducedLunchStatus })
                    .Select(x => new
                    {
                        SchoolYear = x.Key.SchoolYearKey,
                        SchoolYearDescription = x.Key.SchoolYearDescription,
                        FreeReducedLunchStatus = x.Key.FreeReducedLunchStatus,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.SchoolYear)
                    .ToList();

            var headers = new List<string> { "", "Lunch Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var freeReducedLunchStatuses = results.Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<double>>();
            foreach (var freeReducedLunchStatus in freeReducedLunchStatuses)
            {
                var rates = new List<double>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.FreeReducedLunchStatus == freeReducedLunchStatus && x.SchoolYear == schoolYear);
                    var rate = Math.Round(100 * (((double)row.TotalInstructionalDays - (double)row.TotalAbsences) / (double)row.TotalInstructionalDays), 2);
                    rates.Add(rate);
                }
                data.Add(rates);
            }

            return new LineChartModel<double>()
            {
                Title = "Free/Reduced Price Meals",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = freeReducedLunchStatuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        public LineChartModel<double> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.SpecialEducationStatus })
                    .Select(x => new
                    {
                        SchoolYear = x.Key.SchoolYearKey,
                        SchoolYearDescription = x.Key.SchoolYearDescription,
                        SpecialEducationStatus = x.Key.SpecialEducationStatus,
                        TotalAbsences = x.Sum(y => y.TotalAbsences),
                        TotalInstructionalDays = x.Sum(y => y.TotalInstructionalDays)
                    }).OrderBy(x => x.SchoolYear)
                    .ToList();

            var headers = new List<string> { "", "Special Education Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var specialEducationStatuses = results.Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<double>>();
            foreach (var specialEducationStatus in specialEducationStatuses)
            {
                var rates = new List<double>();
                foreach (var schoolYear in schoolYears)
                {
                    var row = results.FirstOrDefault(x => x.SpecialEducationStatus == specialEducationStatus && x.SchoolYear == schoolYear);
                    var rate = Math.Round(100 * (((double)row.TotalInstructionalDays - (double)row.TotalAbsences) / (double)row.TotalInstructionalDays), 2);
                    rates.Add(rate);
                }
                data.Add(rates);
            }

            return new LineChartModel<double>()
            {
                Title = "Special Education",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = specialEducationStatuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                Percentage = true
            };
        }

        private IQueryable<AttendanceFact> BaseQuery(EnrollmentFilterModel model)
        {
            var query = _attendanceRepository
                .GetAll()
                .AsQueryable();

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
    }
}
