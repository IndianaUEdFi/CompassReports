using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/demographic-filters")]
    public class DemographicFiltersController : ApiController
    {
        private readonly IDemographicFiltersService _demographicFiltersService;
        public DemographicFiltersController(IDemographicFiltersService demographicFiltersService)
        {
            _demographicFiltersService = demographicFiltersService;
        }
        [Route("english-learner-statuses")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetEnglishLanguageLearnerStatuses()
        {
            var statuses = await _demographicFiltersService.GetEnglishLanguageLearnerStatuses();
            return Ok(statuses);
        }

        [Route("ethnicities")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetEthnicities()
        {
            var ethnicites = await _demographicFiltersService.GetEthnicities();
            return Ok(ethnicites);
        }

        [Route("grades")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetGrades()
        {
            var grades = await _demographicFiltersService.GetGrades();
            return Ok(grades);
        }

        [Route("lunch-statuses")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetLunchStatuses()
        {
            var statuses = await _demographicFiltersService.GetLunchStatuses();
            return Ok(statuses);
        }

        [Route("special-education-statuses")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetSpecialEducationStatuses()
        {
            var statuses = await _demographicFiltersService.GetSpecialEducationStatuses();
            return Ok(statuses);
        }
    }
}
