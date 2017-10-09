using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/graduate-waivers")]
    public class GraduateWaiversController : ApiController
    {
        private readonly IGraduateWaiversService _graduateWaiversService;
        public GraduateWaiversController(IGraduateWaiversService graduateWaiversService)
        {
            _graduateWaiversService = graduateWaiversService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(GraduateFilterModel model)
        {
            var chart = _graduateWaiversService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(GraduateFilterModel model)
        {
            var chart = _graduateWaiversService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(GraduateFilterModel model)
        {
            var chart = _graduateWaiversService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(GraduateFilterModel model)
        {
            var chart = _graduateWaiversService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(GraduateFilterModel model)
        {
            var chart = _graduateWaiversService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
