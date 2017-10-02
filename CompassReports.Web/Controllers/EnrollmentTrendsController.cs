using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using CompassReports.Resources.Models.Enrollment;
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
        public IHttpActionResult ByEnglishLanguageLearnerStatus(EnrollmentTrendsFilterModel model)
        {
            var chart = _enrollmentTrendsService.ByEnglishLanguageLearnerStatus(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(EnrollmentTrendsFilterModel model)
        {
            var chart = _enrollmentTrendsService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-grade")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByGrade(EnrollmentTrendsFilterModel model)
        {
            var chart = _enrollmentTrendsService.ByGrade(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(EnrollmentTrendsFilterModel model)
        {
            var chart = _enrollmentTrendsService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducationStatus(EnrollmentTrendsFilterModel model)
        {
            var chart = _enrollmentTrendsService.BySpecialEducationStatus(model);
            return Ok(chart);
        }
    }
}
