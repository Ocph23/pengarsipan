angular.module("app", ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "../ClientViews/main.html",
                controller:"MainController"
            })

            .when("/suratmasuk", {
                templateUrl: "../ClientViews/suratmasuk.html",
                controller: "SuratMasukController"
            })

            .when("/AddSuratMasuk", {
                templateUrl: "../ClientViews/AddSuratMasuk.html",
                controller: "AddSuratMasukController"
            })

            .when("/AddSuratKeluar", {
                templateUrl: "../ClientViews/AddSuratKeluar.html",
                controller: "AddSuratKeluarController"
            })

            .when("/suratkeluar", {
                templateUrl: "../ClientViews/suratkeluar.html",
                controller: "SuratKeluarController"
            })

            .when("/ViewFile/:id", {
                templateUrl: "../ClientViews/ViewFile.html",
                controller: "ViewFileController"
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
                    method: "PUT",
                    url: '/api/SuratMasuk/' + model.SuratMasukId,
                    data: model
                }).then(function (response) {
                    alert("Data Tersimpan ...")
                }, function (response) {
                    alert(response.Message);
                });
        };

        service.Delete = function (model) {
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


        service.AddNewDisposisi = function (item, model)
        {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: "/api/disposisi",
                data: model
            }).then(function (response) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                angular.forEach(this.Datas, function (value, key) {
                    if (value.SuratMasukId == item.SuratMasukId) {
                        value.Disposisi = response.data;
                    }
                });
                deferred.resolve(response.data);
                }, function (error) {
                    alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }
        service.UpdateDisposisi = function (model) {
            deferred = $q.defer();
            $http({
                method: 'put',
                url: "/api/disposisi?id=" + model.Id,
                data: model
            }).then(function (response) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                angular.forEach(this.Datas, function (value, key) {
                    if (value.SuratMasukId == model.SuratMasukId) {
                        value.Disposisi = response.data;
                    }
                });
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }

        return service;

    })

    .factory("SuratKeluarService", function ($http,$q) {
        var service = {};
        service.Datas = [];
        service.isInstance = false;
        service.Get = function () {
            deferred = $q.defer();
            if (!this.isInstance) {
                $http({
                    method: 'get',
                    url: "/api/suratkeluar",
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    this.Datas = response.data;
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
            $http(
                {
                    method: "Post",
                    url: '/api/SuratKeluar',
                    data: model
                }).then(function (response) {
                    this.Datas.push(response);
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                }
                    );
        };

        service.Update = function (model) {
            $http(
                {
                    method: "PUT",
                    url: '/api/SuratKeluar/' + model.SuratMasukId,
                    data: model

                }).then(function (response) {
                    return true;
                }, function (response) {
                    alert(response.Message);
                    return false;
                });
        }

        service.Delete = function (model) {
            $http(
                {
                    method: "Post",
                    url: '/api/SuratKeluar/Delete',
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
    .controller("MainController", function ($scope,$http) {
        $scope.Summary = {};
        $scope.Init = function ()
        {
            $http({
                method: 'get',
                url: "/api/summary/1",
            }).then(function (response) {
                $scope.Summary = response.data;
            }, function (error) {
                alert(error.data);
                
            });

        }
    })


    .controller("SuratMasukController", function ($scope, SuratMasukService,$http,$sce,$rootScope) {

        $scope.SuratMasuk = [];
        $scope.Init = function ()
        {
            document.getElementById('fake-file-button-browse').addEventListener('click', function () {
                document.getElementById('file').click();
            });

            document.getElementById('file').addEventListener('change', function (item) {
                var el = document.getElementById('fake-file-button-browse');
                el.value =this.files[0].name;
            });


            SuratMasukService.Get().then(function (data) {
                $scope.SuratMasuk = data;
            });
        }

        $scope.EditSurat = function(item)
        {
            $rootScope.SuratMasuk = item;
        }

        $scope.SetSelectedItem = function (item)
        {
            $scope.SelectedItem = item;
            $scope.model = item.Disposisi;
            $scope.model.TglPenyelesaian = new Date(item.Disposisi.TglPenyelesaian);
        }


        $scope.AddNewDisposisi = function (model) {
            if (model.Id == undefined)
            {
                model.SuratMasukId = $scope.SelectedItem.SuratMasukId;
                SuratMasukService.AddNewDisposisi($scope.SelectedItem, model).then(
                    function (response) {
                        $scope.SelectedItem.Disposisi = response;
                    },
                    function (error) {
                        alert(error);
                    }

                );
            } else
            {
                SuratMasukService.UpdateDisposisi(model).then(
                    function (response) {
                        $scope.SelectedItem.Disposisi = response;
                        demo.showNotification('top', 'center','Data Tersimpan');
                    },
                    function (error) {
                        alert(error);
                    }

                );
            }
            

        }

        $scope.AddNewFile = function () {

            var f = document.getElementById("file");
            var res = f.files[0];

            var form = new FormData();

            form.append("file", res);
            form.append("SuratMasukId", $scope.SelectedItem.SuratMasukId);
            form.append("IsMasuk", "true");

            var settings = {
                "async": true,
                "crossDomain": true,
                "url": "/api/File/post",
                "method": "POST",
                "headers": {
                    "cache-control": "no-cache",
                },
                "processData": false,
                "contentType": false,
                "mimeType": "multipart/form-data",
                "data": form
            };
           // $scope.progressbar.start();
            $.ajax(settings).done(function (data, response) {
                if (response == "success") {
                    //  $scope.progressbar.complete();
                    $scope.SelectedItem.File = data;
                    alert("Berhasil menambah data");
                  
                } else {
                   // $scope.progressbar.reset();
                    alert(["Gagal Menambahkan data"]);
                }
            }).error(function (err, response) {
               // $scope.progressbar.reset();
                alert(err.responseText);
            });


        }
        $scope.pdfData = {};
      


    })

    .controller("AddSuratMasukController", function ($scope, SuratMasukService,$rootScope) {

        $scope.Init = function ()
        {
            $scope.model = $rootScope.SuratMasuk;
        }

        $scope.AddNewItem = function(model)
        {
            if (model.SuratMasukId != undefined && model.SuratMasukId >0)
            {
                var result = SuratMasukService.Update(model);
                if (result == true) {
                    $scope.model = {};
                    alert("Data tersimpan");
                }
            } else
            {
             
                var result = SuratMasukService.Insert(model);
                if (result == true) {
                    $scope.model = {};
                    alert("Data tersimpan");
                }
            }
           
        }

    })

    .controller("AddSuratKeluarController", function ($scope, SuratKeluarService, $rootScope) {

        $scope.Init = function ()
        {
            $scope.model = $rootScope.SuratKeluar;
        }
        $scope.AddNewItem = function (model) {
            if (model.SuratMasukId != undefined && model.SuratMasukId > 0) {
                var result = SuratKeluarService.Update(model);
                if (result == true) {
                    $scope.model = {};
                    alert("Data tersimpan");
                }
            } else {
                model.TanggalKeluar = new Date();
                var result = SuratKeluarService.Insert(model);
                if (result == true) {
                    $scope.model = {};
                    alert("Data tersimpan");
                }
            }
        }

       



    })

    .controller("SuratKeluarController", function ($scope, SuratKeluarService, $rootScope) {
        SuratKeluarService.Get().then(function (data) {
            $scope.SuratKeluar = data;
        });
        $scope.EditSurat = function (item) {
            $rootScope.SuratKeluar = item;
        }
        $scope.SetSelectedItem = function (item) {
            $scope.SelectedItem = item;
            $scope.model = item.Disposisi;
            $scope.model.TglPenyelesaian = new Date(item.Disposisi.TglPenyelesaian);
        }
        $scope.AddNewFile = function () {

            var f = document.getElementById("file");
            var res = f.files[0];

            var form = new FormData();

            form.append("file", res);
            form.append("SuratMasukId", $scope.SelectedItem.SuratMasukId);
            form.append("IsMasuk", "false");

            var settings = {
                "async": true,
                "crossDomain": true,
                "url": "/api/File/post",
                "method": "POST",
                "headers": {
                    "cache-control": "no-cache",
                },
                "processData": false,
                "contentType": false,
                "mimeType": "multipart/form-data",
                "data": form
            };
            // $scope.progressbar.start();
            $.ajax(settings).done(function (data, response) {
                if (response == "success") {
                    //  $scope.progressbar.complete();
                    $scope.SelectedItem.File = data;
                    alert("Berhasil menambah data");

                } else {
                    // $scope.progressbar.reset();
                    alert(["Gagal Menambahkan data"]);
                }
            }).error(function (err, response) {
                // $scope.progressbar.reset();
                alert(err.responseText);
            });


        }
        $scope.pdfData = {};

    })

    .controller("DisposisiController", function ($scope) {

    })


    .controller("ViewFileController", function ($scope, $routeParams,$http,$sce) {
        $scope.File = $routeParams.id;
        $scope.pdfData;
        $http({
            method: 'get',
            url: "/api/File?file=" + $scope.File,
            responseType: 'arraybuffer',
        }).then(function (response) {
            // With the data succesfully returned, we can resolve promise and we can access it in controller
            headers = response.headers();

            var filename = headers['x-filename'];
            var contentType = headers['content-type'];

            var linkElement = document.createElement('a');
            try {
                var blob = new Blob([response.data], { type: contentType });
                var url = window.URL.createObjectURL(blob);
                $scope.pdfData = $sce.trustAsResourceUrl(url);
            } catch (ex) {
                console.log(ex);
            }
        }, function (error) {
            alert(Helpers.getMessage(error.status, error.data));
            // deferred.reject(error);
        });
    })

;