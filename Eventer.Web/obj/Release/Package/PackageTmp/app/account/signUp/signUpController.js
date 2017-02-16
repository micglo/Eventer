(function () {
    angular.module('signUp')
        .controller('SignUpController', ['signUpService', SignUpController]);

    function SignUpController(signUpService) {
        var self = this;

        self.user = {
            email: '',
            password: '',
            confirmPassword: ''
        };

        self.signUpResponseSuccess = '';
        self.signUpResponseError = '';

        self.emailValid = signUpService.emailValid;
        self.emailInvalid = signUpService.emailInvalid;
        self.emailShowError = signUpService.emailShowError;

        self.passwordValid = signUpService.passwordValid;
        self.passwordInvalid = signUpService.passwordInvalid;
        self.confirmPasswordValid = signUpService.confirmPasswordValid;
        self.confirmPasswordInvalid = signUpService.confirmPasswordInvalid;
        self.passwordShowError = signUpService.passwordShowError;
        self.confirmPasswordShowError = signUpService.confirmPasswordShowError;

        self.reset = reset;
        self.signUp = signUp;
        self.signUpResponse = signUpResponse;
        self.close = close;


        function reset(form) {
            if (form) {
                self.signUpResponseSuccess = '';
                self.signUpResponseError = '';
                self.user = {
                    email: '',
                    password: '',
                    confirmPassword: ''
                };
                form.$setPristine();
                form.$setUntouched();
            }
        }

        function signUp(form, user) {
            self.signUpResponseSuccess = '';
            self.signUpResponseError = '';
            signUpService.signUp(user).$promise.then(success).catch(error);

            function success(response) {
                self.signUpResponseSuccess = 'Account created. Check your emial and active your account.'
                self.user = {
                    email: '',
                    password: '',
                    confirmPassword: ''
                };
                form.$setPristine();
                form.$setUntouched();
            }

            function error(response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                self.signUpResponseError = errors[1];
            }
        }

        function signUpResponse() {
            return !!(self.signUpResponseSuccess || self.signUpResponseError);
        }

        function close() {
            self.dismiss();
        };
    }
})();