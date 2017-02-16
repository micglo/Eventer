(function () {
    angular.module('signUp')
        .factory('signUpService', ['$uibModal', '$log', 'apiService', signUpService]);

    function signUpService($uibModal, $log, apiService) {
        var self = this;

        self.signUpService = {
            openModal: openModal,
            emailValid: emailValid,
            emailInvalid : emailInvalid,
            emailShowError : emailShowError,
            passwordValid : passwordValid,
            passwordInvalid : passwordInvalid,
            confirmPasswordValid : confirmPasswordValid,
            confirmPasswordInvalid : confirmPasswordInvalid,
            passwordShowError : passwordShowError,
            confirmPasswordShowError : confirmPasswordShowError,
            signUp : signUp
        };

        return self.signUpService;

        function openModal() {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'signUp',
                size: 'lg'
            });

            modalInstance.result.then(function (response) {
                //$log.info(response);
            }, function (response) {
                //$log.info(response);
            });
        }

        function emailInvalid(form) {
            return form.$submitted && form.Email.$invalid || form.Email.$invalid && form.Email.$dirty;
        }

        function emailValid(form) {
            return form.Email.$valid && form.Email.$dirty;
        }

        function emailShowError(form) {
            return form.$submitted || form.Email.$touched && form.Email.$dirty;
        }

        function passwordValid(form) {
            return form.Password.$valid;
        }

        function passwordInvalid(form) {
            return form.$submitted && form.Password.$invalid || form.Password.$invalid &&
                form.Password.$dirty;
        }

        function confirmPasswordValid(form) {
            return form.ConfirmPassword.$valid;
        }

        function confirmPasswordInvalid(form) {
            return form.$submitted && form.ConfirmPassword.$invalid || form.ConfirmPassword.$invalid && form.ConfirmPassword.$dirty;
        }

        function passwordShowError(form) {
            return form.$submitted || form.Password.$touched && form.Password.$dirty;
        }

        function confirmPasswordShowError(form) {
            return form.$submitted || form.ConfirmPassword.$touched && form.ConfirmPassword.$dirty;
        }

        function signUp(user) {
            return apiService.account().register({}, user, success, error);

            function success(response) {
                return response;
            }

            function error(response){
                console.log('signUpServiceError', response);
            }
        }
    }
})();