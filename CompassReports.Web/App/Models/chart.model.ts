module App.Models {
    export abstract class ChartModel<T> {
        Type: string;
        ChartCall: string;
        Colors: string[];
        Title: string;
        Headers: string[];
        Labels: string[];
        ShowChart: boolean;
        Options: any;
        Data: T[] | T[][];
        FlexXL: number;
        FlexLG: number;
        FlexMD: number;
        FlexSM: number;

        Update = (model: ChartModel<T>) => {
            this.Title = model.Title;
            this.Headers = model.Headers;
            this.Labels = model.Labels;
            this.Data = model.Data;

            if (this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        }

        protected constructor(chartCall: string) {
            this.ChartCall = chartCall;
        }
    }
}