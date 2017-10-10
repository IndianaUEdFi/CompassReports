using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment-taking-trend")]
    public class AssessmentTakingTrendController : ApiController
    {
        private readonly IAssessmentTakingTrendService _assessmentTakingTrendService;

        public AssessmentTakingTrendController(IAssessmentTakingTrendService assessmentTakingTrendService)
        {
            _assessmentTakingTrendService = assessmentTakingTrendService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentTakingTrendService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentTakingTrendService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentTakingTrendService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentTakingTrendService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentTakingTrendService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
