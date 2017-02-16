(function () {
    angular.module('changePassword')
        .controller('ChangePasswordController', ['$scope', '$timeout', '$location', 'changePasswordService', 'authService',
            ChangePasswordController]);

    function ChangePasswordController($scope, $timeout, $location, changePasswordService, authService) {
        var self = this;

        if (authService.userIsAllowed(['Users'])) {
            self.user = {
                oldPassword: '',
                newPassword: '',
                confirmNewPassword: ''
            };

            self.changePasswordSuccess = '';
            self.changePasswordError = '';

            self.oldPasswordValid = changePasswordService.oldPasswordValid;
            self.oldPasswordInvalid = changePasswordService.oldPasswordInvalid;
            self.oldPasswordShowError = changePasswordService.oldPasswordShowError;

            self.newPasswordValid = changePasswordService.newPasswordValid;
            self.newPasswordInvalid = changePasswordService.newPasswordInvalid;
            self.newPasswordShowError = changePasswordService.newPasswordShowError;

            self.confirmNewPasswordValid = changePasswordService.confirmNewPasswordValid;
            self.confirmNewPasswordInvalid = changePasswordService.confirmNewPasswordInvalid;
            self.confirmNewPasswordShowError = changePasswordService.confirmNewPasswordShowError;

            self.reset = reset;
            self.changePassword = changePassword;
            self.changePasswordResponse = changePasswordResponse;
            self.close = close;


            function reset(form) {
                if (form) {
                    self.user = {
                        oldPassword: '',
                        newPassword: '',
                        confirmNewPassword: ''
                    };
                    self.changePasswordSuccess = '';
                    self.changePasswordError = '';
                    form.$setPristine();
                    form.$setUntouched();
                }
            }

            function changePassword(form, user) {
                self.changePasswordSuccess = '';
                self.changePasswordError = '';
                changePasswordService.changePassword(user).$promise.then(success).catch(error);

                function success(response) {
                    self.user = {
                        oldPassword: '',
                        newPassword: '',
                        confirmNewPassword: ''
                    };
                    form.$setPristine();
                    form.$setUntouched();
                    self.changePasswordSuccess = 'Password changed. Logging out.';
                    startLoggingOut();
                }

                function error(response) {
                    var errors = [];
                    for (var key in response.data.modelState) {
                        for (var i = 0; i < response.data.modelState[key].length; i++) {
                            errors.push(response.data.modelState[key][i]);
                            self.changePasswordError += response.data.modelState[key][i] + '<br>'
                        }
                    }
                    self.signUpResponseError = errors[1];
                }
            }

            function changePasswordResponse() {
                return !!(self.changePasswordSuccess || self.changePasswordError);
            }

            function close() {
                self.dismiss();
            };

            function startLoggingOut() {
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    self.close();
                    logOut();
                }, 1000);
            }
            function logOut() {
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    authService.logOut();
                    $location.path('/');
                }, 1000);
            }
            
        } else {
            $location.path('/');
        }
    }
})();