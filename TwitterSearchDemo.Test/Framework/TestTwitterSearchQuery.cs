using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterSearchDemo.Framework;

namespace TwitterSearchDemo.Test.Framework
{
    [TestClass]
    public class TestTwitterSearchQuery
    {
        [TestMethod]
        public void TestEquality()
        {
            var criteria1 = new TwitterSearchQuery();
            var criteria2 = new TwitterSearchQuery();

            Assert.AreEqual(criteria1, criteria2);
        }

        [TestMethod]
        public void TestInequalityByQuery()
        {
            var criteria1 = new TwitterSearchQuery {Query = "test"};
            var criteria2 = new TwitterSearchQuery {Query = "test1"};

            Assert.AreNotEqual(criteria1, criteria2);
        }

        [TestMethod]
        public void TestInequalityByResultType()
        {
            var criteria1 = new TwitterSearchQuery {ResultType = ResultType.Mixed};
            var criteria2 = new TwitterSearchQuery {ResultType = ResultType.Popular};

            Assert.AreNotEqual(criteria1, criteria2);
        }

        [TestMethod]
        public void TestInequalityBySearchType()
        {
            var criteria1 = new TwitterSearchQuery {SearchType = SearchType.Hashtag};
            var criteria2 = new TwitterSearchQuery {SearchType = SearchType.Text};

            Assert.AreNotEqual(criteria1, criteria2);
        }
    }
}