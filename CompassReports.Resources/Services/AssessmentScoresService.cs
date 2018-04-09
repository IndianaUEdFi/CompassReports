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
    public interface IAssessmentScoresService
    {
        Task<BarChartModel<int>> Get(AssessmentFilterModel model);
        Task<BarChartModel<int>> ByEnglishLanguageLearner(AssessmentFilterModel model);
        Task<BarChartModel<int>> ByEthnicity(AssessmentFilterModel model);
        Task<BarChartModel<int>> ByLunchStatus(AssessmentFilterModel model);
        Task<BarChartModel<int>> BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentScoresService : IAssessmentScoresService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;

        public AssessmentScoresService(IRepository<AssessmentFact> assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        public class AssessmentScoreGroupBy
        {
            public int? ScoreResult { get; set; }
            public string GroupByProperty { get; set; }
        }

        public async Task<BarChartModel<int>> Get(AssessmentFilterModel model)
        {
            var results = await BaseQuery(model)
                .GroupBy(x => x.Performance.ScoreResult)
                .Select(x => new
                {
                    ScoreResult = x.Key.HasValue ? x.Key.Value : 0,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToListAsync();

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
            };
        }

        public async Task<BarChartModel<int>> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreGroupBy {
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await CreateChart(groupings, "English Language Learner");
        }

        public async Task<BarChartModel<int>> ByEthnicity(AssessmentFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreGroupBy
                {
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.Ethnicity
                });

            return await CreateChart(groupings, "Ethnicity");
        }

        public async Task<BarChartModel<int>> ByLunchStatus(AssessmentFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreGroupBy
                {
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.FreeReducedLunchStatus
                });

            return await CreateChart(groupings, "Free/Reduced Meal Status");
        }

        public async Task<BarChartModel<int>> BySpecialEducation(AssessmentFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreGroupBy
                {
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.SpecialEducationStatus
                });

            return await CreateChart(groupings, "Special Education");
        }

        private IQueryable<AssessmentFact> BaseQuery(AssessmentFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .Where(x => x.SchoolYearKey == model.SchoolYear && x.Performance.ScoreResult != null);

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

        private async Task<BarChartModel<int>> CreateChart(IQueryable<IGrouping<AssessmentScoreGroupBy, AssessmentFact>> groupings, string type)
        {
            var results = await groupings
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.HasValue ? x.Key.ScoreResult.Value : 0,
                    GroupByProperty = x.Key.GroupByProperty,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .OrderBy(x => x.GroupByProperty)
                .ToListAsync();

            var properties = results.Select(x => x.GroupByProperty).Distinct().OrderBy(x => x).ToList();
            var data = new List<List<int>>();
            foreach (var property in properties)
            {
                var resultTotal = results.Where(x => x.GroupByProperty == property).Sum(x => x.ScoreResult * x.Total);
                var totalParticipants = results.Where(x => x.GroupByProperty == property).Sum(x => x.Total);

                var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                data.Add(new List<int> { averageScore });
            }

            return new BarChartModel<int>
            {
                Title = "Average Score By " + type,
                TotalRowTitle = "Average Score",
                Headers = new List<string> { "", "Assessment", "Average Score" },
                Labels = new List<string> { "Score" },
                Series = properties,
                Data = data,
                ShowChart = true,
                HideTotal = true,
            };
        }
    }
}
