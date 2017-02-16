(function() {
    angular.module('core')
        .factory('myHttpInterceptor', ['$q', '$injector', '$location', 'authService', myHttpInterceptor]);

    function myHttpInterceptor($q, $injector, $location, authService) {
        return {
            request: request,
            responseError: responseError
        };

        function request(config) {
            if (config.headers.Authorization === 'Bearer ') {
                config.headers.Authorization = 'Bearer ' + authService.currentUser().access_token;
            }
            return config;
        }

        function responseError(response) {
            if (response.status === 401) {
                var authService = $injector.get('authService');
                var tokenService = $injector.get('tokenService');
                var http = $injector.get('$http');
                var deferred = $q.defer();

                var user = authService.currentUser();
                tokenService.refreshToken(user).$promise.then(deferred.resolve, deferred.reject);

                return deferred.promise.then(success).catch(error);

                function success(tokenResponse) {
                    response.config.headers.Authorization = 'Bearer ' + tokenResponse.access_token;
                    return http(response.config);
                }

                function error() {
                    authService.logOut();
                    $location.path('/');
                    deferred.reject();
                }
            }
            else if (response.status === 403) {
                $location.path('/forbidden');
            }

            return $q.reject(response);
        }
    }
})();