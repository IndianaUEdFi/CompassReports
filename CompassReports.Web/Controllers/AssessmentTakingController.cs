using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment-taking")]
    public class AssessmentTakingController : ApiController
    {
        private readonly IAssessmentTakingService _assessmentTakingService;

        public AssessmentTakingController(IAssessmentTakingService assessmentTakingService)
        {
            _assessmentTakingService = assessmentTakingService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(AssessmentFilterModel model)
        {
            var chart = _assessmentTakingService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = _assessmentTakingService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(AssessmentFilterModel model)
        {
            var chart = _assessmentTakingService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(AssessmentFilterModel model)
        {
            var chart = _assessmentTakingService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(AssessmentFilterModel model)
        {
            var chart = _assessmentTakingService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
