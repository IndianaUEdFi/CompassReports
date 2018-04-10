/// <reference path="./filter-value.model.ts" />
/// <reference path="./filter.model.ts" />

module App.Models {
    export class DistrictFilterModel extends FilterModel<FilterValueModel> {
        constructor(districts: DistrictModel[], onChange?: (model: IReportFilterModel, filters: FilterModel<any>[], api: IApi) => void ) {

            const values: FilterValueModel[] = [];

            for (let i = 0; i < districts.length; i++) {
                values.push(new FilterValueModel(districts[i].Id, districts[i].DistrictName));
            }

            super(values, 'Districts', 'Districts', true, false, onChange);
        }
    }
}