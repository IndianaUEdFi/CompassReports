module App {
    angular
        .module('app', [
            //Api
            'app.api',

            //Vendors
            'chart.js',
            'ngAnimate',
            'ngMaterial',
            'ngMaterialSidemenu',
            'ui.router',

            ////Settings
            'app.settings',

            ////Services
            'app.services',

            ////Directives
            'app.directives',

            ////Modules
            'app.reports'
        ]);
}