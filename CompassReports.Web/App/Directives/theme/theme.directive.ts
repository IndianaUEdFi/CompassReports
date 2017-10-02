module App.Directive.Theme {

    interface ITruncateTooltipScope extends angular.IScope {
        showTooltip: boolean;
        value: string;
    }

    class ThemeController {
        static $inject = ['$rootScope', '$mdSidenav', '$mdTheming', 'themeProvider'];

        colors: any = [
            { color: '#F44336', name: 'red' },
            { color: '#E91E63', name: 'pink' },
            { color: '#9C27B0', name: 'purple' },
            { color: '#673AB7', name: 'deep-purple' },
            { color: '#3F51B5', name: 'indigo' },
            { color: '#2196F3', name: 'blue' },
            { color: '#03A9F4', name: 'light-blue' },
            { color: '#00BCD4', name: 'cyan' },
            { color: '#009688', name: 'teal' },
            { color: '#4CAF50', name: 'green' },
            { color: '#8BC34A', name: 'light-green' },
            { color: '#CDDC39', name: 'lime' },
            { color: '#FFEB3B', name: 'yellow' },
            { color: '#FFC107', name: 'amber' },
            { color: '#FF9800', name: 'orange' },
            { color: '#FF5722', name: 'deep-orange' },
            { color: '#795548', name: 'brown' },
            { color: '#9E9E9E', name: 'grey' },
            { color: '#607D8B', name: 'blue-grey' }
        ];

        colorCount = 1;

        setTheme = (primaryColor: any, secondaryColor: any) => {

            this.colorCount++;

            this.$mdThemingProvider
                .theme('compass-reports-theme' + this.colorCount)
                .primaryPalette(primaryColor.name)
                .accentPalette(secondaryColor.name)
                .warnPalette('red');

            this.$rootScope.currentTheme = 'compass-reports-theme' + this.colorCount;
            this.$rootScope.primaryColor = primaryColor;
            this.$rootScope.secondaryColor = secondaryColor;

            this.$rootScope.$emit('theme-change');

            this.$mdTheming.generateTheme('compass-reports-theme' + this.colorCount);
            this.$mdThemingProvider.setDefaultTheme('compass-reports-theme' + this.colorCount);
        }

        toggleThemes = () => this.$mdSidenav('colornav').toggle();

        constructor(
            private readonly $rootScope: IAppRootScope,
            private readonly $mdSidenav: ng.material.ISidenavService,
            private readonly $mdTheming: any,
            private readonly $mdThemingProvider: ng.material.IThemingProvider) {

            $mdThemingProvider.generateThemesOnDemand(true);
            $mdThemingProvider.alwaysWatchTheme(true);
            this.setTheme($rootScope.defaultPrimary, $rootScope.defaultSecondary);
        }          
    }

    function themeDirective(settings: ISystemSettings) {
        return {
            restrict: 'E',
            templateUrl: `${settings.directiveBaseUri}/theme/theme.view.html`,
            scope: {},
            controller: ThemeController,
            controllerAs: 'ctrl'
        }
    }

    angular
        .module('app.directives.theme', [])
        .directive('themeNav', ['settings', themeDirective]);
}