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
        public async Task<IHttpActionResult> GetAssessments()
        {
            var assessments = await _assessmentFiltersService.GetAssessments();
            return Ok(assessments);
        }

        [Route("good-cause-excemptions")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetGoodCauseExcemptions(string assessmentTitle, string subject)
        {
            var statuses = await _assessmentFiltersService.GetGoodCauseExcemptions(assessmentTitle, subject);
            return Ok(statuses);
        }

        [Route("grades")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetGrades(string assessmentTitle, string subject)
        {
            var grades = await _assessmentFiltersService.GetGrades(assessmentTitle, subject);
            return Ok(grades);
        }

        [Route("performance-levels")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetPerformanceLevels(string assessmentTitle, string subject = null)
        {
            var levels = await _assessmentFiltersService.GetPerformanceLevels(assessmentTitle, subject);
            return Ok(levels);
        }

        [Route("subjects")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetSubjects(string assessmentTitle)
        {
            var statuses = await _assessmentFiltersService.GetSubjects(assessmentTitle);
            return Ok(statuses);
        }

        [Route("school-years")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetSchoolYears(string assessmentTitle, string subject)
        {
            var years = await _assessmentFiltersService.GetSchoolYears(assessmentTitle, subject);
            return Ok(years);
        }
    }
}
