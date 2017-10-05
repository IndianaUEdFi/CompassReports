module App.Models {
    export class PieChartModel extends ChartModel{
        Data: number[];
        Total: number;
        DelayDataCall: boolean;

        Update = (model: PieChartModel) => {
            if (!model) {
                this.HideChart = true;
                return;
            }

            this.Title = model.Title;
            this.Headers = model.Headers;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.TotalRowTitle = model.TotalRowTitle;
            this.Total = model.Total;

            if(this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        };

        constructor(apiCall: string, chartCall: string, delayDataCall?: boolean) {
            super(apiCall, chartCall, delayDataCall);

            this.FlexXL = 33;
            this.FlexLG = 50;
            this.FlexMD = 50;
            this.FlexSM = 100;

            this.Type = 'pie';
            this.Options = {
                responsive: true,
                legend: { display: true, position: 'left' }
            };
        }
    }
}