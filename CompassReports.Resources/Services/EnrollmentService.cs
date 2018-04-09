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
    public interface IEnrollmentService
    {
        Task<PieChartModel<int>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model);
        Task<PieChartModel<int>> ByEthnicity(EnrollmentFilterModel model);
        Task<PieChartModel<int>> ByGrade(EnrollmentFilterModel model);
        Task<PieChartModel<int>> ByLunchStatus(EnrollmentFilterModel model);
        Task<PieChartModel<int>> BySpecialEducationStatus(EnrollmentFilterModel model);
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentFactService _enrollmentFactService;

        public EnrollmentService(IEnrollmentFactService enrollmentFactService)
        {
            _enrollmentFactService = enrollmentFactService;
        }

        public async Task<PieChartModel<int>> ByEnglishLanguageLearnerStatus(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.EnglishLanguageLearnerStatus,
                    SortOrder = x.Demographic.EnglishLanguageLearnerStatus
                });

            return await _enrollmentFactService.CreateChart(groupings, "English Language Learner Status", "English Language Learner");
        }

        public async Task<PieChartModel<int>> ByEthnicity(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.Ethnicity,
                    SortOrder = x.Demographic.Ethnicity
                });

            return await _enrollmentFactService.CreateChart(groupings, "Ethnicity", "Ethnicity");
        }
        public async Task<PieChartModel<int>> ByGrade(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.GradeLevel,
                    SortOrder = x.Demographic.GradeLevelSort
                });

            return await _enrollmentFactService.CreateChart(groupings, "Grade", "Grade");
        }

        public async Task<PieChartModel<int>> ByLunchStatus(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.FreeReducedLunchStatus,
                    SortOrder = x.Demographic.FreeReducedLunchStatus
                });

            return await _enrollmentFactService.CreateChart(groupings, "Lunch Status", "Free/Reduced Price Meals");
        }

        public async Task<PieChartModel<int>> BySpecialEducationStatus(EnrollmentFilterModel model)
        {
            var groupings = _enrollmentFactService.BaseQuery(model)
                .GroupBy(x => new EnrollmentGroupByModel
                {
                    Property = x.Demographic.SpecialEducationStatus,
                    SortOrder = x.Demographic.SpecialEducationStatus
                });

            return await _enrollmentFactService.CreateChart(groupings, "Special Education Status", "Sepcial Education");
        }
    }
}
