module App.Models {
    export class FilterValueModel {
        Display: string | number;
        SubDisplay?: string | number;
        Value: string | number;

        constructor(value: string | number, display: string | number, subDisplay?: string | number) {
            this.Value = value;
            this.Display = display;
            this.SubDisplay = subDisplay;
        }
    }
}