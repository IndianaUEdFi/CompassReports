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
    /// The School resource endpoint.
    /// </summary>

    [RoutePrefix("api/school")]
    public class SchoolController : ApiController
    {
        private readonly ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public async Task<IHttpActionResult> Get()
        {
            var schools = await _schoolService.GetAll();
            return Ok(schools);
        }
    }
}
