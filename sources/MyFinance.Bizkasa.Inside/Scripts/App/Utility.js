
app.service("Utilityservice", function ($http) {
   
    this.GetUtilities = function (model) {
        var request = $http({
            method: "post",
            url: "/Utility/GetUtilities",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

 

    this.AddOrUpdateUtilityGroup = function (model) {
        var request = $http({
            method: "post",
            url: "/Utility/AddOrUpdateUtilityGroup",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.AddOrUpdateUtility = function (model) {
        var request = $http({
            method: "post",
            url: "/Utility/AddOrUpdateUtility",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetUtilityGroups = function (model) {
        var request = $http({
            method: "post",
            url: "/Utility/GetUtilityGroups",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetUtilityForEdit = function (val) {
        var request = $http({
            method: "POST",
            url: "/Utility/GetUtilityForEdit",
            dataType: 'json',
            data: JSON.stringify({Id:val}),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});


app.controller("UtilityController", function ($scope, Utilityservice) {
    $scope.Group = {
        Id: 0,
        Name: undefined,
        IsDeleted:false
    };
    $scope.Utility = {
        Id: 0,
        Name: undefined,
        IsDeleted: false,
        GroupId: 0,
        FeatureType:1
    };
    $scope.UtilityType = [{ Id: 2, Name: "Tiện ích khách sạn" }, { Id:1, Name: "Tiện ích loại phòng" }];
    $scope.GetUtilityForEdit = function (val) {
        var promiseGet = Utilityservice.GetUtilityForEdit(val);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Utility = pl.data.Data;
                $("#AddUtility").modal("show");
            }
        },
        function (errorPl) {
        });

    };
    $scope.AddOrUpdateUtility = function () {
        if ($scope.Utility.Name == undefined) {
            toastr.error("Bạn chưa nhập đủ thông tin !");
            return;
        }
        var promiseGet = Utilityservice.AddOrUpdateUtility($scope.Utility);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success("Cập nhật thông tin thành công !");

            }
        },
        function (errorPl) {
        });

    };

    $scope.AddOrUpdateUtilityGroup = function () {
        if ($scope.Group.Name == undefined)
        {
            toastr.error("Bạn chưa nhập đủ thông tin !");
            return;
        }
        var promiseGet = Utilityservice.AddOrUpdateUtilityGroup($scope.Group);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.GetUtilityGroups();
                toastr.success("Cập nhật thông tin thành công !");

            }
        },
        function (errorPl) {
        });

    };

    $scope.GetUtilities = function () {
        var promiseGet = Utilityservice.GetUtilities();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {debugger
                $scope.UtilityGroups = pl.data.Data;
               
            }
        },
        function (errorPl) {
        });

    };
    $scope.GetUtilityGroups = function () {
        var promiseGet = Utilityservice.GetUtilityGroups();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Groups = pl.data.Data;

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
});