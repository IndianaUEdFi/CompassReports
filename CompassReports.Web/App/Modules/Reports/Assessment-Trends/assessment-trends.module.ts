/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports.AssessmentTrends {
    import PercentageTotalBarChartModel = Models.PercentageTotalBarChartModel;

    const DefaultAssessmentTitle = 'ISTAR';
    const DefaultSubject = 'English/Language Arts Only';

    interface IAssessmentTrendParams {
        assessmentTitle: string
    }

    function onSubjectChange(model: Models.AssessmentFilterModel, filters: Models.FilterModel<any>[], api: IApi) {
        if (model.AssessmentTitle && model.Subject) {

            model.SchoolYear = null;
            model.Assessments = [];

            filters[2].update([] as Models.FilterValueModel[]);

            api.assessmentFilters.getSchoolYears(model.AssessmentTitle, model.Subject).then((schoolYears: Models.FilterValueModel[]) => {
                filters[1].update(schoolYears);
                model.SchoolYear = schoolYears[0].Value as number;
            });

            api.assessmentFilters.getGrades(model.AssessmentTitle, model.Subject).then((grades: Models.FilterValueModel[]) => {
                filters[2].update(grades);
                if (grades.length === 1) {
                    model.Assessments = [grades[0].Value as number];
                }
            });
        }
    }

    class AssessmentTrendsReportView extends ReportBaseView {
        resolve = {
            report: ['$rootScope', '$stateParams', 'subjects',
                'schoolYears', 'grades', 'englishLanguageLearnerStatuses',
                'ethnicities', 'lunchStatuses', 'performanceLevels',
                'specialEducationStatuses',
                ($rootScope: IAppRootScope, $stateParams: IAssessmentTrendParams, subjects: string[],
                    schoolYears: Models.FilterValueModel[], grades: Models.FilterValueModel[], englishLanguageLearnerStatuses: string[],
                    ethnicities: string[], lunchStatuses: string[], performanceLevels: Models.FilterValueModel[],
                    specialEducationStatuses: string[]) => {

                    var filters = [
                        new Models.FilterModel<number>(subjects, 'Subject', 'Subject', false, true, onSubjectChange),
                        new Models.FilterModel<number>(schoolYears, 'School Years', 'SchoolYears', true),
                        new Models.FilterModel<number>(grades, 'Grades', 'Assessments', true, true),
                        new Models.FilterModel<number>(ethnicities, 'Ethnicities', 'Ethnicities', true),
                        new Models.FilterModel<number>(lunchStatuses, 'Meal Plans', 'LunchStatuses', true),
                        new Models.FilterModel<number>(specialEducationStatuses, 'Education Types', 'SpecialEducationStatuses', true),
                        new Models.FilterModel<number>(englishLanguageLearnerStatuses, 'Language Learners', 'EnglishLanguageLearnerStatuses', true)
                    ];

                    var charts = [
                        new PercentageTotalBarChartModel('assessmentPerformanceTrend', 'get')
                    ];

                    angular.forEach(performanceLevels, (level) => {
                        charts.push(new PercentageTotalBarChartModel(
                            'assessmentPerformanceTrend',
                            'get',
                            {
                                name: 'app.reports.assessment-performance-trend',
                                parameters: {
                                    assessmentTitle: $stateParams.assessmentTitle,
                                    performanceKey: level.Value
                                }
                            },
                            { PerformanceKey: level.Value }));
                    });

                    var model = new Models.AssessmentFilterModel();
                    model.AssessmentTitle = $stateParams.assessmentTitle || DefaultAssessmentTitle;

                    if ($rootScope.filterModel) {
                        model = $rootScope.filterModel as Models.AssessmentFilterModel;
                        $rootScope.filterModel = null;
                    } else {
                        model.Subject = ($stateParams.assessmentTitle) ? ((subjects && subjects.length) ? subjects[0] : null) : DefaultSubject;
                        model.Assessments = (grades && grades.length === 1) ? [grades[0].Value as number] : [];
                    }

                    let backState = null;
                    let backParameters = null;

                    if ($rootScope.backState) {
                        backState = $rootScope.backState;
                        backParameters = $rootScope.backParameters;
                        $rootScope.backState = null;
                        $rootScope.backParameters = null;

                    }

                    return {
                        filters: filters,
                        charts: charts,
                        title: $stateParams.assessmentTitle || DefaultAssessmentTitle,
                        model: model,
                        backState: backState,
                        backParameters: backParameters
                    }
                }],
            subjects: ['$stateParams', 'api', ($stateParams: IAssessmentTrendParams, api: IApi) => {
                var assessmentTitle = $stateParams.assessmentTitle || DefaultAssessmentTitle;
                return api.assessmentFilters.getSubjects(assessmentTitle);
            }],
            schoolYears: ['$stateParams', 'api', 'subjects', ($stateParams: IAssessmentTrendParams, api: IApi, subjects: string[]) => {
                if ($stateParams.assessmentTitle) {
                    return (subjects && subjects.length) ? api.assessmentFilters.getSchoolYears($stateParams.assessmentTitle, subjects[0]) : [];
                } else {
                    return api.assessmentFilters.getSchoolYears(DefaultAssessmentTitle, DefaultSubject);
                }
            }],
            grades: ['$stateParams', 'api', 'subjects', ($stateParams: IAssessmentTrendParams, api: IApi, subjects: string[]) => {
                if ($stateParams.assessmentTitle) {
                    return (subjects && subjects.length) ? api.assessmentFilters.getGrades($stateParams.assessmentTitle, subjects[0]) : [];
                } else {
                    return api.assessmentFilters.getGrades(DefaultAssessmentTitle, DefaultSubject);
                }
            }],
            performanceLevels: ['$stateParams', 'api', ($stateParams: IAssessmentTrendParams, api: IApi) => {
                if ($stateParams.assessmentTitle) {
                    return api.assessmentFilters.getPerformanceLevels($stateParams.assessmentTitle);
                } else {
                    return api.assessmentFilters.getPerformanceLevels(DefaultAssessmentTitle);
                }
            }],
            englishLanguageLearnerStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getEnglishLanguageLearnerStatuses();
            }],
            ethnicities: ['api', (api: IApi) => {
                return api.demographicFilters.getEthnicities();
            }],
            lunchStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getLunchStatuses();
            }],
            specialEducationStatuses: ['api', (api: IApi) => {
                return api.demographicFilters.getSpecialEducationStatuses();
            }]
        }
    }

    class AssessmentTrendsConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.assessment-trends',
                {
                    url: '/assessment-trends?assessmentTitle',
                    views: {
                        'report@app.reports': new AssessmentTrendsReportView(settings)
                    }
                });
        }
    }

    angular
        .module('app.reports.assessment-trends', ['app.reports.assessment-trends.performance'])
        .config(AssessmentTrendsConfig);
}