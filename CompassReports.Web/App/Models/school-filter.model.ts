module App.Models {
    export class SchoolFilterModel extends FilterModel<FilterValueModel> {
        constructor(schools: SchoolModel[]) {

            const values: FilterValueModel[] = [];

            for (let i = 0; i < schools.length; i++) {
                values.push(new FilterValueModel(schools[i].Id, schools[i].SchoolName, schools[i].DistrictName));
            }

            console.log(values);

            super(values, 'Schools', 'Schools', true, false);
        }
    }
}