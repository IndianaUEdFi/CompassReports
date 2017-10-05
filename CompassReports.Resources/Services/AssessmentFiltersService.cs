using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Context;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentFiltersService
    {
        List<string> GetAssessments();
        List<string> GetEnglishLanguageLearnerStatuses();
        List<string> GetEthnicities();
        List<FilterModel<int>> GetGoodCauseExcemptions(string assessmentTitle, string subject);
        List<FilterModel<int>> GetGrades(string assessmentTitle, string subject);
        List<string> GetLunchStatuses();
        List<FilterModel<int>> GetPerformanceLevels(string assessmentTitle, string subject);
        List<string> GetSubjects(string assessmentTitle);
        List<string> GetSpecialEducationStatuses();
        List<FilterModel<short>> GetSchoolYears(string assessmentTitle, string subject);
    }

    public class AssessmentFiltersService: IAssessmentFiltersService
    {
        private readonly IRepository<DemographicJunkDimension> _demographicJunkRepository;
        private readonly IRepository<AssessmentDimension> _assessmentDimensionRepository;

        public AssessmentFiltersService(
            IRepository<DemographicJunkDimension> demographicJunkRepository,
            IRepository<AssessmentDimension> assessmentDimensionRepository)
        {
            _demographicJunkRepository = demographicJunkRepository;
            _assessmentDimensionRepository = assessmentDimensionRepository;;
        }

        public List<string> GetAssessments()
        {
            return _assessmentDimensionRepository.GetAll()
            .Select(x => x.AssessmentTitle)
            .Distinct()
            .OrderBy(x => x)
            .ToList();
        }

        public List<string> GetEnglishLanguageLearnerStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToList();
        }
        public List<string> GetEthnicities()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToList();
        }

        public  List<FilterModel<int>> GetGoodCauseExcemptions(string assessmentTitle, string subject)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .SelectMany(x => x.AssessmentFacts.Select(y => new FilterModel<int> { Display = y.GoodCauseExemption.GoodCauseExemption, Value = y.GoodCauseExemptionKey }))
                .Distinct()
                .OrderBy(x => x.Display)
                .ToList();
        }

        public List<FilterModel<int>> GetGrades(string assessmentTitle, string subject)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .Select(x => new FilterModel<int> { Display = x.AssessedGradeLevel, Value = x.AssessmentKey })
                .Distinct()
                .OrderBy(x => x.Display)
                .ToList();
        }
        public List<string> GetLunchStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();
        }

        public List<FilterModel<int>> GetPerformanceLevels(string assessmentTitle, string subject)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .SelectMany(x => x.AssessmentFacts.Select(y => new FilterModel<int> { Display = y.PerformanceLevel.PerformanceLevel, Value = y.PerformanceLevelKey }))
                .Distinct()
                .OrderBy(x => x.Display)
                .ToList();
        }

        public List<string> GetSubjects(string assessmentTitle)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle)
                .Select(x => x.AcademicSubject)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public List<string> GetSpecialEducationStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();
        }

        public List<FilterModel<short>> GetSchoolYears(string assessmentTitle, string subject)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .SelectMany(x => x.AssessmentFacts.Select(y => new FilterModel<short> { Display = y.SchoolYearDimension.SchoolYearDescription, Value = y.SchoolYearKey }))
                .Distinct()
                .OrderByDescending(x => x.Value)
                .ToList();
        }
    }
}
