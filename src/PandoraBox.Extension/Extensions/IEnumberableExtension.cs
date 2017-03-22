using PandoraBox.DataBuilds.Formatter;
using PandoraBox.Runtime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.Extensions
{
    public static class IEnumberableExtension
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> @this, Type markAttr = null)
        {
            var dt = new DataTable(typeof(T).Name);
            var properties = ReflectHelper.GetMarkedProperty<T>(markAttr);

            foreach (var property in properties)
            {
                dt.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var eachRow in @this)
            {
                var newDataRow = dt.NewRow();
                foreach (var property in properties)
                {
                    newDataRow[property.Name] = eachRow.GetPropertyValue(property);
                }
                dt.Rows.Add(newDataRow);
            }

            return dt;
        }

        public static IEnumerable<T> IfWhere<T>(this IEnumerable<T> @this, bool condition, Func<T, bool> predicate)
        {
            if (condition)
            {
                return @this.Where(predicate);
            }

            return @this;
        }

        public static void ExportExcelFile<T>(this IEnumerable<T> @this, string filePath, bool hasTitle = true, char separator = ',')
        {
            DataBuilder.Instance.ExportFile(@this, filePath, FormatType.Excel, hasTitle, separator);
        }

        public static void ExportCsvFile<T>(this IEnumerable<T> @this, string filePath, bool hasTitle = true, char separator = ',')
        {
            DataBuilder.Instance.ExportFile(@this, filePath, FormatType.Csv, hasTitle, separator);
        }
    }
}
