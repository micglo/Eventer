(function () {
    angular.module('menu')
        .controller('MenuController', ['$rootScope', '$location', 'authService', 'loginService', 'signUpService', MenuController]);

    function MenuController($rootScope, $location, authService, loginService, signUpService) {
        var self = this;
        self.logOut = authService.logOut;
        
        self.userName = userName;
        self.isAllowed = isAllowed;
        self.openLoginModal = loginService.openModal;
        self.openSignUpModal = signUpService.openModal;
        

        function userName() {
            return authService.userName();
        }

        function isAllowed(permissions) {
            return authService.userIsAllowed(permissions);
        }
    }
})();