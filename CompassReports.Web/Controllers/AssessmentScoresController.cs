using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment-scores")]
    public class AssessmentScoresController : ApiController
    {
        private readonly IAssessmentScoresService _assessmentScoresService;

        public AssessmentScoresController(IAssessmentScoresService assessmentScoresService)
        {
            _assessmentScoresService = assessmentScoresService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(AssessmentFilterModel model)
        {
            var chart = _assessmentScoresService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = _assessmentScoresService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(AssessmentFilterModel model)
        {
            var chart = _assessmentScoresService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(AssessmentFilterModel model)
        {
            var chart = _assessmentScoresService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(AssessmentFilterModel model)
        {
            var chart = _assessmentScoresService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
