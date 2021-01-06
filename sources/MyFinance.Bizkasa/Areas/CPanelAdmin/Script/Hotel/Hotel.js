
app.service("HotelService", function ($http) {

    this.InsertOrUpdateCamera = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/InsertOrUpdateCamera",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetCameraByHotel = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Hotel/GetCameraByHotel",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetOrderByCompany = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderByCompany",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetHotels = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/GetHotels",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.UpdateStatusInvocie = function (invoiceId, status) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/UpdateStatusInvocie",
            dataType: 'json',
            data: JSON.stringify({ invoiceId: invoiceId, status: status }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.GetHotelById = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/GetHotelById",
            dataType: 'json',
            data: JSON.stringify({ id: val}),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.AddHotel = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/AddHotel",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

   

    this.GetHotelInfo = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/GetHotelInfo",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }




    this.GetHotelUtilityBy = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/GetHotelUtilityBy",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

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
    };


    this.DeleteHotel= function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/DeleteHotel",
            dataType: 'json',
            data: JSON.stringify({ Ids: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.ResetDataHotel = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/ResetDataHotel",
            dataType: 'json',
            data: JSON.stringify({ hotelId: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
});


app.controller("HotelController", function ($scope, HotelService) {
    $scope.initDate = function () {
        $('#id-date').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY',
            timezone: 'local',
            singleDatePicker: true
        },
         function (start, end, label) {
             $scope.HotelRegisterModel.DateExpired = start;
             if ($scope.Hotel)
                $scope.Hotel.DateExpired = start;
             //$scope.Filter.ToDate = end;
         });
    };
   

    function validateEmail(sEmail) {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (filter.test(sEmail)) {
            return true;
        }
        else {
            return false;
        }
    }
    $scope.HotelRegisterModel = {
        Name: undefined,
        Email: undefined,
        IsChecked: true,
        User: {
            Email: undefined,
            Password: undefined,
            ComfirmPassword: undefined
        }
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



        if ($scope.HotelRegisterModel.User.Email == undefined ) {
            toastr.error("Bạn chưa nhập đủ thông tin quản trị !");
            return;
        }
        //if (!validateEmail($scope.HotelRegisterModel.User.Email)) {
        //    toastr.error("Email không hợp lệ. Tên đăng nhập phải là email !");
        //    return;
        //}

        if ($scope.HotelRegisterModel.User.Password != $scope.HotelRegisterModel.User.ComfirmPassword) {
            toastr.error("Xác nhận mật khẩu không chính xác !");
            return;
        }
        if (!$scope.HotelRegisterModel.IsChecked) {
            toastr.error("Bạn chưa đồng ý với điều khoản sử dụng !");
            return;
        }




        var promiseGet = HotelService.RegisterHotel($scope.HotelRegisterModel);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success("Chúc mừng bạn đã đăng ký thành công! Bạn có thể đăng nhập để sử dụng ngay và luôn ! ");
                $scope.GetHotels();
            }
            CommonUtils.showWait(false);
        },
        function (errorPl) {
           
        });


    };
    
    $scope.GetCameraByHotel = function () {
        CommonUtils.showWait(true);
        var promiseGet = HotelService.GetCameraByHotel();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Camera = pl.data.Data;
                $scope.Cameras = [];
                if ($scope.Camera.NumCamera > 1) {
                    for (var i = 1; i <= $scope.Camera.NumCamera; i++) {
                        $scope.Cameras.push({Name:"Camera "+i,Value:i});
                    }
                }
                $scope.Target = 'rtsp://ksphamhoanglam.dyndns.org:554/user=admin&password=&channel=3&stream=1.sdp?real_stream--rtp-caching=100';
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };
    $scope.InsertOrUpdateCamera = function () {
        CommonUtils.showWait(true);
        if ($scope.Camera.CameraDefault > $scope.Camera.NumCamera) {
            toastr.error("Camera mặc định phải nhỏ hơn tổng số camera hiện có");
            return;
        }
        var promiseGet = HotelService.InsertOrUpdateCamera($scope.Camera);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success("Cập nhật thành công !");
            
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };


    
    $scope.GetHotelById = function (val) {
        CommonUtils.showWait(true);
        var promiseGet = HotelService.GetHotelById(val);
        promiseGet.then(function (pl) {

            if (!pl.data.IsError) {
                $scope.Hotel = pl.data.Data;
                $('#id-date').data('daterangepicker').setStartDate($scope.Hotel.DateExpiredString);
                $('#id-date').data('daterangepicker').setEndDate($scope.Hotel.DateExpiredString);
                $("#EditHotel").modal("show");
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
    };
    $scope.GetHotelUtilityBy = function () {
        CommonUtils.showWait(true);
        var promiseGet = HotelService.GetHotelUtilityBy();
        promiseGet.then(function (pl) {
            
            if (!pl.data.IsError) {
                $scope.HotelUtilities = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
    };

    $scope.GetHotelInfo = function () {
        var promiseGet = HotelService.GetHotelInfo();
        promiseGet.then(function (pl) {
            if(!pl.data.IsError)
                $scope.Hotel = pl.data.Data;
            else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });

    };
    $scope.SetLogo = function (val) {
        if (val != undefined)
            $scope.Hotel.Logo = val;
    };
    $scope.DeleteHotel = function (Id) {
        var promiseGet = HotelService.DeleteHotel(Id);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success('Xóa thành công !');
                $("#EditHotel").modal("hide");
            } else {
                toastr.error(pl.data.Message);

            }

        },
        function (errorPl) {
        });
    };

    $scope.ResetDataHotel = function (Id) {
        var promiseGet = HotelService.ResetDataHotel(Id);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success('Xóa thành công !');
                $("#EditHotel").modal("hide");
            } else {
                toastr.error(pl.data.Message);

            }

        },
        function (errorPl) {
        });
    };

    $scope.AddHotel = function (val) {
       
       // $scope.Hotel.Description = $scope.ckEditors;
        var promiseGet = HotelService.AddHotel($scope.Hotel);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError){
                toastr.success("Cập nhật thông tin thành công !");
                $scope.GetHotels();
                if(val)
                    $("#EditHotel").modal("hide");
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });

    };

    $scope.CreateHotelFromSystem = function () {
        var promiseGet = HotelService.CreateHotelFromSystem($scope.Hotel);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError)
                toastr.success("Cập nhật thông tin thành công !");
        },
        function (errorPl) {
        });

    };

    $scope.Filter = {
        Page: {
            currentPage: 1,
            pageSize: 20,
            total: 0
        },
        InvoiceType: 1,
        FromDate: undefined,
        ToDate: undefined,
        Keyword: undefined

    };
   

    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetHotels();
    };
    $scope.GetHotels = function (val) {

      
        CommonUtils.showWait(true);
        var promiseGet = HotelService.GetHotels($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Hotels = pl.data.Data.Data;
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                CommonUtils.showWait(false);
            }
        },
        function (errorPl) {
        });
    };
});
app.directive('ckEditor', [function () {
    return {
        require: '?ngModel',
        link: function ($scope, elm, attr, ngModel) {

            var ck = CKEDITOR.replace(elm[0]);

            ck.on('pasteState', function () {
                $scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$modelValue);
            };
        }
    };
}]);