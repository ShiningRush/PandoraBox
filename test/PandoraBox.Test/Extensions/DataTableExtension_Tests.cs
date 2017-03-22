using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using PandoraBox.Extensions;
using PandoraBox.DataBuilds.Formatter;

namespace PandoraBox.Test.Extensions
{

    [TestClass]
    public class DataTableExtension_Tests
    {
        [TestMethod]
        public void Should_Get_CorrectList()
        {
            var dt = TestDatas.StudentsTable;
            var students = dt.Cast<Student>();

            CollectionAssert.AreEqual(students, TestDatas.Students);
        }

        [TestMethod]
        public void Should_Get_Marked_CorrectList()
        {
            var dt = TestDatas.TeachersTable;
            var teachers = dt.Cast<Teacher>(typeof(MarkAttribute));
            var list = TestDatas.TeachersTable.Cast<Teacher>();
            list.ForEach(p => p.Salary = default(decimal));

            CollectionAssert.AreEqual(teachers, list);
        }
    }
}
