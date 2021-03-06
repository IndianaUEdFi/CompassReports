﻿/// <reference path="chart.model.ts" />

module App.Models {
    export class PercentageTotalData {
        Percentage: number;
        Total: number;
    }
    
    export class PercentageTotalBarChartModel extends ChartModel {
        Series: string[];
        Data: PercentageTotalData[][];
        Totals: PercentageTotalData;
        ShowPercentage: boolean;
        HidePercentageTotal: boolean;

        Update = (model: PercentageTotalBarChartModel) => {
            if (!model) {
                this.HideChart = true;
                return;
            }

            this.Title = model.Title;
            this.Headers = model.Headers;
            this.HideTotal = model.HideTotal;
            this.HidePercentageTotal = model.HidePercentageTotal;
            this.TotalRowTitle = model.TotalRowTitle;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.Series = model.Series;
            this.Totals = model.Totals;
            this.HideChart = false;

            if (this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;

            if (this.ShowPercentage === undefined)
                this.ShowPercentage = model.ShowPercentage;
        };

        constructor(apiCall: string, chartCall: string, detailState?: RouteState, chartFilters?: any) {
            super(apiCall, chartCall, detailState, chartFilters);

            this.FlexXL = 50;
            this.FlexLG = 100;
            this.FlexMD = 100;
            this.FlexSM = 100;

            this.Type = 'percentage-total';
            this.Options = {
                responsive: true,
                maintainAspectRatio: false,
                legend: { display: true, position: 'bottom' },
                tooltips: {
                    mode: 'x',
                    callbacks: {
                        label: (tooltipItem, data) => {

                            var value = this.Data[tooltipItem.datasetIndex][tooltipItem.index];

                            if (this.ShowPercentage)
                                return `${this.Series[tooltipItem.datasetIndex]}: ${value.Percentage}%, ${value.Total}`;
                            else
                                return `${this.Series[tooltipItem.datasetIndex]}: ${value.Total}, ${value.Percentage}%`;
                        }
                    }    
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            callback: (value) => {
                                return this.ShowPercentage ? value + '%' : value;
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