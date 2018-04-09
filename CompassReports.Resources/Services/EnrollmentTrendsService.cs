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
    public interface IEnrollmentTrendsService
    {
        Task<BarChartModel<int>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        Task<BarChartModel<int>> ByEthnicity(EnrollmentFilterModel model);
        Task<BarChartModel<int>> ByGrade(EnrollmentFilterModel model);
        Task<BarChartModel<int>> ByLunchStatus(EnrollmentFilterModel model);
        Task<BarChartModel<int>> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class EnrollmentTrendsService : IEnrollmentTrendsService
    {
        private readonly IEnrollmentFactService _enrollmentFactService;

        public EnrollmentTrendsService(IEnrollmentFactService enrollmentFactService)
        {
            _enrollmentFactService = enrollmentFactService;
        }

 
        public async Task<BarChartModel<int>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.EnglishLanguageLearnerStatus,
                    SortOrder = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _enrollmentFactService.CreateTrendChart(groupings, "English Language Learner Statuses", "English Language Learner");
        }

        public async Task<BarChartModel<int>> ByEthnicity(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.Ethnicity,
                    SortOrder = x.Demographic.Ethnicity
                });

            return await _enrollmentFactService.CreateTrendChart(groupings, "Ethncities", "Ethnicity");
        }

        public async Task<BarChartModel<int>> ByGrade(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.GradeLevel,
                    SortOrder = x.Demographic.GradeLevelSort
                });

            return await _enrollmentFactService.CreateTrendChart(groupings, "Grades", "Grade");
        }

        public async Task<BarChartModel<int>> ByLunchStatus(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.FreeReducedLunchStatus,
                    SortOrder = x.Demographic.FreeReducedLunchStatus
                });

            return await _enrollmentFactService.CreateTrendChart(groupings, "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<BarChartModel<int>> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.SpecialEducationStatus,
                    SortOrder = x.Demographic.SpecialEducationStatus
                });

            return await _enrollmentFactService.CreateTrendChart(groupings, "Special Education Statuses", "Special Education");
        }
    }
}
