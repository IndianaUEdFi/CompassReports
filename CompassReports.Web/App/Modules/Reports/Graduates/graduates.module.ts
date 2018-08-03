/// <reference path="../Graduates-Report-View/graduates-report-view.module.ts" />

module App.Reports.Graduate {
    angular
        .module('app.reports.graduates', [
            'app.reports.graduates.diploma-type',
            'app.reports.graduates.overview',
            'app.reports.graduates.status',
            'app.reports.graduates.waviers'
        ]);
}