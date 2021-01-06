app.service("userservice", function ($http) {

    this.Login = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Home/Login",
            dataType: 'json',
            contentType: "application/json; charset=UTF-8",
            data: model
        });
        return request;
    };


    this.GetAllUserByHotel = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/User/GetUserByHotel",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };

    this.GetUserForEdit = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/User/GetUserForEdit",
            data: JSON.stringify({userId:val}),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };

    this.GetUserForEditCurentAccount = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/User/GetUserForEditCurentAccount",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };

    this.AddUser = function (userhotel) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/User/AddUser",
            data: userhotel,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.DeleteUser = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/User/DeleteUser",
            data: JSON.stringify({ Ids: val }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };
    this.InitPermission = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/User/InitPermission",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };

    this.CheckStoreToken = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Home/CheckStoreToken",
            dataType: 'json',
            data: JSON.stringify({StoreName:val}),
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };

    this.AddStoreToken = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Home/AddStoreToken",
            dataType: 'json',
            data: JSON.stringify({ Model: model }),
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };
});

app.controller("UserController", function ($scope, userservice) {

    $scope.AddStoreToken = function (model) {
      
        CommonUtils.showWait(true);
        var promiseGet = userservice.AddStoreToken(model);
        promiseGet.then(function (pl) {
         
            if (pl.data.IsError) {
                toastr.error(pl.data.Message)
            }
            
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
      
    };

    $scope.CheckStoreToken = function (val) {
        CommonUtils.showWait(true);
        var promiseGet = userservice.CheckStoreToken(val.StoreName);
        promiseGet.then(function (pl) {
            if (pl.data.IsError) {
                toastr.error(pl.data.Message)
            }
            
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };

    $scope.Login = function () {
        CommonUtils.showWait(true);
        var promiseGet = userservice.Login($scope.User);
        promiseGet.then(function (pl) {
            
            if (pl.data.IsError) {
                toastr.error(pl.data.Message)
            }
            else {
               
                window.location.href = "/CPanelAdmin/Home/";
            }
        
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };

    $scope.InitUser = function GetAllRecords() {
        var promiseGet = userservice.GetAllUserByHotel();
        promiseGet.then(function (pl) { $scope.Users = pl.data.Data; },
              function (errorPl) {
                 
              });
    };

    $scope.InitPermission = function InitPermission() {
        var promiseGet = userservice.InitPermission();
        promiseGet.then(function (pl) { $scope.Permissions = $.parseJSON(pl.data); },
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);
              });
    };

    $scope.GetUserForEdit = function (val) {
        if (!val) {
            toastr.error("Cần chọn nhân viên !");
            return;
        };
        CommonUtils.showWait(true);
        var promiseGet = userservice.GetUserForEdit(val);
        promiseGet.then(function (pl) {
            $scope.User = pl.data.Data;
            CommonUtils.showWait(false);
            $("#Adduser").modal("show");
        },
              function (errorPl) {
                  toastr.error('Some Error in Getting Records.', errorPl);
              });
    };


    $scope.GetUserForEditCurentAccount = function () {
      
        CommonUtils.showWait(true);
        var promiseGet = userservice.GetUserForEditCurentAccount();
        promiseGet.then(function (pl) {
            $scope.User = pl.data.Data;
            CommonUtils.showWait(false);
            $("#Adduser").modal("show");
        },
              function (errorPl) {
                  toastr.error('Some Error in Getting Records.', errorPl);
              });
    };


    var ItemSelecteds = [];
    $scope.ItemSelected = function (val) {
        if (val.IsSelected) {
            ItemSelecteds.push(val.Id);
        }
        else {
            ItemSelecteds = $.grep(ItemSelecteds, function (item, i) {
                return item != val.Id;
            });
        }
    };
    $scope.reallyDelete = function (item) {
        DeleteUser(item);
    };
    var DeleteUser = function (val) {
        if (val)
            ItemSelecteds.push(val.Id);
        if (ItemSelecteds.length <= 0)
        {
            toastr.error("Bạn chưa chọn nhân viên cần xóa !");
            return;
        }
        var promiseGet = userservice.DeleteUser(ItemSelecteds);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.InitUser();
                toastr.success("Xóa nhân viên thành công !");
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
              function (errorPl) {
                  toastr.error('Some Error in Getting Records.', errorPl);
              });
    };


    $scope.AddUser = function () {
        
        if ($scope.User.Email == null || $scope.User.Email == undefined) {
            toastr.error("Dữ liệu rông !"); return;
        }

        if ($scope.User.Password != $scope.User.RePassword) {
            toastr.error("Nhập lại mật khẩu không trùng khớp !"); return;
        }
        
        var promisePost = userservice.AddUser($scope.User);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success("Thêm mới thành công !");
                $("#Adduser").modal('hide');
                $scope.InitUser();
            }
            else
            {
                toastr.error(pl.data.Message);

            }
            
        }, function (err) {
            console.log("Err" + err);
        });
    };

});
app.directive('ngEnter', function () {
    return function(scope, element, attrs) {
        element.bind("keydown keypress", function(event) {
            if(event.which === 13) {
                scope.$apply(function(){
                    scope.$eval(attrs.ngEnter, {'event': event});
                });

                event.preventDefault();
            }
        });
    };
});

