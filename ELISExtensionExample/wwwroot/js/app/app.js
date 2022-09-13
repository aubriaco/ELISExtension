var app = angular.module('app', ['ngRoute', 'ngSanitize', 'angularFileUpload', '720kb.datepicker', 'ui.mask', 'ngTagsInput']);
app.config(['$routeProvider',
    function ($routeProvider) {
        //Routes
        $routeProvider
            .when('/index', { templateUrl: 'views/index.html' })
            .when('/login', { templateUrl: 'views/login.html' })
            .when('/order/:id', { templateUrl: 'views/order.html' })

            .otherwise({ redirectTo: 'index' });
    }]);


app.controller('Root', ['$scope', '$http', '$routeParams', '$sce', '$location', '$timeout',
    function ($scope, $http, $routeParams, $sce, $location, $timeout) {
        $http.defaults.headers.post["Content-Type"] = "application/json";

        $scope.checkCurrent = function (callback, errorCallback) {
            $http.get('api/auth/current').then(function (r) {
                if (r.data.errorCode) {
                    $location.path('/login');
                    if (errorCallback)
                        errorCallback();
                }
                else {
                    $scope.User = r.data.item;
                    if (callback)
                        callback($scope.User);
                }
            });
        };

        $scope.set = function (a, b) {
            $scope[a] = b;
        };

        $scope.logout = function () {
            $http.post('api/auth/logout').then(function (r) {
                if (r.data.errorCode) {
                    alert(r.data.errorMessage);
                }
                else {
                    $location.path('/login');
                    $scope.User = false;
                }
            });
        };

        $scope.checkCurrent();

        $scope.requireLogin = function (callback) {
            $scope.checkCurrent(callback, function () {
                $location.path('/login');
            });
        };


    }]);




app.controller('Login', ['$scope', '$http', '$routeParams', '$sce', '$location', '$timeout',
    function ($scope, $http, $routeParams, $sce, $location, $timeout) {

        $scope.Auth = {};

        $scope.login = function () {
            $http.post('api/auth/login', $scope.Auth).then(function (r) {
                if (r.data.errorCode) {
                    alert(r.data.errorMessage);
                }
                else {
                    $location.path('/index');
                    $scope.set('User', true);
                }
            });
        };
        
    }]);


app.controller('Main', ['$scope', '$http', '$routeParams', '$sce', '$location', '$timeout',
    function ($scope, $http, $routeParams, $sce, $location, $timeout) {

        $scope.requireLogin(function () {
            $http.get('api/lab/order/list').then(function (r) {
                if (r.data.errorCode)
                    alert(r.data.errorMessage);
                else {
                    $scope.List = r.data.list;
                }
            });
        });
  

    }]);



app.controller('Order', ['$scope', '$http', '$routeParams', '$sce', '$location', '$timeout',
    function ($scope, $http, $routeParams, $sce, $location, $timeout) {

        $scope.requireLogin(function () {
            $http.post('api/lab/order/results', angular.toJson({ id: $routeParams.id })).then(function (r) {
                if (r.data.errorCode)
                    alert(r.data.errorMessage);
                else {
                    $scope.Item = r.data.item;
                }
            });
        });


    }]);
