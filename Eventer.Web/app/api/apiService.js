(function () {
    angular.module('api')
        .factory('apiService', ['$resource', 'apiConfig', apiService]);

    function apiService($resource, apiConfig) {
        return {
            account: account,
            roles: roles,
            token: token,
            client: client,
            event: event
        };

        function account() {
            return $resource(apiConfig.url + 'api/v1/account/:action', {}, {
                register: {
                    method: 'POST',
                    params: { 'action': 'Register' },
                    interceptor: { //todo move to global
                        response: function(response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                changePassword: {
                    method: 'POST',
                    params: { 'action': 'ChangePassword' },
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) { 
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                }
            });
        }

        function roles() {
            return $resource(apiConfig.url + 'api/v1/roles/GetMyRoles', {}, {
                getMyRoles: {
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    isArray: true,
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                }
            });
        }

        function token(){
            return $resource(apiConfig.url + 'token', {}, {
                getToken: {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    interceptor: {
                        response: function(response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                }
            });
        }

        function client(page, pageSize) {
            return $resource(apiConfig.url + 'api/v1/Clients/:action/:clientId', {}, {
                addClient: {
                    method: 'POST',
                    params: {'action': 'AddClient'},
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function(response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                getMyClients: {
                    method: 'GET',
                    params: {'action': 'GetMyClients'},
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function(response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                resetMyClientSecret: {
                    method: 'POST',
                    params: {'action': 'ResetMyClientSecret'},
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function(response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                //getMyClient: {
                //    method: 'GET',
                //    params: {'action': 'GetMyClient'},
                //    headers: {
                //        'Authorization': 'Bearer '
                //    },
                //    interceptor: {
                //        response: function(response) {
                //            var result = response.resource;
                //            result.$status = response.status;
                //            return result;
                //        }
                //    }
                //},
                getClients: {
                    url: apiConfig.url + 'api/v1/Clients?page=:page&pageSize=:pageSize',
                    method: 'GET',
                    params: { 'page': page, 'pageSize': pageSize },
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                getClient: {
                    url: apiConfig.url + 'api/v1/Clients/:clientId',
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                putMyClient: {
                    method: 'PUT',
                    params: { 'action': 'PutMyClient' },
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                putClient: {
                    method: 'PUT',
                    params: { 'action': 'PutClientNoSecret' },
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                resetClientSecret: {
                    method: 'POST',
                    params: { 'action': 'ResetClientSecret' },
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                deleteClient: {
                    url: apiConfig.url + 'api/v1/Clients/:clientId',
                    method: 'DELETE',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                }
                ,
                postClient: {
                    method: 'POST',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                }
            });
        }

        function event() {
            return $resource(apiConfig.url + 'api/v1/events?page=:page&pageSize=:pageSize&cityName=:cityName', {}, {
                getEvents: {
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                getEventsByDate: {
                    url: apiConfig.url + 'api/v1/events?page=:page&pageSize=:pageSize&cityName=:cityName&dateFrom=:dateFrom&dateTo=:dateTo',
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                getEvent: {
                    url: apiConfig.url + 'api/v1/events/:eventId',
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                },
                searchEvents: {
                    url: apiConfig.url + 'api/v1/events?page=:page&pageSize=:pageSize&cityName=:cityName&eventName=:eventName',
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer '
                    },
                    interceptor: {
                        response: function (response) {
                            var result = response.resource;
                            result.$status = response.status;
                            return result;
                        }
                    }
                }
            });
        }
    }
})();