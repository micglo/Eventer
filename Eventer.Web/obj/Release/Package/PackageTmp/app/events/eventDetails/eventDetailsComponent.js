(function() {
    angular.module('eventDetails')
        .component('eventDetails',
        {
            templateUrl: 'app/events/eventDetails/eventDetails.html',
            controller: 'EventDetailsController',
            controllerAs: 'ctrl'
    });
})();