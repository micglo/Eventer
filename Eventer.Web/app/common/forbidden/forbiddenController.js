(function () {
    angular.module('forbidden')
        .controller('ForbiddenController', [ForbiddenController]);

    function ForbiddenController() {
        var self = this;

        self.text = 'You have no access to this content.';
    }
})();