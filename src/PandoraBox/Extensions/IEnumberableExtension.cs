using PandoraBox.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.Extensions
{
    public static class IEnumberableExtension
    {
        public static IEnumerable<T> IfWhere<T>(this IEnumerable<T> @this, bool condition, Func<T, bool> predicate)
        {
            if (condition)
            {
                return @this.Where(predicate);
            }

            return @this;
        }
    }
}
