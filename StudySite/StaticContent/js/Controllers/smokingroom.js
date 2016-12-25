angular.module('SmokingRoom', ['ngSanitize', 'ngCookies'])
.controller('SmokingRoomController', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
    var autoUpdaterStarted = false;
    $scope.authenticated = false;
    var userGuidCookieKey = 'guid';

    var updateFunction =  function() {
        var intervalId = window.setInterval(function () {
            if ($scope.messages.length <= 10) {
                loadMessages(10);
            } else {
                loadMessages($scope.messages.length);
            }
        }, 5000);
    }

    $scope.Logout = function() {
        $scope.user.userGuid = undefined;
        $scope.authenticated = false;
        $cookies.remove(userGuidCookieKey);
    }

    var loadUser = function () {
        if ($scope.user == null ||
          $scope.user == undefined ||
          $scope.user.userGuid == null ||
          $scope.user.userGuid == undefined) {
            return;
        }

        $http({
            method: 'GET',
            url: 'api/auth/' + $scope.user.userGuid
        }).then(function (response) {
            return response.data;
        })
           .then(function (response) {
               $scope.userProfile = angular.fromJson(response);
           });
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
                if ($scope.messages.length % 10 != 0 || $scope.messages.length == 0) {
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
                var json = angular.fromJson(response);
                angular.forEach(json, function (value, key) {
                    var text = value.text.split('\n');
                    json[key].text = text;
                });

                $scope.messages = $scope.messages.concat(json);
                if ($scope.messages.length % 10 != 0 || $scope.messages.length == 0) {
                   $scope.showNextMessagesButtonVisibility = false;
               }
               else {
                   $scope.showNextMessagesButtonVisibility = true;
               }
            });

    }

    var UpdateAuthModel = function () {
        $scope.userGuidCookie = $cookies.get(userGuidCookieKey);
        if ($scope.userGuidCookie != undefined) {
            $scope.user = {
                userGuid: $scope.userGuidCookie
            }

            $scope.authenticated = true;
        }
    }

    loadMessages(10);
    UpdateAuthModel();
    loadUser();

    $scope.userGuidCookie = $cookies.get(userGuidCookieKey);
    if ($scope.userGuidCookie != undefined) {
        $scope.user = {
            userGuid: $scope.userGuidCookie
        }

        $scope.authenticated = true;
    }


    $scope.SendMessage = function () {
       

        if ($scope.user == null ||
            $scope.user == undefined ||
            $scope.user.newMessage == null ||
            $scope.user.newMessage == undefined ||
            $scope.user.userGuid == null ||
            $scope.user.userGuid == undefined) {
            return;
        }

        var postData = {
            userGuid: $scope.user.userGuid,
            message: $scope.user.newMessage
        }

        $http.post('api/Messages', postData).then(function(response) {
            loadMessages($scope.messages.length+1);
            $scope.user.newMessage = undefined;
        });
    }
}]);

