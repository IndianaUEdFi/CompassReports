using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentTakingService
    {
        PieChartModel<int> Get(AssessmentFilterModel model);
        PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentFilterModel model);
        PercentageTotalBarChartModel ByEthnicity(AssessmentFilterModel model);
        PercentageTotalBarChartModel ByLunchStatus(AssessmentFilterModel model);
        PercentageTotalBarChartModel BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentTakingService : IAssessmentTakingService
    {
        private readonly IAssessmentPerformanceService _assessmentPerformanceService;
        private readonly IRepository<AssessmentFact> _assessmentRepository;

        public AssessmentTakingService(IAssessmentPerformanceService assessmentPerformanceService,
            IRepository<AssessmentFact> assessmentRepository)
        {
            _assessmentPerformanceService = assessmentPerformanceService;
            _assessmentRepository = assessmentRepository;
        }

        public PieChartModel<int> Get(AssessmentFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart =  _assessmentPerformanceService.Get(model);
            chart.Title = "Taking " + model.AssessmentTitle + " Exam";
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceService.ByEnglishLanguageLearner(model);
            chart.Title = "Taking " + model.AssessmentTitle + " Exam By English Language Learner";
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel ByEthnicity(AssessmentFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceService.ByEthnicity(model);
            chart.Title = "Taking " + model.AssessmentTitle + " Exam By Ethnicity";
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel ByLunchStatus(AssessmentFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceService.ByLunchStatus(model);
            chart.Title = "Taking " + model.AssessmentTitle + " Exam By Free/Reduced Meal Status";
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel BySpecialEducation(AssessmentFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceService.BySpecialEducation(model);
            chart.Title = "Taking " + model.AssessmentTitle + " Exam By Special Eduaction";
            chart.HideTotal = true;
            return chart;
        }

        private List<int> GetPerformanceKeys(AssessmentFilterModel model)
        {
            return _assessmentRepository
                .GetAll()
                .Where(x => x.Assessment.AssessmentTitle == model.AssessmentTitle && x.Assessment.AcademicSubject == model.Subject &&
                    x.Performance.PerformanceLevel.ToLower().Contains("took") || x.Performance.PerformanceLevel.ToLower().Contains("did not take"))
                .Select(x => x.PerformanceKey)
                .Distinct()
                .ToList();
        }
    }
}
