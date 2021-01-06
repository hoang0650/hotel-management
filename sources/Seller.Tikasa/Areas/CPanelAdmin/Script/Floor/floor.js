app.service("Floorservice", function ($http) {

   

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

    this.InsertOrUpdateFloor = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Floor/InsertOrUpdateFloor",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetFloorBy = function (val) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Floor/GetFloorBy",
            dataType: 'json',
            data: JSON.stringify({ floorId: val }),
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.InsertRoom = function (model) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/Floor/InsertRoom",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});


app.controller("FloorController", function ($scope, Floorservice) {

    $scope.Room = {
        Names:undefined,
        FloorId: undefined,
        RoomClassId:undefined
    };
    $scope.InsertRoom = function () {
        if ($scope.Room.Names == undefined) {
            toastr.error("Cần nhập tên phòng");
            return;
        }
        var rooms = [];
        var roomArr = $scope.Room.Names.split(';');
        for (var i = 0; i < roomArr.length; i++) {
            if (roomArr[i] != "" || roomArr[i]!=undefined)
            rooms.push({ RoomClassId: $scope.Room.RoomClassId, FloorId: $scope.Room.FloorId, RoomName: roomArr[i] });
        }

        var promisePost = Floorservice.InsertRoom(rooms);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.GetListFloor();
                toastr.success("Cập nhật thành công !");
                $("#InsertRoom").modal("hide");
            }
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    $scope.InsertOrUpdateFloor = function () {

        var promisePost = Floorservice.InsertOrUpdateFloor($scope.Floor);
        promisePost.then(function (pl) {
            if (!pl.data.IsError) {
                $scope.GetListFloor();
                toastr.success("Cập nhật thành công !");
                $("#InsertOrUpdateFloor").modal("hide");
            }
        }, function (err) {
            toastr.error(err.statusText);
        });
    };
    $scope.GetFloorBy = function (val) {
        var promisePost = Floorservice.GetFloorBy(val);
        promisePost.then(function (pl) {
            $scope.Floor = pl.data.Data;
            $("#InsertOrUpdateFloor").modal("show");
        }, function (err) {
            toastr.error(err.statusText);
        });
    };
    $scope.GetListFloor = function () {
        var promisePost = Floorservice.GetListFloor();
        promisePost.then(function (pl) {
            $scope.FloorList = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    $scope.GetListRoomClass = function () {

        var promisePost = Floorservice.GetListRoomClass();
        promisePost.then(function (pl) {
            $scope.RoomClassList = pl.data.Data;
        }, function (err) {
            toastr.error(err.statusText);
        });
    };

    $scope.ShowPopup = function () {
        $scope.Floor = undefined;
        $scope.GetListRoomClass();
        $("#InsertOrUpdateFloor").modal("show");
    };
    $scope.ShowPopupInsertRoom = function () {
        $scope.GetListRoomClass();
        $("#InsertRoom").modal("show");
    };
});