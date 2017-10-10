using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment-pass")]
    public class AssessmentPassController : ApiController
    {
        private readonly IAssessmentPassService _assessmentPassService;

        public AssessmentPassController(IAssessmentPassService assessmentPassService)
        {
            _assessmentPassService = assessmentPassService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(AssessmentFilterModel model)
        {
            var chart = _assessmentPassService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = _assessmentPassService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(AssessmentFilterModel model)
        {
            var chart = _assessmentPassService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(AssessmentFilterModel model)
        {
            var chart = _assessmentPassService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(AssessmentFilterModel model)
        {
            var chart = _assessmentPassService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
