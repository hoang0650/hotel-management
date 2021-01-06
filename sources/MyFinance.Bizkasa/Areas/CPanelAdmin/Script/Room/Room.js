app.service("Roomservice", function ($http) {
    this.POST = function (model) {
        var request = $http({
            method: "post",
            url: model.url,
            dataType: 'json',
            data: model.data,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GET = function (model) {
        var request = $http({
            method: "get",
            url: model.url,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };


    this.GetListCustomerCheckIn = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/GetListCustomerCheckIn",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetWidgets = function () {
        var request = $http({
            method: "get",
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


    this.GetConfigPriceByRoom = function (id) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetConfigPriceBy",
            data: JSON.stringify({ roomid: id })
        });
        return request;
    }

   
    this.AddOrder = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/AddOrder",
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

   


    this.GetOrderForEdit = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderForEdit",
            data: JSON.stringify({ orderId: val })
        });
        return request;
    }

    this.GetOrderForCheckOut = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderForCheckOut",
            data: model,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
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
            method: "get",
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
            contentType: 'application/json; charset=utf-8',
            data:model
        });
        return request;
    }

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
            url: "/CPanelAdmin/Order/GetCountries",
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

    this.GetRoomsByShort = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomsByShort",
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.DeleteRoom = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/DeleteRoom",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({roomId:val})
        });
        return request;
    }
    this.GetHotelInfo = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Hotel/GetHotelInfo",
            dataType: 'json',
            cache: true,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetStaticRoom = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetStaticRoom"
        });
        return request;
    }
    this.GetRoomsByStatus = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetRoomsByStatus",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ status: val })
        });
        return request;
    }

    this.GetPaymentMethod = function () {
        var request = $http({
            method: "get",
            cache: true,
            url: "/CPanelAdmin/Room/GetPaymentMethod"
        });
        return request;
    };
    this.GetCaculatorMode = function () {
        var request = $http({
            method: "get",
            cache: true,
            url: "/CPanelAdmin/Room/GetCaculatorMode"
        });
        return request;
    };

    this.ChangeStatusRoom = function (id,status) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/ChangeStatusRoom",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ roomId: id, status: status })
        });
        return request;
    }
    this.GetFolioCustomer = function (val, typeCustomer) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Order/GetFolioCustomer",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ companyId: val, typeCustomer: typeCustomer })
        });
        return request;
    }
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

    this.GetOrderBookingByCompany = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Order/GetOrderBookingByCompany",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetInvoices = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/GetInvoices",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.AddOrderDetail = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Order/AddOrderDetail",
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
});


