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
    public interface IGraduateDiplomaTypeService
    {
        Task<PieChartModel<int>> Get(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByEthnicity(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByLunchStatus(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> BySpecialEducation(GraduateFilterModel model);
    }

    public class GraduateDiplomaTypeService : IGraduateDiplomaTypeService
    {
        private readonly IGraduationFactService _graduationFactService;

        public GraduateDiplomaTypeService(IGraduationFactService graduationFactService)
        {
            _graduationFactService = graduationFactService;
        }


        public async Task<PieChartModel<int>> Get(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.DiplomaType != "Not Applicable")
                .GroupBy(x => x.GraduationStatus.DiplomaType);

            return await _graduationFactService.CreateBaseChart(groupings, "Diploma Type");
        }

        public async Task<PercentageTotalBarChartModel> ByEnglishLanguageLearner(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.DiplomaType != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.DiplomaType,
                    Property = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _graduationFactService.CreateChart(groupings, "Diploma Type", "Language Statuses", "English Language Learners");
        }
   
        public async Task<PercentageTotalBarChartModel> ByEthnicity(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.DiplomaType != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.DiplomaType,
                    Property = x.Demographic.Ethnicity
                });

            return await _graduationFactService.CreateChart(groupings, "Diploma Type", "Ethnicities", "Ethnicity");
        }
  
        public async Task<PercentageTotalBarChartModel> ByLunchStatus(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.DiplomaType != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.DiplomaType,
                    Property = x.Demographic.FreeReducedLunchStatus
                });

            return await _graduationFactService.CreateChart(groupings, "Diploma Type", "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<PercentageTotalBarChartModel> BySpecialEducation(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.DiplomaType != "Not Applicable")
                .GroupBy(x => new GraduateGroupByModel
                {
                    ChartGroupBy = x.GraduationStatus.DiplomaType,
                    Property = x.Demographic.SpecialEducationStatus
                });

            return await _graduationFactService.CreateChart(groupings, "Diploma Type", "Education Statuses", "Special Education");
        }
    }
}
