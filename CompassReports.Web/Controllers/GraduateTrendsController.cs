using System;
using System.Threading.Tasks;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/graduate-trends")]
    public class GraduateTrendsController : ApiController
    {
        private readonly IGraduateTrendsService _graduateTrendsService;
        public GraduateTrendsController(IGraduateTrendsService graduateTrendsService)
        {
            _graduateTrendsService = graduateTrendsService;
        }

        [Route("by-status")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByStatus(GraduateFilterModel model)
        {
            var chart = await _graduateTrendsService.ByStatus(model);
            return Ok(chart);
        }

        [Route("by-waiver")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByWaiver(GraduateFilterModel model)
        {
            var chart = await _graduateTrendsService.ByWaiver(model);
            return Ok(chart);
        }

    }
}
