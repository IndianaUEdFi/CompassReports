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
    public interface IAttendanceService
    {
        Task<BarChartModel<double>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        Task<BarChartModel<double>> ByEthnicity(EnrollmentFilterModel model);
        Task<BarChartModel<double>> ByGrade(EnrollmentFilterModel model);
        Task<BarChartModel<double>> ByLunchStatus(EnrollmentFilterModel model);
        Task<BarChartModel<double>> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceFactService _attendanceFactService;

        public AttendanceService(IAttendanceFactService attendanceFactService)
        {
            _attendanceFactService = attendanceFactService;
        }

        public async Task<BarChartModel<double>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.EnglishLanguageLearnerStatus,
                    SortOrder = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _attendanceFactService.CreateChart(groupings, "English Language Learner Status", "English Language Leaner Statuses", "English Language Learner");
        }

        public async Task<BarChartModel<double>> ByEthnicity(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.Ethnicity,
                    SortOrder = x.Demographic.Ethnicity
                });

            return await _attendanceFactService.CreateChart(groupings, "Ethnicity", "Ethncities", "Ethnicity");
        }

        public async Task<BarChartModel<double>> ByGrade(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.GradeLevel,
                    SortOrder = x.Demographic.GradeLevelSort
                });

            return await _attendanceFactService.CreateChart(groupings, "Grade", "Grades", "Grade");
        }

        public async Task<BarChartModel<double>> ByLunchStatus(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.FreeReducedLunchStatus,
                    SortOrder = x.Demographic.FreeReducedLunchStatus
                });

            return await _attendanceFactService.CreateChart(groupings, "Lunch Status", "Lunch Statuses", "Free/Reduced Price Meals");
        }

        public async Task<BarChartModel<double>> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var groupings = _attendanceFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.SpecialEducationStatus,
                    SortOrder = x.Demographic.SpecialEducationStatus,
                });

            return await _attendanceFactService.CreateChart(groupings, "Special Education Status", "Special Education Statuses", "Special Education");
        }
    }
}
