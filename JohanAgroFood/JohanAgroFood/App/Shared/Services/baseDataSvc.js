app.service('baseDataSvc', ['$http', '$window',

    function ($http, $window) {

        var baseSvc = {
            executeQuery: executeQuery,
            save: save,
            remove: remove,
            removeEntity: removeEntity
        };
        return baseSvc;

        function _addHttpHeader() {

            try {

                //make authentication object
                var authentication = {
                    userPKID: userInfo.userPKID,
                    userID: userInfo.userName,
                    password: 'Admin',
                    userName: userInfo.userName,
                    pageID: pageInfo.id,
                    pageName: pageInfo.moduleName,
                    clientIPAddress: $window.location.href,
                    stateName: pageInfo.action,
                    PermissionType: ''
                }

                //encode authentication object
                var objJson = angular.toJson(authentication);
                var encodedData = window.btoa(objJson);

                //var _pageInfo = angular.copy(pageInfo);
                //_pageInfo.uRL = $window.location.href;
                //var userData = angular.toJson(userInfo) + "#" + angular.toJson(_pageInfo);
                //var encodedData = window.btoa(userData); // encode a string
                $http.defaults.headers.common.Authorization = 'Basic ' + encodedData;
            } catch (e) {
                throw e;
            }
        }

        function executeQuery(url, params) {
            try {
                return $http({
                    url: url,
                    method: "POST",
                    data: params
                }).then(function (results) {
                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        };

        function remove(url, entity, key) {
            try {
                //_addHttpHeader();

                //if (!hasAction) {
                //    url = url + 'Delete';
                //}

                return $http({
                    url: url,
                    method: "post",
                    params: { pk: (typeof key === "undefined" ? entity.id : entity[key]) }
                }).then(function (result) {
                    return result;
                }).catch(function (ex) {
                    ex.entity = entity;
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }

        function removeEntity(url, entity) {
            try {
                _addHttpHeader();

                return $http.post(url + 'Delete', entity).then(function (result) {
                    return result;
                }).catch(function (ex) {
                    throw ex;
                });

            } catch (ex) {
                throw ex;
            }
        }
        //To perform Server side insert/update/delete operation (Post to BreezeWebApi)
        function save(url, data) {
            //_addHttpHeader();

            //if (!hasAction) {
            //    url = url + 'saveChanges';
            //}

            return $http.post(url, data).then(function (results) {
                data = results.data;
                return data;
            }).catch(function (ex) {
                throw ex;
            });
        }
    }
]);
