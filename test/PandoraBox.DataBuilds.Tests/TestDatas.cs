using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandoraBox.DataBuilds.Formatter;

namespace PandoraBox.DataBuilds.Tests
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }

        public override bool Equals(object obj)
        {
            var changeType = obj as Student;
            if (changeType != null)
            {
                return this.Age == changeType.Age &&
                        this.Birthday == changeType.Birthday &&
                        this.Name == changeType.Name;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Teacher
    {
        [Mark]
        public string Name { get; set; }
        [Mark]
        public int Age { get; set; }
        [Mark]
        public DateTime Birthday { get; set; }
        public decimal Salary { get; set; }

        public override bool Equals(object obj)
        {
            var changeType = obj as Teacher;
            if (changeType != null)
            {
                return this.Age == changeType.Age &&
                        this.Birthday == changeType.Birthday &&
                        this.Name == changeType.Name &&
                        this.Salary == changeType.Salary;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class TestDatas
    {
        public static string TempFilePath => @"E:\text.xlsx";

        public static List<Student> Students { get; set; }
        public static List<Teacher> Teachers { get; set; }

        public static DataTable StudentsTable { get; set; }
        public static DataTable TeachersTable { get; set; }
        static TestDatas()
        {
            Students = new List<Student>()
            {
                new Student(){  Name="Steven", Age=20, Birthday = new DateTime(1990, 2, 20)  },
                new Student(){  Name="Smith", Age=21, Birthday = new DateTime(1991, 3, 21)  },
                new Student(){  Name="Amy", Age=22, Birthday = new DateTime(1992, 4, 22)  },
                new Student(){  Name="Ten", Age=23, Birthday = new DateTime(1993, 5, 23)  },
            };

            Teachers = new List<Teacher>()
            {
                new Teacher(){  Name="Mr.Steven", Age=40, Birthday = new DateTime(1980, 2, 20), Salary = 150  },
                new Teacher(){  Name="Mr.Smith", Age=41, Birthday = new DateTime(1981, 3, 21), Salary = 200  },
                new Teacher(){  Name="Ms.Amy", Age=42, Birthday = new DateTime(1982, 4, 22), Salary = 250  },
                new Teacher(){  Name="Mr.Ten", Age=43, Birthday = new DateTime(1983, 5, 23), Salary = 300  },
            };

            var dt = new DataTable("Student");
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Birthday", typeof(DateTime));

            dt.Rows.Add("Steven", 20, "1990/2/20");
            dt.Rows.Add("Smith", 21, "1991/3/21");
            dt.Rows.Add("Amy", 22, "1992/4/22");
            dt.Rows.Add("Ten", 23, "1993/5/23");
            StudentsTable = dt;

            var dt2 = new DataTable("Teacher");
            dt2.Columns.Add("Name", typeof(string));
            dt2.Columns.Add("Age", typeof(int));
            dt2.Columns.Add("Birthday", typeof(DateTime));
            dt2.Columns.Add("Salary", typeof(decimal));

            dt2.Rows.Add("Mr.Steven", 40, "1980/2/20", 150);
            dt2.Rows.Add("Mr.Smith", 41, "1981/3/21", 200);
            dt2.Rows.Add("Ms.Amy", 42, "1982/4/22", 250);
            dt2.Rows.Add("Mr.Ten", 43, "1983/5/23", 300);
            TeachersTable = dt2;
        }
    }
}
