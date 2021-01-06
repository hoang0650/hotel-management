app.service("RoomTypeservice", function ($http) {

    this.GetWidgets = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Widget/GetWidget",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.GetRoomsByFloor = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/AdminIndex/GetRoomsByFloor"
        });
        return request;
    }

    this.GetRoomsStaticByTime = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomsStaticByTime",
            data: data,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }


    this.GetRoomsClassByRoom = function (id) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/AdminIndex/GetRoomsClassByRoom",
            data: JSON.stringify({ roomid: id })
        });
        return request;
    }


    this.GetRoomClassById = function (id) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomClassById",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ id: id })
        });
        return request;
    }




    this.GetRoomclass = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomClass",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.GetRoomclassForCheckin = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomClassForCheckinMutil",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.AutoGeneralRoom = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GeneralRoomAndFloor",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.AddRoomclass = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/AddRoomClass",
            data: data,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };





    this.GetRoomForEdit = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomForEdit",
            data: JSON.stringify({ roomid: val })
        });
        return request;
    }

    this.GetListFloor = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Room/GetListFloor",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetListRoomClass = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Room/GetListRoomClass",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetRoomUtilityBy = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomUtilityBy",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ roomtypeId: val })
        });
        return request;
    }

    this.RequestAddOrUpdateConfigPriceForOne = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/RequestAddOrUpdateConfigPriceForOne",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ data: model.ConfigPriceViewModel, roomClassId: model.RoomClassId })
        });
        return request;
    }
    this.InitConfigType = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Room/InitConfigType",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.DeleteRoomClass = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/DeleteRoomClass",
            dataType: 'json',
            data: JSON.stringify({ Ids: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.DeleteConfigPrice = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/DeleteConfigPrice",
            dataType: 'json',
            data: JSON.stringify({ Ids: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
});


app.controller("RoomTypeController", function ($scope, RoomTypeservice) {

    //GetAllRecords();
    //To Get All Records 
    var WidgetSelecteds = [];
    $scope.reallyDeleteRoomClass = function (item) {
        if (item) {
            WidgetSelecteds.push(item.Id);
        }
        if (WidgetSelecteds.length <= 0) {
            toastr.error("Chưa chọn tầng cần xóa !");
            return;
        }
        DeleteRoomClass(WidgetSelecteds);
    };
    var DeleteRoomClass = function (WidgetSelecteds) {
        var promiseGet = RoomTypeservice.DeleteRoomClass(WidgetSelecteds);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success('Xóa thành công !');
                WidgetSelecteds = [];
                $scope.GetRoomclass();
            } else {
                toastr.error(pl.data.Message);

            }

        },
        function (errorPl) {
        });
    };



    $scope.reallyDeleteConfigPrice = function (item) {
        if (item) {
            WidgetSelecteds.push(item.Id);
        }
        if (WidgetSelecteds.length <= 0) {
            toastr.error("Chưa chọn tầng cần xóa !");
            return;
        }
        DeleteConfigPrice(WidgetSelecteds);
    };
    var DeleteConfigPrice = function (WidgetSelecteds) {
        var promiseGet = RoomTypeservice.DeleteConfigPrice(WidgetSelecteds);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success('Xóa thành công !');
                WidgetSelecteds = [];
                $scope.GetRoomClassById();
            } else {
                toastr.error(pl.data.Message);

            }

        },
        function (errorPl) {
        });
    };

    $scope.SelectedConfigRow = function (val) {
        $.each($scope.RoomClassModel.ConfigPrices, function (index, item) {
            if (item.ConfigPriceRow.Id == val) {
                $scope.Roomtype = item.ConfigPriceRow;
                $scope.ConfigType = item.ConfigType;
                $scope.CheckoutDayList = ToList($scope.Roomtype.CheckoutDayList, false);
                $scope.CheckoutNightList = ToList($scope.Roomtype.CheckoutNightList, false);
                $scope.CheckinDayList = ToList($scope.Roomtype.CheckinDayList, false);
                $scope.CheckinNightList = ToList($scope.Roomtype.CheckinNightList, false);
                $scope.PriceByDayList = ToList($scope.Roomtype.PriceByDayList, false);
                $scope.AddtionCustomerList = ToList($scope.Roomtype.AddtionCustomerList, true);
                $scope.ConfigTimeList = ToListForWeek(item.ConfigPriceRow.ConfigTime, false);

            }
        });
        if ($scope.Roomtype) $("#AddOrUpdateConfig").modal('show');
    };


    $scope.InitConfigType = function () {
        var promiseGet = RoomTypeservice.InitConfigType();
        promiseGet.then(function (pl) {
            $scope.ConfigTypes = $.parseJSON(pl.data);
        },
        function (errorPl) {
        });

    };

    var ReadyUpdateConfigPrice = function () {

        var RoomClass = {
            Id: $scope.RoomClassModel.RoomClass.Id,
            Name: $scope.RoomClassModel.RoomClass.Name,
            NumBed: $scope.RoomClassModel.RoomClass.NumBed,
            NumCustomer: $scope.RoomClassModel.RoomClass.NumCustomer

        };

        var ConfigPriceRowModel = {
            Id: $scope.Roomtype.Id,
            Name: $scope.Roomtype.Name,
            PriceByDay: $scope.Roomtype.PriceByDay,
            PriceByMonth: $scope.Roomtype.PriceByMonth,
            PriceByNight: $scope.Roomtype.PriceByNight,
            IsActive: $scope.Roomtype.IsActive,
            IsDefault: $scope.Roomtype.IsDefault,
            RoomClassId: $scope.RoomClassModel.RoomClass.Id,
            ConfigTime: $scope.ConfigTimeList,
            CheckoutDayList: $scope.CheckoutDayList,
            CheckoutNightList: $scope.CheckoutNightList,
            CheckinDayList: $scope.CheckinDayList,
            CheckinNightList: $scope.CheckinNightList,
            PriceByDayList: $scope.PriceByDayList,
            AddtionCustomerList: $scope.AddtionCustomerList,
        };
        var ConfigPriceViewModel = {
            ConfigPriceRow: ConfigPriceRowModel,
            ConfigType: 2
        };
        $scope.dataSend = {
            ConfigPriceViewModel: ConfigPriceViewModel,
            RoomClassId: RoomClass.Id
        };
    };
    $scope.RequestAddOrUpdateConfigPriceForOne = function () {

        ReadyUpdateConfigPrice();

        CommonUtils.showWait(true);
        var promisePost = RoomTypeservice.RequestAddOrUpdateConfigPriceForOne($scope.dataSend);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.RoomClasses = pl.data.Data;
                $scope.GetRoomClassById();
                toastr.success("Cập nhật thành công !");
                $("#AddOrUpdateConfig").modal('hide');
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
        }, function (err) {
        });
    }



    $scope.GetListRoomType = function () {
        var id = getUrlParameter('id');
        CommonUtils.showWait(true);
        var promiseGet = RoomTypeservice.GetRoomUtilityBy(id);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Roomtypes = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }


        },
        function (errorPl) {
        });
    };


    $scope.GetRoomClassById = function () {
        var id = getUrlParameter('id');
        CommonUtils.showWait(true);
        var promiseGet = RoomTypeservice.GetRoomClassById(id);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.RoomClassModel = pl.data.Data;
              
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
        CommonUtils.showWait(false);
    };

    var InitCountries = function () {
        CommonUtils.showWait(true);
        var promiseGet = RoomTypeservice.GetListCountries();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                // $scope.Widgets = pl.data.Data;
                $scope.Countries = pl.data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };
    var InitWidget = function () {
        CommonUtils.showWait(true);
        var promiseGet = RoomTypeservice.GetWidgets();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                // $scope.Widgets = pl.data.Data;
                $scope.Widgets = $.map(pl.data.Data, function (n, i) {
                    return $.map(n.Widgets, function (item, index) {
                        return {
                            GroupName: n.GroupName,
                            Name: item.Name,
                            PricePaid: item.PricePaid,
                            Id: item.Id

                        };
                    });
                });
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };

    $scope.GetRoomsByFloor = function () {

        CommonUtils.showWait(true);
        var promiseGet = RoomTypeservice.GetRoomsByFloor();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Floors = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }


        },
        function (errorPl) {
        });
    };




    $scope.GetHotelBy = function () {
        var promiseGet = Hotelservice.getById();
        promiseGet.then(function (pl) {
            $scope.Hotel = pl.data;
        },
        function (errorPl) {
        });
    };

    $scope.pop = function () {
        $scope.Roomtype = null;
        $("#AddCate").modal('show');
    }

    $scope.GetRoomclassRow = function (id) {
        $.each($scope.RoomClasses, function (index, item) {
            if (item.Id == id) {

                $scope.Roomtype = item;
                $scope.CheckoutDayList = ToList($scope.Roomtype.CheckoutDayList, false);
                $scope.CheckoutNightList = ToList($scope.Roomtype.CheckoutNightList, false);
                $scope.CheckinDayList = ToList($scope.Roomtype.CheckinDayList, false);
                $scope.CheckinNightList = ToList($scope.Roomtype.CheckinNightList, false);
                $scope.PriceByDayList = ToList($scope.Roomtype.PriceByDayList, false);
                $scope.AddtionCustomerList = ToList($scope.Roomtype.AddtionCustomerList, true);
            }
        });
        $("#AddCate").modal('show');
    };

    $scope.GetRoomclass = function () {
        CommonUtils.showWait(true);
        var promiseGet = RoomTypeservice.GetRoomclass();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                
                $scope.RoomClasses = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }

        },
        function (errorPl) {
            
        });
    };



    // them loai phong
    var InitRow = function (Ilist, iscus) {
        var Hours = [];
        var unit = iscus ? 'người' : 'h';
        for (var i = Ilist.length + 1; i < 25; i++) {
            var hour = { Name: i + unit, Value: i };
            Hours.push(hour);
        }
        var data = { Hours: Hours, Key: 0, Value: 0 };
        if (Ilist.length <= 0)
            data.Key = Hours[0].Value;
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == Ilist[Ilist.length - 1].Key + 1)
                    data.Key = item.Value;
            });
        }
        return data;
    };

    var removeOneRow = function (Ilist, data, iscus) {
        Ilist.splice(data, 1);
        var unit = iscus ? 'người' : 'h';
        for (var i = data; i < Ilist.length; i++) {
            var Hours = [];
            for (var j = i + 1; j < 25; j++) {
                var hour = { Name: j + unit, Value: j };
                Hours.push(hour);
            }
            var row = Ilist[i];
            row.Hours = Hours;
            $.each(Hours, function (i, item) {
                if (item.Value == row.Key)
                    row.Key = item.Value;
            });
            Ilist[i] = row;

        }
    };

    var selectedOneRow = function (Ilist, val) {
        if (val == 0) {
            for (var i = val + 1; i < Ilist.length; i++) {
                var row = Ilist[i];
                if (Ilist[i].Key < Ilist[i - 1].Key) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == Ilist[i - 1].Key + 1)
                            row.Key = item.Value;
                    });
                    Ilist[i] = row;
                }

            }

        }
        else {
            for (var i = val - 1; i >= 0; i--) {
                var row = Ilist[i];
                if (Ilist[i].Key.Value > Ilist[i + 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == Ilist[i + 1].Key - 1)
                            row.Key = item.Value;
                    });
                    Ilist[i] = row;
                }

            }

            for (var i = val + 1; i < Ilist.length; i++) {
                var row = Ilist[i];
                if (Ilist[i].Key < Ilist[i - 1].Key) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == Ilist[i - 1].Key + 1)
                            row.Key = item.Value;
                    });
                    Ilist[i] = row;
                }
            }

        }

    };
    var ToListForWeek = function (FromList, iscus) {
        result = [];
        var Hours = [{ Name: 'Thứ 2', Value: 1 }, { Name: 'Thứ 3', Value: 2 }, { Name: 'Thứ 4', Value: 3 }, { Name: 'Thứ 5', Value: 4 }, { Name: 'Thứ 6', Value: 5 }, { Name: 'Thứ 7', Value: 6 }, { Name: 'Chủ nhật', Value: 0 }];
       
        if (FromList == null || FromList.length <= 0)
            return result;
        $.each(FromList, function (index, item) {

            var data = { Hours: Hours, Key: item.Key, Value: item.Value };
            //if (FromList.length <= 0)
            //    data.Key = Hours[0];
            //else {
            //    $.each(Hours, function (i, item) {
            //        if (item.Value == FromList[FromList.length - 1].Key + 1)
            //            data.Key = item.Value;
            //    });
            //}
            result.push(data);
        });
        return result;
    };

    var ToList = function (FromList, iscus) {
        result = [];
        var unit = iscus ? 'người' : 'h';
        if (FromList == null || FromList.length <= 0)
            return result;
        $.each(FromList, function (index, item) {
            var Hours = [];
            for (var i = item.Key; i < 25; i++) {
                var hour = { Name: i + unit, Value: i };
                Hours.push(hour);
            }
            var data = { Hours: Hours, Key: item.Key, Value: item.Value };
            //if (FromList.length <= 0)
            //    data.Key = Hours[0];
            //else {
            //    $.each(Hours, function (i, item) {
            //        if (item.Value == FromList[FromList.length - 1].Key + 1)
            //            data.Key = item.Value;
            //    });
            //}
            result.push(data);
        });
        return result;
    };



    $scope.CheckoutDayList = [];
    $scope.CheckoutDayclick = function () {
        var row = InitRow($scope.CheckoutDayList, false);
        $scope.CheckoutDayList.push(row);

    };
    $scope.CheckoutDayRemoveclick = function (data) {
        removeOneRow($scope.CheckoutDayList, data, false);

    };
    $scope.CheckoutDaySelectedclick = function (val) {
        selectedOneRow($scope.CheckoutDayList, val);

    };




    $scope.ParseRow = function (val) {
        var Hours = [{ Name: 'Thứ 2', Value: 1 }, { Name: 'Thứ 3', Value: 2 }, { Name: 'Thứ 4', Value: 3 }, { Name: 'Thứ 5', Value: 4 }, { Name: 'Thứ 6', Value: 5 }, { Name: 'Thứ 7', Value: 6 }, { Name: 'Chủ nhật', Value: 0 }];
        var theItem = jQuery.grep(Hours, function (p) { return p.Value == val });
        return theItem[0].Name;

    };
    var InitRowForWeek = function (Ilist, iscus) {
        var Hours = [{ Name: 'Thứ 2', Value: 1 }, { Name: 'Thứ 3', Value: 2 }, { Name: 'Thứ 4', Value: 3 }, { Name: 'Thứ 5', Value: 4 }, { Name: 'Thứ 6', Value: 5 }, { Name: 'Thứ 7', Value: 6 }, { Name: 'Chủ nhật', Value: 0 }];

        var data = { Hours: Hours, Key: 0, Value: 0 };
        if (Ilist.length <= 0)
            data.Key = Hours[0].Value;
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == Ilist[Ilist.length - 1].Key + 1)
                    data.Key = item.Value;
            });
        }
        return data;
    };


    $scope.ConfigTimeList = [];
    $scope.ConfigTimeclick = function () {
        var row = InitRowForWeek($scope.ConfigTimeList, false);
        $scope.ConfigTimeList.push(row);

    };
    $scope.ConfigTimeRemoveclick = function (data) {
        removeOneRow($scope.ConfigTimeList, data, false);

    };
    $scope.ConfigTimeSelectedclick = function (val) {
        selectedOneRow($scope.ConfigTimeList, val);

    };









    $scope.AddtionCustomerList = [];
    $scope.AddtionCustomerclick = function () {
        var row = InitRow($scope.AddtionCustomerList, true)
        $scope.AddtionCustomerList.push(row);

    };
    $scope.AddtionCustomerRemoveclick = function (data) {
        removeOneRow($scope.AddtionCustomerList, data, true);

    };
    $scope.AddtionCustomerSelectedclick = function (val) {
        selectedOneRow($scope.AddtionCustomerList, val);

    };





    $scope.CheckoutNightList = [];
    $scope.CheckoutNightclick = function () {
        var row = InitRow($scope.CheckoutNightList, false);
        $scope.CheckoutNightList.push(row);
    };
    $scope.CheckoutNightRemoveclick = function (data) {
        removeOneRow($scope.CheckoutNightList, data, false);
    };
    $scope.CheckoutNightSelectedclick = function (val) {
        selectedOneRow($scope.CheckoutNightList, val);
    };



    $scope.CheckinDayList = [];
    $scope.CheckinDayclick = function () {
        var row = InitRow($scope.CheckinDayList, false);
        $scope.CheckinDayList.push(row);

    };
    $scope.CheckinDayRemoveclick = function (data) {

        removeOneRow($scope.CheckinDayList, data, false);

    };
    $scope.CheckinDaySelectedclick = function (val) {
        selectedOneRow($scope.CheckinDayList, val);

    };



    $scope.CheckinNightList = [];
    $scope.CheckinNightclick = function () {
        var row = InitRow($scope.CheckinNightList, false);
        $scope.CheckinNightList.push(row);
    };
    $scope.CheckinNightRemoveclick = function (data) {

        removeOneRow($scope.CheckinNightList, data, false);

    };
    $scope.CheckinNightSelectedclick = function (val) {

        selectedOneRow($scope.CheckinNightList, val);

    };
    $scope.PriceByDayList = [];
    $scope.PriceByDayclick = function () {
        var row = InitRow($scope.PriceByDayList, false);
        $scope.PriceByDayList.push(row);

    };
    $scope.PriceByDayRemoveclick = function (data) {

        removeOneRow($scope.PriceByDayList, data, false);

    };
    $scope.PriceByDaySelectedclick = function (val) {

        selectedOneRow($scope.PriceByDayList, val);

    };



    $scope.ShowPopUpConfig = function () {
        if ($scope.Roomtype)
             $scope.Roomtype.Id = 0;
        $("#AddOrUpdateConfig").modal('show');
    };


    var ReadyDataForAdd = function () {

        var RoomClass = {
            Id: 0,
            Name: $scope.Roomtype.Name,
            NumBed: $scope.Roomtype.NumBed,
            NumCustomer: $scope.Roomtype.NumCustomer

        };
        var ConfigPriceRowModel = {
            Id: 0,
            Name: "Mặc định",
            PriceByDay: $scope.Roomtype.PriceByDay,
            PriceByNight: $scope.Roomtype.PriceByNight,
            PriceByMonth: $scope.Roomtype.PriceByMonth,
            CheckoutDayList: $scope.CheckoutDayList,
            CheckoutNightList: $scope.CheckoutNightList,
            CheckinDayList: $scope.CheckinDayList,
            CheckinNightList: $scope.CheckinNightList,
            PriceByDayList: $scope.PriceByDayList,
            AddtionCustomerList: $scope.AddtionCustomerList,
            IsDefault: true,
            IsActive:true
        };
        var ConfigPriceViewModel = {
            ConfigPriceRow: ConfigPriceRowModel,
            ConfigType: 2
        };

        $scope.RoomClassModel = {
            ConfigPrices: [],
            RoomClass: RoomClass
        }; 
        $scope.RoomClassModel.ConfigPrices.push(ConfigPriceViewModel);
        $scope.Roomtype.RoomTypeFeatureIds = [];
      
    };

    $scope.AddRoomClass = function () {
        ReadyDataForAdd();
        CommonUtils.showWait(true);
        
        var promisePost = RoomTypeservice.AddRoomclass($scope.RoomClassModel);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.RoomClasses = pl.data.Data;
                toastr.success("Cập nhật thành công !");
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
            //toastr.success("Thêm mới thành công !");
            //$("#AddCate").modal('hide');
        }, function (err) {
        });
    }
    var ReadyDataForUpdate = function () {
        $scope.RoomClassModel.RoomTypeFeatureIds = [];
        $.each($scope.Roomtypes, function (index, item) {
            if (item.IsSelected) {
                $scope.RoomClassModel.RoomTypeFeatureIds.push(item.Id);
            }
        });
        $scope.RoomClassModel.RoomTypeFeatureIds = $scope.RoomTypeFeatureIds;
    };
    $scope.UpdateRoomClass = function () {
        ReadyDataForUpdate();
        CommonUtils.showWait(true);

        var promisePost = RoomTypeservice.AddRoomclass($scope.RoomClassModel);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.RoomClasses = pl.data.Data;
                toastr.success("Cập nhật thành công !");
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
            //toastr.success("Thêm mới thành công !");
            //$("#AddCate").modal('hide');
        }, function (err) {
        });
    }

    // end



    //=================================== Room and Floor ===========================
    $scope.GetRoomForEdit = function (val) {
        var promisePost = RoomTypeservice.GetRoomForEdit(val);
        promisePost.then(function (pl) {
            $scope.Room = pl.data.Data;
            GetListFloor();
            GetListRoomClass();
            $("#EditRoom").modal("show");
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    var GetListFloor = function (val) {

        var promisePost = RoomTypeservice.GetListFloor();
        promisePost.then(function (pl) {
            $scope.FloorList = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    var GetListRoomClass = function (val) {

        var promisePost = RoomTypeservice.GetListRoomClass();
        promisePost.then(function (pl) {
            $scope.RoomClassList = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };



    //=================================================================

    function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }
    //===================================== end -----------------------------------------

});