/// <reference path="app.module.ts"/>

module App {

    class AppController {

        static $inject = [];

        constructor() { }
    }

    angular
        .module('app')
        .controller('app', AppController);
}