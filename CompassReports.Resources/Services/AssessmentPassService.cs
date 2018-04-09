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
    public interface IAssessmentPassService
    {
        Task<PieChartModel<int>> Get(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model);
    }

    public class AssessmentPassService : IAssessmentPassService
    {
        private readonly IAssessmentPerformanceService _assessmentPerformanceService;
        private readonly IRepository<AssessmentFact> _assessmentRepository;

        public AssessmentPassService(IAssessmentPerformanceService assessmentPerformanceService,
            IRepository<AssessmentFact> assessmentRepository)
        {
            _assessmentPerformanceService = assessmentPerformanceService;
            _assessmentRepository = assessmentRepository;
        }

        public async Task<PieChartModel<int>> Get(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceService.Get(model);
            chart.Title = "Passing " + model.AssessmentTitle + " Exam";
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceService.ByEnglishLanguageLearner(model);
            chart.Title = "Passing " + model.AssessmentTitle + " Exam By English Language Learner";
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> ByEthnicity(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceService.ByEthnicity(model);
            chart.Title = "Passing " + model.AssessmentTitle + " Exam By Ethnicity";
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> ByLunchStatus(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceService.ByLunchStatus(model);
            chart.Title = "Passing " + model.AssessmentTitle + " Exam By Free/Reduced Meal Status";
            chart.HideTotal = true;
            return chart;
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(AssessmentFilterModel model)
        {
            model.ExcludePerformanceKeys = await GetPerformanceKeys(model);
            var chart = await _assessmentPerformanceService.BySpecialEducation(model);
            chart.Title = "Passing " + model.AssessmentTitle + " Exam By Special Eduaction";
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
