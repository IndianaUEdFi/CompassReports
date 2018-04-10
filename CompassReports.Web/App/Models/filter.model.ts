module App.Models {
    export class FilterModel<T> {
        Title: string;
        ModelParam: string;
        Multiple: boolean;
        Required: boolean;
        Disabled: boolean;

        Values: FilterValueModel[];
        Display: any;
        OnChange?: (model: IReportFilterModel, filters: FilterModel<any>[], api: IApi) => void;

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

        update = (values: FilterValueModel[] | string[] | number[]) => {
            this.setValues(values);
            this.createDisplay();
        }

        findByValue = (value: string | number) => {
            let returnValue: FilterValueModel;

            for (let i = 0; i < this.Values.length; i++) {
                if (this.Values[i].Value === value) {
                    returnValue = this.Values[i];
                    break;
                }   
            }

            return returnValue;
        }

        constructor(values: FilterValueModel[] | string[] | number[],
            title: string,
            modelParam: string,
            multiple: boolean,
            required?: boolean,
            onChange?: (model: IReportFilterModel, filters: FilterModel<any>[], api: IApi) => void) {

            if (values && values.length) this.update(values);

            this.Title = title;
            this.Multiple = multiple;
            this.ModelParam = modelParam;
            this.Required = required ? required : false;
            this.OnChange = onChange;
        }
    }
}