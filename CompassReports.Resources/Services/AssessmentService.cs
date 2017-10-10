using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentService
    {
        PercentageTotalBarChartModel ByGoodCauseExcemption(AssessmentFilterModel model);
    }

    public class AssessmentService : IAssessmentService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;
        private readonly IRepository<GoodCauseExemptionJunkDimension> _goodCauseExemptionRepository;

        public AssessmentService(IRepository<AssessmentFact> assessmentRepository,
            IRepository<GoodCauseExemptionJunkDimension> goodCauseExemptionRepository)
        {
            _assessmentRepository = assessmentRepository;
            _goodCauseExemptionRepository = goodCauseExemptionRepository;
        }

        public PercentageTotalBarChartModel ByGoodCauseExcemption(AssessmentFilterModel model)
        {
            var baseQuery = BaseQuery(model);

            if (baseQuery.All(x => x.GoodCauseExemptionKey == 3)) return null;

            var results = (
                from cause in
                _goodCauseExemptionRepository.GetAll().Where(x => (new[] {1, 2}).Contains(x.GoodCauseExemptionKey))
                join fact in baseQuery on cause.GoodCauseExemptionKey equals fact.GoodCauseExemptionKey into facts
                from fact in facts.DefaultIfEmpty()
                select new
                {
                    GoodCauseExemptionKey = cause.GoodCauseExemptionKey,
                    GoodCauseExemption = cause.GoodCauseExemption,
                    AssessmentStudentCount = fact == null ? 0 : fact.AssessmentStudentCount
                }
            ).GroupBy(x => new
            {
                GoodCauseExemptionKey = x.GoodCauseExemptionKey,
                GoodCauseExemption = x.GoodCauseExemption
            }).Select(x => new
            {
                GoodCauseExemption = x.Key.GoodCauseExemption,
                GoodCauseExemptionKey = x.Key.GoodCauseExemptionKey,
                Total = x.Sum(y => y.AssessmentStudentCount)
            }).ToList();

            var goodCause = results.Where(x => x.GoodCauseExemptionKey == 1).ToList();

            return new PercentageTotalBarChartModel
            {
                Title = "Good Cause Exemptions",
                Headers = new List<string> { "", "Good Cause Exemptions", "Total"},
                Labels = goodCause.Select(x => x.GoodCauseExemption).ToList(),
                Series = new List<string> { goodCause.First().GoodCauseExemption },
                Data = new List<List<PercentageTotalDataModel>> {
                    goodCause.Select( x => new PercentageTotalDataModel
                    {
                        Percentage = GetPercentage(x.Total, results.Sum(y => y.Total)),
                        Total = x.Total
                    }).ToList()
                },
                ShowChart = true,
                ShowPercentage = true,
                HideTotal = true
            };
        }

        private IQueryable<AssessmentFact> BaseQuery(AssessmentFilterModel model)
        {
            var query = _assessmentRepository
                .GetAll()
                .Where(x => x.SchoolYearKey == model.SchoolYear);

            if (model.Assessments != null && model.Assessments.Any())
                query = query.Where(x => model.Assessments.Contains(x.AssessmentKey));
            else
                query = query.Where(x => x.Assessment.AssessmentTitle == model.AssessmentTitle && x.Assessment.AcademicSubject == model.Subject);

            if (model.EnglishLanguageLearnerStatuses != null && model.EnglishLanguageLearnerStatuses.Any())
                query = query.Where(x => model.EnglishLanguageLearnerStatuses.Contains(x.Demographic.EnglishLanguageLearnerStatus));

            if (model.Ethnicities != null && model.Ethnicities.Any())
                query = query.Where(x => model.Ethnicities.Contains(x.Demographic.Ethnicity));

            if (model.GoodCauseExcemptions != null && model.GoodCauseExcemptions.Any())
                query = query.Where(x => model.GoodCauseExcemptions.Contains(x.GoodCauseExemptionKey));

            if (model.LunchStatuses != null && model.LunchStatuses.Any())
                query = query.Where(x => model.LunchStatuses.Contains(x.Demographic.FreeReducedLunchStatus));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            return query;
        }

        private static double GetPercentage(int subTotal, int total)
        {
            return Math.Round(100 * ((double) subTotal / (double) total), 2);
        }
    }
}
