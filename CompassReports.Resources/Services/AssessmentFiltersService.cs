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
        List<FilterModel<int>> GetGoodCauseExcemptions(int assessmentKey);
        List<string> GetGrades(int assessmentKey);
        List<string> GetLunchStatuses();
        List<FilterModel<int>> GetPerformanceLevels(int assessmentKey);
        List<FilterModel<int>> GetSubjects(string assessmentTitle);
        List<string> GetSpecialEducationStatuses();
        List<FilterModel<short>> GetSchoolYears();
    }

    public class AssessmentFiltersService: IAssessmentFiltersService
    {
        private readonly IRepository<DemographicJunkDimension> _demographicJunkRepository;
        private readonly IRepository<AssessmentDimension> _assessmentDimensionRepository;
        private readonly IRepository<AssessmentFact> _assessmentRepository;

        public AssessmentFiltersService(
            IRepository<DemographicJunkDimension> demographicJunkRepository,
            IRepository<AssessmentDimension> assessmentDimensionRepository,
            IRepository<AssessmentFact> assessmentRepository)
        {
            _demographicJunkRepository = demographicJunkRepository;
            _assessmentDimensionRepository = assessmentDimensionRepository;
            _assessmentRepository = assessmentRepository;
        }

        public List<string> GetAssessments()
        {
            return _assessmentDimensionRepository.GetAll()
            .Select(x => x.AcademicSubject)
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

        public  List<FilterModel<int>> GetGoodCauseExcemptions(int assessmentKey)
        {
            return _assessmentRepository.GetAll()
                .Where(x => x.AssessmentKey == assessmentKey)
                .Select(x => new FilterModel<int> { Display = x.GoodCauseExemption.GoodCauseExemptionGranted, Value = x.GoodCauseExemptionKey })
                .Distinct()
                .OrderBy(x => x.Display)
                .ToList();
        }

        public List<string> GetGrades(int assessmentKey)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentKey == assessmentKey)
                .Select(x => x.AssessedGradeLevel)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }
        public List<string> GetLunchStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();
        }

        public List<FilterModel<int>> GetPerformanceLevels(int assessmentKey)
        {
            return _assessmentRepository.GetAll()
                .Where(x => x.AssessmentKey == assessmentKey)
                .Select(x => new FilterModel<int> { Display = x.PerformanceLevel.PerformanceLevel, Value = x.PerformanceLevelKey })
                .Distinct()
                .OrderBy(x => x.Display)
                .ToList();
        }

        public List<FilterModel<int>> GetSubjects(string assessmentTitle)
        {
            return _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle)
                .Select(x => new FilterModel<int> { Display = x.AcademicSubject, Value = x.AssessmentKey })
                .Distinct()
                .OrderBy(x => x.Display)
                .ToList();
        }

        public List<string> GetSpecialEducationStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();
        }

        public List<FilterModel<short>> GetSchoolYears()
        {
            return _assessmentRepository
                .GetAll()
                .Select(x => new FilterModel<short>
                {
                    Display = x.SchoolYearDimension.SchoolYearDescription,
                    Value = x.SchoolYearKey
                })
                .Distinct()
                .OrderByDescending(x => x.Value)
                .ToList();
        }
    }
}
