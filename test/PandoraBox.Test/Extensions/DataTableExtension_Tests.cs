using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using PandoraBox.Extensions;

namespace PandoraBox.Test.Extensions
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void GetAge()
        {

        }
    }

    [TestClass]
    public class DataTableExtension_Tests
    {
        [TestMethod]
        public void ShouldGetList()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name", typeof(string)), new DataColumn("Age", typeof(int)) });
            var col1 = dt.NewRow();
            col1["Name"] = "John";
            col1["Age"] = 16;
            dt.Rows.Add(col1);
            var col2 = dt.NewRow();
            col2["Name"] = "Danny";
            col2["Age"] = 18;
            dt.Rows.Add(col2);
            var students = dt.Cast<Student>();
            Assert.AreEqual(dt.Rows.Count, students.Count);
        }
    }
}
