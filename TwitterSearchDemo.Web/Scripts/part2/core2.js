var Demo = {};

Demo.Helpers = (function (undefined) {

    function getPage(array, page, pageSize) {
        var result = [];
        for (var i = (page - 1) * pageSize; i < page * pageSize; i++) {
            if (array[i] === undefined)
                return result;
            result.push(array[i]);
        }
        return result;
    }

    function addRange(array, items) {
        for (var i = 0; i < items.length; i++) {
            array.push(items[i]);
        }
    }

    return { getPage: getPage, addRange: addRange };
})();

Demo.Api = (function () {
    var call = function (url, success) {
        $.ajax({
            url: url,
            dataType: "jsonp",
            success: success
        });
    };
    return { getStatuses: call };
})();

Demo.SearchStore = (function () {
    var getUrlFromCriteria = function (data) {
        var base = "http://search.twitter.com/search.json";
        var queryStr = '?q=';
        if (data.searchType === 'text') {
            queryStr += '"' + data.query + '"';
        } else {
            queryStr += encodeURIComponent(data.searchType + data.query);
        }
        queryStr += "&page=" + data.page;
        queryStr += "&result_type=" + data.resultType;
        queryStr += "&rpp=100";
        return base + queryStr;
    };
    
    var getKeyFromCriteria = function(data) {
        return data.query + data.searchType + data.resultType;
    };
    
    var cacheItem = function (key, data) {
        var that = this;
        that.results = [];
        that.add = function (newData) {
            Demo.Helpers.addRange(that.results, newData.results);
            if (newData.next_page !== undefined)
                that.nextUrl = 'http://search.twitter.com/search.json' + newData.next_page;
        };
        that.add(data);
    };
    
    var searchStore = function() {
        var that = this;
        that.cache = {};
    };
    searchStore.prototype.getStatusesForCriteria = function (criteria, callback) {
        var that = this;
        var key = getKeyFromCriteria(criteria);
        var initialLoad = function () {
            var url = getUrlFromCriteria(criteria);

            Demo.Api.getStatuses(url, function (data) {
                that.cache[key] = new cacheItem(key, data);
                callback(Demo.Helpers.getPage(that.cache[key].results, criteria.page, 10));
            });
        };

        var loadFromNextUrl = function () {
            var url = that.cache[key].nextUrl;

            Demo.Api.getStatuses(url, function (data) {
                that.cache[key].add(data);
                callback(Demo.Helpers.getPage(that.cache[key].results, criteria.page, 10));
            });
        };

        var populateCache = function () {
            if (that.cache[key] === undefined) {
                initialLoad(key, criteria, callback);
            } else if (that.cache[key].nextUrl !== undefined) {
                loadFromNextUrl(key, criteria, callback);
            } else {
                callback([]);
            }
        };
        
        if (this.cache[key] === undefined || this.cache[key].results.length < criteria.page * 10) {
            populateCache();
        } else {
            callback(Demo.Helpers.getPage(this.cache[key].results, criteria.page, 10));
        }
    };
    return searchStore;
})();

Demo.ViewModels = (function () {
    var mainViewModel = function () {
        var searchStore = new Demo.SearchStore();
        var that = this;
        that.criteria = ko.observable(new search());
        that.results = ko.observableArray([]);
        that.page = ko.observable(1);
        that.loading = ko.observable(false);
        var refresh = function () {
            that.loading(true);
            that.results.removeAll();
            searchStore.getStatusesForCriteria({
                searchType: that.criteria().searchType(),
                resultType: that.criteria().resultType(),
                query: that.criteria().query(),
                page: that.page()
            }, function (data) {
                Demo.Helpers.addRange(that.results, $.map(data, function (elem) { return new result(elem); }));
                that.loading(false);
            });
        };

        that.changePage = function (newPage) {
            that.page(newPage);
            refresh();
        };


    };

    var result = function (data) {
        this.name = data.from_user_name;
        this.userUrl = "http://twitter.com/" + encodeURIComponent(data.from_user);
        this.profileUrl = data.profile_image_url;
        this.text = data.text;
        this.created = new Date(data.created_at).toLocaleString();
        this.retweets = (data.metadata.recent_retweets !== undefined) ? data.metadata.recent_retweets : 0;
    };

    var search = function () {
        var that = this;
        that.searchType = ko.observable('#');
        that.resultType = ko.observable('both');
        that.query = ko.observable('');
    };

    return { MainViewModel: mainViewModel };
})();