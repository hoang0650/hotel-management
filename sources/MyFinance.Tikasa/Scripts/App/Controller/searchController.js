"use strict";
define(['appModule'], function (app) {

      app.register.controller('SearchController',
        ['$scope', '$rootScope', function ($scope, $rootScope) {
        $scope.GetRoomsByFloor = function () {
            roomService.GetRoomsByFloor(function (response) {
                if (!response.HasError) {
                    $scope.Floors = response.Data;
                }
            }, function (response) {
                //alertsService.RenderErrorMessage(response.ReturnMessage);
            });
        };





       
    }]);



});