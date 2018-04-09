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
    [RoutePrefix("api/graduate-diploma-type")]
    public class GraduateDiplomaTypeController : ApiController
    {
        private readonly IGraduateDiplomaTypeService _graduateDiplomaTypeService;
        public GraduateDiplomaTypeController(IGraduateDiplomaTypeService graduateDiplomaTypeService)
        {
            _graduateDiplomaTypeService = graduateDiplomaTypeService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> Get(GraduateFilterModel model)
        {
            var chart = await _graduateDiplomaTypeService.Get(model);
            return Ok(chart);
        }

        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEnglishLanguageLearner(GraduateFilterModel model)
        {
            var chart = await _graduateDiplomaTypeService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByEthnicity(GraduateFilterModel model)
        {
            var chart = await _graduateDiplomaTypeService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> ByLunchStatus(GraduateFilterModel model)
        {
            var chart = await _graduateDiplomaTypeService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> BySpecialEducation(GraduateFilterModel model)
        {
            var chart = await _graduateDiplomaTypeService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
