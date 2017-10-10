using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentTakingTrendService
    {
        PercentageTotalBarChartModel Get(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel ByEthnicity(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel ByLunchStatus(AssessmentTrendFilterModel model);
        PercentageTotalBarChartModel BySpecialEducation(AssessmentTrendFilterModel model);
    }

    public class AssessmentTakingTrendService : IAssessmentTakingTrendService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;
        private readonly IAssessmentPerformanceTrendService _assessmentPerformanceTrendService;

        public AssessmentTakingTrendService(IRepository<AssessmentFact> assessmentRepository,
            IAssessmentPerformanceTrendService assessmentPerformanceTrendService)
        {
            _assessmentRepository = assessmentRepository;
            _assessmentPerformanceTrendService = assessmentPerformanceTrendService;
        }

        public PercentageTotalBarChartModel Get(AssessmentTrendFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceTrendService.Get(model);
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel ByEnglishLanguageLearner(AssessmentTrendFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceTrendService.ByEnglishLanguageLearner(model);
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel ByEthnicity(AssessmentTrendFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceTrendService.ByEthnicity(model);
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel ByLunchStatus(AssessmentTrendFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceTrendService.ByLunchStatus(model);
            chart.HideTotal = true;
            return chart;
        }

        public PercentageTotalBarChartModel BySpecialEducation(AssessmentTrendFilterModel model)
        {
            model.PerformanceKeys = GetPerformanceKeys(model);
            var chart = _assessmentPerformanceTrendService.BySpecialEducation(model);
            chart.HideTotal = true;
            return chart;
        }

        private List<int> GetPerformanceKeys(AssessmentTrendFilterModel model)
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
