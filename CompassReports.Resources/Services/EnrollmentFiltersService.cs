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
        List<FilterModel<short>> GetSchoolYears();
    }

    public class EnrollmentFiltersService : IEnrollmentFiltersService
    {
        private readonly IRepository<EnrollmentFact> _enrollmentRepository;

        public EnrollmentFiltersService(IRepository<EnrollmentFact> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
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
