var App;
(function (App) {
    var Reports;
    (function (Reports) {
        var PieChartModel = (function () {
            function PieChartModel(chartCall) {
                this.ChartCall = chartCall;
            }
            return PieChartModel;
        }());
        Reports.PieChartModel = PieChartModel;
        var BarChartModel = (function () {
            function BarChartModel(chartCall) {
                this.ChartCall = chartCall;
            }
            return BarChartModel;
        }());
        Reports.BarChartModel = BarChartModel;
    })(Reports = App.Reports || (App.Reports = {}));
})(App || (App = {}));
//# sourceMappingURL=chart-model.module.js.map