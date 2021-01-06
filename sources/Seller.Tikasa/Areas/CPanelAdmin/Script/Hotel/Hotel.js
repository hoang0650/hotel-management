
app.service("HotelService", function ($http) {

    this.GetOrderByCompany = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderByCompany",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetHotels = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Hotel/GetHotels",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.UpdateStatusInvocie = function (invoiceId, status) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/UpdateStatusInvocie",
            dataType: 'json',
            data: JSON.stringify({ invoiceId: invoiceId, status: status }),
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
            method: "get",
            url: "/CPanelAdmin/Hotel/GetHotelInfo",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }




    this.GetHotelUtilityBy = function (val) {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Hotel/GetHotelUtilityBy",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});


app.controller("HotelController", function ($scope, HotelService) {

    $scope.GetHotelUtilityBy = function () {
        app.showWait(true);
        var promiseGet = HotelService.GetHotelUtilityBy();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.HotelUtilities = pl.data.Data;
                app.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }


        },
        function (errorPl) {
        });
    };

    $scope.GetHotelInfo = function () {
        var promiseGet = HotelService.GetHotelInfo();
        promiseGet.then(function (pl) {
            if(!pl.data.IsError)
                $scope.Hotel=pl.data.Data;
        },
        function (errorPl) {
        });

    };
    $scope.SetLogo = function (val) {
        if (val != undefined)
            $scope.Hotel.Logo = val;
    };

    $scope.AddHotel = function () {
        $scope.Hotel.HotelUtilityIds = [];
        $.each($scope.HotelUtilities, function (index, item) {
            if (item.IsSelected) {
                $scope.Hotel.HotelUtilityIds.push(item.Id);
            }
        });
        debugger
       // $scope.Hotel.Description = $scope.ckEditors;
        var promiseGet = HotelService.AddHotel($scope.Hotel);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError)
                toastr.success("Cập nhật thông tin thành công !");
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

        if (val)
            $scope.Filter.InvoiceType = val
        app.showWait(true);
        var promiseGet = HotelService.GetHotels($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Hotels = pl.data.Data.Data;
                $.each($scope.Hotels, function (index, item) {
                    item.CreateDate = item.CreateDate ? new Date(parseInt(item.CreateDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    
                });
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                app.showWait(false);
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