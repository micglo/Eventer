(function () {
    angular.module('login')
        .component('login', {
            templateUrl: 'app/account/login/login.html',
            bindings: {
                close: '&',
                dismiss: '&'
            },
            controller: 'LoginController',
            controllerAs: 'ctrl'
        });
})();