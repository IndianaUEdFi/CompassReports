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
    public interface IGraduateWaiversService
    {
        Task<PieChartModel<int>> Get(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(GraduateFilterModel model);
    }

    public class GraduateWaiversService : IGraduateWaiversService
    {
        private readonly IGraduationFactService _graduationFactService;

        public GraduateWaiversService(IGraduationFactService graduationFactService)
        {
            _graduationFactService = graduationFactService;
        }

        public async Task<PieChartModel<int>> Get(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => x.GraduationStatus.GraduationWaiver);

            return await _graduationFactService.CreateBaseChart(groupings, "Graduation Waiver");
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationWaiver,
                    Property = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _graduationFactService.CreateChart(groupings, "Graduation Waiver", "Language Statuses", "English Language Learners");
        }

        public async Task<PercentageTotalBarChartModel> ByEthnicity(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationWaiver,
                    Property = x.Demographic.Ethnicity
                });

            return await _graduationFactService.CreateChart(groupings, "Graduation Waiver", "Ethnicities", "Ethnicity");
        }

        public async Task<PercentageTotalBarChartModel> ByLunchStatus(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationWaiver,
                    Property = x.Demographic.FreeReducedLunchStatus
                });

            return await _graduationFactService.CreateChart(groupings, "DGraduation Waiver", "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.GraduationWaiver,
                    Property = x.Demographic.SpecialEducationStatus
                });

            return await _graduationFactService.CreateChart(groupings, "Graduation Waiver", "Education Statuses", "Special Education");
        }
    }
}
