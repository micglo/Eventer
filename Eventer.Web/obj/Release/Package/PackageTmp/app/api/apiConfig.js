(function () {
    angular.module('api')
        .constant('apiConfig',
        {
            'url': 'https://eventerapi.azurewebsites.net/',
            'client_id': '7b3c30f3-e54a-4ea9-8bf7-a25d8ddc2537', //prod
            //'client_id': '7383729f-ffbb-4402-9847-4a8e9998d7d3', //dev
            'grant_type_token': 'password',
            'grant_type_refresh': 'refresh_token',
            'username': 'eventerspa@gmail.com',
            'password': 'P@$$w0rd1'
        });
})();