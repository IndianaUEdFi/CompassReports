/// <reference path="../Assessment-Report-View/assessment-report-view.module.ts" />

module App.Reports.CollegeReadinessTrends {

    angular
        .module('app.reports.college-readiness-trends',
        [
            'app.reports.college-readiness-trends.pass',
            'app.reports.college-readiness-trends.score',
            'app.reports.college-readiness-trends.took'
        ]);
}