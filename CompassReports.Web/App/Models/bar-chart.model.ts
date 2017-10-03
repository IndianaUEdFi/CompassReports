/// <reference path="chart.model.ts" />

module App.Models {
    export class BarChartModel<T> extends ChartModel<T>{
        Series: string[];
        Data: T[][];
        Totals: number[];

        Update = (model: BarChartModel<T>) => {
            this.Title = model.Title;
            this.Headers = model.Headers;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.Series = model.Series;
            this.Totals = model.Totals;

            if (this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        };

        constructor(chartCall: string) {
            super(chartCall);

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
                    }]
                }
            };
        }
    }
}