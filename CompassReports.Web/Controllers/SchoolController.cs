using System;
using System.Web.Http;
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
    }
}
