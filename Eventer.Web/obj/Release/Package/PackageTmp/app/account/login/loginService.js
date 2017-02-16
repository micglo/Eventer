(function () {
    angular.module('login')
        .factory('loginService', ['$uibModal', '$log', 'tokenService', loginService]);

    function loginService($uibModal, $log, tokenService) {
        var self = this;

        self.loginService = {
            openModal: openModal,
            loginValid: loginValid,
            loginInvalid: loginInvalid,
            loginShowError: loginShowError,
            passwordValid: passwordValid,
            passwordInvalid: passwordInvalid,
            passwordShowError: passwordShowError,
            login: login
        };

        return self.loginService;

        function openModal() {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'login',
                size: 'lg'
            });

            modalInstance.result.then(function (response) {
                //$log.info(response);
            }, function (response) {
                //$log.info(response);
            });
        }

        function loginValid(form) {
            return form.Login.$valid && form.Login.$dirty;
        }

        function loginInvalid(form) {
            return form.$submitted && form.Login.$invalid || form.Login.$invalid && form.Login.$dirty;
        }

        function loginShowError(form) {
            return form.$submitted || form.Login.$touched && form.Login.$dirty;
        }

        function passwordValid(form) {
            return form.Password.$valid;
        }

        function passwordInvalid(form) {
            return form.$submitted && form.Password.$invalid || form.Password.$invalid &&
                form.Password.$dirty;
        }

        function passwordShowError(form) {
            return form.$submitted || form.Password.$touched && form.Password.$dirty;
        }

        function login(user) {
            var userData = {
                username: user.login,
                password: user.password,
                rememberMe: user.rememberMe
            }
            return tokenService.getToken(userData, success, error);

            function success(response) {
                return response;
            }

            function error(response){
                return response;
            }
        }
    }
})();