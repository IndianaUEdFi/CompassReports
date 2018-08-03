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
        public async Task<IHttpActionResult> Get(AssessmentFilterModel model)
        {
            var chart = await _assessmentPassTrendService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = await _assessmentPassTrendService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEthnicity(AssessmentFilterModel model)
        {
            var chart = await _assessmentPassTrendService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByLunchStatus(AssessmentFilterModel model)
        {
            var chart = await _assessmentPassTrendService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> BySpecialEducation(AssessmentFilterModel model)
        {
            var chart = await _assessmentPassTrendService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
