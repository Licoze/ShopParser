var parserApp = angular.module("parserApp", ["ngRoute"]);
parserApp.config(function ($routeProvider) {
    $routeProvider.when("/product/:id",
        {
            controller:"productsController"
        });
});
parserApp.controller("searchForm", function ($scope, $http) {
    $scope.Parse = function (link) {
        var linkObj = { RequestString: link }
        $http({ method: "POST", url: "../api/Product/Parse/", data: linkObj }).then(function successCallback(response) {
            $http({ method: "POST", dataType: "html", url: "../Product/GetAll/" }).then(function successCallback(response) {
                    $("#product-list").html(response.data);
                });
            });
        }
    $scope.Refresh = function (link) {
        $http({ method: "GET", url: "../api/Product/Refresh/" }).then(function successCallback(response) {
            $http({ method: "POST", dataType: "html", url: "../Product/GetAll/" }).then(function successCallback(response) {
                $("#product-list").html(response.data);
            });
        });
    }
    });
parserApp.controller("productsController", function ($scope, $routeParams, $http) {
    $scope.$on('$routeChangeSuccess', function () {
        var id = $routeParams["id"];
        $http({ method: "GET", url: "../Product/GetById/", params: { "id": id } }).then(function successCallback(response) {
            var product = response.data;
            $("#product-list").html(product);
        });
    });
    

});
