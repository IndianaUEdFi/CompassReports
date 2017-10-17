using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentScoresService
    {
        BarChartModel<int> Get(AssessmentFilterModel model);
        BarChartModel<int> ByEnglishLanguageLearner(AssessmentFilterModel model);
        BarChartModel<int> ByEthnicity(AssessmentFilterModel model);
        BarChartModel<int> ByLunchStatus(AssessmentFilterModel model);
        BarChartModel<int> BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentScoresService : IAssessmentScoresService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;
        private readonly IRepository<AssessmentDimension> _assessmentDimensionRepository;

        public AssessmentScoresService(IRepository<AssessmentFact> assessmentRepository,
            IRepository<AssessmentDimension> assessmentDimensionRepository)
        {
            _assessmentRepository = assessmentRepository;
            _assessmentDimensionRepository = assessmentDimensionRepository;
        }

        public BarChartModel<int> Get(AssessmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => x.Performance.ScoreResult)
                .Select(x => new
                {
                    ScoreResult = x.Key.Value,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToList();

            var resultTotal = results.Sum(x => x.ScoreResult * x.Total);
            var totalParticipants = results.Sum(x => x.Total);

            var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
        
            return new BarChartModel<int>
            {
                Title = "Average Score",
                TotalRowTitle = "Average Score",
                Headers = new List<string> { "", "Assessment", "Average Score" },
                Labels = new List<string> { "Score" },
                Series = new List<string> { model.AssessmentTitle },
                Data = new List<List<int>> { new List<int> { averageScore } },
                ShowChart = true,
                HideTotal = true,
                MaxYAxisValue = GetMaxScore(model)
            };
        }

        public BarChartModel<int> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Performance.ScoreResult, x.Demographic.EnglishLanguageLearnerStatus })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.Value,
                    EnglishLanguageLearnerStatus = x.Key.EnglishLanguageLearnerStatus,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .OrderBy(x => x.EnglishLanguageLearnerStatus)
                .ToList();

            var englishLanguageLearnerStatuses = results.Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToList();
            var data = new List<List<int>>();
            foreach (var status in englishLanguageLearnerStatuses)
            {
                var resultTotal = results.Where(x => x.EnglishLanguageLearnerStatus == status).Sum(x => x.ScoreResult * x.Total);
                var totalParticipants = results.Where(x => x.EnglishLanguageLearnerStatus == status).Sum(x => x.Total);

                var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                data.Add(new List<int> {averageScore});
            }

            return new BarChartModel<int>
            {
                Title = "Average Score By English Language Learner",
                TotalRowTitle = "Average Score",
                Headers = new List<string> { "", "Assessment", "Average Score" },
                Labels = new List<string> { "Score" },
                Series = englishLanguageLearnerStatuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                MaxYAxisValue = GetMaxScore(model)
            };
        }

        public BarChartModel<int> ByEthnicity(AssessmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Performance.ScoreResult, x.Demographic.Ethnicity })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.Value,
                    Ethnicity = x.Key.Ethnicity,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .OrderBy(x => x.Ethnicity)
                .ToList();

            var ethnicities = results.Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToList();
            var data = new List<List<int>>();
            foreach (var ethnicity in ethnicities)
            {
                var resultTotal = results.Where(x => x.Ethnicity == ethnicity).Sum(x => x.ScoreResult * x.Total);
                var totalParticipants = results.Where(x => x.Ethnicity == ethnicity).Sum(x => x.Total);

                var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                data.Add(new List<int> { averageScore });
            }

            return new BarChartModel<int>
            {
                Title = "Average Score By Ethnicity",
                TotalRowTitle = "Average Score",
                Headers = new List<string> { "", "Assessment", "Average Score" },
                Labels = new List<string> { "Score" },
                Series = ethnicities,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                MaxYAxisValue = GetMaxScore(model)
            };
        }

        public BarChartModel<int> ByLunchStatus(AssessmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Performance.ScoreResult, x.Demographic.FreeReducedLunchStatus })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.Value,
                    FreeReducedLunchStatus = x.Key.FreeReducedLunchStatus,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .OrderBy(x => x.FreeReducedLunchStatus)
                .ToList();

            var statuses = results.Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();
            var data = new List<List<int>>();
            foreach (var status in statuses)
            {
                var resultTotal = results.Where(x => x.FreeReducedLunchStatus == status).Sum(x => x.ScoreResult * x.Total);
                var totalParticipants = results.Where(x => x.FreeReducedLunchStatus == status).Sum(x => x.Total);

                var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                data.Add(new List<int> { averageScore });
            }

            return new BarChartModel<int>
            {
                Title = "Average Score by Free/Reduced Meal Status",
                TotalRowTitle = "Average Score",
                Headers = new List<string> { "", "Assessment", "Average Score" },
                Labels = new List<string> { "Score" },
                Series = statuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                MaxYAxisValue = GetMaxScore(model)
            };
        }

        public BarChartModel<int> BySpecialEducation(AssessmentFilterModel model)
        {
            var query = BaseQuery(model);

            var results = query.GroupBy(x => new { x.Performance.ScoreResult, x.Demographic.SpecialEducationStatus })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.Value,
                    SpecialEducationStatus = x.Key.SpecialEducationStatus,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .OrderBy(x => x.SpecialEducationStatus)
                .ToList();

            var statuses = results.Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();
            var data = new List<List<int>>();
            foreach (var status in statuses)
            {
                var resultTotal = results.Where(x => x.SpecialEducationStatus == status).Sum(x => x.ScoreResult * x.Total);
                var totalParticipants = results.Where(x => x.SpecialEducationStatus == status).Sum(x => x.Total);

                var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                data.Add(new List<int> { averageScore });
            }

            return new BarChartModel<int>
            {
                Title = "Average Score by Special Education",
                TotalRowTitle = "Average Score",
                Headers = new List<string> { "", "Assessment", "Average Score" },
                Labels = new List<string> { "Score" },
                Series = statuses,
                Data = data,
                ShowChart = true,
                HideTotal = true,
                MaxYAxisValue = GetMaxScore(model)
            };
        }

        private IQueryable<AssessmentFact> BaseQuery(AssessmentFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .Where(x => x.SchoolYearKey == model.SchoolYear && x.Performance.ScoreResult.HasValue);

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

        private int? GetMaxScore(AssessmentFilterModel model)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == model.AssessmentTitle && x.AcademicSubject == model.Subject)
                .Max(x => x.MaxScore);

        }
    }
}
