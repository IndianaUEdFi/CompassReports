using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/graduate-status")]
    public class GraduateStatusController : ApiController
    {
        private readonly IGraduateStatusService _graduatesStatusService;
        public GraduateStatusController(IGraduateStatusService graduateStatusService)
        {
            _graduatesStatusService = graduateStatusService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(GraduateFilterModel model)
        {
            var chart = _graduatesStatusService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(GraduateFilterModel model)
        {
            var chart = _graduatesStatusService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(GraduateFilterModel model)
        {
            var chart = _graduatesStatusService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(GraduateFilterModel model)
        {
            var chart = _graduatesStatusService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(GraduateFilterModel model)
        {
            var chart = _graduatesStatusService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
