angular.module("app", ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "../ClientViews/main.html",
            })
            .when("/suratmasuk", {
                templateUrl: "../ClientViews/suratmasuk.html",
                controller: "SuratMasukController"
            })

            .when("/AddSuratMasuk", {
                templateUrl: "../ClientViews/AddSuratMasuk.html",
                controller: "AddSuratMasukController"
            })

            .when("/suratkeluar", {
                templateUrl: "../ClientViews/suratkeluar.html",
                controller: "SuratMasukController"
            })
            
            ;
    })





    .factory("SuratMasukService", function ($http,$q) {
        var service = {};
        Datas = [];
        isInstance = false;
        service.Get = function ()
        {
            deferred = $q.defer();
            if (!this.isInstance) {
                $http({
                    method: 'get',
                    url: "/api/suratmasuk",
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    this.Datas=response.data;
                    deferred.resolve(this.Datas);
                    this.isInstance = true;
                }, function (error) {
                    alert(Helpers.getMessage(error.status, error.data));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(this.Datas);
            }

            return deferred.promise;

        }

        service.Insert = function (model) {
            deferred = $q.defer();
            if (!this.isInstance) {
                $http({
                    method: 'post',
                    url: "/api/suratmasuk",
                    data:model
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    this.Datas.push(response.data);
                    deferred.resolve(response.data);
                    this.isInstance= true;
                }, function (error) {
                    alert(Helpers.getMessage(error.status, error.data));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(this.Datas);
            }

            return deferred.promise;
        };

        service.Update = function (model) {
            $http(
                {
                    method: "Post",
                    ulr: '/api/SuratMasuk/put'
                }.then(function (response) {
                    model.KodeSurat = response.KodeSurat;
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                }
                    ));
        };

        service.Update = function (model) {
            $http(
                {
                    method: "Post",
                    ulr: '/api/SuratMasuk/Delete'
                }.then(function (response) {
                    this.Datas.S
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                }
                    ));
        };


        return service;

    })
    .factory("SuratKeluarService", function ($http) {
        var service = {};
        service.Datas = [];
        service.isInstance = false;
        service.Get = function () {
            if (this.isInstance) {
                $http(
                    {
                        method: "Get",
                        ulr: '/api/SuratKeluar/get'
                    }.then(function (response) {
                        this.Datas = response;
                        return this.Datas;
                    }, function (response) {
                        alert(response.Message);
                    }
                        ));
            } else {
                return this.Datas;
            }

        }

        service.Insert = function (model) {
            $http(
                {
                    method: "Post",
                    url: '/api/SuratMasuk/post',
                    data: model
                }.then(function (response) {
                    this.Datas.push(response);
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                }
                    ));
        };

        service.Update = function (model) {
            $http(
                {
                    method: "Post",
                    url: '/api/SuratMasuk/put',
                    data: model

                }.then(function (response) {
                    model.KodeSurat = response.KodeSurat;
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                }
                    ));
        };

        service.Update = function (model) {
            $http(
                {
                    method: "Post",
                    url: '/api/SuratMasuk/Delete',
                    data:model
                }.then(function (response) {
                    this.Datas.S
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                }
                    ));
        };


        return service;

    })




    .controller("SuratMasukController", function ($scope, SuratMasukService) {

        $scope.SuratMasuk = [];
        $scope.Init = function ()
        {
            SuratMasukService.Get().then(function (data) {
                $scope.SuratMasuk = data;
            });
        }
    })

    .controller("AddSuratMasukController", function ($scope,SuratMasukService) {
        $scope.AddNewItem = function(model)
        {
            var result = SuratMasukService.Insert(model);
            if (result == true)
            {
                model = {};
            }
        }
    })

    .controller("SuratKeluarController", function ($scope) {

    })

    .controller("DisposisiController", function ($scope) {

    })

;