(function() {
    angular.module('app')
        .config(['$locationProvider', '$routeProvider', '$httpProvider', config])
        .run(['$rootScope', '$location', 'authService', function ($rootScope, $location, authService) {
            authService.init();

            $rootScope.$on('$routeChangeStart', function (event, next) {
                if (!authService.checkAuthentication(next)) {
                    event.preventDefault();
                    $location.path("/");
                }
            });
        }]);

    function config($locationProvider, $routeProvider, $httpProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider
            .when('/', {
                template: '<home></home>'
            })
            .when('/forbidden', {
                template: '<forbidden></forbidden>'
            })
            .when('/myAccount', {
                template: '<account></account>',
                permissions: ["Users"]
            })
            .when('/myAccount/myClients', {
                template: '<my-clients></my-clients>',
                permissions: ["Users"]
            })
            .when('/myAccount/myClients/:clientId', {
                template: '<my-client-details></my-client-details>',
                permissions: ["Users"]
            })
            .when('/admin/clients', {
                template: '<clients></clients>',
                permissions: ["Administrators"]
            })
            .when('/admin/clients/:clientId', {
                template: '<client-details></client-details>',
                permissions: ["Administrators"]
            })
            .when('/events', {
                template: '<events></events>'
            })
            .when('/events/:eventId', {
                template: '<event-details></event-details>'
            })
            .otherwise({
                template: '<page-not-found></page-not-found>'
            });

        $httpProvider.interceptors.push('myHttpInterceptor');
    }
})();