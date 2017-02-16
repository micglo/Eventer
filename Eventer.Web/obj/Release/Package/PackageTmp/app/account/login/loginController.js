(function () {
    angular.module('login')
        .controller('LoginController', ['$scope', 'loginService', LoginController]);

    function LoginController($scope, loginService) {
        var self = this;

        self.user = {
            login: '',
            password: '',
            rememberMe: false
        };
        self.loginError = '';

        self.loginValid = loginService.loginValid;
        self.loginInvalid = loginService.loginInvalid;
        self.loginShowError = loginService.loginShowError;

        self.passwordValid = loginService.passwordValid;
        self.passwordInvalid = loginService.passwordInvalid;
        self.passwordShowError = loginService.passwordShowError;

        self.reset = reset;
        self.login = login;
        self.close = close;


        function reset(form) {
            self.loginError = '';
            if (form) {
                self.user = {
                    login: '',
                    password: null,
                    rememberMe: false
                };
                form.$setPristine();
                form.$setUntouched();
            }
        }

        function login(form, user) {
            self.loginError = '';
            loginService.login(user).$promise.then(success).catch(error);

            function success(response) {
                form.$setPristine();
                form.$setUntouched();
                self.close();
            }

            function error(response) {
                console.log('LoginControllerError', response);
                if (response && response.data)
                    self.loginError = response.data.error_description;
            }
        }

        function close() {
            self.dismiss();
        };
    }
})();