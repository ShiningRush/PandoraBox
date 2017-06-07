using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandoraBox.DataBuilds.Extensions;
using PandoraBox.DataBuilds.Formatter;
using PandoraBox.DataBuilds.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.Test.DataBuilds.Formatter
{
    [TestClass]
    public class ExcelFormatter_Tests
    {
        [TestMethod]
        public void Should_Read_CorrectValue()
        {
            var file = TestDatas.TempFilePath;
            var students = TestDatas.Students;
            students.ExportExcelFile(file);
            var option = new FormatOption(file);
            using (var formatter = new ExcelFormatter(option))
            {
                var actaulValue = formatter.ReadExcel<string>(2, 1);
                Assert.AreEqual("Steven", actaulValue);
            }
        }

        [TestMethod]
        public void Should_Write_CorrectValue_ByAddr()
        {
            var file = TestDatas.TempFilePath;
            var students = TestDatas.Students;
            students.ExportExcelFile(file);
            var option = new FormatOption(file);
            using (var formatter = new ExcelFormatter(option))
            {

                var changedValue = "Test";
                formatter.WriteExcel("A2", changedValue);
                var actaulValue = formatter.ReadExcel<string>(2, 1);

                Assert.AreEqual(changedValue, actaulValue);
            }
        }

        [TestMethod]
        public void Should_Write_CorrectValue_ByRowCol()
        {
            var file = TestDatas.TempFilePath;
            var students = TestDatas.Students;
            students.ExportExcelFile(file);
            var option = new FormatOption(file);
            using (var formatter = new ExcelFormatter(option))
            {

                var changedValue = "Test";
                formatter.WriteExcel(2, 1, changedValue);
                var actaulValue = formatter.ReadExcel<string>(2, 1);

                Assert.AreEqual(changedValue, actaulValue);
            }
        }
    }
}
