angular.module('Index', [])
.controller('IndexController', ['$scope', '$http', function ($scope, $http) {
	$scope.submit = function () {
		alert($scope.Name);
	}
}]);