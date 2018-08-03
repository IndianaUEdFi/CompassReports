using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;

namespace CompassReports.Resources.Services
{
    public interface IDemographicFiltersService
    {
        Task<List<string>> GetEnglishLanguageLearnerStatuses();
        Task<List<string>> GetEthnicities();
        Task<List<string>> GetGrades();
        Task<List<string>> GetLunchStatuses();
        Task<List<string>> GetSpecialEducationStatuses();
    }

    public class DemographicFiltersService : IDemographicFiltersService
    {
        private readonly IRepository<DemographicJunkDimension> _demographicJunkRepository;

        public DemographicFiltersService(IRepository<DemographicJunkDimension> demographicJunkRepository)
        {
            _demographicJunkRepository = demographicJunkRepository;
        }
        public async Task<List<string>> GetEnglishLanguageLearnerStatuses()
        {
            return await _demographicJunkRepository.GetAll().Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToListAsync();
        }
        public async Task<List<string>> GetEthnicities()
        {
            return await _demographicJunkRepository.GetAll().Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToListAsync();
        }

        public async Task<List<string>> GetGrades()
        {
            return await _demographicJunkRepository.GetAll().Select(x => new { x.GradeLevel, x.GradeLevelSort}).Distinct().OrderBy(x => x.GradeLevelSort).Select(x => x.GradeLevel).ToListAsync();
        }

        public async Task<List<string>> GetLunchStatuses()
        {
            return await _demographicJunkRepository.GetAll().Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToListAsync();
        }

        public async Task<List<string>> GetSpecialEducationStatuses()
        {
            return await _demographicJunkRepository.GetAll().Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToListAsync();
        }
    }
}
