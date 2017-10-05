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

        [Route("by-performance-level")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByPerformanceLevel(AssessmentFilterModel model)
        {
            var chart = _assessmentService.ByPerformanceLevel(model);
            return Ok(chart);
        }

        [Route("performance-level-language")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = _assessmentService.PerformanceLevelByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("performance-level-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelByEthnicity(AssessmentFilterModel model)
        {
            var chart = _assessmentService.PerformanceLevelByEthnicity(model);
            return Ok(chart);
        }

        [Route("performance-level-lunch")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelByLunchStatus(AssessmentFilterModel model)
        {
            var chart = _assessmentService.PerformanceLevelByLunchStatus(model);
            return Ok(chart);
        }

        [Route("performance-level-special")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelBySpecialEducation(AssessmentFilterModel model)
        {
            var chart = _assessmentService.PerformanceLevelBySpecialEducation(model);
            return Ok(chart);
        }
    }
}
