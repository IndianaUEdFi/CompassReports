module App.Reports.Home {
    class HomeController {
        static $inject = [];

        constructor() {
        }
    }

    class HomeConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.home', {
                url: '/home',
                views: {
                    'report@app.reports': {
                        templateUrl: `${settings.moduleBaseUri}/reports/home/home.view.html`,
                        controller: HomeController,
                        controllerAs: 'ctrl'
                    }
                }
            });
        }
    }

    angular
        .module('app.reports.home', [])
        .config(HomeConfig);
}