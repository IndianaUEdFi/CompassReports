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
    [RoutePrefix("api/assessment-performance-trend")]
    public class AssessmentPerformanceTrendController : ApiController
    {
        private readonly IAssessmentPerformanceTrendService _assessmentPerformanceTrendService;

        public AssessmentPerformanceTrendController(IAssessmentPerformanceTrendService assessmentPerformanceTrendService)
        {
            _assessmentPerformanceTrendService = assessmentPerformanceTrendService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> Get(AssessmentFilterModel model)
        {
            var chart = await _assessmentPerformanceTrendService.Get(model);
            return Ok(chart);
        }


        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            var chart = await _assessmentPerformanceTrendService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEthnicity(AssessmentFilterModel model)
        {
            var chart = await _assessmentPerformanceTrendService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByLunchStatus(AssessmentFilterModel model)
        {
            var chart = await _assessmentPerformanceTrendService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> BySpecialEducation(AssessmentFilterModel model)
        {
            var chart = await _assessmentPerformanceTrendService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
