app.service('loader', ['$http',
    function ($http) {
        this.getContacts = function () {
            return $http({
                method: "GET",
                url: helper.actionUrl("GetContacts", "Home"),
            });
        };

        this.getContact = function (id) {
            return $http({
                method: "GET",
                url: helper.actionUrl("GetContact", "Home"),
                params: {
                    id: id
                }
            });
        };
    }]);


app.factory('modalFactory', [
    "$modal", function(m) {
        return {
            createDetailModal: function(id, loader) {
                return m.open({
                    templateUrl: helper.actionUrl("Details", "Home"),
                    controller: details.controller,
                    resolve: {
                        id: function() {
                            return id;
                        },
                        loader: function() {
                            return loader;
                        }
                    }
                });
            }
        };
    }
]);

app.controller('contactsController', ['$scope', '$http', 'loader', 'modalFactory',
    function ($scope, $http, loader, modalFactory) {
        var __self = this;
        this.pageSize = 24;
        $scope.all_contacts = [];
        $scope.contacts = [];

        this.buildPages = function() {
            $scope.pages = [];
            for (var i = 0; i < $scope.pageCount; i++) {
                $scope.pages.push(i + 1);
            }
        };

        $scope.init = function () {
            loader.getContacts().then(function(res) {
                $scope.all_contacts = res.data;
                $scope.pageCount = Math.ceil($scope.all_contacts.length / __self.pageSize);
                $scope.contacts = $scope.all_contacts.slice(0, __self.pageSize);
                __self.buildPages();
                $scope.currentPage = 0;
            });
        };

        $scope.loadPage = function (page) {
            var index = page * __self.pageSize;
            $scope.contacts = $scope.all_contacts.slice(index, index + __self.pageSize);
            $scope.currentPage = page;
//            loader.getContacts(page).then(function (res) {
//
//                $scope.contacts = res.data.Contacts;
//                $scope.pageCount = res.data.PageCount;
//                if ($scope.contacts.length == 0) {
//                    //No contacts have been loaded
//                    if (page > $scope.pageCount - 1 && $scope.pageCount > 0) {
//                        //If we deleted whole page.
//                        $scope.loadPage(page - 1);
//                        __self.buildPages();
//                    }
//                }
//                $scope.currentPage = page;
//            });
        };

        $scope.refresh

        $scope.editContact = function(id) {
            var modalInstance = modalFactory.createDetailModal(id, loader);
            modalInstance.result.then(function (res) {
                if (res == "updated") {
                    $scope.loadPage($scope.currentPage);
                }
            }, function(res) {
                if (res == "deleted") {
                    $scope.loadPage($scope.currentPage);
                }
            });
        };

        $scope.addContact = function () {
            var modalInstance = modalFactory.createDetailModal();
            modalInstance.result.then(function () {

            });
        };
    }]);