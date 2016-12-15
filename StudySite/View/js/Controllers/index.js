angular.module('Index', ['ngSanitize'])
.controller('IndexController', ['$scope', '$http', function ($scope, $http) {
        var loadNews = function() {
            var headers = {
                'Access-Control-Allow-Origin': '*'
            };

            var host = getServer();

            $http({
                    headers: headers,
                    method: 'GET',
                    url: host+'api/Index'
                }).then(function(response) {
                    return response.data;
                })
                .then(function(response) {
                    $scope.newsHtml = response;
                });
        };

        loadNews();
}]);

