(function () {
    angular.module('changePassword')
        .directive("passwordMatch", passwordMatch);

    function passwordMatch() {
        return {
            require: "ngModel",
            scope: {
                otherModelValue: "=passwordMatch"
            },
            restrict: 'A',
            link: link
        };

        function link(scope, element, attributes, ngModel) {

            ngModel.$validators.passwordMatch = matchValue;

            scope.$watch("otherModelValue", validate);

            function matchValue(modelValue) {
                return modelValue !== scope.otherModelValue;
            }

            function validate() {
                ngModel.$validate();
            }
        }
    }
})();