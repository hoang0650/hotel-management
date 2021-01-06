app.service("Hotelservice", function ($http) {

    this.getAll = function () {
        var request = $http({
            method: "post",
            url: "/Hotel/GetallBy"
        });
        return request;
    }

    this.getById = function () {
        var request = $http({
            method: "post",
            url: "/Hotel/GetById"
        });
        return request;
    }
    this.SaveUserHotel = function (userhotel) {
        var request = $http({
            method: "post",
            url: "/pk_admin/Hotel/SaveUserHotel",
            data: userhotel
        });
        return request;
    };

    this.AddHotel = function (userhotel) {
        var request = $http({
            method: "post",
            url: "/Hotel/AddHotel",
            data: userhotel
        });
        return request;
    };
});