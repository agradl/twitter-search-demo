using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using TwitterSearchDemo.Business;
using TwitterSearchDemo.Framework;
using TwitterSearchDemo.Framework.TwitterPoco;

namespace TwitterSearchDemo.Test.Business
{
    [TestClass]
    public class TestTwitterSearch
    {
        private ICallTwitterApi _api;
        private TwitterSearch _search;
        private IBuildRequestUrl _urlBuilder;

        [TestInitialize]
        public void Setup()
        {
            _urlBuilder = MockRepository.GenerateStrictMock<IBuildRequestUrl>();
            _api = MockRepository.GenerateStrictMock<ICallTwitterApi>();
            _search = new TwitterSearch(_urlBuilder, _api);
        }

        [TestMethod]
        public void ShouldBuildUrlAndCallApiOnCacheMiss()
        {
            var criteria = new TwitterSearchQuery {Query = "test"};
            _urlBuilder.Expect(x => x.GetUrlFor(criteria, 1, 100)).Return("url");
            _api.Expect(x => x.GetDataFor("url")).Return(GetRootResult(20));

            _search.GetResultsFor(criteria);
        }

        [TestMethod]
        public void ShouldNotBuildUrlOrCallApiOnCacheHit()
        {
            var criteria = new TwitterSearchQuery {Query = "test"};
            _urlBuilder.Expect(x => x.GetUrlFor(criteria, 1, 100)).Return("url");
            _api.Expect(x => x.GetDataFor("url")).IgnoreArguments().Return(GetRootResult(100));

            _search.GetResultsFor(criteria);

            criteria.Page = 2;
            _search.GetResultsFor(criteria);

            criteria.Page = 3;
            _search.GetResultsFor(criteria);
        }

        private Rootobject GetRootResult(int many)
        {
            var list = new List<TwitterResult>();
            for (int i = 0; i < many; i++)
            {
                list.Add(new TwitterResult());
            }
            return new Rootobject
                       {
                           results = list.ToArray()
                       };
        }
    }
}