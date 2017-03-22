using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PandoraBox.Extensions;

namespace PandoraBox.Runtime
{
    internal class ReflectHelper
    {
        public static void SetPropertyValue(PropertyInfo property, object settingObj, object settingValue)
        {
            property.SetValue(settingObj, settingValue);
        }

        public static object GetPropertyValue(PropertyInfo property, object gettingObj)
        {
            return property.GetValue(gettingObj);
        }

        public static T GetPropertyValue<T>(PropertyInfo property, object gettingObj)
        {
            return (T)Convert.ChangeType(property.GetValue(gettingObj), typeof(T));
        }

        public static List<PropertyInfo> GetMarkedProperty<T>(Type exportAttr)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .IfWhere(exportAttr != null, p => p.GetCustomAttribute(exportAttr) != null).ToList();
        }
    }
}
