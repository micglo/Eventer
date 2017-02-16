(function () {
    angular.module('clientDetails')
        .controller('ClientDetailsController', ['$uibModal', '$log', '$routeParams', '$location', 'apiService', 'authService', ClientDetailsController]);

    function ClientDetailsController($uibModal, $log, $routeParams, $location, apiService, authService) {
        var self = this;

        if (authService.userIsAllowed(['Administrators'])) {
            var clientId = $routeParams.clientId;

            self.client = {
                isJsClient: false,
                jsClientUrl: ''
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
            self.editClient = editClient;
            self.resetClientSecret = resetClientSecret;
            self.deleteClient = deleteClient;


            getClientDetails();

            function getClientDetails() {
                self.client = apiService.client().getClient({ clientId: clientId }).$promise.then(success).catch(error);
            }

            function success(response) {
                self.client = response;
                if (response.applicationType === 0) {
                    self.client.isJsClient = true;
                    self.client.jsClientUrl = response.allowedOrigin;
                }
            }

            function error(response) {
                self.clientDetailsError = response.data.message;
            }

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
                    self.clientDetailsError = '';
                    form.$setPristine();
                    form.$setUntouched();
                }
                getClientDetails();
            }

            function editClient(form, client) {
                self.clientDetailsError = '';

                var postData = {
                    id: client.id,
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
                    postData.allowedOrigin = '*';
                }

                apiService.client().putClient({ clientId: clientId }, postData).$promise.then(editSuccess).catch(editError);

                function editSuccess(response) {
                    reset(form);
                    openInfoModal('Client successfuly edited.');
                }

                function editError(response) {
                    openInfoModal(response.data.message);
                }
            }

            function resetClientSecret(form, client) {
                self.clientDetailsError = '';

                var postData = {
                    clientId: client.id
                };
                apiService.client().resetClientSecret({}, postData).$promise.then(resetClientSecretSuccess).catch(resetClientSecretError);

                function resetClientSecretSuccess(response) {
                    form.$setPristine();
                    form.$setUntouched();
                    openInfoModal('client_secret changed. Check your email for details.');
                }

                function resetClientSecretError(response) {
                    openInfoModal(response.data.message);
                }
            }

            function deleteClient() {
                apiService.client().deleteClient({ clientId: clientId }).$promise.then(deleteClientSuccess).catch(deletetClientError);

                function deleteClientSuccess(response) {
                    openInfoModal('Client successfuly deleted');
                    $location.path('/admin/clients');
                }

                function deletetClientError(response) {
                    openInfoModal(response.data.message);
                }
            }


            function openInfoModal(msg) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: 'app/admin/clients/clientDetails/infoModal.html',
                    controller: 'InfoModalInstanceController',
                    controllerAs: 'ctrl',
                    resolve: {
                        message: function () {
                            return msg;
                        }
                    }
                });

                modalInstance.result.then(function () {
                    //$log.info();
                }, function () {
                    //$log.info();
                });
            };

        } else {
            $location.path('/forbidden');
        }
    }
})();