module App.Models {
    export class FilterModel<T> {
        Title: string;
        ModelParam: string;
        Multiple: boolean;

        Values: FilterValueModel[];
        Display: any;

        private setValues = (values: FilterValueModel[] | string[] | number[]) => {
            if (typeof values[0] === 'object') this.Values = values as FilterValueModel[];
            else {
                this.Values = [];
                for (let i = 0; i < values.length; i++) {
                    const value = values[i] as string;
                    this.Values.push(new FilterValueModel(value, value));
                }
            }
        }

        private createDisplay = () => {
            this.Display = {};
            for (let i = 0; i < this.Values.length; i++) {
                const value = this.Values[i];
                this.Display[value.Value] = value.Display;
            }
        }

        constructor(values: FilterValueModel[] | string[] | number[],
            title: string,
            modelParam: string,
            multiple: boolean) {
            if (values.length) {
                this.setValues(values);
                this.createDisplay();
            }

            this.Title = title;
            this.Multiple = multiple;
            this.ModelParam = modelParam;
        }
    }
}