/// <reference path="../Report-Base/report-base.module.ts" />

module App.Reports {

    import ChartModel = Models.ChartModel;
    import FilterModel = Models.FilterModel;
    import FilterValueModel = Models.FilterValueModel;

    const defaultAssessmentTitle = 'ISTAR';
    const defaultAssessmentSubject = 'English/Language Arts Only';

    export interface IAssessmentParams {
        assessmentTitle: string;
        performanceKey?: string;
    }

    export class AssessmentReportView extends ReportBaseView {

        constructor(settings: ISystemSettings, multipleSchoolYears: boolean, includePerformanceLevels: boolean, charts: any[]) {
            super(settings);

            this.resolve = new AssessmentReportResolve(multipleSchoolYears, includePerformanceLevels, charts);
        }
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

    class AssessmentReportResolve extends ReportBaseResolve {

        model: any[];
        performanceLevels: any[] = [() => []];
        charts: any[] = [() => []];

        subjects: any[] = ['$stateParams', 'api', ($stateParams: IAssessmentParams, api: IApi) => {
            var assessmentTitle = $stateParams.assessmentTitle || defaultAssessmentTitle;
            return api.assessmentFilters.getSubjects(assessmentTitle);
        }];

        schoolYears: any[] = ['$stateParams', 'api', 'subjects', ($stateParams: IAssessmentParams, api: IApi, subjects: string[]) => {
            if ($stateParams.assessmentTitle) {
                return (subjects && subjects.length) ? api.assessmentFilters.getSchoolYears($stateParams.assessmentTitle, subjects[0]) : [];
            } else {
                return api.assessmentFilters.getSchoolYears(defaultAssessmentTitle, defaultAssessmentSubject);
            }
        }];

        grades: any[] = ['$stateParams', 'api', 'subjects', ($stateParams: IAssessmentParams, api: IApi, subjects: string[]) => {
            if ($stateParams.assessmentTitle) {
                return (subjects && subjects.length) ? api.assessmentFilters.getGrades($stateParams.assessmentTitle, subjects[0]) : [];
            } else {
                return api.assessmentFilters.getGrades(defaultAssessmentTitle, defaultAssessmentSubject);
            }
        }];

        filters: any[] = ['baseFilters', 'subjects', (baseFilters: FilterModel<any>[], subjects: string[]) => {
            return [new Models.FilterModel<number>(subjects, 'Subject', 'Subject', false, true, onSubjectChange)]
                .concat(baseFilters);
        }];

       report = ['$rootScope', '$stateParams', 'charts', 'filters', 'model',
           ($rootScope: IAppRootScope, $stateParams: IAssessmentParams, charts: ChartModel[],
               filters: FilterModel<any>[], model: Models.AssessmentFilterModel) => {

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
                    title: model.AssessmentTitle,
                    model: model,
                    backState: backState,
                    backParameters: backParameters
                }
            }];

        constructor(multipleSchoolYears: boolean, includePerformanceLevels: boolean, charts: any[]) {
            super(multipleSchoolYears);

            this.model = ['$rootScope', '$stateParams', 'grades', 'schoolYears', 'subjects',
                ($rootScope: IAppRootScope, $stateParams: IAssessmentParams, grades: Models.FilterValueModel[],
                    schoolYears: FilterValueModel[], subjects: string[]) => {

                    var model = new Models.AssessmentFilterModel();
                    model.AssessmentTitle = $stateParams.assessmentTitle || defaultAssessmentTitle;
                    model.Subject = ($stateParams.assessmentTitle) ? ((subjects && subjects.length) ? subjects[0] : null) : defaultAssessmentSubject;
                    model.Assessments = (grades && grades.length === 1) ? [grades[0].Value as number] : [];

                    if (!multipleSchoolYears)
                        model.SchoolYear = (schoolYears && schoolYears.length) ? schoolYears[0].Value as number : null;

                    if ($rootScope.filterModel) {
                        model = $rootScope.filterModel as Models.AssessmentFilterModel;
                        $rootScope.filterModel = null;
                    }

                    return model;
                }];

            if (includePerformanceLevels) {
                this.performanceLevels = ['$stateParams', 'api', ($stateParams: IAssessmentParams, api: IApi) => {
                    if ($stateParams.assessmentTitle) {
                        return api.assessmentFilters.getPerformanceLevels($stateParams.assessmentTitle);
                    } else {
                        return api.assessmentFilters.getPerformanceLevels(defaultAssessmentTitle);
                    }
                }];   
            }

            if (charts && charts.length) {
                this.charts = (typeof charts[0] == 'string') ? charts : [() => charts];
            }
        }
    }
}