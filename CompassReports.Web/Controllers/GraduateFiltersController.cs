using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/graduate-filters")]
    public class GraduateFiltersController : ApiController
    {
        private readonly IGraduateFiltersService _graduateFiltersService;
        public GraduateFiltersController(IGraduateFiltersService graduateFiltersService)
        {
            _graduateFiltersService = graduateFiltersService;
        }

        [Route("cohorts")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetCohorts(short? expectedGraduationYear = null)
        {
            var cohorts = await _graduateFiltersService.GetCohorts(expectedGraduationYear);
            return Ok(cohorts);
        }

        [Route("school-years")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetSchoolYears()
        {
            var years = await _graduateFiltersService.GetSchoolYears();
            return Ok(years);
        }
    }
}
