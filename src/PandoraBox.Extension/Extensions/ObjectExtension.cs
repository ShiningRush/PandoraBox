using PandoraBox.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.Extensions
{
    public static class ObjectExtension
    {
        public static void SetPropertyValue(this object @this, PropertyInfo property, object settingValue)
        {
            ReflectHelper.SetPropertyValue(property, @this, settingValue);
        }

        public static object GetPropertyValue(this object @this, PropertyInfo property)
        {
            return ReflectHelper.GetPropertyValue(property, @this);
        }

        public static T GetPropertyValue<T>(this object @this, PropertyInfo property)
        {
            return ReflectHelper.GetPropertyValue<T>(property, @this);
        }
    }
}
