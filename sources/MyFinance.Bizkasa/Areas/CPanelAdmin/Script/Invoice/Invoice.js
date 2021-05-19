app.service("InvoiceService", function ($http) {

    this.GetOrderByCompany = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderByCompany",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.SummaryInShift = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Invoice/SummaryInShift",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

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
    this.AddOrUpdateShift = function (model) {
        var request = $http({
            method: "POST",
            url:"/CPanelAdmin/Invoice/AddOrUpdateShift",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: model
        });
        return request;
    };

    this.InitUsers = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/User/GetUserByHotel",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
        });
        return request;
    };

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
    this.GetInvoiceByPayment = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/GetInvoiceByPayment",
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

    this.InsertOrUpdateInvoice = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/InsertOrUpdateInvoice",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.InitCategoryInvoice = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Invoice/InitCategoryInvoice",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.InitCategoryInvoicePayment = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Invoice/InitCategoryInvoicePayment",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.InitFilterModel = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Invoice/InitFilterModel",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };


    this.GetHotelInfo = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Hotel/GetHotelInfo",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.DeleteInvoice = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/DeleteInvoice",
            dataType: 'json',
            data: JSON.stringify({ InvoiceIds: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.TransferToManager = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/TransferToManager",
            dataType: 'json',
            data: data,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.InitAdmins = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/User/GetAdminByHotel",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
});

