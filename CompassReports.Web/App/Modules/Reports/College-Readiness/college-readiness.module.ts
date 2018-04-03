/// <reference path="../Assessment-Report-View/assessment-report-view.module.ts" />

module App.Reports.CollegeReadiness {
    angular
        .module('app.reports.college-readiness', [
            'app.reports.college-readiness.pass',
            'app.reports.college-readiness.scores',
            'app.reports.college-readiness.took'
        ]);
}