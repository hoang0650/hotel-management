
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
    $scope.GetListCustomer = function (val) {
        if (val)
            $scope.Filter.InvoiceType = val
        app.showWait(true);
        var promiseGet = CustomerService.GetListCustomer($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Customers = pl.data.Data.Data;
                $.each($scope.Customers, function (index, item) {
                    item.CreatedDate = item.CreatedDate ? new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                  
                });
                
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                app.showWait(false);
            }
        },
        function (errorPl) {
        });
    };
    
    $scope.ShowPopupAddCustomer = function (val) {

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


        if(val)
        {
            app.showWait(true);
            var promiseGet = CustomerService.GetCustomerById(val);
            promiseGet.then(function (pl) {
                if (!pl.data.HasError) {
                    $scope.Customer = pl.data.Data;
                    
                    $scope.Customer.PassportCreatedDate =$scope.Customer.PassportCreatedDate? new Date(parseInt($scope.Customer.PassportCreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1'))):null;
                    $('#id-date-picker-3').data('daterangepicker').setStartDate(new Date($scope.Customer.PassportCreatedDate));
                    $('#id-date-picker-3').data('daterangepicker').setEndDate(new Date($scope.Customer.PassportCreatedDate));
                    app.showWait(false);
                }
                else
                    toastr.error(pl.data.Data.Message);
            },
            function (errorPl) {
            });
        }
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
        if ($scope.Customer.PassportId != undefined && $scope.Customer.PassportCreatedDate == undefined) {
            toastr.error("Bạn chưa nhập ngày cấp CMND/Passport !");
            return;
        }
        if ($scope.Customer.Name == null) {
            toastr.error("Bạn chưa nhập tên khách hàng !");
            return;
        }

        var promisePost = CustomerService.InsertOrUpdateCustomer($scope.Customer);
        promisePost.then(function (response) {
            if (!response.data.IsError)
            {
                toastr.success("Cập nhật thành công !");
            }
        }, function (err) {
            toastr.error(err.statusText);
        });
    };



    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetListCustomer();
    };

    $scope.GetInvoicesByCustomer = function (val) {
        $scope.CustomerSelected = val;
        app.showWait(true);
        var promiseGet = CustomerService.GetInvoicesByCustomer(val.OrderIds);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Invoices = pl.data.Data;
                $.each($scope.Invoices, function (index, item) {
                    item.CreatedDate = item.CreatedDate ? new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    item.CheckInDate = item.CheckInDate ? new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    item.CheckOutDate = item.CheckOutDate ? new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                });
                $("#InvoiceByCustomer").modal("show");
                app.showWait(false);
            }
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
      
        app.showWait(true);
        var promiseGet = CustomerService.GetListCustomerCheckIn($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.CustomerCheckIns = pl.data.Data.Data;
                $.each($scope.CustomerCheckIns, function (index, item) {
                    item.CheckInDate = item.CheckInDate ? new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    item.CheckOutDate = item.CheckOutDate ? new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;

                });
                debugger
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                app.showWait(false);
            }
        },
        function (errorPl) {
        });
    };
});
