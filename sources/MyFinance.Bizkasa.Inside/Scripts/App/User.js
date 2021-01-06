
app.service("Userservice", function ($http) {  
   
    this.GetUsers = function (model) {
        var request = $http({
            method: "post",
            url: "/User/GetUsers",
            dataType: 'json',
            data: model,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

 

});


app.controller("UserController", function ($scope, Userservice) {

    
    $scope.Filter = {
        Page: {
            currentPage: 1,
            pageSize: 20,
            total: 0
        },
        InvoiceType: 1,
        FromDate: undefined,
        ToDate: undefined,
        Keyword: undefined

    };
   

    $scope.DoCtrlPagingAct = function (text, page, pageSize, total) {
        $scope.Filter.Page.currentPage = page;
        $scope.GetUsers();
    };
    $scope.GetUsers = function (val) {

        if (val)
            $scope.Filter.InvoiceType = val
        app.showWait(true);
        var promiseGet = Userservice.GetUsers($scope.Filter);
        promiseGet.then(function (pl) {
            if (!pl.data.HasError) {
                $scope.Users = pl.data.Data.Data;
                $scope.Filter.Page.total = pl.data.Data.TotalRecord;
                app.showWait(false);
            }
        },
        function (errorPl) {
        });
   
};
});