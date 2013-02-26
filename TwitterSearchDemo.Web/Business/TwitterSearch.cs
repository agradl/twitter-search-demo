using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TwitterSearchDemo.Framework;
using TwitterSearchDemo.Framework.TwitterPoco;

namespace TwitterSearchDemo.Business
{
    public class TwitterSearch : IManageTwitterSearch
    {
        public const string BaseUrl = "http://search.twitter.com/search.json";
        private readonly IBuildRequestUrl _urlBuilder;
        private readonly ICallTwitterApi _api;
        private const int PageSize = 10;

        private readonly ConcurrentDictionary<TwitterSearchQuery, CacheItem> _cache =
            new ConcurrentDictionary<TwitterSearchQuery, CacheItem>();

        public TwitterSearch(IBuildRequestUrl urlBuilder, ICallTwitterApi api)
        {
            _urlBuilder = urlBuilder;
            _api = api;
        }

        public List<TwitterResult> GetResultsFor(TwitterSearchQuery criteria)
        {
            if (!_cache.ContainsKey(criteria) || _cache[criteria].Results.Count < criteria.Page*PageSize)
            {
                PopulateMoreResultsFor(criteria);
            }
            return _cache[criteria].Results.Skip((criteria.Page - 1) * PageSize).Take(PageSize).ToList();
        }

        private void PopulateMoreResultsFor(TwitterSearchQuery criteria)
        {
            if (!_cache.ContainsKey(criteria))
            {
                _cache.TryAdd(criteria, new CacheItem(criteria));
                InitialPopulate(criteria);
            }
            else
            {
                lock (_cache[criteria])
                {
                    if (IsCacheMiss(criteria))
                    {
                        NextPopulate(criteria);
                    }
                }    
            }
            
        }

        private bool IsCacheMiss(TwitterSearchQuery criteria)
        {
            return _cache[criteria].Results.Count < criteria.Page*PageSize;
        }

        private void InitialPopulate(TwitterSearchQuery criteria)
        {
            var url = _urlBuilder.GetUrlFor(criteria, 1, 100);
            var data = _api.GetDataFor(url);
            _cache[criteria].Add(data);
        }

        private void NextPopulate(TwitterSearchQuery criteria)
        {
            var url = _cache[criteria].NextUrl;
            var data = _api.GetDataFor(url);
            _cache[criteria].Add(data);
        }

        class CacheItem
        {
            public CacheItem(TwitterSearchQuery key)
            {
                Key = key;
                Results = new List<TwitterResult>();
            }
            public void Add(Rootobject data)
            {
                Results.AddRange(data.results);
                NextUrl = BaseUrl + data.next_page;
            }
            public TwitterSearchQuery Key { get; set; }
            public List<TwitterResult> Results { get; set; }
            public string NextUrl { get; set; }
        }
    }
}