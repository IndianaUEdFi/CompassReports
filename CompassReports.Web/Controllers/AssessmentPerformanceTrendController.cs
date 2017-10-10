using System;
using System.Web.Http;
using CompassReports.Resources.Models;
using CompassReports.Resources.Services;

namespace CompassReports.Web.Controllers
{
    /// <summary>
    /// The EnrollmentFilters resource endpoint.
    /// </summary>
    [RoutePrefix("api/assessment-performance-trend")]
    public class AssessmentPerformanceTrendController : ApiController
    {
        private readonly IAssessmentPerformanceTrendService _assessmentPerformanceTrendService;

        public AssessmentPerformanceTrendController(IAssessmentPerformanceTrendService assessmentPerformanceTrendService)
        {
            _assessmentPerformanceTrendService = assessmentPerformanceTrendService;
        }

        [Route("")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Get(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPerformanceTrendService.Get(model);
            return Ok(chart);
        }


        [Route("by-language-learner")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPerformanceTrendService.ByEnglishLanguageLearner(model);
            return Ok(chart);
        }

        [Route("by-ethnicity")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByEthnicity(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPerformanceTrendService.ByEthnicity(model);
            return Ok(chart);
        }

        [Route("by-lunch-status")]
        [AcceptVerbs("POST")]
        public IHttpActionResult ByLunchStatus(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPerformanceTrendService.ByLunchStatus(model);
            return Ok(chart);
        }

        [Route("by-special-education")]
        [AcceptVerbs("POST")]
        public IHttpActionResult BySpecialEducation(AssessmentTrendFilterModel model)
        {
            var chart = _assessmentPerformanceTrendService.BySpecialEducation(model);
            return Ok(chart);
        }
    }
}
