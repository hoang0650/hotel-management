app.controller("CategoryController", function ($scope, Categoryservice) {

    //GetAllRecords();
    //To Get All Records  
   
    $scope.InitData = function () {
        var val = getUrlParameter('t');
        var promiseGet = Categoryservice.getByType(val);
        promiseGet.then(function (pl) {
            $scope.Categories = pl.data;
        },
        function (errorPl) {
        });
    };
    $scope.GetHotelBy = function() {
        var promiseGet = Hotelservice.getById();
        promiseGet.then(function (pl) {
            $scope.Hotel = pl.data;
        },
        function (errorPl) {
        });
    };

    $scope.pop = function () {
        $("#AddCate").modal('show');       
    }
    $scope.CheckoutDayList = [];   
    $scope.CheckoutDayclick = function () {
        var Hours=[];
        for (var i = $scope.CheckoutDayList.length + 1; i < 25; i++) {
            var hour = {Name:i+' h',Value:i};
            Hours.push(hour);
        }
        var data = { Hours: Hours, Key:0 , Value: 45 };
        if ($scope.CheckoutDayList.length <= 0)
            data.Key = Hours[0];
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == $scope.CheckoutDayList[$scope.CheckoutDayList.length - 1].Key.Value + 1)
                    data.Key = item;
            });
        }
        $scope.CheckoutDayList.push(data);
       
    };
    $scope.CheckoutDayRemoveclick = function (data) {
        
        $scope.CheckoutDayList.splice(data, 1);
        for (var i = data; i < $scope.CheckoutDayList.length; i++) {
            var Hours = [];
            for (var j = i+1; j < 25; j++) {
                var hour = { Name: j + ' h', Value: j };
                Hours.push(hour);
            }
            var row = $scope.CheckoutDayList[i];
            row.Hours = Hours;
            $.each(Hours, function (i, item) {
                if (item.Value ==row.Key.Value)
                    row.Key = item;
            });
            $scope.CheckoutDayList[i] = row;

        }

    };
    $scope.CheckoutDaySelectedclick = function (val) {
       
        if (val == 0)
        {
            for (var i = val + 1; i < $scope.CheckoutDayList.length; i++) {
                var row = $scope.CheckoutDayList[i];
                if ($scope.CheckoutDayList[i].Key.Value < $scope.CheckoutDayList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckoutDayList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckoutDayList[i] = row;
                }

            }
         
        }
        else {
            for (var i = val - 1; i >= 0; i--) {
                var row = $scope.CheckoutDayList[i];
                if ($scope.CheckoutDayList[i].Key.Value > $scope.CheckoutDayList[i + 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckoutDayList[i + 1].Key.Value - 1)
                            row.Key = item;
                    });
                    $scope.CheckoutDayList[i] = row;
                }

            }

            for (var i = val + 1; i < $scope.CheckoutDayList.length; i++) {
                var row = $scope.CheckoutDayList[i];
                if ($scope.CheckoutDayList[i].Key.Value < $scope.CheckoutDayList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckoutDayList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckoutDayList[i] = row;
                }
            }

        }

    };



    $scope.CheckoutNightList = [];
    $scope.CheckoutNightclick = function () {
        var Hours = [];
        for (var i = $scope.CheckoutNightList.length + 1; i < 25; i++) {
            var hour = { Name: i + ' h', Value: i };
            Hours.push(hour);
        }
        var data = { Hours: Hours, Key: 0, Value: 45 };
        if ($scope.CheckoutNightList.length <= 0)
            data.Key = Hours[0];
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == $scope.CheckoutNightList[$scope.CheckoutNightList.length - 1].Key.Value + 1)
                    data.Key = item;
            });
        }
        $scope.CheckoutNightList.push(data);

    };
    $scope.CheckoutNightRemoveclick = function (data) {

        $scope.CheckoutNightList.splice(data, 1);
        for (var i = data; i < $scope.CheckoutNightList.length; i++) {
            var Hours = [];
            for (var j = i + 1; j < 25; j++) {
                var hour = { Name: j + ' h', Value: j };
                Hours.push(hour);
            }
            var row = $scope.CheckoutNightList[i];
            row.Hours = Hours;
            $.each(Hours, function (i, item) {
                if (item.Value == row.Key.Value)
                    row.Key = item;
            });
            $scope.CheckoutNightList[i] = row;

        }

    };
    $scope.CheckoutNightSelectedclick = function (val) {

        if (val == 0) {
            for (var i = val + 1; i < $scope.CheckoutNightList.length; i++) {
                var row = $scope.CheckoutNightList[i];
                if ($scope.CheckoutNightList[i].Key.Value < $scope.CheckoutNightList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckoutNightList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckoutNightList[i] = row;
                }

            }

        }
        else {
            for (var i = val - 1; i >= 0; i--) {
                var row = $scope.CheckoutNightList[i];
                if ($scope.CheckoutNightList[i].Key.Value > $scope.CheckoutNightList[i + 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckoutNightList[i + 1].Key.Value - 1)
                            row.Key = item;
                    });
                    $scope.CheckoutNightList[i] = row;
                }

            }

            for (var i = val + 1; i < $scope.CheckoutNightList.length; i++) {
                var row = $scope.CheckoutNightList[i];
                if ($scope.CheckoutNightList[i].Key.Value < $scope.CheckoutNightList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckoutNightList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckoutNightList[i] = row;
                }
            }

        }

    };



    $scope.CheckinDayList = [];
    $scope.CheckinDayclick = function () {
        var Hours = [];
        for (var i = $scope.CheckinDayList.length + 1; i < 25; i++) {
            var hour = { Name: i + ' h', Value: i };
            Hours.push(hour);
        }
        var data = { Hours: Hours, Key: 0, Value: 45 };
        if ($scope.CheckinDayList.length <= 0)
            data.Key = Hours[0];
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == $scope.CheckinDayList[$scope.CheckinDayList.length - 1].Key.Value + 1)
                    data.Key = item;
            });
        }
        $scope.CheckinDayList.push(data);

    };
    $scope.CheckinDayRemoveclick = function (data) {

        $scope.CheckinDayList.splice(data, 1);
        for (var i = data; i < $scope.CheckinDayList.length; i++) {
            var Hours = [];
            for (var j = i + 1; j < 25; j++) {
                var hour = { Name: j + ' h', Value: j };
                Hours.push(hour);
            }
            var row = $scope.CheckinDayList[i];
            row.Hours = Hours;
            $.each(Hours, function (i, item) {
                if (item.Value == row.Key.Value)
                    row.Key = item;
            });
            $scope.CheckinDayList[i] = row;

        }

    };
    $scope.CheckinDaySelectedclick = function (val) {

        if (val == 0) {
            for (var i = val + 1; i < CheckinDayList.CheckinDayList.length; i++) {
                var row = $scope.CheckinDayList[i];
                if ($scope.CheckinDayList[i].Key.Value < $scope.CheckinDayList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckinDayList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckinDayList[i] = row;
                }

            }

        }
        else {
            for (var i = val - 1; i >= 0; i--) {
                var row = $scope.CheckinDayList[i];
                if ($scope.CheckinDayList[i].Key.Value > $scope.CheckinDayList[i + 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckinDayList[i + 1].Key.Value - 1)
                            row.Key = item;
                    });
                    $scope.CheckinDayList[i] = row;
                }

            }

            for (var i = val + 1; i < $scope.CheckinDayList.length; i++) {
                var row = $scope.CheckinDayList[i];
                if ($scope.CheckinDayList[i].Key.Value < $scope.CheckinDayList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckinDayList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckinDayList[i] = row;
                }
            }

        }

    };



    $scope.CheckinNightList = [];
    $scope.CheckinNightclick = function () {
        var Hours = [];
        for (var i = $scope.CheckinNightList.length + 1; i < 25; i++) {
            var hour = { Name: i + ' h', Value: i };
            Hours.push(hour);
        }
        var data = { Hours: Hours, Key: 0, Value: 45 };
        if ($scope.CheckinNightList.length <= 0)
            data.Key = Hours[0];
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == $scope.CheckinNightList[$scope.CheckinNightList.length - 1].Key.Value + 1)
                    data.Key = item;
            });
        }
        $scope.CheckinNightList.push(data);

    };
    $scope.CheckinNightRemoveclick = function (data) {

        $scope.CheckinNightList.splice(data, 1);
        for (var i = data; i < $scope.CheckinNightList.length; i++) {
            var Hours = [];
            for (var j = i + 1; j < 25; j++) {
                var hour = { Name: j + ' h', Value: j };
                Hours.push(hour);
            }
            var row = $scope.CheckinNightList[i];
            row.Hours = Hours;
            $.each(Hours, function (i, item) {
                if (item.Value == row.Key.Value)
                    row.Key = item;
            });
            $scope.CheckinNightList[i] = row;

        }

    };
    $scope.CheckinNightSelectedclick = function (val) {

        if (val == 0) {
            for (var i = val + 1; i < $scope.CheckinNightList.length; i++) {
                var row = $scope.CheckinNightList[i];
                if ($scope.CheckinNightList[i].Key.Value < $scope.CheckinNightList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckinNightList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckinNightList[i] = row;
                }

            }

        }
        else {
            for (var i = val - 1; i >= 0; i--) {
                var row = $scope.CheckinNightList[i];
                if ($scope.CheckinNightList[i].Key.Value > $scope.CheckinNightList[i + 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckinNightList[i + 1].Key.Value - 1)
                            row.Key = item;
                    });
                    $scope.CheckinNightList[i] = row;
                }

            }

            for (var i = val + 1; i < $scope.CheckinNightList.length; i++) {
                var row = $scope.CheckinNightList[i];
                if ($scope.CheckinNightList[i].Key.Value < $scope.CheckinNightList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.CheckinNightList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.CheckinNightList[i] = row;
                }
            }

        }

    };




    $scope.PriceByDayList = [];
    $scope.PriceByDayclick = function () {
        var Hours = [];
        for (var i = $scope.PriceByDayList.length + 1; i < 25; i++) {
            var hour = { Name: i + ' h', Value: i };
            Hours.push(hour);
        }
        var data = { Hours: Hours, Key: 0, Value: 45 };
        if ($scope.PriceByDayList.length <= 0)
            data.Key = Hours[0];
        else {
            $.each(Hours, function (i, item) {
                if (item.Value == $scope.PriceByDayList[$scope.PriceByDayList.length - 1].Key.Value + 1)
                    data.Key = item;
            });
        }
        $scope.PriceByDayList.push(data);

    };
    $scope.PriceByDayRemoveclick = function (data) {

        $scope.PriceByDayList.splice(data, 1);
        for (var i = data; i < $scope.PriceByDayList.length; i++) {
            var Hours = [];
            for (var j = i + 1; j < 25; j++) {
                var hour = { Name: j + ' h', Value: j };
                Hours.push(hour);
            }
            var row = $scope.PriceByDayList[i];
            row.Hours = Hours;
            $.each(Hours, function (i, item) {
                if (item.Value == row.Key.Value)
                    row.Key = item;
            });
            $scope.PriceByDayList[i] = row;

        }

    };
    $scope.PriceByDaySelectedclick = function (val) {

        if (val == 0) {
            for (var i = val + 1; i < $scope.PriceByDayList.length; i++) {
                var row = $scope.PriceByDayList[i];
                if ($scope.PriceByDayList[i].Key.Value < $scope.PriceByDayList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.PriceByDayList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.PriceByDayList[i] = row;
                }

            }

        }
        else {
            for (var i = val - 1; i >= 0; i--) {
                var row = $scope.PriceByDayList[i];
                if ($scope.PriceByDayList[i].Key.Value > $scope.PriceByDayList[i + 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.PriceByDayList[i + 1].Key.Value - 1)
                            row.Key = item;
                    });
                    $scope.PriceByDayList[i] = row;
                }

            }

            for (var i = val + 1; i < $scope.PriceByDayList.length; i++) {
                var row = $scope.PriceByDayList[i];
                if ($scope.PriceByDayList[i].Key.Value < $scope.PriceByDayList[i - 1].Key.Value) {
                    $.each(row.Hours, function (index, item) {
                        if (item.Value == $scope.PriceByDayList[i - 1].Key.Value + 1)
                            row.Key = item;
                    });
                    $scope.PriceByDayList[i] = row;
                }
            }

        }

    };
   

    $scope.SaveUserHotel = function () {
        var data = {
            userId: $scope.UserSelected,
            HotelId: $scope.HotelId
        }
        var promisePost = Userservice.MapUserHotel($scope.HotelId, $scope.UserSelected[0]);
        promisePost.then(function (pl) {
            
            toastr.success("Thêm mới thành công !");
        }, function (err) {
        });
    }

    


    $scope.AddCategory = function () {
        var val = getUrlParameter('t');
        var hotelModel = { Name: $scope.Name, Description: $scope.Description, CategoryType: val };
        if (hotelModel.Name == null || hotelModel.Name == undefined)
        {
            toastr.error("dữ liệu rông !"); return;
        }
        var promisePost = Categoryservice.AddCategory(hotelModel);
        promisePost.then(function (pl) {
            $scope.Categories = pl.data;
            toastr.success("Thêm mới thành công !");
            $("#AddCate").modal('hide');
        }, function (err) {
            console.log("Err" + err);
        });
    };

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
});