module App {
    export interface ISystemSettings {
        apiBaseUrl: string;
        directiveBaseUri: string;
        moduleBaseUri: string;
        componentBaseUri: string;
    }

    class SystemSettings implements ISystemSettings {
        apiBaseUrl = 'api';
        directiveBaseUri = 'app/directives';
        moduleBaseUri = 'app/modules';
        componentBaseUri = 'app/components';
    }

    angular
        .module('app.settings', [])
        .constant('settings', new SystemSettings());
}    