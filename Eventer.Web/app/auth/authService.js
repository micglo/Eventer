(function () {
    angular.module('auth')
        .factory('authService', ['$rootScope', '$location', '$cookies', '$sessionStorage', '$timeout', authService]);

    function authService($rootScope, $location, $cookies, $sessionStorage, $timeout) {
        return {
            userName: userName,
            isLoggedIn: isLoggedIn,
            setUser: setUser,
            currentUser: currentUser,
            logOut: logOut,
            init: init,
            checkAuthentication: checkAuthentication,
            userIsAllowed: userIsAllowed
        };

        function userName() {
            if (isLoggedIn())
                return currentUser().userName;

            return '';
        }
        function isLoggedIn() {
            var user = $cookies.getObject('user');

            if ($sessionStorage.user)
                return true;
            else if (user && !$sessionStorage.user) {
                setUser(user);
                return true;
            }
            return false;
        }
        
        function setUser(data) {
            clearUserData();

            $sessionStorage.user = data;
            $rootScope.user = data;

            if(data.rememberMe){
                $cookies.putObject('user', data);
            }
        }

        function currentUser() {
            return $sessionStorage.user;
        }

        function logOut() {
            clearUserData();
            $location.path('/');
        }

        function init(){
            if (isLoggedIn()){
                $rootScope.user = currentUser();
            }
        }

        function checkAuthentication(view) {
            if (!view.requiresAuthentication && !view.permissions) {
                return true;
            }
            else if (view.requiresAuthentication && !view.permissions) {
                return isLoggedIn();
            }
            else {
                if (!isLoggedIn()) {
                    return false;
                }
                if (!userHasPermission(view.permissions))
                    $location.path('/forbidden');
                return true;
            }
        }

        function userIsAllowed(permissions) {
            if (!isLoggedIn()) {
                return false;
            }
            return userHasPermission(permissions);
        }

        function userHasPermission(permissions) {
            var found = false;

            if (!$sessionStorage.user.permissions) {
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    checkPermissions();
                }, 500);
            } else {
                checkPermissions();
            }

            function checkPermissions() {
                angular.forEach(permissions, function (permission, index) {
                    if ($sessionStorage.user.permissions.indexOf(permission) >= 0) {
                        found = true;
                        return;
                    }
                });
            }

            return found;
        }

        function clearUserData() {
            delete $sessionStorage.user;
            delete $rootScope.user;
            $cookies.remove('user');
        }
    }
})();