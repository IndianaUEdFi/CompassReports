using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Context;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;
using CompassReports.Resources.Models.Enrollment;

namespace CompassReports.Resources.Services
{
    public interface IEnrollmentService
    {
        EnrollmentChartModel<int> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        EnrollmentChartModel<int> ByEthnicity(EnrollmentFilterModel model);
        EnrollmentChartModel<int> ByGrade(EnrollmentFilterModel model);
        EnrollmentChartModel<int> ByLunchStatus(EnrollmentFilterModel model);
        EnrollmentChartModel<int> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly IRepository<EnrollmentFact> _enrollmentRepository;

        public EnrollmentService(IRepository<EnrollmentFact> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public EnrollmentChartModel<int> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.EnglishLanguageLearnerStatus)
                    .Select(x => new
                    {
                        EnglishLanguageLearnerStatus = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.EnglishLanguageLearnerStatus);

            return new EnrollmentChartModel<int>
            {
                Title = "English Language Learner",
                Headers = new List<string> { "", "English Language Learner Status", "Enrollment Count" },
                Labels = results.Select(x => x.EnglishLanguageLearnerStatus).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                ShowChart = true
            };
        }

        public EnrollmentChartModel<int> ByEthnicity(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.Ethnicity)
                    .Select(x => new
                    {
                        Ethnicity = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.Ethnicity);

            return new EnrollmentChartModel<int>
            {
                Title = "Ethnicity",
                Headers = new List<string> { "", "Ethnicity", "Enrollment Count" },
                Labels = results.Select(x => x.Ethnicity).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                ShowChart = true
            };
        }
        public EnrollmentChartModel<int> ByGrade(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.GradeLevel)
                    .Select(x => new
                    {
                        GradeLevel = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.GradeLevel);

            return new EnrollmentChartModel<int>
            {
                Title = "Grade",
                Headers = new List<string> {"", "Grades", "Enrollment Count"},
                Labels = results.Select(x => x.GradeLevel).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                ShowChart = false
            };
        }

        public EnrollmentChartModel<int> ByLunchStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.FreeReducedLunchStatus)
                    .Select(x => new
                    {
                        LunchStatus = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.LunchStatus);

            return new EnrollmentChartModel<int>
            {
                Title = "Free/Reduced Price Meals",
                Headers = new List<string> { "", "Lunch Status", "Enrollment Count" },
                Labels = results.Select(x => x.LunchStatus).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                ShowChart = true
            };
        }

        public EnrollmentChartModel<int> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Demographic.SpecialEducationStatus)
                    .Select(x => new
                    {
                        SpecialEducationStatus = x.Key,
                        Total = x.Sum(y => y.EnrollmentStudentCount)
                    }).OrderBy(x => x.SpecialEducationStatus);

            return new EnrollmentChartModel<int>
            {
                Title = "Special Education",
                Headers = new List<string> { "", "Special Education Status", "Enrollment Count" },
                Labels = results.Select(x => x.SpecialEducationStatus).ToList(),
                Data = results.Select(x => x.Total).ToList(),
                ShowChart = true
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

    }
}
