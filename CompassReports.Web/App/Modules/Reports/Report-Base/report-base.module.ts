module App.Reports {

    export class ReportBaseView {
        templateUrl: string;
        controller: ng.IController;
        controllerAs: string;

        constructor(settings: ISystemSettings) {
            this.templateUrl = `${settings.moduleBaseUri}/reports/report-base/report-base.view.html`;
            this.controller = ReportBaseController;
            this.controllerAs = 'ctrl';
        }
    }

    export class ReportBaseController implements ng.IController{
        static $inject = ['$rootScope', '$mdSidenav', 'report'];

        apply = () => this.$rootScope.$emit('update-charts');

        isFiltering = () => this.report.model.isFiltering();

        reset = () => this.report.model.reset();

        toggleFilters = () => this.$mdSidenav('filternav').toggle();

        constructor(
            public $rootScope,
            private readonly $mdSidenav: ng.material.ISidenavService,
            private readonly report: Models.BaseReport
        ) {
            this.reset();
        }
    }
}