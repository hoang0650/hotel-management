
app.service("HistoryService", function ($http) {

    this.GetOrderByCompany = function () {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Room/GetOrderByCompany",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetHistories = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/History/GetHistories",
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

    this.InsertOrUpdateInvoice = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Invoice/InsertOrUpdateInvoice",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.InitCategoryInvoice = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Invoice/InitCategoryInvoice",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});


app.controller("HistoryController", function ($scope, HistoryService) {

    $scope.Filter = {
        Page: {
            currentPage: 1,
            pageSize: 20,
            total: 0
        },
        InvoiceType: 1,
        FromDate: undefined,
        ToDate: undefined,
        Keyword:undefined
       
    };
    $('#id-date').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
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
        $scope.GetHistories();
    };
    var InitCategoryInvoice = function () {
        var promiseGet = InvoiceService.InitCategoryInvoice();
        promiseGet.then(function (pl) {
            $scope.CategoryInvoice=$.parseJSON(pl.data);
        },
        function (errorPl) {
        });

    };
    $scope.GetHistories = function (val) {
      

        if (val)
            $scope.Filter.InvoiceType=val
        app.showWait(true);
        var promiseGet = HistoryService.GetHistories($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Histories = pl.data.Data.Data;
                $.each($scope.Histories, function (index, item) {
                    item.CreatedDate =item.CreatedDate? new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1'))):null;
                   
                });
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
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

    $scope.UpdateStatusInvocie = function () {
        app.showWait(true);
        var promiseGet = InvoiceService.UpdateStatusInvocie($scope.Invoice.Id,2);
        promiseGet.then(function (pl) {
            $('#ViewInvoiceDetail').modal('hide');
            $scope.GetInvoices();
            app.showWait(false);
        },
        function (errorPl) {
        });

    };
    
    $scope.PrintInvoice=function()
    {
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
        debugger
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
            InvoiceType:val
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
        app.showWait(true);
        var promiseGet = InvoiceService.InsertOrUpdateInvoice($scope.Invoice);
        promiseGet.then(function (pl) {
            $('#ViewInvoiceDetail').modal('hide');
            $scope.GetInvoices($scope.Invoice.InvoiceType);
            app.showWait(false);
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
   
});
