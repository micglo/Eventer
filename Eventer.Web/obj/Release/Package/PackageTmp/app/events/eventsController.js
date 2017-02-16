(function () {
    angular.module('events')
        .controller('EventsController', ['$location', 'authService', 'apiService', 'tokenService', 'apiConfig', EventsController]);

    function EventsController($location, authService, apiService, tokenService, apiConfig) {
        var self = this;

        if (!authService.isLoggedIn()) {
            var user = {
                username: apiConfig.username,
                password: apiConfig.password
            }

            tokenService.getToken(user).$promise.then(success).catch(error);

            function success(response) {
                initEvents();
            }

            function error(response) {
                console.log('EventsControllerError', response);
            }
        } else {
            initEvents();
        }

        function initEvents() {
            self.currentPage = 1;
            self.currentSearchPage = 1;
            self.search = '';
            self.noEvents = '';

            self.eventDateFromOpened = false;
            self.eventDateToOpened = false;
            self.eventDateFrom = new Date();
            self.eventDateTo = new Date();

            self.getEvents = getEvents;
            self.getEventsByName = getEventsByName;

            self.openEventDateFrom = openEventDateFrom;
            self.openEventDateTo = openEventDateTo;

            self.goToEventDetails = goToEventDetails;

            self.itemsPerPage = {
                availableOptions: [
                    { id: '1', value: '10' },
                    { id: '2', value: '20' },
                    { id: '3', value: '30' }
                ],
                selectedOption: { id: '1', value: '10' }
            };

            self.city = {
                availableOptions: [
                    { id: '1', value: 'Wrocław' },
                    { id: '2', value: 'Poznań' }
                ],
                selectedOption: { id: '1', value: 'Wrocław' }
            };
            

            getEvents();
        }

        function getEvents() {
            self.noEvents = '';
            self.events = [];
            if (self.search.length > 0) {
                getEventsByName();
            } else {
                apiService.event()
                    .getEventsByDate({
                        page: self.currentPage,
                        pageSize: self.itemsPerPage.selectedOption.value,
                        cityName: self.city.selectedOption.value,
                        dateFrom: formatDateFrom(self.eventDateFrom),
                        dateTo: formatDateTo(self.eventDateTo)
                    })
                    .$promise.then(eventsSuccess, eventsError);

                function eventsSuccess(eventsResponse) {
                    self.totalItems = eventsResponse.totalNumberOfRecords;
                    self.events = eventsResponse.items;
                    if (!eventsResponse.items.length)
                        self.noEvents = "We have no events witch match your search criteria :(";
                }

                function eventsError(eventsResponse) {
                    console.log('eventsError', eventsResponse);
                }
            }
        }

        function getEventsByName() {
            apiService.event()
                    .searchEvents({
                        page: self.currentSearchPage,
                        pageSize: self.itemsPerPage.selectedOption.value,
                        cityName: self.city.selectedOption.value,
                        eventName: self.search
                    })
                    .$promise.then(eventsSearchSuccess, eventsSearchError);

            function eventsSearchSuccess(eventsSearchResponse) {
                self.totalSearchItems = eventsSearchResponse.totalNumberOfRecords;
                self.events = eventsSearchResponse.items;
                if (!eventsSearchResponse.items.length)
                    self.noEvents = "We have no events witch match your search criteria :(";
            }

            function eventsSearchError(eventsSearchResponse) {
                console.log('eventsSearchError', eventsSearchResponse);
            }
        }

        function openEventDateFrom() {
            self.eventDateFromOpened = true;
        }

        function openEventDateTo() {
            self.eventDateToOpened = true;
        }

        function goToEventDetails(eventId) {
            $location.path('/events/' + eventId);
        }

        function formatDateFrom(date) {
            var currentDay = date.getDate();
            var currentMonth = date.getMonth()+1;
            var currentYear = date.getFullYear();
            return currentYear + '-' + currentMonth + '-' + currentDay + 'T00:00:00';
        }
        function formatDateTo(date) {
            var currentDay = date.getDate();
            var currentMonth = date.getMonth() + 1;
            var currentYear = date.getFullYear();
            return currentYear + '-' + currentMonth + '-' + currentDay + 'T23:59:59';
        }
    }
})();