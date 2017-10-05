using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IEnrollmentService
    {
        PieChartModel<int> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        PieChartModel<int> ByEthnicity(EnrollmentFilterModel model);
        PieChartModel<int> ByGrade(EnrollmentFilterModel model);
        PieChartModel<int> ByLunchStatus(EnrollmentFilterModel model);
        PieChartModel<int> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly IRepository<EnrollmentFact> _enrollmentRepository;

        public EnrollmentService(IRepository<EnrollmentFact> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public PieChartModel<int> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.EnglishLanguageLearnerStatus)
                    .Select(x => new
                    {
                        EnglishLanguageLearnerStatus = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.EnglishLanguageLearnerStatus)
                    .ToList();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = "English Language Learner",
                Headers = new List<string> { "", "English Language Learner Status", "Enrollment Count" },
                PercentageHeaders = new List<string> { "", "English Language Learner Status", "Enrollment Percentage" },
                Labels = results.Select(x => x.EnglishLanguageLearnerStatus).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                TotalRowTitle = "Enrollment Total",
                Total = total
            };
        }

        public PieChartModel<int> ByEthnicity(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.Ethnicity)
                    .Select(x => new
                    {
                        Ethnicity = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.Ethnicity)
                    .ToList();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = "Ethnicity",
                Headers = new List<string> { "", "Ethnicity", "Enrollment Count" },
                PercentageHeaders = new List<string> { "", "Ethnicity", "Enrollment Percentage" },
                Labels = results.Select(x => x.Ethnicity).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                TotalRowTitle = "Enrollment Total",
                Total = total
            };
        }
        public PieChartModel<int> ByGrade(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Demographic.GradeLevel, x.Demographic.GradeLevelSort})
                    .Select(x => new
                    {
                        GradeLevel = x.Key.GradeLevel,
                        GradeLevelSort = x.Key.GradeLevelSort,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.GradeLevelSort)
                    .ToList();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = "Grade",
                Headers = new List<string> {"", "Grades", "Enrollment Count"},
                PercentageHeaders = new List<string> { "", "Grades", "Enrollment Percentage" },
                Labels = results.Select(x => x.GradeLevel).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = false,
                TotalRowTitle = "Enrollment Total",
                Total = total
            };
        }

        public PieChartModel<int> ByLunchStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.FreeReducedLunchStatus)
                    .Select(x => new
                    {
                        LunchStatus = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.LunchStatus)
                    .ToList();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = "Free/Reduced Price Meals",
                Headers = new List<string> { "", "Lunch Status", "Enrollment Count" },
                PercentageHeaders = new List<string> { "", "Lunch Status", "Enrollment Percentage" },
                Labels = results.Select(x => x.LunchStatus).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                TotalRowTitle = "Enrollment Total",
                Total = total
            };
        }

        public PieChartModel<int> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.SpecialEducationStatus)
                    .Select(x => new
                    {
                        SpecialEducationStatus = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.SpecialEducationStatus)
                    .ToList();

            var total = results.Sum(x => x.Total);

            return new PieChartModel<int>
            {
                Title = "Special Education",
                Headers = new List<string> { "", "Special Education Status", "Enrollment Count" },
                PercentageHeaders = new List<string> { "", "Special Education Status", "Enrollment Percentage" },
                Labels = results.Select(x => x.SpecialEducationStatus).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                Percentages = results.Select(x => GetPercentage(x.Total, total)).ToList(),
                ShowChart = true,
                TotalRowTitle = "Enrollment Total",
                Total = total
            };
        }

        private IQueryable<EnrollmentFact> BaseQuery(EnrollmentFilterModel model)
        {
            var query = _enrollmentRepository
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

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double)subTotal / (double)total), 2);
        }

    }
}
