(function () {
    angular.module('myClientDetails')
        .controller('MyClientDetailsController', ['$location', '$routeParams', 'apiService', 'authService', MyClientDetailsController]);

    function MyClientDetailsController($location, $routeParams, apiService, authService) {
        var self = this;
        var clientId = $routeParams.clientId;

        if (authService.userIsAllowed(['Users'])) {
            self.client = {
                isJsClient: false,
                jsClientUrl: ''
            };

            self.jsClientValid = jsClientValid;
            self.jsClientInvalid = jsClientInvalid;
            self.jsClientShowValidIcon = jsClientShowValidIcon;
            self.jsClientShowInvalidIcon = jsClientShowInvalidIcon;
            self.jsClientShowError = jsClientShowError;
            self.clearJsClientUrl = clearJsClientUrl;

            self.reset = reset;
            self.editClient = editClient;
            self.editClientResponse = editClientResponse;
            self.resetClientSecret = resetClientSecret;
            self.resetClientSecretResponse = resetClientSecretResponse;


            getClientDetails();

            function getClientDetails() {
                self.client = apiService.client().getMyClient({ clientId: clientId }).$promise.then(success).catch(error);
            }

            function success(response) {
                self.client = response;
                if (response.applicationType === 'JavaScript') {
                    self.client.isJsClient = true;
                    self.client.jsClientUrl = response.allowedOrigin;
                }
            }

            function error(response) {
                self.myClientDetailsError = response.data.message;
            }

            function jsClientValid(form) { //todo poprawiæ wszedzie form.$submitted
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
                    self.editClientSuccess = '';
                    self.editClientError = '';
                    self.resetClientSecretSuccess = '';
                    self.resetClientSecretError = '';
                    self.clientDetailsError = '';
                    form.$setPristine();
                    form.$setUntouched();
                }
                getClientDetails();
            }

            function editClient(form, client) {
                self.editClientSuccess = '';
                self.editClientError = '';
                self.resetClientSecretSuccess = '';
                self.resetClientSecretError = '';
                self.clientDetailsError = '';

                var postData = {
                    id: client.id,
                    username: authService.userName(),
                    active: client.active,
                    refreshTokenLifeTime: client.refreshTokenLifeTime
                };

                if (client.isJsClient) {
                    postData.applicationType = 0;
                    postData.allowedOrigin = client.jsClientUrl;
                }
                else {
                    postData.applicationType = 1;
                    postData.allowedOrigin = '*';
                }

                apiService.client().putMyClient({ clientId: clientId }, postData).$promise.then(editSuccess).catch(editError);

                function editSuccess(response) {
                    self.editClientSuccess = 'Client successfuly edited.';
                    form.$setPristine();
                    form.$setUntouched();
                    getClientDetails();
                }

                function editError(response) {
                    self.editClientError = response.data.message;
                }
            }

            function editClientResponse() {
                return !!(self.editClientSuccess || self.editClientError);
            }

            function resetClientSecret(form, client) {
                self.editClientSuccess = '';
                self.editClientError = '';
                self.resetClientSecretSuccess = '';
                self.resetClientSecretError = '';
                self.clientDetailsError = '';

                var postData = {
                    clientId: client.id
                };
                apiService.client().resetSecret({}, postData).$promise.then(resetClientSecretSuccess).catch(resetClientSecretError);

                function resetClientSecretSuccess(response) {
                    self.resetClientSecretSuccess = 'client_secret changed. Check your email for details.';
                    form.$setPristine();
                    form.$setUntouched();
                }

                function resetClientSecretError(response) {
                    if (response)
                        self.resetClientSecretError = response.data.message;
                }
            }

            function resetClientSecretResponse() {
                return !!(self.resetClientSecretSuccess || self.resetClientSecretError);
            }
        } else {
            $location.path('/');
        }
    }
})();