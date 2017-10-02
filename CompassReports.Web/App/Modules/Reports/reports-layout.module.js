var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var ReportsLayoutController = (function () {
            function ReportsLayoutController($mdSidenav) {
                var _this = this;
                this.$mdSidenav = $mdSidenav;
                this.isOpen = false;
                this.menuId = 0;
                this.toggleExpanded = function (selectedMenuId) {
                    if (!_this.isOpen) {
                        _this.isOpen = true;
                        _this.menuId = selectedMenuId;
                    }
                    else {
                        if (_this.menuId === selectedMenuId) {
                            _this.isOpen = false;
                            _this.menuId = 0;
                        }
                        else {
                            _this.menuId = selectedMenuId;
                        }
                    }
                };
                this.toggleSidenav = function () {
                    _this.isOpen = !_this.isOpen;
                    if (_this.menuId === 0)
                        _this.menuId = 1;
                };
                this.isSelected = function (value) {
                    return _this.menuId === value;
                };
                this.toggleThemes = function () { return _this.$mdSidenav('colornav').toggle(); };
            }
            return ReportsLayoutController;
        }());
        ReportsLayoutController.$inject = ['$mdSidenav'];
        var ReportsLayoutConfig = (function () {
            function ReportsLayoutConfig($stateProvider, settings) {
                $stateProvider
                    .state('app.reports', {
                    abstract: true,
                    views: {
                        'content@': {
                            templateUrl: settings.moduleBaseUri + "/reports/reports-layout.view.html",
                            controller: ReportsLayoutController,
                            controllerAs: 'ctrl'
                        }
                    }
                });
            }
            return ReportsLayoutConfig;
        }());
        ReportsLayoutConfig.$inject = ['$stateProvider', 'settings'];
        angular
            .module('app.reports', [
            'app.reports.enrollment',
            'app.reports.enrollment-trends',
            'app.reports.home'
        ])
            .config(ReportsLayoutConfig);
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
//# sourceMappingURL=reports-layout.module.js.map