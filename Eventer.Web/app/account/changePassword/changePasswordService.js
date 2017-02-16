(function () {
    angular.module('changePassword')
        .factory('changePasswordService', ['$uibModal', '$log', 'apiService', changePasswordService]);

    function changePasswordService($uibModal, $log, apiService) {
        var self = this;

        self.changePasswordService = {
            openModal: openModal,
            oldPasswordValid: oldPasswordValid,
            oldPasswordInvalid: oldPasswordInvalid,
            oldPasswordShowError: oldPasswordShowError,

            newPasswordValid: newPasswordValid,
            newPasswordInvalid: newPasswordInvalid,
            newPasswordShowError: newPasswordShowError,

            confirmNewPasswordValid: confirmNewPasswordValid,
            confirmNewPasswordInvalid: confirmNewPasswordInvalid,
            confirmNewPasswordShowError: confirmNewPasswordShowError,
            changePassword: changePassword
        };

        return self.changePasswordService;

        function openModal() {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'changePassword',
                size: 'lg'
            });

            modalInstance.result.then(function (response) {
                //$log.info(response);
            }, function (response) {
                //$log.info(response);
            });
        }

        function oldPasswordValid(form) {
            return form.OldPassword.$valid;
        }

        function oldPasswordInvalid(form) {
            return form.$submitted && form.OldPassword.$invalid || form.OldPassword.$invalid && form.OldPassword.$dirty;
        }

        function oldPasswordShowError(form) {
            return form.$submitted || form.OldPassword.$touched && form.OldPassword.$dirty;
        }

        function newPasswordValid(form) {
            return form.NewPassword.$valid;
        }

        function newPasswordInvalid(form) {
            return form.$submitted && form.NewPassword.$invalid || form.NewPassword.$invalid && form.NewPassword.$dirty;
        }

        function newPasswordShowError(form) {
            return form.$submitted || form.NewPassword.$touched && form.NewPassword.$dirty;
        }

        function confirmNewPasswordValid(form) {
            return form.ConfirmNewPassword.$valid;
        }

        function confirmNewPasswordInvalid(form) {
            return form.$submitted && form.ConfirmNewPassword.$invalid || form.ConfirmNewPassword.$invalid &&
                form.ConfirmNewPassword.$dirty;
        }

        function confirmNewPasswordShowError(form) {
            return form.$submitted || form.ConfirmNewPassword.$touched && form.ConfirmNewPassword.$dirty;
        }

        function changePassword(user) {
            var postData = {
                oldPassword: user.oldPassword,
                newPassword: user.newPassword,
                confirmPassword: user.confirmNewPassword
            };

            return apiService.account().changePassword({}, postData, success, error);

            function success(response) {
                return response;
            }

            function error(response){
                console.log('changePasswordServiceError', response);
            }
        }
    }
})();