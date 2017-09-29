var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var Home;
        (function (Home) {
            var HomeController = (function () {
                function HomeController() {
                    console.log('home app');
                }
                return HomeController;
            }());
            HomeController.$inject = [];
            var HomeConfig = (function () {
                function HomeConfig($stateProvider, settings) {
                    $stateProvider.state('app.reports.home', {
                        url: '/home',
                        views: {
                            'report@app.reports': {
                                templateUrl: settings.moduleBaseUri + "/reports/home/home.view.html",
                                controller: HomeController,
                                controllerAs: 'ctrl'
                            }
                        }
                    });
                }
                return HomeConfig;
            }());
            HomeConfig.$inject = ['$stateProvider', 'settings'];
            angular
                .module('app.reports.home', [])
                .config(HomeConfig);
        })(Home = Reports.Home || (Reports.Home = {}));
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
//# sourceMappingURL=home.module.js.map