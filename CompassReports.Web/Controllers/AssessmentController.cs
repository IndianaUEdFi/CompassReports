using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment")]
    public class AssessmentController : ApiController
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [Route("by-good-cause")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByGoodCauseExcemption(AssessmentFilterModel model)
        {
            var chart = _assessmentService.ByGoodCauseExcemption(model);
            return Ok(chart);
        }
    }
}
