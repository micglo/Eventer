(function () {
    angular.module('resetClientSecret')
        .controller('ResetClientSecretController', ['$location', 'authService', 'apiService', ResetClientSecretController]);

    function ResetClientSecretController($location, authService, apiService) {
        var self = this;

        if (authService.userIsAllowed(['Users'])) {
            self.noNativeClients = true;

            self.resetClientSecret = resetClientSecret;
            self.resetClientSecretResponse = resetClientSecretResponse;
            self.close = close;

            apiService.client().getMyClients().$promise.then(success).catch(error);

            function success(response) {
                var items = response.items;

                var clients = [];
                angular.forEach(items, function (value) {
                    if (value.active === true && value.applicationType === 'NativeConfidential') {
                        clients.push(value.id);
                    }
                });
                
                if (clients.length) {
                    self.noNativeClients = false;                   
                    var clientsToSelectList = [];
                    
                    for (var i = 0; i < clients.length; i++) {
                        var client = {
                            id: i+1,
                            name: clients[i]
                        };
                        clientsToSelectList.push(client);
                    }
                    self.data = {
                        availableOptions: clientsToSelectList,
                        selectedOption: { id: clientsToSelectList[0].id, name: clientsToSelectList[0].name }
                    };
                    
                }
            }

            function error(response) {
                if (response)
                    self.resetClientSecretError = response.data.message;
            }


            function resetClientSecret(form, selectedItem) {
                var postData = {
                    clientId: selectedItem.name
                };
                apiService.client().resetMyClientSecret({}, postData).$promise.then(success).catch(error);

                function success(response) {
                    self.resetClientSecretSuccess = 'client_secret changed. Check your email for details.';
                    form.$setPristine();
                    form.$setUntouched();
                }

                function error(response) {
                    if (response)
                        self.resetClientSecretError = response.data.message;
                }
            }

            function resetClientSecretResponse() {
                return !!(self.resetClientSecretSuccess || self.resetClientSecretError);
            }

            function close() {
                self.dismiss();
            };

        } else {
            $location.path('/');
        }
    }
})();