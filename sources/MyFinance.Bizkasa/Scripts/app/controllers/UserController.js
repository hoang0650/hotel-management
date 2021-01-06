app.controller('UserController', function ($scope, Userservice) {
    //GetAllRecords();
    //To Get All Records  
    $scope.InitUser = function GetAllRecords() {
        var promiseGet = Userservice.getUserByHotel();
        promiseGet.then(function (pl) { $scope.Users = pl.data; },
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);
              });
    };

    $scope.AddUser= function () {

        var userModel = { Name: $scope.Name, Phone: $scope.Phone, Email: $scope.Email, Password: $scope.Password };
        if (userModel.Name == null || userModel.Name == undefined) {
            toastr.error("Dữ liệu rông !"); return;
        }
        var promisePost = Userservice.AddUser(userModel);
        promisePost.then(function (pl) {
            toastr.success("Thêm mới thành công !");
        }, function (err) {
            console.log("Err" + err);
        });
    };


    $scope.Login = function () {

        var userModel = {  Email: $scope.Email, Password: $scope.Password };
        if (userModel.Email == null || userModel.Password == undefined) {
            toastr.error("Bạn chưa nhập dữ liệu !"); return;
        }
        
        var promisePost = Userservice.Login(userModel);
        promisePost.then(function (pl) {
            
            if (pl.data.HasError)
                toastr.error("Đăng nhập không thành công !");
            location.href = "/home";
           
        }, function (err) {
            console.log("Err" + err);
        });
    };

    $scope.ShowPopup = function () {
        $scope.InitUser();
        $("#userhotel").Modal('show');
    };

    $scope.MapUserHotel = function () {


    };
});