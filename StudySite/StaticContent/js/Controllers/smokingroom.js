angular.module('SmokingRoom', ['ngSanitize', 'ngCookies'])
.controller('SmokingRoomController', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
    var autoUpdaterStarted = false;
    var updateFunction =  function() {
        var intervalId = window.setInterval(function () {
            loadMessages($scope.messages.length);
        }, 5000);
    }

    var loadMessages = function (count) {
        $http({
            method: 'GET',
            url: 'api/Messages?count='+ count + '&timeZone=' + (new Date().getTimezoneOffset() / 60)
        }).then(function (response) {
            return response.data;
        })
            .then(function (response) {
                $scope.messages = angular.fromJson(response);
                angular.forEach($scope.messages, function (value, key) {
                    var text = value.text.split('\n');
                    $scope.messages[key].text = text;
                });
                if ($scope.messages.length % 10 != 0) {
                    $scope.showNextMessagesButtonVisibility = false;
                }
                else {
                    $scope.showNextMessagesButtonVisibility = true;
                }

                if (!autoUpdaterStarted) {
                    updateFunction();
                    autoUpdaterStarted = true;
                }
            });
    };

    $scope.LoadNextTenMessages = function() {
        $http({
            method: 'GET',
            url: 'api/Messages?count=10&timeZone=' + (new Date().getTimezoneOffset() / 60) + '&dateTime=' + $scope.messages[$scope.messages.length - 1].unixTime
        }).then(function (response) {
            return response.data;
        })
           .then(function (response) {
               $scope.messages = $scope.messages.concat(angular.fromJson(response));
               if ($scope.messages.length % 10 != 0) {
                   $scope.showNextMessagesButtonVisibility = false;
               }
               else {
                   $scope.showNextMessagesButtonVisibility = true;
               }
            });

    }

    loadMessages(10);


    var userNameCookieKey = 'userName';
    var userNameCookie = $cookies.get(userNameCookieKey);
    if (userNameCookie != undefined) {
        $scope.user = {
            name: userNameCookie
        }
    }
    $scope.SendMessage = function () {
        if ($scope.user == null ||
            $scope.user == undefined ||
            $scope.user.newMessage == null ||
            $scope.user.newMessage == undefined ||
            $scope.user.name == null ||
            $scope.user.name == undefined) {
            return;
        }

        $cookies.put(userNameCookieKey, $scope.user.name);


        var postData = {
            userName: $scope.user.name,
            message: $scope.user.newMessage
        }

        $http.post('api/Messages', postData).then(function(response) {
            loadMessages($scope.messages.length+1);
            $scope.user.newMessage = undefined;
        });
    }
}]);

