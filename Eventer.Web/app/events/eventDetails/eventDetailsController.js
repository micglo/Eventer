(function() {
    angular.module('eventDetails')
        .controller('EventDetailsController', ['$routeParams', 'authService', 'apiService', 'tokenService', 'apiConfig', EventDetailsController]);

    function EventDetailsController($routeParams, authService, apiService, tokenService, apiConfig) {
        var self = this;

        if (!authService.isLoggedIn()) {
            var user = {
                username: apiConfig.username,
                password: apiConfig.password
            }

            tokenService.getToken(user).$promise.then(success).catch(error);

            function success(response) {
                getEvent();
            }

            function error(response) {
                console.log('EventsControllerError', response);
            }
        } else {
            getEvent();
        }

        function getEvent() {
            var eventId = $routeParams.eventId;
            apiService.event().getEvent({ eventId: eventId }).$promise.then(eventDetailsSuccess).catch(eventDetailsError);

            function eventDetailsSuccess(eventDetailsResponse) {
                self.event = eventDetailsResponse;
                self.eventDesc = eventDetailsResponse.eventDescription;
            }

            function eventDetailsError(eventDetailsResponse) {
                console.log('eventDetailsError', eventDetailsResponse);
            }
        }
    }
})();