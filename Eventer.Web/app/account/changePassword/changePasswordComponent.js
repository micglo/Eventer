(function () {
    angular.module('changePassword')
        .component('changePassword', {
            templateUrl: 'app/account/changePassword/changePassword.html',
            bindings: {
                close: '&',
                dismiss: '&'
            },
            controller: 'ChangePasswordController',
            controllerAs: 'ctrl'
        });
})();