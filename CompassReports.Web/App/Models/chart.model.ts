module App.Models {
    export abstract class ChartModel {
        Type: string;
        ApiCall: string;
        ChartCall: string;
        Colors: string[];
        Title: string;
        TotalRowTitle: string;
        Headers: string[];
        Labels: string[];
        ShowChart: boolean;
        HideTotal: boolean;
        HideChart: boolean;
        Options: any;
        FlexXL: number;
        FlexLG: number;
        FlexMD: number;
        FlexSM: number;

        DetailState: string;

        Update = (model: ChartModel) => {
            this.Title = model.Title;
            this.Headers = model.Headers;
            this.HideTotal = model.HideTotal;
            this.Labels = model.Labels;

            if (this.ShowChart === undefined)
                this.ShowChart = model.ShowChart;
        }

        protected constructor(apiCall: string, chartCall: string, detailState?: string) {
            this.ApiCall = apiCall;
            this.ChartCall = chartCall;
            this.DetailState = detailState;
        }
    }
}