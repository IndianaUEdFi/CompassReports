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
        static $inject = ['$rootScope', 'api', 'services', '$mdSidenav', 'report'];

        toggleFilters = () => this.$mdSidenav('filternav').toggle();

        isFiltering = () => this.report.model.isFiltering();

        reset = () => this.report.model.reset();

        apply = () => {
            angular.forEach(this.report.charts, chart => {
                return this.api[this.report.api][chart.ChartCall](this.report.model)
                    .then((result: Models.ChartModel<number>) => {
                        chart.Update(result);
                        chart.Colors = this.services.colorGradient.getColors(result.Data.length);
                    });
            });

        }

        constructor(
            public $rootScope,
            private readonly api: IApi,
            private readonly services: IServices,
            private readonly $mdSidenav: ng.material.ISidenavService,
            private readonly report: Models.BaseReport
        ) {
            this.reset();
            this.apply();
        }
    }
}