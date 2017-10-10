using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentScoreTrendService
    {
        BarChartModel<int> Get(AssessmentTrendFilterModel model);
        BarChartModel<int> ByEnglishLanguageLearner(AssessmentTrendFilterModel model);
        BarChartModel<int> ByEthnicity(AssessmentTrendFilterModel model);
        BarChartModel<int> ByLunchStatus(AssessmentTrendFilterModel model);
        BarChartModel<int> BySpecialEducation(AssessmentTrendFilterModel model);
    }

    public class AssessmentScoreTrendService : IAssessmentScoreTrendService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;

        public AssessmentScoreTrendService(IRepository<AssessmentFact> assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        public BarChartModel<int> Get(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Performance.ScoreResult, x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    SchoolYear = x.Key.SchoolYearKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToList()
                .Select(x => new
                {
                    ScoreResult = int.Parse(x.ScoreResult),
                    SchoolYear = x.SchoolYear,
                    SchoolYearDescription = x.SchoolYearDescription,
                    Total = x.Total
                })
                .OrderBy(x => x.SchoolYear)
                .ToList();

            var schoolYears = results.Select(x => x.SchoolYear).Distinct().OrderBy(x => x).ToList();
            var schoolYearDescriptions = results.Select(x => new { y = x.SchoolYear, d = x.SchoolYearDescription }).Distinct().OrderBy(x => x.y).Select(x => x.d).ToList();

            var data = new List<int>();
            foreach (var year in schoolYears)
            {
                var resultTotal = results.Where(x => x.SchoolYear == year).Sum(x => x.ScoreResult * x.Total);
                var totalParticipants = results.Where(x => x.SchoolYear == year).Sum(x => x.Total);

                var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                data.Add(averageScore);
            }

            var headers = new List<string> {"", "Assessment"};
            headers.AddRange(schoolYearDescriptions);

            return new BarChartModel<int>
            {
                Title = "Average Score Trend",
                Headers = headers,
                Labels = schoolYearDescriptions,
                Series = new List<string> { model.AssessmentTitle },
                Data = new List<List<int>> { data },
                ShowChart = true,
                HideTotal = true,
            };
        }

        public BarChartModel<int> ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Performance.ScoreResult, x.Demographic.EnglishLanguageLearnerStatus })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult,
                    EnglishLanguageLearnerStatus = x.Key.EnglishLanguageLearnerStatus,
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToList()
                .Select(x => new
                {
                    ScoreResult = int.Parse(x.ScoreResult),
                    EnglishLanguageLearnerStatus = x.EnglishLanguageLearnerStatus,
                    SchoolYear = x.SchoolYear,
                    SchoolYearDescription = x.SchoolYearDescription,
                    Total = x.Total
                })
                .OrderBy(x => x.EnglishLanguageLearnerStatus)
                .ToList();

            var statuses = results.Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToList();
            var schoolYears = results.Select(x => x.SchoolYear).Distinct().OrderBy(x => x).ToList();
            var schoolYearDescriptions = results.Select(x => new { y = x.SchoolYear, d = x.SchoolYearDescription }).Distinct().OrderBy(x => x.y).Select(x => x.d).ToList();

            var data = new List<List<int>>();
            foreach (var ethnicity in statuses)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var resultTotal = results.Where(x => x.EnglishLanguageLearnerStatus == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.ScoreResult * x.Total);
                    var totalParticipants = results.Where(x => x.EnglishLanguageLearnerStatus == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.Total);

                    var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                    values.Add(averageScore);
                }
                data.Add(values);
            }

            var headers = new List<string> { "", "Assessment" };
            headers.AddRange(schoolYearDescriptions);

            return new BarChartModel<int>
            {
                Title = "Average Score Trend By English Language Learner",
                TotalRowTitle = "Average Score",
                Headers = headers,
                Labels = schoolYearDescriptions,
                Series = statuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
            };
        }

        public BarChartModel<int> ByEthnicity(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Performance.ScoreResult, x.Demographic.Ethnicity })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult,
                    Ethnicity = x.Key.Ethnicity,
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToList()
                .Select(x => new
                {
                    ScoreResult = int.Parse(x.ScoreResult),
                    Ethnicity = x.Ethnicity,
                    SchoolYear = x.SchoolYear,
                    SchoolYearDescription = x.SchoolYearDescription,
                    Total = x.Total
                })
                .OrderBy(x => x.Ethnicity)
                .ToList();

            var ethnicities = results.Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToList();
            var schoolYears = results.Select(x => x.SchoolYear).Distinct().OrderBy(x => x).ToList();
            var schoolYearDescriptions = results.Select(x => new { y = x.SchoolYear, d = x.SchoolYearDescription }).Distinct().OrderBy(x => x.y).Select(x => x.d).ToList();

            var data = new List<List<int>>();
            foreach (var ethnicity in ethnicities)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var resultTotal = results.Where(x => x.Ethnicity == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.ScoreResult * x.Total);
                    var totalParticipants = results.Where(x => x.Ethnicity == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.Total);

                    var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                    values.Add(averageScore);
                }
                data.Add(values);
            }

            var headers = new List<string> { "", "Assessment" };
            headers.AddRange(schoolYearDescriptions);

            return new BarChartModel<int>
            {
                Title = "Average Score Trend By Ethnicity",
                TotalRowTitle = "Average Score",
                Headers = headers,
                Labels = schoolYearDescriptions,
                Series = ethnicities,
                Data = data,
                ShowChart = true,
                HideTotal = true,
            };
        }

        public BarChartModel<int> ByLunchStatus(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Performance.ScoreResult, x.Demographic.FreeReducedLunchStatus })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult,
                    FreeReducedLunchStatus = x.Key.FreeReducedLunchStatus,
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToList()
                .Select(x => new
                {
                    ScoreResult = int.Parse(x.ScoreResult),
                    FreeReducedLunchStatus = x.FreeReducedLunchStatus,
                    SchoolYear = x.SchoolYear,
                    SchoolYearDescription = x.SchoolYearDescription,
                    Total = x.Total
                })
                .OrderBy(x => x.FreeReducedLunchStatus)
                .ToList();

            var statuses = results.Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();
            var schoolYears = results.Select(x => x.SchoolYear).Distinct().OrderBy(x => x).ToList();
            var schoolYearDescriptions = results.Select(x => new { y = x.SchoolYear, d = x.SchoolYearDescription }).Distinct().OrderBy(x => x.y).Select(x => x.d).ToList();

            var data = new List<List<int>>();
            foreach (var ethnicity in statuses)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var resultTotal = results.Where(x => x.FreeReducedLunchStatus == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.ScoreResult * x.Total);
                    var totalParticipants = results.Where(x => x.FreeReducedLunchStatus == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.Total);

                    var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                    values.Add(averageScore);
                }
                data.Add(values);
            }

            var headers = new List<string> { "", "Assessment" };
            headers.AddRange(schoolYearDescriptions);

            return new BarChartModel<int>
            {
                Title = "Average Score Trend By Free/Reduced Lunch Status",
                TotalRowTitle = "Average Score",
                Headers = headers,
                Labels = schoolYearDescriptions,
                Series = statuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
            };
        }

        public BarChartModel<int> BySpecialEducation(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription, x.Performance.ScoreResult, x.Demographic.SpecialEducationStatus })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult,
                    SpecialEducationStatus = x.Key.SpecialEducationStatus,
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToList()
                .Select(x => new
                {
                    ScoreResult = int.Parse(x.ScoreResult),
                    SpecialEducationStatus = x.SpecialEducationStatus,
                    SchoolYear = x.SchoolYear,
                    SchoolYearDescription = x.SchoolYearDescription,
                    Total = x.Total
                })
                .OrderBy(x => x.SpecialEducationStatus)
                .ToList();

            var statuses = results.Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();
            var schoolYears = results.Select(x => x.SchoolYear).Distinct().OrderBy(x => x).ToList();
            var schoolYearDescriptions = results.Select(x => new { y = x.SchoolYear, d = x.SchoolYearDescription }).Distinct().OrderBy(x => x.y).Select(x => x.d).ToList();

            var data = new List<List<int>>();
            foreach (var ethnicity in statuses)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var resultTotal = results.Where(x => x.SpecialEducationStatus == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.ScoreResult * x.Total);
                    var totalParticipants = results.Where(x => x.SpecialEducationStatus == ethnicity && x.SchoolYear == schoolYear).Sum(x => x.Total);

                    var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                    values.Add(averageScore);
                }
                data.Add(values);
            }

            var headers = new List<string> { "", "Assessment" };
            headers.AddRange(schoolYearDescriptions);

            return new BarChartModel<int>
            {
                Title = "Average Score Trend By Special Education",
                TotalRowTitle = "Average Score",
                Headers = headers,
                Labels = schoolYearDescriptions,
                Series = statuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
            };
        }

        private IQueryable<AssessmentFact> BaseQuery(AssessmentTrendFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .Where(x => x.Performance.ScoreResult != "Not Applicable");

            if (model.SchoolYears != null && model.SchoolYears.Any())
                query = query.Where(x => model.SchoolYears.Contains(x.SchoolYearKey));

            if (model.Assessments != null && model.Assessments.Any())
                query = query.Where(x => model.Assessments.Contains(x.AssessmentKey));
            else
                query = query.Where(x => x.Assessment.AssessmentTitle == model.AssessmentTitle && x.Assessment.AcademicSubject == model.Subject);

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
