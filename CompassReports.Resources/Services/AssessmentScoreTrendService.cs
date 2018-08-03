using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentScoreTrendService
    {
        Task<BarChartModel<int>> Get(AssessmentTrendFilterModel model);
        Task<BarChartModel<int>> ByEnglishLanguageLearner(AssessmentTrendFilterModel model);
        Task<BarChartModel<int>> ByEthnicity(AssessmentTrendFilterModel model);
        Task<BarChartModel<int>> ByLunchStatus(AssessmentTrendFilterModel model);
        Task<BarChartModel<int>> BySpecialEducation(AssessmentTrendFilterModel model);
    }

    public class AssessmentScoreTrendService : IAssessmentScoreTrendService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;

        public AssessmentScoreTrendService(IRepository<AssessmentFact> assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }
        public class AssessmentScoreTrendGroupBy
        {
            public int? ScoreResult { get; set; }
            public string GroupByProperty { get; set; }
            public short SchoolYearKey { get; set; }
            public string SchoolYearDescription { get; set; }
        }

        public async Task<BarChartModel<int>> Get(AssessmentTrendFilterModel model)
        {
            var query = BaseQuery(model);

            var results = await query.GroupBy(x => new { x.Performance.ScoreResult, x.SchoolYearKey, x.SchoolYearDimension.SchoolYearDescription })
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.HasValue ? x.Key.ScoreResult.Value : 0,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    SchoolYear = x.Key.SchoolYearKey,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .ToListAsync();

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

        public async Task<BarChartModel<int>> ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreTrendGroupBy
                {
                    SchoolYearKey = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await CreateChart(groupings, "English Language Learner");
        }

        public async Task<BarChartModel<int>> ByEthnicity(AssessmentTrendFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreTrendGroupBy
                {
                    SchoolYearKey = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.Ethnicity
                });

            return await CreateChart(groupings, "Ethnicity");
        }

        public async Task<BarChartModel<int>> ByLunchStatus(AssessmentTrendFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreTrendGroupBy
                {
                    SchoolYearKey = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.FreeReducedLunchStatus
                });

            return await CreateChart(groupings, "Free/Reduced Lunch Status");
        }

        public async Task<BarChartModel<int>> BySpecialEducation(AssessmentTrendFilterModel model)
        {
            var groupings = BaseQuery(model)
                .GroupBy(x => new AssessmentScoreTrendGroupBy
                {
                    SchoolYearKey = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    ScoreResult = x.Performance.ScoreResult,
                    GroupByProperty = x.Demographic.SpecialEducationStatus
                });

            return await CreateChart(groupings, "Special Education");
        }

        private IQueryable<AssessmentFact> BaseQuery(AssessmentTrendFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .Where(x => x.Performance.ScoreResult != null);

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

        private async Task<BarChartModel<int>> CreateChart(IQueryable<IGrouping<AssessmentScoreTrendGroupBy, AssessmentFact>> groupings, string type)
        {
            var results = await groupings
                .Select(x => new
                {
                    ScoreResult = x.Key.ScoreResult.HasValue ? x.Key.ScoreResult.Value : 0,
                    GroupByProperty = x.Key.GroupByProperty,
                    SchoolYear = x.Key.SchoolYearKey,
                    SchoolYearDescription = x.Key.SchoolYearDescription,
                    Total = x.Sum(y => y.AssessmentStudentCount)
                })
                .OrderBy(x => x.GroupByProperty)
                .ToListAsync();

            var properties = results.Select(x => x.GroupByProperty).Distinct().OrderBy(x => x).ToList();
            var schoolYears = results.Select(x => x.SchoolYear).Distinct().OrderBy(x => x).ToList();
            var schoolYearDescriptions = results.Select(x => new { y = x.SchoolYear, d = x.SchoolYearDescription }).Distinct().OrderBy(x => x.y).Select(x => x.d).ToList();

            var data = new List<List<int>>();
            foreach (var property in properties)
            {
                var values = new List<int>();
                foreach (var schoolYear in schoolYears)
                {
                    var resultTotal = results.Where(x => x.GroupByProperty == property && x.SchoolYear == schoolYear).Sum(x => x.ScoreResult * x.Total);
                    var totalParticipants = results.Where(x => x.GroupByProperty == property && x.SchoolYear == schoolYear).Sum(x => x.Total);

                    var averageScore = (totalParticipants == 0) ? 0 : resultTotal / totalParticipants;
                    values.Add(averageScore);
                }
                data.Add(values);
            }

            var headers = new List<string> { "", "Assessment" };
            headers.AddRange(schoolYearDescriptions);

            return new BarChartModel<int>
            {
                Title = "Average Score Trend By " + type,
                TotalRowTitle = "Average Score",
                Headers = headers,
                Labels = schoolYearDescriptions,
                Series = properties,
                Data = data,
                ShowChart = true,
                HideTotal = true,
            };
        }
    }
}
