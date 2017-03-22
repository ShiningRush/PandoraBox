using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandoraBox.Extensions;

namespace PandoraBox.Test.Extensions
{
    [TestClass]
    public class StringExtension_Tests
    {
        [TestMethod]
        public void EmptyString_ShouldBe_True()
        {
            string empty = "";
            Assert.IsTrue(empty.IsNullOrEmpty());
        }
    }
}
