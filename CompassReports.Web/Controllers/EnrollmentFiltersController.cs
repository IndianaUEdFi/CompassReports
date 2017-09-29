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
    [RoutePrefix("api/enrollment-filters")]
    public class EnrollmentFiltersController : ApiController
    {
        private readonly IEnrollmentFiltersService _enrollmentFiltersService;
        public EnrollmentFiltersController(IEnrollmentFiltersService enrollmentFiltersService)
        {
            _enrollmentFiltersService = enrollmentFiltersService;
        }
        [Route("english-learner-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetEnglishLanguageLearnerStatuses()
        {
            var statuses = _enrollmentFiltersService.GetEnglishLanguageLearnerStatuses();
            return Ok(statuses);
        }

        [Route("ethnicities")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetEthnicities()
        {
            var ethnicites = _enrollmentFiltersService.GetEthnicities();
            return Ok(ethnicites);
        }

        [Route("grades")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGrades()
        {
            var grades = _enrollmentFiltersService.GetGrades();
            return Ok(grades);
        }

        [Route("lunch-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetLunchStatuses()
        {
            var statuses = _enrollmentFiltersService.GetLunchStatuses();
            return Ok(statuses);
        }

        [Route("special-education-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSpecialEducationStatuses()
        {
            var statuses = _enrollmentFiltersService.GetSpecialEducationStatuses();
            return Ok(statuses);
        }

        [Route("school-years")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSchoolYears()
        {
            var years = _enrollmentFiltersService.GetSchoolYears();
            return Ok(years);
        }
    }
}
