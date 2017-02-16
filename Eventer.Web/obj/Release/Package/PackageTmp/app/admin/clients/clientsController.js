(function () {
    angular.module('clients')
        .controller('ClientsController', ['$uibModal', '$log', '$location', 'authService', 'apiService', ClientsController]);

    function ClientsController($uibModal, $log, $location, authService, apiService) {
        var self = this;

        if (authService.userIsAllowed(['Administrators'])) {

            self.currentPage = 1;
            self.itemsPerPage = {
                availableOptions: [
                    { id: '1', value: '10' },
                    { id: '2', value: '25' },
                    { id: '3', value: '50' }
                ],
                selectedOption: { id: '1', value: '10' }
            };
            self.pageChanged = pageChanged;
            self.openAddClientModal = openAddClientModal;

            self.clients = [];
            self.goToClientDetails = goToClientDetails;
            pageChanged();

            function pageChanged() {
                apiService.client(self.currentPage, self.itemsPerPage.selectedOption.value)
                    .getClients()
                    .$promise.then(success)
                    .catch(error);

                function success(response) {
                    self.totalItems = response.totalNumberOfRecords;
                    self.clients = response.items;
                }

                function error(response) {
                    console.log('ClientsControllerError', response);
                }
            }

            function openAddClientModal() {
                var modalInstance = $uibModal.open({
                    animation: true,
                    component: 'adminAddClient',
                    size: 'lg'
                });

                modalInstance.result.then(function (response) {
                    //$log.info(response);
                }, function (response) {
                    //$log.info(response);
                });
            }

            function goToClientDetails(clientId) {
                $location.path('/admin/clients/' + clientId);
            }

        } else {
            $location.path('/forbidden');
        }
    }
})();