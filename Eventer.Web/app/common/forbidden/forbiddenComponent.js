(function () {
    angular.module('forbidden')
        .component('forbidden',
        {
            templateUrl: 'app/common/forbidden/forbidden.html',
            controller: 'ForbiddenController',
            controllerAs: 'ctrl'
        });
})();