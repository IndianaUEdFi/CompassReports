using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        Task<List<FilterModel<short>>> GetCohorts(short? expectedGradYear = null);
        Task<List<FilterModel<short>>> GetSchoolYears();
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

        public async Task<List<FilterModel<short>>> GetCohorts(short? expectedGradYear = null)
        {
            return await (expectedGradYear.HasValue ? GetCohortsByExpectedGraduationYear(expectedGradYear.Value) : GetUniqueCohorts());
        }

        private async Task<List<FilterModel<short>>> GetCohortsByExpectedGraduationYear(short expectedGradYear)
        {
            return (await _graduationFactRepository
                .GetAll()
                .Where(x => x.Demographic.ExpectedGraduationYear == expectedGradYear)
                .Select(x => x.SchoolYearKey)
                .Distinct()
                .ToListAsync())
                .Select(x => new FilterModel<short>
                {
                    Display = GetCohortName(x, expectedGradYear),
                    Value = x
                })
                .ToList();
        }

        private async Task<List<FilterModel<short>>> GetUniqueCohorts()
        {
            var values = new[] { "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };

            return (await _graduationFactRepository
                .GetAll()
                .Select(x => new { SchoolYear = x.SchoolYearKey, x.Demographic.ExpectedGraduationYear })
                .Distinct()
                .ToListAsync())
                .Select(x => (short)(x.SchoolYear - x.ExpectedGraduationYear))
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

        public async Task<List<FilterModel<short>>> GetSchoolYears()
        {
            var graduationYears = await _graduationFactRepository
                .GetAll()
                .Select(x => x.Demographic.ExpectedGraduationYear)
                .Distinct()
                .ToListAsync();

            return await _schoolYearRepository
                .GetAll()
                .Where(x => graduationYears.Contains(x.SchoolYearKey))
                .Select(x => new FilterModel<short>
                {
                    Display = x.SchoolYearDescription,
                    Value = x.SchoolYearKey
                })
                .Distinct()
                .OrderByDescending(x => x.Value)
                .ToListAsync();

        }
    }
}
