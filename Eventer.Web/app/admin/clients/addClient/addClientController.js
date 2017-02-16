(function () {
    angular.module('adminAddClient')
        .controller('AdminAddClientController', ['$location', 'apiService', 'authService', AdminAddClientController]);

    function AdminAddClientController($location, apiService, authService) {
        var self = this;

        if (authService.userIsAllowed(['Administrators'])) {
            self.client = {
                username: '',
                refreshTokenLifeTime: null,
                isJsClient: false,
                jsClientUrl: '',
                active: true
            };

            self.refreshTokenValid = refreshTokenValid;
            self.refreshTokenInvalid = refreshTokenInvalid;
            self.refreshTokenShowError = refreshTokenShowError;

            self.usernameValid = usernameValid;
            self.usernameInvalid = usernameInvalid;
            self.usernameShowError = usernameShowError;


            self.jsClientValid = jsClientValid;
            self.jsClientInvalid = jsClientInvalid;
            self.jsClientShowValidIcon = jsClientShowValidIcon;
            self.jsClientShowInvalidIcon = jsClientShowInvalidIcon;
            self.jsClientShowError = jsClientShowError;
            self.clearJsClientUrl = clearJsClientUrl;

            self.reset = reset;
            self.addClient = addClient;
            self.addClientResponse = addClientResponse;
            self.close = close;


            function refreshTokenValid(form) {
                return form.RefreshTokenLifeTime.$valid && form.RefreshTokenLifeTime.dirty;
            }

            function refreshTokenInvalid(form) {
                return form.$submitted && form.RefreshTokenLifeTime.$invalid || form.RefreshTokenLifeTime.$invalid &&
                form.RefreshTokenLifeTime.$dirty;
            }

            function refreshTokenShowError(form) {
                return form.$submitted && form.RefreshTokenLifeTime.$invalid || form.RefreshTokenLifeTime.$touched && form.RefreshTokenLifeTime.$dirty;
            }

            function usernameValid(form) {
                return form.Username.$valid && form.Username.dirty;
            }

            function usernameInvalid(form) {
                return form.$submitted && form.Username.$invalid || form.Username.$invalid &&
                form.Username.$dirty;
            }

            function usernameShowError(form) {
                return form.$submitted && form.Username.$invalid || form.Username.$touched && form.Username.$dirty;
            }

            function jsClientValid(form) {
                return form.$submitted && form.$valid || self.client.isJsClient && form.JsClientUrl.$valid && form.JsClientUrl.$touched && form.JsClientUrl.$dirty;
            }

            function jsClientInvalid(form) {
                return self.client.isJsClient && form.$submitted && form.JsClientUrl.$invalid ||
                    self.client.isJsClient && form.JsClientUrl.$invalid && form.JsClientUrl.$dirty &&
                    form.JsClientUrl.$touched;
            }

            function jsClientShowValidIcon(form) {
                return form.JsClientUrl.$valid && form.JsClientUrl.$dirty;
            }

            function jsClientShowInvalidIcon(form) {
                return form.$submitted && form.JsClientUrl.$invalid || form.JsClientUrl.$invalid && form.JsClientUrl.$dirty;
            }

            function jsClientShowError(form) {
                return form.$submitted || form.JsClientUrl.$touched && form.JsClientUrl.$dirty;
            }

            function clearJsClientUrl(form) {
                self.client.jsClientUrl = null;
                form.JsClientUrl.$setPristine();
                form.JsClientUrl.$setUntouched();
            }

            function reset(form) {
                if (form) {
                    self.client = {
                        username: '',
                        refreshTokenLifeTime: null,
                        isJsClient: false,
                        jsClientUrl: '',
                        active: true
                    };
                    self.addClientError = '';
                    self.addClientSuccess = '';
                    form.$setPristine();
                    form.$setUntouched();
                }
            }

            function addClient(form, client) {
                self.addClientError = '';
                self.addClientSuccess = '';

                var postData = {
                    username: client.username,
                    active: client.active,
                    refreshTokenLifeTime: client.refreshTokenLifeTime
                };

                if (client.isJsClient) {
                    postData.applicationType = 0;
                    postData.allowedOrigin = client.jsClientUrl;
                }
                else {
                    postData.applicationType = 1;
                }

                apiService.client().postClient({}, postData).$promise.then(success).catch(error);

                function success(response) {
                    console.log(response);
                    self.addClientSuccess = 'Client successfuly added.';
                    form.$setPristine();
                    form.$setUntouched();
                }

                function error(response) {
                    console.log(response);
                    self.addClientError = response.data.message;
                }
            }

            function addClientResponse() {
                return !!(self.addClientSuccess || self.addClientError);
            }

            function close() {
                self.dismiss();
            };

        } else {
            $location.path('/forbidden');
        }
    }
})();