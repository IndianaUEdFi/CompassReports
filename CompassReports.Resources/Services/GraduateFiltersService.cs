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
    public interface IGraduateFiltersService
    {
        List<FilterModel<short>> GetCohorts(short? expectedGradYear = null);
        List<FilterModel<short>> GetSchoolYears();
    }

    public class GraduateFiltersService : IGraduateFiltersService
    {
        private readonly IRepository<SchoolYearDimension> _schoolYearRepository;
        private readonly IRepository<GraduationFact> _graduationFactRepository;

        public GraduateFiltersService(
            IRepository<SchoolYearDimension> schoolYearRepository,
            IRepository<GraduationFact> graduationFactRepository)
        {
            _schoolYearRepository = schoolYearRepository;
            _graduationFactRepository = graduationFactRepository;
        }

        public List<FilterModel<short>> GetCohorts(short? expectedGradYear = null)
        {
            return expectedGradYear.HasValue ? GetCohortsByExpectedGraduationYear(expectedGradYear.Value) : GetUniqueCohorts();
        }

        private List<FilterModel<short>> GetCohortsByExpectedGraduationYear(short expectedGradYear)
        {
            return _graduationFactRepository
                .GetAll()
                .Where(x => x.Demographic.ExpectedGraduationYear == expectedGradYear.ToString())
                .Select(x => x.SchoolYearKey)
                .Distinct()
                .ToList()
                .Select(x => new FilterModel<short>
                {
                    Display = GetCohortName(x, expectedGradYear),
                    Value = x
                })
                .ToList();
        }

        private List<FilterModel<short>> GetUniqueCohorts()
        {
            var values = new[] { "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };

            return _graduationFactRepository
                .GetAll()
                .Select(x => new { SchoolYear = x.SchoolYearKey, x.Demographic.ExpectedGraduationYear })
                .Distinct()
                .ToList()
                .Select(x => (short)(x.SchoolYear - short.Parse(x.ExpectedGraduationYear)))
                .Distinct()
                .OrderBy(x => x)
                .Select(x => new FilterModel<short>
                {
                    Display = values[x] + " Year",
                    Value = x
                })
                .ToList();
        }

        private static string GetCohortName(short schoolYear, short expectedGradYear)
        {
            var values = new [] {"Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"};
            return values[schoolYear - expectedGradYear] + " Year";
        }

        public List<FilterModel<short>> GetSchoolYears()
        {
            var graduationYears = _graduationFactRepository
                .GetAll()
                .Select(x => x.Demographic.ExpectedGraduationYear)
                .Distinct()
                .ToList()
                .Select(short.Parse)
                .ToList();

            return _schoolYearRepository
                .GetAll()
                .Where(x => graduationYears.Contains(x.SchoolYearKey))
                .Select(x => new FilterModel<short>
                {
                    Display = x.SchoolYearDescription,
                    Value = x.SchoolYearKey
                }).Distinct()
                .OrderByDescending(x => x.Value)
                .ToList();

        }
    }
}
