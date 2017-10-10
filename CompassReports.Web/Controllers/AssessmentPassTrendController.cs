using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment-pass-trend")]
    public class AssessmentPassTrendController : ApiController
    {
        private readonly IAssessmentPassTrendService _assessmentPassTrendService;

        public AssessmentPassTrendController(IAssessmentPassTrendService assessmentPassTrendService)
        {
            _assessmentPassTrendService = assessmentPassTrendService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPassTrendService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPassTrendService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPassTrendService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPassTrendService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPassTrendService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
