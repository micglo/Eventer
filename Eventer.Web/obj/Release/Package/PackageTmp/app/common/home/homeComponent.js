(function () {
    angular.module('home')
        .component('home', {
            templateUrl: 'app/common/home/home.html',
            controller: 'HomeController',
            controllerAs: 'ctrl'
        });
})();