(function (helper) {
    helper.actionUrl = function (action, controller, par, area) {
        var url = window.sessionStorage.baseUrl;
        if (area) {
            url = url + area + '/';
        }
        url = url + controller;
        if (action != "Index") {
            url = url + '/' + action;
        }
        if (par) {
            url = url + '?' + $.param(par);
        }
        return url;
    };
}(window.helper = window.helper || {}));