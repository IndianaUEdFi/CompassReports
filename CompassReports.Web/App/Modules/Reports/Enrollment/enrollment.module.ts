module App.Reports.Enrollment {
    class EnrollmentController {
        static $inject = ['$mdSidenav'];

        charts: any = [
            {
                title: 'Grade',
                headers: ['', 'Grade', 'Count'],
                labels: ['Pre-Kindergarden', 'Kindergarten', 'Grade 1', 'Grade 2', 'Grade 3', 'Grade 4', 'Grade 5', 'Grade 6', 'Grade 7', 'Grade 8', 'Grade 9', 'Grade 10', 'Grade 11', 'Grade 12', 'Grade 12+/Adult'],
                data: [23097, 83263, 83750, 84596, 89159, 84813, 85383, 84284, 85791, 83981, 97023, 88652, 85913, 82367, 1308],
                colors: [
                    '#003E69', '#124862', '#24525C',
                    '#365C55', '#48664F', '#5A7148',
                    '#6C7B42', '#7E853C', '#908F35',
                    '#A2992F', '#B4A428', '#C6AE22', 
                    '#D8B81B', '#EAC215', '#FDCD0F'
                ],
                options: { legend: { display: true, position: 'left' } }
            },
            {
                title: 'Ethnicity',
                headers: ['', 'Ethnicity', 'Count'],
                labels: ['American Indian', 'Asian', 'Black', 'Hispanic', 'Multiracial', 'Navite Hawaiian or Other Pacific Islander', 'White'],
                data: [2301, 26090, 137338, 130842, 54109, 817, 781883],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#2A555A', '#546D4B', '#7E853C', '#A89D2D', '#D2B51E', '#FDCD0F']
            },
            {
                title: 'Free/Reduced Price Meals',
                headers: ['', 'Meal Type', 'Count'],
                labels: ['Free meals', 'Reduced price meals', 'Paid meals'],
                data: [432677, 85564, 615139],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#7E853C', '#FDCD0F']
            },
            {
                title: 'Spcial Education',
                headers: ['', 'Education', 'Count'],
                labels: ['Special Education', 'General Education'],
                data: [164706, 968674],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#FDCD0F' ]
            },
            {
                title: 'English Language Learners',
                headers: ['', 'Language Learner', 'Count'],
                labels: ['English Language Learner', 'Non-English Language Learner'],
                data: [1082703, 50677],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#FDCD0F']
            }
        ];

        toggleFilters = () => {
            this.$mdSidenav('filternav').toggle();
        }

        constructor(private readonly $mdSidenav: ng.material.ISidenavService) {
            console.log('home app');
        }
    }

    class EnrollmentConfig {
        static $inject = ['$stateProvider', 'settings'];

        constructor($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) {

            $stateProvider.state('app.reports.enrollment', {
                url: '/enrollment',
                views: {
                    'report@app.reports': {
                        templateUrl: `${settings.moduleBaseUri}/reports/enrollment/enrollment.view.html`,
                        controller: EnrollmentController,
                        controllerAs: 'ctrl'
                    }
                }
            });
        }
    }

    angular
        .module('app.reports.enrollment', [])
        .config(EnrollmentConfig);
}