(function () {
    angular.module('signUp')
        .component('signUp', {
            templateUrl: 'app/account/signUp/signUp.html',
            bindings: {
                close: '&',
                dismiss: '&'
            },
            controller: 'SignUpController',
            controllerAs: 'ctrl'
        });
})();