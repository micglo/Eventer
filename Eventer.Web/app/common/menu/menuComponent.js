(function () {
    angular.module('menu')
        .component('menu', {
            templateUrl: 'app/common/menu/menu.html',
            controller: 'MenuController',
            controllerAs: 'ctrl'
        });
})();