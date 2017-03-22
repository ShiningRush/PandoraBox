using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandoraBox.Extensions;

namespace PandoraBox.Test.Extensions
{
    [TestClass]
    public class ObjectExtension_Tests
    {
        [TestMethod]
        public void Should_Get_CorrectValue()
        {
            var ageAproperty = typeof(Student).GetProperty("Birthday");
            Assert.AreEqual(TestDatas.Students[0].GetPropertyValue(ageAproperty), TestDatas.Students[0].Birthday);
        }

        [TestMethod]
        public void Should_Set_CorrectValue()
        {
            var testValue = "_correctValue";
            var nameAproperty = typeof(Student).GetProperty("Name");
            var student = new Student();
            student.SetPropertyValue(nameAproperty, testValue);
            Assert.AreEqual(testValue, student.Name);
        }
    }
}
