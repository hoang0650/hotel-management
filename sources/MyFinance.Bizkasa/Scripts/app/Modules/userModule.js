var app;
(function () {
    app = angular.module("FontEndModule", []);
    app.showWait = function (val) {
        if (val)
            $('#loading').show();
        else
            $('#loading').hide();
    }
    app.service("LoginService", function ($http) {
        this.Login = function (user) {
            var request = $http({
                method: "post",
                url: "/CPanelAdmin/Home/Login",
                contentType: "application/json; charset=UTF-8",
                data: JSON.stringify({ email: user.Email, password: user.Password })
            });
            return request;
        };
    });
    app.controller("LoginController", function ($scope, LoginService) {
        $scope.Login = function () {
            if (!$scope.User || !$scope.User.Email || !$scope.User.Password)
            {
                toastr.error("Bạn chưa nhập đủ thông tin");
                return;
            }
            app.showWait(true);
            var promiseGet = LoginService.Login($scope.User);
            promiseGet.then(function (pl) {
                if (pl.data.IsError) {
                    toastr.error(pl.data.Message)
                }
                else {
                    window.location.href = "/CPanelAdmin/Home/";
                }

                app.showWait(false);
            },
            function (errorPl) {
            });
        };
    });
})()