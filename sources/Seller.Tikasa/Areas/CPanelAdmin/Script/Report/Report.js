app.service("Reportservice", function ($http) {

    this.GetSystemConfig = function (id) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/SystemConfig/GetConfig",
            data: JSON.stringify({ roomid: id })
        });
        return request;
    }
    this.GetStaticReport = function (fromDate, toDate) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/GetStaticReport",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate ,ToDate:toDate})
        });
        return request;
    };
    this.GetRoomPopularReport = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Report/GetRoomPopularReport",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };

    this.GetReceiptReport = function (fromDate, toDate) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/GetReceiptReport",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate, ToDate: toDate })
        });
        return request;
    };
    this.ReportByRoom = function (fromDate, toDate,ByRoomType) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/ReportByRoom",

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate, ToDate: toDate, ByRoomType: ByRoomType })
        });
        return request;
    };

    this.ReportByService = function (fromDate, toDate) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/ReportByService",

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate, ToDate: toDate})
        });
        return request;
    };

    this.ReportGoodsReceipt = function (fromDate, toDate) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/ReportGoodsReceipt",

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate, ToDate: toDate })
        });
        return request;
    };
    this.ReportRoomHistory = function (fromDate, toDate, roomid) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/ReportRoomHistory",

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ FromDate: fromDate, ToDate: toDate, roomId: roomid })
        });
        return request;
    };

    this.GetListRooms = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Report/GetListRooms",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.ReportRevenue = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Report/ReportRevenue",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    this.InitFilterModel = function () {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/Invoice/InitFilterModel",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

});

