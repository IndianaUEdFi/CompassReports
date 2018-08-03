using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IGraduateStatusService
    {
        Task<PieChartModel<int>> Get(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(GraduateFilterModel model);
    }

    public class GraduateStatusService : IGraduateStatusService
    {
        private readonly IGraduationFactService _graduationFactService;

        public GraduateStatusService(IGraduationFactService graduationFactService)
        {
            _graduationFactService = graduationFactService;
        }

        public async Task<PieChartModel<int>> Get(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .GroupBy(x => x.GraduationStatus.GraduationStatus);

            return await _graduationFactService.CreateBaseChart(groupings, "Graduation Status");
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationStatus,
                    Property = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _graduationFactService.CreateChart(groupings, "Graduation Status", "Language Statuses", "English Language Learners");
        }

        public async Task<PercentageTotalBarChartModel> ByEthnicity(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationStatus,
                    Property = x.Demographic.Ethnicity
                });

            return await _graduationFactService.CreateChart(groupings, "Graduation Status", "Ethnicities", "Ethnicity");
        }

        public async Task<PercentageTotalBarChartModel> ByLunchStatus(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationStatus,
                    Property = x.Demographic.FreeReducedLunchStatus
                });

            return await _graduationFactService.CreateChart(groupings, "DGraduation Status", "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
            .GroupBy(x => new GraduateGroupByModel
            {
                ChartGroupBy = x.GraduationStatus.GraduationStatus,
                Property = x.Demographic.SpecialEducationStatus
            });

            return await _graduationFactService.CreateChart(groupings, "Graduation Status", "Education Statuses", "Special Education");
        }
    }
}