app.controller("InvoiceController", function ($scope, InvoiceService) {
    $scope.InitAdmins = function () {
        var promiseGet = InvoiceService.InitAdmins();
        promiseGet.then(function (pl) {
            $scope.Admins = pl.data.Data;
        },
            function (errorPl) {
            });

    };
    $scope.TransferToManager = function () {
        if (!$scope.Shift.ManagerId) {
            toastr.error("Chưa chọn tài khoản quản lý !");
            return;
        }

        if (!$scope.Shift.ManagerPassword) {
            toastr.error("Cần nhập mật khẩu !");
            return;
        }

        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.TransferToManager($scope.Shift);
        promiseGet.then(function (pl) {
            CommonUtils.showWait(false);
            if (!pl.data.IsError) {
                if (pl.data.Data) {
                    $('#transferManager').modal('hide');
                    toastr.success("Cập nhật thành công");
                    $scope.SummaryInShift();
                }
            }
            else {
                toastr.error(pl.data.Message);
            }

        },
            function (errorPl) {
            });

    };
    $scope.TransferPopup = function (val) {
        $scope.Shift.MaxTransferAmount = val;
        $scope.InitAdmins();
        $('#transferManager').modal('show');
    };
    $scope.GetHotelInfo = function () {
        var promiseGet = InvoiceService.GetHotelInfo();
        promiseGet.then(function (pl) {
            $scope.Hotel = pl.data.Data;
        },
        function (errorPl) {
        });

    };
    $scope.Login = function () {
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.Login($scope.User);
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
    $scope.InitUsers = function () {
        var promiseGet = InvoiceService.InitUsers();
        promiseGet.then(function (pl) {
            $scope.Users = pl.data.Data;
        },
        function (errorPl) {
        });

    };

    $scope.SummaryInShift = function () {
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.SummaryInShift();
        promiseGet.then(function (pl) {
            $scope.SummaryShift = pl.data.Data;
            $scope.Shift = {
                DeliveryManagerAmount:0,
                OpenAmount: 0,
                ReceiptAmount: 0,
                DeliveryAmount: 0,
                CloseAmount:0
            };
            if ($scope.SummaryShift.ShiftPrev)
                $scope.Shift.OpenAmount = ($scope.SummaryShift.ShiftPrev.OpenAmount + $scope.SummaryShift.TotalAmount) - $scope.SummaryShift.ManagerAmount;
            else
                $scope.Shift.OpenAmount = $scope.SummaryShift.TotalAmount;
            CommonUtils.showWait(false);
        
          
        },
        function (errorPl) {
        });

    };
    $scope.ShiftPopup = function () {
        $scope.InitUsers();
        $('#addShift').modal('show');

    };

    $scope.ComputedOpenAmount = function () {
        if (!$scope.SummaryShift.ShiftPrev) {
            $scope.SummaryShift.ShiftPrev.OpenAmount=0
        }
        if ($scope.SummaryShift.ShiftPrev)
            $scope.Shift.OpenAmount = ($scope.SummaryShift.ShiftPrev.OpenAmount + $scope.SummaryShift.TotalAmount) - $scope.Shift.DeliveryManagerAmount;
        else
            $scope.Shift.OpenAmount = $scope.SummaryShift.TotalAmount;
    };

    $scope.AddOrUpdateShift = function () {
        if (!$scope.Shift.UserId) {
            toastr.error("Chưa chọn tài khoản giao ca !");
            return;
        }
        var user = $.grep($scope.Users, function (n, i) {
            return n.Id == $scope.Shift.UserId;
        });
        if (user.length > 0) {
            $scope.Shift.Email = user[0].Email;
        }
        if (!$scope.Shift.Password) {
            toastr.error("Cần nhập mật khẩu !");
            return;
        }
        $scope.Shift.ReceiptAmount = $scope.SummaryShift.InvoiceAmount + $scope.SummaryShift.ReceiptAmount;
        $scope.Shift.DeliveryAmount = $scope.SummaryShift.DeliveryAmount;
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.AddOrUpdateShift($scope.Shift);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                if (pl.data.Data) {
                

                    $scope.User = {
                        UserName: $scope.Shift.Email,
                        Password: $scope.Shift.Password
                       
                    };
                    $scope.Login();

                }
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);
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
       
        Keyword: undefined,
        PaymentMethod: undefined,
        InvoiceStatus: undefined

    };

    $('#id-date').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
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
        },
       
    },

  function (start, end, label) {
      $scope.Filter.FromDate = start;
      $scope.Filter.ToDate = end;
        });
    $('#id-date').on('apply.daterangepicker', function (ev, picker) {
        $scope.Filter.FromDate = picker.startDate;
        $scope.Filter.ToDate = picker.endDate;

    });
    
  

    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetInvoices();
    };
    $scope.PagingPayment = function (page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetInvoiceByPayment();
    };
    var InitCategoryInvoice = function () {
        var promiseGet = InvoiceService.InitCategoryInvoicePayment();
        promiseGet.then(function (pl) {
            $scope.CategoryInvoice = $.parseJSON(pl.data);
           
        },
        function (errorPl) {
        });

    };

    $scope.InitFilterModel = function () {
        var promiseGet = InvoiceService.InitFilterModel();
        promiseGet.then(function (pl) {
            $scope.InvoiceStatus = $.parseJSON(pl.data.InvoiceStatus);
            $scope.PaymentMethods = $.parseJSON(pl.data.PaymentMethod);
        },
        function (errorPl) {
        });

    };
    var getAllSelected = function () {
        if (!$scope.Invoices)
            return;
        var selectedItems = $scope.Invoices.filter(function (item) {
            return item.IsSelected;
        });

        return selectedItems.length === $scope.Invoices.length;
    }

    var setAllSelected = function (value) {
        $.each($scope.Invoices, function (index, item) {
            item.IsSelected = value;
        });
    }

    $scope.allSelected = function (value) {
        if (value !== undefined) {
            return setAllSelected(value);
        } else {
            return getAllSelected();
        }
    }

    var selectedItems = [];
    $scope.reallyDeleteMore = function () {
        selectedItems = [];
        $.each($scope.Invoices, function (index, item) {
            if (item.IsSelected)
                selectedItems.push(item.Id);
        });
        if (!selectedItems) {
            toastr.error("Chưa chọn hóa đơn cần xóa");
            return;
        }
        if (selectedItems.length <= 0) {
            toastr.error("Chưa chọn hóa đơn cần xóa");
            return;
        }
        DeleteInvoice(selectedItems);
    };

    var DeleteInvoice = function (val) {
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.DeleteInvoice(val);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success('Xóa thành công !');
                $scope.GetInvoiceByPayment();
            }
            else {
                toastr.error(pl.data.Message);
            }
            CommonUtils.showWait(false);

        },
        function (errorPl) {
        });
    };
    $scope.SumAmount = {
        Amount: undefined,
        Paid: undefined,
        Debit: undefined
    }
    $scope.GetInvoices = function (val) {
        if (!$("#id-date").val()) {
            $scope.Filter.FromDate = undefined;
            $scope.Filter.ToDate = undefined;
        }
        $scope.SumAmount = {
            Amount: 0,
            Paid: 0,
            Debit:0
        };
        if (val)
            $scope.Filter.InvoiceType = val
        
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.GetInvoices($scope.Filter);
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


    $scope.GetInvoiceByPayment = function (val) {
        if (!$scope.CategoryInvoice)
            InitCategoryInvoice();
        $scope.SumAmount = 0;
       
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.GetInvoiceByPayment($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.Invoices = pl.data.Data.Data;
                $.each($scope.Invoices, function (index, item) {
                    $scope.SumAmount += item.SubAmount;
                    
                });
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                CommonUtils.showWait(false);
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

    $scope.UpdateStatusInvocie = function () {
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.UpdateStatusInvocie($scope.Invoice.Id, 7);
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
    }

    $scope.ShowPopup = function (val) {
        $scope.Invoice = {
            InvoiceDetails: [],
            TotalAmount: 0,
            InvoiceType: val
        };
        $scope.InvoiceDetail = {
            Descriptions: undefined,
            Quantity: undefined,
            Price: undefined,
            Notes: undefined,
            SubAmount: undefined
        }
        InitCategoryInvoice();
        $('#addInvoice').modal('show');
    };
    $scope.AddInvoice = function () {
        if (!$scope.Invoice.CompanyName) {
            toastr.error("Cần nhập tên công ty/Đơn vị !");
            return;
        }

        if ($scope.Invoice.InvoiceDetails.length <= 0) {
            toastr.error("Chưa nhập chi tiết hóa đơn !");
            return;
        }
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.InsertOrUpdateInvoice($scope.Invoice);
        promiseGet.then(function (pl) {

            $('#addInvoice').modal('hide');
            $scope.GetInvoices($scope.Invoice.InvoiceType);
            CommonUtils.showWait(false);
        },
        function (errorPl) {
        });
    };


    $scope.AddInvoiceDetail = function (val) {
        if ($scope.InvoiceDetail.Quantity == undefined || $scope.InvoiceDetail.Quantity == "") {
            toastr.error("Bạn chưa nhập số lượng !");
            return;
        }
        if ($scope.InvoiceDetail.Price == undefined || $scope.InvoiceDetail.Price == "") {
            toastr.error("Bạn chưa nhập giá nhập !");
            return;
        }
        $scope.Invoice.InvoiceDetails.push($scope.InvoiceDetail);
        $scope.InvoiceDetail.SubAmount = $scope.InvoiceDetail.Price * $scope.InvoiceDetail.Quantity;
        $scope.Invoice.TotalAmount += $scope.InvoiceDetail.SubAmount;
        $scope.InvoiceDetail = undefined;
        if (val)
            $('#addInvoiceDetail').modal('hide');
    };

    $scope.AddInvoiceShort = function () {
        $scope.Invoice = {
            InvoiceDetails: [],
            TotalAmount: 0,
            InvoiceType: 2,//phieu chi

        };

        if (!$scope.InvoiceDetail) {
            toastr.error("Dữ liệu nhập không hợp lệ !");
            return;
        }

        if (!$scope.InvoiceDetail.SubAmount) {
            toastr.error("Bạn chưa nhập tổng tiền !");
            return;
        }
        if (!$scope.InvoiceDetail.CategoryInvoice) {
            toastr.error("Bạn chưa chọn mục chi !");
            return;
        }

        if (!$scope.InvoiceDetail.Descriptions) {
            toastr.error("Bạn chưa nhập diễn giải !");
            return;
        }

        $scope.InvoiceDetail.Price = $scope.InvoiceDetail.SubAmount / $scope.InvoiceDetail.Quantity;
        $scope.InvoiceDetail.Quantity = 1;// mac dinh so luong nhap
        $scope.Invoice.InvoiceDetails.push($scope.InvoiceDetail);
        $scope.Invoice.TotalAmount = $scope.InvoiceDetail.SubAmount;
        CommonUtils.showWait(true);
        var promiseGet = InvoiceService.InsertOrUpdateInvoice($scope.Invoice);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                toastr.success("Nhập kho thành công !");
                $scope.GetInvoiceByPayment();
                $scope.InvoiceDetail = {};
            } else {
                toastr.error(pl.data.Message);
            }
        },
        function (errorPl) {
        });

        CommonUtils.showWait(false);

    };

});

