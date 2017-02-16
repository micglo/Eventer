(function () {
    angular.module('addClient')
        .component('addClient', {
            templateUrl: 'app/account/addClient/addClient.html',
            bindings: {
                close: '&',
                dismiss: '&'
            },
            controller: 'AddClientController',
            controllerAs: 'ctrl'
        });
})();