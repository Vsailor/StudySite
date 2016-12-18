angular.module('Index', ['ngSanitize'])
.controller('IndexController', ['$scope', '$http', function ($scope, $http) {
        var loadNews = function() {
            $http({
                    method: 'GET',
                    url: 'api/Index'
                }).then(function(response) {
                    return response.data;
                })
                .then(function(response) {
                    $scope.newsHtml = response;
                });
        };

        loadNews();
}]);

