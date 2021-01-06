app.service("Categoryservice", function ($http) {

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

    this.getByType = function (val) {
        var request = $http({
            method: "post",
            url: "/Category/getCateByType",
            data: JSON.stringify({ Type: val})
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

    this.AddCategory = function (userhotel) {
        var request = $http({
            method: "post",
            url: "/Category/AddCategory",
            data: userhotel
        });
        return request;
    };
});