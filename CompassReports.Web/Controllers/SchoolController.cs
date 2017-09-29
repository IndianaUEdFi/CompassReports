using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The Students resource endpoint.
    /// </summary>
    public class SchoolController : ApiController
    {
        private readonly ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public IHttpActionResult Get()
        {
            _schoolService.Get();
            return Ok();
        }
    }
}
