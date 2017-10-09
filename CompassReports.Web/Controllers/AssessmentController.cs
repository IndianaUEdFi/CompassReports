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
        private readonly IAssessmentPerformanceService _assessmentPerformanceService;

        public AssessmentController(IAssessmentService assessmentService, IAssessmentPerformanceService assessmentPerformanceService)
        {
            _assessmentService = assessmentService;
            _assessmentPerformanceService = assessmentPerformanceService;
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
            var chart = _assessmentPerformanceService.Get(model);
            return Ok(chart);
        }

        [Route("performance-level-language")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = _assessmentPerformanceService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("performance-level-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelByEthnicity(AssessmentFilterModel model)
        {
            var chart = _assessmentPerformanceService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("performance-level-lunch")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelByLunchStatus(AssessmentFilterModel model)
        {
            var chart = _assessmentPerformanceService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("performance-level-special")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PerformanceLevelBySpecialEducation(AssessmentFilterModel model)
        {
            var chart = _assessmentPerformanceService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
