﻿/// <reference path="chart.model.ts" />

module App.Models {
    export class LineChartModel extends ChartModel{
        Percentage: boolean;
        Series: string[];
        Data: number[][];
        Totals: number[];

        Update = (model: LineChartModel) => {
            if (!model) {
                this.HideChart = true;
                return;
            }

            this.Title = model.Title;
            this.Headers = model.Headers;
            this.HideTotal = model.HideTotal;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.Percentage = model.Percentage;
            this.Series = model.Series;
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

            this.Type = 'line';
            this.Options = {
                responsive: true,
                fill: false,
                lineTension: 0,
                maintainAspectRatio: false,
                legend: { display: true, position: 'bottom' },
                scales: {
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