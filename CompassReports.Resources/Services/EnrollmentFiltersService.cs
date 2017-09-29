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
    public interface IEnrollmentFiltersService
    {
        List<string> GetEnglishLanguageLearnerStatuses();
        List<string> GetEthnicities();
        List<string> GetGrades();
        List<string> GetLunchStatuses();
        List<string> GetSpecialEducationStatuses();
        List<FilterModel<short>> GetSchoolYears();
    }

    public class EnrollmentFiltersService : IEnrollmentFiltersService
    {
        private readonly IRepository<DemographicJunkDimension> _demographicJunkRepository;
        private readonly IRepository<EnrollmentFact> _enrollmentRepository;

        public EnrollmentFiltersService(
            IRepository<DemographicJunkDimension> demographicJunkRepository, 
            IRepository<EnrollmentFact> enrollmentRepository)
        {
            _demographicJunkRepository = demographicJunkRepository;
            _enrollmentRepository = enrollmentRepository;
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
            return _demographicJunkRepository.GetAll().Select(x => x.GradeLevel).Distinct().OrderBy(x => x).ToList();
        }
        public List<string> GetLunchStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.FreeReducedLunchStatus).Distinct().OrderBy(x => x).ToList();
        }
        public List<string> GetSpecialEducationStatuses()
        {
            return _demographicJunkRepository.GetAll().Select(x => x.SpecialEducationStatus).Distinct().OrderBy(x => x).ToList();
        }

        public List<FilterModel<short>> GetSchoolYears()
        {
            return _enrollmentRepository
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
