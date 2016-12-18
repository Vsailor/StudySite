angular.module('SmokingRoom', ['ngSanitize', 'ngCookies'])
.controller('SmokingRoomController', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {

    var loadMessages = function () {
        var date = new Date();
        var dateString = date.getFullYear() + "-" + (date.getMonth()+1) + "-" + date.getDate() + "-" + date.getHours() + "-" + date.getMinutes() + "-" + date.getSeconds() + "-" + date.getMilliseconds()
        $http({
            method: 'GET',
            url: 'api/Messages?dateTime=' + dateString + '&count=10'
        }).then(function (response) {
            return response.data;
        })
            .then(function (response) {
                $scope.messages = angular.fromJson(response);
                angular.forEach($scope.messages, function (value, key) {
                    var date = value.date.split("-");
                    $scope.messages[key].dateTime = (date[2].length == 1 ? "0" + date[2] : date[2]) + "." + (date[1].length == 1 ? "0" + date[1] : date[1]) + "." + (date[0].length == 1 ? "0" + date[0] : date[0]) + ", " + (date[3].length == 1 ? "0" + date[3] : date[3]) + ":" + (date[4].length == 1 ? "0" + date[4] : date[4]) + ":" + (date[5].length == 1 ? "0" + date[5] : date[5]);
                });
            });
    };

    loadMessages();


    var userNameCookieKey = 'userName';
    var userNameCookie = $cookies.get(userNameCookieKey);
    if (userNameCookie != undefined) {
        $scope.user = {
            name: userNameCookie
        }
    }
    $scope.SendMessage = function () {
        $cookies.put(userNameCookieKey, $scope.user.name);
        if ($scope.user.newMessage == null || $scope.user.newMessage == undefined) {
            return;
        }

        var postData = {
            userName: $scope.user.name,
            message: $scope.user.newMessage
        }

        $http.post('api/Messages', postData).then(function(response) {
            loadMessages();
            $scope.user.newMessage = undefined;
        });
    }
}]);

