using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IAssessmentPassTrendService
    {
        Task<PercentageTotalBarChartModel> Get(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentPassTrendService : IAssessmentPassTrendService
    {
        private readonly IRepository<AssessmentFact> _assessmentRepository;
        private readonly IAssessmentPerformanceTrendService _assessmentPerformanceTrendService;

        public AssessmentPassTrendService(IRepository<AssessmentFact> assessmentRepository,
            IAssessmentPerformanceTrendService assessmentPerformanceTrendService)
        {
            _assessmentRepository = assessmentRepository;
            _assessmentPerformanceTrendService = assessmentPerformanceTrendService;
        }

        public async Task<PercentageTotalBarChartModel> Get(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceTrendService.Get(model);
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceTrendService.ByEnglishLanguageLearner(model);
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceTrendService.ByEthnicity(model);
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceTrendService.ByLunchStatus(model);
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceTrendService.BySpecialEducation(model);
            chart.HideTotal = true;
            return chart;
        }

        private async Task<List<int>> GetPerformanceKeys(AssessmentFilterModel model)
        {
            return await _assessmentRepository
                .GetAll()
                .Where(x => x.Assessment.AssessmentTitle == model.AssessmentTitle && x.Assessment.AcademicSubject == model.Subject &&
                    x.Performance.PerformanceLevel.ToLower().Contains("took") || x.Performance.PerformanceLevel.ToLower().Contains("did not take"))
                .Select(x => x.PerformanceKey)
                .Distinct()
                .ToListAsync();
        }
    }
}
