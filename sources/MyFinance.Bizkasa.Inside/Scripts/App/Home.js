
app.service("Homeservice", function ($http) {  
    this.Login = function (user) {
        var request = $http({
            method: "post",
            url: "/Home/Login",
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify({ email: user.Email, password: user.Password })
        });
        return request;
    };
});


app.controller("HomeController", function ($scope, Homeservice) {

    //GetAllRecords();
    $scope.Login = function () {
    if (!$scope.User || !$scope.User.Email || !$scope.User.Password)
    {
        toastr.error("Bạn chưa nhập đủ thông tin");
        return;
    }
    app.showWait(true);
    var promiseGet = Homeservice.Login($scope.User);
    promiseGet.then(function (pl) {
        if (pl.data.IsError) {
            toastr.error(pl.data.Message)
        }
        else {
            window.location.href = "/Home/";
        }

        app.showWait(false);
    },
    function (errorPl) {
    });
};
});