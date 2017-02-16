(function () {
    angular.module('account')
        .controller('AccountController', ['$uibModal', '$log', 'changePasswordService', AccountController]);

    function AccountController($uibModal, $log, changePasswordService) {
        var self = this;

        self.openChangePasswordModal = changePasswordService.openModal;
        self.openAddClientModal = openAddClientModal;
        self.openResetClientSecretModal = openResetClientSecretModal;

        function openAddClientModal() {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'addClient',
                size: 'lg'
            });

            modalInstance.result.then(function (response) {
                //$log.info(response);
            }, function (response) {
                //$log.info(response);
            });
        }

        function openResetClientSecretModal() {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'resetClientSecret',
                size: 'lg'
            });

            modalInstance.result.then(function (response) {
                //$log.info(response);
            }, function (response) {
                //$log.info(response);
            });
        }
    }
})();