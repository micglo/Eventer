(function () {
    angular.module('resetClientSecret')
        .component('resetClientSecret', {
            templateUrl: 'app/account/resetClientSecret/ResetClientSecret.html',
            bindings: {
                close: '&',
                dismiss: '&'
            },
            controller: 'ResetClientSecretController',
            controllerAs: 'ctrl'
        });
})();