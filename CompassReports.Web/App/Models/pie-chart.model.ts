module App.Models {
    export class PieChartModel<T> extends ChartModel<T>{
        Data: T[];
        Total: number;

        Update = (model: PieChartModel<T>) => {
            this.Title = model.Title;
            this.Headers = model.Headers;
            this.Labels = model.Labels;
            this.Data = model.Data;
            this.Total = model.Total;

            if(this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        };

        constructor(apiCall: string, chartCall: string) {
            super(apiCall, chartCall);

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