
app.service("CustomerService", function ($http) {

    
    this.GetListCustomer = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/GetListCustomer",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetInvoicesByCustomer = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/GetInvoicesByCustomer",
            dataType: 'json',
            data: JSON.stringify({ OrderIds: model }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetCustomerById = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/GetCustomerById",
            dataType: 'json',
            data: JSON.stringify({ CustomerId: model }),
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
    this.InsertOrUpdateCustomer = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/InsertOrUpdateCustomer",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.GetListCustomerCheckIn = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Customer/GetListCustomerCheckIn",
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
});


app.controller("CustomerController", function ($scope, CustomerService) {

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
    $scope.Customer = {
        Id: undefined,
        Address: undefined,
        Mobile: undefined,
        Name: undefined,
        PassportId: undefined,
        Email: undefined,
        Notes: undefined,
        PassportCreatedDate: undefined
    };

    $scope.remoteUrlRequestcustomerFn = function (str) {
        return { CustomerType: 1, Keyword: str };
    };

    $scope.SearchByPassport = function (str) {
        return { passportId: str };
    };

    $scope.CustomerSelected = function (selected) {
        if (selected) {
            $scope.Customer = {
                Id: selected.originalObject.Id,
                Address: selected.originalObject.Address,
                Mobile: selected.originalObject.Mobile,
                Name: selected.originalObject.Name,
                PassportId: selected.originalObject.PassportId,
                Email: selected.originalObject.Email,
                Notes: selected.originalObject.Notes,
                PassportCreatedDate:  new Date(parseInt(selected.originalObject.PassportCreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1')))
            };
           $("#passport_value").val( $scope.Customer.PassportId );
            $("#customerName_value").val($scope.Customer.Name );
            $("#passportDate").data("daterangepicker").setEndDate($scope.Customer.PassportCreatedDate);
            
        } 
    };

    $scope.GetListCustomer = function (val) {
        if (val)
            $scope.Filter.InvoiceType = val
        CommonUtils.showWait(true);
        var promiseGet = CustomerService.GetListCustomer($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Customers = pl.data.Data.Data;
                $.each($scope.Customers, function (index, item) {
                    item.CreatedDate = item.CreatedDate ? new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                  
                });
                
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                CommonUtils.showWait(false);
            }
        },
        function (errorPl) {
        });
    };
    
    var InitCountries = function () {
        CommonUtils.showWait(true);
        var promiseGet = CustomerService.GetListCountries();
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

    $scope.ShowPopupAddCustomer = function (val) {
        var date = new Date();
        //InitCountries();
        $('#passportDate').daterangepicker({
            format: 'DD/MM/YYYY',            
            singleDatePicker: true,
            startDate: date,
            endDate: date.setDate(6),
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
            $scope.Customer.BirthDate = start;
        });


        if(val)
        {
            CommonUtils.showWait(true);
            var promiseGet = CustomerService.GetCustomerById(val);
            promiseGet.then(function (pl) {
                if (!pl.data.IsError) {
                  
                        $scope.Customer = pl.data.Data;
                        $("#passport_value").val( $scope.Customer.PassportId);
                        $("#customerName_value").val( $scope.Customer.Name);
                        $('#passportDate').data('daterangepicker').setStartDate($scope.Customer.BirthDateView);
                        $('#passportDate').data('daterangepicker').setEndDate($scope.Customer.BirthDateView);
                  
                   
                }
                else
                    toastr.error(pl.data.Message);
              
            },
            function (errorPl) {
            });
        }
        CommonUtils.showWait(false);
        $("#AddOrrUpdateCustomer").modal("show");
    };

    $scope.GetCustomerPassportId = function () {
        if ($scope.Customer.PassportId == undefined) {
            return;
        }
        var promisePost = CustomerService.GetCustomerPassportId($scope.Customer.PassportId);
        promisePost.then(function (pl) {
            $scope.CustomerResults = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };


    $scope.InsertOrUpdateCustomer = function () {
        $scope.Customer.PassportId = $("#passport_value").val();
        $scope.Customer.Name = $("#customerName_value").val();
        $scope.Customer.CustomerType = 1;
        //if ($scope.Customer.PassportId != undefined && $scope.Customer.PassportCreatedDate == undefined) {
        //    toastr.error("Bạn chưa nhập ngày cấp CMND/Passport !");
        //    return;
        //}
        if ($scope.Customer.Name == null) {
            toastr.error("Bạn chưa nhập tên khách hàng !");
            return;
        }
        CommonUtils.showWait(true);
        var promisePost = CustomerService.InsertOrUpdateCustomer($scope.Customer);
        promisePost.then(function (response) {
            if (!response.data.IsError)
            {
                toastr.success("Cập nhật thành công !");
                $scope.Customer = {};
                $("#AddOrrUpdateCustomer").modal("hide");
                $scope.GetListCustomer();
            }
        }, function (err) {
            toastr.error(err.statusText);
        });
        CommonUtils.showWait(false);
    };



    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetListCustomer();
    };

    $scope.GetInvoicesByCustomer = function (val) {
        $scope.CustomerSelected = val;
        CommonUtils.showWait(true);
        var promiseGet = CustomerService.GetInvoicesByCustomer(val.OrderIds);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
               
                $scope.Invoices = pl.data.Data;
                
                $("#InvoiceByCustomer").modal("show");
              
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



    $scope.GetListCustomerCheckIn = function (val) {
      
        CommonUtils.showWait(true);
        var promiseGet = CustomerService.GetListCustomerCheckIn($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.CustomerCheckIns = pl.data.Data.Data;
                $.each($scope.CustomerCheckIns, function (index, item) {
                    item.CheckInDate = item.CheckInDate ? new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    item.CheckOutDate = item.CheckOutDate ? new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;

                });
                
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                
            }
            else
                toastr.error(pl.data.Message);
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };
});
