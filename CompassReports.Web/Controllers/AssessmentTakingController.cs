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
        public async Task<IHttpActionResult> Get(AssessmentFilterModel model)
        {
            var chart = await _assessmentTakingService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = await _assessmentTakingService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEthnicity(AssessmentFilterModel model)
        {
            var chart = await _assessmentTakingService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByLunchStatus(AssessmentFilterModel model)
        {
            var chart = await _assessmentTakingService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> BySpecialEducation(AssessmentFilterModel model)
        {
            var chart = await _assessmentTakingService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
