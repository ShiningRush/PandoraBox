using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PandoraBox.Extensions;
using System.Collections;
using System.Data;
using System.IO;
using PandoraBox.DataBuilds.Formatter;

namespace PandoraBox.Test.Extensions
{
    [TestClass]
    public class IEnumberableExtension_Tests
    {
        [TestMethod]
        public void Should_Get_CorrectDataTable()
        {
            var students = TestDatas.Students;
            var dt = students.ToDataTable();
            var isEqual = true;

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for(var j = 0; j < dt.Columns.Count; j++)
                {
                    isEqual = dt.Rows[i][j].Equals(TestDatas.StudentsTable.Rows[i][j]);
                }
            }

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Should_Get_Marked_CorrectDataTable()
        {
            var isEqual = true;
            var teachers = TestDatas.Teachers;
            var dt = teachers.ToDataTable(typeof(MarkAttribute));
            isEqual = !dt.Columns.Contains("Salary");
            Assert.IsTrue(isEqual);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    isEqual = dt.Rows[i][j].Equals(TestDatas.TeachersTable.Rows[i][j]);
                }
            }

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Should_Export_ExcelFile()
        {
            var students = TestDatas.Students;
            var file = TestDatas.TempFilePath;
            students.ExportExcelFile(file);
            Assert.IsTrue(File.Exists(file));
        }

        [TestMethod]
        public void Should_Export_CsvFile()
        {
            var students = TestDatas.Students;
            var file = @"E:\text.csv";
            students.ExportCsvFile(file);
            Assert.IsTrue(File.Exists(file));
        }
    }
}
