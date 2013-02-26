using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterSearchDemo.Framework.Web;

namespace TwitterSearchDemo.Test.Framework
{
    [TestClass]
    public class TestValidation
    {
        private EnumRadioAttribute _attribute;
        private ModelMetadata _meta;
        [TestInitialize]
        public void Setup()
        {
            _attribute = new EnumRadioAttribute(typeof (TestEnum));
            _meta = new ModelMetadata(new EmptyModelMetadataProvider(),null, null,  typeof (TestValidation), null);
        }

        [TestMethod]
        public void ShouldPopulateTemplateHint()
        {
            _attribute.OnMetadataCreated(_meta);

            Assert.IsNotNull(_meta.TemplateHint);
        }

        [TestMethod]
        public void ShouldPopulateAdditionalValuest()
        {
            _attribute.OnMetadataCreated(_meta);

            Assert.IsNotNull(_meta.AdditionalValues["EnumValues"]);
            Assert.IsTrue(_meta.AdditionalValues["EnumValues"] is IEnumerable<SelectListItem>);
            var typedMeta = ((IEnumerable<SelectListItem>)_meta.AdditionalValues["EnumValues"]);
            Assert.IsTrue(typedMeta.Any(x => x.Value == TestEnum.Test1.ToString()));
            Assert.IsTrue(typedMeta.Any(x => x.Text == TestEnum.Test1.ToString().ToLower()));
            Assert.IsTrue(typedMeta.Any(x => x.Value == TestEnum.Test2.ToString()));
            Assert.IsTrue(typedMeta.Any(x => x.Text == TestEnum.Test2.ToString()));
            Assert.IsTrue(typedMeta.Any(x => x.Value == TestEnum.Test3.ToString()));
            Assert.IsTrue(typedMeta.Any(x => x.Text == TestEnum.Test3.ToString()));


        }

        [TestMethod]
        public void ValueIsOptional()
        {
            Assert.IsTrue(_attribute.IsValid(null));
        }

        [TestMethod]
        public void EnumValuesAreValid()
        {
            Assert.IsTrue(_attribute.IsValid(TestEnum.Test1));
            Assert.IsTrue(_attribute.IsValid(TestEnum.Test2));
            Assert.IsTrue(_attribute.IsValid(TestEnum.Test3));
        }

        [TestMethod]
        public void NonEnumValuesAreNotValid()
        {
            Assert.IsFalse(_attribute.IsValid("Test12"));
        }

        private enum TestEnum
        {
            [System.ComponentModel.Description("test1")] Test1,
            Test2,
            Test3
        }
    }
}