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
        static $inject = ['$rootScope', '$mdSidenav', 'services', 'report'];

        apply = () => this.$rootScope.$emit('update-charts');

        filteringCount = () => this.report.model.filteringCount();

        goBack = () => {
            this.$rootScope.filterModel = this.report.model;
            this.services.state.go(this.report.backState, this.report.backParameters);
        }

        isFiltering = () => this.report.model.isFiltering();

        reset = () => this.report.model.reset();

        toggleFilters = () => this.$mdSidenav('filternav').toggle();

        constructor(
            public $rootScope,
            private readonly $mdSidenav: ng.material.ISidenavService,
            private readonly services: IServices,
            private readonly report: Models.BaseReport
        ) { }
    }
}