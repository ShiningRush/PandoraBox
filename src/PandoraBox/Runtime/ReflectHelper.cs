using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using PandoraBox.Extensions;


namespace PandoraBox.Runtime
{
    public class ReflectHelper
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
        public static List<PropertyInfo> GetMarkedProperty(Type srcType,Type exportAttr)
        {
            return srcType.GetTypeInfo().DeclaredProperties
                        .IfWhere(exportAttr != null, p => p.GetCustomAttribute(exportAttr) != null).ToList();
        }

        public static List<PropertyInfo> GetMarkedProperty<T>(Type exportAttr)
        {
            return GetMarkedProperty(typeof(T), exportAttr);
        }
    }
}
