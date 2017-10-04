/// <reference path="chart.model.ts" />

module App.Models {
    export class BarChartModel<T> extends ChartModel<T>{
        Percentage: boolean;
        Series: string[];
        SingleSeries: boolean;
        Data: T[][];
        Totals: number[];

        Update = (model: BarChartModel<T>) => {
            this.Title = model.Title;
            this.Headers = model.Headers;
            this.HideTotal = model.HideTotal;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.Percentage = model.Percentage;
            this.Series = model.Series;
            this.SingleSeries = model.SingleSeries;
            this.Totals = model.Totals;

            if (this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        };

        constructor(apiCall: string, chartCall: string) {
            super(apiCall, chartCall);

            this.FlexXL = 50;
            this.FlexLG = 50;
            this.FlexMD = 100;
            this.FlexSM = 100;

            this.Type = 'bar';
            this.Options = {
                responsive: true,
                maintainAspectRatio: false,
                legend: { display: true, position: 'bottom' },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }],
                    xAxes: [
                    {
                        ticks: {
                            autoSkip: false
                        }
                    }]
                }
            };
        }
    }
}