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
    [RoutePrefix("api/enrollment")]
    public class EnrollmentController : ApiController
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [Route("by-english-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var chart = _enrollmentService.ByEnglishLanguageLearnerStatus(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(EnrollmentFilterModel model)
        {
            var chart = _enrollmentService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-grade")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByGrade(EnrollmentFilterModel model)
        {
            var chart = _enrollmentService.ByGrade(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(EnrollmentFilterModel model)
        {
            var chart = _enrollmentService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var chart = _enrollmentService.BySpecialEducationStatus(model);
            return Ok(chart);
        }
    }
}