app.controller("RoomController", function ($scope, Roomservice, hotkeys) {
   
    $scope.FilterCutomer = {
        Page: {
            currentPage: 1,
            pageSize: 20,
            total: 0
        },
        FromDate: undefined,
        ToDate: undefined,
        Keyword: undefined

    };
    $scope.GetListCustomerCheckIn = function (val) {

        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetListCustomerCheckIn($scope.FilterCutomer);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.CustomerCheckIns = pl.data.Data.Data;
                $scope.FilterCutomer.Page.total = pl.data.Data.TotalRecord;

            }
            else
                toastr.error(pl.data.Message);
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };

    $scope.ViewInvoiceDetail = function (val) {
        $.each($scope.Invoices, function (index, item) {
            if (item.Id == val) {
                $scope.Invoice = item;
            }
        });
        $('#ViewInvoiceDetail').modal('show');
    };

    $scope.GetInvoices = function (val) {
        $scope.SumAmount = {
            Amount: 0,
            Paid: 0,
            Debit: 0
        };
        if (val)
            $scope.Filter.InvoiceType = val
        CommonUtils.showWait(true);
        $scope.Filter.IsShowInDay = true;
        var promiseGet = Roomservice.GetInvoices($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Invoices = pl.data.Data.DataPaging.Data;
                $scope.SumAmount.Paid = pl.data.Data.Summary.TotalAmount;
                $scope.SumAmount.Debit = pl.data.Data.Summary.DeductibleAmount;
                $scope.Filter.Page.total = pl.data.Data.DataPaging.TotalRecord;
                CommonUtils.showWait(false);
            }
        },
        function (errorPl) {
        });
    };
    $scope.PagingInvoicesAct = function (page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetInvoices();
    };
    $scope.PagingCustomerCheckInAct = function (page, pageSize, total) {
        $scope.FilterCustomer.Page.currentPage = page;
        $scope.GetListCustomerCheckIn();
    };

    hotkeys.add('f3', 'chon dich vu', function (event) {
        event.preventDefault();
        $("#ex1_value").focus();
    });

    hotkeys.add('f10', 'print', function (event) {
        event.preventDefault();
        $scope.PrintInvoice();
    });

    hotkeys.add('f9', 'tra phong', function (event) {
        event.preventDefault();
        $scope.CheckOutOrder();
    });
    //GetAllRecords();
    //To Get All Records  
    $scope.CompanySelected = function (selected) {
        if (selected) {
            $scope.Order.CompanyName = selected.originalObject.Name;
            $scope.Order.CompanyId = selected.originalObject.Id;
            $scope.Order.CompanyMobile = selected.originalObject.Mobile;
            $scope.Order.CompanyAdd = selected.originalObject.Address;
            $scope.Order.Company = {
                Id: selected.originalObject.Id,
                Address: selected.originalObject.Address,
                Mobile: selected.originalObject.Mobile,
                Name: selected.originalObject.Name
            };
        } else {
            console.log('cleared');
        }
    };
    $scope.SearchByPassport = function (str) {
        return { passportId: str };
    };

    $scope.CustomerSelected = function (selected) {
        if (selected) {
            $scope.Customer = {
               
                Address: selected.originalObject.Address,
                Mobile: selected.originalObject.Mobile,
                Name: selected.originalObject.Name,
                PassportId: selected.originalObject.PassportId,
                Email: selected.originalObject.Email,
                Notes: selected.originalObject.Notes,
                //PassportCreatedDate: new Date(parseInt(selected.originalObject.PassportCreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')))
            };
            $("#pas_value").val($scope.Customer.PassportId);
            $("#cus_value").val($scope.Customer.Name);
            $("#passportDate").data("daterangepicker").setEndDate(selected.originalObject.BirthDateView);
        } else {
           
        }
    };
    

    $scope.remoteUrlRequestCompanyFn = function (str) {
        return { CustomerType: 2, Keyword: str };
    };


    $scope.remoteUrlRequestcustomerFn = function (str) {
        return { CustomerType: 1, Keyword: str };
    };

    $scope.AddMoreRoomPopup = function (val) {
        $scope.Order.CompanyIdToAdd = val.CompanyId;

        $('#id-date-addmore').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY h:mm A',
            timePicker: true,
            timePicker24Hour: true,
            timePickerIncrement: 5,
            startDate: moment(),
            minDate: moment(),
            timezone: 'Asia/Ho_Chi_Minh',
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


           var promisePost = Roomservice.GetRoomAvailable(start, end);
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


        $("#AddMoreRoomBooking").modal("show");
        $("#CheckInMutilNew").modal("hide");


    };


    $scope.GetDataFolio = function (val, typeCustomer) {

        var promiseGet = Roomservice.GetFolioCustomer(val, typeCustomer);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {

                $scope.Folio = pl.data.Data;
                $scope.Folio.CheckInDate = new Date(parseInt($scope.Folio.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $scope.Folio.CheckOutDate = new Date(parseInt($scope.Folio.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
            }

            else {
                toastr.error(pl.data.Message);
            };

        });

        CommonUtils.showWait(true);
        setTimeout(function () {
            $scope.PrintInvoice('printfolio');
        }, 2000);
        CommonUtils.showWait(false);
    };


    $scope.ChangeStatusRoom = function (id,status) {

        CommonUtils.showWait(true);
        var promiseGet = Roomservice.ChangeStatusRoom(id, status);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.ModeOrderRoom();
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
    };


    $scope.GetPaymentMethod = function () {

        var promiseGet = Roomservice.GetPaymentMethod();
        promiseGet.then(function (result) {
            $scope.PaymentMethod=   JSON.parse(result.data)
           
        },
        function (errorPl) {
        });
    };

    $scope.GetCaculatorMode = function () {

        var promiseGet = Roomservice.GetCaculatorMode();
        promiseGet.then(function (result) {
            $scope.CaculatorModes = JSON.parse(result.data)

        },
        function (errorPl) {
        });
    };


    $scope.GetRoomsByStatus = function (val) {

        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetRoomsByStatus(val);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Floors = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
    };



    $scope.HotelConfig = function () {

        var promiseGet = Roomservice.GetStaticRoom();
        promiseGet.then(function (result) {
            ConvertToRoomStatus(result.data.Data);
        },
        function (errorPl) {
        });
    };
    var ConvertToRoomStatus = function (val) {
        var data = val.split(';');
        if (data.length > 0) {
            var array = [];
            for (var i = 0; i < data.length; i++) {
                var item = data[i].split('_');
                var row = {
                    Key: item[0],
                    Value: item[2],
                    Color: item[1],
                    Num: item[3],
                    Class:undefined
                };

                switch (item[0]) {
                    case "Active":
                        row.Class = ' fa fa-bed';
                        break;
                    case "InActive":
                        row.Class = ' fa-check-circle';
                        break;
                    case "Refresh":
                        row.Class = ' fa fa-recycle';
                        break;
                    case "Booking":
                        row.Class = ' fa fa-street-view';
                        break;
                    case "Repair":
                        row.Class = ' fa-wrench';
                        break;

                }
              
                array.push(row);
            }
            $scope.RoomStatus = array;
        }
    };

    $scope.GetHotelInfo = function () {
        var promiseGet = Roomservice.GetHotelInfo();
        promiseGet.then(function (pl) {
            $scope.Hotel = pl.data.Data;
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
        var promiseGet = Roomservice.DeleteRoom(val);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success('Xóa thành công !');
                $scope.GetRoomsByFloor();
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
    };


    $scope.ModeOrder = "ByShort";
    $scope.ModeOrderRoom = function () {
        if ($scope.ModeOrder == "ByFloor") {
            $scope.GetRoomsByFloor();
        }
        if ($scope.ModeOrder == "ByRoomClass") {
            $scope.GetRoomsByClass();
        }

        if ($scope.ModeOrder == "ByShort") {
            $scope.GetRoomsByShort();
        }
    };


    var InitCountries = function () {
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetListCountries();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                // $scope.Widgets = pl.data.Data;
                $scope.Countries = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });
    };
    $scope.InitWidget = function () {
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetWidgets();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                // $scope.Widgets = pl.data.Data;
                $scope.Widgets = $.map(pl.data.Data,function (n,i) {
                    return $.map(n.Widgets, function (item,index) {
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
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });
    };

    $scope.GetRoomsByFloor = function () {
        
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetRoomsByFloor();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError)
            {
                $scope.Floors = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else
            {
                toastr.error(pl.data.Message);
            }
          
            
        },
        function (errorPl) {
        });
    };

    $scope.GetRoomsByClass = function () {

        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetRoomsByClass();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Floors = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }


        },
        function (errorPl) {
        });
    };
    $scope.GetRoomsByShort = function () {

        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetRoomsByShort();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Rooms = pl.data.Data;
               
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);

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
            if(item.Id==id)
            {
               
                $scope.Roomtype = item;
                $scope.CheckoutDayList= ToList($scope.Roomtype.CheckoutDayList,false) ;
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
        var promiseGet = Roomservice.GetRoomclass();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                
                $scope.RoomClasses = pl.data.Data;
                CommonUtils.showWait(false);
            }
            else {
                toastr.error(pl.data.Message);
            }
           
        },
        function (errorPl) {
        });
    };

    $scope.AutoGeneralRoom = function () {
        var promiseGet = Roomservice.AutoGeneralRoom();
        promiseGet.then(function (pl) {
           
        },
        function (errorPl) {
        });
    };



    // them loai phong
    var InitRow = function (Ilist,iscus) {
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
        if (FromList == null || FromList.length<=0)
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
        var row = InitRow($scope.CheckoutDayList,false);
        $scope.CheckoutDayList.push(row);

    };
    $scope.CheckoutDayRemoveclick = function (data) {
        removeOneRow($scope.CheckoutDayList,data,false);     

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
        var row = InitRow($scope.CheckinDayList,false);
        $scope.CheckinDayList.push(row);

    };
    $scope.CheckinDayRemoveclick = function (data) {

        removeOneRow($scope.CheckinDayList, data,false);

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
    
   

    // end


    
    // checkin 
    $scope.Order = {
        CheckInDate: null,
        TimeCheckin:null,
        RoomId: 0,
        RoomName: null,
        Price: 0,
        Customers: [],
        Services: [],
        RoomIds:[]

    };
    $scope.Customer = {
        PassportId: undefined,
        Name: undefined,
        Id: 0,
        IsPrimary:false
    };
    $scope.CustomerIndexSelected = 0;
    $scope.addCutomerOrder = function (val) {
        

        $scope.Customer.PassportId= $("#pas_value").val();
        if ( $scope.Customer.PassportId  && $scope.Customer.PassportCreatedDate==undefined)
        {
            toastr.error("Bạn chưa nhập ngày cấp CMND/Passport !");
            return;
        }

        //if ($scope.Customer.PassportId != "" && $scope.Customer.PassportCreatedDate == undefined) {
        //    toastr.error("Bạn chưa nhập ngày cấp CMND/Passport !");
        //    return;
        //}

        $scope.Customer.Name = $("#cus_value").val();
        if ($scope.Customer.Name == undefined) {
            toastr.error("Bạn chưa nhập tên khách hàng !");
            return;
        }
        var check = false;
        $.each($scope.Order.Customers, function (i,item) {
            if (item.Id == $scope.Customer.Id)
            {
                check = true;
                toastr.error('Khách hàng đã tồn tại trong danh sách !');
                return;
            }

        });
        if (check)
            return;
        $scope.Order.Customers.push($scope.Customer);        
        ResetFormCustomer();
        if (val)
            $('#addCustomerToOrder').modal('hide');

    };

    var ResetFormCustomer = function () {
        $scope.Customer = {};
        $("#cus_value").val(undefined)
        $("#pass_value").val(undefined)
    };
    $scope.UnitServices = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    $scope.Quantity = $scope.UnitServices[0];

    $scope.addSeviceCheckin = function (data) {
        if (!$scope.OrderService) {
            toastr.error("Cần chọn dịch vụ trước khi thêm !");
            return;
        }
        var row = {
            Name: $scope.OrderService.Name,
            Id: $scope.OrderService.Id,
            Price: $scope.OrderService.PricePaid,
            Quantity: $scope.Quantity
        };
        var isExist = false;
        if ($scope.Order.Services) {
            $.each($scope.Order.Services, function (i, n) {

                if ($scope.OrderService.Id == n.Id) {
                    n.Quantity = n.Quantity + $scope.Quantity;
                    isExist = true;
                }
            });
        }
        if (!isExist)
            $scope.Order.Services.push(row);
        //GetTotalAmount();
        // $scope.Customer = null;

    };


    $scope.addSeviceToOrder = function (id) {
        if (!$scope.OrderService) {
            toastr.error("Cần chọn dịch vụ trước khi thêm !");
            return;
        }




        var row = {
            Title: $scope.OrderService.Name,
            RelatedId: $scope.OrderService.Id,
            Price: $scope.OrderService.PricePaid,
            OrderId: $scope.Order.Id,
            Quantity: $scope.Quantity,
            DetailTypeId: 2//service
        };
        addOrderDetail(row, id);


    };
    $scope.addSurchargeToOrder = function (id) {
        if (!$scope.Surcharge) {
            toastr.error("Cần nhập thông tin phụ thu !");
            return;
        }
        if (!$scope.Surcharge.Price) {
            toastr.error("Chưa nhập số tiền !");
            return;
        }
        if (!$scope.Surcharge.Title) $scope.Surcharge.Title = "";
        var row = {
            Title: "[Phụ thu] - " + $scope.Surcharge.Title,
            RelatedId: undefined,
            Price: $scope.Surcharge.Price,
            OrderId: $scope.Order.Id,
            Quantity: 1,
            DetailTypeId: 10//Surcharge
        };
        addOrderDetail(row, id);
        $scope.Surcharge = {};
    };

    $scope.addDeductibleToOrder = function (id) {
        if (!$scope.Deductible) {
            toastr.error("Cần nhập thông tin khuyến mãi !");
            return;
        }
        if (!$scope.Deductible.Price) {
            toastr.error("Chưa nhập số tiền !");
            return;
        }
        if (!$scope.Deductible.Title) $scope.Deductible.Title = "";
        var row = {
            Title: "[Giảm trừ] - " + $scope.Deductible.Title,
            RelatedId: undefined,
            Price: $scope.Deductible.Price,
            OrderId: $scope.Order.Id,
            Quantity: 1,
            DetailTypeId: 11//Surcharge
        };
        addOrderDetail(row, id);
        $scope.Deductible = {};
    };
    $scope.addPrepaidToOrder = function (id) {
        if (!$scope.Prepaid) {
            toastr.error("Cần nhập thông tin trả trước !");
            return;
        }
        if (!$scope.Prepaid.Price) {
            toastr.error("Chưa nhập số tiền !");
            return;
        }
        if (!$scope.Prepaid.Title) $scope.Prepaid.Title = "";
        var row = {
            Title: "[Trả trước] - " + $scope.Prepaid.Title,
            RelatedId: undefined,
            Price: $scope.Prepaid.Price,
            OrderId: $scope.Order.Id,
            Quantity: 1,
            DetailTypeId: 12//Surcharge
        };
        addOrderDetail(row, id);
        $scope.Prepaid = {};
    };


    function addOrderDetail(row,id) {
        var promisePost = Roomservice.AddOrderDetail(row);
        promisePost.then(function (pl) {
            CommonUtils.showWait(false);
            if (!pl.data.IsError) {
                var model = {
                    OrderId: $scope.Order.Id,
                    CaculatorMode: $scope.Order.CaculatorMode
                };
              
                $('#' + id).collapse('hide');
                $scope.Checkout(model);
                toastr.success("Cập nhật thành công");
            }
            else {
                toastr.error(pl.data.Message);
            }

        });
    };



    $scope.RemoveSeviceFromOrder = function (row) {

        var req = {
            data: row,
            url: "/CPanelAdmin/Order/DeleteOrderDetail"
        };
        var promisePost = Roomservice.POST(req);
        promisePost.then(function (pl) {
            CommonUtils.showWait(false);
            if (!pl.data.IsError) {
                var model = {
                    OrderId: $scope.Order.Id,
                    CaculatorMode: $scope.Order.CaculatorMode
                };
                $scope.Checkout(model);
                toastr.success("Cập nhật thành công");
            }
            else {
                toastr.error(pl.data.Message);
            }

        });
    };

    $scope.BindtoEditCustomer = function (item) {
        $scope.Customer = item;
    };

    $scope.RemoveItemCustomer = function (index) {
        $scope.Order.Customers.splice(index, 1);
    };


    $scope.GetPriceDefault = function (val) {       
        var promisePost = Roomservice.GetConfigPriceByRoom(val);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.ConfigPrices = pl.data.Data;
                var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.IsDefault == true });
                $scope.ConfigSelected = theItem[0].Id;
                $scope.Order.Price = theItem[0].PriceByDay;
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
        });
    };

    $scope.showCheckin = function (val,isCheckin) {
        $scope.OrderReset();
        if (isCheckin) {
            $scope.Order.RoomId = val.Id;
            $scope.Order.RoomClassName = val.RoomClassName;
            $scope.GetPriceDefault($scope.Order.RoomId);
        }
        $scope.InitWidget();
        $('#id-date').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY h:mm A',
            timePicker: true,
            timePicker24Hour: true,
            timePickerIncrement: 5,
            startDate: moment(),
            minDate:moment(),
            timezone: 'Asia/Ho_Chi_Minh',
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Vào',
                toLabel: 'Ra',
                "daysOfWeek": [
                  "CN",
                  "T2",
                  "T3",
                  "T4",
                  "T5",
                  "T6",
                  "T7"
                ],
                "monthNames": [
                    "Tháng 1",
                    "Tháng 2",
                    "Tháng 3",
                    "Tháng 4",
                    "Tháng 5",
                    "Tháng 6",
                    "Tháng 7",
                    "Tháng 8",
                    "Tháng 9",
                    "Tháng 10",
                    "Tháng 11",
                    "Tháng 12"
                ],
            }
        },

        function (start, end, label) {
            $scope.Order.CheckInDate = start;
            $scope.Order.CheckOutDate = end;           
        });

      
        $('#passportDate').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY',
            startDate: moment(),
            singleDatePicker: true,
            showDropdowns: true,
            timezone: 'Asia/Ho_Chi_Minh',
            locale: {
                applyLabel: 'Đồng ý',
                cancelLabel: 'Hủy',
                fromLabel: 'Từ ',
                toLabel: 'Đến',
                "daysOfWeek": [
                     "CN",
                     "T2",
                     "T3",
                     "T4",
                     "T5",
                     "T6",
                     "T7"
                ],
                "monthNames": [
                    "Tháng 1",
                    "Tháng 2",
                    "Tháng 3",
                    "Tháng 4",
                    "Tháng 5",
                    "Tháng 6",
                    "Tháng 7",
                    "Tháng 8",
                    "Tháng 9",
                    "Tháng 10",
                    "Tháng 11",
                    "Tháng 12"
                ]
            }
           
        },

        function (start, end, label) {
            $scope.Customer.BirthDate = start;
        });


        $("#CheckInSingle").modal('show');
    };

    $scope.CheckinNow = function (val, isCheckin,typeCheckin) {
        $scope.OrderReset();
        if (isCheckin) {
            $scope.Order.RoomId = val.Id;
            $scope.Order.RoomClassName = val.RoomClassName;
            $scope.Order.RoomName = val.Name;
            $scope.Order.CustomerName = "Khách lẻ";
            $scope.Order.CaculatorMode = typeCheckin;
        }
        $scope.AddOrderSingle();
    };

    $scope.AddOrderSingle = function () {      
      
        $scope.Order.OrderStatus = 4;
   

        
        if ($scope.Customer.Name && $scope.Customer.PassportId) {
            
            var customer = {
                Name: $scope.Customer.Name,
                PassportId: $scope.Customer.PassportId,
                IsPrimary: true,
                BirthDate: $scope.Customer.BirthDate,
                Address:$scope.Customer.Address
            }
            $scope.Order.Customers.push(customer);
        } else if ($("#cus_value").val() && $("#pas_value").val()) {
            var customer = {
                Name: $("#cus_value").val(),
                PassportId: $("#pas_value").val(),
                IsPrimary: true,
                BirthDate: $scope.Customer.BirthDate,
                Address:$scope.Customer.Address
            }
            $scope.Order.Customers.push(customer);
        }

       
        if ($scope.Order.Customers.length > 0) {
            $scope.Order.CustomerName = $scope.Order.Customers[0].Name;
            $scope.Order.PassportId = $scope.Order.Customers[0].PassportId;
           // $scope.Order.Customers[$scope.CustomerIndexSelected].IsPrimary = true;
        }
        if (!$scope.Order.CustomerName)
            $scope.Order.CustomerName = "Khách không CMT";
       

        CommonUtils.showWait(true);
        var promisePost = Roomservice.AddOrder($scope.Order);
        promisePost.then(function (pl) {
            if (!pl.data.IsError)
            {
                
                toastr.success("Thêm mới thành công !");
                var modelCheckOut = {
                    OrderId: pl.data.Data,
                    CaculatorMode: $scope.Order.CaculatorMode=='ByHour'?3:2
                }
                
                //
                $scope.OrderReset();
                $scope.ModeOrderRoom();
                $scope.GetListCustomerCheckIn();
                $("#CheckInSingle").modal('hide');
            } else {
                toastr.error(pl.data.Message);
            }
          
            CommonUtils.showWait(false);
        }, function (err) {
        });
    };



    //end
    

    // chon nhieu phong

    $scope.SelectedClick = function (parent) {
        var countSelected=0;
        $.each(parent.Rooms, function (index, item) {
            if(item.Selected) countSelected++;
        });
        parent.Quanlity = countSelected;
    };

    var GetRoomsByClassForChecinMutil = function () {
        var promiseGet = Roomservice.GetRoomclassForCheckin();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.RoomForCheckinMutil= pl.data.Data;
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });
    };
    $scope.EnterRoomClick = function (val) {
        if (val.Quanlity > val.RoomAvailable)
        {
            toastr.error("Không được nhập quá số phòng khả dụng !");
            val.Quanlity = 1;
            return;
        }
        $.each(val.Rooms, function (index, item) {
            item.Selected = false;
        });
        for (var i = 0; i < val.Quanlity; i++) {
            val.Rooms[i].Selected=true;
        }
      
    };
    $scope.ShowPopupForMutil = function () {
        GetRoomsByClassForChecinMutil();
        $('#id-date-mutil').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY h:mm A',
            timePicker: true,
            timePicker24Hour: true,
            timePickerIncrement: 5,
            startDate: moment(),
            minDate: moment(),
            timezone: 'Asia/Ho_Chi_Minh',
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
            var promisePost = Roomservice.GetRoomAvailable(start, end);
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


    
    $scope.AddOrderMutil = function () {
        $scope.Order.CompanyName = $("#com_value").val();
        if ($scope.Order.CompanyName == undefined)
        {
            toastr.error("Cần nhập tên đoàn !");
            return;
        }
        if ($scope.Order.Customers.length > 0) {
            $scope.Order.CustomerName = $scope.Order.Customers[$scope.CustomerIndexSelected].Name;
            $scope.Order.CustomerId = $scope.Order.Customers[$scope.CustomerIndexSelected].Id;
            $scope.Order.Customers[$scope.CustomerIndexSelected].IsPrimary = true;
        }
        
        $scope.Order.Company = {
            Id: $scope.Order.CompanyId,
            Address: $scope.Order.CompanyAdd,
            Mobile: $scope.Order.CompanyMobile,
            Name: $scope.Order.CompanyName
        };
       

        $scope.Order.RoomIds = [];

        $.each($scope.RoomAvailables, function (index, item) {

            if (item.Quanlity > 0) {

                $.each(item.Rooms, function (i, row) {
                    if (row.Selected) {
                        var row={RoomId:row.Id,ConfigPriceId:item.ConfigPriceSelected}
                        $scope.Order.RoomIds.push(row);
                    }

                });
            }
        });
        if ($scope.Order.RoomIds <= 0) {
            toastr.error("Cần chọn số phòng !");
            return;
        }
        var promisePost = Roomservice.AddOrderMutil($scope.Order);

        promisePost.then(function (result) {
            if (!result.data.IsError) {
                toastr.success("Thêm mới thành công !");
                $scope.GetRoomsByFloor();
                $scope.OrderReset();
                $scope.HotelConfig();
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
        var promiseGet = Roomservice.GetOrderForEdit(val);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Order = pl.data.Data;
                ListRoomClassForCheckOut();

           
            $scope.Order.CheckInDate = new Date(parseInt($scope.Order.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
            $scope.Order.CheckOutDate = new Date(parseInt($scope.Order.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
            InitWidget();
            $('#id-date').daterangepicker({
                'applyClass': 'btn-sm btn-success',
                'cancelClass': 'btn-sm btn-default',
                format: 'DD/MM/YYYY h:mm A',
                timePicker: true,
                timePicker24Hour: true,
                timezone: 'Asia/Ho_Chi_Minh',
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
            $('#id-date').data('daterangepicker').setStartDate(new Date($scope.Order.CheckInDate));
            $('#id-date').data('daterangepicker').setEndDate(new Date($scope.Order.CheckOutDate));


            $('#passportDate').daterangepicker({
                singleDatePicker: true,
                timezone: 'Asia/Ho_Chi_Minh',
                format: 'DD/MM/YYYY',
                showDropdowns:true

            }, function (start, end, label) {
                
             $scope.Customer.PassportCreatedDate = start;
         });
          
          
           // $("#CheckInSingle").modal('show');
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });
    };

    var ListRoomClassForCheckOut = function () {
        if (!$scope.Order) return;
        var promisePost = Roomservice.GetConfigPriceByRoom($scope.Order.RoomId);
        promisePost.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.ConfigPrices = pl.data.Data;
                var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.Id == $scope.Order.ConfigPriceId });
                $scope.ConfigSelected = theItem[0].Id;
            }
        });
    };
    $scope.viewOrderAttach = function (val) {

        if (!$scope.Widgets)
            $scope.InitWidget();

        CommonUtils.showWait(true);
        var data = {
            orderId: val,
            mode: undefined
        };

        var promiseGet = Roomservice.GetOrderForCheckOut(data);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Order = pl.data.Data;
                $("#Checkout").modal('show');
            }
            else {
                CommonUtils.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
        });


    };
    $scope.Checkout = function (val) {
        $scope.Order.isPay = true;
        if (!$scope.Widgets)
            $scope.InitWidget();
        $scope.CaculatorMode = val.CaculatorMode;// "ByHour";
        var hourCheckOut = new Date().getHours();
        if (hourCheckOut >= 0 && hourCheckOut <= 5)
            $scope.CaculatorMode = 2;
        if (!$scope.CaculatorMode)
            $scope.CaculatorMode = 3;
        CommonUtils.showWait(true);
        var data = {
            orderId: val.OrderId,
            mode: $scope.CaculatorMode,
            isByNight: val.isByNight
        };
        var promiseGet = Roomservice.GetOrderForCheckOut(data);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Order = pl.data.Data;
                $scope.CaculatorMode = $scope.Order.CaculatorMode;
               
                ListRoomClassForCheckOut();
                $scope.Invoice = $scope.Order;
                $scope.Invoice.InvoiceDetails = ReadyDataForPrint();
               
                $("#Checkout").modal('show');
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
        });

       
    };

    var ReadyDataForPrint = function () {
        result = [];
        var Descriptions = "Tiền phòng " + $scope.Order.RoomName;
        var notes="";
        var SubAmount=0;
        $.each($scope.Order.TimeUseds, function (i,item) {
            notes += +"\n" + item.Description;
            SubAmount += item.SumAmount;
        });
        
        var row={
            Descriptions: Descriptions,
            SubAmount:SubAmount,
            Notes: notes
        };
        result.push(row);
        if ($scope.Order.Services) {
            $.each($scope.Order.Services, function (i, item) {
                var row = {
                    Descriptions:  item.Name,
                    SubAmount: item.Quantity * item.Price,
                    Notes:"x"+item.Quantity
                };
                result.push(row);
            });
        }
        if ($scope.Order.OrderAttachs) {
            $.each($scope.Order.OrderAttachs, function (i, item) {
                var row = {
                    Descriptions:"Hóa đơn [" + item.OrderId + "] Phòng [" + item.RoomName + "]",
                    SubAmount: item.TotalAmount,
                    Notes: "x1"
                };
                result.push(row);
            });
        }
        return result;
    }



    $scope.ChangCalculatorMode = function () {
        var req = {
            data: {
                orderId: $scope.Order.Id,
                mode: $scope.CaculatorMode
            },
            url: "/CPanelAdmin/Order/ChangCalculatorMode"
           
        };
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.POST(req);
        promiseGet.then(function (pl) {
            CommonUtils.showWait(false);
            if (!pl.data.IsError) {
                $scope.Order = pl.data.Data;


            //GetTotalAmount();
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });

    };

    $scope.isBynightClickChange = function () {
        var data = {
            orderId: $scope.Order.Id,
            mode: $scope.CaculatorMode,
            isByNight: $scope.Order.isByNight
        };
        var promiseGet = Roomservice.GetOrderForCheckOut(data);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Order = pl.data.Data;
               
            }
            else {
                toastr.error(pl.data.Message);
            }
        },
            function (errorPl) {
            });

    };



  

    $scope.UpdateOrder = function () {
        CommonUtils.showWait(true);
        
        var promiseGet = Roomservice.UpdateOrder($scope.Order);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $("#Checkout").modal('hide');
                toastr.success("Cập nhật thành công !");
            }
            else {
                toastr.error(pl.data.Message);
            }
           
            CommonUtils.showWait(false);
        });
    };
   
    $scope.OrderReset = function () {
        $scope.Order = {};
        $scope.Customer = {};
        $scope.Order.Customers=[];
        $scope.Order.Services = [];
        $scope.Order.Id = 0;
        $("#cus_value").val(undefined);
        $("#pas_value").val(undefined);
        $("#id-date").val(undefined);
        $("#passportDate").val(undefined);
        
    };

    $scope.CheckOutOrder = function (val) {
        if (val) {
            $scope.Order.Cashed = 0;
            $scope.Order.OrderStatus = 1;
        } else {
            $scope.Order.OrderStatus = 7;
        }
           
        if ($scope.OrderAttachs)
            $scope.Order.OrderAttachment = $scope.OrderAttachs;
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.UpdateOrder($scope.Order);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success("Cập nhật thành công !");
               
                $("#Checkout").modal('hide');
                $scope.OrderReset();
                $scope.ModeOrderRoom();
                $scope.GetInvoices();
                $scope.GetListCustomerCheckIn();
            }
            else {
                toastr.error(pl.data.Message);

            }
            CommonUtils.showWait(false);
        },
        function (errorPl) {
            toastr.error("Đã có lỗi xẩy ra !");
        });
    };
    //---------------------- check out company ----------------------------------------------

    $scope.GetOrderByCompany = function () {
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetOrderByCompany();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.OrderCompany = pl.data.Data;
                CommonUtils.showWait(false);
                if ($scope.OrderCompany.length <= 0)
                    toastr.warning("Không có đoàn nào đang Checkin !");
                else
                    $("#CheckoutCompany").modal('show');
            } else { toastr.error(pl.data.Message); CommonUtils.showWait(false); };
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
    };

    $scope.DoBookingByCompanyPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetOrderBookingByCompany();
    };


    $scope.GetOrderBookingByCompany = function (val) {
        
        CommonUtils.showWait(true);
        $scope.Filter.OrderStatus = val;
        var promiseGet = Roomservice.GetOrderBookingByCompany($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                
                $scope.OrderBookingCompany = pl.data.Data.Data;
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                CommonUtils.showWait(false);
                
            } else { toastr.error(pl.data.Message); CommonUtils.showWait(false); };
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
    $scope.CompanyOrderchecked = function (val,child) {
        if (!val) return;
        if (companyId > 0 && companyId != val.CompanyId) {
            toastr.error("Chỉ được chọn 1 công ty/đoàn !");
            val.IsSelected = false;
            child.IsSelected = false;
            return;
        }
        var check=false;
        $.each(val.Orders, function (index, item) {
            if (item.IsSelected) check = true;
        });
        val.IsSelected = check;
    };

    $scope.CheckOutCompany = function () {
        var Selecteds = [];
        $.each($scope.OrderCompany, function (index, item) {
            if(item.IsSelected)
            {
                $.each(item.Orders, function (i, row) {
                    if (row.IsSelected) Selecteds.push(row.Id);
                });
            }
        });
      
        if(Selecteds.length<=0)
        {
            toastr.error("Cần chọn hóa đơn trước khi thanh toán !");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.CompanyDoCheckOut(Selecteds,"ByDay");
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Order = pl.data.Data.OrderKey;
                $scope.Order.CheckInDate = new Date(parseInt($scope.Order.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                $scope.Order.CheckOutDate = new Date(Date.now());
                $scope.OrderAttachs = pl.data.Data.OrderAttach;
                $scope.CaculatorMode = "ByDay";
               // GetTotalAmount();
                InitWidget();
                CommonUtils.showWait(false);
                $("#CheckoutCompany").modal('hide');
                $("#Checkout").modal('show');
            }
            else toastr.error(pl.data.Message);
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
    };
    
   
  
    $('#CheckInMutil').on('hide.bs.modal', function () {
       // $scope.Order = undefined;
        $scope.Customer = undefined;
       
    }); 
    $('#CheckoutCompany').on('hide.bs.modal', function () {
        companyId = 0;
    });
    $('#StaticRooms').on('shown.bs.modal', function () {
       

    });

    $('#StaticRooms').on('hide.bs.modal', function () {
       
    });

    $('#addCustomerToOrder').on('shown.bs.modal', function () {
        $('#passportDate').daterangepicker({
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
            timezone: 'Asia/Ho_Chi_Minh',
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
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.GetRoomsStaticByTime($scope.SearchRoomModel);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.RoomStatic = pl.data.Data;
             
                $("#PanelTime").modal('hide');


                var groups = new vis.DataSet();
                groups = $scope.RoomStatic.Rooms;
                var items = new vis.DataSet();
                
                items = $scope.RoomStatic.events;
                var container = document.getElementById('timeline');
                container.innerHTML = "";
                var options = {
                    // option groupOrder can be a property name or a sort function
                    // the sort function must compare two groups and return a value
                    //     > 0 when a > b
                    //     < 0 when a < b
                    //       0 when a == b
                    max: $scope.SearchRoomModel.ToDateView,
                    min: $scope.SearchRoomModel.FromDateView,
                    zoomable: false,
                    orientation: {axis: 'top'},
                    groupOrder: function (a, b) {
                        return a.value - b.value;
                    }
                   // editable: true
                };

                var timeline = new vis.Timeline(container, items, groups, options);
              
                //timeline.setOptions(options);
                //timeline.setGroups(groups);
                //timeline.setItems(items);



                $("#StaticRooms").modal('show');
                CommonUtils.showWait(false);
                //$('#calendar').fullCalendar({
                //    now: $scope.SearchRoomModel.FromDate,
                //    editable: false,
                //    lang: 'vi',
                //    aspectRatio: 1,
                //    scrollTime: '00:00',
                //    defaultView: 'timelineTenDay',
                //    views: {
                //        timelineTenDay: {
                //            type: 'timeline',
                //            duration: { days: $scope.days },
                //            slotDuration: '12:00'
                //        }

                //    },
                //    resourceAreaWidth: '100px',
                //    resourceLabelText: 'Phòng',
                //    resources: $scope.RoomStatic.Rooms,
                //    events: $scope.RoomStatic.events
                //});
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
        var promisePost = Roomservice.GetConfigPriceByRoom($scope.Order.RoomId);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {

                $scope.ConfigPrices = pl.data.Data;
                var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.IsDefault == true });
                $scope.ConfigSelected = theItem[0].Id;
                $scope.Order.Price = theItem[0].PriceByDay;

            }
        }, function (err) {
            console.log("Err" + err);
        });
        InitWidget();
        $('#id-date-booking').daterangepicker({
            'applyClass': 'btn-sm btn-success',
            'cancelClass': 'btn-sm btn-default',
            format: 'DD/MM/YYYY h:mm A',
            startDate:new Date(),
            timePicker: true,
            timePicker24Hour: true,
            timezone: 'Asia/Ho_Chi_Minh',
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


        $('#passportDate').daterangepicker({
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

        var theItem = jQuery.grep($scope.ConfigPrices, function (p) { return p.Id == $scope.ConfigSelected });
        $scope.Order.ConfigPriceId = theItem[0].Id;
        $scope.Order.Price = theItem[0].PriceByDay;


        $scope.Order.CustomerName = $scope.Order.Customers[$scope.CustomerIndexSelected].Name;
        $scope.Order.CustomerId = $scope.Order.Customers[$scope.CustomerIndexSelected].Id;
        $scope.Order.Customers[$scope.CustomerIndexSelected].IsPrimary = true;
        var promisePost = Roomservice.BookingOrder($scope.Order);
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
        FromDate: undefined,
        ToDate: undefined,
        Keyword: undefined

    };

    $('#id-date-filter-booking').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'Asia/Ho_Chi_Minh',
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Từ ',
            toLabel: 'Đến',
        }
    },

 function (start, end, label) {
     $scope.Filter.FromDate = start;
     $scope.Filter.ToDate = end;
 });
    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetBookingOrders();
    };

    $scope.GetBookingOrders = function () {
        var promisePost = Roomservice.GetBookingOrders($scope.Filter);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.OrderBookings = pl.data.Data.Data;
                if ($scope.OrderBookings.length > 0) {
                    $.each($scope.OrderBookings, function (index, item) {
                        item.CheckInDate = new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CheckOutDate = new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        item.CreatedDate = new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    });
                   
                    $("#OrderBookings").modal('show');
                } else
                    toastr.warning('Hiện không có phòng được đặt trước !');
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
            }
           
        }, function (err) {
            console.log("Err" + err);
        });
       
    };




    $scope.TranferBookingToCheckIn = function (val) {
        $scope.OrderIdChange = val;
        var promisePost = Roomservice.TranferBookingToCheckIn(val);
        promisePost.then(function (pl) {
            if (pl.data.IsError) {
                toastr.error(pl.data.Message);
                GetRoomsByClassForChecinMutil()
                $('#RoomAvailable').modal('show');
                $('#OrderBookings').modal('hide');
            }
            else
            {
                toastr.success('Nhận phòng thành công !');
                $scope.GetRoomsByFloor();
            }
            
        }, function (err) {
            toastr.error(err.statusText);
        });
       
    };
    $scope.RadioSelected = function (roomid,classroomid) {
        $scope.RoomIdChange = roomid;
        $scope.ClassRoomSelectedId = classroomid;

    };
    $scope.ShowPopupchangeRoom = function (val) {
        $scope.OrderIdChange = val;
        GetRoomsByClassForChecinMutil()
        $('#RoomAvailable').modal('show');
    };

    $scope.ChangeRoomInOrder = function (only) {
        
       
        var roomClassSelected = $.grep($scope.RoomForCheckinMutil, function (n, i) {
            
            return n.Id == $scope.ClassRoomSelectedId;
        });
        if ($scope.OrderIdChange == undefined || $scope.RoomIdChange == undefined) {
            toastr.error('Cần chọn phòng sử dụng !');
            return;
        }
        debugger
        if (!roomClassSelected[0].ConfigPriceSelected) {
            toastr.error('Chưa chọn cấu hình giá !');
            return;
        }
        var req = {
            data: {
                orderId: $scope.OrderIdChange,
                roomId: $scope.RoomIdChange,
                configPriceId: roomClassSelected[0].ConfigPriceSelected,
                isOnlyChangeRoom: only
            },
            url: "/CPanelAdmin/Room/ChangeRoomInOrder"
        };
        var promisePost = Roomservice.POST(req);
        promisePost.then(function (pl) {
            if (pl.data.IsError) {
                toastr.error(pl.data.Message);               
            }
            else {
                toastr.success('Đổi phòng thành công !');
                $scope.ModeOrderRoom();
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
        $('#passportDate').data('daterangepicker').setStartDate(val.PassportCreatedDate);
        $('#passportDate').data('daterangepicker').setEndDate(val.PassportCreatedDate);
        $scope.Customer = val;

    };

    $scope.GetCustomerPassportId = function () {
        if ($scope.Customer.PassportId == undefined ) {
            return;
        }
        var promisePost = Roomservice.GetCustomerPassportId($scope.Customer.PassportId);
        promisePost.then(function (pl) {
            $scope.CustomerResults = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };



    //=================================== Room and Floor ===========================
    $scope.GetRoomForEdit = function (val) {
        var promisePost = Roomservice.GetRoomForEdit(val);
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

        var promisePost = Roomservice.GetListFloor();
        promisePost.then(function (pl) {
            $scope.FloorList = pl.data.Data;           
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    var GetListRoomClass = function (val) {

        var promisePost = Roomservice.GetListRoomClass();
        promisePost.then(function (pl) {
            
            $scope.RoomClassList = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };
    $scope.EditRoom = function () {

        var promisePost = Roomservice.EditRoom($scope.Room);
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


    //=================================================================


    $scope.UpdateStatusInvocie = function () {
        CommonUtils.showWait(true);
        var promiseGet = Roomservice.UpdateStatusInvocie($scope.Invoice.Id, 7);
        promiseGet.then(function (pl) {
            $('#ViewInvoiceDetail').modal('hide');
            $scope.GetInvoices();
            CommonUtils.showWait(false);
        },
            function (errorPl) {
            });

    };

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
    