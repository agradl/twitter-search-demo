using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterSearchDemo.Framework.Web;

namespace TwitterSearchDemo.Test.Framework
{
    [TestClass]
    public class TestExtensions
    {
        [TestMethod]
        public void ShouldIncludePublicNonNullProperties()
        {
            var obj = new TestObj {Property1 = ""};
            RouteValueDictionary result = obj.ToRouteValues();

            Assert.IsTrue(result.ContainsKey("Property1"));
            Assert.IsTrue(result.ContainsKey("Property2"));
            Assert.IsFalse(result.ContainsKey("Property3"));
        }

        [TestMethod]
        public void ShouldIncludeValuesOfPublicProperties()
        {
            var obj = new TestObj {Property1 = "test1", Property2 = 52};
            RouteValueDictionary result = obj.ToRouteValues();

            Assert.AreEqual("test1", result["Property1"]);
            Assert.AreEqual(52, result["Property2"]);
        }

        private class TestObj
        {
            public string Property1 { get; set; }
            public int Property2 { get; set; }
            public string Property3 { get; set; }
        }
    }
}