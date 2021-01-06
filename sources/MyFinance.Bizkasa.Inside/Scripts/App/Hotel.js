
app.service("Hotelservice", function ($http) {  
   
    this.GetHotels = function (model) {
        var request = $http({
            method: "post",
            url: "/Hotel/GetHotels",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.GetHistoriesByInside = function (model) {
        var request = $http({
            method: "post",
            url: "/Hotel/GetHistoriesByInside",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

  

 

    this.AddHotel = function (model) {
        var request = $http({
            method: "post",
            url: "/Hotel/AddHotel",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }



    this.GetHotelInfo = function () {
        var request = $http({
            method: "get",
            url: "/Hotel/GetHotelInfo",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }


    this.CreateHotelFromSystem = function (model) {
        var request = $http({
            method: "post",
            url: "/Hotel/CreateHotelFromSystem",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.DisableHotel = function (val) {
        var request = $http({
            method: "post",
            url: "/Hotel/DisableHotel",
            dataType: 'json',
            data: JSON.stringify({hotelId:val}),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});


app.controller("HotelController", function ($scope, Hotelservice) {

    $scope.DisableHotel = function (val) {
        var promiseGet = Hotelservice.DisableHotel(val);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.GetHotels();
                toastr.success("Cập nhật thông tin thành công !");
            }
        },
        function (errorPl) {
        });

    };


    $scope.CreateHotelFromSystem = function () {
        var promiseGet = Hotelservice.CreateHotelFromSystem($scope.Hotel);
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
        var promiseGet = Hotelservice.GetHotels($scope.Filter);
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



    $scope.HistoriesPaging = function (page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetHistoriesByInside();
    };
    $scope.GetHistoriesByInside = function (val) {
        app.showWait(true);
        var promiseGet = Hotelservice.GetHistoriesByInside($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Histories = pl.data.Data.Data;
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                app.showWait(false);
            }
        });

    };
});