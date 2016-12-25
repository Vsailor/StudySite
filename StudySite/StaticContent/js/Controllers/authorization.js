angular.module('Authorization', ['ngCookies'])
.controller('AuthController', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
    var checkAuthorize = function() {
        var guid = $cookies.get("guid");
        if (guid != undefined) {
            $scope.authenticated = true;
        } else {
            $scope.authenticated = false;
        }
    }
}]);

