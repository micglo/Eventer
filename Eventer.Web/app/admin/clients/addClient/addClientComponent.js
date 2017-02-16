(function () {
    angular.module('adminAddClient')
        .component('adminAddClient',
        {
            templateUrl: 'app/admin/clients/addClient/addClient.html',
            bindings: {
                close: '&',
                dismiss: '&'
            },
            controller: 'AdminAddClientController',
            controllerAs: 'ctrl'
        });
})();