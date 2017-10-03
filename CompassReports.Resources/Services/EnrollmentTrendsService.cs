using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IEnrollmentTrendsService
    {
        BarChartModel<int> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        BarChartModel<int> ByEthnicity(EnrollmentFilterModel model);
        BarChartModel<int> ByGrade(EnrollmentFilterModel model);
        BarChartModel<int> ByLunchStatus(EnrollmentFilterModel model);
        BarChartModel<int> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class EnrollmentTrendsService : IEnrollmentTrendsService
    {
        private readonly IRepository<EnrollmentFact> _enrollmentRepository;

        public EnrollmentTrendsService(IRepository<EnrollmentFact> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public BarChartModel<int> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.EnglishLanguageLearnerStatus })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    EnglishLanguageLearnerStatus = x.Key.EnglishLanguageLearnerStatus,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", "English Language Learner Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var englishLanguageLearnerStatuses = results.Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<int>>();
            foreach (var englishLanguageLearnerStatus in englishLanguageLearnerStatuses)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var total = results.FirstOrDefault(x => x.EnglishLanguageLearnerStatus == englishLanguageLearnerStatus && x.SchoolYear == schoolYear);
                    values.Add(total == null ? 0 : total.Total);
                }
                data.Add(values);
            }

            return new BarChartModel<int>
            {
                Title = "English Language Learner",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = englishLanguageLearnerStatuses,
                Data = data,
                ShowChart = true,
                Totals = results.GroupBy(x => x.SchoolYear).OrderBy(x => x.Key).Select(x => x.Sum(y => y.Total)).ToList()
            };
        }

        public BarChartModel<int> ByEthnicity(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.Ethnicity })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Ethnicity = x.Key.Ethnicity,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", "Ethnicitys" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var ethnicities = results.Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<int>>();
            foreach (var ethnicity in ethnicities)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var total = results.FirstOrDefault(x => x.Ethnicity == ethnicity && x.SchoolYear == schoolYear);
                    values.Add(total == null ? 0 : total.Total);
                }
                data.Add(values);
            }

            return new BarChartModel<int>
            {
                Title = "Ethnicity",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = ethnicities,
                Data = data,
                ShowChart = true,
                Totals = results.GroupBy(x => x.SchoolYear).OrderBy(x => x.Key).Select(x => x.Sum(y => y.Total)).ToList()
            };
        }
        public BarChartModel<int> ByGrade(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new {x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.GradeLevel})
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    GradeLevel = x.Key.GradeLevel,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> {"", "Grades"};
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var gradeLevels = results.Select(x => x.GradeLevel).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<int>>();
            foreach (var grade in gradeLevels)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var total = results.FirstOrDefault(x => x.GradeLevel == grade && x.SchoolYear == schoolYear);
                    values.Add(total == null ? 0 : total.Total);
                }
                data.Add(values);
            }

            return new BarChartModel<int>
            {
                Title = "Grade",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = gradeLevels,
                Data = data,
                ShowChart = false,
                Totals = results.GroupBy(x => x.SchoolYear).OrderBy(x => x.Key).Select(x => x.Sum(y => y.Total)).ToList()
            };
        }

        public BarChartModel<int> ByLunchStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.FreeReducedLunchStatus })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    FreeReducedLunchStatus = x.Key.FreeReducedLunchStatus,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", "Lunch Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var freeReducedLunchStatuses = results.Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<int>>();
            foreach (var freeReducedLunchStatus in freeReducedLunchStatuses)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var total = results.FirstOrDefault(x => x.FreeReducedLunchStatus == freeReducedLunchStatus && x.SchoolYear == schoolYear);
                    values.Add(total == null ? 0 : total.Total);
                }
                data.Add(values);
            }

            return new BarChartModel<int>
            {
                Title = "Free/Reduced Price Meals",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = freeReducedLunchStatuses,
                Data = data,
                ShowChart = true,
                Totals = results.GroupBy(x => x.SchoolYear).OrderBy(x => x.Key).Select(x => x.Sum(y => y.Total)).ToList()
            };
        }

        public BarChartModel<int> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Demographic.SpecialEducationStatus })
                .Select(x => new
                {
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    SpecialEducationStatus = x.Key.SpecialEducationStatus,
                    Total = x.Sum(y => y.EnrollmentStudentCount)
                }).OrderBy(x => x.SchoolYear)
                .ToList();

            var headers = new List<string> { "", "Special Education Statuses" };
            headers.AddRange(results.Select(x => x.SchoolYearDescription).Distinct());

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().ToList();
            var specialEducationStatuses = results.Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();

            var data = new List<List<int>>();
            foreach (var specialEducationStatus in specialEducationStatuses)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var total = results.FirstOrDefault(x => x.SpecialEducationStatus == specialEducationStatus && x.SchoolYear == schoolYear);
                    values.Add(total == null ? 0 : total.Total);
                }
                data.Add(values);
            }

            return new BarChartModel<int>
            {
                Title = "Special Education",
                Headers = headers,
                Labels = results.Select(x => x.SchoolYearDescription).Distinct().ToList(),
                Series = specialEducationStatuses,
                Data = data,
                ShowChart = true,
                Totals = results.GroupBy(x => x.SchoolYear).OrderBy(x => x.Key).Select(x => x.Sum(y => y.Total)).ToList()
            };
        }

        private IQueryable<EnrollmentFact> BaseQuery(EnrollmentFilterModel model)
        {
            var query = _enrollmentRepository
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
