


app.controller("HomeController", function ($scope, Hotelservice, Userservice) {

    //GetAllRecords();
    //To Get All Records  
    $scope.InitData = function GetAllRecords() {
        var promiseGet = Hotelservice.getAll();
        promiseGet.then(function (pl) {
            $scope.Hotels = pl.data;
        },
        function (errorPl) {
            $log.error('Some Error in Getting Records.', errorPl);
        });
    };
    $scope.GetHotelBy = function() {
        var promiseGet = Hotelservice.getById();
        promiseGet.then(function (pl) {
            
            $scope.Hotel = pl.data;
        },
        function (errorPl) {
            $log.error('Some Error in Getting Records.', errorPl);
        });
    };

    $scope.pop = function showpopup(val) {
        $scope.HotelId = val;
        var promiseget = Userservice.getAllUser();
        promiseget.then(function (pl) {
            $scope.Users = pl.data;
        },
              function (errorpl) {
                  $log.error('some error in getting records.', errorpl);
              });
        $("#userhotel").modal('show');
       
    }

    $scope.UserSelected = [];
    $scope.Selected = function () {
        // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
        for (var i = 0; i < $scope.Users.length; i++) {
            if ($scope.Users[i].isChecked) {
                $scope.UserSelected.push($scope.Users[i].Id);
                //$scope.model.allItemsSelected = false;
                return;
            }
        }

        // ... otherwise check the "allItemsSelected" checkbox
       // $scope.model.allItemsSelected = true;
    }

    $scope.SaveUserHotel = function () {
        var data = {
            userId: $scope.UserSelected,
            HotelId: $scope.HotelId
        }
        var promisePost = Userservice.MapUserHotel($scope.HotelId, $scope.UserSelected[0]);
        promisePost.then(function (pl) {
            
            toastr.success("Thêm mới thành công !");
        }, function (err) {
            
            console.log("Err" + err);
        });
    }

    


    $scope.AddHotel = function () {
        
        var hotelModel = { Name: $scope.Name, Phone: $scope.Phone };
        if (hotelModel.Name == null || hotelModel.Name == undefined)
        {
            toastr.error("dữ liệu rông !"); return;
        }
        var promisePost = Hotelservice.AddHotel(hotelModel);
        promisePost.then(function (pl) {
            toastr.success("Thêm mới thành công !");
        }, function (err) {
            console.log("Err" + err);
        });
    };
});