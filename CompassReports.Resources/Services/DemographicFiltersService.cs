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
    public interface IDemographicFiltersService
    {
        List<string> GetEnglishLanguageLearnerStatuses();
        List<string> GetEthnicities();
        List<string> GetGrades();
        List<string> GetLunchStatuses();
        List<string> GetSpecialEducationStatuses();
    }

    public class DemographicFiltersService : IDemographicFiltersService
    {
        private readonly IRepository<DemographicJunkDimension> _demographicJunkRepository;

        public DemographicFiltersService(IRepository<DemographicJunkDimension> demographicJunkRepository)
        {
            _demographicJunkRepository = demographicJunkRepository;
        }
        public List<string> GetEnglishLanguageLearnerStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.EnglishLanguageLearnerStatus).Distinct().OrderBy(x => x).ToList();
        }
        public List<string> GetEthnicities()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.Ethnicity).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetGrades()
        {
            return _demographicJunkRepository.GetAll().Select(x => new { x.GradeLevel, x.GradeLevelSort}).Distinct().OrderBy(x => x.GradeLevelSort).Select(x => x.GradeLevel).ToList();
        }

        public List<string> GetLunchStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetSpecialEducationStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();
        }
    }
}
