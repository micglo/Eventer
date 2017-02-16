(function () {
    angular.module('addClient')
        .controller('AddClientController', ['$location', 'authService', 'apiService', AddClientController]);

    function AddClientController($location, authService, apiService) {
        var self = this;

        if (authService.userIsAllowed(['Users'])) {
            self.client = {
                isJsClient: false,
                jsClientUrl: null
            };

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

            function jsClientValid(form) {
                return self.client.isJsClient && form.JsClientUrl.$valid;
            }

            function jsClientInvalid(form) {
                return self.client.isJsClient && form.$submitted && form.JsClientUrl.$invalid ||
                    self.client.isJsClient && form.JsClientUrl.$invalid && form.JsClientUrl.$dirty;
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
                    self.addClientSuccess = '';
                    self.addClientError = '';
                    self.client = {
                        isJsClient: false,
                        jsClientUrl: null
                    };
                    form.$setPristine();
                    form.$setUntouched();
                }
            }

            function addClient(form, client) {
                self.addClientSuccess = '';
                self.addClientError = '';

                var postData = {};

                if (client.isJsClient) {
                    postData.isJavaScriptClient = client.isJsClient;
                    postData.allowedOrigin = client.jsClientUrl;
                }
                else {
                    postData.isJavaScriptClient = client.isJsClient;
                }


                apiService.client().addClient({}, postData).$promise.then(success).catch(error);

                function success(response) {
                    self.addClientSuccess = 'New client added. Check your email for client details.';
                    self.client = {
                        isJsClient: false,
                        jsClientUrl: null
                    };
                    form.$setPristine();
                    form.$setUntouched();
                }

                function error(response) {
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
            $location.path('/');
        }
        
    }
})();