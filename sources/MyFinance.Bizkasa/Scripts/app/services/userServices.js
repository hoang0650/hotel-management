app.service("Userservice", function ($http) {
    this.getAllUser = function () {
        var request = $http({
            method: "post",
            url: "/User/GetallBy"
        });

        return request;
    };


    this.getUserByHotel = function () {
        var request = $http({
            method: "post",
            url: "/User/GetUserByHotel"
        });

        return request;
    };

    this.AddUser = function (userhotel) {
        var request = $http({
            method: "post",
            url: "/User/AddUser",
            data: userhotel
        });
        return request;
    };


    this.MapUserHotel = function (hotelid, userid) {
        var request = $http({
            method: "post",
            url: "/User/MapUserHotel",
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify({HotelId : hotelid, UserId : userid})
        });
        return request;
    };


    this.Login = function (userhotel) {
        var request = $http({
            method: "post",
            url: "/Home/Login",
            contentType: "application/json; charset=UTF-8",
            data: userhotel
        });
        return request;
    };

});