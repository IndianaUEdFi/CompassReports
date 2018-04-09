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
    public interface IAttendanceTrendsService
    {
        Task<LineChartModel<double>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        Task<LineChartModel<double>> ByEthnicity(EnrollmentFilterModel model);
        Task<LineChartModel<double>> ByGrade(EnrollmentFilterModel model);
        Task<LineChartModel<double>> ByLunchStatus(EnrollmentFilterModel model);
        Task<LineChartModel<double>> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class AttendanceTrendsService : IAttendanceTrendsService
    {
        private readonly IAttendanceFactService _attendanceFactService;

        public AttendanceTrendsService(IAttendanceFactService attendanceFactService)
        {
            _attendanceFactService = attendanceFactService;
        }


        public async Task<LineChartModel<double>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.EnglishLanguageLearnerStatus,
                    SortOrder = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _attendanceFactService.CreateTrendChart(groupings, "English Language Learner Statuses", "English Language Learner");
        }

        public async Task<LineChartModel<double>> ByEthnicity(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.Ethnicity,
                    SortOrder = x.Demographic.Ethnicity
                });

            return await _attendanceFactService.CreateTrendChart(groupings, "Ethnicities", "Ethnicity");
        }

        public async Task<LineChartModel<double>> ByGrade(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.GradeLevel,
                    SortOrder = x.Demographic.GradeLevelSort
                });

            return await _attendanceFactService.CreateTrendChart(groupings, "Grades", "Grade");
        }

        public async Task<LineChartModel<double>> ByLunchStatus(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.FreeReducedLunchStatus,
                    SortOrder = x.Demographic.FreeReducedLunchStatus
                });

            return await _attendanceFactService.CreateTrendChart(groupings, "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<LineChartModel<double>> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentTrendGroupByModel
                {
                    SchoolYear = x.SchoolYearKey,
                    SchoolYearDescription = x.SchoolYearDimension.SchoolYearDescription,
                    Property = x.Demographic.SpecialEducationStatus,
                    SortOrder = x.Demographic.SpecialEducationStatus
                });

            return await _attendanceFactService.CreateTrendChart(groupings, "Special Education Statuses", "Special Education");
        }
    }
}
