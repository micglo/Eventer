(function() {
    angular.module('clients')
        .component('clients',
        {
            templateUrl: 'app/admin/clients/clients.html',
            controller: 'ClientsController',
            controllerAs: 'ctrl'
        });
})();