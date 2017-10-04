/// <reference path="../app.config.ts" />

module App {

    export interface IApi {
        attendance: Api.Attendance.IAttendanceApi;
        enrollment: Api.Enrollment.IEnrollmentApi;
        enrollmentFilters: Api.EnrollmentFilters.IEnrollmentFiltersApi;
        enrollmentTrends: Api.EnrollmentTrends.IEnrollmentTrendsApi;
    }

    class ApiService implements IApi {

        static $inject = [
            'api.attendance',
            'api.enrollment',
            'api.enrollment-filters',
            'api.enrollment-trends'
        ];

        constructor(
            public attendance: Api.Attendance.IAttendanceApi,
            public enrollment: Api.Enrollment.IEnrollmentApi,
            public enrollmentFilters: Api.EnrollmentFilters.IEnrollmentFiltersApi,
            public enrollmentTrends: Api.EnrollmentTrends.IEnrollmentTrendsApi
        ) {
        }
    }

    export interface IApiBaseDefault<T> {
        resourceUrl: string;
        getAll(): angular.IPromise<Array<T>>;
        get(id: number): angular.IPromise<T>;
        post(model: T): angular.IPromise<T>;
        put(id: number, model: T): angular.IPromise<T>;
        delete(id: number): angular.IPromise<boolean>;
    }

    export class ApiBase {

        static $inject = ['services', 'settings'];

        resourceUrl = '';

        constructor(protected services: IServices, protected settings: ISystemSettings) {
        }
    }


    export class ApiBaseDefault<T> extends ApiBase implements IApiBaseDefault<T> {
        getAll(): angular.IPromise<Array<T>> { return this.services.http.get<Array<T>>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`).then((data) => { return data.data; }); }
        get(id: number): angular.IPromise<T> { return this.services.http.get<T>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/${id}`).then((data) => { return data.data; }); }
        post(model: T): angular.IPromise<T> { return this.services.http.post<T>(`${this.settings.apiBaseUrl}/${this.resourceUrl}`, model).then((data) => { return data.data; }); }
        put(id: number, model: T): angular.IPromise<T> { return this.services.http.put<T>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/${id}`, model).then((data) => { return data.data; }); }
        delete(id: number): angular.IPromise<boolean> { return this.services.http.delete<boolean>(`${this.settings.apiBaseUrl}/${this.resourceUrl}/${id}`).then((data) => { return data.data; }); }
    }

    angular
        .module("app.api", [
            'app.api.attendance',
            'app.api.enrollment',
            'app.api.enrollment-filters',
            'app.api.enrollment-trends'
        ])
        .service("api", ApiService);

} 