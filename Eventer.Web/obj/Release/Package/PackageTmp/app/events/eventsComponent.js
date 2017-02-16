(function () {
    angular.module('events')
        .component('events',
        {
            templateUrl: 'app/events/events.html',
            controller: 'EventsController',
            controllerAs: 'ctrl'
        });
})();