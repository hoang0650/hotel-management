app.service("Housekeepingservice", function ($http) {

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


    this.UpdateServiceForOrder = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/UpdateServiceForOrder",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: data
        });
        return request;
    };
    this.BookingOrder = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/BookingOrder",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: data
        });
        return request;
    };


    this.UpdateOrder = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/UpdateOrder",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: data
        });
        return request;
    };


    this.AddOrderMutil = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/AddOrderMutil",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: data
        });
        return request;
    };

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

    this.GetRoomAvailable = function (fromDate, toDate) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomAvailable",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate, ToDate: toDate })
        });
        return request;
    };

    this.GetRoomAvailableNow = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomAvailableNow",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',

        });
        return request;
    };

    this.AddRoomClass = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/AddRoomClass",
            data: data,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.GetOrderForEdit = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderForEdit",
            data: JSON.stringify({ orderId: val })
        });
        return request;
    }




    this.GetOrderForCheckOut = function (val, mode) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderForCheckOut",
            data: JSON.stringify({ orderId: val, mode: mode })
        });
        return request;
    }

    this.GetBookingForChecking = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetBookingForChecking",
            data: JSON.stringify({ orderId: val })
        });
        return request;
    }


    this.CompanyDoCheckOut = function (val, mode) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/CompanyDoCheckOut",
            data: JSON.stringify({ OrderIds: val, mode: mode }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetOrderByCompany = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderByCompany",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetBookingOrders = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetBookingOrders",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetBookingOrdersUnPrePay = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetBookingOrdersUnPrePay",
            dataType: 'json',

            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetBookingOrdersNearDate = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetBookingOrdersNearDate",
            dataType: 'json',

            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    //GetBookingOrdersUnPrePay

    this.TranferBookingToCheckIn = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/TranferBookingToCheckIn",
            dataType: 'json',
            data: JSON.stringify({ orderid: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.ChangeRoomInOrder = function (orderid, roomid) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/ChangeRoomInOrder",
            dataType: 'json',
            data: JSON.stringify({ orderid: orderid, roomid: roomid }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetCustomerPassportId = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/GetCustomerPassportId",
            dataType: 'json',
            data: JSON.stringify({ passportId: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }


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
    this.EditRoom = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/EditRoom",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetListCountries = function () {
        var request = $http({
            method: "get",
            url: "https://restcountries.eu/rest/v1/all",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetRoomsByClass = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomsByClass",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.CancelOrder = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/CancelOrder",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ OrderId: val })
        });
        return request;
    }

    this.DeleteRoom = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/DeleteRoom",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ roomId: val })
        });
        return request;
    }
    this.BlockRoom = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/BlockRoom",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ roomId: val })
        });
        return request;
    }
    this.UnBlockRoom = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/UnBlockRoom",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ roomId: val })
        });
        return request;
    }
    this.RefreshRoom = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/RefreshRoom",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ roomId: val })
        });
        return request;
    }


    this.CheckConnectLock = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/SystemConfig/CheckConnectLock",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.AddMoreCard = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/AddMoreCard",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ LockNo: val })
        });
        return request;
    }

});


