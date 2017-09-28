module App.Reports.Enrollment {
    class EnrollmentController {
        static $inject = ['$mdSidenav'];

        years = ['2005-2006', '2006-2007', '2007-2008', '2008-2009', '2009-2010', '2010-2011', '2011-2012', '2012-2013', '2013-2014', '2014-2015', '2015-2016', '2016-2017'];
        grades = ['Pre-Kindergarden', 'Kindergarten', 'Grade 1', 'Grade 2', 'Grade 3', 'Grade 4', 'Grade 5', 'Grade 6', 'Grade 7', 'Grade 8', 'Grade 9', 'Grade 10', 'Grade 11', 'Grade 12', 'Grade 12+/Adult'];
        ethnicities = ['American Indian', 'Asian', 'Black', 'Hispanic', 'Multiracial', 'Navite Hawaiian or Other Pacific Islander', 'White'];
        mealPlans = ['Free meals', 'Reduced price meals', 'Paid meals'];
        educationTypes = ['Special Education', 'General Education'];
        languageLearners = ['English Language Learner', 'Non-English Language Learner'];

        model: any = {
            SchoolYear: '2016-2017'
        }

        charts: any = [
            {
                title: 'Grade',
                headers: ['', 'Grade', 'Count'],
                labels: this.grades,
                data: [23097, 83263, 83750, 84596, 89159, 84813, 85383, 84284, 85791, 83981, 97023, 88652, 85913, 82367, 1308],
                colors: [
                    '#003E69', '#124862', '#24525C',
                    '#365C55', '#48664F', '#5A7148',
                    '#6C7B42', '#7E853C', '#908F35',
                    '#A2992F', '#B4A428', '#C6AE22', 
                    '#D8B81B', '#EAC215', '#FDCD0F'

                    //'#E57373', '#F06292', '#BA68C8',
                    //'#9575CD', '#7986CB', '#64B5F6',
                    //'#4FC3F7', '#4DD0E1', '#4DB6AC',
                    //'#81C784', '#AED581', '#DCE775', 
                    //'#FFF176', '#FFD54F', '#FFB74D'
                ],
                options: { legend: { display: true, position: 'left' } }
            },
            {
                title: 'Ethnicity',
                headers: ['', 'Ethnicity', 'Count'],
                labels: this.ethnicities,
                data: [2301, 26090, 137338, 130842, 54109, 817, 781883],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#2A555A', '#546D4B', '#7E853C', '#A89D2D', '#D2B51E', '#FDCD0F']
            },
            {
                title: 'Free/Reduced Price Meals',
                headers: ['', 'Meal Type', 'Count'],
                labels: this.mealPlans,
                data: [432677, 85564, 615139],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#7E853C', '#FDCD0F']
            },
            {
                title: 'Special Education',
                headers: ['', 'Education', 'Count'],
                labels: this.educationTypes,
                data: [164706, 968674],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#FDCD0F' ]
            },
            {
                title: 'English Language Learners',
                headers: ['', 'Language Learner', 'Count'],
                labels: this.languageLearners,
                data: [1082703, 50677],
                options: { legend: { display: true, position: 'left' } },
                colors: ['#003E69', '#FDCD0F']
            }
        ];

        toggleFilters = () => {
            this.$mdSidenav('filternav').toggle();
        }

        reset = () => {
            this.model = {
                SchoolYear: '2016-2017'
            };
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