module App.Models {
    export class FilterValueModel {
        Display: string | number;
        Value: string | number;

        constructor(display: string | number, value: string | number) {
            this.Display = display;
            this.Value = value;
        }
    }
}