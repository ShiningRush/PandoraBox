using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Formatter
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MarkAttribute : Attribute
    {
        public string MarkName { get; private set; }

        public MarkAttribute()
        {
        }

        public MarkAttribute(string markName)
        {
            this.MarkName = markName;
        }
    }
}
