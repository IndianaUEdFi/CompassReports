using System;
using System.Data.SqlClient;
using System.Net.Http;
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
        public IHttpActionResult GetEnglishLanguageLearnerStatuses()
        {
            var statuses = _demographicFiltersService.GetEnglishLanguageLearnerStatuses();
            return Ok(statuses);
        }

        [Route("ethnicities")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetEthnicities()
        {
            var ethnicites = _demographicFiltersService.GetEthnicities();
            return Ok(ethnicites);
        }

        [Route("grades")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGrades()
        {
            var grades = _demographicFiltersService.GetGrades();
            return Ok(grades);
        }

        [Route("lunch-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetLunchStatuses()
        {
            var statuses = _demographicFiltersService.GetLunchStatuses();
            return Ok(statuses);
        }

        [Route("special-education-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSpecialEducationStatuses()
        {
            var statuses = _demographicFiltersService.GetSpecialEducationStatuses();
            return Ok(statuses);
        }
    }
}
