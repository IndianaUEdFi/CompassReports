using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentFiltersService
    {
        Task<List<string>> GetAssessments();
        Task<List<FilterModel<int>>> GetGoodCauseExcemptions(string assessmentTitle, string subject);
        Task<List<FilterModel<int>>> GetGrades(string assessmentTitle, string subject);
        Task<List<FilterModel<int>>> GetPerformanceLevels(string assessmentTitle, string subject);
        Task<List<string>> GetSubjects(string assessmentTitle);
        Task<List<FilterModel<short>>> GetSchoolYears(string assessmentTitle, string subject);
    }

    public class AssessmentFiltersService: IAssessmentFiltersService
    {
        private readonly IRepository<AssessmentDimension> _assessmentDimensionRepository;

        public AssessmentFiltersService(IRepository<AssessmentDimension> assessmentDimensionRepository)
        {
            _assessmentDimensionRepository = assessmentDimensionRepository;;
        }

        public async Task<List<string>> GetAssessments()
        {
            var hideAssessments = new[] {"ACT", "AP", "SAT"};

            return await _assessmentDimensionRepository.GetAll()
                .Where(x => !hideAssessments.Contains(x.AssessmentTitle))
                .Select(x => x.AssessmentTitle)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
        }

        public async Task<List<FilterModel<int>>> GetGoodCauseExcemptions(string assessmentTitle, string subject)
        {
            return await _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .SelectMany(x => x.AssessmentFacts.Select(y => new FilterModel<int> { Display = y.GoodCauseExemption.GoodCauseExemption, Value = y.GoodCauseExemptionKey }))
                .Distinct()
                .OrderBy(x => x.Display)
                .ToListAsync();
        }

        public async Task<List<FilterModel<int>>> GetGrades(string assessmentTitle, string subject)
        {
            return await _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .Select(x => new FilterModel<int> { Display = x.AssessedGradeLevel, Value = x.AssessmentKey })
                .Distinct()
                .OrderBy(x => x.Display)
                .ToListAsync();
        }

        public async Task<List<FilterModel<int>>> GetPerformanceLevels(string assessmentTitle, string subject)
        {
            var query =  _assessmentDimensionRepository.GetAll().Where(x => x.AssessmentTitle == assessmentTitle);
            if(subject != null) query = query.Where(x => x.AcademicSubject == subject);

            return await query
                .SelectMany(x => x.AssessmentFacts.Select(y => new FilterModel<int> { Display = y.Performance.PerformanceLevel, Value = y.PerformanceKey }))
                .Distinct()
                .OrderBy(x => x.Display)
                .ToListAsync();
        }

        public async Task<List<string>> GetSubjects(string assessmentTitle)
        {
            return await _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle)
                .Select(x => x.AcademicSubject)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
        }

        public async Task<List<FilterModel<short>>> GetSchoolYears(string assessmentTitle, string subject)
        {
            return await _assessmentDimensionRepository.GetAll()
                .Where(x => x.AssessmentTitle == assessmentTitle && x.AcademicSubject == subject)
                .SelectMany(x => x.AssessmentFacts.Select(y => new FilterModel<short> { Display = y.SchoolYearDimension.SchoolYearDescription, Value = y.SchoolYearKey }))
                .Distinct()
                .OrderByDescending(x => x.Value)
                .ToListAsync();
        }
    }
}