app.controller("ReportController", function ($scope, Reportservice) {


    $scope.Filter = {
        Page: {
            currentPage: 1,
            pageSize: 20,
            total: 0
        },
        InvoiceType: 1,
        FromDate: undefined,
        ToDate: undefined,
        Keyword: undefined,
        PaymentMethod: undefined,
        InvoiceStatus: undefined

    };

    $scope.ReportRevenue = function () {
        app.showWait(true);
        var promiseGet = Reportservice.ReportRevenue($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {debugger
                $scope.Invoices = pl.data.Data.Data.Data;
                $scope.TotalAmount = pl.data.Data.Data.TotalAmount;
                $.each($scope.Invoices, function (index, item) {

                    item.CreatedDate = item.CreatedDate ? new Date(parseInt(item.CreatedDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    item.CheckInDate = item.CheckInDate ? new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                    item.CheckOutDate = item.CheckOutDate ? new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1'))) : null;
                });
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                app.showWait(false);
            }
        },
        function (errorPl) {
        });
    };


    $('#id-date-revenue').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
        startDate: moment(),
        endDate: '-30d',
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

    $scope.FromDateView = start.format('DD/MM/YYYY');
    $scope.ToDateView = end.format('DD/MM/YYYY');

   
});




    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.ReportRevenue();
    };
    $scope.InitFilterModel = function () {
        var promiseGet = Reportservice.InitFilterModel();
        promiseGet.then(function (pl) {
            $scope.InvoiceStatus = $.parseJSON(pl.data.InvoiceStatus);
            $scope.PaymentMethods = $.parseJSON(pl.data.PaymentMethod);
        },
        function (errorPl) {
        });

    };


    var today = new Date();
    var fromDate = new Date(today).setDate(today.getDate() - 15);
    $scope.FromDate = fromDate;
    $scope.FromDateView = fromDate;
    $scope.ToDate = today;
    $scope.ToDateView = today;

    $('#id-date').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
        startDate: moment(),
        endDate: '-30d',
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Từ ',
            toLabel: 'Đến',
        }
    },

 function (start, end, label) {
     



     $scope.FromDate = start;
     $scope.ToDate = end;

     $scope.FromDateView = start.format('DD/MM/YYYY');
     $scope.ToDateView = end.format('DD/MM/YYYY');

     $scope.GetStaticReport();
     $("#PanelTime").modal("hide");
 });


    $('#id-date-ReportByRoom').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
        startDate: moment(),
       
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Từ ',
            toLabel: 'Đến',
        }
    },function (start, end, label) {
    $scope.FromDate = start;
    $scope.ToDate = end;

    $scope.FromDateView = start.format('DD/MM/YYYY');
    $scope.ToDateView = end.format('DD/MM/YYYY');
});
    $scope.ReportBys = [{ Name: "Xem theo phòng ", Value: false }, { Name: "Xem theo loại phòng", Value: true }];
    $scope.ByRoomType = false;
    $scope.ReportByRoom = function () {
        app.showWait(true);
        var promiseGet = Reportservice.ReportByRoom($scope.FromDate, $scope.ToDate, $scope.ByRoomType);
        promiseGet.then(function (pl) {
            $scope.ReportRoom = pl.data.Data;
            var list = [];
            $.each($scope.ReportRoom, function (index, item) {
                if (item.TotalAmount > 0) {
                    list.push({
                        name:$scope.ByRoomType? item.RoomTypeName:'Phòng '+ item.RoomName,
                        y: item.TotalAmount
                    });
                }
            });

            $('#roomPerformant').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Thống kê hiệu suất sử dụng phòng/loại phòng từ ' + $scope.FromDateView + ' - ' + $scope.ToDateView
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Tỷ lệ',
                    colorByPoint: true,
                    data: list
                }]
            });
            app.showWait(false);
        },
        function (errorPl) {
        });
    };




    $('#id-date-ReportByService').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
        startDate: moment(),
        endDate: '-30d',
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Từ ',
            toLabel: 'Đến',
        }
    }, function (start, end, label) {
        $scope.FromDate = start;
        $scope.ToDate = end;

        $scope.FromDateView = start.format('DD/MM/YYYY');
        $scope.ToDateView = end.format('DD/MM/YYYY');
    });
    $scope.TotalAmount=0;
    $scope.ReportByService = function () {
        app.showWait(true);
        var promiseGet = Reportservice.ReportByService($scope.FromDate, $scope.ToDate);
        promiseGet.then(function (pl) {
            $scope.ReportRoom = pl.data.Data;
            var list = [];
            
            $.each($scope.ReportRoom, function (index, item) {
                if (item.TotalAmount > 0) {
                    list.push({
                        name:  item.RoomName,
                        y: item.TotalAmount
                    });

                    $scope.TotalAmount += item.TotalAmount;
                }
            });

            $('#roomPerformant').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Thống kê hiệu suất sử dụng dịch vụ từ ' + $scope.FromDateView + ' - ' + $scope.ToDateView
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Tỷ lệ',
                    colorByPoint: true,
                    data: list
                }]
            });
            app.showWait(false);
        },
        function (errorPl) {
        });
    };





    $('#id-date-ReportGoodsReceipt').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
        startDate: moment(),
        endDate: '+30d',
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Từ ',
            toLabel: 'Đến',
        }
    }, function (start, end, label) {
        $scope.FromDate = start;
        $scope.ToDate = end;

        $scope.FromDateView = start.format('DD/MM/YYYY');
        $scope.ToDateView = end.format('DD/MM/YYYY');
    });
   
    $scope.ReportGoodsReceipt = function () {
        app.showWait(true);
        var promiseGet = Reportservice.ReportGoodsReceipt($scope.FromDate, $scope.ToDate);
        promiseGet.then(function (pl) {
            $scope.GoodsReceipt = pl.data.Data;
            var list = [];

            $.each($scope.GoodsReceipt, function (index, item) {
                if (item.TotalAmount > 0) {
                    list.push({
                        name: item.ServiceName,
                        y: item.TotalAmount
                    });

                    $scope.TotalAmount += item.TotalAmount;
                }
            });

            $('#roomPerformant').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Thống kê nhập kho từ ' + $scope.FromDateView + ' - ' + $scope.ToDateView
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Tỷ lệ',
                    colorByPoint: true,
                    data: list
                }]
            });
            app.showWait(false);
        },
        function (errorPl) {
        });
    };



    $scope.InitRooms = function () {
        app.showWait(true);
        var promiseGet = Reportservice.GetListRooms();
        promiseGet.then(function (pl) {
            $scope.Rooms = pl.data.Data;
            app.showWait(false);
        },
        function (errorPl) {
        });
    };



    $('#id-date-ReportRoomHistory').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        format: 'DD/MM/YYYY',
        timezone: 'local',
        startDate: moment(),
        endDate: '-7d',
        locale: {
            applyLabel: 'Đồng ý',
            cancelLabel: 'Hủy',
            fromLabel: 'Từ ',
            toLabel: 'Đến',
        }
    }, function (start, end, label) {
        $scope.FromDate = start;
        $scope.ToDate = end;

        $scope.FromDateView = start.format('DD/MM/YYYY');
        $scope.ToDateView = end.format('DD/MM/YYYY');
    });

    $scope.RoomSeleted=0;
    $scope.ReportRoomHistory = function () {
        if (!$scope.RoomSeleted)
        {
            toastr.error("Chọn phòng cần xem !");
            return;
        }
        app.showWait(true);
        var promiseGet = Reportservice.ReportRoomHistory($scope.FromDate, $scope.ToDate, $scope.RoomSeleted);
        promiseGet.then(function (pl) {
            $scope.RoomHistories = pl.data.Data.Histories;
            $.each($scope.RoomHistories, function (index, item) {
                item.CheckInDate = new Date(parseInt(item.CheckInDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                item.CheckOutDate = new Date(parseInt(item.CheckOutDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                
            });
            app.showWait(false);
        },
        function (errorPl) {
        });
    };



    function getRandomColor() {
        var letters = '0123456789ABCDEF'.split('');
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }
    $scope.GetRoomPopularReport = function () {
        app.showWait(true);
        var promiseGet = Reportservice.GetRoomPopularReport();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError)
            {
                var label = [];
                var data = [];
                $.each(pl.data.Data, function (i, item) {
                    label.push(item.RoomName);
                    data.push(item.Count);
                });

                var ctx = document.getElementById("myChart").getContext("2d");

                var lineChartData = {
                    labels: label,
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.2)",
                            strokeColor: "rgba(220,220,220,1)",
                            pointColor: "rgba(220,220,220,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(220,220,220,1)",
                            data: data
                        }
                    ]

                };

                var myLineChart = new Chart(ctx).Bar(lineChartData, {
                    responsive: true
                });

            }
            app.showWait(false);
        },
        function (errorPl) {
        });
    };

    $scope.GetStaticReport = function () {
        app.showWait(true);
        var promiseGet = Reportservice.GetStaticReport($scope.FromDate, $scope.ToDate);
        promiseGet.then(function (pl) {
            $scope.StaticReport = pl.data.Data;
            app.showWait(false);
        },
        function (errorPl) {
        });
    };


    $scope.GetReceiptReport = function () {
        app.showWait(true);
        var promiseGet = Reportservice.GetReceiptReport();
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                var label = [];
                var data = [];
                $.each(pl.data.Data, function (i, item) {
                    label.push(item.CreatedDateView);
                    
                    data.push(item.Amount);                   
                });

                var ctx = document.getElementById("ReceiptChart").getContext("2d");

                var Linedata = {
                    labels: label,
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.2)",
                            strokeColor: "rgba(220,220,220,1)",
                            pointColor: "rgba(220,220,220,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(220,220,220,1)",
                            data: data
                        }
                    ]
                };
                var myLineChart = new Chart(ctx).Line(Linedata, {
                    scaleLabel: function (label) { return label.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' đ'; },
                    //tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') %>",
                    tooltipTemplate: function (label) {
                        return label.label + ': ' + label.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' đ';
                    },
                    responsive: true
                });
            }
            app.showWait(false);
        },
        function (errorPl) {
        });
    };

    $scope.Printreport = function (Divid) {debugger
        var divToPrint = document.getElementById(Divid);

        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        frame1.css({ "position": "absolute", "top": "-1000000px" });
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        //Create a new HTML document.
        frameDoc.document.write('<html><head><title>Báo cáo - thống kê </title>');
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

});