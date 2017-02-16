(function() {
    angular.module('clientDetails')
        .controller('InfoModalInstanceController', ['$uibModalInstance', 'message', InfoModalInstanceController]);

    function InfoModalInstanceController($uibModalInstance, message) {
        var self = this;

        self.message = message;

        self.ok = function () {
            $uibModalInstance.close();
        };

        self.close = function () {
            $uibModalInstance.dismiss();
        };
    }
})();