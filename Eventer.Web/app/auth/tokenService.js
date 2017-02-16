(function() {
    angular.module('auth')
        .factory('tokenService', ['$httpParamSerializer', 'apiService', 'apiConfig', 'authService', tokenService]);

    function tokenService($httpParamSerializer, apiService, apiConfig, authService) {
        return {
            getToken: getToken,
            refreshToken: refreshToken
        };

        function getToken(user) {
            return apiService.token()
                .getToken({},
                $httpParamSerializer({
                    grant_type: apiConfig.grant_type_token,
                    username: user.username,
                    password: user.password,
                    client_id: apiConfig.client_id
                }), success, error);

            function success(response) {
                var userData = {
                    access_token: response.access_token,
                    refresh_token: response.refresh_token,
                    userName: user.username,
                    rememberMe: user.rememberMe
                };
                authService.setUser(userData);

                apiService.roles().getMyRoles().$promise.then(rolesSuccess, rolesError);

                function rolesSuccess(rolesResponse) {
                    userData.permissions = rolesResponse;
                    authService.setUser(userData);
                }

                function rolesError(rolesResponse) {
                    console.log('tokenServicerRoleError', rolesResponse);
                }

                return response;
            }

            function error(response) {
                console.log('tokenServicerError', response);
                return response;
            }
        }

        function refreshToken(user) {
            return apiService.token()
                .getToken({},
                $httpParamSerializer({
                    grant_type: apiConfig.grant_type_refresh,
                    refresh_token: user.refresh_token,
                    client_id: apiConfig.client_id
                }), success, error);

            function success(response) {
                var userData = {
                    access_token: response.access_token,
                    refresh_token: response.refresh_token,
                    userName: user.username,
                    rememberMe: user.rememberMe
                };
                authService.setUser(userData);

                apiService.roles().getMyRoles().$promise.then(rolesSuccess, rolesError);

                function rolesSuccess(rolesResponse) {
                    userData.permissions = rolesResponse;
                    authService.setUser(userData);
                }

                function rolesError(rolesResponse) {
                    console.log('refreshTokenServicerRoleError', rolesResponse);
                }

                return response;
            }

            function error(response) {
                console.log('refreshTokenServicerError', response);
                return response;
            }
        }
    }
})();