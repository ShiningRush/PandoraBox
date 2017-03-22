using PandoraBox.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Formatter
{
    public class FormatOption
    {
        public string FilePath { get; set; }

        public bool HasColumnTitle { get; set; }

        public char Separator { get; set; }

        public string SheetName { get; set; }

        public MarkAttribute ExportAttr { get; set; }

        public FormatOption(
            string filePath,
            bool hasColumnTitle = true,
            char separator = ',',
            string sheetName = "autoExportResult",
            MarkAttribute exportAttr = null)
        {
            this.FilePath = filePath;
            this.HasColumnTitle = hasColumnTitle;
            this.Separator = separator;
            this.SheetName = sheetName;
            this.ExportAttr = exportAttr;
        }
    }

    public abstract class BaseFormatter : IDisposable
    {
        public FormatOption Option { get; private set; }

        protected abstract void BeginComponent();

        public BaseFormatter(FormatOption option)
        {
            this.Option = option;
            this.BeginComponent();
        }

        protected abstract void WriteByComponent(string combinedString);

        /// <param name="content"></param>
        public void WriteList<T>(IEnumerable<T> writeList, Type markAttr = null)
        {
            var properties = ReflectHelper.GetMarkedProperty<T>(markAttr); ;
            var titles = String.Join(this.Option.Separator.ToString(), properties.Select(p => p.Name));
            if(this.Option.HasColumnTitle)
                this.WriteByComponent(titles);

            foreach (var eachRow in writeList)
            {
                this.WriteObj(eachRow);
            }
        }

        public void WriteObj<T>(T writeObj, Type markAttr = null)
        {
            var properties = ReflectHelper.GetMarkedProperty<T>(markAttr); ;

            var stringBuilder = new StringBuilder();
            foreach (var eachProperty in properties)
            {
                stringBuilder.AppendFormat("{0}{1}", eachProperty.GetValue(writeObj), this.Option.Separator);
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            this.WriteByComponent(stringBuilder.ToString());
        }

        protected abstract void SaveComponent();
        public void Save()
        {
            this.SaveComponent();
        }

        protected abstract void DisposeComponent();
        private bool isDispoed = false;
        public void Dispose()
        {
            if (!isDispoed)
            {
                this.DisposeComponent();
            }

            isDispoed = true;
        }
    }
}
