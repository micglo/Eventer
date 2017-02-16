(function () {
    angular.module('account')
        .directive("passwordCheck", passwordCheck);

    function passwordCheck() {
        return {
            require: "ngModel",
            scope: {
                otherModelValue: "=passwordCheck"
            },
            restrict: 'A',
            link: link
        };

        function link(scope, element, attributes, ngModel) {

            ngModel.$validators.passwordCheck = checkValue;

            scope.$watch("otherModelValue", validate);

            function checkValue(modelValue) {
                return modelValue == scope.otherModelValue;
            }

            function validate() {
                ngModel.$validate();
            }
        }
    }
})();