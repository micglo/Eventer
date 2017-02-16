(function () {
    angular.module('home')
        .controller('HomeController', ['loginService', 'signUpService', HomeController]);

    function HomeController(loginService, signUpService) {
        var self = this;

        self.openLoginModal = loginService.openModal;
        self.openSignUpModal = signUpService.openModal;
    }
})();