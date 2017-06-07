using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Formatter
{
    public enum FormatType
    {
        Excel,
        Csv
    }

    public class DataBuilder
    {
        private static object _lock = "lock";
        private static DataBuilder _instance = null;
        public static DataBuilder Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataBuilder();
                    }

                    return _instance;
                }
            }
        }

        public DataBuilder()
        {
        }

        public void ExportFile<T>(IEnumerable<T> dataSource, string filePath, FormatType formatType, bool hasTitle = true, char separator = ',')
        {
            BaseFormatter formatter;
            var option = new FormatOption(filePath: filePath, hasColumnTitle:hasTitle, separator:separator);
            switch (formatType)
            {
                case FormatType.Excel:
                    formatter = new ExcelFormatter(option);
                    break;
                case FormatType.Csv:
                    formatter = new TxtFormatter(option);
                    break;
                default:
                    formatter = new TxtFormatter(option);
                    break;
            }

            formatter.WriteList(dataSource);
            formatter.Save();
        }
    }
}
