using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Formatter
{
    public interface IFormatter
    {
        void Begin(string filePath);
        void Write(string content);
        void Save();
    }
}
