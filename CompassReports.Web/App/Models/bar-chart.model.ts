/// <reference path="chart.model.ts" />

module App.Models {
    export class BarChartModel extends ChartModel{
        Percentage: boolean;
        Series: string[];
        SingleSeries: boolean;
        Data: number[][];
        PercentageData: number[][];
        Totals: number[];

        Update = (model: BarChartModel) => {
            if (!model) {
                this.HideChart = true;
                return;
            }

            this.Title = model.Title;
            this.Headers = model.Headers;
            this.HideTotal = model.HideTotal;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.PercentageData = model.PercentageData;
            this.Percentage = model.Percentage;
            this.Series = model.Series;
            this.SingleSeries = model.SingleSeries;
            this.TotalRowTitle = model.TotalRowTitle;
            this.Totals = model.Totals;

            if (this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        };

        constructor(apiCall: string, chartCall: string, delayDataCall?: boolean) {
            super(apiCall, chartCall, delayDataCall);

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
                            beginAtZero: true,
                            callback: (value) => {
                                return this.Percentage ? value + '%' : value;
                            }
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