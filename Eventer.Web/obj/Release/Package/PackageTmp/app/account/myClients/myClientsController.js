(function () {
    angular.module('myClients')
        .controller('MyClientsController', ['$location', 'authService', 'apiService', MyClientsController]);

    function MyClientsController($location, authService, apiService) {
        var self = this;

        if (authService.userIsAllowed(['Users'])) {
            apiService.client().getMyClients().$promise.then(success).catch(error);

            function success(response) {
                self.clients = response.items;
            }

            function error(response) {
                self.myClientsError = response.data.message;
            }
        } else {
            $location.path('/');
        }
    }
})();