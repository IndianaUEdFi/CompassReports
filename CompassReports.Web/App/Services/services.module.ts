module App {
    export interface IServices {
        colorGradient: Services.IColorGradient,
        compile: ng.ICompileService;
        document: ng.IDocumentService;
        filter: ng.IFilterService;
        http: ng.IHttpService;
        q: ng.IQService;
        state: ng.ui.IStateService;
        timeout: ng.ITimeoutService;
        template: ng.ITemplateRequestService;
        window: ng.IWindowService;
    }

    class Services implements IServices {

        static $inject = [
            'app.services.color-gradient',
            '$compile',
            '$document',
            '$filter',
            '$http',
            '$q',
            '$state',
            '$timeout',
            '$templateRequest',
            '$window'
        ];

        constructor(
            public colorGradient: Services.IColorGradient,
            public compile: ng.ICompileService,
            public document: ng.IDocumentService,
            public filter: ng.IFilterService,
            public http: ng.IHttpService,
            public q: ng.IQService,
            public state: ng.ui.IStateService,
            public timeout: ng.ITimeoutService,
            public template: ng.ITemplateRequestService,
            public window: ng.IWindowService,
        ) { }
    }

    angular
        .module("app.services", ['app.services.color-gradient'])
        .service("services", Services);
}