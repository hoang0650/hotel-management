
app.service("Homeservice", function ($http) {  

    this.RegisterHotel = function (model) {
        var request = $http({
            method: "post",
            url: "/Home/RegisterHotel",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.CheckUserExist = function (val) {
        var request = $http({
            method: "post",
            url: "/Home/CheckUserExist",
            dataType: 'json',
            data: JSON.stringify({ username: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});


app.controller("HomeController", function ($scope, Homeservice) {

    //GetAllRecords();
    //To Get All Records 

    function validateEmail(sEmail) {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (filter.test(sEmail)) {
            return true;
        }
        else {
            return false;
        }
    }

    $scope.HotelRegisterModel={
        Name:undefined,
        Email: undefined,
        IsChecked:true,
        User:{
            Email:undefined,
            Password: undefined,
            ComfirmPassword: undefined
        }
    };
    $scope.showpopup = function () {

        jQuery("#RegisterHotel").modal("show");

    };

   
  
    $scope.RegisterHotel = function () {
        if ($scope.HotelRegisterModel.Name == undefined) {
            toastr.error("Bạn chưa nhập tên khách sạn !");
            return;
        }
        if ($scope.HotelRegisterModel.NumFloors <= 0 || $scope.HotelRegisterModel.NumRooms <= 0) {
            toastr.error("Bạn chưa nhập số phòng hoặc số tầng của khách sạn !");
            return;
        }

      

        if ($scope.HotelRegisterModel.User.Email == undefined || $scope.HotelRegisterModel.User.Password == undefined) {
            toastr.error("Bạn chưa nhập đủ thông tin quản trị !");
            return;
        }
        if (!validateEmail($scope.HotelRegisterModel.User.Email)) {
            toastr.error("Email không hợp lệ. Tên đăng nhập phải là email !");
            return;
        }
        if ($scope.HotelRegisterModel.User.Password != $scope.HotelRegisterModel.User.ComfirmPassword) {
            toastr.error("Xác nhận mật khẩu không chính xác !");
            return;
        }
        if (!$scope.HotelRegisterModel.IsChecked) {
            toastr.error("Bạn chưa đồng ý với điều khoản sử dụng !");
            return;
        }


      

        var promiseGet = Homeservice.CheckUserExist($scope.HotelRegisterModel.User.Email);
        app.showWait(true);
        promiseGet.then(function (result) {
            if (!result.data.IsError) {
                if (!result.data.Data) {
                    var promiseGet = Homeservice.RegisterHotel($scope.HotelRegisterModel);
                    promiseGet.then(function (pl) {
                        if (!pl.data.IsError) {
                            toastr.success("Chúc mừng bạn đã đăng ký thành công! Bạn có thể đăng nhập để sử dụng ngay và luôn ! ");
                            window.location.href = "/CPanelAdmin/Home/";
                        }
                        app.showWait(false);
                    },
                    function (errorPl) {
                        $log.error('Some Error in Getting Records.', errorPl);
                    });
                }
                else {
                    app.showWait(false);
                    toastr.error("Tài khoản quản lý đã tồn tại ! ");
                }
                    
               
            }
          
        },
        function (errorPl) {
            $log.error('Some Error in Getting Records.', errorPl);
        });

       
    };

    $scope.pop = function showpopup(val) {
        $scope.HotelId = val;
        var promiseget = Userservice.getAllUser();
        promiseget.then(function (pl) {
            $scope.Users = pl.data;
        },
              function (errorpl) {
                  $log.error('some error in getting records.', errorpl);
              });
        $("#userhotel").modal('show');
       
    }

    $scope.UserSelected = [];
    $scope.Selected = function () {
        // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
        for (var i = 0; i < $scope.Users.length; i++) {
            if ($scope.Users[i].isChecked) {
                $scope.UserSelected.push($scope.Users[i].Id);
                //$scope.model.allItemsSelected = false;
                return;
            }
        }

        // ... otherwise check the "allItemsSelected" checkbox
       // $scope.model.allItemsSelected = true;
    }

    $scope.SaveUserHotel = function () {
        var data = {
            userId: $scope.UserSelected,
            HotelId: $scope.HotelId
        }
        var promisePost = Userservice.MapUserHotel($scope.HotelId, $scope.UserSelected[0]);
        promisePost.then(function (pl) {
            
            toastr.success("Thêm mới thành công !");
        }, function (err) {
            
            console.log("Err" + err);
        });
    }

    


    $scope.AddHotel = function () {
        
        var hotelModel = { Name: $scope.Name, Phone: $scope.Phone };
        if (hotelModel.Name == null || hotelModel.Name == undefined)
        {
            toastr.error("dữ liệu rông !"); return;
        }
        var promisePost = Hotelservice.AddHotel(hotelModel);
        promisePost.then(function (pl) {
            toastr.success("Thêm mới thành công !");
        }, function (err) {
            console.log("Err" + err);
        });
    };
});