using PandoraBox.Runtime;
using PandoraBox.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Extensions
{
    public static class DataTableExtension
    {
        public static List<TTarget> Cast<TTarget>(this DataTable @this, Type markAttr = null) where TTarget : new()
        {
            var properties = ReflectHelper.GetMarkedProperty<TTarget>(markAttr);
            var result = new List<TTarget>();
            foreach(DataRow eachRow in @this.Rows)
            {
                var newRow = new TTarget();
                foreach (var targetProperty in properties)
                {
                    if (@this.Columns.Contains(targetProperty.Name) && eachRow[targetProperty.Name] != DBNull.Value)
                    {
                        newRow.SetPropertyValue(targetProperty, eachRow[targetProperty.Name]);
                    }
                }
                result.Add(newRow);
            }

            return result;
        }
    }
}
