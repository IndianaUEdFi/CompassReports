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
    [RoutePrefix("api/enrollment-filters")]
    public class EnrollmentFiltersController : ApiController
    {
        private readonly IEnrollmentFiltersService _enrollmentFiltersService;
        public EnrollmentFiltersController(IEnrollmentFiltersService enrollmentFiltersService)
        {
            _enrollmentFiltersService = enrollmentFiltersService;
        }

        [Route("school-years")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetSchoolYears()
        {
            var years = await _enrollmentFiltersService.GetSchoolYears();
            return Ok(years);
        }
    }
}
