using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data;
using CompassReports.Data.Entities;
using CompassReports.Resources.Models;

namespace CompassReports.Resources.Services
{
    public interface IGraduateTrendsService
    {
        Task<PercentageTotalBarChartModel> ByStatus(GraduateFilterModel model);
        Task<PercentageTotalBarChartModel> ByWaiver(GraduateFilterModel model);
    }

    public class GraduateTrendsService : IGraduateTrendsService
    {
        private readonly IGraduationFactService _graduationFactService;

        public GraduateTrendsService(IGraduationFactService graduationFactService)
        {
            _graduationFactService = graduationFactService;
        }

        public async Task<PercentageTotalBarChartModel> ByStatus(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .GroupBy(x => new GraduateTrendGroupByModel {
                    ExpectedGraduationYear = x.Demographic.ExpectedGraduationYear,
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    ChartGroupBy = x.GraduationStatus.GraduationStatus
                });

            return await _graduationFactService.CreateTrendChart(groupings, model, "Graduation Status");
        }

        public async Task<PercentageTotalBarChartModel> ByWaiver(GraduateFilterModel model)
        {
            var groupings = _graduationFactService.BaseQuery(model)
                .Where(x => x.GraduationStatus.GraduationWaiver != "Not Applicable")
                .GroupBy(x => new GraduateTrendGroupByModel
                {
                    ExpectedGraduationYear = x.Demographic.ExpectedGraduationYear,
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    ChartGroupBy = x.GraduationStatus.GraduationWaiver
                });

            return await _graduationFactService.CreateTrendChart(groupings, model, "Graduation Wavier");
        }
    }
}
