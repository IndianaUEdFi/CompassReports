module App.Reports {
    export class PieChartModel<T> {
        ChartCall: string;
        Chart: Models.EnrollmentChartModel<T>;
        Colors: string[];
        Options: any;
        ShowChart: boolean;

        constructor(chartCall: string) {
            this.ChartCall = chartCall;
        }
    }

    export class BarChartModel<T> {
        ChartCall: string;
        Chart: Models.EnrollmentTrendsChartModel<T>;
        Colors: string[];
        Options: any;
        ShowChart: boolean;

        constructor(chartCall: string) {
            this.ChartCall = chartCall;
        }
    }
}