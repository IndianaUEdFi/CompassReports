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
    public interface IAssessmentFactService
    {
        IQueryable<AssessmentFact> BaseQuery(AssessmentFilterModel model);
    }

    public class AssessmentFactService : IAssessmentFactService
    {
        private readonly IRepository<AssessmentFact> _assessmentFactRepository;

        public AssessmentFactService(IRepository<AssessmentFact> assessmentFactRepository) {
            _assessmentFactRepository = assessmentFactRepository;
        }

        public IQueryable<AssessmentFact> BaseQuery(AssessmentFilterModel model)
        {
            var query = _assessmentFactRepository
                .GetAll()
                .AsQueryable();

            if (model.SchoolYear.HasValue)
                query = query.Where(x => x.SchoolYearKey == model.SchoolYear);

            if (model.SchoolYears != null && model.SchoolYears.Any())
                query = query.Where(x => model.SchoolYears.Contains(x.SchoolYearKey));

            if (model.Schools != null && model.Schools.Any())
                query = query.Where(x => model.Schools.Contains(x.SchoolKey));

            if (model.Districts != null && model.Districts.Any())
                query = query.Where(x => model.Districts.Contains(x.School.LocalEducationAgencyKey));

            if (model.Assessments != null && model.Assessments.Any())
                query = query.Where(x => model.Assessments.Contains(x.AssessmentKey));
            else
                query = query.Where(x => x.Assessment.AssessmentTitle == model.AssessmentTitle && x.Assessment.AcademicSubject == model.Subject);

            if (model.EnglishLanguageLearnerStatuses != null && model.EnglishLanguageLearnerStatuses.Any())
                query = query.Where(x => model.EnglishLanguageLearnerStatuses.Contains(x.Demographic.EnglishLanguageLearnerStatus));

            if (model.Ethnicities != null && model.Ethnicities.Any())
                query = query.Where(x => model.Ethnicities.Contains(x.Demographic.Ethnicity));

            if (model.PerformanceKeys != null && model.PerformanceKeys.Any())
                query = query.Where(x => model.PerformanceKeys.Contains(x.PerformanceKey));

            if (model.ExcludePerformanceKeys != null && model.ExcludePerformanceKeys.Any())
                query = query.Where(x => !model.ExcludePerformanceKeys.Contains(x.PerformanceKey));

            if (model.LunchStatuses != null && model.LunchStatuses.Any())
                query = query.Where(x => model.LunchStatuses.Contains(x.Demographic.FreeReducedLunchStatus));

            if (model.SpecialEducationStatuses != null && model.SpecialEducationStatuses.Any())
                query = query.Where(x => model.SpecialEducationStatuses.Contains(x.Demographic.SpecialEducationStatus));

            return query;
        }
    }
}
