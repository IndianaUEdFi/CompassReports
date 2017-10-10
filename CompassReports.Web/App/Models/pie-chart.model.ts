module App.Models {
    export class PieChartModel extends ChartModel{
        Data: number[];
        Percentages: number[];
        PercentageHeaders: number[];
        ShowPercentage: boolean;
        Total: number;

        Update = (model: PieChartModel) => {
            if (!model) {
                this.HideChart = true;
                return;
            }

            this.Title = model.Title;
            this.Headers = model.Headers;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.Percentages = model.Percentages;
            this.PercentageHeaders = model.PercentageHeaders;
            this.TotalRowTitle = model.TotalRowTitle;
            this.Total = model.Total;

            if(this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;

            if (this.ShowPercentage === undefined)
                this.ShowPercentage = model.ShowPercentage;
        };

        constructor(apiCall: string, chartCall: string, detailState?: RouteState) {
            super(apiCall, chartCall, detailState);

            this.FlexXL = 50;
            this.FlexLG = 50;
            this.FlexMD = 50;
            this.FlexSM = 100;

            this.Type = 'pie';
            this.Options = {
                responsive: true,
                maintainAspectRatio: false,
                legend: { display: true, position: 'left' },
                tooltips: {
                    callbacks: {
                        label: (tooltipItem, data) => {
                            if (this.Percentages && this.Percentages.length)
                                return `${this.Labels[tooltipItem.index]}: ${this.Percentages[tooltipItem.index]}%, ${this.Data[tooltipItem.index]}`;
                            else
                                return `${this.Labels[tooltipItem.index]}: ${this.Data[tooltipItem.index]}`;
                        }
                    }   
                }
            };
        }
    }
}