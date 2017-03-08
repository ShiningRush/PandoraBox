using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.Extensions
{
    public static class DataTableExtension
    {
        public static List<TTarget> Cast<TTarget>(this DataTable @this) where TTarget : new()
        {
            var properties = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var result = new List<TTarget>();
            foreach(DataRow eachRow in @this.Rows)
            {
                var newRow = new TTarget();
                foreach (var targetProperty in properties)
                {
                    if (@this.Columns.Contains(targetProperty.Name))
                    {
                        targetProperty.SetValue(newRow, eachRow[targetProperty.Name]);
                    }
                }
                result.Add(newRow);
            }

            return result;
        }
    }
}
