(function() {
    var parserApp = angular.module("parserApp", ["ngRoute"]);
    parserApp.config(function($routeProvider) {
        $routeProvider.when("/Products/:id",
            {
                templateUrl: "../Products/ProductDetails",
                controller: "detailsController"
            });
        $routeProvider.when("/",
            {
                templateUrl: "../Products/Products",
                controller: "productsController"
            });
        $routeProvider.otherwise({ redirectTo: "/" });
    });
    parserApp.controller("searchForm",
        function($scope, $http) {
            $scope.Parse = function(link) {
                var linkObj = { RequestString: link };
                $http({ method: "POST", url: "../api/Product/Parse/", data: linkObj }).then(
                    function() {
                    });
            };
            $scope.Refresh = function(link) {
                $http({ method: "GET", url: "../api/Product/Refresh/" }).then(function successCallback(response) {
                });
            };
        });
    parserApp.controller("detailsController",
        function ($scope, $http, $routeParams) {
            
            $scope.$on("$routeChangeSuccess",
                function() {
                    var id = $routeParams["id"];
                    if (id !== undefined) {
                        $http({ method: "GET", url: "../api/Products/", params: { "id": id } }).then(
                            function(response) {
                                $scope.product = response.data;
                            });
                    }
                });
        });
    parserApp.controller("productsController",
        function ($scope, $http) {
            function chunk(arr, size) {
                var newArr = [];
                for (var i = 0; i < arr.length; i += size) {
                    newArr.push(arr.slice(i, i + size));
                }
                return newArr;
            }
            $scope.$on("$routeChangeSuccess",
                function() {
                    $http({ method: "GET", url: "../api/Products/" }).then(
                        function(response) {
                            $scope.products = response.data;
                            $scope.rows = chunk($scope.products, 3);
                        });
                });
        });

}())