app.controller("HousekeepingController", function ($scope, Housekeepingservice) {

    //GetAllRecords();
    //To Get All Records  



    $scope.ShowNote = function () {

        $("#Note").modal('show');
    };

    $scope.AddMoreCard = function (val) {
        if (!val) {
            toastr.error("Phòng chưa được cấp mã khóa !");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.AddMoreCard(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                toastr.success("Cấp thẻ thành công !");
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }


        },
        function (errorPl) {
        });
    };



    $scope.CheckConnectLock = function () {

        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.CheckConnectLock();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                

                $scope.Lock = pl.data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };

    $scope.reallyBlock = function (item) {
        BlockRoom(item);
    };
    var BlockRoom = function (val) {
        if (val == undefined) {
            toastr.error("Chưa chọn phòng cần khóa");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.BlockRoom(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                toastr.success('Khóa thành công !');
                $scope.GetRoomsByFloor();
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };
    $scope.reallyUnBlock = function (item) {
        UnBlockRoom(item);
    };
    var UnBlockRoom = function (val) {
        if (val == undefined) {
            toastr.error("Chưa chọn phòng cần mở khóa");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.UnBlockRoom(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                toastr.success('Mở khóa thành công !');
                $scope.GetRoomsByFloor();
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }


        },
        function (errorPl) {
        });
    };
    $scope.refreshRoom = function (item) {
        RefreshRoom(item);
    };
    var RefreshRoom = function (val) {
        if (val == undefined) {
            toastr.error("Chưa chọn phòng cần dọn");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.RefreshRoom(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                toastr.success('Đã dọn !');
                $scope.GetRoomsByFloor();
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }


        },
        function (errorPl) {
        });
    };

    $scope.reallyCancle = function (item) {
        CancelOrder(item);
    };

    $scope.CancelOrder = function (val) {
        if (val == undefined) {
            toastr.error("Chưa chọn đơn cần hủy");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.CancelOrder(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                toastr.success('Hủy thành công !');
                //GetBookingOrders();

                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };


    $scope.reallyDelete = function (item) {
        DeleteRoom(item);
    };
    var DeleteRoom = function (val) {
        if (val == undefined) {
            toastr.error("Chưa chọn phòng cần xóa");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.DeleteRoom(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                toastr.success('Xóa thành công !');
                $scope.GetRoomsByFloor();
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };
    $scope.ModeOrder = "ByFloor";
    $scope.ModeOrderRoom = function () {
        if ($scope.ModeOrder == "ByFloor") {
            $scope.GetRoomsByFloor();
        }
        if ($scope.ModeOrder == "ByRoomClass") {
            $scope.GetRoomsByClass();
        }
    };



    var InitWidget = function () {
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.GetWidgets();
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
        var promiseGet = Housekeepingservice.GetRoomsByFloor();
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

    $scope.GetRoomsByClass = function () {

        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.GetRoomsByClass();
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
        toastr.error("ở đây");
        var promiseGet = Housekeepingservice.GetRoomclass();
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

    $scope.AutoGeneralRoom = function () {
        var promiseGet = Housekeepingservice.AutoGeneralRoom();
        promiseGet.then(function (pl) {

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


    $scope.AddRoomClass = function () {

        $scope.Roomtype.CheckoutDayList = $scope.CheckoutDayList;
        $scope.Roomtype.CheckoutNightList = $scope.CheckoutNightList;
        $scope.Roomtype.CheckinDayList = $scope.CheckinDayList;
        $scope.Roomtype.CheckinNightList = $scope.CheckinNightList;
        $scope.Roomtype.PriceByDayList = $scope.PriceByDayList;
        $scope.Roomtype.AddtionCustomerList = $scope.AddtionCustomerList;
        CommonUtils.showWait(true);
        var promisePost = Housekeepingservice.AddRoomClass($scope.Roomtype);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.RoomClasses = pl.data.Data;
                toastr.success("Thêm mới thành công !");
                CommonUtils.showWait(false);
                $("#AddCate").modal('hide');

            }
            else {
                toastr.error(pl.data.Error);
                CommonUtils.showWait(false);
            }

        }, function (err) {
        });
    }

    // end



    // checkin 
    $scope.Order = {
        CheckInDate: null,
        TimeCheckin: null,
        RoomId: 0,
        RoomName: null,
        Price: 0,
        Customers: [],
        Services: [],
        RoomIds: [],


    };
    $scope.Customer = {
        PassportId: null,
        Name: null,
        FistName: null,
        Sex: null,
        Id: 0,
        BirthDay: null,
        IsPrimary: false
    };
    $scope.CustomerIndexSelected = 0;

    $scope.addCutomerOrder = function (val) {

        if ($scope.Customer.PassportId != undefined && $scope.Customer.PassportCreatedDate == undefined) {
            toastr.error("Bạn chưa nhập ngày cấp CMND/Passport !");
            return;
        }
        if ($scope.Customer.Name == null) {
            toastr.error("Bạn chưa nhập tên khách hàng !");
            return;
        }
        if ($scope.Customer.FistName == null) {
            toastr.error("Bạn chưa nhập họ khách hàng !");
            return;
        }
        if ($scope.Customer.Sex == null) {
            toastr.error("Bạn chưa chọn giới tính !");
            return;
        }
        if ($scope.Customer.BirthDay == null) {
            toastr.error("Bạn chưa chọn ngày sinh !");
            return;
        }
        var check = false;
        $.each($scope.Order.Customers, function (i, item) {
            if (item.PassportId == $scope.Customer.PassportId) {
                check = true;
                toastr.error('Khách hàng đã tồn tại trong danh sách !');
                return;
            }

        });
        if (check)
            return;
        $scope.Order.Customers.push($scope.Customer);


        if (val)
            $('#addCustomerToOrder').modal('hide');
        $scope.Customer = null;
    };

    $scope.addCutomerOrder = function (val) {

        if ($scope.Customer.PassportId != undefined && $scope.Customer.PassportCreatedDate == undefined) {
            toastr.error("Bạn chưa nhập ngày cấp CMND/Passport !");
            return;
        }
        if ($scope.Customer.Name == null) {
            toastr.error("Bạn chưa nhập tên khách hàng !");
            return;
        }
        if ($scope.Customer.FistName == null) {
            toastr.error("Bạn chưa nhập họ khách hàng !");
            return;
        }
        if ($scope.Customer.Sex == null) {
            toastr.error("Bạn chưa chọn giới tính !");
            return;
        }
        if ($scope.Customer.BirthDay == null) {
            toastr.error("Bạn chưa chọn ngày sinh !");
            return;
        }
        if ($scope.stt != null) {
            item = $scope.Customer;
            $('#addCustomerToOrder').modal('hide');
            return;
        }
        var check = false;
        $.each($scope.Order.Customers, function (i, item) {
            if (item.PassportId == $scope.Customer.PassportId) {
                check = true;
                toastr.error("Khách hàng đã tồn tại trong danh sách !");
                return;
            }


        });
        if (check)
            return;
        $scope.Order.Customers.push($scope.Customer);


        if (val)
            $('#addCustomerToOrder').modal('hide');
        $scope.Customer = null;
    };



    $scope.UnitServices = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    $scope.Quantity = $scope.UnitServices[0];

    $scope.addSeviceToOrder = function () {
        var check = false;
        $.each($scope.Order.Services, function (index, item) {
            if (item.Id == $scope.OrderService.Id) {
                check = true;
                item.Quantity += $scope.Quantity;
            }
        });
        if (!check) {
            var row = {
                Name: $scope.OrderService.Name,
                Id: $scope.OrderService.Id,
                Price: $scope.OrderService.PricePaid,
                Quantity: $scope.Quantity
            }
            $scope.Order.Services.push(row);
        }
        GetTotalAmount();
        // $scope.Customer = null;

    };
    $scope.RemoveSeviceFromOrder = function (index) {
        $scope.Order.Services.splice(index, 1);
    };

    $scope.BindtoEditCustomer = function (item, i) {
        $scope.Customer = item;
        $scope.stt = i;
        $('#addCustomerToOrder').modal('show');
    };

    $scope.RemoveItemCustomer = function (index) {
        $scope.Order.Customers.splice(index, 1);
    };

    $('#id-date').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY h:mm A',
        timePicker: true,
        timePicker24Hour: true,
        timePickerIncrement: 5,
        startDate: moment(),
        minDate: moment(),
        timezone: 'local',
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Vào',
            toLabel: 'Ra',
        }
    },

        function (start, end, label) {
            $scope.Order.CheckInDate = start;
            $scope.Order.CheckOutDate = end;
        });


    $('#id-date-picker-3').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        startDate: moment(),
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Vào',
            toLabel: 'Ra',
        }

    },

    function (start, end, label) {
        $scope.Customer.PassportCreatedDate = start;
    });

    $('#id-date-picker-birthday').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        startDate: moment(),
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Vào',
            toLabel: 'Ra',
        }

    },

   function (start, end, label) {
       $scope.Customer.BirthDay = start;
   });


    var ListRoomClassForCheckOut = function () {
        var promisePost = Housekeepingservice.GetRoomsClassByRoom($scope.Order.RoomId);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.ConfigPrices = pl.data.Data;
                var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.Id == $scope.Order.ConfigPriceId });
                $scope.ConfigSelected = theItem[0].Id;
                // $scope.Order.Price = theItem[0].Price;
            }
        }, function (err) {
            console.log("Err" + err);
        });
    };


    $scope.showCheckin = function (data) {
        $scope.OrderReset();
        $scope.Order.RoomId = data.Id;
        $scope.Order.RoomName = data.Name;
        $scope.Order.LockNo = data.LockNo;
        CommonUtils.showWait(true);
        var promisePost = Housekeepingservice.GetRoomsClassByRoom($scope.Order.RoomId);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.ConfigPrices = pl.data.Data;
                var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.IsDefault == true });
                $scope.ConfigSelected = theItem[0].Id;
                $scope.Order.Price = theItem[0].Price;
            }
            else {
                toastr.error(pl.data.Error);
            }
        }, function (err) {
            console.log("Err" + err);
        });

        InitWidget();



        $("#CheckInSingle").modal('show');
    };

    $scope.UpdateServiceForOrder = function () {
        var promisePost = Housekeepingservice.UpdateServiceForOrder($scope.Order);
        promisePost.then(function (pl) {
            toastr.success("Thêm mới thành công !");
            $scope.GetRoomsByFloor();
            $scope.OrderReset();
            //$scope.Customers = null;
            $("#CheckInSingle").modal('hide');

        }, function (err) {
        });
    };



    //end


    // chon nhieu phong

    $scope.SelectedClick = function (parent) {
        var countSelected = 0;
        $.each(parent.Rooms, function (index, item) {
            if (item.Selected) countSelected++;
        });
        parent.Quanlity = countSelected;
    };

    var GetRoomsByClassForChecinMutil = function () {
        var promiseGet = Housekeepingservice.GetRoomclassForCheckin();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.RoomForCheckinMutil = pl.data.Data;
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };
    $scope.EnterRoomClick = function (val) {
        if (val.Quanlity > val.RoomAvailable) {
            toastr.error("Không được nhập quá số phòng khả dụng !");
            val.Quanlity = 1;
            return;
        }
        $.each(val.Rooms, function (index, item) {
            item.Selected = false;
        });
        for (var i = 0; i < val.Quanlity; i++) {
            val.Rooms[i].Selected = true;
        }

    };
    $scope.ShowPopupForMutil = function () {
        //GetRoomsByClassForChecinMutil();
        $('#id-date-mutil').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY h:mm A',
            timePicker: true,
            timePicker24Hour: true,
            timePickerIncrement: 5,
            startDate: moment(),
            minDate: moment(),
            timezone: 'local',
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Vào',
                toLabel: 'Ra',
            }
        },

        function (start, end, label) {
            $scope.Order.CheckInDate = start;
            $scope.Order.CheckOutDate = end;

            var promisePost = Housekeepingservice.GetRoomAvailable(start, end);
            promisePost.then(function (result) {
                if (!result.data.IsError) {
                    $scope.RoomAvailables = result.data.Data;
                }
                else {
                    toastr.error(result.data.Message);
                }
            }, function (err) {
            });
        });


        $('#id-date-picker-PassportCreatedDate').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY',
            startDate: moment(),
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Vào',
                toLabel: 'Ra',
            }

        },
        function (start, end, label) {
            $scope.Customer.PassportCreatedDate = start;
        });

        $("#CheckInMutil").modal('show');

    };

    $scope.GetRoomAvailableNow = function () {

        var promisePost = Housekeepingservice.GetRoomAvailableNow();
        promisePost.then(function (result) {
            if (!result.data.IsError) {
                $scope.RoomAvailablesNow = result.data.Data;
            }
            else {
                toastr.error(result.data.Message);
            }
        }, function (err) {
        });
    };

    $scope.AddOrderMutil = function () {
        //if ($scope.Customer.Name == undefined) {
        //    toastr.error("Bạn chưa nhập người đại diện");
        //    return;
        //}
        //if ($scope.Order.CompanyName == undefined) {
        //    toastr.error("Bạn chưa nhập tên công ty/đơn vị ! ");
        //    return;
        //}

        //$scope.Order.CustomerName = $scope.Customer.Name;
        //$scope.Order.CustomerId = $scope.Customer.Id;
        //$scope.Order.Customers.push($scope.Customer);

        $scope.Order.CustomerName = $scope.Order.Customers[$scope.CustomerIndexSelected].Name;
        $scope.Order.CustomerId = $scope.Order.Customers[$scope.CustomerIndexSelected].Id;
        $scope.Order.Customers[$scope.CustomerIndexSelected].IsPrimary = true;

        $scope.Order.RoomIds = [];

        $.each($scope.RoomAvailables, function (index, item) {

            if (item.Quanlity > 0) {
                $scope.Order.Price = item.Price;
                $.each(item.Rooms, function (i, row) {
                    if (row.Selected) {
                        $scope.Order.RoomIds.push(row.Id);
                    }

                });
            }
        });
        if ($scope.Order.RoomIds <= 0) {
            toastr.error("Cần chọn số phòng !");
            return;
        }
        var promisePost = Housekeepingservice.AddOrderMutil($scope.Order);

        promisePost.then(function (result) {
            if (!result.data.IsError) {
                toastr.success("Thêm mới thành công !");
                $scope.GetRoomsByFloor();
                $scope.OrderReset();
                //$scope.Customers = null;
                $("#CheckInMutil").modal('hide');
            }
            else {
                toastr.error(result.data.Message);
            }


        }, function (err) {
            toastr.error('erros');
        });
    };




    // end


    // get order info for edit
    $scope.GetOrderForEdit = function (val) {
        $scope.OrderReset();

        var promiseGet = Housekeepingservice.GetOrderForEdit(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Order = pl.data.Data;
                InitWidget();
                $("#CheckInSingle").modal('show');
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });
    };



    $scope.Checkout = function (val) {
        $scope.Order.isPay = true;
        $scope.CaculatorMode = "ByDay";

        var promiseGet = Housekeepingservice.GetOrderForCheckOut(val, $scope.CaculatorMode);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Order = pl.data.Data;
                
                ListRoomClassForCheckOut();
                $scope.Order.CheckInDate = new Date(parseInt($scope.Order.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $scope.Order.CheckOutDate = new Date(Date.now());
                $scope.OrderAttachs = undefined;
                GetTotalAmount();
                InitWidget();
                $scope.Invoice = $scope.Order;
                $scope.Invoice.CreatedDate = new Date();
                $scope.Invoice.InvoiceDetails = ReadyDataForPrint();

                $("#Checkout").modal('show');
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });


    };

    var ReadyDataForPrint = function () {
        result = [];
        var Descriptions = "Tiền phòng " + $scope.Order.RoomName;
        var notes = "";
        var SubAmount = 0;
        $.each($scope.Order.TimeUseds, function (i, item) {
            notes += +"\n" + item.Description;
            SubAmount += item.SumAmount;
        });

        var row = {
            Descriptions: Descriptions,
            SubAmount: SubAmount,
            Notes: notes
        };
        result.push(row);
        if ($scope.Order.Services) {
            $.each($scope.Order.Services, function (i, item) {
                var row = {
                    Descriptions: item.Name,
                    SubAmount: item.Quantity * item.Price,
                    Notes: "x" + item.Quantity
                };
                result.push(row);
            });
        }
        if ($scope.Order.OrderAttachs) {
            $.each($scope.Order.OrderAttachs, function (i, item) {
                var row = {
                    Descriptions: "Hóa đơn [" + item.OrderId + "] Phòng [" + item.RoomName + "]",
                    SubAmount: item.TotalAmount,
                    Notes: "x1"
                };
                result.push(row);
            });
        }
        return result;
    }



    $scope.ChangCalculatorMode = function () {

        var promiseGet = Housekeepingservice.GetOrderForCheckOut($scope.Order.Id, $scope.CaculatorMode);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Order = pl.data.Data;
                $scope.Order.CheckInDate = new Date(parseInt($scope.Order.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $scope.Order.CheckOutDate = new Date(Date.now());
                GetTotalAmount();
            }
            else {
                toastr.error(pl.data.Error);
            }
        },
        function (errorPl) {
        });

    };


    var GetTotalAmount = function () {
        $scope.TotalAmount = 0;
        if ($scope.Order.TimeUseds == undefined || $scope.Order.TimeUseds == null) return 0;
        $scope.Order.TotalAmount = 0;
        $.each($scope.Order.TimeUseds, function (index, item1) {
            $scope.Order.TotalAmount = $scope.Order.TotalAmount + item1.SumAmount;

            $scope.TotalAmount = $scope.TotalAmount + item1.SumAmount;
        });
        $.each($scope.Order.Services, function (index, item) {
            $scope.TotalAmount = $scope.TotalAmount + (item.Price * item.Quantity);
        });


        //$scope.Order.TotalAmount = $scope.Order.TotalAmount + parseInt($scope.Order.Surcharge);
        //$scope.Order.TotalAmount = $scope.Order.TotalAmount - (parseInt($scope.Order.Deductible) + parseInt($scope.Order.Prepay));

        //$scope.TotalAmount = ($scope.TotalAmount + parseInt($scope.Order.Surcharge)) - (parseInt($scope.Order.Deductible) + parseInt($scope.Order.Prepay));
    };

    $scope.UpdateOrder = function () {
        CommonUtils.showWait(true);
        $scope.Order.ConfigPriceId = $scope.ConfigSelected;

        var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.Id == $scope.Order.ConfigPriceId });
        $scope.Order.Price = theItem[0].Price;
        var promiseGet = Housekeepingservice.UpdateOrder($scope.Order);
        promiseGet.then(function (pl) {
            toastr.success("Cập nhật thành công !");
            CommonUtils.showWait(false);
            $("#Checkout").modal('hide');
        },
        function (errorPl) {
            toastr.error("Đã có lỗi xẩy ra !");
        });
    };

    $scope.OrderReset = function () {
        $scope.Order.Customers = [];
        $scope.Order.Services = [];
        $scope.Order.Id = 0;
    };

    $scope.CheckOutOrder = function () {
        if ($scope.Order.isPay)
            $scope.Order.OrderStatus = 2;
        else
            $scope.Order.OrderStatus = 1;

        if ($scope.OrderAttachs)
            $scope.Order.OrderAttachment = $scope.OrderAttachs;
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.UpdateOrder($scope.Order);
        promiseGet.then(function (pl) {
            toastr.success("Cập nhật thành công !");
            CommonUtils.showWait(false);
            $("#Checkout").modal('hide');
            $scope.OrderReset();
            $scope.GetRoomsByFloor();
        },
        function (errorPl) {
            toastr.error("Đã có lỗi xẩy ra !");
        });
    };
    //---------------------- check out company ----------------------------------------------

    $scope.GetOrderByCompany = function () {
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.GetOrderByCompany();
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.OrderCompany = pl.data.Data;
                CommonUtils.showWait(false);
                if ($scope.OrderCompany.length <= 0)
                    toastr.warning("Không có đoàn nào đang Checkin !");
                else
                    $("#CheckoutCompany").modal('show');
            } else toastr.error(pl.data.Error);
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
    };

    $scope.GetRoomAmount = function (val) {
        var result = 0;
        if (val) {
            $.each(val, function (index, item) {
                result += item.SumAmount;
            });
        }
        return result;
    };

    $scope.GetServiceAmount = function (val) {
        var result = 0;
        if (val) {
            $.each(val, function (index, item) {
                result += (item.Price * item.Quantity);
            });
        }
        return result;
    };

    var companyId = 0;
    $scope.CompanycheckAll = function (val) {
        if (!val) return;
        if (companyId > 0 && companyId != val.CompanyId) {
            toastr.error("Chỉ được chọn 1 công ty/đoàn !");
            val.IsSelected = false;
            return;

        }
        $.each(val.Orders, function (index, item) {
            if (val.IsSelected) companyId = val.CompanyId;
            else companyId = 0;
            item.IsSelected = val.IsSelected;
        });
    };
    $scope.CaculatorModes = [{ Name: "Theo ngày", Value: "ByDay" }, { Name: "Theo giờ", Value: "ByHour" }, { Name: "Qua đêm", Value: "ByNight" }]
    $scope.CompanyOrderchecked = function (val, child) {
        if (!val) return;
        if (companyId > 0 && companyId != val.CompanyId) {
            toastr.error("Chỉ được chọn 1 công ty/đoàn !");
            val.IsSelected = false;
            child.IsSelected = false;
            return;
        }
        var check = false;
        $.each(val.Orders, function (index, item) {
            if (item.IsSelected) check = true;
        });
        val.IsSelected = check;
    };

    $scope.CheckOutCompany = function () {
        var Selecteds = [];
        $.each($scope.OrderCompany, function (index, item) {
            if (item.IsSelected) {
                $.each(item.Orders, function (i, row) {
                    if (row.IsSelected) Selecteds.push(row.Id);
                });
            }
        });

        if (Selecteds.length <= 0) {
            toastr.error("Cần chọn hóa đơn trước khi thanh toán !");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Housekeepingservice.CompanyDoCheckOut(Selecteds, "ByDay");
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Order = pl.data.Data.OrderKey;
                $scope.Order.CheckInDate = new Date(parseInt($scope.Order.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $scope.Order.CheckOutDate = new Date(Date.now());
                $scope.OrderAttachs = pl.data.Data.OrderAttach;
                $scope.CaculatorMode = "ByDay";
                GetTotalAmount();

                InitWidget();
                $scope.Invoice = $scope.Order;
                $scope.Invoice.CreatedDate = new Date();
                $scope.Invoice.InvoiceDetails = ReadyDataForPrint();
                CommonUtils.showWait(false);
                $("#CheckoutCompany").modal('hide');
                $("#Checkout").modal('show');
            }
            else toastr.error(pl.data.Error);
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
    };
    $scope.TotalAmountAttach = function () {
        var result = 0;
        if (!$scope.OrderAttachs) return 0;
        $.each($scope.OrderAttachs, function (index, item) {
            result += item.TotalAmount;
        });
        return result;
    };
    $('#Checkout').on('hide.bs.modal', function () {
        $scope.OrderAttachs = undefined;
    });

    $('#CheckInMutil').on('hide.bs.modal', function () {
        // $scope.Order = undefined;
        $scope.Customer = undefined;
    });
    $('#CheckoutCompany').on('hide.bs.modal', function () {
        companyId = 0;
    });
    $('#StaticRooms').on('shown.bs.modal', function () {
        $("#calendar").fullCalendar('render');
    });

    $('#StaticRooms').on('hide.bs.modal', function () {
        $("#calendar").fullCalendar('destroy');
    });


    // ------------------ end -----------------------------------------------------------

    //--------------------- check hien trang phong va dat phong truoc --------------------
    $scope.ViewStateModes = [{ Name: "Ngày", Value: "ByDay" }, { Name: "Giờ", Value: "ByHour" }];
    $scope.ShowPanelTime = function () {
        $scope.ViewStateMode = 'ByDay';


        $('#id-date-picker-From').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY',
            timezone: 'local',
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Từ',
                toLabel: 'Đến',
            }
        },

        function (start, end, label) {
            $scope.SearchRoomModel.FromDate = start;
            $scope.SearchRoomModel.ToDate = end;

            $scope.SearchRoomModel.FromDateView = start.format('DD/MM/YYYY');
            $scope.SearchRoomModel.ToDateView = end.format('DD/MM/YYYY');
            $scope.days = (end - start) / 1000 / 60 / 60 / 24;
            $scope.GetRoomsStaticByTime();
        });

        $('#PanelTime').modal('show');
    };
    $scope.SearchRoomModel = {
        FromDate: null,
        ToDate: null,
        FromTime: null,
        ToTime: null,
        SearchBy: 'ByDay'
    };
    $scope.GetRoomsStaticByTime = function () {
        $scope.SearchRoomModel.SearchBy = $scope.ViewStateMode;
        var promiseGet = Housekeepingservice.GetRoomsStaticByTime($scope.SearchRoomModel);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.RoomStatic = pl.data.Data;
                CommonUtils.showWait(false);
                $("#PanelTime").modal('hide');
                $("#StaticRooms").modal('show');

                $('#calendar').fullCalendar({
                    now: $scope.SearchRoomModel.FromDate,
                    editable: false,
                    lang: 'vi',
                    aspectRatio: 1.8,
                    scrollTime: '00:00',
                    defaultView: 'timelineTenDay',
                    views: {
                        timelineTenDay: {
                            type: 'timeline',
                            duration: { days: $scope.days },
                            slotDuration: '12:00'
                        }
                    },
                    resourceAreaWidth: '100px',
                    resourceAreaHeight: '30px',
                    resourceLabelText: 'Phòng',
                    resources: $scope.RoomStatic.Rooms,
                    events: $scope.RoomStatic.events
                });
            }
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
    };


    $scope.changeDateFilter = function () {
        $("#PanelTime").modal('show');
        $("#StaticRooms").modal('hide');
    };


    //--------------------- end ----------------------------------------------------------









    //------------------------ Booking -----------------------------------------

    $scope.showPopupBooking = function (data) {
        //$scope.Order.CheckInDate = Date.now().toString();
        $scope.Order.RoomId = data.Id;
        $scope.Order.RoomName = data.Name;
        var promisePost = Housekeepingservice.GetRoomsClassByRoom($scope.Order.RoomId);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {

                $scope.RoomClass = pl.data;
                $scope.Order.Price = $scope.RoomClass.PriceByDay;
            }
        }, function (err) {
            console.log("Err" + err);
        });
        InitWidget();

        $('#id-date-mutile').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY h:mm A',
            startDate: new Date(),
            timePicker: true,
            timePicker24Hour: true,
            timezone: 'local',
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Vào',
                toLabel: 'Ra',
            }
        },
        function (start, end, label) {
            $scope.Order.CheckInDate = start;
            $scope.Order.CheckOutDate = end;

            var promisePost = Housekeepingservice.GetRoomAvailable(start, end);
            promisePost.then(function (result) {
                if (!result.data.IsError) {
                    $scope.RoomAvailables = result.data.Data;
                }
                else {
                    toastr.error(result.data.Message);
                }
            }, function (err) {
            });
        });


        $('#id-date-picker-3').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY',
            startDate: moment(),
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Vào',
                toLabel: 'Ra',
            }

        },

      function (start, end, label) {
          $scope.Customer.PassportCreatedDate = start;
      });



        $("#Booking").modal('show');
    };

    $scope.DoBookingOrder = function () {
        if ($scope.Order.CheckOutDate == undefined) {
            toastr.error("Cần nhập ngày ra dự kiến !");
            return;
        }
        if ($scope.Order.Customers.length <= 0) {
            toastr.error("Cần nhập khách hàng !");
            return;
        }
        CommonUtils.showWait(true);

        $scope.Order.CustomerName = $scope.Order.Customers[$scope.CustomerIndexSelected].Name;
        $scope.Order.CustomerId = $scope.Order.Customers[$scope.CustomerIndexSelected].Id;
        $scope.Order.Customers[$scope.CustomerIndexSelected].IsPrimary = true;



        var promisePost = Housekeepingservice.BookingOrder($scope.Order);
        promisePost.then(function (pl) {
            toastr.success("Thêm mới thành công !");
            $scope.GetRoomsByFloor();
            $("#Booking").modal('hide');
            CommonUtils.showWait(false);

        }, function (err) {
            toastr.error(err.statusText);
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



    $scope.GetBookingOrders = function () {
        $scope.Filter.OrderStatus = 5;
        var promisePost = Housekeepingservice.GetBookingOrders($scope.Filter);
        promisePost.then(function (pl) {

            if (!pl.data.HasError) {
                $scope.OrderBookings = pl.data.Data;

                if ($scope.OrderBookings.length > 0) {
                    $.each($scope.OrderBookings, function (index, item) {
                        item.CheckInDate = new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CheckOutDate = new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CreatedDate = new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    });

                    //$("#OrderBookings").modal('show');
                    $("#CheckInMutilNew").modal('show');
                } else
                    toastr.warning('Hiện không có phòng được đặt trước !');
            }
        }, function (err) {
            console.log("Err" + err);
        });
    };

    $scope.GetBookingOrdersUnPrePay = function () {
        var promisePost = Housekeepingservice.GetBookingOrdersUnPrePay();
        promisePost.then(function (pl) {

            if (!pl.data.HasError) {
                $scope.OrderBookingsUnprepay = pl.data.Data;

                if ($scope.OrderBookingsUnprepay.length > 0) {
                    $.each($scope.OrderBookingsUnprepay, function (index, item) {
                        item.CheckInDate = new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CheckOutDate = new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CreatedDate = new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    });

                    //$("#OrderBookings").modal('show');
                    $("#CheckInMutilNew").modal('show');
                }
            }
        }, function (err) {
            console.log("Err" + err);
        });
    };

    $scope.GetBookingOrdersNearDate = function () {
        var promisePost = Housekeepingservice.GetBookingOrdersNearDate();
        promisePost.then(function (pl) {

            if (!pl.data.HasError) {
                $scope.OrderBookingsNearDate = pl.data.Data;

                if ($scope.OrderBookingsNearDate.length > 0) {
                    $.each($scope.OrderBookingsNearDate, function (index, item) {
                        item.CheckInDate = new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CheckOutDate = new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CreatedDate = new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    });

                    //$("#OrderBookings").modal('show');
                    $("#CheckInMutilNew").modal('show');
                }
            }
        }, function (err) {
            console.log("Err" + err);
        });
    };





    $scope.TranferBookingToCheckIn = function (val) {
        //GetRoomsByClassForChecinMutil();
        $scope.OrderIdChange = val;
        var promisePost = Housekeepingservice.TranferBookingToCheckIn(val);
        promisePost.then(function (pl) {
            if (pl.data.IsError) {

                if (pl.data.Message != 'Chưa tới ngày nhận phòng') {
                    toastr.error(pl.data.Message);
                    $('#RoomAvailable').modal('show');
                    //ShowPopupchangeRoom($scope.OrderIdChange);
                    $('#CheckInMutilNew').modal('hide');
                }
                else {
                    toastr.error(pl.data.Message);
                }
            }
            else {
                toastr.success('Nhận phòng thành công !');
                GetRoomsByClassForChecinMutil()
                $scope.GetBookingOrders();
                $scope.GetRoomsByClass();
                $('#CheckInMutilNew').modal('hide');
            }

        }, function (err) {
            toastr.error(err.statusText);
        });

    };
    $scope.RadioSelected = function (val) {
        $scope.RoomIdChange = val;
    };
    $scope.ShowPopupchangeRoom = function (val) {
        $scope.OrderIdChange = val;
        GetRoomsByClassForChecinMutil()
        $('#RoomAvailable').modal('show');
    };

    $scope.ChangeRoomInOrder = function () {
        if ($scope.OrderIdChange == undefined || $scope.RoomIdChange == undefined) {
            toastr.error('Cần chọn phòng sử dụng !');
            return;
        }
        var promisePost = Housekeepingservice.ChangeRoomInOrder($scope.OrderIdChange, $scope.RoomIdChange);
        promisePost.then(function (pl) {
            if (pl.data.IsError) {
                toastr.error(pl.data.Message);
            }
            else {
                toastr.success('Đổi phòng thành công !');
                $scope.GetRoomsByFloor();
            }
            $('#RoomAvailable').modal('hide');
        }, function (err) {
            toastr.error(err.statusText);
        });
    };




    //==================================== End ---------------------------------------------




    //===================================== khach hang -----------------------------------



    $scope.SelectCustomerExist = function (val) {
        val.PassportCreatedDate = new Date(parseInt(val.PassportCreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
        $('#id-date-picker-3').data('daterangepicker').setStartDate(val.PassportCreatedDate);
        $('#id-date-picker-3').data('daterangepicker').setEndDate(val.PassportCreatedDate);
        $scope.Customer = val;

    };

    $scope.GetCustomerPassportId = function () {
        if ($scope.Customer.PassportId == undefined) {
            return;
        }
        var promisePost = Housekeepingservice.GetCustomerPassportId($scope.Customer.PassportId);
        promisePost.then(function (pl) {
            $scope.CustomerResults = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };



    //=================================== Room and Floor ===========================
    $scope.GetRoomForEdit = function (val) {
        var promisePost = Housekeepingservice.GetRoomForEdit(val);
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

        var promisePost = Housekeepingservice.GetListFloor();
        promisePost.then(function (pl) {
            $scope.FloorList = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    var GetListRoomClass = function (val) {

        var promisePost = Housekeepingservice.GetListRoomClass();
        promisePost.then(function (pl) {
            
            $scope.RoomClassList = pl.data.Data;

        }, function (err) {
            toastr.error(err.statusText);
        });
    };
    $scope.EditRoom = function () {

        var promisePost = Housekeepingservice.EditRoom($scope.Room);
        promisePost.then(function (result) {
            if (!result.data.IsError) {
                $scope.GetRoomsByFloor();
                $("#EditRoom").modal("hide");
                toastr.success("Cập nhật thành công !");
            }

        }, function (err) {
            toastr.error(err.statusText);
        });
    };
    $scope.ShowpopUpBookingForEdit = function (val) {
        var promiseGet = Housekeepingservice.GetBookingForChecking(val);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Order = pl.data.Data;
                $scope.Order.CheckInDate = new Date(parseInt($scope.Order.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $scope.Order.CreatedDate = new Date(parseInt($scope.Order.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $("#BookingForEdit").modal("show");
            }

            else {
                toastr.error(pl.data.Error);
            };

        });
    };

    //=================================================================


    $scope.PrintInvoice = function () {
        var divToPrint = document.getElementById('printContent');

        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        frame1.css({ "position": "absolute", "top": "-1000000px" });
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        //Create a new HTML document.
        frameDoc.document.write('<html><head><title>Hóa đơn thanh toán</title>');
        frameDoc.document.write('</head><body>');
        //Append the external CSS file.
        frameDoc.document.write('<link href="../Areas/CPanelAdmin/Content/css/printer.css" rel="stylesheet" type="text/css" />');
        //Append the DIV contents.
        frameDoc.document.write(divToPrint.innerHTML);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            frame1.remove();
        }, 500);

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
    }
    //===================================== end -----------------------------------------
});

app.directive('chosen', function ($timeout) {

    var linker = function (scope, element, attr) {

        scope.$watch('Widgets', function () {
            $timeout(function () {
                element.trigger('chosen:updated');
            }, 0, false);
        }, true);

        $timeout(function () {
            element.chosen();
        }, 0, false);
    };

    return {
        restrict: 'A',
        link: linker
    };
});
app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    }
});
