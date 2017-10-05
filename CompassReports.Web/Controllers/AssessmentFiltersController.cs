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
    [RoutePrefix("api/assessment-filters")]
    public class AssessmentFiltersController : ApiController
    {
        private readonly IAssessmentFiltersService _assessmentFiltersService;
        public AssessmentFiltersController(IAssessmentFiltersService assessmentFiltersService)
        {
            _assessmentFiltersService = assessmentFiltersService;
        }
        [Route("assessments")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAssessments()
        {
            var assessments = _assessmentFiltersService.GetAssessments();
            return Ok(assessments);
        }

        [Route("english-learner-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetEnglishLanguageLearnerStatuses()
        {
            var statuses = _assessmentFiltersService.GetEnglishLanguageLearnerStatuses();
            return Ok(statuses);
        }

        [Route("ethnicities")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetEthnicities()
        {
            var ethnicites = _assessmentFiltersService.GetEthnicities();
            return Ok(ethnicites);
        }

        [Route("good-cause-excemptions")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGoodCauseExcemptions(string assessmentTitle, string subject)
        {
            var statuses = _assessmentFiltersService.GetGoodCauseExcemptions(assessmentTitle, subject);
            return Ok(statuses);
        }

        [Route("grades")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGrades(string assessmentTitle, string subject)
        {
            var grades = _assessmentFiltersService.GetGrades(assessmentTitle, subject);
            return Ok(grades);
        }

        [Route("lunch-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetLunchStatuses()
        {
            var statuses = _assessmentFiltersService.GetLunchStatuses();
            return Ok(statuses);
        }

        [Route("performance-levels")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPerformanceLevels(string assessmentTitle, string subject)
        {
            var levels = _assessmentFiltersService.GetPerformanceLevels(assessmentTitle, subject);
            return Ok(levels);
        }

        [Route("special-education-statuses")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSpecialEducationStatuses()
        {
            var statuses = _assessmentFiltersService.GetSpecialEducationStatuses();
            return Ok(statuses);
        }

        [Route("subjects")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSubjects(string assessmentTitle)
        {
            var statuses = _assessmentFiltersService.GetSubjects(assessmentTitle);
            return Ok(statuses);
        }

        [Route("school-years")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSchoolYears(string assessmentTitle, string subject)
        {
            var years = _assessmentFiltersService.GetSchoolYears(assessmentTitle, subject);
            return Ok(years);
        }
    }
}
