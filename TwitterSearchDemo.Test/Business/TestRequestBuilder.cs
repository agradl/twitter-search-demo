using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterSearchDemo.Business;
using TwitterSearchDemo.Framework;

namespace TwitterSearchDemo.Test.Business
{
    [TestClass]
    public class TestRequestBuilder
    {
        private RequestBuilder _builder;

        [TestInitialize]
        public void Setup()
        {
            _builder = new RequestBuilder();
        }

        [TestMethod]
        public void ShouldIncludeTwitterSearchApiInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "Test"}, 1, 1);
            Assert.IsTrue(url.Contains("http://search.twitter.com/search.json"));
        }

        [TestMethod]
        public void ShouldIncludeQueryInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "Test"}, 1, 1);
            Assert.IsTrue(url.Contains("Test"));
        }

        [TestMethod]
        public void ShouldIncludeEncodedQueryInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "#Test", SearchType = SearchType.Text}, 1, 1);
            Assert.IsTrue(url.Contains("%23Test"));
        }

        [TestMethod]
        public void ShouldIncludeResultTypeInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "Test", ResultType = ResultType.Popular}, 1, 1);
            Assert.IsTrue(url.Contains("result_type=Popular"));
        }

        [TestMethod]
        public void ShouldIncludeSearchTypeInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "Test", SearchType = SearchType.Hashtag}, 1,
                                            1);
            Assert.IsTrue(url.Contains("q=%23Test"));

            url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "Test", SearchType = SearchType.User}, 1, 1);
            Assert.IsTrue(url.Contains("q=%40Test"));

            url = _builder.GetUrlFor(new TwitterSearchQuery {Query = "Test", SearchType = SearchType.Text}, 1, 1);
            Assert.IsTrue(url.Contains("q=%22Test%22"));
        }

        [TestMethod]
        public void ShouldIncludePageSizeInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery { Query = "Test" }, 1, 1);
            Assert.IsTrue(url.Contains("rpp=1"));
        }

        [TestMethod]
        public void ShouldIncludePageInUrl()
        {
            string url = _builder.GetUrlFor(new TwitterSearchQuery { Query = "Test" }, 1, 1);
            Assert.IsTrue(url.Contains("page=1"));
        }
    }
}