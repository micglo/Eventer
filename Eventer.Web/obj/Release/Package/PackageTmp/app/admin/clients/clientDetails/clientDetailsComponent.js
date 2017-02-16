(function () {
    angular.module('clientDetails')
        .component('clientDetails',
        {
            templateUrl: 'app/admin/clients/clientDetails/clientDetails.html',
            controller: 'ClientDetailsController',
            controllerAs: 'ctrl'
        });
})();