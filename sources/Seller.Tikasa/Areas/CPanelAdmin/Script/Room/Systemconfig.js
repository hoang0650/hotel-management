app.service("SystemConfigservice", function ($http) {

    this.GetSystemConfig = function (id) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/SystemConfig/GetConfig"
        });
        return request;
    }
    this.AddConfig = function (data) {
        var request = $http({
            method: "post",
            url: "/CPanelAdmin/SystemConfig/AddConfig",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: data
        });
        return request;
    };
    this.GetRoomStatusEnum = function (id) {
        var request = $http({
            method: "get",
            url: "/CPanelAdmin/SystemConfig/GetRoomStatusEnum"
        });
        return request;
    }
});

app.controller("SystemConfigController", function ($scope, SystemConfigservice) {
    
    $scope.BindColor = function (data) {
        data.Color = $('#' + data.Key).val();
    };
   
    $scope.GetRoomStatusEnum = function () {

        var promiseGet = SystemConfigservice.GetRoomStatusEnum();
        promiseGet.then(function (result) {
            var temp=JSON.parse(result.data);
            var data=[];
            $.each(temp,function(index,item){
                var row={
                    Key:item.Key,
                    Value:item.Value,
                    Color:undefined
                };
                data.push(row);
            });
            $scope.RoomStatus = data;
           
        },
        function (errorPl) {
        });
    };

    $scope.InitData = function () {
        InitHour();

        var promiseGet = SystemConfigservice.GetSystemConfig();
        promiseGet.then(function (result) {
            if (!result.data.IsError) {
                $scope.Configs = result.data.Data;
                if ($scope.Configs.RoomStatusColor == "" || $scope.Configs.RoomStatusColor == undefined) {
                    $scope.GetRoomStatusEnum();
                }
                else
                    ConvertToRoomStatus($scope.Configs.RoomStatusColor);
            }
        },
        function (errorPl) {
        });
    };
    var ConvertToRoomStatus=function(val){
        var data=val.split(';');
        if(data.length>0)
        {
            var array=[];
            for (var i = 0; i < data.length; i++) {
                var item=data[i].split('_');
                var row={
                    Key:item[0],
                    Value:item[2],
                    Color:item[1]
                };
                array.push(row);
            }
            $scope.RoomStatus = array;
        }
    };
    $scope.Hours = [];
    $scope.Configs = {
        TimeCheckOut: 0,
        StartOverNight: 0,
        EndOverNight: 0,
        TimeRound: 0,
        OverCustomer:0
    }
    var InitHour = function () {
        for (var i = 1; i < 25; i++) {
            var row = { Key: i, Name: i + ' h' };
            $scope.Hours.push(row);
        }
    };
    $scope.TimeRounds = [{ Name: "15 Phút ", Value: 15 }, { Name: "20 Phút", Value: 20 }, { Name: "30 phút", Value: 30 }]

    $scope.AddConfig = function () {
        $scope.Configs.RoomStatusColor="";
        $.each($scope.RoomStatus, function (index, item) {
            if(item.Color==undefined)
            {
                toastr.error("Cần chọn màu cho tất cả các tình trạng phòng");
                return;
            }
            else {
                if ($scope.Configs.RoomStatusColor == "")
                    $scope.Configs.RoomStatusColor = item.Key + '_' + item.Color+'_'+item.Value;
                else
                    $scope.Configs.RoomStatusColor =$scope.Configs.RoomStatusColor +';'+ item.Key + '_' + item.Color+'_'+item.Value;
            }

        });

        var promiseGet = SystemConfigservice.AddConfig($scope.Configs);
        promiseGet.then(function (result) {
            if (!result.data.IsError)
            {
                $scope.Configs = result.data.Data;
                toastr.success("Cập nhật thành công !");
            }
                
        },
        function (errorPl) {
        });
    };

   
});
app.directive('myMainDirective', function() {
  return function(scope, element, attrs) {
      angular.element(element).colorpicker();
    //scope.$on('LastElem', function(event){
    //  $(element).children().css('border','5px solid blue');
    //});
  };
});