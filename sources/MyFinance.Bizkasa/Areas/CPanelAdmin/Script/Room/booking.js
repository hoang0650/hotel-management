app.service("RoomBookingservice", function ($http) {

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
});


app.controller("RoomBookingController", function ($scope, RoomBookingservice) {
    var dp;
    $scope.$watch("roomType", function () {
        loadResources();
    });
    $scope.schedulerConfig = {
        scale: "Manual",
        timeline: getTimeline(),
        timeHeaders: [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: "Day", format: "d" }],
        eventDeleteHandling: "Update",
        allowEventOverlap: false,
        cellWidthSpec: "Auto",
        eventHeight: 50,
        rowHeaderColumns: [
            { title: "Room", width: 80 }
            //{ title: "Capacity", width: 80 },
            //{ title: "Status", width: 80 }
        ],
        onBeforeResHeaderRender: function (args) {
            var beds = function (count) {
                return count + " bed" + (count > 1 ? "s" : "");
            };

            args.resource.columns[0].html = beds(args.resource.capacity);
          //  args.resource.columns[1].html = args.resource.status;
            //switch (args.resource.status) {
            //    case "Dirty":
            //        args.resource.cssClass = "status_dirty";
            //        break;
            //    case "Cleanup":
            //        args.resource.cssClass = "status_cleanup";
            //        break;
            //}
        },
        onEventMoved: function (args) {
            //$http.post("backend_move.php", {
            //    id: args.e.id(),
            //    newStart: args.newStart.toString(),
            //    newEnd: args.newEnd.toString(),
            //    newResource: args.newResource
            //}).then(function (response) {
            //    dp.message(response.data.message);
            //});
        },
        onEventResized: function (args) {
            //$http.post("backend_resize.php", {
            //    id: args.e.id(),
            //    newStart: args.newStart.toString(),
            //    newEnd: args.newEnd.toString()
            //}).then(function () {
            //    dp.message("Resized.");
            //});
        },
        onEventDeleted: function (args) {
            //$http.post("backend_delete.php", {
            //    id: args.e.id()
            //}).then(function () {
            //    dp.message("Deleted.");
            //});
        },
        onTimeRangeSelected: function (args) {
            //var modal = new DayPilot.Modal();
            //modal.closed = function () {
            //    dp.clearSelection();

            //    // reload all events
            //    var data = this.result;
            //    if (data && data.result === "OK") {
            //        loadEvents();
            //    }
            //};
            //modal.showUrl("new.php?start=" + args.start + "&end=" + args.end + "&resource=" + args.resource);
        },
        onEventClick: function (args) {
            //var modal = new DayPilot.Modal();
            //modal.closed = function () {
            //    // reload all events
            //    var data = this.result;
            //    if (data && data.result === "OK") {
            //        loadEvents();
            //    }
            //};
            //modal.showUrl("edit.php?id=" + args.e.id());
        },
        onBeforeEventRender: function (args) {
            var start = new DayPilot.Date(args.data.start);
            var end = new DayPilot.Date(args.data.end);

            var now = new DayPilot.Date();
            var today = new DayPilot.Date().getDatePart();
            var status = "";
            
            // customize the reservation bar color and tooltip depending on status
            switch (args.e.status) {
                case "New":
                    var in2days = today.addDays(1);

                    if (start < in2days) {
                        args.data.barColor = 'red';
                        status = 'Hết hạn (not confirmed in time)';
                    }
                    else {
                        args.data.barColor = 'orange';
                        status = 'Đã đặt';
                    }
                    break;
                case "Confirmed":
                    var arrivalDeadline = today.addHours(18);

                    if (start < today || (start === today && now > arrivalDeadline)) { // must arrive before 6 pm
                        args.data.barColor = "#f41616";  // red
                        status = 'Late arrival';
                    }
                    else {
                        args.data.barColor = "green";
                        status = "Confirmed";
                    }
                    break;
                case 'Arrived': // arrived
                    var checkoutDeadline = today.addHours(10);

                    if (end < today || (end === today && now > checkoutDeadline)) { // must checkout before 10 am
                        args.data.barColor = "#f41616";  // red
                        status = "Late checkout";
                    }
                    else {
                        args.data.barColor = "#1691f4";  // blue
                        status = "Đang ở";
                    }
                    break;
                case 'CheckedOut': // checked out
                    args.data.barColor = "gray";
                    status = "Checked out";
                    break;
                default:
                    status = "Unexpected state";
                    break;
            }

            // customize the reservation HTML: text, start and end dates
            args.data.html = args.data.text + " (" + start.toString("M/d/yyyy") + " - " + end.toString("M/d/yyyy") + ")" + "<br /><span style='color:gray'>" + status + "</span>";

            // reservation tooltip that appears on hover - displays the status text
            args.e.toolTip = status;

            // add a bar highlighting how much has been paid already (using an "active area")
            var paid = args.e.paid;
            var paidColor = "#aaaaaa";
            //args.data.areas = [
            //    { bottom: 10, right: 4, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>Paid: " + paid + "%</div>", v: "Visible" },
            //    { left: 4, bottom: 8, right: 4, height: 2, html: "<div style='background-color:" + paidColor + "; height: 100%; width:" + paid + "%'></div>" }
            //];

        }
    };

    function getTimeline(date) {
        var date = date || DayPilot.Date.today();
        var start = new DayPilot.Date(date).firstDayOfMonth();
        var days = start.daysInMonth();

        var timeline = [];

        var checkin = 12;
        var checkout = 12;

        for (var i = 0; i < days; i++) {
            var day = start.addDays(i);
            timeline.push({ start: day.addHours(checkin), end: day.addDays(1).addHours(checkout) });
        }

        return timeline;
    }


    $scope.navigatorConfig = {
        selectMode: "month",
        showMonths: 3,
        skipMonths: 3,
        onTimeRangeSelected: function (args) {
            if ($scope.scheduler.visibleStart().getDatePart() <= args.day && args.day < $scope.scheduler.visibleEnd()) {
                $scope.scheduler.scrollTo(args.day, "fast");  // just scroll
            }
            else {
                loadEvents(args.day);  // reload and scroll
            }
        }
    };
  
    function loadEvents(day) {
        var from = $scope.scheduler.visibleStart();
        var to = $scope.scheduler.visibleEnd();
        if (day) {
            from = new DayPilot.Date(day).firstDayOfMonth();
            to = from.addMonths(1);
        }

        var params = {
            start: from.toString(),
            end: to.toString()
        };
        $scope.SearchRoomModel = {
            FromDate: from,
            ToDate: to,
            FromTime: null,
            ToTime: null,
            SearchBy: 'ByDay'
        };
        CommonUtils.showWait(true);
        var promiseGet = RoomBookingservice.GetRoomsStaticByTime($scope.SearchRoomModel);
        promiseGet.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.RoomStatic = pl.data.Data;
                if (day) {
                    $scope.schedulerConfig.timeline = getTimeline(day);
                    $scope.schedulerConfig.scrollTo = day;
                    $scope.schedulerConfig.scrollToAnimated = "fast";
                    $scope.schedulerConfig.scrollToPosition = "left";
                }
                $scope.events = $scope.RoomStatic.events;//[{ "id": "3", "text": "Mr. Garc\u00eda", "start": "2016-4-02T12:00:00", "end": "2016-4-07T12:00:00", "resource": "3", "bubbleHtml": "Reservation details: <br\/>Mr. Garc\u00eda", "status": "Arrived", "paid": "50" }];

                CommonUtils.showWait(false);
            }
            else
                toastr.error(pl.data.Message);
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
           
    }
  
    function loadResources() {
        var params = {
            capacity: $scope.roomType
        };
        
        var date = date || DayPilot.Date.today();
        var from = new DayPilot.Date(date).firstDayOfMonth();
        var to = from.addMonths(1);
        
        $scope.SearchRoomModel = {
            FromDate: from,
            ToDate: to,
            FromTime: null,
            ToTime: null,
            SearchBy: 'ByDay'
        };
        CommonUtils.showWait(true);
        var promiseGet = RoomBookingservice.GetRoomsStaticByTime($scope.SearchRoomModel);
        promiseGet.then(function (pl) {
            
            if (!pl.data.IsError) {
                $scope.RoomStatic = pl.data.Data;
                $scope.schedulerConfig.resources = $scope.RoomStatic.Rooms;
                $scope.events = $scope.RoomStatic.events;
                $scope.schedulerConfig.visible = true;
               
                CommonUtils.showWait(false);
            }
            else
                toastr.error(pl.data.Message);
        },
        function (errorPl) {
            toastr.error(errorPl.statusText);
        });
       
    }

});