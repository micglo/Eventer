(function () {
    angular.module('api')
        .constant('apiConfig',
        {
            'url': 'https://eventerapi.azurewebsites.net/', //prod
            'client_id': '7b3c30f3-e54a-4ea9-8bf7-a25d8ddc2537', //prod
            //'url': 'https://localhost:44361/', //dev
            //'client_id': 'e7698414-4367-4065-ab3f-91334a1489d4', //dev
            'grant_type_token': 'password',
            'grant_type_refresh': 'refresh_token',
            'username': 'eventerspa@gmail.com',
            'password': 'P@$$w0rd1'
        });
})();