using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Formatter
{
    public class DataBuilder
    {
        // filepath
        private readonly string _filePath;

        public DataBuilder(string filePath)
        {
            _filePath = filePath;
        }

        public void Build<T>(IEnumerable<T> srcList, IFormatter formatter )
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var titles = properties.Select(p => p.Name);

        }
    }
}
