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
    [RoutePrefix("api/enrollment-trends")]
    public class EnrollmentTrendsController : ApiController
    {
        private readonly IEnrollmentTrendsService _enrollmentTrendsService;
        public EnrollmentTrendsController(IEnrollmentTrendsService enrollmentTrendsService)
        {
            _enrollmentTrendsService = enrollmentTrendsService;
        }

        [Route("by-english-language-learner")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var chart = await _enrollmentTrendsService.ByEnglishLanguageLearnerStatus(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEthnicity(EnrollmentFilterModel model)
        {
            var chart = await _enrollmentTrendsService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-grade")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByGrade(EnrollmentFilterModel model)
        {
            var chart = await _enrollmentTrendsService.ByGrade(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByLunchStatus(EnrollmentFilterModel model)
        {
            var chart = await _enrollmentTrendsService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var chart = await _enrollmentTrendsService.BySpecialEducationStatus(model);
            return Ok(chart);
        }
    }
}
